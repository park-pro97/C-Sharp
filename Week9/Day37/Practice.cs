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
//메모리에 파일 내용 쓰는 방법 또는 기법
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
//스레드를 활용한 Hello World 출력
namespace Thread01
{
    internal class Program
    {
        static void Print()
        {
            Console.WriteLine("Hello World!");
        }
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(Print));
            t1.IsBackground = true; t1.Start();

            t1.Join();
            Console.WriteLine("Main 프로그램 종료");
        }
    }
}

---------------------------------------------------------------------------------
//WorkThreadSync - 스레드 동기화
namespace WorkThread01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 작업 스레드 수 설정
            int threadCount = 5;

            // 스레드 배열 생성
            Thread[] threads = new Thread[threadCount];

            // 각 스레드에 작업 할당
            for (int i = 0; i < threadCount; i++)
            {
                int threadIndex = i; // 로컬 변수로 인덱스를 캡처
                threads[i] = new Thread(() => DoWork(threadIndex));
                threads[i].Start();
            }

            // 모든 스레드가 작업을 마치기를 기다림
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("모든 작업이 완료되었습니다.");
        }
        // 각 스레드에서 수행할 작업 메서드
        static void DoWork(int index)
        {
            Console.WriteLine($"스레드 {index} 시작: 작업을 수행 중...");

            // 간단한 작업 시뮬레이션 (예: 1초 동안 대기)
            Thread.Sleep(1000);

            Console.WriteLine($"스레드 {index} 완료: 작업이 끝났습니다.");
        }
    }
}

---------------------------------------------------------------------------------
//스레드 싱크 에러 연습
//콘솔을 실행할 때마다 최종 값이 달라지는 스레드 코드가 있을 때, 코드를 수정해서 항상 최종값이 0이 나오도록 하는 코드
using System;
using System.Threading;

namespace ThreadSyncError
{
    class Program
    {
        static int sharedValue = 0;
        private static readonly object lockObject = new object();
        static void Main(string[] args)
        {
            Thread incrementThread = new Thread(Increment);
            Thread decrementThread = new Thread(Decrement);

            // 스레드 시작
            incrementThread.Start();
            decrementThread.Start();

            // 스레드가 종료되기를 기다림
            incrementThread.Join();
            decrementThread.Join();

            Console.WriteLine($"최종 값: {sharedValue}");
        }

        static void Increment()
        {
            lock (lockObject)
            {
                for (int i = 0; i < 100000; i++)
                {
                    sharedValue++;
                }
            }
        }

        static void Decrement()
        {
            lock (lockObject)
            {
                for (int i = 0; i < 100000; i++)
                {
                    sharedValue--;
                }
            }
        }
    }
}

---------------------------------------------------------------------------------
//FileReaderWriter [ 파일을 작성하고 읽을 수 있도록 하는 코드 ]
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderWriter01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Temp\\file.log";
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine("Hello C#");
                sw.Flush();
                sw.Close();

                fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                string str = sr.ReadToEnd();
                Console.WriteLine(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

---------------------------------------------------------------------------------
//BinaryReaderWriter [ 2진 파일인 경우에는 BinaryWriter를 사용하는 것이 좋음 ]  -영어일 때 좋음
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderWriter_Binary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Temp\\pic1.png";
            byte[] picture;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryReader br = new BinaryReader(fs);
                picture = br.ReadBytes((int)fs.Length);
                br.Close();
            }
            path = "C:\\Temp\\pic2.png";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(picture);
                bw.Flush(); //이진파일 Flush() 신경
                bw.Close();
            }
        }
    }
}

