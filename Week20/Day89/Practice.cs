최종 프로젝트 멘토일 2일차

//멘토님과 함께 수정한 MES 베이스 코드
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
        private NetworkStream stream;
        private Socket clientSocket;

        private Thread receiveThread;

        private Thread MainThread;
        private bool bMainThreadChk = false;

        private string ip = "192.168.0.67";
        private int port = 13000;

        private bool isReceiving = false;
        private System.Windows.Forms.Timer updateTimer;// Timer to display data at 1-second intervals


        private string[] spliteRecvData = new string[] { };

        private string receivedLine = "";
        public Form1()
        {
            InitializeComponent();

            // Initialize timer
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000; // 1-second interval
            updateTimer.Tick += UpdateTimer_Tick;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainThread = new Thread(MainSequenceThread);
            bMainThreadChk = true;
            MainThread.Start();

            isReceiving = true;
            receiveThread = new Thread(ReceiveData);
            receiveThread.Start();

            updateTimer.Start();
        }


        private void ReceiveData()
        {//수신만 하는 쓰레드
            while (isReceiving)
            {
                try
                {
                    if (clientSocket != null && clientSocket.Connected)
                    {//장비 PC 와 연결이 완료되면.
                        byte[] buffer = new byte[1024];

                        int bytesRead = clientSocket.Receive(buffer);
                        //장비 PC 로부터 데이터 수신.
                        receivedLine = Encoding.Default.GetString(buffer, 0, bytesRead).Trim();

                    }
                    else
                    {
                        // 클라이언트 소켓이 존재하고 연결된 상태라면 종료
                        if (clientSocket != null)
                        {
                            try
                            {
                                if (clientSocket.Connected)
                                {
                                    clientSocket.Shutdown(SocketShutdown.Both); // 정상적으로 연결 종료 요청
                                }
                            }
                            catch (SocketException ex)
                            {
                                // 이미 종료된 소켓이라면 예외 무시
                                Console.WriteLine("SocketException during shutdown: " + ex.Message);
                            }
                            finally
                            {
                                clientSocket.Close();
                                clientSocket = null; // 소켓을 완전히 정리하여 새로 연결할 때 새로운 인스턴스 사용
                            }
                        }
                        Thread.Sleep(500);
                        // 새 클라이언트 소켓 생성 및 PLC 서버에 연결
                        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                        clientSocket.Connect(endPoint); // PLC 서버에 연결 시도
                    }
                }
                catch (SocketException ex)
                {
                    //MessageBox.Show("reconnecting...  " + ex.Message);
                }
                Thread.Sleep(100);
            }
        }

        private void MainSequenceThread()
        {
            string preReceivedLine = "";
            while (bMainThreadChk)
            {
                try
                {
                    if (receivedLine != preReceivedLine && receivedLine.Trim() != "")
                    {
                        spliteRecvData = Function.SpliteRecvData(receivedLine);

                        if (spliteRecvData[0] == "10701") //RCMD START or CANCEL
                        {
                            // 착공 시나리오 시퀀스
                            RCMD.RCMD_START.MODELID = spliteRecvData[1];
                            RCMD.RCMD_START.PROCID = spliteRecvData[3];
                            RCMD.RCMD_START.MaterialID = spliteRecvData[4];
                            RCMD.RCMD_START.LOTID = DateTime.Now.ToString("mmdd");

                            this.Invoke(new Action(() =>
                            {
                                textBox1.AppendText($"Received : {receivedLine}\r\n");
                            }));

                            //수신한 데이터 정보에 따라  START, CANCEL 중 하나를 보낸다.
                            string sendmsg;

                            if (true) //바코드 규칙이 맞으면
                            {
                                sendmsg = "_START/" + RCMD.RCMD_START.MODELID + "/" + RCMD.RCMD_START.MaterialID
                                    + "/" + RCMD.RCMD_START.PROCID + "/" + RCMD.RCMD_START.LOTID;
                            }
                            else //바코드 규칙이 안맞으면
                            {
                                sendmsg = "_CANCEL" + RCMD.RCMD_START.MODELID + "/" + RCMD.RCMD_START.MaterialID
                                    + "/" + RCMD.RCMD_START.PROCID + "/" + RCMD.RCMD_START.LOTID;
                            }


                            byte[] msg = Encoding.ASCII.GetBytes(sendmsg);
                            clientSocket.Send(msg);
                        }

                    }
                    preReceivedLine = receivedLine;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    MessageBox.Show("에러 발생");
                }
                Thread.Sleep(100);
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {

        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
