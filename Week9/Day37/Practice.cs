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
//

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
