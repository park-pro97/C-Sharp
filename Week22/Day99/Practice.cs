- 데이터를 받고 처리하는 과정에서 또 새로운 데이터를 받을 수 있으므로 QUEUE를 사용해 쌓아뒀다가 하나씩 처리하는 방식으로 다른 값을 저장해둘 수 있게 했음.
- MES는 CIM에 붙는 클라이언트 입장이라 통신 오류가 생기면 끊겼는데 자동으로 재연결을 시도하는 기능을 추가해서 안정성 향상
- 받은 데이터를 파싱, 변환 과정을 거쳐 각 Report마다 들어가는 테이블이 다르므로 DB에 올리는  기능 구현
- 여태 제일 먼저 받는 데이터는 ID Report였는데 앞에 Online Report를 추가했는데, _10104로 시작하는 데이터가 들어오면 LOG 창에 Online을 출력

//주석처리된 부분은 어제까지 사용했던 코드인데 오늘 수정했음


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
                MessageBox.Show($"DB 연결 실패: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                    updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 소켓 오류 발생");
                }
                Thread.Sleep(100);
            }
        }

        private void updateErrtext(string msg)
        {
            textBoxText = msg;
        }
        private void ProcessReceivedData(string data)
        {
            updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 수신된 데이터: {data}\r\n");

            // 데이터를 파싱하여 필요한 정보를 추출
            var reportData = ParsingRecvData(data);
            InsertToDatabase(reportData);
            //this.Invoke(new Action(() =>
            //{
                

            //    //// 데이터를 변환하여 구조체로 생성
            //    //string[] parts = data.Split('/'); // "/" 기준으로 데이터를 나눔
            //    //var transformedData = TransformData(parts); // 변환 메서드 호출

            //    //if (transformedData != null)
            //    //{
            //    //    // 데이터베이스에 삽입


            //    //    // 큐에 데이터 삽입 (추가 처리 가능)
            //    //    switch (transformedData)
            //    //    {
            //    //        case IDReport idReport:
            //    //            Report.IDReport.Enqueue(idReport);
            //    //            recvReportIDqueue.Enqueue(idReport.reportID);
            //    //            break;

            //    //        case StartedReport startedReport:
            //    //            Report.startedReport.Enqueue(startedReport);
            //    //            recvReportIDqueue.Enqueue(startedReport.reportID);
            //    //            break;

            //    //        case CanceledReport canceledReport:
            //    //            Report.canceledReport.Enqueue(canceledReport);
            //    //            recvReportIDqueue.Enqueue(canceledReport.reportID);
            //    //            break;

            //    //        case CompletedReport completedReport:
            //    //            Report.completedReport.Enqueue(completedReport);
            //    //            recvReportIDqueue.Enqueue(completedReport.reportID);
            //    //            break;

            //    //        default:
            //    //            textBoxLog.AppendText("큐에 추가할 수 없는 데이터 형식입니다.\r\n");
            //    //            break;
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    textBoxLog.AppendText("데이터 변환 실패: 처리되지 않았습니다.\r\n");
            //    //} 
            //}));
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

                string baseQuery = "SELECT * FROM UnifiedReport";
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
                                textBoxLog.AppendText("올바른 센서를 선택하세요.\r\n");
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
                updateErrtext("조회 성공\r\n");
                updateErrtext("조회 결과: {dataTable.Rows.Count} 개의 행 반환\r\n");
            }
            catch (Exception ex)
            {
                updateErrtext("조회 실패: {ex.Message}\r\n");
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

                textBoxLog.AppendText("초기화 완료: 실시간 모드로 전환\r\n");
            }
            catch (Exception ex)
            {
                updateErrtext("초기화 실패: {ex.Message}\r\n");
            }
        }


        private void MainSequenceThread()
        {
            int reportID = new int();

            while (bMainThreadChk)
            {
                if (recvReportIDqueue.Count > 0)
                {
                    string strReport = recvReportIDqueue.Dequeue();
                    reportID = Convert.ToInt32(strReport);

                    switch (reportID)
                    {
                        case ReportIDList.IDReport:
                            // START RCMD
                            try
                            {
                                // PROCID와 LOTID를 사용자 입력으로 가져오기

                                IDReport idReport = Report.IDReport.Dequeue();

                                //string procID = PROCtext.Text.Trim();
                                //string lotID = LOTtext.Text.Trim();

                                //string query = "SELECT TOP 1 ModelID, MaterialID FROM IDReport ORDER BY Code DESC";
                                //string modelID = null;
                                //string materialID = null;

                                //using (SqlCommand command = new SqlCommand(query, dbConnection))
                                //{
                                //    using (SqlDataReader reader = command.ExecuteReader())
                                //    {
                                //        if (reader.Read())
                                //        {
                                //            modelID = reader["ModelID"].ToString();
                                //            materialID = reader["MaterialID"].ToString();
                                //        }
                                //    }
                                //}

                                //// 데이터가 없을 경우 예외 처리
                                //if (string.IsNullOrEmpty(modelID) || string.IsNullOrEmpty(materialID))
                                //{
                                //    MessageBox.Show("Code01 테이블에서 최신 데이터를 가져오지 못했습니다.");
                                //    return;
                                //}

                                // 구조체 생성
                                var startPacket = new StartPacket
                                {
                                    RCMD = "_START",          // RCMD 명령
                                    ModelID = idReport.ModelID,       // IDReport에서 가져온 ModelID
                                    ProcID = idReport.ProcID,         // 텍스트박스에서 가져온 PROC ID
                                    MaterialID = idReport.MaterialID, // Code01에서 가져온 MaterialID
                                    LotID = DateTime.Now.ToString("yyyyMM")           // 텍스트박스에서 가져온 LOT ID
                                };

                                var cancelPacket = new CancelPacket
                                {
                                    RCMD = "_CANCEL",          // RCMD 명령
                                    ModelID = idReport.ModelID,       // IDReport에서 가져온 ModelID
                                    ProcID = idReport.ProcID,         // 텍스트박스에서 가져온 PROC ID
                                    MaterialID = idReport.MaterialID, // Code01에서 가져온 MaterialID
                                    LotID = DateTime.Now.ToString("yyyyMM")           // 텍스트박스에서 가져온 LOT ID
                                };

                                // 패킷을 문자열 형태로 변환
                                string message = $"{startPacket.RCMD}/{startPacket.ModelID}/{startPacket.ProcID}/{startPacket.MaterialID}/{startPacket.LotID}";

                                // 소켓을 통해 패킷 전송
                                byte[] buffer = Encoding.UTF8.GetBytes(message);
                                clientSocket.Send(buffer);

                                // 전송 성공 로그 출력
                                updateErrtext($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] START 신호 전송 성공: {message}\r\n");
                            }
                            catch (Exception ex)
                            {
                                // 예외 처리 로그 출력
                                
                            }


                            // SAVE DB
                            //void InsertIDReportDataToDatabase(IDReport reportData)
                            //{
                            //    try
                            //    {
                            //        // SQL 삽입 쿼리
                            //        string query = "INSERT INTO IDReport (Datetime, ReportID, ReportName, ModelID, OPID, MaterialID) " +
                            //                       "VALUES (@Datetime, @ReportID, @ReportName, @ModelID, @OPID, @MaterialID)";

                            //        using (SqlCommand command = new SqlCommand(query, dbConnection))
                            //        {
                            //            command.Parameters.AddWithValue("@Datetime", reportData.Datetime);
                            //            command.Parameters.AddWithValue("@ReportID", reportData.reportID);
                            //            command.Parameters.AddWithValue("@ReportName", reportData.reportName);
                            //            command.Parameters.AddWithValue("@ModelID", reportData.ModelID);
                            //            command.Parameters.AddWithValue("@OPID", reportData.OPID);
                            //            command.Parameters.AddWithValue("@MaterialID", reportData.MaterialID);

                            //            command.ExecuteNonQuery();
                            //            updateErrtext("DB 삽입 성공: {reportData.reportID}\r\n");
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        updateErrtext("DB 삽입 실패: {ex.Message}\r\n");
                            //    }
                            //}


                            // Clear

                            break;
                    }
                }
            }
        }

        private void STARTbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // PROCID와 LOTID를 사용자 입력으로 가져오기
                string procID = PROCtext.Text.Trim();
                string lotID = LOTtext.Text.Trim();

                // 입력값 검증
                if (string.IsNullOrEmpty(procID) || string.IsNullOrEmpty(lotID))
                {
                    MessageBox.Show("PROCID와 LOTID를 입력하세요.");
                    return;
                }

                // Code01 테이블에서 최신 데이터 가져오기
                string query = "SELECT TOP 1 ModelID, MaterialID FROM Code01 ORDER BY Code DESC";
                string modelID = null;
                string materialID = null;

                using (SqlCommand command = new SqlCommand(query, dbConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            modelID = reader["ModelID"].ToString();
                            materialID = reader["MaterialID"].ToString();
                        }
                    }
                }

                // 데이터가 없을 경우 예외 처리
                if (string.IsNullOrEmpty(modelID) || string.IsNullOrEmpty(materialID))
                {
                    MessageBox.Show("DB에서 최신 데이터를 가져오지 못했습니다.");
                    return;
                }

                // 구조체 생성
                var startPacket = new StartPacket
                {
                    //StartType = "ST",        // 패킷 시작 유형
                    RCMD = "_START",          // RCMD 명령
                    ModelID = modelID,       // Code01에서 가져온 ModelID
                    ProcID = procID,         // 텍스트박스에서 가져온 PROC ID
                    MaterialID = materialID, // Code01에서 가져온 MaterialID
                    LotID = lotID,           // 텍스트박스에서 가져온 LOT ID
                    //EndType = "ET"           // 패킷 종료 유형
                };

                // 패킷을 문자열 형태로 변환
                string message = $"{startPacket.RCMD}/{startPacket.ModelID}/{startPacket.ProcID}/{startPacket.MaterialID}/{startPacket.LotID}";

                // 소켓을 통해 패킷 전송
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(buffer);

                // 전송 성공 로그 출력
                updateErrtext("START 신호 전송 성공: {message}\r\n");
            }
            catch (Exception ex)
            {
                // 예외 처리 로그 출력
                updateErrtext("START 신호 전송 실패: {ex.Message}\r\n");
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

        private void CANCELbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // 입력값 검증
                string procID = PROCtext.Text.Trim();
                string lotID = LOTtext.Text.Trim();

                if (string.IsNullOrEmpty(procID) || string.IsNullOrEmpty(lotID))
                {
                    MessageBox.Show("PROCID와 LOTID를 입력하세요.");
                    return;
                }

                // Code01 테이블에서 최신 데이터 가져오기
                string query = "SELECT TOP 1 ModelID, MaterialID FROM Code01 ORDER BY Code DESC";
                string modelID = null;
                string materialID = null;

                using (SqlCommand command = new SqlCommand(query, dbConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            modelID = reader["ModelID"].ToString();
                            materialID = reader["MaterialID"].ToString();
                        }
                    }
                }

                // 데이터가 없을 경우 예외 처리
                if (string.IsNullOrEmpty(modelID) || string.IsNullOrEmpty(materialID))
                {
                    MessageBox.Show("테이블에서 최신 데이터를 가져오지 못했습니다.");
                    return;
                }

                // StartRcmd 구조체 생성 (CANCEL 명령으로 설정)
                var cancelPacket = new StartPacket
                {
                    RCMD = "_CANCEL",          // RCMD에 CANCEL 설정
                    ModelID = modelID,
                    ProcID = procID,
                    MaterialID = materialID,
                    LotID = lotID
                };

                // 패킷을 문자열 형태로 변환
                string message = $"{cancelPacket.RCMD}/{cancelPacket.ModelID}/{cancelPacket.ProcID}/{cancelPacket.MaterialID}/{cancelPacket.LotID}";

                // 소켓을 통해 패킷 전송
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(buffer);

                // 전송 성공 로그 출력
                updateErrtext("CANCEL 신호 전송 성공: {message}\r\n");
            }
            catch (Exception ex)
            {
                // 예외 처리 로그 출력
                updateErrtext("CANCEL 신호 전송 실패: {ex.Message}\r\n");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxText))
            {
                textBoxLog.AppendText(textBoxText);
                textBoxText = string.Empty;
            }
        }
    }
}
