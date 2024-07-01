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
//1~100 더하는 것을 메소드로 만들고 메인 메소드에선 풀력만 하게
namespace Quiz03
{
    //1~100까지의 합은 5050. 이를 메소드로 만들어서, Main에서는 결과만 출력.
    //5050결과는 NumberAdd 메소드를 만들어서 동작시켜라.

    internal class Program
    {
        static int NumberAdd()
        {
            int sum = 0;
            for(int i = 0; i<=100; i++)
            {
                sum += i;
            }

            return sum;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(NumberAdd()); 
        }
    }
}

----------------------------------------------------------------------------
//max를 구하는 메소드 GetMax(int[] arr) 를 만들어 주세요.
//배열 요소값 중 가장 작은 값을 min로 대입 후 출력하라!!!
namespace Exam03
{
    internal class Program
    {//배열 요소값 중 가장 큰 값을 max로 대입 후 출력하라!!!
        static int GetMax(int[] arr)
        {
            int max = int.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        static int GetMin(int[] arr)
        {
            int min = int.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        static void Main(string[] args)
        {
            int[] arr = { -7, 5, 60, -33, 43 };
            Console.WriteLine($"최대 값은: {GetMax(arr)}");
            Console.WriteLine($"최소 값은: {GetMin(arr)}");

        }

    }

}

----------------------------------------------------------------------------
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
using System.Xml.Serialization;

namespace ScoreApp02
{
    internal class Program
    {
        static int[] InputThreeScore()
        {
            int[] score = new int[3];
            Console.WriteLine("점수를 입력하세요: ");
            for(int i = 0; i < score.Length; i++)
            {
                score[i] = Int32.Parse(Console.ReadLine());
            }
            return score;
        }
        
        static int GetTotalScore(int[] arr)
        {
            int totalScore= 0;
            foreach(int i in arr)
            {
                totalScore += i;
            }
            return totalScore;
        }
        static double GetAvg(int totalScore)
        {
            return totalScore / 3.0;
        }

        static void Main(string[] args)
        {
            
            int[] scores = InputThreeScore();  //세 과목 점수 입력 받음
            int totalScore = GetTotalScore(scores); //꺼내오는 함수
            double avg = GetAvg(totalScore);

            Console.WriteLine($"총점: {GetTotalScore}");
            Console.WriteLine($"평균: {GetAvg:F2}");
        }
    }
}

--------------------------------------------------------------
//교재내용 실습
namespace OOP01
{
    class Mathmatics
    {
        //멤버변수
        //생성자
        //멤버 메소드
        public int f(int x)
        {
            return x * x;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Mathmatics m = new Mathmatics(); //new할 때 생성됨
            int result = m.f(9);
            Console.Write(result);
        }
    }
}

--------------------------------------------------------------
namespace SwapByValue
{
    internal class Program
    {
        static void Swap(int a, int b)        //복사해서 새로 할당한거 ㅇㅇ
        {
            int temp = b;
            b = a; 
            a = temp;
            Console.WriteLine($"{a} {b}");
        }                                      //기존에는 여기서 리턴도 없고 하니 없던일로 되어서(pop)
        static void Main(string[] args)
        {
            int x = 3, y = 4;
            Console.WriteLine($"{x} {y}");
            Swap(x, y);
            Console.WriteLine($"{x} {y}");     //여기에는 메인에서 선언한 대로 3, 4로 나오는데,
        }
    }
}

↓ 바꾸면
--------------------------------------------------------------
namespace SwapByValue
{
    internal class Program
    {
        static void Swap(ref int a, ref int b)
        {
            int temp = b;
            b = a; 
            a = temp;
            Console.WriteLine($"{a} {b}");
        }                                   //ref사용함 ref는 새로운 변수가 아니라 포인터 주소임
        static void Main(string[] args)
        {
            int x = 3, y = 4;
            Console.WriteLine($"{x} {y}");
            Swap(ref x, ref y);             
            Console.WriteLine($"{x} {y}");  // Swap()에서 바꾼대로 4, 3으로 나옴
        }
    }
}

