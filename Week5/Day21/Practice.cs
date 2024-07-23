//실습 14-1 제약   중복되지 않는 값   UNIQUE
DROP TABLE TABLE_UNIQUE; --테이블 삭제하는 문장
CREATE TABLE TABLE_UNIQUE(
    LOGIN_ID VARCHAR2(20) UNIQUE,
    LOGIN_PWD VARCHAR(20) NOT NULL,
    TEL VARCHAR(20)
);

SELECT * FROM TABLE_UNIQUE;
DESC TABLE_UNIQUE;


---------------------------------------------------------------------------
//4-15 제약 조건 확인(테이블의 제약조건만 확인)
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME
FROM USER_CONSTRAINTS
WHERE TABLE_NAME = 'TABLE_UNIQUE';

---------------------------------------------------------------------------
//14-16 TABLE_UNIQUE 테이블에 데이터 입력하기

INSERT INTO TABLE_UNIQUE(LIGIN_ID, LOGIN_PWD, TEL)
VALUES ('TEST_ID_01', 'PWD01', '010-1234-5678');

CREATE TABLE TABLE_UNIQUE2(
    LOGIN_ID VARCHAR2(20) CONSTRAINT TBLUNQ2_LGNID_UNQ UNIQUE, --CONSTRAINT는 내가 제약의 이름을 만듦
    LOGIN_PWD VARCHAR(20) CONSTRAINT TBLUNQ2_LGNPW_NN NULL,
    TEL VARCHAR(20)
);
---------------------------------------------------------------------------
//TABLE_UNIQUE가 들어간 애들 출력인듯
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME
FROM USER_CONSTRAINTS
WHERE TABLE_NAME LIKE 'TABLE_UNIQUE%';


---------------------------------------------------------------------------
//제약 조건 삭제하기
ALTER TABLE TABLE_UNIQUE2
DROP CONSTRAINT TBLUNQ2_LGNPW_NN;

---------------------------------------------------------------------------
//14-29  실습대로 하면 PK만 나와야 하는데 어제 PK테이블을 만들었기에
-- PK1로 테이블 이름을 만들었고, 그래서 PK, PK1 같은 값이 출력됐다.
CREATE TABLE TABLE_PK1(
    LOGIN_ID VARCHAR(20) PRIMARY KEY,
    LOGIN_PWD VARCHAR(20) NOT NULL,
    TEL VARCHAR(20)
);

---------------------------------------------------------------------------
//특정 테이블의 제약 조건을 조회
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME
FROM USER_CONSTRAINTS
WHERE TABLE_NAME LIKE 'TABLE_PK%';
---------------------------------------------------------------------------
//INDEX 확인
SELECT INDEX_NAME, TABLE_OWNER, TABLE_NAME
FROM USER_INDEXES
WHERE TABLE_NAME LIKE 'TABLE_PK%';

---------------------------------------------------------------------------
//14-32 제약에 이름을 넣자
CREATE TABLE TABLE_PK2(
    LOGIN_ID VARCHAR2(20) CONSTRAINT TBLPK2_LGNID_PK PRIMARY KEY,
    LOGIN_PWD VARCHAR(20) CONSTRAINT TBLPK2_LGNPW_NN NOT NULL,
    TEL VARCHAR2(20)
);
DESC TABLE_PK2;

---------------------------------------------------------------------------
//삽입 14-33
INSERT INTO TABLE_PK
VALUES ('TEST_ID_01', 'PWD01', '010-1234-5678');

SELECT * FROM TABLE_PK;

