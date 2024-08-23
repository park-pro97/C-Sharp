//HTML 시작 그냥 페이지만 열어봄
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>나의 첫 웹페이지</title>
</head>
<body>
    <h1>Hello World~!</h1>
    <p></p>
    <h2>안녕하세요!!</h2>
</body>
</html>

-------------------------------------------------------------------
//네이버 다음 등 도메인 링크 걸어두고 누르면 이동하게
<!DOCTYPE html>
<html lang="ko"></html>
<head>
        <meta charset="UTF-8">
        <title>HTML 태그의 속성</title>
</head>
<body>
    <a href="https://www.naver.com" target="_blank"> 네이버 사이트로 이동 </a><br>
    <a href="https://www.daum.net" target="_blank"> 다음 사이트로 이동</a><br>
    <a href="https://www.nate.com" target="_blank"> 네이트 사이트로 이동</a><br>

</body>
</html>

-------------------------------------------------------------------
//제목 크기 다르게 나타내기
<!DOCTYPE html>
<html>
    <head>
        <meta charset="UTF-8">
        <title>제목 연습(h1~h6)</title>
    </head>
    <body>
        <h1>글자 제목(h1)</h1>
        <h2>글자 제목(h2)</h2>
        <h3>글자 제목(h3)</h3>
        <h4>글자 제목(h4)</h4>
        <h5>글자 제목(h5)</h5>
        <h6>글자 제목(h6)</h6>
    </body>
</html>

-------------------------------------------------------------------
//ul_li 사용
<!DOCTYPE html>
<html lang="ko">
    <head>
        <meta charset="UTF-8">
        <title>식물원 관람 유의사항</title>
    </head>
    <body>
        <h3>식물원 관람 유의사항</h3>
        <ul>
            <li>입장권에 게시된 관람요령을 살펴보세요.</li>
            <li>안내원의 안내를 따르세요.</li>
            <li>관람 지역 이외의 출입을 제한합니다.</li>
            <li>풀을 밟지 마세요.</li>
        </ul>
        
    </body>
</html>

-------------------------------------------------------------------
//dt_dd 이건 들여쓰기 같은 기능
<!DOCTYPE html>
<html lang="ko">
    <head>
        <meta charset="UTF-8">
        <title>정의 목록</title>
    </head>
    <body>
        <h3>계절별 양생화</h3>

        <dl>
            <dt>봄꽃</dt>
            <dd>봄에 피느 꽃이 다년생 식물은 꽃잔디</dd>
        </dl>

        <dl>
            <dt>여름꽃</dt>
            <dd>여름에 피느 풀이 옥잠화, 비빛 수국</dd>
        </dl>
    </body>
</html>
-------------------------------------------------------------------
//링크 태그 [ 사진도 ]
<!DOCTYPE html>
<html>
    <body>
        <a href="dt_dd.html" target="_blank"></a>>dd페이지</a> <p></p>

        <a href="./temp1/first.html" target="_blank">첫 웹페이지</a> <p></p>
        <a href="sample.html" target="_blank">sample 웹페이지</a> <p></p>

        <img src="img/a.png"> <p></p>
        <img src="img/b.png" width="200" height"400"> <p></p>
    </body>
</html>
-------------------------------------------------------------------
//테이블 만들어보기
<!DOCTYPE html>
<html>
<head>
    <title>표 그리기</title>
    <style>
        table, th, tr, td {
            border:solid 1px black;
            border-collapse: collapse;
            padding: 8px;
        }
    </style>
</head>
    <body>
        <table>
            <tr>
                <th>번호</th>
                <th>이름</th>
                <th>전화번호</th>
            </tr>
            <tr>
                <th>1</th>
                <th>홍길동</th>
                <th>010-1111-1111</th>
            </tr>
            <tr>
                <th>2</th>
                <th>이명박</th>
                <th>010-2222-2222</th>
            </tr>
        </table>
    </body>
</html>
-------------------------------------------------------------------
//테이블3
<!DOCTYPE html>
<head>

</head>
<body>
    <table border="1">
        <tr>
            <td>지역</td>
            <td>현재기온</td>
            <td colspan="2">불쾌지수/습도(%)</td>
            
            <td>풍속</td>
        </tr>
        <tr>
            <td rowspan="2">서울/경기</td>
            <td>23</td>
            <td>60</td>
            <td>80</td>
            <td>4.7</td>
        </tr>
        <tr>
            <td>20</td>
            <td>60</td>
            <td>80</td>
            <td>5.0</td>
        </tr>
        <tr>
            <td>제주도</td>
            <td>21</td>
            <td>65</td>
            <td>85</td>
            <td>3.6</td>
        </tr>

    </table>
</body>
</html>
-------------------------------------------------------------------
//form 만들기
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <form>
        이름 : <input type="text"><br>
        나이 : <input type="text"><P>

        비밀번호 : <input type="password"><br><br>
        개인정보 : <input type="radio" name="info" checked> 공개
                 <input type="radio" name="info"> 비공개
                 <p></p>
        이메일 : <input type="text">@
                <SELECT>
                    <OPTION>선택</OPTION>
                    <OPTION>naver.com</OPTION>
                    <OPTION>gmail.com</OPTION>
                    <OPTION>daum.net</OPTION>
                    <OPTION>직접입력</OPTION>
                </SELECT>
                <p></p>
        자기소개 : <textarea rows="10" cols="50"></textarea><br>

        <button type="button">검색</button>
        <button type="submit">확인</button>
        <button type="reset">다시 쓰기</button>
    </form>
</body>
</html>
-------------------------------------------------------------------
//생상 폰트 사이즈를 고정하고 작성
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <title>CSS 01</title>
    <!-- CSS 코드를 삽입 가능 -->
    <!-- 나중에 JQuery를 통해 프로그래밍화 -->
    <style>
        p {
            color:red;
            font-size: 30px;
        }
    </style>
</head>
<body>
    <p>나무의 줄기는 땅 위로 높게 자라고 해마다 굵기가 두꺼워지지만,
    풀의 줄기는 일 년만 자라고 겨울을 나는 동안 땅 윗부분이 죽는다.
    </p>
</body>
</html>
-------------------------------------------------------------------
//CSS
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <title>CSS 01</title>
    <!-- CSS 코드를 삽입 가능 -->
    <!-- 나중에 JQuery를 통해 프로그래밍화 -->
    <style>
        p {
            color:red;
            font-size: 30px;
        }
    </style>
</head>
<body>
    <p>나무의 줄기는 땅 위로 높게 자라고 해마다 굵기가 두꺼워지지만,
    풀의 줄기는 일 년만 자라고 겨울을 나는 동안 땅 윗부분이 죽는다.
    </p>
</body>
</html>
-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

-------------------------------------------------------------------
//

