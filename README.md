# Smart-factory
## 1일차 실습
//삼각형 넓이 구하는 예제를 만들어봤다.



namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Quiz 삼각형 넓이 구하기
            //1. 변수선언 및 입력부
            Console.Write("밑변 값을 입력해주세요 : ");
            int width = int.Parse(Console.ReadLine());
            Console.Write("높이를 입력해주세요 : ");
            int height = int.Parse(Console.ReadLine());

            //2. 알고리즘 수식
            double result = (width * height) / 2.0;

            //3. 출력부
            Console.WriteLine($"넓이는 {result} 입니다.");

        }
    }
}
