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
//

-------------------------------------------------------------------------------------------
//

-------------------------------------------------------------------------------------------
//

-------------------------------------------------------------------------------------------
//

-------------------------------------------------------------------------------------------
//

-------------------------------------------------------------------------------------------
//

-------------------------------------------------------------------------------------------
//

-------------------------------------------------------------------------------------------
//
