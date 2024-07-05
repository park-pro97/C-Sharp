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
//랜덤으로 로또번호 출력하기(중복X, 정렬까지) 해본거
namespace RandomApp02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] num = new int[6];
            int bonus;
            Random random = new Random();

            Console.Write("로또 번호: ");

            // 중복 없는 번호 생성
            for (int i = 0; i < num.Length; i++)
            {
                int newNumber;
                do
                {
                    newNumber = random.Next(1, 46); // 1부터 45까지의 숫자
                } while (num.Contains(newNumber));
                num[i] = newNumber;
            }

            // 오름차순 정렬
            Array.Sort(num);

            // 로또 번호 출력
            foreach (int n in num)
            {
                Console.Write($"{n} ");
            }
            Console.WriteLine();

            // 보너스 번호 생성 (기존 번호와 중복되지 않도록)
            do
            {
                bonus = random.Next(1, 46);
            } while (num.Contains(bonus));

            Console.Write("보너스 번호: ");
            Console.WriteLine(bonus);
        }
    }
}

---------------------------------------------------------------------------------------------------------------
//위와 같은 문제 배열만 사용  (알고리즘 측면에선 이 방식이 제일)  -- 달팽이 배열 풀 때 용이
namespace LottoApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[7];
            Random random = new Random();

            for(int i=0; i<7; i++)
            {
                numbers[i] = random.Next(1, 46);
                //전수조사
                for(int j=0; j<i; j++)
                {
                    if(numbers[i] == numbers[j])
                    {
                        i--;
                        break;
                    }
                }
            }
            int bonusNumber = numbers[6];
            int[] lottoNumbers = new int[6];
            Array.Copy(numbers, 0, lottoNumbers, 0, 6);

            Array.Sort(lottoNumbers);
            foreach (int i in lottoNumbers)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"보너스 번호 {bonusNumber}");
        }
    }
}

---------------------------------------------------------------------------------------------------------------
//위와 같은 문제(리스트 정렬기능)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootoApp02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<int> lottoNumberList = new List<int>(); // 정수만 받는 리스트 (리스트에 자체 정렬기능이 있음)

            while(lottoNumberList.Count < 7) // 숫자 7개 될 때까지 계속 돌아라
            {
                int number = random.Next(1, 46);

                //중복체크
                if (lottoNumberList.Contains(number)) ; //요소를 검색할 수 있는 -> Contains
                    lottoNumberList.Add(number);
            }
            //보너스 번호 뽑기 0번지 첫 번째 요소를 보너스 번호로 지정하자
            int bounsNumber = lottoNumberList[0];
            lottoNumberList.RemoveAt(0); //7개는 유지하고 0번지는 뽑아버린다. 그러면 남는 6개가 로또(정렬 안 됐음)

            //이제 로또 6개 정렬
            lottoNumberList.Sort();
            foreach(int i in lottoNumberList)
            {
                Console.Write(i+" ");
            }
            Console.WriteLine();

            //보너스 번호 출력
            Console.WriteLine($"보너스 번호: {bounsNumber}");
        }
    }
}

---------------------------------------------------------------------------------------------------------------
//마지막 가장 C#스러운 방식-- 고급스러운
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoApp03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            HashSet<int> lottoNumbers = new HashSet<int>();

            while(lottoNumbers.Count < 6)
            {
                int number = random.Next(1, 46);
                lottoNumbers.Add(number);
            }//로또 번호 6개 만들기 끝.

            //보너스 번호
            int bonusNumber;
            do
            {
                bonusNumber = random.Next(1, 46);
            }while(lottoNumbers.Contains(bonusNumber)); // 보너스 번호 뽑는데 포함되어 있나? 보고 아니면 출력

            //출력
            //로또번호
            foreach(int number in lottoNumbers)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
            //보너스
            Console.WriteLine($"보너스 번호: ");
        }
    }
}


---------------------------------------------------------------------------------------------------------------
//오름차순 버블정렬
namespace BubbleSortApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //오름차순 버블정렬
          
            int[] list = { 4, 5, 7, 3, 2, 1, 9, 8 };
            //Array.Sort(arr);
            int temp;
            for(int i= list.Length - 1; i>0; i--)
            {
                for(int j=0; j<i; j++)
                {
                    if (list[j] > list[j+1])
                    {
                        //교환조건 이루어짐
                        temp = list[j];
                        list[j] = list[j+1];
                        list[j+1] = temp;
                    }
                }
            }
            foreach(int i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}

---------------------------------------------------------------------------------------------------------------
//버블정렬 - for index가 커지는 방식으로 작성
namespace BubbleSortApp02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //첫번째 for의 i값이 커지는 방식으로 작성한 버블정렬
          
            int[] list = { 4, 5, 7, 3, 2, 1, 9, 8 };
            //Array.Sort(arr);
            int temp;
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = 0; j < list.Length - 1 - i; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        // 교환조건 이루어짐
                        temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}
