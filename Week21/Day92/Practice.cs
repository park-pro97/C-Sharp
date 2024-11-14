//데이터 전송할 때 구조체 타입을 정의했다.

struct ID_Report // Report ID 구조체
{
    public string StartType;  // 패킷 시작 문자
    public string Code;       // "10701" 등 리포트 식별할 고유 코드
    public string ReportID; // "IDReport" 등의 리포트 타입 정보
    public int ModelID;
    public int OPID;
    public int MaterialID;
    public string EndType;    // 패킷 끝 문자
}

struct Started_Report // Started 구조체
{
    public string StartType;  // 패킷 시작 문자
    public string Code;       // "10703" 등 리포트 식별할 고유 코드
    public string ReportID; // 리포트 타입
    public int ModelID;
    public int OPID;
    public int ProcID;
    public int MaterialID;
    public int LotID;
    public string EndType;    // 패킷 끝 문자
}

struct Canceled_Report // Canceled 구조체
{
    public string StartType;  // 패킷 시작 문자
    public string Code;       // "10704" 등 리포트 식별할 고유 코드
    public string ReportID; // 리포트 타입
    public int ModelID;
    public int OPID;
    public int ProcID;
    public int MaterialID;
    public int LotID;
    public string EndType;    // 패킷 끝 문자
}

struct Completed_Report // Completed 구조체
{
    public string StartType;  // 패킷 시작 문자
    public string Code;       // "10713" 등 리포트 식별할 고유 코드
    public string ReportID;      // 리포트 타입
    public int ModelID;
    public int OPID;
    public int ProcID;
    public int MaterialID;
    public int LotID;
    public double Sen1;        // 센서 1 값 (전압)
    public double Sen2;        // 센서 2 값 (전류)
    public double Sen3;        // 센서 3 값 (열)
    public string Jud1;
    public string Jud2;
    public string Jud3;
    public string TotalJud;
    public string EndType;    // 패킷 끝 문자
}

----------------------------------------------------------------------------
//테이블도 구조에 맞게 설계하여 더미 데이터를 넣어 조회하는 기능을 구현하기 위해 최종 불량인 항목을 출력하는 쿼리까지 실행해봤다.
  CREATE TABLE Code01 (
    StartType VARCHAR(10),
    Code VARCHAR(10),               -- 숫자 코드만 저장
    ReportID VARCHAR(50),            -- "IDReport[10701]" 형식
    ModelID INT,
    OPID INT,
    MaterialID INT,
    EndType VARCHAR(10)
);

INSERT INTO Code01 (StartType, Code, ReportID, ModelID, OPID, MaterialID, EndType)
VALUES ('ST', '10701', 'IDReport[10701]', 10001, 200, 300, 'ET');



CREATE TABLE Code03 (
    StartType VARCHAR(10),
    Code VARCHAR(10),               -- 숫자 코드만 저장
    ReportID VARCHAR(50),            -- "StartedReport[10703]" 형식
    ModelID INT,
    OPID INT,
    ProcID INT,
    MaterialID INT,
    LotID INT,
    EndType VARCHAR(10)
);

INSERT INTO Code03 (StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, EndType)
VALUES ('ST', '10703', 'StartedReport[10703]', 10002, 200, 5, 300, 400, 'ET');



CREATE TABLE Code04 (
    StartType VARCHAR(10),
    Code VARCHAR(10),               -- 숫자 코드만 저장
    ReportID VARCHAR(50),            -- "CanceledReport[10704]" 형식
    ModelID INT,
    OPID INT,
    ProcID INT,
    MaterialID INT,
    LotID INT,
    EndType VARCHAR(10)
);

INSERT INTO Code04 (StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, EndType)
VALUES ('ST', '10704', 'CanceledReport[10704]', 10003, 200, 5, 300, 400, 'ET');



CREATE TABLE Code13 (
    StartType VARCHAR(10),
    Code VARCHAR(10),               -- 숫자 코드만 저장
    ReportID VARCHAR(50),            -- "CompletedReport[10713]" 형식
    ModelID INT,
    OPID INT,
    ProcID INT,
    MaterialID INT,
    LotID INT,
    Sen1 FLOAT,
    Sen2 FLOAT,
    Sen3 FLOAT,
    Jud1 VARCHAR(10),
    Jud2 VARCHAR(10),
    Jud3 VARCHAR(10),
    TotalJud VARCHAR(10),
    EndType VARCHAR(10)
);

INSERT INTO Code13 (StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, Sen1, Sen2, Sen3, Jud1, Jud2, Jud3, TotalJud, EndType)
VALUES ('ST', '10713', 'CompletedReport[10713]', 10004, 200, 5, 300, 400, 3.75, 1.25, 85.0, 'OK', 'OK', 'Fail', 'Fail', 'ET');



CREATE TABLE UnifiedReport (
    StartType VARCHAR(10),
    Code VARCHAR(10),               -- 숫자 코드만 저장
    ReportID VARCHAR(50),            -- 리포트 타입과 코드
    ModelID INT,
    OPID INT,
    ProcID INT NULL,
    MaterialID INT,
    LotID INT NULL,
    Sen1 FLOAT NULL,
    Sen2 FLOAT NULL,
    Sen3 FLOAT NULL,
    Jud1 VARCHAR(10) NULL,
    Jud2 VARCHAR(10) NULL,
    Jud3 VARCHAR(10) NULL,
    TotalJud VARCHAR(10) NULL,
    EndType VARCHAR(10)
);

-- 통합 데이터 삽입 쿼리
INSERT INTO UnifiedReport (StartType, Code, ReportID, ModelID, OPID, MaterialID, EndType)
SELECT StartType, Code, ReportID, ModelID, OPID, MaterialID, EndType FROM Code01;

INSERT INTO UnifiedReport (StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, EndType)
SELECT StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, EndType FROM Code03;

INSERT INTO UnifiedReport (StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, EndType)
SELECT StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, EndType FROM Code04;

INSERT INTO UnifiedReport (StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, Sen1, Sen2, Sen3, Jud1, Jud2, Jud3, TotalJud, EndType)
SELECT StartType, Code, ReportID, ModelID, OPID, ProcID, MaterialID, LotID, Sen1, Sen2, Sen3, Jud1, Jud2, Jud3, TotalJud, EndType FROM Code13;




SELECT * FROM Code01;
SELECT * FROM Code03;
SELECT * FROM Code04;
SELECT * FROM Code13;
SELECT * FROM UnifiedReport;


SELECT * 
FROM UnifiedReport 
WHERE TotalJud = 'Fail';
