//delegate 사용
namespace OOPDelegateApp
{
    class Report()
    {
        public string Police()
        {
            return "경찰에 신고하다.";
        }
        public string Fire()
        {
            return "소방서에 신고하다.";
        }
        public string Tax()
        {
            return "국세청에 신고하다.";
        }
    }
    internal class Program
    {
        delegate string Call();
        static void Main(string[] args)
        {
            Report rep = new Report();
            Call call = rep.Police;
            Console.WriteLine(call());
            call = rep.Fire;
            Console.WriteLine(call());
            call = rep.Tax;
            Console.WriteLine(call());
        }
    }
}


----------------------------------------------------------------------------------------------------------
//리스트 사용법
namespace ListTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.리스트를 만들고 10, 20 ... ~ 50까지 넣기 정수형 list이름은 numberList
            List<int> numberList = new List<int>();
            int num = 10;
            for(int i=0; i<5; i++)
            {
                numberList.Add(num);
                num += 10;
            }

            Console.WriteLine($"리스트 요소의 수 : {numberList.Count}");
            Console.WriteLine($"리스트가 가질 수 있는 최대 자료의 수 : {numberList.Capacity}");
            numberList.RemoveAt(3); //index 제거, 전체 크기가 하나 줄어듦
            numberList.Remove(20); //값으로 제거, 전체크기는 줄지 않음, 중복되면 앞쪽 값 1개제거
            numberList.Insert(0, 5);

            numberList.Sort();
            numberList.Reverse(); //값을 역순으로..


            //출력
            foreach (int i in numberList)
            {
                Console.WriteLine(i);
            }
            
        }
    }
  
----------------------------------------------------------------------------------------------------------
//


----------------------------------------------------------------------------------------------------------
//


----------------------------------------------------------------------------------------------------------
//


----------------------------------------------------------------------------------------------------------
//


----------------------------------------------------------------------------------------------------------
//
