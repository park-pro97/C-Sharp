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
