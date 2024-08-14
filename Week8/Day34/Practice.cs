//TCP서버
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleTCPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TCPListener 클래스를 이용해서 작업 -- 서버 만들기
            //1.Server만들고 Binding
            //1-1 ip주소 만들기, 1-2 포트 만들기
            IPAddress localAddr = IPAddress.Parse("127.0.0.1"); // ip를 가져옴
            int port = 13000;
            TcpListener server = new TcpListener(localAddr, port);
            server.Start(); // 서버 열어줌
            Console.WriteLine("연결을 기다리는 중,,,");
            //2.Listener, 3.Acccept
            using (TcpClient client = server.AcceptTcpClient())
            {
                Console.WriteLine("연결 성공");
                //write  서버---->클라이언트로 메시지 전달  (전달하고 받을지, 받고 전달할지 규칙을 정해야 함)
                using(NetworkStream stream = client.GetStream())
                {
                    string message = "안녕하세요!!";
                    byte[] data = Encoding.UTF8.GetBytes(message);

                    stream.Write(data, 0, data.Length); //네트워크로 클라이언트 메시지 전송
                    Console.WriteLine($"전달한 메시지 : {message}");
                }
            }
            //4.Write
            //5.종료
            server.Stop();
        }
    }
}

------------------------------------------------------------------
//TCP 클라이언트
using System.Net.Sockets;

namespace SimpleTCPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string server = "127.0.0.1";
            int port = 13000;

            //1.서버로 접속할 클라이언트 소켓 만들기
            //성공시 접속됨 Connect
            using (TcpClient client = new TcpClient(server, port))
            {
                //2. 메시지 받기
                NetworkStream stream = client.GetStream();
                byte[] data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                Console.WriteLine($"받은 메시지 : {data}");
            }

                
        }
    }
}

------------------------------------------------------------------
//소켓 서버
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SocketTCPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. ServerSocket 만들기
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = 13000;

            // 서버 소켓 생성
            Socket serverSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp); ;

            // 2. Bind - 내 서버 체계에 localAddr과 Port를 바인딩
            serverSocket.Bind(new IPEndPoint(localAddr, port));

            // 3. Listen
            serverSocket.Listen(1); Console.WriteLine("연결을 기다리는 중...");

            // 4. Accept
            Socket clientSocket = serverSocket.Accept(); Console.WriteLine("연결 성공!");

            // 5. Read/Write
            string message = "안녕하세요, 클라이언트님!";
            byte[] bytes = new byte[1024];
            byte[] data = Encoding.UTF8.GetBytes(message);

            clientSocket.Send(data); Console.WriteLine($"전송한 메세지 : {message}");

            // 6. Close
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            // 서버 소켓 종료
            serverSocket.Close();
        }
    }
}
------------------------------------------------------------------
//소켓 클라이언트
  
------------------------------------------------------------------
//
  
------------------------------------------------------------------
//
  
------------------------------------------------------------------
//
  
------------------------------------------------------------------
//
  
------------------------------------------------------------------
//
