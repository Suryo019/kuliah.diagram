using diagram.Services;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;

namespace diagram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DateTime firstDate;
            DateTime lastDate;

            ObservableCollection<DateTimePoint> dates = new ObservableCollection<DateTimePoint>();

            DataTable data = DbHelper.GetInstance().ExecuteQuery("SELECT * FROM public.orders");

            firstDate = (DateTime)data.Rows[0]["order_date"];
            lastDate = (DateTime)data.Rows[data.Rows.Count - 1]["order_date"];

            TimeSpan duration = lastDate - firstDate;

            foreach (DataRow row in data.Rows)
            {
                DateTime tanggal = (DateTime)row["order_date"];
                float total = (float)row["freight"];

                // kondisi jika durasi hari lebih dari 365 hari
                if (duration.Days >= 365)
                {
                    if (dates.Count == 0)
                    {
                        DateTime now = DateTime.Now;
                        for (int i = firstDate.Year; i <= firstDate.Year + duration.Days / 365; i++)
                        {
                            dates.Add(new DateTimePoint(new DateTime(i, now.Month, now.Day), 0));
                        }
                    }
                    var datePoint = dates.SingleOrDefault(e => e.DateTime.Year == tanggal.Year);
                }
                // kondisi jika durasi hari lebih dari 30 hari
                else if (duration.Days >= 30)
                {
                    if (dates.Count == 0)
                    {
                        DateTime now = DateTime.Now;
                        for (int i = firstDate.Month; i <= firstDate.Month + (duration.Days % 365) / 30; i++)
                        {
                            dates.Add(new DateTimePoint(new DateTime(now.Year, i, now.Day), 0));
                        }
                    }
                    dates.Where((e) => e.DateTime.Month == tanggal.Month && e.DateTime.Year == tanggal.Year).Single().Value += total;
                }
                // jika durasi lebih dari 1 hari
                else if (duration.Days >= 1)
                {
                    if (dates.Count == 0)
                    {
                        DateTime now = DateTime.Now;
                        for (int i = 0; i <= 30; i++)
                        {
                            dates.Add(new DateTimePoint(new DateTime(now.Year, now.Month, i), 0));
                        }
                    }
                    dates.Where((e) => e.DateTime.Day == tanggal.Day).Single().Value += total;
                }
                // jika kondisi kurang dari atau sama dengan 1 hari
                else
                {
                    if (dates.Count == 0)
                    {
                        DateTime now = DateTime.Now;
                        for (int i = 0; i <= 23; i++)
                        {
                            dates.Add(new DateTimePoint(new DateTime(now.Year, now.Month, now.Day, i, 0, 0), 0));
                        }
                    }
                    dates.Where((e) => e.DateTime.Hour == tanggal.Hour).Single().Value += total;
                }
            }

            chart_pengunjung_harian.Title = new LabelVisual
            {
                Text = "Laporan Pengunjung",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

            chart_pengunjung_harian.Series = new ISeries[]{
                new ColumnSeries<DateTimePoint>
                {
                    TooltipLabelFormatter = (chartPoint) => $"{new DateTime((long) chartPoint.SecondaryValue):HH:00}: {chartPoint.PrimaryValue:N0}",
                    Name = "Pengujung",
                    Values = dates
                }
            };

            chart_pengunjung_harian.XAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => new DateTime((long) value).ToString("MMMM dd"),
                    LabelsRotation = 80,
                    UnitWidth = TimeSpan.FromDays(1).Ticks, 
                    MinStep = TimeSpan.FromDays(1).Ticks
                }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void cartesianChart1_Load(object sender, EventArgs e)
        {

        }
    }
}