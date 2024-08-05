분식집의 키오스크 기능을 만들어 보는 시간을 가졌음.

// 하는 중 수정해야함
  using Oracle.ManagedDataAccess.Client;
using System;

namespace Consolemk1
{
    internal class Program
    {
        class ConsoleApp
        {
            static string strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=9000)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=scott;Password=tiger;";

            static void Main(string[] args)
            {
                CreateDatabaseAndTables();
                MainMenu();
            }

            static void CreateDatabaseAndTables()
            {
                using (OracleConnection conn = new OracleConnection(strConn))
                {
                    conn.Open();

                    // 시퀀스 생성
                    string createSequenceQuery = @"
                    BEGIN
                        EXECUTE IMMEDIATE 'CREATE SEQUENCE MenuItemSeq START WITH 1 INCREMENT BY 1 NOCACHE NOCYCLE';
                        EXECUTE IMMEDIATE 'CREATE SEQUENCE OrderSeq START WITH 1 INCREMENT BY 1 NOCACHE NOCYCLE';
                    EXCEPTION
                        WHEN OTHERS THEN
                            IF SQLCODE != -955 THEN
                                RAISE;
                            END IF;
                    END;";

                    using (OracleCommand command = new OracleCommand(createSequenceQuery, conn))
                    {
                        command.ExecuteNonQuery();
                    }

                    // 메뉴 항목 테이블 생성
                    string createMenuItemsTableQuery = @"
                    CREATE TABLE MenuItems (
                        ItemID NUMBER PRIMARY KEY,
                        Name VARCHAR2(100) NOT NULL,
                        Price NUMBER(10, 2) NOT NULL,
                        Category VARCHAR2(50) NOT NULL
                    )";

                    using (OracleCommand command = new OracleCommand(createMenuItemsTableQuery, conn))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (OracleException ex)
                        {
                            if (ex.Number != 955) // ORA-00955: name is already used by an existing object
                            {
                                throw;
                            }
                        }
                    }

                    // 주문 테이블 생성
                    string createOrdersTableQuery = @"
                    CREATE TABLE Orders (
                        OrderID NUMBER PRIMARY KEY,
                        MenuItemID NUMBER,
                        Quantity NUMBER NOT NULL,
                        OrderDate DATE DEFAULT SYSDATE,
                        FOREIGN KEY (MenuItemID) REFERENCES MenuItems(ItemID) ON DELETE CASCADE
                    )";

                    using (OracleCommand command = new OracleCommand(createOrdersTableQuery, conn))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (OracleException ex)
                        {
                            if (ex.Number != 955) // ORA-00955: name is already used by an existing object
                            {
                                throw;
                            }
                        }
                    }
                }
            }

            static void MainMenu()
            {
                bool exit = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine("============================");
                    Console.WriteLine("분식점 키오스크 시스템");
                    Console.WriteLine("============================");
                    Console.WriteLine("1. 메뉴 항목 추가");
                    Console.WriteLine("2. 메뉴 항목 수정");
                    Console.WriteLine("3. 메뉴 항목 삭제");
                    Console.WriteLine("4. 메뉴 보기");
                    Console.WriteLine("5. 주문 추가");
                    Console.WriteLine("6. 주문 보기");
                    Console.WriteLine("7. 종료");
                    Console.WriteLine("============================");
                    Console.Write("선택: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            AddMenuItem();
                            break;
                        case "2":
                            UpdateMenuItem();
                            break;
                        case "3":
                            DeleteMenuItem();
                            break;
                        case "4":
                            ViewMenuItems();
                            break;
                        case "5":
                            AddOrder();
                            break;
                        case "6":
                            ViewOrders();
                            break;
                        case "7":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("잘못된 선택입니다. 다시 시도하세요.");
                            break;
                    }
                } while (!exit);
            }

            static void AddMenuItem()
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("메뉴 항목 추가");
                Console.WriteLine("============================");
                Console.Write("이름: ");
                string name = Console.ReadLine();
                Console.Write("가격: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("카테고리: ");
                string category = Console.ReadLine();
                Console.WriteLine("============================");

                using (OracleConnection conn = new OracleConnection(strConn))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO MenuItems (ItemID, Name, Price, Category) VALUES (MenuItemSeq.NEXTVAL, :Name, :Price, :Category)";
                    using (OracleCommand command = new OracleCommand(insertQuery, conn))
                    {
                        command.Parameters.Add(new OracleParameter("Name", name));
                        command.Parameters.Add(new OracleParameter("Price", price));
                        command.Parameters.Add(new OracleParameter("Category", category));
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("메뉴 항목이 추가되었습니다.");
                Console.ReadLine();
            }

            static void UpdateMenuItem()
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("메뉴 항목 수정");
                Console.WriteLine("============================");
                ViewMenuItems();
                Console.Write("수정할 메뉴 항목의 번호를 입력하세요: ");
                int itemId = int.Parse(Console.ReadLine());
                Console.WriteLine("============================");
                Console.Write("수정할 항목 (1:이름, 2:가격, 3:카테고리): ");
                string fieldToUpdate = Console.ReadLine();
                Console.Write("새로운 값: ");
                string newValue = Console.ReadLine();

                string columnName;
                switch (fieldToUpdate)
                {
                    case "1":
                        columnName = "Name";
                        break;
                    case "2":
                        columnName = "Price";
                        newValue = decimal.Parse(newValue).ToString();
                        break;
                    case "3":
                        columnName = "Category";
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다.");
                        return;
                }

                using (OracleConnection conn = new OracleConnection(strConn))
                {
                    conn.Open();
                    string updateQuery = $"UPDATE MenuItems SET {columnName} = :NewValue WHERE ItemID = :ItemID";
                    using (OracleCommand command = new OracleCommand(updateQuery, conn))
                    {
                        command.Parameters.Add(new OracleParameter("NewValue", newValue));
                        command.Parameters.Add(new OracleParameter("ItemID", itemId));
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("메뉴 항목이 수정되었습니다.");
                Console.ReadLine();
            }

            static void DeleteMenuItem()
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("메뉴 항목 삭제");
                Console.WriteLine("============================");
                ViewMenuItems();
                Console.Write("삭제할 메뉴 항목의 번호를 입력하세요: ");
                int itemId = int.Parse(Console.ReadLine());
                Console.Write("정말 삭제하시겠습니까? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                {
                    Console.WriteLine("삭제가 취소되었습니다.");
                    Console.ReadLine();
                    return;
                }

                using (OracleConnection conn = new OracleConnection(strConn))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM MenuItems WHERE ItemID = :ItemID";
                    using (OracleCommand command = new OracleCommand(deleteQuery, conn))
                    {
                        command.Parameters.Add(new OracleParameter("ItemID", itemId));
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("메뉴 항목이 삭제되었습니다.");
                Console.ReadLine();
            }

            static void ViewMenuItems()
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("메뉴 목록");
                Console.WriteLine("============================");

                using (OracleConnection conn = new OracleConnection(strConn))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM MenuItems";
                    using (OracleCommand command = new OracleCommand(selectQuery, conn))
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        int index = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{index}. {reader["Name"]} | {reader["Price"]} | {reader["Category"]}");
                            index++;
                        }
                    }
                }

                Console.WriteLine("============================");
                Console.ReadLine();
            }

            static void AddOrder()
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("주문 추가");
                Console.WriteLine("============================");
                ViewMenuItems();
                Console.Write("주문할 메뉴 항목의 번호를 입력하세요: ");
                int menuItemId = int.Parse(Console.ReadLine());
                Console.Write("수량: ");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("============================");

                using (OracleConnection conn = new OracleConnection(strConn))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Orders (OrderID, MenuItemID, Quantity) VALUES (OrderSeq.NEXTVAL, :MenuItemID, :Quantity)";
                    using (OracleCommand command = new OracleCommand(insertQuery, conn))
                    {
                        command.Parameters.Add(new OracleParameter("MenuItemID", menuItemId));
                        command.Parameters.Add(new OracleParameter("Quantity", quantity));
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("주문이 추가되었습니다.");
                Console.ReadLine();
            }

            static void ViewOrders()
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("주문 목록");
                Console.WriteLine("============================");

                using (OracleConnection conn = new OracleConnection(strConn))
                {
                    conn.Open();
                    string selectQuery = @"
                    SELECT o.OrderID, m.Name, o.Quantity, o.OrderDate
                    FROM Orders o
                    JOIN MenuItems m ON o.MenuItemID = m.ItemID";
                    using (OracleCommand command = new OracleCommand(selectQuery, conn))
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        int index = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{index}. {reader["OrderID"]} | {reader["Name"]} | {reader["Quantity"]} | {reader["OrderDate"]}");
                            index++;
                        }
                    }
                }

                Console.WriteLine("============================");
                Console.ReadLine();
            }
        }
    }
}




