//배열 연습
namespace ArrayTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] Numbers = new int[5] { 1, 2, 3, 4, 5 }; //배열을 선언과 동시에 초기화
            //studentIDs[0] = 1;         //선언 후 초기화
            string[] studentNames = new string[3] {"Ralo", "Paka", "Mouse"};

            int[] evenNums = new int[10];
            for(int x = 0; x < 10; x++)
            {
                evenNums[x] = x * 2;

                Console.WriteLine($"You just saved {evenNums[x]}");
            }
        }
    }
}

-------------------------------------------------------------------------------------------------------------------------------------------------
//배열 연습
namespace ArrayTest02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //변수선언
            int[] numbers = new int[7];
            for(int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = 13 * (i + 1);
                Console.WriteLine(numbers[i]);
            }
            string[] names = new string[3];
            for(int j = 0; j < names.Length; j++)
            {
                names[j] = Console.ReadLine();
                Console.WriteLine(names[j]);
            }
            
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------------------------
//배열 연습(클래스를 배열처럼 사용할 수 있게 만들어줌)
using System.Runtime.CompilerServices;

namespace IndexText
{
    class IdxDemo
    {
        private int[] num = new int[5];

        public int this[int x]
        {
            set { num[x] = value; }
            get { return num[x]; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IdxDemo test = new IdxDemo();
            for(int i = 0; i < 5; i++)
            {
                test[i] = i;
                Console.WriteLine(test[i]);
            }
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------------------------

 \   namespace Code99
{//배열처럼 만들자
    enum Days { Sun=, Mon, Tue, Wen, Thu, Fri, Sat }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Days.Sun);
            Console.WriteLine(Days.Mon);
            Console.WriteLine(Days.Tue);
            Console.WriteLine(Days.Wen);
            Console.WriteLine(Days.Thu);
            Console.WriteLine(Days.Fri);
            Console.WriteLine(Days.Sat);
        }
       
    }
}

