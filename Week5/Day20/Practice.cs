//
CREATE TABLE PERSON(
    ID      NUMBER(5),
    NAME    VARCHAR2(30)
);
------------------------------------------------------------------------------
//
ALTER TABLE PERSON
ADD HP VARCHAR(20);

------------------------------------------------------------------------------
//
ALTER TABLE PERSON
RENAME COLUMN HP TO TEL;
------------------------------------------------------------------------------
//
ALTER TABLE PERSON
MODIFY TEL VARCHAR2(13);
------------------------------------------------------------------------------
//
ALTER TABLE PERSON
DROP COLUMN TEL;
------------------------------------------------------------------------------
//시퀀스 생성
CREATE SEQUENCE DEPT_SEQUENCE
INCREMENT BY 10
START WITH 10
MAXVALUE 90
MINVALUE 0
CYCLE;
CACHE;
------------------------------------------------------------------------------
//시퀀스
INSERT INTO DEPT_SEQUENCE )DEPTNO, DNAME, LOC)
VALUES (seq_dept_sequence.nextval, 'DATABASE', 'ANDONG');

------------------------------------------------------------------------------
//
SELECT * FROM DEPT SEQUENCE;
------------------------------------------------------------------------------
//
SELECT SEQ_DEPT_SEQUENCE.CURRVAL FROM DUAL;
------------------------------------------------------------------------------
//
ALTER SEQUENCE SEQ_DEPT_SEQUENCE
INCREMENT BY 3
MAXVALUE 99
CYCLE;
------------------------------------------------------------------------------
//
DROP SEQUENCE SEQ_DEPT_SEQUENCE;
DROP TABLE DEPT_SEQUENCE;
------------------------------------------------------------------------------
//
CREATE SEQUENCE SEQ_DEPT_SEQUENCE;
INCRMENT BY 10
START WITH 10
MAXVALUE 90
------------------------------------------------------------------------------
//제약14-1   밑은 NOT NULL NULL을 허용하지 않음
CREATE TABLE TABLE_NOTNULL(
    LOGIN_ID VARCHAR2(20) NOT NULL,
    LOGIN_PWD VARCHAR2(20)NOT NULL,
    TEL VARCHAR2(20)
);
------------------------------------------------------------------------------
//위의 테이블을 만들고 비밀번호를 NULL로 삽입하려고 하니 에러가 나옴
INSERT INTO TABLE_NOTNULL
VALUES ('ABCD', NULL, '010-1234-5678');

------------------------------------------------------------------------------
//비밀번호를 입력하니 잘 되는 것을 확인함
INSERT INTO TABLE_NOTNULL
VALUES ('ABCD', 'ABCD', '010-1234-5678');

------------------------------------------------------------------------------
//
SELECT * FROM TABLE_NOTNULL;
------------------------------------------------------------------------------
//14-6 제약에 이름을 지정할 수 있다.  CONSTRAINT
CREATE TABLE TABLE_NOTNULL2(
    LOGOIN_ID VARCHAR2(20) CONSTRAINT AAA_MY_ID_NOTNULL NOT NULL,
    LOGOIN_PWD VARCHAR2(20) CONSTRAINT AAA_MY_PWD_NOTNULL NOT NULL,
    TEL VARCHAR2(20)
);

------------------------------------------------------------------------------
//
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME
FROM USER_CONSTRAINTS;
------------------------------------------------------------------------------
//추가로 제약이 없는 컬럼에 제약(CONSTRAINT)을 줄 수 있다.
ALTER TABLE TABLE_NOTNULL2
MODIFY(TEL NOT NULL);

DESC TABLE_NOTNULL2;

ALTER TABLE TABLE_NOTNULL2
MODIFY(TEL NULL);

ALTER TABLE TABLE_NOTNULL2
MODIFY(TEL CONSTRAINT AAA_MY_NOTNULL3 NOT NULL);
------------------------------------------------------------------------------
//삭제
ALTER TABLE TABLE_NOTNULL2
DROP CONSTRAINT AAA_MY_NOTNULL3;