------------------------------------------------------------
// 윈폼으로 데이터 그리드 실습 진행

  
//DataGridView 컨트롤을 Form으로
privatevoidForm1_Load(objectsender, EventArgse)
{
	this.dataGridView.Columns.Add("No", "No");
	this.dataGridView.Columns.Add("Name", "Name");
	this.dataGridView.Columns.Add("HP", "HP");
	this.dataGridView.Rows.Add("1", "홍길동", "010-1111-1111");
	this.dataGridView.Rows.Add("2", "이순신", "010-2222-2222");
}

// 버튼에 삽입기능 넣기
privatevoidForm1_Load(objectsender, EventArgse)
{
	dataGridView1.Columns.Add("No", "번호");
	dataGridView1.Columns.Add("Name", "이름");
}
privatevoidbutton1_Click(objectsender, EventArgse)
{
	dataGridView1.Rows.Add("1", "홍길동");
}

------------------------------------------------------------
//text박스 안의 내용이 버튼 누르면 올라감
namespace dataGrid01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView.Columns.Add("ID", "개별번호");
            dataGridView.Columns.Add("Name", "이름");
            dataGridView.Columns.Add("HP", "전화번호");

            // 데이터 추가
            //dataGridView.Rows.Add("1", "홍길동", "010-1111-1111");
            //dataGridView.Rows.Add("2", "이순신", "010-2222-2222");
        }
        // 버튼에 기능 삽입
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Add(tbID.Text, tbName.Text, tbHP.Text);
        }
    }
}
------------------------------------------------------------
//객체 코드
namespace dataGrid02
{
    class Student
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Data 만들기
            var dataList = new List<Student>()
            {
                new Student { No = "1", Name = "홍길동", HP = "010-1111-1111"},
                new Student { No = "2", Name = "이순신", HP = "010-2222-2222"},
                new Student { No = "3", Name = "을지문덕", HP = "010-3333-3333"},
                new Student { No = "4", Name = "강감찬", HP = "010-4444-4444"}
            };
            dataGridView1.DataSource = dataList;
        }
    }
}
------------------------------------------------------------
//Oracle에서 데이터 가져오기
  
  --1. 테이블 부터 만들고
  
SELECT * FROM STUDENT;
DROP TABLE STUDENT;

CREATE TABLE student(
    id      number(5) primary key,
    name    varchar2(30) not null,
    hp      varchar2(13)
);

INSERT INTO student
values (1, '홍길동', '010-1111-1111');
INSERT INTO student
values (2, '이순신', '010-2222-2222');

commit;
------------------------------------------------------------
  -- C#윈폼에서 쓰인 코드
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace _20240805_DataGridViewOracleConnect
{
    public partial class Form1 : Form
    {
        private string connectionString = "User Id=scott;Password=TIGER;Data Source=localhost:1521/XE;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, name, hp FROM student";
                    // 브로커 패턴 = 데이터를 맞춰주는 역할
                    OracleDataAdapter adapter = new OracleDataAdapter(query, connection);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataSource
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"에러 : {ex.Message}");
                }
            }
        }
    }
}
