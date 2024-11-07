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

## MES파트를 맡았는데 통신하는 걸 확인하기 위해서 CIM측 서버를 같이 만들었음
   서버를 열고 MES측에서 START를 보내는 것까지 구현.
   추후에는 문자열에서 각 데이터를 추출하여 다른 디스플레이에 출력하게 하는 작업을 진행해야 할 듯.
    
---------------------------------------------------------------
//CIM의 서버
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HW1107_2
{
    public partial class Form1 : Form
    {
        private TcpListener server;

        public Form1()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            try
            {
                // 서버 설정: 로컬 IP와 포트 지정
                IPAddress localAddr = IPAddress.Parse("192.168.0.60");
                int port = 13000;

                server = new TcpListener(localAddr, port);
                server.Start();

                textBox1.AppendText("[INFO] CIM 서버가 시작되었습니다. 클라이언트 연결을 기다리는 중...\r\n");

                Thread serverThread = new Thread(() =>
                {
                    while (true)
                    {
                        // 클라이언트 연결 수락
                        TcpClient client = server.AcceptTcpClient();
                        this.Invoke(new Action(() =>
                        {
                            textBox1.AppendText("[INFO] 클라이언트가 연결되었습니다.\r\n");
                        }));

                        // 데이터 수신 및 전송을 위한 네트워크 스트림 설정
                        NetworkStream stream = client.GetStream();

                        Thread sendThread = new Thread(() =>
                        {
                            try
                            {
                                while (client.Connected)
                                {
                                    // 클라이언트에게 지속적으로 데이터 전송 (예: "10701/ModelID/OPID/ProcID/MaterialID")
                                    string message = "10701/ModelID/OPID/ProcID/MaterialID";
                                    byte[] dataToSend = Encoding.UTF8.GetBytes(message);
                                    stream.Write(dataToSend, 0, dataToSend.Length);
                                    this.Invoke(new Action(() =>
                                    {
                                        textBox1.AppendText($"[INFO] 전송된 메시지: {message}\r\n");
                                    }));
                                    Thread.Sleep(1000); // 1초 간격으로 전송
                                }
                            }
                            catch (Exception ex)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    textBox1.AppendText($"[ERROR] 데이터 전송 중 오류 발생: {ex.Message}\r\n");
                                }));
                            }
                        });

                        sendThread.IsBackground = true;
                        sendThread.Start();
                    }
                });

                serverThread.IsBackground = true;
                serverThread.Start();
            }
            catch (Exception ex)
            {
                textBox1.AppendText($"[ERROR] 서버 시작 중 오류 발생: {ex.Message}\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 버튼 클릭 시, 서버에서 데이터 전송을 처리
            textBox1.AppendText("[INFO] 서버가 이미 실행 중입니다. 클라이언트를 기다리는 중...\r\n");
        }
    }
}
---------------------------------------------------------------
//MES의 클라이언트
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HW1107
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        private bool isReceiving = false;
        private System.Windows.Forms.Timer updateTimer; // Timer to display data at 1-second intervals
        private string latestData = ""; // Variable to store the latest data

        public Form1()
        {
            InitializeComponent();

            // Initialize timer
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000; // 1-second interval
            updateTimer.Tick += UpdateTimer_Tick;
        }

        private void ConnectToCIM()
        {
            try
            {
                client = new TcpClient("192.168.0.60", 13000); // CIM IP address and port
                stream = client.GetStream();
                isReceiving = true;
                receiveThread = new Thread(ReceiveData);
                receiveThread.Start();
                textBox1.AppendText("[INFO] CIM에 연결되었습니다.\r\n");
                updateTimer.Start(); // Start the timer to display data
            }
            catch (Exception ex)
            {
                textBox1.AppendText($"[ERROR] CIM에 연결할 수 없습니다: {ex.Message}\r\n");
            }
        }

        private void ReceiveData()
        {
            try
            {
                while (isReceiving)
                {
                    if (stream.CanRead)
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        latestData = receivedData; // Store the received data
                    }
                    Thread.Sleep(100); // Delay for data reception
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    textBox1.AppendText($"[ERROR] 데이터 수신 중 오류 발생: {ex.Message}\r\n");
                }));
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(latestData))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                textBox1.AppendText($"[{timestamp}] 받은 데이터: {latestData}\r\n");
                latestData = ""; // Clear the data after displaying to prevent repeated display
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToCIM();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (stream != null && stream.CanWrite)
                {
                    string message = "STARTED REPORT";
                    byte[] dataToSend = Encoding.UTF8.GetBytes(message);
                    stream.Write(dataToSend, 0, dataToSend.Length);
                    textBox1.AppendText("[INFO] STARTED REPORT 전송 완료\r\n");
                }
                else
                {
                    textBox1.AppendText("[ERROR] CIM에 연결되지 않았습니다.\r\n");
                }
            }
            catch (Exception ex)
            {
                textBox1.AppendText($"[ERROR] 전송 중 오류 발생: {ex.Message}\r\n");
            }
        }
    }
}
-----------------------------------------------------------------------------------
//
