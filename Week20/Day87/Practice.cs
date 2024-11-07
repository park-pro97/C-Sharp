//최종 프로젝트 1차 개인 과제

스레드 사용 방식 (스레드를 통해 데이터를 지속적으로 수신)
타이머 사용 방식 (타이머를 통해 일정 간격마다 데이터를 수신)


//스레드 사용
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace arduino_plc
{
    public partial class Form1 : Form
    {
        private SerialPort arduino; // 아두이노와 시리얼 통신을 위한 SerialPort 객체
        private Thread? receiveThread; // 데이터를 수신할 때 사용할 스레드
        private bool receiveFlag = false; // 데이터를 계속 읽을지 여부를 결정하는 플래그

        public Form1()
        {
            InitializeComponent(); // 폼의 구성 요소 초기화 (버튼, 텍스트박스 등 UI 설정)
            arduino = new SerialPort("COM6", 9600); // 아두이노와 연결된 포트를 지정하고, 통신 속도를 9600으로 설정
            arduino.Encoding = Encoding.UTF8; // 아두이노에서 보내는 데이터를 UTF-8로 읽음
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 버튼 1을 누르면 데이터 수신을 시작하는 코드

            // 텍스트 박스에 "start" 메시지와 현재 시간을 출력
            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] start" + Environment.NewLine);

            if (!arduino.IsOpen) // 아두이노와의 포트가 아직 열려있지 않다면
            {
                arduino.Open(); // 포트를 열어 아두이노와 연결 시작
            }

            receiveFlag = true; // 데이터를 읽어오도록 플래그를 true로 설정

            // 데이터를 수신하는 스레드를 시작
            receiveThread = new Thread(ReceivingData); // ReceivingData라는 메서드를 실행하는 스레드 생성
            receiveThread.Start(); // 스레드 실행 시작
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 버튼 2를 누르면 데이터 수신을 멈추는 코드

            receiveFlag = false; // 데이터 수신을 멈추기 위해 플래그를 false로 설정

            if (receiveThread != null && receiveThread.IsAlive) // 수신 스레드가 존재하고 실행 중이라면
            {
                receiveThread.Join(); // 스레드를 안전하게 종료할 때까지 대기
            }

            if (arduino.IsOpen) // 아두이노 포트가 열려있다면
            {
                arduino.Close(); // 아두이노 포트를 닫아서 연결 종료
            }

            // 텍스트 박스에 "stop" 메시지와 현재 시간을 출력
            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] stop" + Environment.NewLine);
        }

        private void ReceivingData()
        {
            // 데이터를 계속해서 수신하는 메서드

            while (receiveFlag && arduino.IsOpen) // receiveFlag가 true이고, 포트가 열려있는 동안 반복
            {
                try
                {
                    // 아두이노에서 한 줄의 데이터를 읽어서 문자열로 저장
                    string data = arduino.ReadLine().Trim(); // 데이터를 읽고 양끝 공백을 제거
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 현재 날짜와 시간 가져오기

                    // UI에 데이터를 표시하기 위해 UI 스레드에서 실행 (다른 스레드에서 UI를 수정하려면 Invoke 필요)
                    this.Invoke(new Action(() =>
                    {
                        // 텍스트 박스에 현재 시간과 아두이노 데이터를 출력
                        textBox1.AppendText($"[{timestamp}] {data}" + Environment.NewLine);
                    }));

                    Thread.Sleep(1000); // 1초 동안 대기하여 데이터를 천천히 수신
                }
                catch (Exception ex) // 데이터 수신 중에 예외가 발생하면
                {
                    // 예외 메시지를 텍스트 박스에 출력
                    this.Invoke(new Action(() =>
                    {
                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 현재 날짜와 시간 가져오기
                        textBox1.AppendText($"[{timestamp}] 오류: {ex.Message}" + Environment.NewLine); // 오류 메시지 출력
                    }));
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 텍스트 박스 내용이 변경될 때 호출되는 메서드 (현재는 사용되지 않음)
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

-------------------------------------------------------------------------------------
//타이머 사용
using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace arduino_plc
{
    public partial class Form1 : Form
    {
        private SerialPort arduino; // 아두이노와 시리얼 통신을 위한 SerialPort 객체
        private System.Windows.Forms.Timer updateTimer; // WinForms 타이머 객체
        private bool receiveFlag = false; // 데이터를 계속 읽을지 여부를 결정하는 플래그

        public Form1()
        {
            InitializeComponent(); // 폼의 구성 요소 초기화 (버튼, 텍스트박스 등 UI 설정)
            arduino = new SerialPort("COM6", 9600); // 아두이노와 연결된 포트를 지정하고, 통신 속도를 9600으로 설정
            arduino.Encoding = Encoding.UTF8; // 아두이노에서 보내는 데이터를 UTF-8로 읽음

            // 타이머 초기화
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000; // 1초(1000ms)마다 실행되도록 설정
            updateTimer.Tick += UpdateTimer_Tick; // 타이머의 Tick 이벤트에 메서드 연결
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 버튼 1을 누르면 데이터 수신을 시작하는 코드

            // 텍스트 박스에 "start" 메시지와 현재 시간을 출력
            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] start" + Environment.NewLine);

            if (!arduino.IsOpen) // 아두이노와의 포트가 아직 열려있지 않다면
            {
                arduino.Open(); // 포트를 열어 아두이노와 연결 시작
            }

            receiveFlag = true; // 데이터를 읽어오도록 플래그를 true로 설정
            updateTimer.Start(); // 타이머 시작 (1초마다 값을 읽어오도록 설정)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 버튼 2를 누르면 데이터 수신을 멈추는 코드

            receiveFlag = false; // 데이터 수신을 멈추기 위해 플래그를 false로 설정

            if (arduino.IsOpen) // 아두이노 포트가 열려있다면
            {
                arduino.Close(); // 아두이노 포트를 닫아서 연결 종료
            }

            // 텍스트 박스에 "stop" 메시지와 현재 시간을 출력
            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] stop" + Environment.NewLine);

            updateTimer.Stop(); // 타이머 중지
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // 타이머의 Tick 이벤트가 1초마다 호출되며 데이터를 읽어옴
            if (receiveFlag && arduino.IsOpen) // receiveFlag가 true이고 포트가 열려있는 동안
            {
                try
                {
                    // 아두이노에서 한 줄의 데이터를 읽어서 문자열로 저장
                    string data = arduino.ReadLine().Trim(); // 데이터를 읽고 양끝 공백을 제거
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 현재 날짜와 시간 가져오기

                    // UI에 데이터를 표시하기 위해 UI 스레드에서 실행 (다른 스레드에서 UI를 수정하려면 Invoke 필요)
                    textBox1.AppendText($"[{timestamp}] {data}" + Environment.NewLine);
                }
                catch (Exception ex) // 데이터 수신 중에 예외가 발생하면
                {
                    // 예외 메시지를 텍스트 박스에 출력
                    textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 오류: {ex.Message}" + Environment.NewLine);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 텍스트 박스 내용이 변경될 때 호출되는 메서드 (현재는 사용되지 않음)
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 폼이 로드될 때 호출되는 메서드 (현재는 사용되지 않음)
        }
    }
}
-----------------------------------------------------------
//스레드 방식으로 읽어오고 텍스트 박스에 출력을 1초 타이머로
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace arduino_plc
{
    public partial class Form1 : Form
    {
        private SerialPort arduino; // 아두이노와 시리얼 통신을 위한 SerialPort 객체
        private Thread? receiveThread; // 데이터를 수신할 때 사용할 스레드
        private bool receiveFlag = false; // 데이터를 계속 읽을지 여부를 결정하는 플래그
        private System.Windows.Forms.Timer updateTimer; // WinForms 타이머 객체
        private string latestData = ""; // 최신 데이터 저장용 변수

        public Form1()
        {
            InitializeComponent(); // 폼의 구성 요소 초기화 (버튼, 텍스트박스 등 UI 설정)
            arduino = new SerialPort("COM6", 9600); // 아두이노와 연결된 포트를 지정하고, 통신 속도를 9600으로 설정
            arduino.Encoding = Encoding.UTF8; // 아두이노에서 보내는 데이터를 UTF-8로 읽음

            // 타이머 초기화
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000; // 1초(1000ms)마다 실행되도록 설정
            updateTimer.Tick += UpdateTimer_Tick; // 타이머의 Tick 이벤트에 메서드 연결
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 시작 버튼을 눌렀을 때 실행

            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] start" + Environment.NewLine);

            if (!arduino.IsOpen) // 아두이노와의 포트가 아직 열려있지 않다면
            {
                arduino.Open(); // 포트를 열어 아두이노와 연결 시작
            }

            receiveFlag = true; // 데이터를 읽어오도록 플래그를 true로 설정

            // 데이터를 수신하는 스레드를 시작
            receiveThread = new Thread(ReceivingData); // ReceivingData 메서드를 실행하는 스레드 생성
            receiveThread.Start(); // 스레드 실행 시작

            // 타이머 시작 (UI에 1초마다 데이터를 출력)
            updateTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 중지 버튼을 눌렀을 때 실행

            receiveFlag = false; // 데이터 수신을 멈추기 위해 플래그를 false로 설정

            if (receiveThread != null && receiveThread.IsAlive) // 수신 스레드가 존재하고 실행 중이라면
            {
                receiveThread.Join(); // 스레드를 안전하게 종료할 때까지 대기
            }

            if (arduino.IsOpen) // 아두이노 포트가 열려있다면
            {
                arduino.Close(); // 아두이노 포트를 닫아서 연결 종료
            }

            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] stop" + Environment.NewLine);
            updateTimer.Stop(); // 타이머 중지
        }

        private void ReceivingData()
        {
            // 데이터를 지속적으로 수신하는 메서드 (스레드에서 실행)

            while (receiveFlag && arduino.IsOpen) // receiveFlag가 true이고 포트가 열려있는 동안 반복
            {
                try
                {
                    // 아두이노에서 한 줄의 데이터를 읽어서 latestData 변수에 저장
                    latestData = arduino.ReadLine().Trim(); // 데이터를 읽고 양끝 공백을 제거
                    Thread.Sleep(100); // 0.1초마다 데이터를 수신하여 최신 데이터 유지
                }
                catch (Exception ex) // 데이터 수신 중에 예외가 발생하면
                {
                    latestData = $"오류: {ex.Message}";
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // 타이머를 통해 1초마다 텍스트박스에 최신 데이터를 출력
            if (!string.IsNullOrEmpty(latestData)) // latestData에 값이 있을 경우에만 출력
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                textBox1.AppendText($"[{timestamp}] {latestData}" + Environment.NewLine);
            }
        }
    }
}
-----------------------------------------------------------
//아두이노 DHT-11 온습도 센서 윈폼과 Serial 통신
#include <DHT.h>

#define DHTPIN 2      // DHT-11 센서가 연결된 핀 번호
#define DHTTYPE DHT11 // DHT-11 센서 유형을 지정

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  dht.begin();
  Serial.println("DHT-11 센서 데이터 출력 시작");
}

void loop() {
  delay(1000); // 1초 간격으로 데이터 업데이트

  float humidity = dht.readHumidity();    // 습도 측정
  float temperature = dht.readTemperature(); // 섭씨 온도 측정

  // 측정 실패 시 오류 메시지 출력
  if (isnan(humidity) || isnan(temperature)) {
    Serial.println("DHT-11 센서에서 데이터를 읽을 수 없습니다.");
    return;
  }

  Serial.print("온도: ");
  Serial.print(temperature);
  Serial.print("°C ");
  Serial.print("습도: ");
  Serial.print(humidity);
  Serial.println("%");
}
-----------------------------------------------------------
최종 아두이노
#include <DHT.h>

#define DHTPIN 2      // DHT-11 센서가 연결된 핀 번호
#define DHTTYPE DHT11 // DHT-11 센서 유형을 지정

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  dht.begin();
  Serial.println("DHT-11 센서 데이터 출력 시작");
}

