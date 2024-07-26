//1~100 정수 / 리스트로 홀짝까지 3개 만들고 출력
namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> arr = new List<int>();
            List<int> oddList = new List<int>();
            List<int> evenList = new List<int>();

            for(int i = 0; i < 100; i++)
            {
                arr.Add(i+1);
            }
            foreach(int i in arr)
            {
                if(i % 2 != 0)
                    oddList.Add(i);
                else
                    evenList.Add(i);
            }
            foreach(int i in oddList)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            foreach (int i in evenList)
            {
                Console.Write(i + " ");
            }

        }
    }
}

---------------------------------------------------------------------------
//두 리스트의 교집함
namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] list1 = { 1,2,2,3,4};
            int[] list2 = {2,3,5,6};

            var intersection = list1.Intersect(list2);
            foreach (int i in intersection)
            { 
                Console.WriteLine(i);
            }
            Console.WriteLine();
        }
    }
}

---------------------------------------------------------------------------
//파일처리
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path =  @"C:\temp\abc.txt";
            string content = "Hello World~!~!!!~";

            File.WriteAllText(path, content);
        }
    }
}

@@@@@@@@@@@@@@@@@@
이렇게도 됨
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText(@"C:\\temp\\abc.txt", "파일처리 연습");
        }
    }
}
---------------------------------------------------------------------------
//읽고 쓰기
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp\abc.txt";
            
            FileInfo fi = new FileInfo(path);

            using(StreamWriter sw = fi.CreateText())
            {
                sw.WriteLine("안녕하신가");
            }

            //읽기
            using (StreamReader sr = fi.OpenText())
            {
                var s = "";
                while((s = sr.ReadLine()) != null) // -1을 만나면 끝나는데 널이 eof eof가 정수로 치면 -1임
                {
                    Console.WriteLine(s);
                }
            }
        
            
        }
    }
}
---------------------------------------------------------------------------
//파일 퀴즈 1번쓰기 2번 읽기
namespace File01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp\result1.txt";
            FileInfo fi =  new FileInfo(path);
            using (StreamWriter sw = fi.CreateText())
            {
                for(int i = 1; i <= 100; i++)
                {
                    if(i % 5 == 0)
                        sw.Write (i + " ");
                }

            }
            //읽기
            using (StreamReader sr = fi.OpenText())
            {
                var s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }


        }
    }
}
---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//


---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//

---------------------------------------------------------------------------
//
