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
//




-------------------------------------------------------------------------------------------------------------------------------
//
