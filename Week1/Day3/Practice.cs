//1~100 까지의 숫자 중에 3의 배수와 7의 배수를 찾아 출력
namespace Quiz04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(int i = 1; i <= 100; i++)
            {
                if(i % 3 == 0)
                {
                    Console.Write($" {i} ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 1; i <= 100; i++)
            {
                if (i % 7 == 0)
                {
                    Console.Write($" {i} ");
                }
            }
        }
    }
}


-------------------------------------------------------------

