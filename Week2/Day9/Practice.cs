// 랜덤 숫자 3개 뽑아서 평균 만들기
using System.Threading.Tasks.Sources;

namespace RandomApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] score = new int[3];
            int total = 0;
            double avg = 0.0;

            Random random = new Random();

            for(int i = 0; i < 3; i++)
            {
                score[i] = random.Next(1, 101);
                total += score[i];
                Console.WriteLine(score[i]);
            }

            avg = (double)total / score.Length;
            Console.WriteLine($"평균: {avg:F2}");
        }
    }
}

---------------------------------------------------------------------------------------------------------------
//랜덤으로 로또번호 출력하기(중복허용)
namespace RandomApp02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] num = new int[6];
            int bonus = 0;
            Random random = new Random();

            Console.Write("로또 번호: ");
            
            for(int i = 0; i < num.Length; i++)
            {
                num[i] = random.Next(1, 45);
                bonus = random.Next(1, 45);
                Console.Write($"{num[i]} ");
            }
            Console.WriteLine();

            Console.Write("보너스 번호: ");
            Console.WriteLine(bonus);
            
        }
    }
}

---------------------------------------------------------------------------------------------------------------
//랜덤으로 로또번호 출력하기(중복X, 정렬까지)
