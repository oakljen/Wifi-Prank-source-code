using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace disconnecter
{
    //NOTE FOR ANYONE THAT READS THIS CODE
    //  i am sorry
    //  i am new to this and is probly not the best code
    //  anyway probly just dont touch the code bellow or you might brake it and i will not be responsable for any errors the this program may form
    //  also i am not paying for your counselling after reading this code
    //  Have a great day
    //  :)
    public partial class Form1 : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer(); // Declare a timer object
        private NotifyIcon notifyIcon;
        private bool isMinimized;

        public Form1()
        {
            InitializeComponent();
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = this.Icon; // or set it to a custom icon
            notifyIcon.Visible = false;
            notifyIcon.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            isMinimized = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Call the randomtimer method to start the timer
            randomtimer();
            this.Visible = false;
            this.ShowInTaskbar = false;
        }

        private void randomtimer()
        {
            //set start time in min will happen after the timer passes x min
            int starttime = 10;
            //min time in min
            int mintime = 1;
            //max time in min
            int maxtime = 20;

            //do maths
            int maxtimeplusone = maxtime + 1;
            if (maxtime == 0)
            {
                maxtimeplusone = 0;
            }
            //done some maths

            Random random = new Random();
            int randomMinutes = random.Next(mintime, maxtimeplusone); // generates a random integer between 20 and 30
            int totalMinutes = starttime + randomMinutes; // adds the random minutes to the starting time of 10 minutes
            TimeSpan time = TimeSpan.FromMinutes(totalMinutes); // creates a TimeSpan object with the total minutes

            // Set the interval of the timer object
            timer.Interval = time.TotalMilliseconds;

            // Add an event handler to the Elapsed event of the timer object
            timer.Elapsed += timertrigger;

            // Start the timer object
            timer.Start();
        }

        private void timertrigger(object sender, EventArgs e)
        {
            // Stop the timer object
            timer.Stop();

            // Create a new process
            Process process = new Process();

            // Release the current IP address
            process.StartInfo.FileName = "ipconfig.exe";
            process.StartInfo.Arguments = "/release";
            process.StartInfo.CreateNoWindow = true; // hide the window
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // set the window style to hidden
            process.Start();

            // Wait for 2 seconds before renewing the IP address
            Thread.Sleep(2000);

            // Renew the IP address
            process.StartInfo.Arguments = "/renew";
            process.Start();
            process.StartInfo.CreateNoWindow = true; // hide the window
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // set the window style to hidden
            process.Start();

            // Call the randomtimer method again to start a new random timer
            randomtimer();
        }
    }
}
