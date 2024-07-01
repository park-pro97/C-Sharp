//변수 교환
namespace VariableChange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = Int32.Parse(Console.ReadLine());
            int b = Int32.Parse(Console.ReadLine());

           Console.WriteLine($"{a} {b}");

            //변수 교환 a가100 b가 200일 때 b가 200, a가 100이 되게 하려면?
            // 변수는 공간이다!!

            int tmp = a;
            a = b;
            b = tmp;
            Console.WriteLine($"{a} {b}");
        }
    }
}


----------------------------------------------------------------------------
// 77~700까지 1씩 증가
namespace Exam02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //while문으로 77~700까지
            int i = 77;

            while(i <= 700)
            {
                Console.Write($"{i} ");
                i++;
            }
            Console.WriteLine();
        }
    }
}

----------------------------------------------------------------------------
//4칙연산 메소드 만들어서 계산시키기--스태틱
namespace Exam02
{
    internal class Program
    {
        static int Plus(int a, int b)
        {
            return a + b;
        }
        static int Minus(int a, int b)
        {
            return a - b;
        }
        static int Multipie(int a, int b)
        {
            return a * b;
        }
        static double divide(int a, int b)
        {
            return (double) a / b;  //castring 연산자 -- 형변환 연산자
        }

        static void Main(string[] args)
        {
            //4칙연산 + - * /를 메소드로
            //Plus(,) Minus(,) Multiple(,) ___divide(,)
            //메인 메소드 1 4칙연산 메소드 4
            int result = Minus(100, 5);
            Console.WriteLine(result);

        }
    }
}



----------------------------------------------------------------------------
