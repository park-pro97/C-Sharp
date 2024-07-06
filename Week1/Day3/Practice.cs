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
//1~100 역순으로 짝수만 출력
namespace Quiz04
{
    internal class Program
    {
        static void Main(string[] args)
        {
           for (int i = 100; i >= 1; i = i - 2)
            {
                Console.Write($" {i} ");
            }
        }
    }
}


-------------------------------------------------------------
//1~100 역순으로 홀수만 출력
namespace Quiz04
{
    internal class Program
    {
        static void Main(string[] args)
        {
           for (int i = 99; i >= 1; i = i - 2)
            {
                Console.Write($" {i} ");
            }
        }
    }
}


-------------------------------------------------------------
//성적을 입력하면 학점을 출력해주는 프로그램 작성
namespace Quiz04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("점수를 입력하세요: ");
            int 성적 = int.Parse(Console.ReadLine());
            if(성적 >= 90 && 성적 <= 100)
            {
                Console.WriteLine("A 학점입니다.");
            }
            else if(성적 >= 80 && 성적 <=89)
            {
                Console.WriteLine("B 학점입니다.");
            }
            else if (성적 >= 70 && 성적 <= 79) 
            {
                Console.WriteLine("C 학점입니다.");
            }
            else if (성적 >= 60 && 성적 <= 89)
            {
                Console.WriteLine("D 학점입니다.");
            }
            else if (성적 >= 0 && 성적 <= 59)
            {
                Console.WriteLine("F 학점입니다.");
            }
            
        }
    }
}


-------------------------------------------------------------
//간단한 콘솔 계산기 만들어보기 switch 사용
namespace SwitchApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("첫 번째 숫자를 입력하세요. ");
                double num1 = double.Parse(Console.ReadLine());
                Console.WriteLine("연산자를 입력하세요. ");
                string 연산자 = Console.ReadLine();
                Console.WriteLine("두 번째 숫자를 입력하세요. ");
                double num2 = double.Parse(Console.ReadLine());

                double 결과 = 0;

                switch (연산자)
                {
                    case "+":
                        결과 = num1 + num2;
                        break;
                    case "-":
                        결과 = num1 - num2;
                        break;
                    case "*":
                        결과 = num1 * num2;
                        break;
                    case "/":
                        결과 = num2 / num1;
                        break;


                }
                Console.WriteLine();
                Console.WriteLine($"답: {num1} {연산자} {num2} = {결과}");
                Console.WriteLine("계산을 더 하시겠습니까? (Y/N) ");

                string ox = Console.ReadLine();
                if (ox == "y")
                {
                    Console.WriteLine("계산을 다시 시작.");
                }
                else
                {
                    Console.WriteLine("종료.");
                    break;
                }
            }
        }
    }
}

-------------------------------------------------------------
//구구단
namespace GuGuDan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(int i = 2; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    Console.WriteLine($"{i} * {j} = {i * j}");
                }
            }
        }
    }
}


-------------------------------------------------------------
//구구단 역순으로 출력
namespace GuGuDan
{
    internal class Program
    {
        static void Main(string[] args)
        {

            for (int i = 9; i > 1; i--)
            {
                for (int j = 9; j > 0; j--)
                {
                    Console.WriteLine($"{i} * {j} = {i * j}");

                }
            }
        }
    }
}


-------------------------------------------------------------
//별그리기 1단계
namespace Star
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("50이하 정수를 입력하세요: ");
            int n = int.Parse(Console.ReadLine());
            if(n > 50)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }
}



-------------------------------------------------------------
//별그리기 2단계
namespace Star
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("50이하 정수를 입력하세요: ");
            int n = int.Parse(Console.ReadLine());
            if(n > 50)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = i; j <= n; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }
}



-------------------------------------------------------------
//별그리기 3단계
    namespace Star
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("50이하 정수를 입력하세요: ");
            int n = int.Parse(Console.ReadLine());
            if(n > 50)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n - i; j++)
                {
                    Console.Write(" ");
                }
                for(int k = 1; k <= i; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }
}


-------------------------------------------------------------
//별그리기 4단계
namespace Star
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("50이하 정수를 입력하세요: ");
            int n = int.Parse(Console.ReadLine());
            if(n > 50)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    Console.Write(" ");
                }
                for(int k = i; k <= n; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }
}


-------------------------------------------------------------
//별그리기 5단계
using System;

namespace Star
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("50이하 정수를 입력하세요: ");
            int n = int.Parse(Console.ReadLine());
            if (n > 50)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            for (int i = 1; i <= n; i++)
            {
                for(int j = 1; j <= n - i; j++)
                {
                    Console.Write(" ");
                }
                for(int k = 1; k <= 2 * i - 1; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            for(int i = n - 1; i >= 1; i--)
            {
                for(int j = 1; j <= n-i; j++)
                {
                    Console.Write(" ");
                }
                for(int k = 1; k <= 2 * i - 1; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
