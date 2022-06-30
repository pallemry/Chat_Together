#nullable enable
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Mail;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
// ReSharper disable MemberCanBePrivate.Global

namespace Form_Functions
{
    public partial class AccountStats
    {
        public JsonNode UserAsJsonNode { get; }
        public static readonly Image DefaultUserProfileImage = Image.FromFile(Globals.GetImageResourcesPath() + "\\def\\defaultUser.png");

        public bool HasAdminPrivileges
        {
            get => _hasAdminPrivileges;
            set
            {
                _hasAdminPrivileges = value;

                HasAdminPrivilegesLabel.Text = value ? @"Activated ✔️" : @"Deactivated ❌";
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                UserNameLabel.Text = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            { 
               _password = value;
               if (IsHandleCreated)
               {
                   Invoke((MethodInvoker) delegate {
                       PasswordLabel.Text = string.Concat(Enumerable.Repeat("*", value.Length));
                   });
               }
            }
        }


        private string _password = null!;
        private string _userName = null!;
        private bool _hasAdminPrivileges = false;
        public Image? Pfp { get; }


        public AccountStats(JsonNode userAsJsonNode, SmtpClient smtpClient, Image pfp = null!)
        {
            UserAsJsonNode = userAsJsonNode;
            InitializeComponent();
            var dateOfRegistration = GetStringFromJsonProperty(userAsJsonNode,
                                                               UserTableLiterals.DateOfRegistration);

            DateCreatedLabel.Text = dateOfRegistration.Equals("null") ? "Unknown" : $"{DateTime.Parse(dateOfRegistration):f}";

            var totalTicks = long.Parse(GetStringFromJsonProperty(userAsJsonNode, UserTableLiterals.TotalAmountOfTicksOnline));
            var totalTimeSpan = TimeSpan.FromTicks(totalTicks);

            TotalTimeLabel.Text = $@"{string.Format(new TimeSpanFormatProvider(), "{0:N}", totalTimeSpan)}";
            TotalMessagesLabel.Text = GetStringFromJsonProperty(userAsJsonNode, UserTableLiterals.TotalNumMessagesSent);
            
            UserName = GetStringFromJsonProperty(userAsJsonNode, UserTableLiterals.Username);
            Password = userAsJsonNode[UserTableLiterals.Password]?.ToString() ??
                       throw new ArgumentNullException(nameof(userAsJsonNode));
            HasAdminPrivileges = bool.Parse(userAsJsonNode[UserTableLiterals.HasAdminPrivileges]?.ToString() ?? "false");
            Pfp = pfp;
            var totalMessages = GetStringFromJsonProperty(userAsJsonNode, UserTableLiterals.TotalNumMessagesSent);
            GetStringFromJsonProperty(userAsJsonNode, UserTableLiterals.TotalAmountOfTicksOnline);
            GetTotalMessagesPoints().Clear();
            TotalOnlineTimeChart.Series[0].Points.Clear();

            GetTotalMessagesPoints().Add(new DataPoint(0, totalMessages) { Label = "You" });

            UserPicutreBox.BackgroundImage = OvalImage(Pfp ?? DefaultUserProfileImage);

            
        }

        private DataPointCollection GetTotalMessagesPoints() => TotalMessagesChart.Series[0].Points;

        /// <summary>
        /// Get A Value out of a <see cref="JsonNode"/> from a specified property name
        /// </summary>
        /// <param name="userAsJsonNode">The Json Node to extract information from</param>
        /// <param name="propertyName">The name of the property inside the specified Json Node to extract information from</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static string GetStringFromJsonProperty(in JsonNode userAsJsonNode,string propertyName)
        {
            var jsonNode = userAsJsonNode[propertyName];
            return jsonNode == null ? "null" : jsonNode.ToString();
        }

        public static Image OvalImage(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height);
            using var gp = new GraphicsPath();

            gp.AddEllipse(0, 0, img.Width + 3, img.Height + 3);
            using var gr = Graphics.FromImage(bmp);

            gr.SetClip(gp);
            gr.DrawImage(img, Point.Empty);

            return bmp;
        }

        public void ApplyAverage(Average average)
        {
            var totalSpan = TimeSpan.FromTicks(average.TotalOnlineTicks);
            var userSpan =
                TimeSpan.FromTicks((long) double.Parse(GetStringFromJsonProperty(UserAsJsonNode, 
                                                        UserTableLiterals.TotalAmountOfTicksOnline)));

            //Manage Spans
             Invoke((MethodInvoker) (() => {
                TotalMessagesChart.Series[0].Points.Add(new DataPoint(0, average.TotalMessages) { Label = "Average" });

                if (totalSpan.TotalDays > 1 || userSpan.TotalDays > 1)
                {
                    TotalOnlineTimeChart.Titles[0].Text += " (In Days)";
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, userSpan.TotalDays) { Label = "You" });
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, totalSpan.TotalDays) { Label = "Average" });
                }
                else if (totalSpan.TotalDays > 1.0 / 24 || userSpan.TotalDays > 1.0 / 24)
                {
                    TotalOnlineTimeChart.Titles[0].Text += " (In Hours)";
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, userSpan.TotalHours) { Label = "You" });
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, totalSpan.TotalHours) { Label = "Average" });
                }
                else if (totalSpan.TotalDays > 1.0 / 24 / 60 || userSpan.TotalDays > 1.0 / 24 / 60)
                {
                    TotalOnlineTimeChart.Titles[0].Text += " (In Minutes)";
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, userSpan.TotalMinutes) { Label = "You" });
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, totalSpan.TotalMinutes) { Label = "Average" });
                }

                else if (totalSpan.TotalDays > 1.0 / 24 / 60 / 60 || userSpan.TotalDays > 1.0 / 24 / 60 / 60)
                {
                    TotalOnlineTimeChart.Titles[0].Text += " (In Seconds)";
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, userSpan.TotalSeconds) { Label = "You" });
                    TotalOnlineTimeChart.Series[0].Points.Add(new DataPoint(0, totalSpan.TotalSeconds) { Label = "Average" });
                }

                

                Refresh();
            }));

        }

        private void AccountStats_Load(object sender, EventArgs e)
        {
            Text = UserName;
        }


#nullable enable
    }
}
