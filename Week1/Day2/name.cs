namespace ConsoleApp6
{
    internal class Program // 메인 클래스
    {
        static void Main(string[] args) // 메인 메소드
        {
            string greeting;   // 변수 선언
            Console.Write("이름을 입력해 주세요: ");
            greeting = Console.ReadLine();  // 변수에 값 할당 & 초기화한다.

            Console.WriteLine($"당신의 이름은 {greeting} 입니다."); // 출력!
        }
    }
}
