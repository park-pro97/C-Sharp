
namespace MethodApp01
{
    class MyClass
    {
        public void Print()
        {
            Console.WriteLine("MyClass Hello~!");
        }
        public void Print(string s)
        {
            Console.WriteLine(s);
        }
        public void Print(string s, double speed)
        {
            Console.WriteLine($"{s} , speed : {speed}");
        }
        public void Print(double speed, string s)
        {
            Console.WriteLine($"speed : {speed}, {s} ");
        }
    }//end of MyClass
    internal class Program
    {
        public static void Print()
        {
            Console.WriteLine("Hello~!");
        }
        public static void Print(string s)
        {
            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            Print();
            Print("안녕하세요");

            MyClass mc = new MyClass();
            mc.Print();
            mc.Print("반갑습니다.");
            mc.Print("수고하세요", 100);
            mc.Print(3.14, "어서오세요.");
        }
    }
}

--------------------------------------------------------------------------------

namespace MethodApp02
{
    class Bank
    {
        //1.멤버 변수
        private int money;
        //2.생성자
        public Bank()
        {
            this.money = 10000;
        }
        //3.멤버 메소드
        //예금하다
        public void Deposit()
        {
            Console.WriteLine($"{money} 금액을 예금하다.");
        }
        public void Deposit(int money)
        {
            Console.WriteLine($"{money} 금액을 예금하다.");
        }
        //인출하다
        public void WithDraw()
        {
            Console.WriteLine($"{money} 금액을 인출하다.");
        }
        //이체하다
        public void Transfer()
        {
            Console.WriteLine($"{money} 금액을 이체하다.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 예금하다 ,인출하다, 이체하다
            //객체
            Bank kb = new Bank();
            kb.Deposit();
            kb.Deposit(1000000);
            kb.WithDraw();
            kb.Transfer();
        }
    }
}

--------------------------------------------------------------------------------
namespace MethodApp01
{
    class Sensor
    {
        //멤버변수
        //생성자
        //멤버메소드
        //데이터읽어오기
        public void ReadData()
        {
            Console.WriteLine("데이터를 읽다.");
        }
        public void ReadData(byte[] data)
        {
            Console.WriteLine($"{data[0]}{data[1]}{data[2]} 데이터를 읽다.");
        }
        public void Calibrate()
        {
            Console.WriteLine("설정 값을 조정하다.");
        }
        public void SendAlert()
        {
            Console.WriteLine("경고 보내기");
        }
        public void SendArert(string message)
        {
            Console.WriteLine($"{message} 경고 보내기");
        }
        internal class Program
        {

            static void Main(string[] args)
            {
                Sensor sensor = new Sensor();
                sensor.ReadData();
                byte[] arr = { 0x001, 0x002, 0x003 };
                sensor.ReadData(arr);
                sensor.Calibrate();
                sensor.SendAlert();
            }
        }
    }
}

--------------------------------------------------------------------------------
namespace MethodApp04
{
    class Student
    {
        private int mm;

        public Student()
        {
            this.mm = 0;
        }
        // EnrollCourse() - 수강신청하다
        // DropCourse() - 수강신청 취소하다
        //ViewCourse() - 성적확인하기
        public void EnrollCourse()
        {
            Console.WriteLine("과목 수강 신청을 하다.");
        }
        public void EnrollCourse(int mm)
        {
            Console.WriteLine($"{mm} 과목 수강 신청을 하다.");
        }
        public void DropCourse()
        {
            Console.WriteLine("과목 수강 신청을 취소하다.");
        }
        public void DropCourse(int mm)
        {
            Console.WriteLine($"{mm} 과목 수강 신청을 취소하다.");
        }
        
    }
    class Instructor
    {
        //AssignGrade() - 성적인증하기
        //CreateCourse() - 과정 만들기
        //UpdateCourse() - 과정 수정하기
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Student jjs = new Student();
            jjs.EnrollCourse();
            jjs.EnrollCourse(3);
            jjs.DropCourse();
            jjs.DropCourse(1);
        }
    }
}

