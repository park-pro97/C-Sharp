//List와 ArrayList
using System.Collections;

namespace ListApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1); list.Add(2); list.Add(3);
            foreach (int i in list) { Console.WriteLine(i); }

            // ArrayList는 <> 사이에 제네릭이 필요가 없음
            ArrayList alist = new ArrayList();
            alist.Add('A'); alist.Add('B'); alist.Add('C');
            alist.Insert(2, 'E'); alist.RemoveAt(0);
            foreach (char ch in alist) { Console.WriteLine(ch); }

            // 각각 명시해주면 정상적으로 출력 가능 - 권장하지 않는 방법
            ArrayList blist = new ArrayList();
            blist.Add(1); blist.Add('Z');
            Console.WriteLine((int)blist[0]); Console.WriteLine((char)blist[1]);
        }
    }
}


------------------------------------------------------------------------------------
//Stack
namespace StackTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1); stack.Push(2); stack.Push(3);
            while (stack.Count > 0) { Console.WriteLine(stack.Pop()); }
        }
    }
}
------------------------------------------------------------------------------------
//Queue
namespace QueueApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<string> que = new Queue<string>();
            que.Enqueue("사과"); que.Enqueue("딸기"); que.Enqueue("배");
            while (que.Count > 0) { Console.WriteLine(que.Dequeue()); }
        }
    }
}
------------------------------------------------------------------------------------
//Hashtable       [Key(키)와 Value(값)가 둘 다 존재]
using System.Collections;

namespace HashtableApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hashtable ht = new Hashtable();
            ht["하나"] = "one"; ht["둘"] = "two" ; ht["셋"] = "three"; ht["넷"] = "four";
            Console.WriteLine(ht["하나"]);
            Console.WriteLine(ht["둘"]);
            Console.WriteLine(ht["셋"]);
            Console.WriteLine(ht["넷"]);
        } 
    }
}
------------------------------------------------------------------------------------
//예외처리     try~catch와 finally

    [특정 오류에 대해 메시지를 설정할 수도 있음.             Finally = 무조건 실행되는 구문]
    
namespace ExceptionApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 에러가 나는 부분에 try~catch 이용하기
            int a = 5; int b = 0;
            try { int result = a / b; Console.WriteLine(result); }
            catch (DivideByZeroException e) { Console.WriteLine("0으로 나누는 예외가 발생하였습니다."); }
            catch (Exception ex) { Console.WriteLine("예외가 발생하였습니다."); }
            finally { Console.WriteLine("무조건 실행되는 구문"); }
        }
    }
}
------------------------------------------------------------------------------------
// 종합예제
namespace ExceptionApp02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3 };

            try
            {
                // 얘는 강제로 발생시킨다 --- 그래서 catch의 부모 예외 클래스에 잡혔습니다.가 출력됨.
                //throw new Exception();
                int a = 5;
                int b = 6;
                int result = a / b;
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(arr[i]);
                }
            }
            catch(IndexOutOfRangeException ex)
            {
            Console.WriteLine("배열의 범위를 벗어났습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("부모 예외 클래스에 잡혔습니다.");
            }
            //무조건 실행
            finally
            {
                Console.WriteLine("무조건 실행하는 finally");
            }


            Console.WriteLine("종료");
        }
    }
}


------------------------------------------------------------------------------------
//코드로 txt파일 만들기
namespace FileTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //밑은 경로 잡아주는거
            string path = "c:\\Temp\\abc.txt";
            string content = "Hello World~!";

            File.WriteAllText(path, content);

            Console.WriteLine("파일 작성 성공");
        }
    }
}

------------------------------------------------------------------------------------
//코드로 txt파일 만들고 / 읽어오기 추가됨
namespace FileTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //밑은 경로 잡아주는거
            string path = "c:\\Temp\\abc.txt";
            string content = "안녕?ㅋㅋ C# 파일 읽고 쓰기 연습";

            File.WriteAllText(path, content);

            Console.WriteLine("파일 작성 성공");
            
            //읽기 부분 추가
            string words = File.ReadAllText(path);
            Console.WriteLine(words);
        }
    }
}

------------------------------------------------------------------------------------
//했는데 파일안의 내용은 없음 ( 리소스 반환이 필요함) 주석 지우고 파일 닫아줘야 출력됨
namespace FileTest02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\\hello.txt";
            string content = "안녕하세요. 인사 파일입니다.";

            StreamWriter writer = new StreamWriter(path);  // 아까보다 C#스러운 방식
            writer.WriteLine(content);

            //writer.Close();
            Console.WriteLine("현재 프로그램이 종료됩니다.");
        }
    }
}

------------------------------------------------------------------------------------
//LINQ 2의배수
namespace Linq01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 9, 2, 6, 4, 5, 3, 7, 8, 1, 10 };

            var result = from n in numbers
                         where n % 2 == 0
                         orderby n
                         select n;

            foreach(int n in result)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
        }
    }
}

------------------------------------------------------------------------------------
//3의 배수 내림차순
namespace Linq01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 9, 2, 6, 4, 5, 3, 7, 8, 1, 10 };

            var result = from n in numbers
                         where n % 3 == 0
                         orderby n descending //이게 내림차순 정렬
                         select n;

            foreach(int n in result)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
        }
    }
}

------------------------------------------------------------------------------------
//알파벳 역순  linq로 출력
namespace Linq02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char ch = 'A';
            char[] alpabets = new char[26];

            for (int i = 0; i < 26; i++)
                alpabets[i] = ch++;

            //Linq
            var result = from c in alpabets
                         orderby c descending
                         select c;
            foreach (char e in result)
            {
                Console.Write(e + " ");
            }
            Console.WriteLine();
        }
    }
}

------------------------------------------------------------------------------------
//Linq 연습
namespace LinqStandardTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 3, 5, 7, 9, 11, 13, 15 };

            int even = numbers.FirstOrDefault(n => n % 2 == 0);

            if (even == 0)
                Console.WriteLine("짝수가 없다");
            else
                Console.WriteLine(even);
        }
    }
}

------------------------------------------------------------------------------------
//교재 실습
namespace LinqExam03
{
    //p624
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Person(string name, int age, string address)
        {
            Name = name;
            Age = age;
            Address = address;
        }
        public override string ToString()
        {
            return string.Format($"{Name}{Age}{Address}");
        }
    }
    class MainLanguage
    {
        public string Name { get; set; }
        public string Language { get; set; }

        public MainLanguage(string name, string language)
        {
            Name = name;
            Language = language;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>
            {
                new Person("Tom", 63, "Korea"),
                new Person("Winnie", 40, "Tibet"),
                new Person("Anders", 47, "Sudan"),
                new Person("Hans", 25, "Tibet"),
                new Person("Eureka", 32, "Sudan"),
                new Person("Hawk", 15, "Korea")
            };

            List<MainLanguage> languages = new List<MainLanguage>
            {
                new MainLanguage("Anders", "Delphi"),
                new MainLanguage("Anders", "C#"),
                new MainLanguage("Tom", "Borland C++"),
                new MainLanguage("Hans", "Visual C++"),
                new MainLanguage("Winnie", "R")
            };

        }
    }
}

------------------------------------------------------------------------------------
//


------------------------------------------------------------------------------------
//

    
------------------------------------------------------------------------------------
//


------------------------------------------------------------------------------------
//
