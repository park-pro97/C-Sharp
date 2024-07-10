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
