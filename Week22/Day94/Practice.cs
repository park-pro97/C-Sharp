//구조체 만들었음(통신 프로토콜 생각하면서 만들어서 그런가 ST ET를 넣었음)
public struct IDReport // Report ID 구조체
{
    //st
    public string reportID;       // "10701" 등 리포트 식별할 고유 코드
    public string reportName; // "IDReport" 등의 리포트 타입 정보
    public string ModelID;
    public string OPID;
    public string ProcID;
    public string MaterialID;
    //et    이렇게 있었으나 뒤에 수정하게 됨
}

public struct StartedReport // Started 구조체
{
    public DateTime Datetime;
    public string reportID;       // "10703" 등 리포트 식별할 고유 코드
    public string reportName; // 리포트 타입
    public string ModelID;
    public string OPID;
    public string ProcID;
    public string MaterialID;
    public string LotID;
}

public struct CanceledReport // Canceled 구조체
{
    public DateTime Datetime;
    public string reportID;       // "10704" 등 리포트 식별할 고유 코드
    public string reportName; // 리포트 타입
    public string ModelID;
    public string OPID;
    public string ProcID;
    public string MaterialID;
    public string LotID;
}

public struct CompletedReport // Completed 구조체
{
    public DateTime Datetime;
    public string reportID;       // "10713" 등 리포트 식별할 고유 코드
    public string reportName;      // 리포트 타입
    public string ModelID;
    public string OPID;
    public string ProcID;
    public string MaterialID;
    public string LotID;
    public double Sen1;        // 센서 1 값 (전압)
    public double Sen2;        // 센서 2 값 (전류)
    public double Sen3;        // 센서 3 값 (열)
    public string Jud1;
    public string Jud2;
    public string Jud3;
    public string TotalJud;
}


public struct StartRcmd
{
    public string Code;
    public string RCMD;
    public string ModelID;
    public string ProcID;
    public string MaterialID;
    public string LotID;
}

public struct CancleRcmd
{
    public string Code;
    public string RCMD;
    public string ModelID;
    public string MaterialID;
    public string CancleCode;
    public string CancleReasonText;
}

// 아래는 테이블 설계하면서 만든 쿼리
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
