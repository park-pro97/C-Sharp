//직업별 인원 수
SELECT JOB, COUNT(*) FROM EMP
GROUP BY JOB;

-------------------------------------------------------------------------------------------------
//조건 부서 번호 개수
SELECT DEPTNO, COUNT(*) FROM EMP
WHERE DEPTNO = 20
GROUP BY DEPTNO
HAVING COUNT(*) >= 3;


-------------------------------------------------------------------------------------------------
//급여가 2000이상 3000 이하인 사람의 직업과 부서번호 출력(이 문법 중요)
SELECT DEPTNO, JOB, AVG(SAL)
FROM EMP
WHERE SAL <= 3000
GROUP BY DEPTNO, JOB
HAVING AVG(SAL) >=2000;

ORDER BY DEPTNO; -- 마지막에 부서 번호 순으로 정렬시키기
-------------------------------------------------------------------------------------------------
//JOIN
SELECT *
FROM EMP E, DEPT D
WHERE E.DEPTNO = D.DEPTNO;


-------------------------------------------------------------------------------------------------
//비등가조인
SELECT *
FROM EMP E, SALGRADE S
WHERE E.SAL BETWEEN S.LOSAL AND S.HISAL;


-------------------------------------------------------------------------------------------------
//자체조인(실습 8-8)
SELECT E1.EMPNO, E1.ENAME, E1.MGR, E2.EMPNO, E2.ENAME "매니저"
FROM EMP E1, EMP E2
WHERE E1.MGR = E2.MGR;


-------------------------------------------------------------------------------------------------
//자체조인 같은 감독의 다른 영화를 찾아라
--영화제목, 감독
SELECT  M1.DIRECTOR, M1.TITLE
FROM MOVIES M1, MOVIES M2
WHERE M1.DIRECTOR = M2.DIRECTOR
AND M1.TITLE != M2.TITLE;


-------------------------------------------------------------------------------------------------
//등가 조인
SELECT E1.EMPNO, E1.ENAME, E1.MGR, E2.EMPNO, E2.ENAME
FROM EMP E1, EMP E2
WHERE E1.MGR = E2.MGR(+);


-------------------------------------------------------------------------------------------------
//LEFT OUTER JOIN
SELECT E1.EMPNO, E1.ENAME, E1.MGR, E2.EMPNO, E2.ENAME
FROM EMP E1, EMP E2
WHERE E1.MGR = E2.MGR(+);


-------------------------------------------------------------------------------------------------
//RIGHT OUTER JOIN
SELECT E1.EMPNO, E1.ENAME, E1.MGR, E2.EMPNO, E2.ENAME
FROM EMP E1, EMP E2
WHERE E1.MGR(+) = E2.MGR;


-------------------------------------------------------------------------------------------------
//SQL LEFT OUTER JOIN
SELECT E1.EMPNO, E1.ENAME, E1.MGR,
        E2.EMPNO AS MGR_EMPNO,
        E2.ENAME AS MGR_ENAME
FROM EMP E1 LEFT OUTER JOIN EMP E2 ON (E1.MGR = E2.EMPNO)
ORDER BY E1.EMPNO;


-------------------------------------------------------------------------------------------------
//SQL RIGHT OUTER JOIN
SELECT E1.EMPNO, E1.ENAME, E1.MGR,
        E2.EMPNO AS MGR_EMPNO,
        E2.ENAME AS MGR_ENAME
FROM EMP E1 RIGHT OUTER JOIN EMP E2 ON (E1.MGR = E2.EMPNO)
ORDER BY E1.EMPNO;


-------------------------------------------------------------------------------------------------
//퀴즈 1번
SELECT DEPTNO, D.DNAME, E.EMPNO, E.ENAME, E.SAL
FROM EMP E JOIN DEPT D USING(DEPTNO)
WHERE E.SAL > 2000;


-------------------------------------------------------------------------------------------------
//퀴즈2번
SELECT D.DEPTNO, D.DNAME, 
       TRUNC(AVG(E.SAL), 0) AS AVG_SAL, 
       MAX(E.SAL) AS MAX_SAL, 
       MIN(E.SAL) AS MIN_SAL,
       COUNT(*) AS CNT
FROM DEPT D, EMP E
WHERE D.DEPTNO = E.DEPTNO
GROUP BY D.DEPTNO, D.DNAME;


-------------------------------------------------------------------------------------------------
//SELECT D.DEPTNO,D.DNAME,E.EMPNO,E.ENAME,E.JOB,E.SAL
FROM DEPT D, EMP E
WHERE D.DEPTNO = E.DEPTNO ORDER BY DEPTNO,ENAME;


-------------------------------------------------------------------------------------------------
//4번
SELECT D.DEPTNO,D.DNAME,E.EMPNO,E.ENAME,E.MGR,E.SAL,E.DEPTNO,
S.LOSAL,S.HISAL,S.GRADE,E2.EMPNO AS MGR_EMPNO,
E2.ENAME AS MGR_ENAME
FROM DEPT D,EMP E,SALGRADE S,EMP E2
WHERE D.DEPTNO(+) = E.DEPTNO
AND E.SAL BETWEEN S.LOSAL (+) AND S.HISAL(+)
 AND E.MGR = E2.EMPNO(+)
ORDER BY D.DEPTNO,E.DEPTNO;


-------------------------------------------------------------------------------------------------
//서브쿼리
SELECT ENAME
FROM EMP
WHERE COMM > (SELECT COMM
              FROM EMP
              WHERE ENAME = 'ALLEN');


