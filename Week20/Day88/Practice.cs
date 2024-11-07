//최종 프로젝트 2일차___1차 과제
나는 MES측을 맡아서 CIM에서 오는 데이터를 확인하고 3가지 정보를 추출하여 각각 다른 박스에 출력하여 이상이 없는 것을 확인하고
START REPORT를 CIM으로 다시 내려주는 파트를 맡아서 진행하게 됐다.

우선 소켓 통신하는 베이스 코드
------------------------------------------------------------------------
//소켓통신 서버
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleSocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 인터넷 주소 설정
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            // 포트 설정
            int port = 13000;

            // 서버 소켓 생성
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 로컬 주소 및 포트에 바인드
            serverSocket.Bind(new IPEndPoint(localAddr, port));

            // 연결 요청 대기 시작
            serverSocket.Listen(1);
            Console.WriteLine("연결을 기다리는 중...");

            // 클라이언트 연결 수락
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("연결 성공!");

            byte[] bytes = new byte[1024];
            string response = "안녕하세요, 클라이언트님!";
            byte[] data = Encoding.UTF8.GetBytes(response);

            // 메시지 전송
            clientSocket.Send(data);
            Console.WriteLine($"전송한 메시지: {response}");

            // 클라이언트 연결 종료
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

            // 서버 소켓 종료
            serverSocket.Close();
        }
    }
}
-------------------------------------------------
//소켓통신 클라이언트
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
 
namespace ConsoleSocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 서버의 인터넷 주소 및 포트 설정
            IPAddress serverAddr = IPAddress.Parse("127.0.0.1");
            int port = 13000;

            // 클라이언트 소켓 생성
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 서버에 연결
            clientSocket.Connect(new IPEndPoint(serverAddr, port));
            Console.WriteLine("서버에 연결되었습니다.");

            byte[] bytes = new byte[1024];
            int bytesReceived = clientSocket.Receive(bytes);

            // 메시지 수신
            string response = Encoding.UTF8.GetString(bytes, 0, bytesReceived);
            Console.WriteLine($"서버로부터 받은 메시지: {response}");

            // 클라이언트 소켓 종료
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}
