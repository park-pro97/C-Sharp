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
//리스트 표현법
 internal class Program
    {
        static void Main(string[] args)
        {
            List<int> intList = new List<int>();
            intList.Add(10); intList.Add(20); intList.Add(30);
            foreach (int i in intList) { Console.WriteLine(i); }
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
namespace Listprac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                list.Add(random.Next(1, 101));
            }
            list.Sort();
            list.Reverse();
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            list.Insert(0, -7);
            list.Insert(list.Count, -100);
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            list.Remove(-7);
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}


----------------------------------------------------------------------------------------------------------
//
using System.Globalization;

namespace ListTestApp
{
    class Album
    {
        //private int no;
        //private string title;
        //private StringInfo artist;

        public int No { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }
    class NewJeansAlbum : Album
    {

    }   
    internal class Program
    {
        static void Main(string[] args)
        {
            List<NewJeansAlbum> albumList = new List<NewJeansAlbum>();
            NewJeansAlbum album = new NewJeansAlbum();
            album.No = 1;
            album.Title = "슈퍼 내추럴";
            album.Artist = "뉴진스";
            albumList.Add(album);


            album = new NewJeansAlbum();
            album.No = 2;
            album.Title = "하우 스윗";
            album.Artist = "뉴진스";
            albumList.Add(album);

            

            foreach(NewJeansAlbum na in albumList)
            {
                Console.WriteLine(na.No);
                Console.WriteLine(na.Title);
                Console.WriteLine(na.Artist);
                Console.WriteLine();
            }
        }
    }
}


----------------------------------------------------------------------------------------------------------
//
using System.Net;

namespace MYddressBook
{
    internal class Program
    {
        class Address
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
        }
        static void Main(string[] args)
        {
            List<Address> list = new List<Address>();
            int choice = 0;
            do
            {
                Console.WriteLine("1. 데이터 삽입");
                Console.WriteLine("2. 데이터 삭제");
                Console.WriteLine("3. 데이터 조회");
                Console.WriteLine("4. 데이터 수정");
                Console.WriteLine("5. 프로그램 종료");
                Console.Write("선택한 번호: ");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        InsertData(list);
                        break;
                    case 2:
                        DeleteData(list);
                        break;
                    case 3:
                        SearchData(list);
                        break;
                    case 4:
                        UpdateData(list);
                        break;
                    case 5:
                        Console.WriteLine("프로그램 종료.");
                        break;
                }
            } while (choice != 5);
        }
        static void InsertData(List<Address> list)
        {
            Console.Write("ID를 입력하세요: ");
            int Id = int.Parse(Console.ReadLine());
            Console.Write("이름을 입력하세요: ");
            string Name = Console.ReadLine();
            Console.Write("전화번호를 입력하세요: ");
            string Phone = Console.ReadLine();

            list.Add(new Address { Id = Id, Name = Name, Phone = Phone });
            Console.WriteLine("완료");
            Console.WriteLine();
        }
        static void DeleteData(List<Address> list)
        {
            Console.Write("삭제할 ID를 입력하세요: ");
            int Id = Int32.Parse(Console.ReadLine());
            Address addressDelete = list.Find(a => a.Id == Id);
            if (addressDelete != null)
            {
                list.Remove(addressDelete);
                Console.WriteLine("삭제되었습니다.");
            }
            else
            {
                Console.WriteLine("ID를 찾을 수 없습니다.");
            }
            Console.WriteLine();
        }
        static void SearchData(List<Address> list)
        {
            foreach (var address in list)
            {
                Console.WriteLine($"ID               NAME              PHONE");
                Console.WriteLine($"{address.Id}                {address.Name}            {address.Phone}");
            }
            Console.WriteLine();
        }
        static void UpdateData(List<Address> addressList)
        {
            Console.Write("수정할 ID를 입력해 주세요: ");
            int id = Int32.Parse(Console.ReadLine());
            Address addressToUpdate = addressList.Find(a => a.Id == id);
            if (addressToUpdate != null)
            {
                Console.Write("새 이름을 입력해 주세요: ");
                addressToUpdate.Name = Console.ReadLine();
                Console.Write("새 전화번호를 입력해 주세요: ");
                addressToUpdate.Phone = Console.ReadLine();
                Console.WriteLine("데이터가 수정되었습니다.");
            }
            else
            {
                Console.WriteLine("ID를 찾을 수 없습니다.");
            }
            Console.WriteLine();
        }
    }
}



----------------------------------------------------------------------------------------------------------
//


----------------------------------------------------------------------------------------------------------
//
