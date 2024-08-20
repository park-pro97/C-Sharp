//SimpleTCPServer
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SimpleTCPServer1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 서버 소켓 생성
            TcpListener server = null;
            try
            {
                int port = 8888;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // 서버 소켓 초기화
                server = new TcpListener(localAddr, port);

                // 서버 시작
                server.Start();
                Console.WriteLine("서버가 시작되었습니다. 클라이언트를 기다리는 중...");

                // 클라이언트의 연결을 기다림
                using (TcpClient client = server.AcceptTcpClient())
                {
                    Console.WriteLine("클라이언트가 연결되었습니다.");

                    // 네트워크 스트림을 통해 데이터를 주고받음
                    using (NetworkStream stream = client.GetStream())
                    {
                        // 클라이언트로부터 데이터를 읽음
                        byte[] buffer = new byte[256];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("클라이언트로부터 받은 메시지: " + receivedMessage);

                        // 클라이언트에게 메시지 전송
                        string responseMessage = "메시지를 받았습니다.";
                        byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                        stream.Write(responseData, 0, responseData.Length);
                        Console.WriteLine("클라이언트에게 응답을 전송했습니다.");
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("소켓 예외: " + e.ToString());
            }
            finally
            {
                // 서버 소켓을 종료
                if (server != null)
                {
                    server.Stop();
                }
            }

            Console.WriteLine("서버를 종료합니다.");
        }
    }
-----------------------------------------------------------------------------------
//SimpleTCPClient
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTCPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string server = "127.0.0.1";
            int port = 13000;

            TcpClient client = new TcpClient(server, port);

            NetworkStream stream = client.GetStream();

            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string responseData = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine($"Received: {responseData}");

            client.Close();
        }
    }
}
-----------------------------------------------------------------------------------
//위 클라이언트를 통신 가능하게 수정한 것
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
  
namespace SimpleTCPClient1
{
        internal class Program
        {
            static void Main(string[] args)
            {
                string server = "127.0.0.1";
                int port = 13000;
              
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                string sendMessage = "푸바오";
                byte[] sendeData = Encoding.UTF8.GetBytes(sendMessage);
                stream.Write(sendeData, 0, sendeData.Length);
                Console.WriteLine($"서버에게 {sendMessage}을 전송했습니다.");

              
                byte[] data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string responseData = Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine($"Received: {responseData}");
            }
        }
    }
