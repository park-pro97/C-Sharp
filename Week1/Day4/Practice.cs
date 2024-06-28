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
