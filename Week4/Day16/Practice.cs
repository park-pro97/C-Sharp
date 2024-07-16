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
//테이블로 주소록 만들기
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
