//Accept, Cancel 명령 추가했는데 테스트 해봐야함
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using static HW1107.ReportStructs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Microsoft.VisualBasic;

namespace HW1107
{
    public partial class Form1 : Form
    {
        private SqlConnection dbConnection;
        private string connectionString = "Server=DESKTOP-SBKK1SS\\SQLEXPRESS;Database=mes;Integrated Security=True;";

        private Socket clientSocket;
        private Thread receiveThread;
        private Thread MainThread;
        private bool isReceiving = false;
        private bool bMainThreadChk = false;
        private bool isQueryMode = false;
        private System.Windows.Forms.Timer updateTimer;
        private string logFilePath;

        private Queue<string> recvReportIDqueue = new Queue<string>();
        string textBoxText = string.Empty;

        public Form1()
        {
            InitializeComponent();
            InitializeSocket();
            InitializeTimer();
        }

        // 1. 초기화 메서드
        private void InitializeSocket()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.67"), 13000);
                clientSocket.Connect(endPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 소켓 연결 실패");
            }
        }

        private void InitializeTimer()
        {
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000; // 1초마다 갱신
            updateTimer.Tick += (s, e) => RefreshUnifiedReport();
            updateTimer.Start();

            timer1.Start();
        }
        private void RefreshUnifiedReport()
        {
            if (isQueryMode) return; // 조회 모드일 경우 실시간 갱신 중단

            try
            {
                string query = "SELECT * FROM CompleteReport";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, dbConnection);
                System.Data.DataTable dataTable = new System.Data.DataTable();
                dataAdapter.Fill(dataTable);

                // DataGridView 업데이트
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                updateErrtext("실시간 갱신 실패: {ex.Message}\r\n");
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (!isQueryMode)
            {
                try
                {
                    string query = "SELECT * FROM CompleteReport";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, dbConnection);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        // 2. 폼 로드 및 종료
        private void Form1_Load(object sender, EventArgs e)
        {
            // 로그 파일을 저장할 디렉토리 경로
            string logDirectory = @"C:\Users\Admin\source\repos\HW1107\HW1107\Log";

            // 디렉토리가 없으면 생성
            if (!System.IO.Directory.Exists(logDirectory))
            {
                System.IO.Directory.CreateDirectory(logDirectory);
            }

            // 새로운 로그 파일 경로 생성 (현재 날짜 및 시간을 기반으로 파일명 생성)
            logFilePath = System.IO.Path.Combine(logDirectory, $"Log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            // 로그 파일에 초기 내용 기록
            System.IO.File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 로그 시작\r\n", Encoding.UTF8);

            try
            {
                dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();

                bMainThreadChk = true;
                MainThread = new Thread(MainSequenceThread);
                MainThread.Start();

                isReceiving = true;
                receiveThread = new Thread(ReceiveData);
                receiveThread.Start();

                updateTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] DB 연결 실패: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLogToFile();
            if (dbConnection != null && dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnection.Close();
            }

            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }

            if (receiveThread != null && receiveThread.IsAlive)
            {
                isReceiving = false;
                receiveThread.Join();
                receiveThread = null;
            }

            if (MainThread != null && MainThread.IsAlive)
            {
                bMainThreadChk = false;
                MainThread.Join();
                MainThread = null;
            }

        }

        private void ReconnectSocket()
        {
            if (clientSocket != null)
            {
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                    }
                }
                catch (SocketException) { }
                finally
                {
                    clientSocket.Close();
                    clientSocket = null;
                }
            }

            Thread.Sleep(1000);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.67"), 13000));
        }

        // 3. 데이터 수신 및 처리
        private void ReceiveData()
        {
            while (isReceiving)
            {
                try
                {
                    if (clientSocket != null && clientSocket.Connected)
                    {
                        byte[] buffer = new byte[1024]; // 메시지 버퍼
                        int bytesRead = clientSocket.Receive(buffer); // 데이터 수신
                        string data = Encoding.Default.GetString(buffer, 0, bytesRead).Trim(); // 문자열 변환

                        // _10104로 시작하는 데이터 처리
                        if (data.StartsWith("_10104"))
                        {
                            string output = $"{data}\nOnline"; // 받은 데이터 + "Online" 추가
                            updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {output}\r\n");

                            // textBoxLog에 출력
                            this.Invoke(new Action(() =>
                            {
                                textBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {output}\r\n");
                            }));
                        }
                        else
                        {
                            // 기존 데이터 처리
                            ProcessReceivedData(data);
                        }
                    }
                    else
                    {
                        ReconnectSocket(); // 연결이 끊어진 경우 재연결 시도
                    }
                }
                catch (Exception ex)
                {
                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 소켓 오류 발생\r\n");
                }
                Thread.Sleep(100);
            }
        }

