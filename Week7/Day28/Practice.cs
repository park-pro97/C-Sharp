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
//데이터 넣고 검색,삭제 가능하게
using System.Buffers;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DataGridApp
{
    public partial class Form1 : Form
    {
        public class Data
        {
            public string No { get; set; }
            public string Name { get; set; }
            public string HP { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("No", "번호");
            dataGridView1.Columns.Add("Name", "이름");
            dataGridView1.Columns.Add("HP", "전화번호");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string no = textBoxNo.Text;
            string name = textBoxName.Text;
            string hp = textBoxHP.Text;
            if (!string.IsNullOrEmpty(no) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(hp))
            {
                dataGridView1.Rows.Add(no, name, hp);
                textBoxNo.Clear();
                textBoxName.Clear();
                textBoxHP.Clear();
            }
            else
            {
                MessageBox.Show("모든 필드를 입력해주세요.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;
            bool found = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Name"].Value != null && row.Cells["Name"].Value.ToString().Equals(searchValue))
                {
                    row.Selected = true;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                MessageBox.Show("찾았습니다!!");
            }
            else
            {
                MessageBox.Show("해당 결과를 찾을 수 없습니다!!");
            }
        }
    }
}
---------------------------------------------------------------------------------
//form1
using AccessForm;

namespace AccessForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenForm2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(this, "안녕하세요");
            frm2.ShowDialog();
        }
    }
}
---------------------------------------------------------------------------------
//form2
using AccessForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessForm
{
    public partial class Form2 : Form
    {
        // 생성자
        private Form1 frm1;
        private string str;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(object frm)
        {
            InitializeComponent();
            frm1 = (Form1)frm;
        }
        public Form2(object frm, string _str)
        {
            InitializeComponent();
            frm1 = (Form1)frm;
            str = _str;
        }

        private void btnChangeMainLabel_Click(object sender, EventArgs e)
        {
            frm1.label1.Text = "Form2에서 변경함";
            frm1.label1.BackColor = Color.Red;
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

