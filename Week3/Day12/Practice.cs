//배열 연습
namespace ArrayTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] Numbers = new int[5] { 1, 2, 3, 4, 5 }; //배열을 선언과 동시에 초기화
            //studentIDs[0] = 1;         //선언 후 초기화
            string[] studentNames = new string[3] {"Ralo", "Paka", "Mouse"};

            int[] evenNums = new int[10];
            for(int x = 0; x < 10; x++)
            {
                evenNums[x] = x * 2;

                Console.WriteLine($"You just saved {evenNums[x]}");
            }
        }
    }
}

-------------------------------------------------------------------------------------------------------------------------------------------------
//배열 연습
namespace ArrayTest02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //변수선언
            int[] numbers = new int[7];
            for(int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = 13 * (i + 1);
                Console.WriteLine(numbers[i]);
            }
            string[] names = new string[3];
            for(int j = 0; j < names.Length; j++)
            {
                names[j] = Console.ReadLine();
                Console.WriteLine(names[j]);
            }
            
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------------------------
//배열 연습(클래스를 배열처럼 사용할 수 있게 만들어줌)
using System.Runtime.CompilerServices;

namespace IndexText
{
    class IdxDemo
    {
        private int[] num = new int[5];

        public int this[int x]
        {
            set { num[x] = value; }
            get { return num[x]; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IdxDemo test = new IdxDemo();
            for(int i = 0; i < 5; i++)
            {
                test[i] = i;
                Console.WriteLine(test[i]);
            }
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------------------------

 \   namespace Code99
{//배열처럼 만들자
    enum Days { Sun=, Mon, Tue, Wen, Thu, Fri, Sat }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Days.Sun);
            Console.WriteLine(Days.Mon);
            Console.WriteLine(Days.Tue);
            Console.WriteLine(Days.Wen);
            Console.WriteLine(Days.Thu);
            Console.WriteLine(Days.Fri);
            Console.WriteLine(Days.Sat);
        }
       
    }
}


---------------------------------------------------------------------------------------------------------------------------------
//UI만들기1
namespace WinFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            txtResult.Text = txtName.Text + "님 당신의 학번은" + txtId.Text + "입니다.";
            MessageBox.Show(txtName.Text + "님 당신의 학번은" + txtId.Text + "입니다."
                            ,"알림" , MessageBoxButtons.OKCancel);
        }

        private void btnDispose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

---------------------------------------------------------------------------------------------------------------------------------
//UI 만들기2
namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str;
            str = "당신의 연령은 \n";
            if (radioButton1.Checked == true)
                str = str + radioButton1.Text;
            if (radioButton2.Checked == true)
                str = str + radioButton2.Text;
            if (radioButton3.Checked == true)
                str = str + radioButton3.Text;
            if (radioButton4.Checked == true)
                str = str + radioButton4.Text;
            if (radioButton5.Checked == true)
                str = str + radioButton5.Text;
            if (radioButton6.Checked == true)
                str = str + radioButton6.Text;

            str = str + "\n" + "\n" + "좋아하는 색은" + "\n";

            if(checkBox1.Checked == true)
                str = str + checkBox1.Text + Environment.NewLine;
            if (checkBox2.Checked == true)
                str = str + checkBox2.Text + Environment.NewLine;
            if (checkBox3.Checked == true)
                str = str + checkBox3.Text + Environment.NewLine;
            if (checkBox4.Checked == true)
                str = str + checkBox4.Text + Environment.NewLine;
            if (checkBox5.Checked == true)
                str = str + checkBox5.Text + Environment.NewLine;
            if (checkBox6.Checked == true)
                str = str + checkBox6.Text + Environment.NewLine;

            str = str + "입니다.";
            label1.Text = str;
        }
    }
}

---------------------------------------------------------------------------------------------------------------------------------
//UI 만들기3