이까지는 메소드 연습
--------------------------------------------------------------------------------
//상속
namespace OOPApp03
{
    class Shape
    {
        public string name;
        public Shape()
        {
            this.name = "도형";
            Console.WriteLine("부모 클래스 생성자.");
        }
        //메소드

        public virtual void Draw()
        {
            Console.WriteLine("도형을 그리다.");
        }
    }
    class Rectangle : Shape
    {
        public Rectangle()
        {
            this.name = "사각형";
            Console.WriteLine("자식 클래스 생성자.");
        }
        public override void Draw()
        {
            Console.WriteLine("사각형을 그리다.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle();
            Console.WriteLine(rect.name);
            rect.Draw();

        }
    }
}
(연습)
--------------------------------------------------------------------------------
// 도형이란 부모 클래스를 만들고 삼각, 사각, 원 3개의 자식 클래스를 오버로딩하여 자식 클래스에서 재정의 하라
namespace OOPApp004
{
    class Shape()
    // 밑에 삼각 사각 원을 오버로딩하여 자식 클래스에서 재정의 하라(오버라이딩)
    {
        public virtual void Draw()
        {
            Console.WriteLine("도형을 그리다.");
        }
    }
    class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("삼각형을 그리다.");
        }
    }
    class Rectangle : Shape
    {

    }
    class Circle : Shape
    {

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle();
            Rectangle rectangle = new Rectangle();
            Circle circle = new Circle();

            triangle.Draw();
            rectangle.Draw();
            circle.Draw();

        }
    }
}


--------------------------------------------------------------------------------
//추상클래스
  namespace OOPApp005
{


    abstract class Car
    {
        public abstract void Run(); //추상 메소드 abstract Method
        
    }
    class Bus : Car
    {
        public override void Run()
        {
            Console.WriteLine("버스가 달린다.");
        }
    }
    class Taxi : Car
    {
        public override void Run()
        {
            Console.WriteLine("택시가 달린다.");
        }
    }
    class Truck : Car
    {
        public override void Run()
        {
            Console.WriteLine("트럭이 달린다.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Bus bus = new Bus();
            Taxi taxi = new Taxi();
            Truck truck = new Truck();
            bus.Run();
            taxi.Run();
            truck.Run();

            Car car1 = new Bus();
            Car car2 = new Taxi();
            Car car3 = new Truck();
            car1.Run();
            car2.Run();
            car3.Run();

            Car[] cars = new Car[3];
            cars[0] = new Bus();
            cars[1] = new Taxi();
            cars[2] = new Truck();

            for(int i = 0; i < 3; i++)
            {
                cars[i].Run();
            }
        }
    }
}

--------------------------------------------------------------------------------


namespace OOP06
{
    class Shape
    {
        //멤버변수
        private string color;
        public string Color { get; set; }       //Property
        public void SetColor(string color)
        {
            this.color = color;
        }
        public string GetColor()
        {
            return this.color;
        }
        public virtual void Draw()
        {
            Console.WriteLine("도형을 그리다.");
        }
    }
    class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("원을 그리다.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Circle circle = new Circle();
            circle.SetColor("파란색");
            Console.WriteLine(circle.GetColor());
            circle.Color = "노란색";
            Console.WriteLine(circle.Color);
        }
    }
}

--------------------------------------------------------------------------------
//객체지향문법 get/set 사용법
namespace Quiz10
{
    class Cat
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }

        public void ShowCatInfo()
        {
            Console.WriteLine($"{Name}의 나이는 {Age}살이고 색깔은 {Color}입니다.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Cat cat = new Cat();
            cat.Name = "도르쉐";
            cat.Age = 3;
            cat.Color = "검흰";

            cat.ShowCatInfo();
        }
    }
}
