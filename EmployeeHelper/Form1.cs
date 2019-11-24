using System;
using System.Windows.Forms;

namespace EmployeeHelper
{
    public partial class frmMain : Form
    {
        private DateTime _Dt;
        private bool _OkFlag = false;
        private int _PicNumber = 1;
        private TimeSpan _Ts;

        public frmMain()
        {
            InitializeComponent();
            timer1.Start();
            _Dt = DateTime.Now;
            txtMinute.Text = Properties.Settings.Default.minute;
            _Ts = new TimeSpan(0, int.Parse(Properties.Settings.Default.minute), 0);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_PicNumber > 0)
            {
                _PicNumber -= 1;
            }

            if (_PicNumber == 0)
            {
                _PicNumber = 6;
            }

            ChangePicture(_PicNumber);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_PicNumber < 7)
            {
                _PicNumber += 1;
            }

            if (_PicNumber == 7)
            {
                _PicNumber = 1;
            }

            ChangePicture(_PicNumber);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _OkFlag = true;

            _Ts = new TimeSpan(0, int.Parse(Properties.Settings.Default.minute), 0);
            _Dt = DateTime.Now;

            lblText.Text = "منتظر باش تا بهت بگم پاشی نرمش کنی";
            btnOK.Enabled = false;
            this.TopMost = false;
        }

        private void btnSetMinute_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMinute.Text, out int a))
            {
                Properties.Settings.Default.minute = txtMinute.Text;
                Properties.Settings.Default.Save();

                _Ts = new TimeSpan(0, int.Parse(Properties.Settings.Default.minute), 0);
                _Dt = DateTime.Now;

                MessageBox.Show(string.Format("تنظیم شد و از الان هر {0} دقیقه یکبار برنامه باخبرت میکنه که نرمش کنی", txtMinute.Text));
            }
            else
            {
                MessageBox.Show("فقط عدد وارد کنید");
            }
        }

        private void ChangePicture(int picNumber)
        {
            switch (picNumber)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources._1;
                    break;

                case 2:
                    pictureBox1.Image = Properties.Resources._2;
                    break;

                case 3:
                    pictureBox1.Image = Properties.Resources._3;
                    break;

                case 4:
                    pictureBox1.Image = Properties.Resources._4;
                    break;

                case 5:
                    pictureBox1.Image = Properties.Resources._5;
                    break;

                case 6:
                    pictureBox1.Image = Properties.Resources._6;
                    break;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("سنگ میشی اگه برنامه رو ببندی. میخوای برنامه رو ببندی؟", "توجه", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void ShakeIt()
        {
            this.Top = this.Top + 10;
            System.Threading.Thread.Sleep(50);
            this.Top = this.Top - 10;
            System.Threading.Thread.Sleep(50);
            this.Top = this.Top + 10;
            System.Threading.Thread.Sleep(50);
            this.Top = this.Top - 10;
            System.Threading.Thread.Sleep(50);
            this.Top = this.Top + 10;
            System.Threading.Thread.Sleep(50);
            this.Top = this.Top - 10;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int minute = DateTime.Now.Subtract(_Dt).Minutes;

            int savedTime = int.Parse(Properties.Settings.Default.minute);

            if (minute >= savedTime)
            {
                btnOK.Text = "باشه متوجه شدم";

                if (_OkFlag == false)
                {
                    lblText.Text = "پاشو پاشو پاشو پاشو پاشو پاشو";
                    this.TopMost = true;

                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }

                    btnOK.Enabled = true;
                    Console.Beep();
                    ShakeIt();
                    this.Update();
                }
            }
            else
            {
                TimeSpan ts = new TimeSpan(0, 0, 1);
                _Ts = _Ts.Subtract(ts);
                btnOK.Text = string.Format("{0}:{1}:{2}",_Ts.Hours.ToString(), _Ts.Minutes.ToString(), _Ts.Seconds.ToString());

                _OkFlag = false;
                lblText.Text = "منتظر باش تا بهت بگم پاشی نرمش کنی";
                this.TopMost = false;
                btnOK.Enabled = false;
                this.Update();
            }
        }
    }
}