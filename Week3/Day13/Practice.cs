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
//인터페이스 상속    
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
//인터페이스 상속    
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


-----------------------------------------------------------------------------------------------------------------------
//구조체
namespace Code117
{
    struct School
    {
        public string schName;
        public string stName;
        public int stGrade;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            School sc;
            sc.schName = "능인고등학교";
            sc.stName = "박상민";
            

            Console.WriteLine(sc.schName);
            Console.WriteLine(sc.stName);
        }
    }
}

-----------------------------------------------------------------------------------------------------------------------
//delegate
namespace DelegateApp01
{
    //Delegate
    //Code119
    internal class Program
    {
        //delegate 선언
        delegate void PrintDelegate(string str);
        class Print
        {
            public void PrintOut(string str)
            {
                Console.WriteLine(str);
            }
        }
        static void Main(string[] args)
        {
            Print p = new Print();
            PrintDelegate pdg = p.PrintOut;
            pdg("안녕하세요");
        }
    }
}
-----------------------------------------------------------------------------------------------------------------------
//delegate 사칙연산 (곱셈까지)
namespace DelegateApp02
{
    internal class Program
    {
        public delegate int Compute(int a, int b);
        static void Main(string[] args)
        {

            int x = 10; int y = 5;

            Compute compute = Plus;
            Console.WriteLine($"덧셈을 하게 됩니다. {compute(x, y)}");
            compute = Minus;
            Console.WriteLine($"뺄셈을 하게 됩니다. {compute(x, y)}");
            compute = Multiple;
            Console.WriteLine($"곱셈을 하게 됩니다. {compute(x, y)}");
           
        }
        //참조메소드
        public static int Plus(int a, int b)
        {
            return a + b;
        }
        public static int Minus(int a, int b)
        {
            return a - b;
        }
        public static int Multiple(int a, int b)
        {
            return a * b;
        }
        
    }
}


-----------------------------------------------------------------------------------------------------------------------
//delegate(곱x 나눗셈ㅇ)    ---delegate는 중요함
using System.Data;
namespace DelegateApp03
{
    class Calculator
    {
        public int Plus(int a, int b)
        {
            return a + b;
        }
        public int Minus(int a, int b)
        {
            return a - b;
        }
        public double Divide(int a, int b)
        {
            return (double)a / b;
        }
    }
    internal class Program
    {
        //public delegate int Compute(int a, int b); //선언!!!
        static void Main(string[] args)
        {
            int a = 100, b = 200;
            Calculator calculator = new Calculator();
            //Compute compute = calculator.Plus;
            Func<int, int, int> intCompute = calculator.Plus;
            //Console.WriteLine("덧셈 : {0}", compute(a, b));
            Console.WriteLine("덧셈 : {0}", intCompute(a, b));

            //compute = calculator.Minus;
            intCompute = calculator.Minus;
            //Console.WriteLine("뺄셈 : {0}", compute(a, b));
            Console.WriteLine("뺄셈 : {0}", intCompute(a, b));

            //compute = calculator.Divide;
            Func<int, int, double> doubleCompute = calculator.Divide;
            //Console.WriteLine("나눗셈 : {0}", compute(a, b));
            Console.WriteLine("나눗셈 : {0}", doubleCompute(a, b));
        }
    }
}


-----------------------------------------------------------------------------------------------------------------------
//UI만들기
namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("장갑");
            listBox1.Items.Add("타월");
            listBox1.Items.Add("양말");
            listBox1.Items.Add("바지");
            listBox1.Items.Add("반팔티");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text); //값을 추가하고 남아있음 
            textBox1.Text = ""; // 추가되면 공백을 추가해 비워줌
            textBox1.Focus(); //
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.Text);
        }
    }
}


-----------------------------------------------------------------------------------------------------------------------
//UI만들기
namespace WinFormsApp11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text.Substring(0, 3);
            textBox3.Text = textBox1.Text.Substring(textBox1.TextLength - 3, 3);
            textBox4.Text = textBox1.Text.Substring(5, 3);
            textBox5.Text = textBox1.TextLength.ToString();
        }
    }
}

-----------------------------------------------------------------------------------------------------------------------
//UI만들기
namespace WinFormsApp12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            int a = int.Parse(tbValue1.Text);
            int b = int.Parse(tbValue2.Text);
            int result = a + b;
            tbResult.Text = result.ToString();   // --> 정수를 문자로
        }
    }
}

-----------------------------------------------------------------------------------------------------------------------
//UI만들기
namespace WinFormsApp13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i, N; //N 값을 입력받을 변수와 i 변수 선언
            double dsum = 0;

            int n = int.Parse(textBox1.Text); //텍스트 박스 1의 입력된 숫자를 N에 대입
            textBox2.Text = "";

            for(i = 1; i <= n; ++i)
            {//1부터 n까지 1씩 증가하며
                dsum = dsum + i;
                textBox2.Text = textBox2.Text + i + " + ";
            }
            textBox2.Text = textBox2.Text + " = " + dsum;
        }
    }
}