void loop() {
  delay(1000); // 1초 간격으로 데이터 업데이트

  float humidity = dht.readHumidity();    // 습도 측정
  float temperature = dht.readTemperature(); // 섭씨 온도 측정

  // 측정 실패 시 오류 메시지 출력
  if (isnan(humidity) || isnan(temperature)) {
    Serial.println("DHT-11 센서에서 데이터를 읽을 수 없습니다.");
    return;
  }

  Serial.print(temperature);
  Serial.print(",");
  Serial.println(humidity);
}
-----------------------------------------------------------
//최종 C#
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace arduino_plc2
{
    public partial class Form1 : Form
    {
        private SerialPort arduino; // 아두이노와 시리얼 통신을 위한 SerialPort 객체
        private Thread? receiveThread; // 데이터를 수신할 때 사용할 스레드
        private bool receiveFlag = false; // 데이터를 계속 읽을지 여부를 결정하는 플래그
        private System.Windows.Forms.Timer updateTimer; // WinForms 타이머 객체
        private string latestData = ""; // 최신 데이터 저장용 변수

        public Form1()
        {
            InitializeComponent(); // 폼의 구성 요소 초기화
            arduino = new SerialPort("COM6", 9600); // 아두이노와 연결된 포트를 지정하고, 통신 속도를 9600으로 설정
            arduino.Encoding = Encoding.UTF8; // 아두이노에서 보내는 데이터를 UTF-8로 읽음

            // 타이머 초기화
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000; // 1초마다 실행되도록 설정
            updateTimer.Tick += UpdateTimer_Tick; // 타이머의 Tick 이벤트에 메서드 연결
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] start" + Environment.NewLine);
            textBox2.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] start" + Environment.NewLine);

            if (!arduino.IsOpen) // 아두이노와의 포트가 아직 열려있지 않다면
            {
                arduino.Open(); // 포트를 열어 아두이노와 연결 시작
            }

            receiveFlag = true; // 데이터를 읽어오도록 플래그를 true로 설정

            receiveThread = new Thread(ReceivingData);
            receiveThread.Start();

            updateTimer.Start(); // 타이머 시작
        }

        private void button2_Click(object sender, EventArgs e)
        {
            receiveFlag = false; // 데이터 수신을 멈추기 위해 플래그를 false로 설정

            if (receiveThread != null && receiveThread.IsAlive)
            {
                receiveThread.Join();
            }

            if (arduino.IsOpen)
            {
                arduino.Close();
            }

            textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] stop" + Environment.NewLine);
            textBox2.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] stop" + Environment.NewLine);
            updateTimer.Stop(); // 타이머 중지
        }

        private void ReceivingData()
        {
            while (receiveFlag && arduino.IsOpen)
            {
                try
                {
                    latestData = arduino.ReadLine().Trim(); // 데이터를 읽고 양끝 공백을 제거
                    Thread.Sleep(100); // 데이터 수신 주기
                }
                catch (Exception ex)
                {
                    latestData = $"오류: {ex.Message}";
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(latestData))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string[] dataParts = latestData.Split(',');
                if (dataParts.Length == 2)
                {
                    textBox1.AppendText($"[{timestamp}] 온도: {dataParts[0]}°C" + Environment.NewLine);
                    textBox2.AppendText($"[{timestamp}] 습도: {dataParts[1]}%" + Environment.NewLine);
                }
                else
                {
                    textBox1.AppendText($"[{timestamp}] 데이터 오류: 형식이 올바르지 않습니다." + Environment.NewLine);
                    textBox2.AppendText($"[{timestamp}] 데이터 오류: 형식이 올바르지 않습니다." + Environment.NewLine);
                }
            }
        }
    }
}
