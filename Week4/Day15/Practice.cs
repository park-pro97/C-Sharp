//리스트 활용
using System.Runtime.Serialization;

namespace QuizObjectList
{
    class Car
    {
        public String brand { get; set; }
        public int speed { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. studentList 만들기
            List<Car> carlist = new List<Car>();

            //2. 학생 3명을 만들어서 리스트에 넣음
            Car c1 = new Car();
            c1.brand = "현대";
            c1.speed = 100;

            Car c2 = new Car();
            c2.brand = "기아";
            c2.speed = 110;

            Car c3 = new Car();
            c3.brand = "쌍용";
            c3.speed = 120;


            carlist.Add(c1);
            carlist.Add(c2);
            carlist.Add(c3);
            foreach (Car car in carlist)
            {
                Console.WriteLine(car.brand);
                Console.WriteLine(car.speed);
            }
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------
//UI - 현재 시간날짜 출력
namespace CUrrentType
{
    public partial class Form1 : Form
    {
        private int number;
        private DateTime nowTime;
        public Form1()
        {
            InitializeComponent();
        }
        public void GetNumber()
        {
            number++;
        }
        public void OutNumber()
        {
            textBox1.AppendText(number + "\r\n");
        }
        public void GetTime()
        {
            nowTime = DateTime.Now;
        }
        public void OutTime()
        {
            textBox2.AppendText(nowTime + "\r\n");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 5; i++)
            {
                GetNumber();
                OutNumber();
                GetTime();
                OutTime();
                Thread.Sleep(1000);
            }
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------
//날짜시각 출력 (DateTime 사용)

namespace DateTime01
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("년, 월, 일을 입력하세요. ");
            Console.Write("년도: ");
            int Year = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("월: ");
            int Month = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("일: ");
            int Day = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine($"{Year} - {Month} - {Day}");

            string t = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(t);
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------
//정수 받아서 홀수끼리 짝수끼리 합친 값 출력
namespace WinFormsApp14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i, num;
            Double Odd = 0, Even = 0;

            num = int.Parse(textBox1.Text);

            textBox2.Text = "";
            textBox3.Text = "";

            for(i = 1; i <= num; i++)
            {
                if(i % 2 ==0)
                {
                    Even += i;
                    textBox3.Text = textBox3.Text + i + "+";
                }
                else
                {
                    Odd += i;
                    textBox2.Text = textBox2.Text + i + "+";
                }
            }
            textBox2.Text = textBox2.Text + " = " + Odd;
            textBox3.Text = textBox3.Text + " = " + Even;
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------
//3으로 나눴을 때 나머지가 0,1,2 
using System.Security.Cryptography;
namespace WinFormsApp15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int v1;
        int v2;
        int v3;
        int num;
        private void button1_Click(object sender, EventArgs e)
        {
            num = int.Parse(textBox1.Text);
            for (int i = 1; i <= num; i++)
            {
                if (i % 3 == 0)
                {
                    textBox2.AppendText($"{i} + ");
                    v1 += i;
                }
                else if (i % 3 == 1)
                {
                    textBox3.AppendText($"{i} + ");
                    v2 += i;
                }
                else if (i % 3 == 2)
                {
                    textBox4.AppendText($"{i} + ");
                    v3 += i;
                }
            }
            textBox2.AppendText($" = {v1}");
            textBox3.AppendText($" = {v2}");
            textBox4.AppendText($" = {v3}");
        }
    }
}

