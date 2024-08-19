//네트워크

    namespace network1
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                byte[] shortBytes = BitConverter.GetBytes((short)32000);
                byte[] intBytes = BitConverter.GetBytes(1652300);

                MemoryStream ms = new MemoryStream();
                ms.Write(shortBytes, 0, shortBytes.Length);
                ms.Write(intBytes, 0, intBytes.Length);

                ms.Position = 0;

                //MemoryStream으로부터 short를 역직렬화
                byte[] outBytes = new byte[2];
                ms.Read(outBytes, 0, 2);
                int shortResult = BitConverter.ToInt16(outBytes, 0);
                Console.WriteLine(shortResult);

                //Int 역직렬화
                outBytes = new byte[4];
                ms.Read(outBytes, 0, 4);
                int intResult = BitConverter.ToInt32(outBytes, 0);
                Console.WriteLine(intResult);

            }
        }
    }

---------------------------------------------------------------------------------
//미리 만들어둔 뫄뫄 파일 읽어오기
using System.Text;

namespace MemoryStreamQuiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // StreamReader = Bytes로 변환된 문자열 출력에 유리
            string path = @"C:\Temp\abc.txt";
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string txt = sr.ReadToEnd();

            // MemoryStream으로 만들기 - 문자열 직렬화
            MemoryStream ms = new MemoryStream();
            byte[] strBytes = Encoding.UTF8.GetBytes(txt);
            ms.Write(strBytes, 0, strBytes.Length);

            ms.Position = 0;

            // 역직렬화
            StreamReader sr2 = new StreamReader(ms, Encoding.UTF8, true);
            string txt2 = sr2.ReadToEnd();

            Console.WriteLine(txt2);
        }
    }
}

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
/
  
---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//

---------------------------------------------------------------------------------
//
