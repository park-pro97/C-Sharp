//1~100 정수 / 리스트로 홀짝까지 3개 만들고 출력
namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> arr = new List<int>();
            List<int> oddList = new List<int>();
            List<int> evenList = new List<int>();

            for(int i = 0; i < 100; i++)
            {
                arr.Add(i+1);
            }
            foreach(int i in arr)
            {
                if(i % 2 != 0)
                    oddList.Add(i);
                else
                    evenList.Add(i);
            }
            foreach(int i in oddList)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            foreach (int i in evenList)
            {
                Console.Write(i + " ");
            }

        }
    }
}

---------------------------------------------------------------------------
//두 리스트의 교집함
namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] list1 = { 1,2,2,3,4};
            int[] list2 = {2,3,5,6};

            var intersection = list1.Intersect(list2);
            foreach (int i in intersection)
            { 
                Console.WriteLine(i);
            }
            Console.WriteLine();
        }
    }
}

---------------------------------------------------------------------------
//파일처리
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path =  @"C:\temp\abc.txt";
            string content = "Hello World~!~!!!~";

            File.WriteAllText(path, content);
        }
    }
}

@@@@@@@@@@@@@@@@@@
이렇게도 됨
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText(@"C:\\temp\\abc.txt", "파일처리 연습");
        }
    }
}
---------------------------------------------------------------------------
//읽고 쓰기
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp\abc.txt";
            
            FileInfo fi = new FileInfo(path);

            using(StreamWriter sw = fi.CreateText())
            {
                sw.WriteLine("안녕하신가");
            }

            //읽기
            using (StreamReader sr = fi.OpenText())
            {
                var s = "";
                while((s = sr.ReadLine()) != null) // -1을 만나면 끝나는데 널이 eof eof가 정수로 치면 -1임
                {
                    Console.WriteLine(s);
                }
            }
        
            
        }
    }
}
---------------------------------------------------------------------------
//파일 퀴즈 1번쓰기 2번 읽기
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp\result1.txt";
            FileInfo fi =  new FileInfo(path);
            using (StreamWriter sw = fi.CreateText())
            {
                for(int i = 1; i <= 100; i++)
                {
                    if(i % 5 == 0)
                        sw.Write (i + " ");
                }

            }
            //읽기
            using (StreamReader sr = fi.OpenText())
            {
                var s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }


        }
    }
}
---------------------------------------------------------------------------
//이미지 복사
namespace Fileex_NewJeans
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\newjeans.png";
            string copyPic = @"C:\Temp\CopyNewJeans.png";

            try
            {
                byte[] pictureBytes = File.ReadAllBytes(path);
                File.WriteAllBytes(copyPic, pictureBytes);
                Console.WriteLine("복사 성공");
            }
            catch(Exception Ex)
            {

                Console.WriteLine(Ex.Message);
            }
            


            
        }
    }
}

---------------------------------------------------------------------------
//
using System;
using System.IO;
using System.Text;

namespace File02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\test.log";

            // FileStream을 using 블록으로 감싸서 자동으로 자원을 해제하도록 합니다.
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                // StreamWriter도 using 블록으로 감쌉니다.
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("Hello World!!~");
                    sw.WriteLine("Ronaldo");
                    sw.WriteLine(7);
                }
            }

            Console.WriteLine("파일 작성 완료.");
        }
    }
}

---------------------------------------------------------------------------
//복사
namespace MyCopy
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //args[0] abc.txt
            //args[1] cba.txt
            string add = $@"C:\Temp\{args[0]}";
            string mal = "Hello World~!";
            string copy = $@"C:\Temp\{args[1]}";
            FileStream fs = new FileStream(add, FileMode.Create);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(mal);
            }
            try
            {
                File.Copy(add, copy, true);
                Console.WriteLine("복사 성공");
            }
            catch
            {
                Console.WriteLine("do not");
            }
        }
    }
}
---------------------------------------------------------------------------
//스레드 사용
namespace ThreadTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(threadFunc1);
            Thread t2 = new Thread(threadFunc2);
            t1.Start();
            t2.Start();
        }
        static void threadFunc1()
        {
            for(int i = 1; i < 100; i++)
                Console.WriteLine(i);
        }
        static void threadFunc2()
        {
            char c1 = 'A', c2 = 'a';
            for (int i = 1; i < 26; i++)
                Console.WriteLine((char)c1++);
            for(int j = 1; j < 26; j++)
                Console.WriteLine((char)c2++);
        }
    }
}
---------------------------------------------------------------------------
//스레드
namespace ThreadTest02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(threadFunc);
            t.IsBackground = true;

            t.Start();
            t.Join();

            Console.WriteLine("Main 프로그램 종료");
        }
        static void threadFunc()
        {
            Console.WriteLine("3초 후에 프로그램 종료");
            Thread.Sleep(3000);

            Console.WriteLine("threadFunc 프로그램 종료");
        }
    }
}

---------------------------------------------------------------------------
//위 수정
namespace TheadTest02_6_18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(threadFunc);
            t.IsBackground = true; //Main 종료시 바로 종료됨
            t.Start();
            t.Join(); //Main스레드가 t를 기다려줍니다.

            Thread.CurrentThread.Name = "사장님";
            string mtName = Thread.CurrentThread.Name;
            Console.WriteLine($"{mtName} 프로그램 종료");
        }
        static void threadFunc()
        {
            Console.WriteLine("7초 후에 프로그램 종료");
            Thread.Sleep(7000);

            Thread.CurrentThread.Name = "개발부장";
            string mtName = Thread.CurrentThread.Name;
            Console.WriteLine($"{mtName} 프로그램 종료");
        }
    }
}
---------------------------------------------------------------------------
//디지털 시계 01
namespace DigitalWatch01
{
    public partial class Form1 : Form
    {
        private Thread thread1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 10; i++)
            {
                thread1 = new Thread(UpdateTime);
                thread1.IsBackground = true;
                thread1.Start();
            }
            
        }
        private void UpdateTime()
        {
            while(true)
            {
                DateTime currentTime = DateTime.Now;
                string strTime = currentTime.ToString("hh : mm : ss");

                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = strTime;
                });
                //Invoke((Action)( () => label1.Text = strTime));

                Thread.Sleep(1000);
            }
        }
    }
}

---------------------------------------------------------------------------
//버튼 2개 시작 정지
namespace DigitalWatch02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh : mm : ss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}


