//배열 시드코드
namespace Array01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[] arr = new char[3];
            char ch = 'Z';

            for(int i = 0; i < arr.Length; i ++)
            {
                arr[i] = ch--;
            }
            for(int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

                                               
    }
}

--------------------------------------------------------------
//성적 입력받고 총점, 평균 출력하기
namespace Quiz05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] score = new int[3];
            int total = 0;
            //score[0]: 국어, score[1]: 영어, score[2]: 수학
            Console.WriteLine($"점수를 입력하세요: ");
            for (int i=0; i<score.Length; i++)
            {
                score[i] = Int32.Parse(Console.ReadLine());
                total += score[i];
            }
            double avg = total / 3.0;

            Console.WriteLine($"총점: {total}");
            Console.WriteLine($"평균: {avg:F2}");
        }
    }
}

--------------------------------------------------------------
//배열 사용하기 2가지 방법
using System.Globalization;

namespace Array02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int i=0; i<numbers.Length; i++)
            {
                if (numbers[i] % 2 == 0)
                    Console.WriteLine(numbers[i]);
            }
            
            
            /*인덱스를 이용한

            for(int i=0; i<numbers.Length; i+= 2)
            {
                Console.WriteLine(numbers[i]);
            }*/
        }
    }
}

--------------------------------------------------------------
//입력받은 문자를 역순으로 출력
using System.Globalization;

namespace Array02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            string outText = "";
            for(int i= str.Length - 1; i >=0; i--)
            {
                outText += str[i];
            }
            Console.WriteLine(outText);


        }


    }
}

--------------------------------------------------------------
//크기 100인 정수형 배열을 만들고 2,3의 배수 요소값으로 출력(↓ 코드는 3의 배수지만 숫자만 바꿔주면 됨)
//7의 배수 인덱스로 출력
using System.Runtime.InteropServices;

namespace Quiz06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //크기가 100인 정수형 배열을 만들고 1~100 초기화
            int[] arr = new int[100];
            for(int i=0; i<arr.Length; i++)
            {
                arr[i] = i + 1;
            }
            for(int i = 0; i<arr.Length;i++)
            {
                if(arr[i] % 3 == 0)
                {
                    Console.Write($"{arr[i]} ");
                }
            }
        }
    }
}

--------------------------------------------------------------
//123
//456
//789  출력
namespace Array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = new int[3, 3];
            int cnt = 1;
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    arr[i, j] = cnt++;
                }
            }
            for(int i=0; i<3; ++i)
            {
                for(int j=0; j<3; j++)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}

--------------------------------------------------------------
//메인 함수와 스태틱 --- v1,2 int a,b 4개의 변수 사용해서 밑에서 위로 올라가고 출력된 후 종료
namespace Quiz07
{
    internal class Program
    {
        static void plus(int a, int b)
        {
            Console.WriteLine(a + b);
        }
        static void Main()
        {
            int v1 = 100;
            int v2 = 200;
            plus(v1, v2);
        }
    }
}

--------------------------------------------------------------
//리턴 사용 ------밑에서 시작해서 위로 갔다가 리턴받고 내려가서 출력
namespace Quiz07
{
    internal class Program
    {
        static int Plus(int a, int b)
        {
            return a + b;
        }
        static void Main()
        {
            int v1 = 100;
            int v2 = 200;
            int result = Plus(v1, v2);
            Console.WriteLine(result);
        }
    }
}

--------------------------------------------------------------
//스태틱 함수 플러스 마이너스 2개를 리턴 쓰면서 올라갔다 마지막에 출력하면서 종료
namespace Quiz07
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
        static void Main()
        {
            int v1 = 100;
            int v2 = 200;
            int plus = Plus(v1, v2);
            int minus = Minus(v1, v2);
            Console.WriteLine($"{plus}, {minus}");
        }
    }
}

--------------------------------------------------------------
//이름과 나이를 출력하는 프로그램
namespace Method02 //나이와 이름을 출력해보자
{
    internal class Program
    {
        static void PrintInfo(string name, int age)
        {
            Console.WriteLine($"{name}은 {age}살 입니다.");
        }
        static int ThreePlus(int a, int b, int c)
        {
            return a + b + c;
        }
        double Plus(double a, double b)
        {
            return a + b;
        }
        static void Main(string[] args)
        {
            PrintInfo("홍길동", 20);
            PrintInfo("이순신", 40);
            Console.WriteLine(ThreePlus(1, 2, 3));

            Program p = new Program();
            Console.WriteLine(p.Plus(3.14, 9.88));
        }
        
    }
}

--------------------------------------------------------------
// 국영수 성적을 받아 총점을 내주는 프로그램 메소드로
using System.Data;

namespace Quiz08
{
    internal class Program
    {
        static int[] inputScore()
        {
            int[] score = new int[3];

            for(int i = 0; i < 3; i++)
            {
                score[i] = Int32.Parse(Console.ReadLine());
            }
            return score;
        }
        static int GetSum(int[] score)
        {
            int sum = 0;

            for(int i=0; i<3; i++)
            {
                sum += score[i];
            }    
            return sum; //배열을 리턴하는것이 아님
        }
        static void Main(string[] args) // 여기서 먼저 입력하고 계산된 걸 가져오고
               //메인은 시간이 흐름(동작). 출력까지 다 하고 어떻게 돌아가는지는 위에서
        {
            int[] score = inputScore(); //입력

            int sum = GetSum(score); //계산이 된 것을 가져옴

            Console.WriteLine($"총점: {sum}");//출력

        }
    }
}

--------------------------------------------------------------
//
namespace ScoreApp02
{
    internal class Program
    {

        static int TotalScore(int a, int b, int c)
        {
            return a + b + c;
        }
        static int AvgScore(int a, int b, int c)
        {
            return a + b + c;
        }
        static void Main(string[] args)
        {
            int totalScore = TotalScore(50,50, 50);
            Console.WriteLine(totalScore);
            Console.WriteLine($"평균값: {totalScore / 3.0:F2}");
        }
    }
}

--------------------------------------------------------------
// 국영수 3과목 성적 입력 받아서 총점, 평균 출력하는 프로그램 --- 함수 3개 사용