-----------------------------------------------------------------------------------
//소켓 방식으로 EchoServer
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SocketEchoServer
{
    internal class Program
    {
        static int cnt = 0;
        static void Main(string[] args)
        {
            Thread serverThread = new Thread(ServerAction);
            serverThread.IsBackground = true;

            serverThread.Start(); serverThread.Join();
            Console.WriteLine("Echo Server 메인프로그램 종료");
        }
        private static void ServerAction(object obj)
        {
            using (Socket srvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 13000);

                srvSocket.Bind(endPoint);
                srvSocket.Listen(50);

                Console.WriteLine("메아리 서버(Echo Server) 시작...");
                while (true)
                {
                    Socket cliSocket = srvSocket.Accept(); //여기까지 동작은 공통입니다.
                    cnt++; //클라이언트를 구분하기 위한 카운트 증가

                    //Read,Write 기능은 따로 스레드를 만들어 
                    Thread workThread = new Thread(new ParameterizedThreadStart(workAction));
                    workThread.IsBackground = true;
                    workThread.Start(cliSocket);
                }
            }
        } // end of serveraction
        private static void workAction(object obj)
        {
            byte[] recvBytes = new byte[1024];
            Socket cliSocket = (Socket)obj;
            int nRecv = cliSocket.Receive(recvBytes);

            string echotxt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
            Console.WriteLine($"클라이언트로부터 받은 번호와 메세지 ({cnt}) : {echotxt}");
            byte[] echoMessage = Encoding.UTF8.GetBytes("Echo : " + echotxt);
            cliSocket.Send(echoMessage);
            cliSocket.Close();
        } // end of workaction
    }
}
-----------------------------------------------------------------------------------
//소켓 방식으로 EchoClient
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SocketEchoClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread clientThread = new Thread(clientFunc);
            clientThread.Start();
            clientThread.IsBackground = true;
            clientThread.Join();

            Console.WriteLine("Echo Server가 종료 되었습니다.");
        }
        static void clientFunc(object obj)
        {
            try
            {
                // 1. 소켓 만들기
                Socket socket = new Socket(AddressFamily.InterNetwork,
                                           SocketType.Stream,
                                           ProtocolType.Tcp);

                // 2. 서버에 연결
                EndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13000);
                socket.Connect(serverEP);

                Console.WriteLine("Echo Server에 연결되었습니다. 종료하려면 'exit'를 입력하세요.");

                while (true)
                {
                    // 3. 메시지 입력 및 전송
                    Console.Write("메세지 작성: ");
                    string userInput = Console.ReadLine(); // 사용자 입력 받기

                    // 'exit' 입력 시 프로그램 종료
                    if (userInput.ToLower() == "exit")
                    {
                        break;
                    }

                    byte[] echoBuffer = Encoding.UTF8.GetBytes(userInput);
                    socket.Send(echoBuffer);

                    // 4. 서버로부터 응답 수신
                    byte[] recvBytes = new byte[1024];
                    int nRecv = socket.Receive(recvBytes);

                    string echotxt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                    Console.WriteLine($"{echotxt}");
                }

                // 5. 소켓 종료
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
-----------------------------------------------------------------------------------
//퀴즈 프로그램 서버 --- [ 조별과제 ]
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace QuizServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();

            serverThread.Join();
            Console.WriteLine("퀴즈 서버 종료");
        }

        private static void StartServer()
        {
            using (Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 13000));
                serverSocket.Listen(10);

                Console.WriteLine("퀴즈 서버가 시작되었습니다...");

                while (true)
                {
                    Socket clientSocket = serverSocket.Accept();
                    Thread clientThread = new Thread(HandleClient);
                    clientThread.IsBackground = true;
                    clientThread.Start(clientSocket);
                }
            }
        }

        private static void HandleClient(object clientObj)
        {
            Socket clientSocket = (Socket)clientObj;

            string[] questions = {
                "C#의 창시자는?\n1. Anders Hejlsberg\n2. James Gosling\n3. Bjarne Stroustrup",
                "HTTP의 기본 포트 번호는?\n1. 21\n2. 80\n3. 443",
                "OOP에서 상속을 제공하는 키워드는?\n1. class\n2. interface\n3. extends"
            };
            int[] correctAnswers = { 1, 2, 3 };
            int score = 0;

            try
            {
                for (int i = 0; i < questions.Length; i++)
                {
                    SendMessage(clientSocket, $"문제 {i + 1}: {questions[i]}\n정답을 입력하세요 (1, 2, 3): ");

                    string clientAnswer = ReceiveMessage(clientSocket).Trim();

                    if (int.TryParse(clientAnswer, out int answer) && answer == correctAnswers[i])
                    {
                        SendMessage(clientSocket, "정답입니다!\n");
                        score++;
                    }
                    else
                    {
                        SendMessage(clientSocket, "오답입니다. 다음 기회에 도전하세요.\n");
                        break;
                    }
                }

                if (score == questions.Length)
                {
                    SendMessage(clientSocket, "축하합니다! 모든 문제를 맞추셨습니다. 우승하셨습니다!\n종료합니다.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"오류 발생: {e.Message}");
            }
            finally
            {
                clientSocket.Close();
            }
        }

        private static void SendMessage(Socket socket, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            socket.Send(data);
        }

        private static string ReceiveMessage(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int receivedBytes = socket.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, receivedBytes);
        }
    }
}
  
-----------------------------------------------------------------------------------
//퀴즈 프로그램 클라이언트 --- [ 조별과제 ]
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace QuizClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string serverIp = "192.168.0.4";
            int serverPort = 13000;

            try
            {
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    clientSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIp), serverPort));
                    Console.WriteLine("서버에 연결되었습니다.");

                    while (true)
                    {
                        // 질문 수신
                        string question = ReceiveMessage(clientSocket);
                        Console.WriteLine(question);

                        // "종료합니다."라는 메시지가 포함되어 있다면 게임 종료
                        if (question.Contains("종료합니다.")) break;

                        // 정답 입력 받기
                        string answer = Console.ReadLine();
                        SendMessage(clientSocket, answer);

                        // 결과 수신 및 출력
                        string result = ReceiveMessage(clientSocket);
                        Console.WriteLine(result);

                        // 오답이거나 우승 메시지가 나오면 종료
                        if (result.Contains("오답") || result.Contains("우승"))
                        {
                            break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"소켓 예외: {e.Message}");
                Console.WriteLine();
            }
            finally
            {
                Console.WriteLine("클라이언트를 종료합니다.");
            }
        }

        private static void SendMessage(Socket socket, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            socket.Send(data);
        }

        private static string ReceiveMessage(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int receivedBytes = socket.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, receivedBytes);
        }
    }
}
  
