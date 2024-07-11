//객체지향 연습
namespace OOPTest05
{
    class Person        //명사, 대문자로 시작!
    {
        //1.멤버 변수
        //private string name;
        //private int age;

        //Property
        public string Name { get; set; }
        public int Age { get; set; }

        //2.생성자 , 1개 이상
        public Person() //default 생성자
        {
        }
        public Person(string name)
        {
            Name = name;
        }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        //3.멤버메소드
        public void Eat()
        {
            Console.WriteLine($"밥을 먹습니다.");
        }
        public void Eat(string food)
        {
            Console.WriteLine($"{food}를 먹습니다.");
        }

        //Getter, Setter
        //public string GetName()
        //{
        //    return Name;
        //}
        //public void SetName(string name)
        //{
        //    this.name = name;
        //}
        //public int GetAge()
        //{
        //    return age;
        //}
        //public void SetAge(int age)
        //{
        //    this.age = age;
        //}

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Person tom = new Person();
            tom.Eat();
            tom.Eat("오렌지");
            //Console.WriteLine(tom.GetName());
            Console.WriteLine(tom.Name);

            Person sam = new Person("Sam");
            //Console.WriteLine(sam.GetName());
            //Console.WriteLine(sam.GetAge());
            Console.WriteLine(sam.Name);
            Console.WriteLine(sam.Age);

            Person tony = new Person("Tony", 24);
            //Console.WriteLine(tony.GetName());
            //Console.WriteLine(tony.GetAge());
            Console.WriteLine(tony.Name);
            Console.WriteLine(tony.Age);
        }
    }
}

-----------------------------------------------------------------------------------------------------------------------
//객체지향 연습
using System.Security.AccessControl;

namespace OOPTest05
{
    class Car       //명사, 대문자로 시작!
    {
        //1.멤버 변수
        private string brand;
        private int speed;

        //Property
        public string Brand { get; set; }
        public int Speed { get; set; }

        //2.생성자 , 1개 이상
        public Car() //default 생성자
        {
        }
        public Car(string brand)
        {
            Brand = brand;
        }
        public Car(string brand, int speed)
        {
            Brand = brand;
            speed = speed;
        }
        //3.멤버메소드
        public void Run()
        {
            Console.WriteLine($"차가 달립니다.");
        }
        public void Run(string fast)
        {
            Console.WriteLine($"{fast}으로 달립니다.");
        }

        //Getter, Setter
        //public string GetBrand()
        //{
        //    return Brand;
        //}
        //public void SetBrand(string brand)
        //{
        //    this.brand = brand;
        //}
        //public int GetSpeed()
        //{
        //    return speed;
        //}
        //public void SetSpeed(int speed)
        //{
        //    this.speed = speed;
        //}

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Car BMW = new Car();
            //BMW.Run();
            //BMW.Run("빠르게 ");
            ////Console.WriteLine(tom.GetName());
            //Console.WriteLine(BMW.Brand);

            Car BENZ = new Car("BENZ");
            ////Console.WriteLine(sam.GetName());
            ////Console.WriteLine(sam.GetAge());
            //Console.WriteLine(BENZ.Brand);
            //Console.WriteLine(BENZ.Speed);

            Car AUDI = new Car("ADUI", 120);
            ////Console.WriteLine(tony.GetName());
            ////Console.WriteLine(tony.GetAge());
            //Console.WriteLine(AUDI.Brand);
            //Console.WriteLine(AUDI.Speed);

            
            BMW.Run();
            BMW.Run("110");


            BENZ.Run();
            BENZ.Run("130");
            AUDI.Run();
            AUDI.Run("120");




        }
    }
}

-----------------------------------------------------------------------------------------------------------------------
//product 객체생성, 유효성 검사
namespace PropertyTest
{
    class Product
{
    //private string name;
    //private int price;

    private int stock;
    //Property
    public string Name { get; set; }
    public int Price { get; set; }
    public int Stock
    {
        get { return stock; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("재고가 없습니다.");
            }
            stock = value;
        }
    }
    public Product(string name, int price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Product product = new Product("공책", 1000, 20);
        product.Stock = -5;
    }
}
}


-----------------------------------------------------------------------------------------------------------------------
namespace InterfaceTest01
{
    interface IMaker
    {
        void MadeWhere();
        void WareHouse();
    }
    class Korea : IMaker
    {
        public void MadeWhere()
        {
            Console.WriteLine("국산입니다.");
        }

        public void WareHouse()
        {
            Console.WriteLine("상품 등록 완료.");
        }
    }
    class China : IMaker
    {
        public void MadeWhere()
        {
            Console.WriteLine("중국산입니다.");
        }

        public void WareHouse()
        {
            Console.WriteLine("상품 등록 완료.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IMaker m = new Korea(); //인터페이스타입인 IMaker도 엑세스 가능
            m.MadeWhere();
            m.WareHouse();

            Console.WriteLine(); // m 하나 만든 걸로 중국 클래스에도 사용 가능
                                 // 한국에 쓰고 나중에 쓴 중국에 m이 덮어짐
                                 // 변수 하나만 만들어 출력하고 재사용

            m = new China();
            m.MadeWhere();
            m.WareHouse();
        }
    }
}

-----------------------------------------------------------------------------------------------------------------------
namespace InterfaceTest02
{
    interface IMaker
    {
        void MadeWhere();
    }
    interface IOwner
    {
        void WhoOwns();
    }
    class Korea : IMaker, IOwner
    {
        public void MadeWhere()
        {

        }
        public void WhoOwns()
        {
            Console.WriteLine("대한민국 제품입니다.");
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                IMaker km = new Korea();
                km.MadeWhere();
                // km.WhoOwns(); 안보임
                IOwner ko = new Korea();
                //ko.MadeWhere(); 안보임
                ko.WhoOwns();

                object obj = new Korea();
                //obj.MadeWhere(); --- Access 불가
                //obj.WhoOwns(); --- Access 불가

                Korea korea = new Korea();
                korea.MadeWhere();
                korea.WhoOwns();
            }
        }
    }
}
