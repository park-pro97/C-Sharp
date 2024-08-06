//사진3개 동작
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Page179n1
{
    public partial class Form1 : Form
    {
        private int Sajin = 1;
        private int Sajin_Max = 4;
        public Form1()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(System.Environment.CurrentDirectory + "/RPG/" +
            Sajin + ".jpg");
            Sajin++;
            if (Sajin > Sajin_Max)
                Sajin = 1;
        }
    }
}
-------------------------------------------------------------------------------------
//ProgressBar
namespace WinFormsApp17
{
    public partial class Form1 : Form
    {
        private int progressValue;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            progressLabel.Text = "진행도 : 0%";

            timer1.Interval = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressValue = 0;
            progressBar1.Value = 0;
            progressLabel.Text = "진행도 : 0%";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressValue += 1;
            if(progressValue <= 100)
            {
                progressBar1.Value = progressValue;
                progressLabel.Text = $"진행도: {progressValue}%";
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("진행 완료!");
            }
        }
    }
}

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