---------------------------------------------------------------------------
//테이블 작성 방법 [요 방법도 있음]
CREATE TABLE TABLE_PK1(
    LOGIN_ID VARCHAR(20),
    LOGIN_PWD VARCHAR(20,
    TEL VARCHAR(20),
    PRIMARY KEY(LOGIN_ID)
);

---------------------------------------------------------------------------
//포리이인키ㅣㅣㅣㅣㅣ 중요함

//교재 14-37  반드시 해보고 익히라고 하심
-- 테이블 간의 제약조건 살펴보기
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME, R_OWNER
FROM USER_CONSTRAINTS
WHERE TABLE_NAME IN ('EMP', 'DEPT'); -- 위에 SELECT 중에서 EMP, DEPT 있으면 출력

---------------------------------------------------------------------------
//--테이블 간의 관계 연관(R) 만들기
--14-29
-- 테이블 간의 관계 연관(R) 만들기
DROP TABLE DEPT_FK;
CREATE TABLE DEPT_FK(
    DEPTNO      NUMBER(2) PRIMARY KEY,
    DNAME       VARCHAR2(14),
    LOC         VARCHAR2(13)
);

---------------------------------------------------------------------------
//-- 기존 DEPT_FK 테이블 삭제
DROP TABLE DEPT_FK;

-- DEPT_FK 테이블 생성
CREATE TABLE DEPT_FK(
    DEPTNO      NUMBER(2) PRIMARY KEY,
    DNAME       VARCHAR2(14),
    LOC         VARCHAR2(13)
);

-- DEPT_FK 테이블 데이터 확인
SELECT * FROM DEPT_FK;

-- DEPT_FK 테이블에 데이터 삽입
INSERT INTO DEPT_FK
VALUES (10, '개발1팀', '안동');

-- DEPT_FK 테이블에서 데이터 삭제
DELETE FROM DEPT_FK WHERE DEPTNO = 10;

---------------------------------------------------------------------------
//DROP TABLE EMP_FK;

-- EMP_FK 테이블 생성 (DEPT_FK 테이블과 외래 키 관계 설정)
CREATE TABLE EMP_FK(
    EMPNO       NUMBER(4) PRIMARY KEY,
    ENAME       VARCHAR2(10),
    DEPTNO      NUMBER(2) REFERENCES DEPT_FK(DEPTNO) ON DELETE CASCADE
);

-- EMP_FK 테이블 데이터 확인
SELECT * FROM EMP_FK;

-- EMP_FK 테이블에 데이터 삽입
INSERT INTO EMP_FK
VALUES (3, '이순신', 10);

-- EMP_FK 테이블에서 데이터 삭제
DELETE FROM EMP_FK WHERE DEPTNO = 10;

---------------------------------------------------------------------------
//14-45 테이블 생성 시 CHECK 제약 조건 설정하기
CREATE TABLE TABLE_CHECK(
    LOGIN_ID    VARCHAR2(20) PRIMARY KEY,
    LOGIN_PWD   VARCHAR2(20) CHECK (LENGTH(LOGIN_PWD) > 3),
    TEL         VARCHAR2(20)
);

---------------------------------------------------------------------------
//위에 제약을 걸어두 비번 4자리부터 되게 그래서 123은 안 먹었고 1234는 먹었음
INSERT INTO TABLE_CHECK
VALUES ('TEST_ID', '1234', '010-1234-5678');

---------------------------------------------------------------------------
//기본 값을 정하는 DEFAULT 14-49
DROP TABLE TABLE_DEFAULT;
CREATE TABLE TABLE_DEFAULT(
    LOGIN_ID VARCHAR2(20) PRIMARY KEY,
    LOGIN_PW VARCHAR2(20) DEFAULT '1234'
);
SELECT * FROM TABLE_DEFAULT;
INSERT INTO TABLE_DEFAULT
VALUES ('TEST_ID_01', 'ABCD');


INSERT INTO TABLE_DEFAULT
VALUES ('TEST_ID_02', NULL);


---------------------------------------------------------------------------
//394P NO.1
CREATE TABLE DEPT_CONST(
    DEPTNO  NUBMER(2) CONSTRAINT DEPTCONST_DEPTNO_PK PRIMARY KEY,
    DNAME   VARCHAR(14) CONSTRAINT DEPTCONST_DNAME UNIQUE,
    LOC     VARCHAR2(13) CONSTRAINT DEPTCONST_LOC_NN NOT NULL
);


---------------------------------------------------------------------------
//