------------------------------------------------------------------------------
//아이디 변경 AAA를 BBB로
ALTER TABLE TABLE_NOTNULL2
RENAME CONSTRAINT AAA_MY_ID_NOTNULL TO AAA_BBB;

------------------------------------------------------------------------------
//실습 14--29
DROP TABLE TABLE_PK; -- 테이블 삭제

------------------------------------------------------------------------------
//생성
CREATE TABLE TABLE_PK(
    LOGIN_ID VARCHAR2(20) PRIMARY KEY,
    LOGIN_PWD VARCHAR2(20) NOT NULL,
    TEL VARCHAR2(20)
);
DESC TABLE_PK;


------------------------------------------------------------------------------
//[미니 콘솔 프로그램] 명암관리

--   SELECT * FROM BusinessCards; ---[ 실행해서 확인해봄 developer로]

------------------------------------------------------------------------------
//C# 코드
using Oracle.ManagedDataAccess.Client;
using System;

namespace My
{
    class Program
    {
        static string strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=9000)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=scott;Password=tiger;";

        static void Main(string[] args)
        {
            CreateDatabaseAndTable();
            MainMenu();
        }

        static void CreateDatabaseAndTable()
        {
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                conn.Open();
                // 시퀀스 생성
                string createSequenceQuery = @"
                    BEGIN
                        EXECUTE IMMEDIATE 'CREATE SEQUENCE BusinessCardSeq START WITH 1 INCREMENT BY 1 NOCACHE NOCYCLE';
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

                // 테이블 생성
                string createTableQuery = @"
                    CREATE TABLE BusinessCards (
                        CardID NUMBER PRIMARY KEY,
                        Name VARCHAR2(50) NOT NULL,
                        PhoneNumber VARCHAR2(20) NOT NULL,
                        Email VARCHAR2(50),
                        Company VARCHAR2(100),
                        Address VARCHAR2(200)
                    )";
                using (OracleCommand command = new OracleCommand(createTableQuery, conn))
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
                Console.WriteLine("명함 관리 시스템");
                Console.WriteLine("============================");
                Console.WriteLine("1. 명함 추가");
                Console.WriteLine("2. 명함 목록 보기");
                Console.WriteLine("3. 명함 수정");
                Console.WriteLine("4. 명함 삭제");
                Console.WriteLine("5. 명함 검색");
                Console.WriteLine("6. 종료");
                Console.WriteLine("============================");
                Console.Write("선택: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddBusinessCard();
                        break;
                    case "2":
                        ViewBusinessCards();
                        break;
                    case "3":
                        UpdateBusinessCard();
                        break;
                    case "4":
                        DeleteBusinessCard();
                        break;
                    case "5":
                        SearchBusinessCards();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 시도하세요.");
                        break;
                }
            } while (!exit);
        }

        static void AddBusinessCard()
        {
            Console.Clear();
            Console.WriteLine("============================");
            Console.WriteLine("명함 추가");
            Console.WriteLine("============================");
            Console.Write("이름: ");
            string name = Console.ReadLine();
            Console.Write("전화번호: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("이메일: ");
            string email = Console.ReadLine();
            Console.Write("회사: ");
            string company = Console.ReadLine();
            Console.Write("주소: ");
            string address = Console.ReadLine();
            Console.WriteLine("============================");

            using (OracleConnection conn = new OracleConnection(strConn))
            {
                conn.Open();
                string insertQuery = "INSERT INTO BusinessCards (CardID, Name, PhoneNumber, Email, Company, Address) VALUES (BusinessCardSeq.NEXTVAL, :Name, :PhoneNumber, :Email, :Company, :Address)";
                using (OracleCommand command = new OracleCommand(insertQuery, conn))
                {
                    command.Parameters.Add(new OracleParameter("Name", name));
                    command.Parameters.Add(new OracleParameter("PhoneNumber", phoneNumber));
                    command.Parameters.Add(new OracleParameter("Email", email));
                    command.Parameters.Add(new OracleParameter("Company", company));
                    command.Parameters.Add(new OracleParameter("Address", address));
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("명함이 추가되었습니다.");
            Console.ReadLine();
        }

        static void ViewBusinessCards()
        {
            Console.Clear();
            Console.WriteLine("============================");
            Console.WriteLine("명함 목록");
            Console.WriteLine("============================");

            using (OracleConnection conn = new OracleConnection(strConn))
            {
                conn.Open();
                string selectQuery = "SELECT * FROM BusinessCards";
                using (OracleCommand command = new OracleCommand(selectQuery, conn))
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    int index = 1;
                    while (reader.Read())
                    {
                        Console.WriteLine($"{index}. {reader["Name"]} | {reader["PhoneNumber"]} | {reader["Email"]} | {reader["Company"]} | {reader["Address"]}");
                        index++;
                    }
                }
            }

            Console.WriteLine("============================");
            Console.ReadLine();
        }

        static void UpdateBusinessCard()
        {
            Console.Clear();
            Console.WriteLine("============================");
            Console.WriteLine("명함 수정");
            Console.WriteLine("============================");
            ViewBusinessCards();
            Console.Write("수정할 명함의 번호를 입력하세요: ");
            int cardId;
            while (!int.TryParse(Console.ReadLine(), out cardId))
            {
                Console.Write("유효한 번호를 입력하세요: ");
            }
            Console.WriteLine("============================");
            Console.Write("수정할 항목 (1:이름, 2:전화번호, 3:이메일, 4:회사, 5:주소): ");
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
                    columnName = "PhoneNumber";
                    break;
                case "3":
                    columnName = "Email";
                    break;
                case "4":
                    columnName = "Company";
                    break;
                case "5":
                    columnName = "Address";
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    return;
            }

            using (OracleConnection conn = new OracleConnection(strConn))
            {
                conn.Open();
                string updateQuery = $"UPDATE BusinessCards SET {columnName} = :NewValue WHERE CardID = :CardID";
                using (OracleCommand command = new OracleCommand(updateQuery, conn))
                {
                    command.Parameters.Add(new OracleParameter("NewValue", newValue));
                    command.Parameters.Add(new OracleParameter("CardID", cardId));
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("명함이 수정되었습니다.");
            Console.ReadLine();
        }

        static void DeleteBusinessCard()
        {
            Console.Clear();
            Console.WriteLine("============================");
            Console.WriteLine("명함 삭제");
            Console.WriteLine("============================");
            ViewBusinessCards();
            Console.Write("삭제할 명함의 번호를 입력하세요: ");
            int cardId;
            while (!int.TryParse(Console.ReadLine(), out cardId))
            {
                Console.Write("유효한 번호를 입력하세요: ");
            }
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
                string deleteQuery = "DELETE FROM BusinessCards WHERE CardID = :CardID";
                using (OracleCommand command = new OracleCommand(deleteQuery, conn))
                {
                    command.Parameters.Add(new OracleParameter("CardID", cardId));
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("명함이 삭제되었습니다.");
            Console.ReadLine();
        }

        static void SearchBusinessCards()
        {
            Console.Clear();
            Console.WriteLine("============================");
            Console.WriteLine("명함 검색");
            Console.WriteLine("============================");
            Console.Write("검색할 이름을 입력하세요: ");
            string nameToSearch = Console.ReadLine();
            Console.WriteLine("============================");
            Console.WriteLine("검색 결과");

            using (OracleConnection conn = new OracleConnection(strConn))
            {
                conn.Open();
                string searchQuery = "SELECT * FROM BusinessCards WHERE Name LIKE :Name";
                using (OracleCommand command = new OracleCommand(searchQuery, conn))
                {
                    command.Parameters.Add(new OracleParameter("Name", "%" + nameToSearch + "%"));
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        int index = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{index}. {reader["Name"]} | {reader["PhoneNumber"]} | {reader["Email"]} | {reader["Company"]} | {reader["Address"]}");
                            index++;
                        }
                    }
                }
            }

            Console.WriteLine("============================");
            Console.ReadLine();
        }
    }
}
