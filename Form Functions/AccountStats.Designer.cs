using System.Windows.Forms;
namespace Form_Functions
{
    partial class AccountStats : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 69D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 30D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0.532D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0.11D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.UserPicutreBox = new System.Windows.Forms.PictureBox();
            this.PasswordTitleLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.TotalMessagesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.TotalOnlineTimeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DateCreatedTitle = new System.Windows.Forms.Label();
            this.DateCreatedLabel = new System.Windows.Forms.Label();
            this.TotalTimeTitle = new System.Windows.Forms.Label();
            this.TotalTimeLabel = new System.Windows.Forms.Label();
            this.TotalMessagesTitle = new System.Windows.Forms.Label();
            this.TotalMessagesLabel = new System.Windows.Forms.Label();
            this.HasAdminPrivilegesTitle = new System.Windows.Forms.Label();
            this.HasAdminPrivilegesLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UserPicutreBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalMessagesChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalOnlineTimeChart)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Inter SemiBold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.Location = new System.Drawing.Point(218, 87);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(116, 39);
            this.UserNameLabel.TabIndex = 0;
            this.UserNameLabel.Text = "Admin";
            // 
            // UserPicutreBox
            // 
            this.UserPicutreBox.BackgroundImage = global::Form_Functions.Properties.Resources._3840x2160_lake_dark_night_starry_sky_landscape;
            this.UserPicutreBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UserPicutreBox.Location = new System.Drawing.Point(12, 12);
            this.UserPicutreBox.Name = "UserPicutreBox";
            this.UserPicutreBox.Size = new System.Drawing.Size(200, 200);
            this.UserPicutreBox.TabIndex = 1;
            this.UserPicutreBox.TabStop = false;
            // 
            // PasswordTitleLabel
            // 
            this.PasswordTitleLabel.AutoSize = true;
            this.PasswordTitleLabel.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTitleLabel.Location = new System.Drawing.Point(12, 215);
            this.PasswordTitleLabel.Name = "PasswordTitleLabel";
            this.PasswordTitleLabel.Size = new System.Drawing.Size(117, 26);
            this.PasswordTitleLabel.TabIndex = 2;
            this.PasswordTitleLabel.Text = "Password";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Inter SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(12, 241);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(45, 23);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "test";
            // 
            // TotalMessagesChart
            // 
            this.TotalMessagesChart.BorderlineColor = System.Drawing.Color.Black;
            this.TotalMessagesChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea1.Name = "TotalTimeOnline";
            this.TotalMessagesChart.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.DockedToChartArea = "TotalTimeOnline";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            this.TotalMessagesChart.Legends.Add(legend1);
            this.TotalMessagesChart.Location = new System.Drawing.Point(287, 3);
            this.TotalMessagesChart.Name = "TotalMessagesChart";
            series1.ChartArea = "TotalTimeOnline";
            series1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.LabelBackColor = System.Drawing.Color.White;
            series1.Legend = "Legend1";
            series1.LegendText = "Total Messages";
            series1.Name = "Series1";
            dataPoint1.Label = "You";
            dataPoint2.Label = "Average";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            this.TotalMessagesChart.Series.Add(series1);
            this.TotalMessagesChart.Size = new System.Drawing.Size(278, 634);
            this.TotalMessagesChart.TabIndex = 4;
            this.TotalMessagesChart.Text = "AverageStats";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Average Messages Stats";
            this.TotalMessagesChart.Titles.Add(title1);
            // 
            // TotalOnlineTimeChart
            // 
            this.TotalOnlineTimeChart.BorderlineColor = System.Drawing.Color.Black;
            this.TotalOnlineTimeChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea2.AxisX2.Title = "vvhk";
            chartArea2.AxisY2.Title = "guig";
            chartArea2.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.Name = "TotalTimeOnline";
            this.TotalOnlineTimeChart.ChartAreas.Add(chartArea2);
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.DockedToChartArea = "TotalTimeOnline";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.IsDockedInsideChartArea = false;
            legend2.Name = "Legend1";
            this.TotalOnlineTimeChart.Legends.Add(legend2);
            this.TotalOnlineTimeChart.Location = new System.Drawing.Point(19, 3);
            this.TotalOnlineTimeChart.Name = "TotalOnlineTimeChart";
            series2.ChartArea = "TotalTimeOnline";
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.Label = "Total Time Online";
            series2.LabelBackColor = System.Drawing.Color.White;
            series2.Legend = "Legend1";
            series2.LegendText = "Total Time Online";
            series2.Name = "Series1";
            dataPoint3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.None;
            dataPoint3.IsVisibleInLegend = true;
            dataPoint3.Label = "You";
            dataPoint3.LabelBackColor = System.Drawing.Color.White;
            dataPoint4.Label = "Average";
            dataPoint4.LabelBackColor = System.Drawing.Color.White;
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            this.TotalOnlineTimeChart.Series.Add(series2);
            this.TotalOnlineTimeChart.Size = new System.Drawing.Size(273, 634);
            this.TotalOnlineTimeChart.TabIndex = 5;
            this.TotalOnlineTimeChart.Text = "AverageStats";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "Average Time Stats";
            this.TotalOnlineTimeChart.Titles.Add(title2);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TotalOnlineTimeChart);
            this.panel1.Controls.Add(this.TotalMessagesChart);
            this.panel1.Location = new System.Drawing.Point(479, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(565, 642);
            this.panel1.TabIndex = 6;
            // 
            // DateCreatedTitle
            // 
            this.DateCreatedTitle.AutoSize = true;
            this.DateCreatedTitle.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateCreatedTitle.Location = new System.Drawing.Point(7, 288);
            this.DateCreatedTitle.Name = "DateCreatedTitle";
            this.DateCreatedTitle.Size = new System.Drawing.Size(153, 26);
            this.DateCreatedTitle.TabIndex = 7;
            this.DateCreatedTitle.Text = "Date Created";
            // 
            // DateCreatedLabel
            // 
            this.DateCreatedLabel.AutoSize = true;
            this.DateCreatedLabel.Font = new System.Drawing.Font("Inter SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateCreatedLabel.Location = new System.Drawing.Point(13, 314);
            this.DateCreatedLabel.Name = "DateCreatedLabel";
            this.DateCreatedLabel.Size = new System.Drawing.Size(45, 23);
            this.DateCreatedLabel.TabIndex = 8;
            this.DateCreatedLabel.Text = "test";
            // 
            // TotalTimeTitle
            // 
            this.TotalTimeTitle.AutoSize = true;
            this.TotalTimeTitle.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalTimeTitle.Location = new System.Drawing.Point(7, 359);
            this.TotalTimeTitle.Name = "TotalTimeTitle";
            this.TotalTimeTitle.Size = new System.Drawing.Size(304, 26);
            this.TotalTimeTitle.TabIndex = 9;
            this.TotalTimeTitle.Text = "Total Amount of time Online";
            // 
            // TotalTimeLabel
            // 
            this.TotalTimeLabel.AutoSize = true;
            this.TotalTimeLabel.Font = new System.Drawing.Font("Inter SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalTimeLabel.Location = new System.Drawing.Point(12, 385);
            this.TotalTimeLabel.Name = "TotalTimeLabel";
            this.TotalTimeLabel.Size = new System.Drawing.Size(45, 23);
            this.TotalTimeLabel.TabIndex = 10;
            this.TotalTimeLabel.Text = "test";
            // 
            // TotalMessagesTitle
            // 
            this.TotalMessagesTitle.AutoSize = true;
            this.TotalMessagesTitle.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalMessagesTitle.Location = new System.Drawing.Point(7, 434);
            this.TotalMessagesTitle.Name = "TotalMessagesTitle";
            this.TotalMessagesTitle.Size = new System.Drawing.Size(344, 26);
            this.TotalMessagesTitle.TabIndex = 11;
            this.TotalMessagesTitle.Text = "Total Amount of messages sent";
            // 
            // TotalMessagesLabel
            // 
            this.TotalMessagesLabel.AutoSize = true;
            this.TotalMessagesLabel.Font = new System.Drawing.Font("Inter SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalMessagesLabel.Location = new System.Drawing.Point(13, 460);
            this.TotalMessagesLabel.Name = "TotalMessagesLabel";
            this.TotalMessagesLabel.Size = new System.Drawing.Size(0, 23);
            this.TotalMessagesLabel.TabIndex = 12;
            // 
            // HasAdminPrivilegesTitle
            // 
            this.HasAdminPrivilegesTitle.AutoSize = true;
            this.HasAdminPrivilegesTitle.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HasAdminPrivilegesTitle.Location = new System.Drawing.Point(7, 495);
            this.HasAdminPrivilegesTitle.Name = "HasAdminPrivilegesTitle";
            this.HasAdminPrivilegesTitle.Size = new System.Drawing.Size(258, 26);
            this.HasAdminPrivilegesTitle.TabIndex = 13;
            this.HasAdminPrivilegesTitle.Text = "Admin Priviliges Status";
            // 
            // HasAdminPrivilegesLabel
            // 
            this.HasAdminPrivilegesLabel.AutoSize = true;
            this.HasAdminPrivilegesLabel.Font = new System.Drawing.Font("Inter SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HasAdminPrivilegesLabel.Location = new System.Drawing.Point(12, 521);
            this.HasAdminPrivilegesLabel.Name = "HasAdminPrivilegesLabel";
            this.HasAdminPrivilegesLabel.Size = new System.Drawing.Size(45, 23);
            this.HasAdminPrivilegesLabel.TabIndex = 14;
            this.HasAdminPrivilegesLabel.Text = "test";
            // 
            // AccountStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 650);
            this.Controls.Add(this.HasAdminPrivilegesLabel);
            this.Controls.Add(this.HasAdminPrivilegesTitle);
            this.Controls.Add(this.TotalMessagesLabel);
            this.Controls.Add(this.TotalMessagesTitle);
            this.Controls.Add(this.TotalTimeLabel);
            this.Controls.Add(this.TotalTimeTitle);
            this.Controls.Add(this.DateCreatedLabel);
            this.Controls.Add(this.DateCreatedTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.PasswordTitleLabel);
            this.Controls.Add(this.UserPicutreBox);
            this.Controls.Add(this.UserNameLabel);
            this.Name = "AccountStats";
            this.Text = "AccountSettings";
            this.Load += new System.EventHandler(this.AccountStats_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UserPicutreBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalMessagesChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalOnlineTimeChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.PictureBox UserPicutreBox;
        private System.Windows.Forms.Label PasswordTitleLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart TotalMessagesChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart TotalOnlineTimeChart;
        private Panel panel1;
        private Label DateCreatedTitle;
        private Label DateCreatedLabel;
        private Label TotalTimeTitle;
        private Label TotalTimeLabel;
        private Label TotalMessagesTitle;
        private Label TotalMessagesLabel;
        private Label HasAdminPrivilegesTitle;
        private Label HasAdminPrivilegesLabel;
    }
}