        private void updateErrtext(string msg)
        {
            try
            {
                // 로그 파일에 메시지 추가
                System.IO.File.AppendAllText(logFilePath, $"{msg}\r\n", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // 로그 기록 실패 시 예외 처리
                MessageBox.Show($"로그 기록 실패: {ex.Message}");
            }

            // 기존 TextBoxLog에 메시지 추가
            textBoxLog.AppendText(msg);
        }


        private void ProcessReceivedData(string data)
        {
            updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 수신된 데이터: {data}\r\n");

            // 데이터를 파싱하여 필요한 정보를 추출
            var reportData = ParsingRecvData(data);
            InsertToDatabase(reportData);
            
        }

        // 받은 데이터를 구조체로 변환하는 메서드
        private object TransformData(string[] parts)
        {
            int reportID = Convert.ToInt32(parts[0].Substring(1, parts[0].Length - 1)); // 첫 번째 부분에서 ReportID를 추출
            int index = 1; // 배열의 나머지 부분을 처리하기 위해 인덱스 초기화

            switch (reportID)
            {
                case ReportIDList.IDReport:
                    return new IDReport
                    {
                        Datetime = DateTime.Now,
                        reportID = parts[index++],
                        reportName = parts[index++],
                        ModelID = parts[index++],
                        OPID = parts[index++],
                        ProcID = parts[index++],
                        MaterialID = parts[index++]
                    };

                case ReportIDList.StartedReport:
                    return new StartedReport
                    {
                        Datetime = DateTime.Now,
                        reportID = parts[index++],
                        reportName = parts[index++],
                        ModelID = parts[index++],
                        OPID = parts[index++],
                        ProcID = parts[index++],
                        MaterialID = parts[index++],
                        LotID = parts[index++]
                    };

                case ReportIDList.CanceledReport:
                    return new CanceledReport
                    {
                        Datetime = DateTime.Now,
                        reportID = parts[index++],
                        reportName = parts[index++],
                        ModelID = parts[index++],
                        OPID = parts[index++],
                        ProcID = parts[index++],
                        MaterialID = parts[index++],
                        LotID = parts[index++]
                    };

                case ReportIDList.CompleteReport:
                    return new CompletedReport
                    {
                        Datetime = DateTime.Now,
                        reportID = parts[index++],
                        reportName = parts[index++],
                        ModelID = parts[index++],
                        OPID = parts[index++],
                        ProcID = parts[index++],
                        MaterialID = parts[index++],
                        LotID = parts[index++],
                        Sen1 = Convert.ToDouble(parts[index++]),
                        Sen2 = Convert.ToDouble(parts[index++]),
                        Sen3 = Convert.ToDouble(parts[index++]),
                        Jud1 = parts[index++],
                        Jud2 = parts[index++],
                        Jud3 = parts[index++],
                        TotalJud = parts[index++]
                    };

                default:
                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 알 수 없는 ReportID: {reportID}\r\n");
                    return null; // 알 수 없는 ReportID는 null 반환
            }
        }


        private object ParsingRecvData(string p_data)
        {
            int recvReportID = new int();
            int index = 0;
            string[] parts = new string[] { };

            try
            {
                // 데이터가 예상한 포맷인지 확인 (예: "_10701/Wafer/...") 
                if (p_data != "")
                {
                    parts = p_data.Split('/');

                    recvReportID = Convert.ToInt32(parts[index].Substring(1, parts[index++].Length - 1));
                }

                switch (recvReportID)
                {
                    case ReportIDList.offlineChange:
                        // 해당 처리 내용 추가
                        break;

                    case ReportIDList.onlineRemote:
                        // 해당 처리 내용 추가
                        break;

                    case ReportIDList.IDReport:
                        IDReport tempIDReport = new IDReport();
                        tempIDReport.Datetime = DateTime.Now;
                        tempIDReport.reportID = recvReportID.ToString();
                        tempIDReport.reportName = "IDReport";
                        tempIDReport.ModelID = parts[index++];
                        tempIDReport.OPID = parts[index++];
                        tempIDReport.ProcID = parts[index++];
                        tempIDReport.MaterialID = parts[index++];

                        Report.IDReport.Enqueue(tempIDReport);
                        recvReportIDqueue.Enqueue(tempIDReport.reportID);

                        return tempIDReport;

                    case ReportIDList.StartedReport:
                        StartedReport startedReport = new StartedReport();
                        startedReport.Datetime = DateTime.Now;
                        startedReport.reportID = recvReportID.ToString();
                        startedReport.reportName = "StartedReport";
                        startedReport.ModelID = parts[index++];
                        startedReport.OPID = parts[index++];
                        startedReport.ProcID = parts[index++];
                        startedReport.MaterialID = parts[index++];
                        startedReport.LotID = parts[index++];

                        Report.startedReport.Enqueue(startedReport);
                        recvReportIDqueue.Enqueue(startedReport.reportID);

                        return startedReport;

                    case ReportIDList.CanceledReport:
                        CanceledReport canceledReport = new CanceledReport();
                        canceledReport.Datetime = DateTime.Now;
                        canceledReport.reportID = recvReportID.ToString();
                        canceledReport.reportName = "CanceledReport";
                        canceledReport.ModelID = parts[index++];
                        canceledReport.OPID = parts[index++];
                        canceledReport.ProcID = parts[index++];
                        canceledReport.MaterialID = parts[index++];
                        canceledReport.LotID = parts[index++];

                        Report.canceledReport.Enqueue(canceledReport);
                        recvReportIDqueue.Enqueue(canceledReport.reportID);

                        return canceledReport;

                    case ReportIDList.CompleteReport:
                        CompletedReport completedReport = new CompletedReport();
                        completedReport.Datetime = DateTime.Now;
                        completedReport.reportID = recvReportID.ToString();
                        completedReport.reportName = "CompleteReport";
                        completedReport.ModelID = parts[index++];
                        completedReport.OPID = parts[index++];
                        completedReport.ProcID = parts[index++];
                        completedReport.MaterialID = parts[index++];
                        completedReport.LotID = parts[index++];
                        completedReport.Sen1 = Convert.ToDouble(parts[index++]);
                        completedReport.Sen2 = Convert.ToDouble(parts[index++]);
                        completedReport.Sen3 = Convert.ToDouble(parts[index++]);
                        completedReport.Jud1 = parts[index++];
                        completedReport.Jud2 = parts[index++];
                        completedReport.Jud3 = parts[index++];
                        completedReport.TotalJud = parts[index++];

                        Report.completedReport.Enqueue(completedReport);
                        recvReportIDqueue.Enqueue(completedReport.reportID);

                        return completedReport;

                    default:
                        updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]: {recvReportID}\r\n");
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // DB에 데이터를 삽입하는 메서드
        private void InsertToDatabase(object reportData)
        {
            try
            {
                // 1. 구조체 또는 클래스 이름에 따라 테이블 이름 매핑
                string tableName = reportData switch
                {
                    IDReport => "IDReport",
                    StartedReport => "StartedReport",
                    CanceledReport => "CanceledReport",
                    CompletedReport => "CompleteReport",
                    _ => throw new Exception($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 알 수 없는 데이터 타입")
                };

                // 2. SQL 컬럼 및 파라미터 생성
                var columns = new List<string>();
                var parameters = new List<string>();

                // 구조체 또는 클래스 필드 가져오기
                var fields = reportData.GetType().GetFields(); // 구조체에서 필드 추출
                foreach (var field in fields)
                {
                    columns.Add(field.Name);
                    parameters.Add($"@{field.Name}");
                }

                // 3. SQL 쿼리 문자열 생성
                string query = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) " +
                               $"VALUES ({string.Join(", ", parameters)})";

                // 4. SQL 커맨드 실행
                using (SqlCommand command = new SqlCommand(query, dbConnection))
                {
                    foreach (var field in fields)
                    {
                        var value = field.GetValue(reportData) ?? DBNull.Value; // NULL 값 처리
                        command.Parameters.AddWithValue($"@{field.Name}", value);
                    }

                    command.ExecuteNonQuery();
                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {tableName} DB 삽입 성공: {reportData.GetType().Name}\r\n");


                }
            }
            catch (Exception ex)
            {
                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] DB 삽입 실패: {ex.Message}\r\n");
            }
        }


        // 4. 버튼 이벤트
        private void Selectbtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 조회 모드 활성화 및 실시간 갱신 중지
                isQueryMode = true;
                updateTimer.Stop();

                string baseQuery = "SELECT * FROM CompleteReport";
                List<string> conditions = new List<string>();

                // 불량 조건 추가
                if (Judgecheck.Checked)
                {
                    string selectedDefect = comboBox2.SelectedItem?.ToString();
                    if (!string.IsNullOrEmpty(selectedDefect))
                    {
                        switch (selectedDefect)
                        {
                            case "Sen1":
                                conditions.Add("Jud1 = 'Fail'");
                                break;
                            case "Sen2":
                                conditions.Add("Jud2 = 'Fail'");
                                break;
                            case "Sen3":
                                conditions.Add("Jud3 = 'Fail'");
                                break;
                            case "Total":
                                conditions.Add("TotalJud = 'Fail'");
                                break;
                            default:
                                textBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 올바른 센서를 선택하세요.\r\n");
                                return;
                        }
                    }
                }

                // 넘버 조건 추가
                if (Barcodecheck.Checked)
                {
                    string selectedModelID = comboBox3.SelectedItem?.ToString();
                    if (!string.IsNullOrEmpty(selectedModelID))
                    {
                        conditions.Add($"ModelID = '{selectedModelID}'");
                    }
                }

                // 조건 조합
                string query = baseQuery;
                if (conditions.Count > 0)
                {
                    query += " WHERE " + string.Join(" AND ", conditions);
                }

                // 쿼리 실행
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, dbConnection);
                System.Data.DataTable dataTable = new System.Data.DataTable();
                dataAdapter.Fill(dataTable);

                // 결과를 DataGridView에 바인딩
                dataGridView1.DataSource = null; // 기존 데이터 초기화
                dataGridView1.DataSource = dataTable;

                // 로그 출력
                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 조회 성공\r\n");
                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 조회 결과: {dataTable.Rows.Count} 개의 행 반환\r\n");
            }
            catch (Exception ex)
            {
                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 조회 실패: {ex.Message}\r\n");
            }
        }

        private void Clearbtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                isQueryMode = false; // 실시간 모드로 전환
                updateTimer.Start(); // 실시간 갱신 재개

                // 콤보박스와 체크박스 초기화
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                Judgecheck.Checked = false;
                Barcodecheck.Checked = false;

                // 전체 데이터 조회
                RefreshUnifiedReport();

                textBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 초기화 완료: 실시간 모드로 전환\r\n");
            }
            catch (Exception ex)
            {
                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 초기화 실패: {ex.Message}\r\n");
            }
        }

        private void MainSequenceThread()
        {
            int reportID = new int(); // 수신된 ReportID
            string lastModelID = string.Empty; // 이전에 처리된 ModelID 저장
            string lastMaterialType = string.Empty; // MaterialID의 마지막 문자 저장 (A 또는 B)

            while (bMainThreadChk)
            {
                if (recvReportIDqueue.Count > 0)
                {
                    string strReport = recvReportIDqueue.Dequeue(); // 큐에서 ReportID 가져오기
                    reportID = Convert.ToInt32(strReport);

                    switch (reportID)
                    {
                        case ReportIDList.IDReport:
                            // IDReport 처리 로직
                            try
                            {
                                IDReport idReport = Report.IDReport.Dequeue(); // 큐에서 IDReport 가져오기

                                // 모델 체인지 처리 (ACCEPT / CANCEL)
                                string currentModelIDLastChar = idReport.ModelID.Length > 0 ? idReport.ModelID[^1].ToString() : string.Empty;

                                if (currentModelIDLastChar == "A" || currentModelIDLastChar == "B")
                                {
                                    // ModelID 마지막 문자가 A 또는 B이면 ACCEPT
                                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 모델 변경 일치, ACCEPT 전송\r\n");

                                    var acceptMessage = CreatePacket("MODEL_CHANGE_ACCEPT", idReport.ModelID, idReport.ProcID, idReport.MaterialID, idReport.code, idReport.text);
                                    SendPacket(acceptMessage);
                                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ACCEPT 신호 전송 성공: {acceptMessage}\r\n");
                                }
                                else
                                {
                                    // A, B가 아닌 경우 CANCEL
                                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 잘못된 ModelID, CANCEL 전송\r\n");

                                    var cancelMessage = CreatePacket("MODEL_CHANGE_CANCEL", idReport.ModelID, idReport.ProcID, idReport.MaterialID, idReport.code, idReport.text);
                                    SendPacket(cancelMessage);
                                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] CANCEL 신호 전송 성공: {cancelMessage}\r\n");
                                }

                                // ACCEPT 후 START/STOP 처리
                                if (currentModelIDLastChar == "A" || currentModelIDLastChar == "B")
                                {
                                    // 모델이 동일하고, Material 타입이 다르면 START 전송
                                    if (idReport.ModelID == lastModelID && currentModelIDLastChar != lastMaterialType)
                                    {
                                        updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ModelID 동일, Material 타입 다름, START 전송\r\n");

                                        // START 패킷 생성 및 전송
                                        var startMessage = CreatePacket("_START", idReport.ModelID, idReport.ProcID, idReport.MaterialID, idReport.code, idReport.text);
                                        SendPacket(startMessage);

                                        updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] START 신호 전송 성공: {startMessage}\r\n");
                                    }
                                    else
                                    {
                                        // ModelID가 다르거나 Material 타입이 동일할 경우 CANCEL
                                        updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ModelID 불일치 또는 Material 타입 동일, CANCEL 전송\r\n");

                                        // CANCEL 패킷 생성 및 전송
                                        var cancelMessage = CreatePacket("_CANCEL", idReport.ModelID, idReport.ProcID, idReport.MaterialID, idReport.code, idReport.text);
                                        SendPacket(cancelMessage);

                                        updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] CANCEL 신호 전송 성공: {cancelMessage}\r\n");
                                    }
                                }

                                // 마지막 처리된 ModelID와 MaterialID 저장
                                lastModelID = idReport.ModelID;
                                lastMaterialType = currentModelIDLastChar;
                            }
                            catch (Exception ex)
                            {
                                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] IDReport 처리 중 오류 발생: {ex.Message}\r\n");
                            }
                            break;

                        default:
                            updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 처리되지 않은 ReportID: {reportID}\r\n");
                            break;
                    }
                }
            }
        }




        // 패킷 문자열 생성 메서드
        private string CreatePacket(string commandType, string modelID, string procID, string materialID, string code, string text)
        {
            string lotID = DateTime.Now.ToString("yyyyMM"); // LotID 생성 (현재 년월)
            return $"{commandType}/{modelID}/{procID}/{materialID}/{lotID}/{code}/{text}"; // 패킷 형식으로 문자열 반환
        }

        // 패킷 전송 메서드
        private void SendPacket(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message); // 메시지를 바이트 배열로 변환
                clientSocket.Send(buffer); // 소켓을 통해 메시지 전송
            }
            catch (Exception ex)
            {
                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 패킷 전송 실패: {ex.Message}\r\n"); // 전송 실패 로그
            }
        }



        // 구조체 정의
        public struct StartPacket
        {
            public string StartType;  // 패킷 시작 유형
            public string RCMD;       // 명령 유형
            public string ModelID;    // Code01에서 가져온 ModelID
            public string ProcID;     // 사용자 입력 PROC ID
            public string MaterialID; // Code01에서 가져온 MaterialID
            public string LotID;      // 사용자 입력 LOT ID
            public string EndType;    // 패킷 종료 유형
        }
        public struct CancelPacket
        {
            public string StartType;  // 패킷 시작 유형
            public string RCMD;       // 명령 유형
            public string ModelID;    // Code01에서 가져온 ModelID
            public string ProcID;     // 사용자 입력 PROC ID
            public string MaterialID; // Code01에서 가져온 MaterialID
            public string LotID;      // 사용자 입력 LOT ID
            public string EndType;    // 패킷 종료 유형
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxText))
            {
                textBoxLog.AppendText(textBoxText);
                textBoxText = string.Empty;
            }
        }

        //LOG 저장
        private void SaveLogToFile()
        {
            try
            {
                // 로그 파일에 기록
                using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 로그 저장 시작");
                    writer.WriteLine(textBoxLog.Text); // TextBoxLog에 출력된 내용을 파일에 저장
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 로그 저장 종료");
                }
            }
            catch (Exception ex)
            {
                // 로그 저장 실패 시 메시지 표시
                MessageBox.Show($"로그 저장 실패: {ex.Message}");
            }
        }
    }
}
