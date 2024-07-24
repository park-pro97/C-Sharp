//3의배수
DECLARE
BEGIN
    FOR I IN 1..10 LOOP   --IN뒤에 REVERSE만 넣으면 뒤집어서 출력 바로 됨
        IF MOD(I,3) = 0 THEN
            DBMS_OUTPUT.PUT_LINE('출력 : ' || I);  -- || 문자열끼리 연결
        END IF;
    END LOOP;
END;
/


-------------------------------------------------------------------------------------------
//실습 16-20 0~4 반전시켜 출력
DECLARE
    BEGIN
        FOR I IN REVERSE 0..4 LOOP
            DBMS_OUTPUT.PUT_LINE('출력 : ' || I);
    END LOOP; 
END;
/


-------------------------------------------------------------------------------------------
//16-21 FOR LOOP 안에 CONTINUE
BEGIN
    FOR I IN 0..20 LOOP
        CONTINUE WHEN MOD(I, 2) = 1;
        DBMS_OUTPUT.PUT_LINE('출력 : ' || I);
    END LOOP;
END;
/



-------------------------------------------------------------------------------------------
//레코드를 정의해서 사용하기
DECLARE
    TYPE REC_DEPT IS RECORD(
        deptno NUMBER(2) NOT NULL := 99,
        dname DEPT.DNAME%TYPE,
        loc DEPT.LOC%TYPE
    );
    dept_rec REC_DEPT;
BEGIN
    dept_rec.deptno := 99;
    dept_rec.dname := 'DATABASE';
    dept_rec.loc := 'SEOUL';
    
    DBMS_OUTPUT.PUT_LINE(dept_rec.deptno);
    DBMS_OUTPUT.PUT_LINE(dept_rec.dname);
    DBMS_OUTPUT.PUT_LINE(dept_rec.loc);
END;
/



-------------------------------------------------------------------------------------------
//17-2 DEPT_RECORD
DECLARE
    TYPE REC_DEPT IS RECORD(
        deptno NUMBER(2) NOT NULL := 99,
        dname DEPT.DNAME%TYPE,
        loc DEPT.LOC%TYPE
    );
    dept_rec REC_DEPT; --객체선언
BEGIN
    dept_rec.deptno := 99;
    dept_rec.dname := 'DATABASE';
    dept_rec.loc := 'SEOUL';
    
    INSERT INTO DEPT_RECORD
    VALUES dept_rec;
END;
/
CREATE TABLE DEPT_RECORD
AS SELECT * FROM DEPT;


SELECT * FROM DEPT_RECORD;


-------------------------------------------------------------------------------------------
//17-3
DECLARE
    TYPE DEPTREC IS RECORD(
        deptno NUMBER(2) NOT NULL := 88,
        dname DEPT.DNAME%TYPE,
        loc DEPT.LOC%TYPE
    );
    deptrecord DEPTREC; --객체선언
BEGIN
    deptrecord.deptno := 88;
    deptrecord.dname := '스마트팩토리';
    deptrecord.loc := '대구';
    
    INSERT INTO DEPT_RECORD
    VALUES deptrecord;
END;
/



-------------------------------------------------------------------------------------------
//레코드로 INSERT하기
SELECT * FROM DEPT_RECORD;
--17-4
DECLARE
    TYPE REC_DEEPT IS RECORD(
        deptno NUMBER(2) NOT NULL := 99,
        dname DEPT.DNAME%TYPE,
        loc DEPT.LOC%TYPE
    );
    dept_rec REC_DEPT;
BEGIN
    dept_rec.deptno := 50;
    dept_rec.dname := 'DB';
    dept_rec.loc := 'SEOUL';
    
    UPDATE DEPT_RECCORD
    SET ROW = dept_rec
    WHERE DEPTNO = 99;
END;
/



-------------------------------------------------------------------------------------------
//사용자 사전
SELECT COUNT (*) FROM DICT;



-------------------------------------------------------------------------------------------
//사용자 사전
SELECT TABLE_NAME FROM USER_TABLES
ORDER BY TABLE_NAME;

--데이터 사전ALL_%
SELECT * FROM ALL_TABLES;

--데이터 사전
SELECT OEWNER, TABLE_NAME FROM ALL_TABLE;

--DBA_%
SELECT * FROM DBA_TABLES
WHERE USERNAME = 'scott';


-------------------------------------------------------------------------------------------
//인덱스 확인
SELECT * FROM USER_INDEXES;

SELECT * FROM USER_IND_COLUMNS;

