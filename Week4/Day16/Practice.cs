SQL

//FIRST_NAME, SALARY 출력(SELECT문)
SELECT FIRST_NAME, SALARY FROM EMPLOYEES;
SELECT LAST_NAME, HIRE_DATE FROM EMPLOYEES;

SELECT LAST_NAME, HIRE_DATE FROM EMPLOYEES ORDER BY LAST_NAME;


-----------------------------------------------------------------------------------------
//(SELCT~FROM)
SELECT EMPNO, ENAME, DEPTNO
    FROM EMP;


-----------------------------------------------------------------------------------------
//중복이 있는 경우 DISTINCT
--중복이 있는 경우
SELECT DISTINCT JOB, DEPTNO FROM EMP;
--연산식 사용
SELECT ENAME, SAL, SAL * 12, COMM FROM EMP;


-----------------------------------------------------------------------------------------
//AS, ORDER BY
SELECT ENAME, SAL 월급, (SAL+SAL+SAL+SAL+COMM) AS FOURMONTHSAL, COMM FROM EMP;


-----------------------------------------------------------------------------------------
//이름(ENAME), 급여(SAL), 12개월급여(SAL*12) 검색하기
SELECT ENAME 이름, SAL 급여, (SAL*12) 일년급여 FROM EMP
ORDER BY SAL DESC;

SELECT * FROM EMP ORDER BY DEPTNO ASC, SAL DESC;


-----------------------------------------------------------------------------------------
//정렬
SELECT * FROM EMP;
SELECT DEPTNO FROM EMP;
-- 중복 제거 + 오름차순 정렬
SELECT DISTINCT DEPTNO FROM EMP
ORDER BY DEPTNO;
-- 급여가 높은 순으로 이름과 급여 출력, 정렬은 급여를 기준으로 내림차순
SELECT ENAME, SAL FROM EMP ORDER BY SAL DESC;


-----------------------------------------------------------------------------------------
//테이블 만들기
--테이블 삭제
DROP TABLE EMP_DDL;
--테이블 생성
CREATE TABLE EMP_DDL
(
    EMPNO   NUMBER(4),
    ENAME   VARCHAR2(10),
    JOB     VARCHAR2(9)
);
--검색
SELECT * FROM EMP_DDL;
--삽입
INSERT INTO EMP_DDL VALUES (1, 'TOM', 'MANAGER');
INSERT INTO EMP_DDL VALUES (2, '홍길동', '대리');


-----------------------------------------------------------------------------------------
//테이블로 주소록 만들기(C# 콘솔 앱으로 만들던 DB의 정보 기입을 쉽게 가능함)
DROP TABLE STUDENT;
CREATE TABLE STUDENT
(
    ID          NUMBER(4),
    NAME        VARCHAR2(20),
    HP          VARCHAR2(13),
    LOCATION    VARCHAR2(50)
);
DESC STUDENT;
SELECT * FROM STUDENT;

INSERT INTO STUDENT (ID, NAME, HP, LOCATION)
VALUES (1, '홍길동', '010-1111-1111', '안동');
INSERT INTO STUDENT (ID, NAME, HP, LOCATION)
VALUES (2, '이순신', '010-2222-2222', '대구');


-----------------------------------------------------------------------------------------
//WHERE절과 연산자
--EMP테이블에서 부서번호가 30인 데이터만 호출해 봅시다.
SELECT * FROM EMP
WHERE DEPTNO = 30;
--직업이 매니저인 데이터만 호출
SELECT * FROM EMP
WHERE JOB = 'MANAGER';
--직업이 매니저면서 부서번호가 30인 사원을 출력
SELECT * FROM EMP
WHERE DEPTNO = 30 AND JOB = 'MANAGER';
--부서번호가 30번인 모든 사람과 JOB이 'CLERK'인 모든 사람을 출력
SELECT * FROM EMP
WHERE DEPTNO = 30 OR JOB = 'CLERK';
--산술연산
SELECT * FROM EMP
WHERE SAL * 12 = 36000;
--급여가 3000$ 이상인 직원을 모두 출력
SELECT * FROM EMP WHERE SAL >= 3000;
--급여가 2500 이상이고 직업이 'ANALYST'인 사람은?
SELECT * FROM EMP
WHERE SAL >= 2500 AND JOB = 'ANALYST';


-----------------------------------------------------------------------------------------
//급여가 3000이 아닌 모든 데이터 추출
SELECT * FROM EMP WHERE SAL != 3000;
SELECT * FROM EMP WHERE SAL <> 3000;
SELECT * FROM EMP WHERE SAL ^= 3000;
SELECT * FROM EMP WHERE NOT SAL = 3000;

// !!! @@가 아닌 값들을 전부 출력하려면 네 가지 표현이 존재하는데 보통 느낌표를 제일 많이 사용한다(1번째) !!!


-----------------------------------------------------------------------------------------
//IN과 BETWEEN
-- IN 연산자
SELECT * FROM EMP
WHERE JOB = 'MANAGER' OR JOB = 'SALESMAN';

SELECT * FROM EMP
WHERE JOB IN ('MANAGER', 'SALESMAN');
SELECT * FROM EMP
WHERE JOB NOT IN ('MANAGER','SALESMAN');


-----------------------------------------------------------------------------------------
//BETWEEN 연산자
-- BETWEEN 연산자
SELECT * FROM EMP WHERE SAL >= 2000 AND SAL <= 3000;
SELECT * FROM EMP WHERE SAL BETWEEN 2000 AND 3000;


-----------------------------------------------------------------------------------------
//오라클 접속(콘솔앱으로 모듈 nu get 받고난 후
//도구 - Nuget 패키지 관리에서 패키지 관리자 콘솔 실행
//Oracle.ManagedDataAccess.Core를 솔루션에 설치 (안정적인 23.5 버전) 사용하기 위하여 각 프로그램마다 설정해줘야함
//연결 스크립트 불러오고
//연결 객체 만들고( Client )
//DB접속을 위한 연결
// 리소스 반환 및 종료
using Oracle.ManagedDataAccess.Client;

namespace OracleQueryTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 연결 스크립트
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +  //나는 여기 포트 9000
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=SCOTT;Password=TIGER;";

            // 1. 연결 객체 만들기 - 작성 시 위에 Nuget으로 받은 모델이 적용됨을 확인 가능
            OracleConnection conn = new OracleConnection(strConn);

            // 2. 데이터베이스 접속을 위한 연결 후 서버 가동 완료
            conn.Open();

            // 3. 리소스 반환 및 종료
            conn.Close();
        }
    }
}


-----------------------------------------------------------------------------------------
