namespace diagram
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chart_pengunjung_harian = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            SuspendLayout();
            // 
            // chart_pengunjung_harian
            // 
            chart_pengunjung_harian.Dock = DockStyle.Fill;
            chart_pengunjung_harian.Location = new Point(0, 0);
            chart_pengunjung_harian.Margin = new Padding(3, 4, 3, 4);
            chart_pengunjung_harian.Name = "chart_pengunjung_harian";
            chart_pengunjung_harian.Size = new Size(914, 600);
            chart_pengunjung_harian.TabIndex = 0;
            chart_pengunjung_harian.Load += cartesianChart1_Load;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(chart_pengunjung_harian);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart chart_pengunjung_harian;
    }
}