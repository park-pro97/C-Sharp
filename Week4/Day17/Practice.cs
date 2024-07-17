@@오늘도 SQL


//부서 번호가 20인 사람 다 출력
SELECT * FROM EMP
WHERE DEPTNO = 20 OR DEPTNO = 10;


----------------------------------------------------------------------------------------------
//EMP 테이블에서 급여가 1500 이상 3000인 직원정보
SELECT * FROM EMP
WHERE SAL BETWEEN 1500 AND 3000;


----------------------------------------------------------------------------------------------
//조회
SELECT ENAME "이름", SAL "월급" FROM EMP;


----------------------------------------------------------------------------------------------
//전체 직원 중에 'CLERK', 'SALESMAN'이 아닌 모든 직원 데이터를 출력
SELECT * FROM EMP
WHERE JOB <>'CLERK' -- <>RHK !=은 같은 표현 ~~가 닌
AND JOB != 'SALESMAN';

[한줄로 SELECT * FROM EMP WHERE JOB NOT IN ('CLERK', 'SALESMAN'); <- 이렇게 해도 된다.
----------------------------------------------------------------------------------------------
//이름이 S로 시작하는 데이터를 출력
SELECT * FROM EMP WHERE ENAME LIKE 'S%';


----------------------------------------------------------------------------------------------
//이름이 S로 끝나는 데이터를 출력
SELECT * FROM EMP WHERE ENAME LIKE '%S';


----------------------------------------------------------------------------------------------
//이름이 S이 들어가는 데이터를 출력
SELECT * FROM EMP WHERE ENAME LIKE '%S%';

 
----------------------------------------------------------------------------------------------
//이름에 'AM' 글자가 들어가는 데이터를 출력
SELECT * FROM EMP WHERE ENAME LIKE'%AM%';

 
----------------------------------------------------------------------------------------------
//이름에 'AM' 글자가 안 들어가는 데이터를 출력
SELECT * FROM EMP WHERE ENAME NOT LIKE'%AM%';

 
----------------------------------------------------------------------------------------------
//COMM이 NULL인 데이터만 출력
SELECT * FROM EMP WHERE COMM IS NULL;

 
----------------------------------------------------------------------------------------------
//상관이 없는 직원(NGR이) NULL인 즉, 사장을 출력
SELECT * FROM EMP WHERE MGR IS NULL;

 
----------------------------------------------------------------------------------------------
//실습 5-29 OR연산자와 IS NULL 같이 사용
SELECT * FROM EMP WHERE SAL > NULL
OR COMM IS NULL;

----------------------------------------------------------------------------------------------
//오라클 내장함수와 함께 사용 [대문자, 소문자, 젤앞글자만 대문자]
SELECT ENAME, UPPER(ENAME), LOWER(ENAME), INITCAP(ENAME)
FROM EMP;

 
----------------------------------------------------------------------------------------------
//이름 비교 (대소문자 구문 없이 킹 출력)
SELECT * FROM EMP WHERE UPPER(ENAME) = UPPER('king');

 
----------------------------------------------------------------------------------------------
//EMPNO, ENAME, SAL, DEPTNO 부서번호 10 데이터 출력
SELECT EMPNO, ENAME, SAL, DEPTNO
FROM EMP
WHERE DEPTNO = 10;


----------------------------------------------------------------------------------------------
//부서번호 10,20 같이 출력 (잡합 사용)
SELECT EMPNO, ENAME, SAL, DEPTNO
FROM EMP
WHERE DEPTNO = 10
UNION -- 집합 (컬럼의 개수가 같아야 동작)
SELECT EMPNO, ENAME, SAL, DEPTNO
FROM EMP
WHERE DEPTNO = 20;

 
----------------------------------------------------------------------------------------------
//차집합(DEPTNO 10번 빼고)
SELECT EMPNO, ENAME, SAL, DEPTNO FROM EMP
MINUS
SELECT EMPNO, ENAME, SAL, DEPTNO FROM EMP
WHERE DEPTNO = 10;

 
----------------------------------------------------------------------------------------------
//교집합
SELECT EMPNO, ENAME, SAL, DEPTNO FROM EMP
INTERSECT
SELECT EMPNO, ENAME, SAL, DEPTNO FROM EMP
WHERE DEPTNO = 10;


----------------------------------------------------------------------------------------------
//5장연습문제
1번
SELECT * FROM EMP WHERE ENAME LIKE '%S';

2번
SELECT EMPNO, ENAME, JOB, SAL, DEPTNO FROM EMP
WHERE DEPTNO = 30 AND JOB = 'SALESMAN';

3-1번
집합 연산자 사용X
SELECT EMPNO, ENAME, SAL, DEPTNO FROM EMP
WHERE DEPTNO IN (20,30) AND SAL > 2000;
3-2번
집합 연산자 사용
SELECT EMPNO, ENAME, SAL, DEPTNO
FROM EMP
WHERE SAL > 2000 AND DEPTNO = '20'
UNION
SELECT EMPNO, ENAME, SAL, DEPTNO
FROM EMP
WHERE SAL > 2000 AND DEPTNO = '30';

4번
SELECT * FROM EMP
WHERE NOT SAL >= 2000 AND SAL <=3000;
 
5번
SELECT ENAME,EMPNO,SAL,DEPTNO FROM EMP
WHERE ENAME LIKE '%E%' AND
DEPTNO = 30 AND
SAL NOT BETWEEN 1000 AND 2000;
 
6번
SELECT * FROM EMP
WHERE COMM IS NULL AND JOB IN('CLERK','MANAGER')
AND ENAME NOT LIKE '_L%';

----------------------------------------------------------------------------------------------
//LENGTH연산자와 DUAL
SELECT LENGTH('홍길동') AS 문자열길이,
       LENGTHB('홍길동') AS 문자열크기
FROM DUAL;
SELECT 12 + 5 FROM DUAL;

 
----------------------------------------------------------------------------------------------
//문자열 추출 연산자 SUBSTRING
SELECT JOB, SUBSTRING(JOB, 1, 2) FROM EMP;


----------------------------------------------------------------------------------------------
//주민번호 조합
SELECT '080808-3454577',
SUBSTR('080808-3454577',1,2) 출생년도,
SUBSTR('080808-3454577',3,4) 생일,
SUBSTR('080808-3454577',8,1) 성별
FROM DUAL;


----------------------------------------------------------------------------------------------
//마이너스 표현
SELECT JOB,
    SUBSTR(JOB, -LENGTH(JOB)),
    SUBSTR(JOB, -LENGTH(JOB), 2),
    SUBSTR(JOB, -3)
FROM EMP;


----------------------------------------------------------------------------------------------
//INSTR와 REPLACE 함수
SELECT INSTR('HELLO ORACLE', 'L') FROM DUAL;
SELECT INSTR('HELLO ORACLE', 'L', 5) FROM DUAL;
SELECT INSTR('HELLO ORACLE', 'L', 2, 2) FROM DUAL;
SELECT INSTR('홍길동이 홍길동을 홍길동하다', '길', 3) FROM DUAL;

----------------------------------------------------------------------------------------------
//REPLACE 수정과 교환 기능 함수
SELECT '010-1234-5678',
        REPLACE('010-1234-5678', '-', ' '),
        REPLACE('010-1234-5678', '-')
FROM DUAL;


----------------------------------------------------------------------------------------------
//주민번호 991225-1234567, 생일추출, 13자리로 표현, 앞자리 뒷자리 사이 공백 표현
SELECT SUBSTR('991225-1234567', 3, 4) 생일추출,
REPLACE('991225-1234567', '-') 열세자리표현,
REPLACE('991225-1234567', '-', ' ') 앞뒷공백
FROM DUAL;


----------------------------------------------------------------------------------------------
//6장 연습문제
1번
SELECT EMPNO,RPAD(SUBSTR(EMPNO,-LENGTH(EMPNO),2),4,'*')AS MASKING_EMPNO,
ENAME,RPAD(SUBSTR(ENAME,-LENGTH(ENAME),1),5,'*')AS MASKING_ENAME
FROM EMP WHERE LENGTH(ENAME) >= 5 AND LENGTH(ENAME) < 6;
 
2번
SELECT EMPNO,ENAME,SAL,TRUNC((SAL/21.5),2)AS DAY_PAY,
ROUND(((SAL/21.5)/8),1)AS TIME_PAY
FROM EMP;
 
3번
SELECT EMPNO,ENAME,TO_CHAR(HIREDATE,'YYYY/MM/DD')AS HIREDATE,
TO_CHAR(NEXT_DAY(ADD_MONTHS(HIREDATE,3),'월요일'),'YYYY-MM-DD')AS R_JOB,
NVL(TO_CHAR(COMM),'N/A')AS COMM
FROM EMP;
 
4번
SELECT EMPNO,ENAME,MGR,
CASE WHEN MGR IS NULL THEN 0000
WHEN SUBSTR(MGR,-LENGTH(MGR),2)='75' THEN 5555
WHEN SUBSTR(MGR,-LENGTH(MGR),2)='76' THEN 6666
WHEN SUBSTR(MGR,-LENGTH(MGR),2)='77' THEN 7777
WHEN SUBSTR(MGR,-LENGTH(MGR),2)='78' THEN 8888
ELSE MGR END AS CHG_MGR FROM EMP;


----------------------------------------------------------------------------------------------
//ROUND와 TRUNC 함수
-- ROUND 함수
SELECT 31.41592, ROUND(31.41592, 0)
FROM DUAL;

-- TRUNC 함수 = 특정 자리에서 버림
SELECT TRUNC(1234.5678),
       TRUNC(1234.5678, 0),
       TRUNC(1234.5678, 1),
       TRUNC(1234.5678, 2),
       TRUNC(1234.5678, -1),
       TRUNC(1234.5678, -2)
FROM DUAL;

----------------------------------------------------------------------------------------------
//CEIL, FLOOR, MOD 함수
-- CEIL, FLOOR
SELECT CEIL(3.14), FLOOR(3.14) FROM DUAL;
-- MOD
SELECT MOD(15,6) FROM DUAL;

----------------------------------------------------------------------------------------------
//날짜 데이터 함수
-- 현재 날짜 SYSDATE
SELECT SYSDATE,
       SYSDATE - 1 AS "어제",
       SYSDATE + 1 AS "내일"
FROM DUAL;

-- 이후 몇 개월 후의 날짜
SELECT SYSDATE, ADD_MONTHS(SYSDATE, 2),
                ADD_MONTHS(SYSDATE, 120) AS "입사10년" FROM DUAL;
-- 마지막 날짜, 다음 날짜 구하기
SELECT SYSDATE,
    NEXT_DAY(SYSDATE, '월요일'),
    LAST_DAY(SYSDATE),
    LAST_DAY(TO_DATE('24/02/10'))
FROM DUAL;

----------------------------------------------------------------------------------------------
//HIREDATE와 SYSDATE 사이 개월수
SELECT EMPNO, ENAME, HIREDATE, SYSDATE,
    MONTHS_BETWEEN(HIREDATE, SYSDATE) AS MONTHS1,
    MONTHS_BETWEEN(SYSDATE, HIREDATE) AS MONTHS2,
    TRUNC(MONTHS_BETWEEN(SYSDATE, HIREDATE)) AS MONTHS3
FROM EMP;


----------------------------------------------------------------------------------------------
//ROUND 함수 사용하여 날짜 데이터 출력
SELECT SYSDATE,
    ROUND(SYSDATE, 'CC') AS FORMAT_CC,
    ROUND(SYSDATE, 'YYYY') AS FORMAT_YYYY,
    ROUND(SYSDATE, 'Q') AS FORMAT_Q,
    ROUND(SYSDATE, 'DDD') AS FORMAT_DDD,
    ROUND(SYSDATE, 'HH') AS FORMAT_HH
FROM DUAL;


----------------------------------------------------------------------------------------------
//TRUNC 함수 사용하여 날짜 데이터 출력
SELECT SYSDATE,
    TRUNC(SYSDATE, 'CC') AS FORMAT_CC,
    TRUNC(SYSDATE, 'YYYY') AS FORMAT_YYYY,
    TRUNC(SYSDATE, 'Q') AS FORMAT_Q,
    TRUNC(SYSDATE, 'DDD') AS FORMAT_DDD,
    TRUNC(SYSDATE, 'HH') AS FORMAT_HH
FROM DUAL;


----------------------------------------------------------------------------------------------
//OPTION일 구하기 1
SELECT
    NEXT_DAY(NEXT_DAY('24/09/01', '목요일'),'목요일')
FROM DUAL;

-- OPTION일 구하기 2
SELECT
    NEXT_DAY(TRUNC(TO_DATE('24/09/01', 'YY/MM/DD'), 'MM') + 6, '목요일')
FROM DUAL;


----------------------------------------------------------------------------------------------
//오라클의 자료형
SELECT EMPNO, ENAME, EMPNO+'500'
FROM EMP;

SELECT 'ABCD' + EMPNO FROM EMP;


----------------------------------------------------------------------------------------------
//날짜를 문자로 변환 TO_CHAR
SELECT SYSDATE, TO_CHAR(SYSDATE), TO_CHAR(SYSDATE, 'YYYY/MM/DD HH24:MI:SS') 
AS 현재시간 FROM DUAL;


----------------------------------------------------------------------------------------------
//ORACLE 포맷 날짜 표시 형식
SELECT SYSDATE,
    TO_CHAR(SYSDATE),
    TO_CHAR(SYSDATE, 'YYYY/MM/DD HH24:MI:SS'),
    TO_CHAR(SYSDATE, 'MM') AS "월",
    TO_CHAR(SYSDATE, 'MON') AS "MON",
    TO_CHAR(SYSDATE, 'MONTH') AS "MONTH",
    TO_CHAR(SYSDATE, 'DD') AS "DD",
    TO_CHAR(SYSDATE, 'DY') AS "DY",
    TO_CHAR(SYSDATE, 'DAY') AS "DAY"
FROM DUAL;

----------------------------------------------------------------------------------------------
//TO_CHAR을 활용한 통화형식 지정
SELECT SAL,
    TO_CHAR(SAL, '$999,999') AS "달러",
    TO_CHAR(SAL, 'L999,999') AS "원화",
    TO_CHAR(SAL, '$999,999.00') AS "원화+소수점",
    TO_CHAR(SAL, '999,999.00') AS "SAL_1",
    TO_CHAR(SAL, '000,999,999.00') AS "SAL_2"
FROM EMP;


----------------------------------------------------------------------------------------------
//문자 데이터 처리
SELECT 1300 - '1500' FROM DUAL;
SELECT TO_NUMBER('1,300', '999,999') - TO_NUMBER('1,500', '999,999') FROM DUAL;


----------------------------------------------------------------------------------------------
//TO_DATE 함수
SELECT TO_DATE('2018-07-04', 'YYYY-MM-DD') FROM DUAL;
[TO_DATE('2018-07-04', 'RR-MM-DD') FROM DUAL;]  ---> 이렇게도 가능

----------------------------------------------------------------------------------------------
//NULL 처리 함수
SELECT EMPNO, ENAME, SAL, COMM, SAL+COMM,
       NVL(COMM, 0),
       SAL + NVL(COMM, 0)
FROM EMP;

SELECT COMM, NVL(COMM, 500) FROM EMP;

SELECT ENAME, COMM,
    NVL2(COMM, 'O', 'X'),
    NVL2(COMM, SAL*12+COMM, SAL*12) AS 연봉
FROM EMP;
