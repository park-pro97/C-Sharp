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
            Console.WriteLine($"보너스 번호: {bonus}");
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

---------------------------------------------------------------------------------------------------------------
// 생성자, 소멸자
namespace TestApp01
{
    class Person
    {
        public Person()
        {
            Console.WriteLine("디폴트 생성자 호출");
        }
        ~Person()
        {
            Console.WriteLine("소멸자 호출");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
        }
    }
}


---------------------------------------------------------------------------------------------------------------
//get set 초기값 설정
namespace TestApp02
{
    class User
    {
        private readonly string userID; //상수 처리
        private readonly string userPW; //상수 처리

        public User(string userID, string userPW)
        {
            this.userID = userID;
            this.userPW = userPW;
        }
        public void Print()
        {
            Console.WriteLine(this.userID);
            Console.WriteLine(this.userPW);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string uID = "abc";
            string uPW = "abc";

            User user = new User(uID, uPW);
            user.Print();

        }
    }
}


---------------------------------------------------------------------------------------------------------------
// 배열 안에 숫자 넣고 메소드로 연산
namespace TestApp03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr4 = new int[5] { 21, 98, 43, 27, 13 };
            int[] arr5 = { 21, 98, 43, 27, 13 };

            Console.WriteLine($"Max : {arr4.Max()}");
            Console.WriteLine($"Max : {arr4.Min()}");
            Console.WriteLine($"Max : {arr4.Sum()}");
            Console.WriteLine($"Max : {arr4.Average()}");
            Console.WriteLine($"Max : {arr4.Count()}");
        }
    }
}


---------------------------------------------------------------------------------------------------------------
//Array 메소드를 이용해 총점, 평균 구하기
namespace TestApp03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] score = new int[3];

            for(int i = 0; i < 3; i++)
            {
                score[i] = random.Next(85, 101);
            }

            Console.WriteLine($"총점 : {score.Sum()}");
            Console.WriteLine($"평균 : {score.Average():F2}");
            Console.WriteLine($"Max : {score.Max()}");
            Console.WriteLine($"Min : {score.Min()}");

        }
    }
}

---------------------------------------------------------------------------------------------------------------
//키보드로 입력을 받으면 알파벳 대.소문자 숫자 특수문자의 개수를 출력
using System.ComponentModel.Design;
using System.Diagnostics.Tracing;

namespace TestApp05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. 문자열을 입력.
           string words = Console.ReadLine();
            //2. 변수 카운트변수 4개
            int BigCnt = 0, SmallCnt = 0, numberCnt = 0, specialCnt = 0;

            //3. 카운팅 로직
            for(int i = 0; i < words.Length; i++)
            {
                if (words[i] >= 'A' && words[i] <= 'Z')
                BigCnt++;
                else if (words[i] >= 'a' && words[i] <= 'z')
                SmallCnt++;
                else if (words[i] >= '0' && words[i] <= '9')
                numberCnt++;
                else specialCnt++;
            }
            Console.WriteLine($"대문자 : {BigCnt}");
            Console.WriteLine($"소문자 : {SmallCnt}");
            Console.WriteLine($"숫자 : {numberCnt}");
            Console.WriteLine($"특수문자 : {specialCnt}");
        }
    }
}


---------------------------------------------------------------------------------------------------------------
//달팽이 출력
