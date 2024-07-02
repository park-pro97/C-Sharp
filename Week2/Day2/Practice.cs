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
//게임 메뉴 만들기
    매개변수로 딜레이와 한줄 띄우기를 만들어 함수2개 사용하지 않고 만들면 더 편함
namespace DoWhile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            Console.WriteLine("용사의 서막.");
            Console.WriteLine();
            Console.Write("이름을 알려주세요: ");
            name = Console.ReadLine();
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine($"{name} 형님!!! 반갑습니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("드디어 떠나는 모험의 첫 걸음을 내딛게 되었군요.");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("먼 길을 험난한 여정을 앞두고 있지만,");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("용기와 지혜로 모든 위기를 헤쳐나가길 바랍니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("플레이어는 각 선택에 따라 다양한 이야기를 경험하게 됩니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("게임에는 여러 가지 숨겨진 요소와 이스터 에그가 포함되어 있습니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("플레이어는 자신의 선택에 따라 게임의 결말을 바꿀 수 있습니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine("다음은 플레이어가 선택할 수 있는 첫 번째 메뉴입니다.");
            Console.WriteLine();
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine();

            int choice;
            do
            {
                Console.WriteLine("번호를 선택하세요.");
                Console.WriteLine("1. 마을 방문");
                Console.WriteLine("2. 숲 속 오두막 방문");
                Console.WriteLine("3. 게임 종료");
                Console.WriteLine();
                Console.Write("번호 선택: ");
                Console.WriteLine();

                choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("플레이어가 낡은 마을에 도착합니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        Console.WriteLine("마을 주민들과 대화하고, 마을의 비밀을 파헤칠 수 있는 단서를 얻습니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        Console.WriteLine("마을의 문제를 해결하기 위해 퀘스트를 수행해야 할 수도 있습니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        Console.WriteLine("퀘스트를 완료하면 보상을 받을 수 있습니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        break;
                    case 2:
                        Console.WriteLine("플레이어가 숲 속 오두막에 도착합니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        Console.WriteLine("오두막에는 은둔하는 마법사가 살고 있습니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        Console.WriteLine("마법사로부터 새로운 기술을 배우거나, 아이템을 구입할 수 있습니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        Console.WriteLine("마법사는 플레이어의 여정에 중요한 조언을 해줄 수도 있습니다.");
                        Console.WriteLine();
                        Thread.Sleep(500);
                        break;
                    case 3:
                        Console.WriteLine("게임을 종료합니다.");
                        break;

                }
                Console.WriteLine();
                Console.WriteLine();
            }
            while (choice != 3);
        }
    }
}

-------------------------------------------------------------------
//3개의 문제에 번호를 달아 입력받으면 정답을 출력  ---- 메소드 하나씩 따로 만들면 메인이 더 간편해짐
namespace Quiz09
{
    internal class Program
    {

        
        static void Main(string[] args)
        {
            int choice = 1;
            do
            {
                Console.WriteLine("문제를 선택하세요.");
                Console.WriteLine();
                Console.WriteLine("1. 1 ~ 100까지의 홀수만 출력");
                Console.WriteLine("2. A ~ Z, a ~ z 출력");
                Console.WriteLine("3. 12와 18의 최대공약수 출력");
                Console.WriteLine("4. 프로그램 종료");
                Console.WriteLine();
                Console.Write("문제 선택: ");

                choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("1번: 1 ~ 100까지의 홀수만 출력");
                        Console.WriteLine();
                        for(int i = 1; i <= 100; i = i += 2)
                        {
                            Console.Write($"{i} " );
                        }
                        
                        break;
                    case 2:
                        Console.WriteLine("2번: A ~ Z, a ~ z 출력");
                        for(char c = 'A'; c <= 'Z'; c++)
                        {
                            Console.Write($"{c} ");
                        }
                        Console.WriteLine();
                        for (char d = 'a'; d <= 'z'; d++)
                        {
                            Console.Write($"{d} ");
                        }
                        break;
                    case 3:
                        Console.WriteLine("3번: 12와 18의 최대공약수 출력");
                        int num1 = 12;
                        int num2 = 18;
                        int a = num1;
                        int b = num2;
                        while(b!= 0)
                        {
                            int temp = b;
                            b = a % b;
                            a = temp;
                        }
                        int gcd = a;
                        Console.WriteLine();
                        Console.WriteLine($"답: {a}");
                        break;
                    case 4:
                        Console.WriteLine("프로그램 종료");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            while (choice != 4);
        }
    }
}
