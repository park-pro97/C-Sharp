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