-------------------------------------------------------------------------------------------------
//서브쿼리2             
SELECT ENAME
FROM EMP
WHERE DEPTNO = 20
AND SAL > (SELECT AVG(SAL)
           FROM EMP);


-------------------------------------------------------------------------------------------------
//사원 이름이 JONES인 사원의 급여
SELECT SAL FROM EMP
WHERE ENAME = 'JONES';

-------------------------------------------------------------------------------------------------
//서브 쿼리를 사용해서 EMP 테이블의 사원 정보 중에서 이름이 'ALLEN'인 사원의
--추가수당(COMM)보다 많이 받는 사원의 이름을 출력
SELECT ENAME, COMM FROM EMP
WHERE COMM > (SELECT COMM FROM EMP WHERE ENAME = 'ALLEN');


-------------------------------------------------------------------------------------------------
//2015년에 개봉한 드라마 장르의 영화의 제목
SELECT TITLE, GENRE, RELEASE_DATE FROM MOVIES
WHERE GENRE = '드라마' AND SUBSTR(RELEASE_DATE,1,2) = '15'


-------------------------------------------------------------------------------------------------
//모든 직원들의 평균 급여보다 많이 받는 20번 부서 직원들을 출력하라.
SELECT E.EMPNO, E.ENAME, E.JOB, E.SAL,
        D.DEPTNO, D.DNAME, D.LOC
FROM EMP E, DEPT D
WHERE E.DEPTNO = D.DEPTNO
    AND E.DEPTNO = 20
    AND E.SAL > (SELECT AVG(SAL) FROM EMP);

-------------------------------------------------------------------------------------------------
//IN 연산자 사용
SELECT * FROM EMP
WHERE DEPTNO IN (20, 30);


-------------------------------------------------------------------------------------------------
//ANY
SELECT * FROM EMP
WHERE SAL = ANY(SELECT MAX(SAL) FROM EMP GROUP BY DEPTNO);

-------------------------------------------------------------------------------------------------
//ALL
SELECT * FROM EMP
WHERE SAL < ALL(SELECT SAL FROM EMP WHERE DEPTNO = 30);


-------------------------------------------------------------------------------------------------
//삽입(테이블에)
INSERT INTO DEPT_TEMP(DEPTNO, DNAME, LOC)
VALUES(50, 'DATABASE', 'SEOUL');
using Oracle.ManagedDataAccess.Client;

-------------------------------------------------------------------------------------------------
//UPDATE 수정
UPDATE DEPT_TEMP2
SET LOC = 'SEOUL'
WHERE DEPTNO = 10;


-------------------------------------------------------------------------------------------------
//ROLLBACK;--되돌림 커밋 전이면.


-------------------------------------------------------------------------------------------------
//테이블 삭제
DROP TABLE EMP_TEMP;


-------------------------------------------------------------------------------------------------
//테이블 복사 생성
CREATE TABLE EMP_TEMP
AS SELECT * FROM EMP;


-------------------------------------------------------------------------------------------------
//12장 시작--ALTER TABLE
CREATE TABLE EMP_ALTER
 AS SELECT * FROM EMP;
 
 SELECT * FROM EMP EMP_ALTER;


-------------------------------------------------------------------------------------------------
//컬럼 추가
ALTER TABLE EMP_ALTER
ADD HP VARCHAR2(20);

-------------------------------------------------------------------------------------------------
//컬럼 이름 변경 RENAME COLUMN
ALTER TABLE EMP_ALTER
RENAME COLUMN HP TO TEL;
DESC EMP_ALTER;

-------------------------------------------------------------------------------------------------
//컬럼 TYPE(자료형)DMF QUSRUD MODIFY
ALTER TABLE EMP_ALTER
MODIFY EMPNO NUBER(6);


-------------------------------------------------------------------------------------------------
//컬럼을 삭제 DROP COLUMN
ALTER TABLE EMP_ALTER
DROP COLUMN TEL;


-------------------------------------------------------------------------------------------------
//테이블 이름 변경 RENAME [COLUMN은 RENAME뒤에 COLUMN 적었는데 테이블은 X]
RENAME EMP_ALTER TO EMP_RENAME;


-------------------------------------------------------------------------------------------------
//테이블의 데이터를 삭제하는 TRUNCATE --잘 안 쓰임 있다는 것만 알아두삼
TRUNCATE TABLE EMP_RENAME;


-------------------------------------------------------------------------------------------------
//SQL DEVELOPER를 사용하지 않고 VISUAL STUDIO에서 ORACLE 설치하고 코드로 DB에 접속하여
테이블을 만들어 보았다.
-------------------------------------------------------------------------------------------------
//PERSON 테이블 있으니까 접속 C#으로
OracleCommand cmd = new OracleCommand();
cmd.Connection = conn;

cmd.CommandText = "CREATE TABLE PERSON" +
    "(ID number(4), " +
    "NAME varchar(20), " +
    "TEL varchar(20))";
cmd.ExecuteNonQuery();


-------------------------------------------------------------------------------------------------
//테이블에 자료를 넣어본 예시 코드
using Oracle.ManagedDataAccess.Client;

namespace _20240719_OracleTableAppCreate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=SCOTT;Password=TIGER;";
            // 연결 객체
            OracleConnection conn = new OracleConnection(strConn);
            // 데이터베이스 접속 연결
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO PERSON (ID, NAME, TEL) " +
                              "VALUES (:ID, :NAME, :TEL)";

            cmd.Parameters.Add(new OracleParameter("ID", 1));
            cmd.Parameters.Add(new OracleParameter("NAME", "홍길동"));
            cmd.Parameters.Add(new OracleParameter("TEL", "010-1111-1111"));

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}   