-------------------------------------------------------------------------------------------
//인덱스 만들기
CREATE INDEX IDX_EMP_SAL ON EMP(SAL);
SELECT * SAL FROM EMP;

-------------------------------------------------------------------------------------------
//인덱스 삭제
DROP INDEX IDX_EMP_SAL;

-------------------------------------------------------------------------------------------
//VIEW

SELECT * FROM VW_EMP20;

SELECT * FROM (SELECT EMPNO, ENAME, SAL FROM EMP WHERE DEPTNO = 20);

-------------------------------------------------------------------------------------------
//SCOTT계정에 VIEW를 만드는 권한을 부여

GRANT CREATE VIEW TO SCOTT;

-------------------------------------------------------------------------------------------
//뷰 만들기
CREATE VIEW VW_EMP20
AS (SELECT EMPNO, ENAME, JOB, DEPTNO
FROM EMP
WHERE DEPTNO = 20);

--뷰 확인
SELECT * FROM USER_VIEWS;

--뷰 삭제
DROP VIEW VW_EMP20;

-------------------------------------------------------------------------------------------
//동의어 SYNONYM
GRANT CREATE SYNONYM TO SCOTT;

GRANT CREATE PUBLIC SYNONYM TO SCOTT

--동의어
CREATE SYNONYM E FOR EMP;

SELECT * FROM E;
DROP SYNONYM E;

-------------------------------------------------------------------------------------------
//18-1 단일행 데이터 저장하기
DECLARE
    V_DEPT_ROW      DEPT%ROWTYPE;
BEGIN
    SELECT DEPTNO, DNAME, LOC INTO V_DEPT_ROW FROM DEPT
    WHERE DEPTNO = 40;
    
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DEPTNO);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DNAME);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.LOC);
END;
/

-------------------------------------------------------------------------------------------
//18-2 단일행 데이터 저장하는 커서 사용
DECLARE
    V_DEPT_ROW DEPT%ROWTYPE;
    --명시적 커서 선언
    CURSOR c1 IS
        SELECT DEPTNO, DNAME, LOC FROM DEPT WHERE DEPTNO = 40;
BEGIN
    --커서 열기 OPEN
    OPEN c1;
    FETCH c1 INTO V_DEPT_ROW;
    
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DEPTNO);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DNAME);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.LOC);
        
    --커서닫기 리소스 반환
    CLOSE c1;
END;
/

-------------------------------------------------------------------------------------------
//18-3
DECLARE
    V_DEPT_ROW DEPT%ROWTYPE;
    --명시적 커서 선언
    CURSOR c1 IS
        SELECT DEPTNO, DNAME, LOC FROM DEPT;
BEGIN
    --커서 열기 OPEN
    OPEN c1;
    LOOP
        FETCH c1 INTO V_DEPT_ROW;
        --커서의 모든 행을 읽어오기 위해 속성을 지정
        EXIT WHEN c1%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('부서번호 : ' || V_DEPT_ROW.DEPTNO ||
                            '부서이름 : ' || V_DEPT_ROW.DNAME ||
                            '부서위치 : ' || V_DEPT_ROW.LOC);
    END LOOP;      
    --커서닫기 리소스 반환
    CLOSE c1;
END;
/

-------------------------------------------------------------------------------------------
//18-8
DECLARE
    v_wrong NUMBER;
BEGIN
    SELECT DNAME INTO v_wrong
        FROM DEPT
    WHERE DEPTNO = 10;
EXCEPTION
    WHEN VALUE_ERROR THEN
        DBMS_OUTPUT.PUT_LINE('에러발생!!');
END;
/

-------------------------------------------------------------------------------------------
//19-1 저장 프로시저
CREATE OR REPLACE PROCEDURE pro_noparam
IS
    V_EMPNO NUMBER(4) := 7788;
    V_ENAME VARCHAR2(10);
BEGIN
    V_ENAME := 'SCOTT';
    DBMS_OUTPUT.PUT_LINE(V_EMPNO);
    DBMS_OUTPUT.PUT_LINE(V_ENAME);
END;
/
EXECUTE pro_noparam;   --얘로 계속 실행 가능  저장 프로시저의 장점.

-------------------------------------------------------------------------------------------
//USER_SOURCE
SELECT TEXT FROM USER_SOURCE
WHERE NAME = 'PRO_NOPARAM';

[--삭제는 그냥 DROP 늘 하듯]


