//재귀함수
namespace Recursive01
{
    internal class Program
    {
        static int Factorial(int n)
        {
            if (n == 1)
                return n;
            else
                return n * Factorial(n - 1);
        }
        
        static void Main(string[] args)
        {
            int a = 5;
            Console.WriteLine(Factorial(a));
        }
    }
}


------------------------------------------------------------------------------------------------------------------------------
↓ 교재 내용 실습
  
namespace FactorialDynamic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            int[] arr = new int[n + 1];

            arr[0] = 1; //핵심 
            for(int i = 1; i <= n; i++)
            {
                arr[i] = i * arr[i - 1];
            }
            Console.WriteLine(arr[n]);
        }
    }
}


------------------------------------------------------------------------------------------------------------------------------
using System.Numerics;

namespace ConsoleApp12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 17;
            BigInteger[] arr = new BigInteger[n + 1];
            arr[0] = 1;
            for(int i = 1; i <= n; i++)
            {
                arr[i] = i * arr[i - 1];
            }
            foreach(BigInteger i in arr)
                Console.Write(i + " ");    
        }
    }
}



------------------------------------------------------------------------------------------------------------------------------

  namespace LocalVariableTest
{
    internal class Program
    {
        static string name = "David";
        static void ShowName()
        {
            name = "홍길동";
            Console.WriteLine(name);
        }
        static void Main(string[] args)
        {
            ShowName();
            Console.WriteLine(name);
        }
    }
}


------------------------------------------------------------------------------------------------------------------------------

namespace Code68
{
    class BankAccoumt
    {
        private double balance = 0;
        public void Deposit(double money)
        {
            balance += money;
        }
        public void Withdraw(double money)
        {
            balance -= money;
        }
        public void GetBalance()
        {
            Console.WriteLine($"잔고 확인: {balance}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccoumt account = new BankAccoumt();
            account.Deposit(0000);

            account.Withdraw(30000);
            account.GetBalance();
            
        }
    }
}


------------------------------------------------------------------------------------------------------------------------------
