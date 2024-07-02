//do while문으로 구구단 출력
namespace DoWhile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            do
            {
                Console.WriteLine($"3 * {i} = {3 * i} ");
                i++;
            } while (i < 10);
            
        }
    }
}

-------------------------------------------------------------------
//메뉴 선택
namespace DoWhile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("번호를 선택하세요.");
                Console.WriteLine("1. DB 입력");
                Console.WriteLine("2. DB 검색");
                Console.WriteLine("3. DB 수정");
                Console.WriteLine("4. DB 삭제");
                Console.WriteLine("5. 프로그램 종료");
                Console.Write("번호 선택: ");

                choice = Int32.Parse(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        Console.WriteLine("DB 입력을 선택하셨습니다.");
                        break;
                    case 2:
                        Console.WriteLine("DB 검색을 선택하셨습니다.");
                        break;
                case 3:
                        Console.WriteLine("DB 수정을 선택하셨습니다.");
                        break;
                case 4:
                        Console.WriteLine("DB 삭제를 선택하셨습니다.");
                        break;
                case 5:
                        Console.WriteLine("프로그램 종료");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            while (choice != 5);
        }
    }
}

-------------------------------------------------------------------
//
