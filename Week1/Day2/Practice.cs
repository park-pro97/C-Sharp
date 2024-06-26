//내 이름 알려주고 내 이름 출력시키기
namespace ConsoleApp6
{
    internal class Program // 메인 클래스
    {
        static void Main(string[] args) // 메인 메소드
        {
            string greeting;   // 변수 선언
            Console.Write("이름을 입력해 주세요: ");
            greeting = Console.ReadLine();  // 변수에 값 할당 & 초기화한다.

            Console.WriteLine($"당신의 이름은 {greeting} 입니다."); // 출력!
        }
    }
}


-------------------------------------------------------------------------
//두 정수를 입력받아 둘을 더한 값을 출력시키기
    namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 두 정수를 입력받아 둘을 더해서 출력
            int value1, value2;
            Console.Write("정수를 입력하세요: ");
            value1 = Int32.Parse(Console.ReadLine());
            Console.Write("정수를 입력하세요: ");
            value2 = Int32.Parse(Console.ReadLine());

            int result = value1 + value2;
            Console.WriteLine($"합한 값은: {result} 입니다.");
        }
    }
}

-----------------------------------------------------------
//섭씨 온도를 입력해 화씨 온도로 변환한 값을 출력시키기
    namespace CToF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("섭씨 온도를 입력하세요: ");
            int C = Int32.Parse(Console.ReadLine());

            double F = 0.0;
            F= (double)9 / 5 * C + 32;
            Console.WriteLine($"화씨 온도는 {F} 입니다.");
        }
    }
}


-----------------------------------------------------------
//반지름을 입력하면 원의 넓이 구해서 출력시키기
    namespace Quiz002
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("반지름을 입력하세요: ");
            double R;
            double A;
            R = Int32.Parse(Console.ReadLine());

            A = Math.PI * R * R;
            Console.WriteLine($"원의 넓이는 {A:F2} 입니다.");
            
        }
    }
}


-----------------------------------------------------------
//정수를 입력하면 홀수인지 짝수인지 출력시키기
    namespace ifApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("정수를 입력하세요: ");
            int value = Int32.Parse(Console.ReadLine());
            if (value % 2 == 0)
            {
                Console.WriteLine("짝수");
            }
            else
            {
                Console.WriteLine("홀수");
            }
        }
    }
}


-----------------------------------------------------------
//for문으로 1~100 더하기
    namespace FirstFor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 초기화조건 2. 종료조건//
            int sum = 0; // sum이 0
            for (int i = 1; i <=100; i++) // 1씩 늘어남
            {
                sum += i;
            }
            Console.WriteLine(sum); // 출력문을 안에 넣으면 과정이 보임 (내용 보고 싶으면)
        }
    }
}


-----------------------------------------------------------
//while문으로 1~100 더하기
    namespace WhileApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            int sum = 0;
            while(i <= 100)
            {
                sum += i;
                i++;
            }
            Console.Write(sum);
        }
    }
}


-----------------------------------------------------------
//for문으로 1~100 숫자 홀수, 짝수끼리 더하기
    namespace Quiz03
{
    internal class Program

    {

        static void Main(string[] args)

        {

            int sum1 = 0;
            int sum2 = 0;

            for (int j = 0; j <= 100; j++)
            {
                if (j % 2 == 1)
                {
                    sum1 += j;  //홀수
                }
                else
                {
                    sum2 += j;  //짝수
                }
            }
            Console.WriteLine($"홀수: {sum1}, 짝수: {sum2}");
        }
    }
}


-----------------------------------------------------------
//while문으로 1~100 숫자 홀수, 짝수끼리 더하기
    namespace Quiz03
{
    internal class Program

    {

        static void Main(string[] args)

        {
            int sum1;
            int sum2;
            int i = 0;
            sum1 = 0;
            sum2 = 0;

            while (i <= 100)
            {
                if (i % 2 == 1)
                {
                    sum1 += i++;  //홀수
                }
                else
                {
                    sum2 += i++;  //짝수
                }
            }
            Console.WriteLine($"홀수: {sum1}, 짝수: {sum2}");
        }
    }
}
