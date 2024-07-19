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






using Oracle.ManagedDataAccess.Client;

namespace ConsoleApp16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=9000)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";

            //1. 연결 격체 만들기 - Client
            OracleConnection conn = new OracleConnection(strConn);

            //2.데이터베이스 접속을 위한 연결
            conn.Open();

            //3.서버와 함께 신나게 놀기

            //3.1 Query 명령객체 만들기

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn; //연결객체와 연동

            //3.2 명령하기, 테이블 생성하기
            cmd.CommandText = "CREATE TABLE PERSON " +
                "(ID number(4)   " +
                "NAME varchar(20), " +
                "HP varchar(20))";

            //3.3 쿼리 실행하기
            cmd.ExecuteNonQuery();

            //4. 리소스 반환 및 종료
            conn.Close();

        }
    }
}