---------------------------------------------------------------------------------
//사진 덮어쓰기 활용한 코드
namespace BinaryReaderWriter1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\pic1.png";
            byte[] picture;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryReader br = new BinaryReader(fs);
                // 사진의 Binary 값 - 직렬화 되어있는 값으로 읽어오기
                picture = br.ReadBytes((int)fs.Length);
                br.Close();
            } // 사진 파일 읽어오기 -> 메모리로 가져왔음 (byte[] picture)

            // pic2.png로 Write 해보기
            string path2 = @"C:\Temp\pic2.png";
            using (FileStream fs = new FileStream(path2, FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(picture);
                bw.Flush(); // 파일을 비우는 과정
                bw.Close();
            }
        }
    }
}

---------------------------------------------------------------------------------
//MultiThread Console_Server
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MultiThreadServer01
{
    internal class Program
    {
        static int cnt = 0;
        private static void serverFunc(object obj)
        {
            using (Socket srvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 13000);
                srvSocket.Bind(endPoint);
                srvSocket.Listen(50);

                Console.WriteLine("서버 시작...");
                while (true)
                {
                    // Client Socket의 위치를 알게 됨 (Accept로 인해)
                    Socket cliSocket = srvSocket.Accept();
                    cnt++;

                    // Client에서 받은 일을 처리 - Thread로 만들어서 처리
                    // 1대多 를 1대1 로 만들어주는 과정
                    Thread workThread = new Thread(new ParameterizedThreadStart(workFunc));
                    workThread.IsBackground = true;

                    workThread.Start(cliSocket);
                }
            }
        }//end of servFunc

        private static void workFunc(object obj)
        {
            byte[] recvBytes = new byte[1024];
            Socket cliSocket = (Socket)obj;
            int nRecv = cliSocket.Receive(recvBytes);

            string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
            Console.WriteLine($"클라이언트 번호 ({cnt}) : {txt}");
            byte[] sendBytes = Encoding.UTF8.GetBytes("Hello : " + txt);
            cliSocket.Send(sendBytes);
            cliSocket.Close();
        }

        static void Main(string[] args)
        {
            Thread serverThread = new Thread(serverFunc);
            serverThread.IsBackground = true;
            serverThread.Start();

            serverThread.Join();
            Console.WriteLine("서버 프로그램을 종료합니다.");
        }
    }
}

---------------------------------------------------------------------------------
//MultiThread Console_Client
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadClient
{
    internal class Program
    {
        static void clientFunc(object obj)
        {
            try
            {
                //1.소켓만들기
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.연결
                //EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 10000);
                //IP에 접속하고 싶은 IP를 입력해줄 것
                EndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13000);
                socket.Connect(serverEP);
                //3.Read, Write
                //write
                //byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                byte[] buffer = Encoding.UTF8.GetBytes("푸바오 사랑해~~");
                socket.Send(buffer);

                //read
                byte[] recvBytes = new byte[1024];
                int nRecv = socket.Receive(recvBytes);

                string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                Console.WriteLine(txt);
                //4.종료
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            Thread clientThread = new Thread(clientFunc);
            clientThread.Start();
            clientThread.IsBackground = true;
            clientThread.Join();

            Console.WriteLine("클라이언트가 종료 되었습니다.");
        }
    }
}
---------------------------------------------------------------------------------
//IP 교수님 걸로 찍고 메시지 보내기
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadClient
{
    internal class Program
    {
        static void clientFunc(object obj)
        {
            try
            {
                //1.소켓만들기
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.연결
                //EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 10000);
                //IP에 접속하고 싶은 IP를 입력해줄 것
                EndPoint serverEP = new IPEndPoint(IPAddress.Parse("192.168.81.71"), 13000);
                socket.Connect(serverEP);
                //3.Read, Write
                //write
                //byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                byte[] buffer = Encoding.UTF8.GetBytes(""); // 여기
                socket.Send(buffer);

                //read
                byte[] recvBytes = new byte[1024];
                int nRecv = socket.Receive(recvBytes);

                string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                Console.WriteLine(txt);
                //4.종료
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            Thread clientThread = new Thread(clientFunc);
            clientThread.Start();
            clientThread.IsBackground = true;
            clientThread.Join();

            Console.WriteLine("클라이언트가 종료 되었습니다.");
        }
    }
}
