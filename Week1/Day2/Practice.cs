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
﻿namespace ConsoleApp7
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
