// 상속, 다향성 연습
namespace OOPApp07
{
    class Shape
    {
        public int vertex; //멤버변수
        public Shape() //Default Constructor(디폴트 생성자)
        {
            vertex = 0;
        }
        public void ShowVertex()
        {
            Console.WriteLine(vertex);
        }
        public void ShowVertex(string msg)
        {
            Console.WriteLine(msg + " "+ vertex);
        }
        public void ShowVertex(string msg, string position, int repeat)
        {
            Console.WriteLine(msg + " " + vertex + " 현재 지역은 " + position + " 반복 횟수는 " + repeat);
        }
        public virtual void ShowName()
        {
            Console.WriteLine("도형입니다.");
        }
    }
        class Triangle : Shape
        {
            public Triangle()
            {
                vertex = 3;
            }

        public override void ShowName()
        {
            Console.WriteLine();
        }
    }
        class Circle : Shape
        {
            
        }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle();
            triangle.ShowVertex();
            triangle.ShowVertex("곡짓점의 개수: ");
            triangle.ShowVertex("곡짓점의 개수: ", "안동,", 3);

            triangle.ShowName();
            Circle circle = new Circle();
            //
        }
    }
}


----------------------------------------------------------------------------------------------------------------------------
//Quiz 상속, 다향성
using System.Globalization;

namespace OOPApp07
{
    class Car
    {
        public string name;
        public int speed; //멤버변수
        public Car() //디폴트 생성자
        {
            name = "차";
            speed = 0;
        }
        public Car(string name, int speed)
        {
            this.name = name;
            this.speed = speed;
        }
        public virtual void ShowType()
        {
            Console.WriteLine($"차 종류는: {name}, 속도는: {speed}입니다.");
        }
    }
    class Bus : Car
    {
        public Bus()
        {
            name = "버스";
            speed = 60;
        }
        public Bus(string name)
        {
            this.name= name;
            speed = 60;
        }
        public Bus(string name, int speed)
        {
            this.name =name;
            this.speed = speed;
        }
        public override void ShowType()
        {
            Console.WriteLine($"차 종류는: {name}, 속도는: {speed}입니다.");
        }

    }
    class Taxi : Car
    {
        public Taxi()
        {
            name = "택시";
            speed = 70;
        }
        public Taxi(string name)
        {
            this.name = name;
            speed = 70;
        }
        public Taxi(string name, int speed)
        {
            this.name = name;
            this.speed = speed;
        }
        public override void ShowType()
        {
            Console.WriteLine($"차 종류는: {name}, 속도는: {speed}입니다.");
        }

    }
    class Truck : Car
    {
        public Truck()
        {
            name = "트럭";
            speed = 40;
        }
        public Truck(string name)
        {
            this.name = name;
            speed = 40;
        }
        public Truck(string name, int speed)
        {
            this.name = name;
            this.speed = speed;
        }
        public override void ShowType()
        {
            Console.WriteLine($"차 종류는: {name}, 속도는: {speed}입니다.");
        }
    }
       
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Car bus = new Car();
            Car Taxi = new Car();
            Car Truck = new Car();
            bus.ShowType();
            Taxi.ShowType();
            Truck.ShowType();

            Car bus1 = new Bus("녹색");
            Car taxi1 = new Taxi("회색");
            Car truck1 = new Truck("검정색");
            bus1.ShowType();
            taxi1.ShowType();
            truck1.ShowType();

            Car bus2 = new Bus("녹색", 60);
            Car taxi2 = new Taxi("회색", 70);
            Car truck2 = new Truck("검정색", 40);
            bus2.ShowType();
            taxi2.ShowType();
            truck2.ShowType();

        }
    }
}


----------------------------------------------------------------------------------------------------------------------------
//인터페이스 구현해보기(다중 상속)
namespace OOPA07
{
    class Horse
    {
        public void Run()
        {
            Console.WriteLine("말이 달리고 있다.");
        }
    }
    class Angel { }
    interface IWing 
    {
        public void Fly(); // Abtract Method
    }
    interface IWing2
    {
        public void Fly();
    }
    class Unicon : Horse, IWing
    {
        //밑은 인터페이스 메소드의 구현
        public void Fly()
        {
            Console.WriteLine("유니콘이 날고 있다.");
        }
        //여기는 유니콘의 멤버 메소드
        public void PerformMagic()
        {
            Console.WriteLine("마법을 사용한다.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Unicon jack = new Unicon();
            jack.Run();
            jack.Fly();
            jack.PerformMagic();
        }
    }
}


----------------------------------------------------------------------------------------------------------------------------
//완전수 찾기
    namespace ConsoleApp9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("정수를 입력하세요: ");
            int n = Int32.Parse(Console.ReadLine());
            int result = 0;
            for (int i = 1; i < n; i++)
            {
                if (n % i == 0)
                    result += i;
            }
            if (result == n)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}

----------------------------------------------------------------------------------------------------------------------------
// 소수찾기
namespace PrimeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(int i = 2; i < 100; i++)
            {
                int count = 0;
                for(int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        count++;
                        break;
                    }
                }
                if(count == 0)
                {
                    Console.WriteLine($"{i}");
                }
            }
        }
    }
}
