//WinForm EchoServer(교수님코드)
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormEchoServer
{
    public partial class Form1 : Form
    {
        private static TcpListener server = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();
        }//end of Form1_Load
        private void StartServer()
        {
            try
            {
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                int port = 13000;

                server = new TcpListener(serverIP, port);
                server.Start();

                //textBox1.AppendText("Echo Server 시작 ...\n");
                AppendText("Echo Server 시작 ...");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    //textBox1.AppendText("클라이언트가 연결되었습니다.\n");
                    AppendText("클라이언트가 연결되었습니다.");

                    Thread clientThread = new Thread(new ParameterizedThreadStart(ClientAction));
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); 
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }//end finally
        }//end of StartServer
        private void ClientAction(object obj)
        {
            TcpClient client = (TcpClient)obj;
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    //받기
                    byte[] buffer = new byte[2048];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string receiveMsg = Encoding.UTF8.GetString(buffer);
                    //Console.WriteLine("클라이언트에게 받은 메시지 : " + receiveMsg);
                    AppendText("클라이언트에게 받은 메시지 : " + receiveMsg);


                    //보내기
                    byte[] echoMsg = Encoding.UTF8.GetBytes(receiveMsg);
                    stream.Write(echoMsg, 0, echoMsg.Length);
                    //Console.WriteLine("에코메시지 전송 완료");
                    AppendText("에코메시지 전송 완료");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }

        private void AppendText(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + Environment.NewLine);
            }
        }


    }//end of Form
}

-----------------------------------------------------------------------
//WinForm Echo Client 수정코드
위랑 맞는 코드
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinFormEchoClient
{
    public partial class EchoClient : Form
    {
        private TcpClient client;
        private NetworkStream stream;

        public EchoClient()
        {
            InitializeComponent();
        }

        private void EchoClient_Load(object sender, EventArgs e)
        {
            // 클라이언트가 로드될 때 서버와 연결 시도
            try
            {
                client = new TcpClient("127.0.0.1", 13000);
                stream = client.GetStream();
                AppendText("서버에 연결되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 연결 실패: " + ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (client != null && stream != null)
                {
                    // tbxFill에 입력된 텍스트를 서버로 전송
                    string sendMsg = tbxFill.Text;
                    byte[] data = Encoding.UTF8.GetBytes(sendMsg);
                    stream.Write(data, 0, data.Length);

                    // 서버로부터 에코 메시지를 받음
                    byte[] buffer = new byte[2048];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string receiveMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // tbxMsg에 서버로부터 받은 메시지 출력
                    AppendText("서버로부터 받은 메시지: " + receiveMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("메시지 전송 오류: " + ex.Message);
            }
        }

        private void AppendText(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                tbxMsg.AppendText(text + Environment.NewLine);
            }
        }

        private void EchoClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 클라이언트 종료 시 자원 해제
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
        }
    }
}
-----------------------------------------------------------------------
//채팅 프로그램 서버
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ChatProgramServer01
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;
        private Thread listenThread;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listenThread = new Thread(StartServer);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void StartServer()
        {
            try
            {
                // TCP 연결을 수신 대기할 서버 객체를 생성
                server = new TcpListener(IPAddress.Any, 13000);
                server.Start();
                AppendText(tbxOut, "채팅 서버가 시작되었습니다!");

                // AcceptTcpClient()는 클라이언트가 연결될 때까지 실행을 멈추고 대기
                client = server.AcceptTcpClient();
                AppendText(tbxOut, "클라이언트가 연결되었습니다!");

                // 이 스트림을 통해 클라이언트와 서버는 데이터를 주고받을 수 있음
                stream = client.GetStream();

                // 클라이언트의 메시지를 비동기적으로 수신
                Thread receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server error: " + ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[2048];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string receiveMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        AppendText(tbxOut, "클라이언트: " + receiveMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("수신 오류: " + ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
        }

        private void AppendText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                // 새로운 Action 델리게이트를 생성하고, 현재 메서드를 재귀적으로 호출
                textBox.Invoke(new Action<TextBox, string>(AppendText), new object[]
                { textBox, text });
            }
            // 만약 UI 스레드에서 호출되었거나, 이미 UI 스레드인 경우 텍스트를 추가
            else
            {
                textBox.AppendText(text + Environment.NewLine);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stream != null) stream.Close();
            if (client != null) client.Close();
            if (server != null) server.Stop();
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (stream != null && client.Connected)
                {
                    string sendMsg = tbxIn.Text;
                    byte[] data = Encoding.UTF8.GetBytes(sendMsg);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();  // 데이터 전송 후 스트림 플러시

                    AppendText(tbxOut, "서버: " + sendMsg);
                    tbxIn.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("전송 오류: " + ex.Message);
            }
        }
    }
}
-----------------------------------------------------------------------
//채팅 프로그램 클라이언트
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProgramClient01
{
    public partial class ChattingClient : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        public ChattingClient()
        {
            InitializeComponent();
        }

        private void ChattingClient_Load(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 13000);
                stream = client.GetStream();
                AppendText(tbxOut, "서버에 연결되었습니다.");

                // 서버로부터 메시지를 비동기적으로 수신
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 연결 실패: " + ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[2048];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string receiveMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        AppendText(tbxOut, "서버: " + receiveMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("수신 오류: " + ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
        }

        private void AppendText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<TextBox, string>(AppendText), new object[] { textBox, text });
            }
            else
            {
                textBox.AppendText(text + Environment.NewLine);
            }
        }

        private void ChattingClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stream != null) stream.Close();
            if (client != null) client.Close();
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (stream != null && client.Connected)
                {
                    string sendMsg = tbxIn.Text;
                    byte[] data = Encoding.UTF8.GetBytes(sendMsg);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();  // 데이터 전송 후 스트림 플러시

                    AppendText(tbxOut, "클라이언트: " + sendMsg);
                    tbxIn.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("전송 오류: " + ex.Message);
            }
        }
    }
}