-------------------------------------------------------------------------------------------
//프로시저에 파라미터 지정하기   19-10
CREATE OR REPLACCE PROCEDURE pro_param_in
(
    param1 IN NUMBER,
    param2 NUMBER,
    param3 NUMBER := 3,
    param4 NUMBER DEFAULT 4
)
IS
BEGIN
    DBMS_OUTPUT.PUT_LINE(param1);
    DBMS_OUTPUT.PUT_LINE(param2);
    DBMS_OUTPUT.PUT_LINE(param3);
    DBMS_OUTPUT.PUT_LINE(param4);
END;
/


-------------------------------------------------------------------------------------------
//19-12
CREATE OR REPLACCE PROCEDURE pro_param_out
(
    in_empno IN EMP.EMPNO%TYPE,
    out_ename OUT EMP.ENAME%TYPE,
    out_sal OUT EMP.SAL%TYPE
)
IS
BEGIN
    SELECT ENAME, SAL INTO out_ename, out_sal
        FROM    EMP
        WHERE EMPNO = in_empno;
    END pro_param_out;
END;
/


-------------------------------------------------------------------------------------------
//19-13
DECLARE
    v_ename EMP.ENAME%TYPE;
    v_sal EMP.SAL%TYPE;
BEGIN
    pro _param_out(7839, v_ename, v_sal);
    DBMS_OUTPUT.PUT_LINE(v_ename);
    DBMS_OUTPUT.PUT_LINE(v_sal);
END;
/

SELECT * FROM emp;


-------------------------------------------------------------------------------------------
//19-16 프로시저 오류 정보
CREATE OR REPLACE PROCEDURE pro_err
IS
    err_no  NUMBER;
BEGIN
    err_no = 100;
    DBMS_OUTPUT.PUT_LINE(err_no);
END pro_err;
/
SHOW ERRORS;



-------------------------------------------------------------------------------------------
//ERROR를USER_%에서 확인하기
SELECT * FROM USER_ERRORS
WHERE NAME := 'PRO_ERR';



-------------------------------------------------------------------------------------------
//19-19함수
CREATE OR REPLACE FUNCTION func_aftertax(
    sal IN NUMBER -- 변수 입력받아야함
)
RETURN NUMBER
IS
    tax NUMBER := 0.05;
BEGIN
    RETURN( ROUND(sal - (sal * tax)));
END func_aftertax;
/


-------------------------------------------------------------------------------------------
//19-20함수 사용
DECLARE
    aftertax NUMBER;
BEGIN
    aftertax := func_aftertax(3000);
    DBMS_OUTPUT.PUT_LINE(aftertax);
END;
/

SELECT func_aftertax(3000) FROM DUAL;

SELECT EMPNO, ENAME, SAL, func_aftertax(SAL) FROM EMP;


-------------------------------------------------------------------------------------------
//19-24
CREATE OR REPLACE PACKAGE pkg_example
IS
    spec_np NUMBER := 10;
    FUNCTION func_aftertax(sal NUMBER) RETURN NUMBER;
    PROCEDURE pro_emp(in_empno IN EMP.EMPNO%TYPE);
    PROCEDURE pro_dept(in_deptno IN DEPT.DEPTNO%TYPE);
END;
/

    -----
사용
SELECT TEXT FROM USER_SOURCE
WHERE TYPE = 'PACKAGE';

DESC pkg_example;

-------------------------------------------------------------------------------------------
//트리거를 사용할 테이블
CREATE TABLE EMP_TRG AS SELECT * FROM EMP;

SELECT * FROM EMP_TRG;


-------------------------------------------------------------------------------------------
//19-32 주말 작업 불가 트리거
--DROP TRIGGER trg_emp_nodm1_weekend;
CREATE OR REPLACE TRIGGER trg_emp_nodm1_weekend
BEFORE
INSERT OR UPDATE OR DELETE ON EMP_TRG
BEGIN
    IF TO_CHAR(SYSDATE, 'DY', 'NLS_DATE_LANGUAGE=ENGLISH') IN ('SAT', 'SUN') THEN
        IF INSERTING THEN
            raise_application_error(-2000, '주말 사원정보 추가 불가');
        ELSIF UPDATING THEN
            raise_application_error(-2001, '주말 사원정보 수정 불가');
        ELSIF DELETING THEN
            raise_application_error(-2002, '주말 사원정보 삭제 불가');
        ELSE
            raise_application_error(-2003, '주말 사원정보 변경 불가');
        END IF;
    END IF;
END;
/
SELECT * FROM EMP_TRG WHERE ENAME= 'KING';

-------------------------------------------------------------------------------------------
//


