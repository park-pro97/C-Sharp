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
