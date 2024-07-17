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
1번   LAPD, RPAD 함수 활용 출력
SELECT 'Oracle',
    LPAD('Oracle', 10, '#') AS LPAD_1,
    RPAD('Oracle', 10, '*') AS RPAD_1,
    LPAD('Oracle', 10) AS LPAD_2,
    LPAD('Oracle', 10) AS RPAD_2 FROM DUAL;
 
2번   RPAD 함수 활용 주민 뒷자리 *표시 출력
RPAD('971225-', 14, '*') AS RPAD_JMMO,
    RPAD('010-1234-', 13, '*') AS RPAD_PHONE FROM DUAL;
 
3번   두 열 사이 콜론 넣고 연결
SELECT CONCAT(EMPNO, ENAME),
       CONCAT(EMPNO, CONCAT(' : ', ENAME)) FROM EMP;
 
4번   TRIM 함수로 공백 제거하여 출력
SELECT '[' || TRIM(' _ _ORACLE_ _ ') || ']' AS TRIM,
       '[' || TRIM(LEADING FROM ' _ _ORACLE_ _ ') || ']' AS TRIM_LEADING,
       '[' || TRIM(TRAILING FROM ' _ _ORACLE_ _ ') || ']' AS TRIM_TRAILING,
       '[' || TRIM(BOTH FROM ' _ _ORACLE_ _ ') || ']' AS TRIM_BOTH
FROM DUAL;
 
5번   TRIM 함수로 삭제할 문자 _ 삭제 후 출력하기
SELECT '[' || TRIM('_' FROM '_ _ORACLE_ _') || ']' AS TRIM,
       '[' || TRIM(LEADING '_' FROM '_ _ORACLE_ _') || ']' AS TRIM_LEADING,
       '[' || TRIM(TRAILING '_' FROM '_ _ORACLE_ _') || ']' AS TRIM_TRAILING,
       '[' || TRIM(BOTH '_' FROM '_ _ORACLE_ _') || ']' AS TRIM_BOTH
FROM DUAL;
 
6번   TRIM, LTRIM, RTRIM 활용
SELECT '[' || TRIM(' _ORACLE_ ') || ']' AS TRIM,
       '[' || LTRIM(' _ORACLE_ ') || ']' AS LTRIM,
       '[' || LTRIM('<_ORACLE_>', '_<') || ']' AS LTRIM_2,
       '[' || RTRIM(' _ORACLE_ ') || ']' AS RTRIM,
       '[' || RTRIM('<_ORACLE_>', '>_') || ']' AS RTRIM_2
FROM DUAL;


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
//
