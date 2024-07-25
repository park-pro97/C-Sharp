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
//


------------------------------------------------------------------------------------
//

------------------------------------------------------------------------------------
//

------------------------------------------------------------------------------------
//

------------------------------------------------------------------------------------
//

------------------------------------------------------------------------------------
//

------------------------------------------------------------------------------------
//

------------------------------------------------------------------------------------
//

------------------------------------------------------------------------------------
//
