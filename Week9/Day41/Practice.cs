//다른 폴더에 이미지 있을 때
<!-- 요건 주석 -->

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>PictureBox</title>
</head>
<body>
    <img src="./img/pic1.PNG">
</body>
</html>
-----------------------------------------------------------------
//CSS 배경 색상 텍스트 등에 색상을 넣어주는 코드
<!DOCTYPE html>
<html>
<head>
    <style>
        body{
            background-color: #4e4f02;
        }
        h1{
            background-color: yellow;
        }
        p{
            width: 500px; border: solid 5px #0000ff45;
            background-color: #ffffff;
        }
        #cyan{
            background-color: cyan;
        }
    </style>
</head>
<body>
    <h1>식물원</h1>
    <p>
        식물명으로 표시된 다양한 식물들의 수집, 재배, 보존, 전시 등을 위한 정원이다. 
        선인장과 <span id="cyan">다른 다육식물, 허브정원,</span> 
        세계 특수 지역의 식물 등과 같은 전문 식물 수집품을 포함할 수 있으며, 
        열대식물, 고산식물 또는 다른 외래 식물과 같은 특별한 수집품과 함께 온실과 그늘집이 있을 수 있다. 
        식물원의 방문객 서비스에는 관광, 교육 전시, 미술 전시회, 도서실, 야외 연극 및 음악 공연, 
        기타 오락 등이 포함될 수 있다.
    </p>
</body>
</html>
-----------------------------------------------------------------
//CSS 배경 이미지 삽입
<!DOCTYPE html>
<html>
<head>
    <style>
        body{
            background-image: url("../img/yellow_bg.jpg");
        }
    </style>
</head>
<body>
    <h1>양귀비 꽃</h1>
    <P><img src="../img/yangguibi.jpg"></P>
</body>
</html>
-----------------------------------------------------------------
//No-Repeat Code [ 배경 공간에 이미지 반복되는게 Default이지만 no-repeat를 쓰면 반복하지 않음 ]
<!DOCTYPE html>
<html>
<head>
    <style>
        body{
            background-image: url("../img/flower_bee.jpg");
            background-repeat: no-repeat;
        }
    </style>
</head>
<body>
    <p><img src="../img/banner.png"></p>
</body>
</html>
-----------------------------------------------------------------
//CSS 배경, 포지션 속성
<!DOCTYPE html>
<html>
<head>
    <style>
        body{
            background-image: url("../img/narrow_bg.png");
            background-repeat: repeat-x;
        }
    </style>
</head>
<body>
    <img src="../img/bombit_logo.png">
</body>
</html>
-----------------------------------------------------------------
//
<!DOCTYPE html>
<html>
<head>
    <style>
        body{
            background-image: url("../img/gradient_bg.png");
            background-repeat: no-repeat;
            background-position: center top;
        }
        p{
            text-align: center;
        }
    </style>
</head>
<body>
    <p><img src="../img/rose.jpg"></p>
</body>
</html>
-----------------------------------------------------------------
//CSS 목록 작성
<html>
<head>
    <style>
        li{
            list-style: armenian;
        }
    </style>
</head>
<body>
    <h3>★녹색 문화 체험여행</h3>
    <ul>
        <li>숲 체험 : 수목원 탐방</li>
        <li>곤충 체험 : 곤충 체험</li>
        <li>생태 체험 : 생태 체험</li>
    </ul>
</body>
</html>
-----------------------------------------------------------------
//Border 코드 실습
<!DOCTYPE html>
<html>
<head>
    <style>
        #border1{
            border: solid 1px #ff0000;
        }
        #border2{
            border: double 5px #00ff00;
        }
        #border3{
            border-top: dotted 4px #0000ff;
            border-bottom: dashed 4px #ff00ff;
        }
        #border4{
            border-left: solid 5px #ffff00;
            border-right: solid 5px #00ffff;
        }
    </style>
</head>
<body>
    <h3 id="border1">봄빛 식물원</h3>
    <h3 id="border2">봄빛 식물원</h3>
    <h3 id="border3">봄빛 식물원</h3>
    <h3 id="border4">봄빛 식물원</h3>
</body>
</html>
-----------------------------------------------------------------
//둥근 경계선을 만들어보는 코드
<!DOCTYPE html>
<html>
<head>
    <style>
        #login_box{
            width: 200px; height: 150px;
            background-color: #eee;
            border: solid 2px #aaa;
            border-radius: 15px;
            box-shadow: 6px 6px 5px #888;
            text-align: center;
        }
    </style>
</head>
<body>
    <div id="login_box">로그인 화면</div>
</body>
</html>
-----------------------------------------------------------------
/시시계 방향으로 Top부터 돌면서 Margin 수치가 순차적 적용 [4방향]
<!DOCTYPE html>
<html>
<head>
    <style>
        p{
            width: 500px;
            border: solid 5px #50f;
            margin: 20px 40px 60px 80px
        }
    </style>
</head>
<body>
    <p>
        식물원은 종종 대학이나 다른 과학 연구 기관들에 의해 운영되며,
         식물 분류학이나 식물 과학의 다른 측면에 있는 헤르바리아와 
         연구 프로그램들을 종종 연관시켜왔다. 
         원칙적으로, 그들의 역할은 과학적 연구, 보존, 전시, 그리고 교육의 
        목적을 위해 문서화된 살아있는 식물의 컬렉션을 유지하는 것이다.
    </p>
</body>
</html>
-----------------------------------------------------------------
//padding 속성을 부여하는 코드
<!DOCTYPE html>
<html>
<head>
    <style>
        p{
            width: 500px;
            border: solid 5px #50f;
            padding: 10px 20px 30px 40px
        }
    </style>
</head>
<body>
    <p>
        식물원은 종종 대학이나 다른 과학 연구 기관들에 의해 운영되며,
         식물 분류학이나 식물 과학의 다른 측면에 있는 헤르바리아와 
         연구 프로그램들을 종종 연관시켜왔다. 
         원칙적으로, 그들의 역할은 과학적 연구, 보존, 전시, 그리고 교육의 
        목적을 위해 문서화된 살아있는 식물의 컬렉션을 유지하는 것이다.
    </p>
</body>
</html>
-----------------------------------------------------------------
//책 페이지 만들기
<!DOCTYPE html>
<html>
<head>
    <style>
        #banner{
            width: 185px; height: 236px;
            background-image: url("./img/banner_bg.jpg");
            border: solid 1px #ccc;
        }
        #banner img{
            margin:202px 0 0 50px;
        }
    </style>
</head>
<body>
    <ul>
        <li>저자 : 황재호</li>
        <li>출판사 : 한빛미디어</li>
        <li>가격 : 30,000원</li>
    </ul>
    <div id="banner">
        <img src="./img/buy.png">
    </div>
</body>
</html>
-----------------------------------------------------------------
//별(*) 을 사용하여 전체 Style을 부여
<!DOCTYPE html>
<html>
<head>
    <style>
        * {
            margin: 0; padding: 0;
        }
        h3{
            margin: 20px 0 0 10px;
        }
        ul{
            margin: 10px 0 0 30px;
        }
        li{
            margin-top: 2px;
        }
        #banner{
            width: 185px; height: 236px;
            background-image: url("./img/banner_bg.jpg");
            border: solid 1px #ccc;
        }
        #banner img{
            margin:202px 0 0 50px;
        }
    </style>
</head>
<body>
    <h3>PHP 책 소개</h3>
    <ul>
        <li>저자 : 황재호</li>
        <li>출판사 : 한빛미디어</li>
        <li>가격 : 30,000원</li>
    </ul>
    <div id="banner">
        <img src="./img/buy.png">
    </div>
</body>
</html>
-----------------------------------------------------------------
//인라인과 블록을 직접 사용
<!DOCTYPE html>
<html>
<head>
    <!-- 스타일을 자바에 넣고 싶다면 script로 -->
    <style>

    </style>
</head>
<body>
    <h2>인라인 블록</h2>

    <h3>1. 인라인 요소</h3>
        <span>텍스트1</span>
        <span>텍스트2</span>
        <span>텍스트3</span>
        <img src="./img/image1.jpg">
        <img src="./img/image2.jpg">
    <h3>2. 블록 요소</h3>
    <p>이것은 단락입니다.</p>
    <div>박스 1</div>
</body>
</html>
-----------------------------------------------------------------
//Display 속성 사용     [ display; 과 inline;을 사용하면 블록 요소가 인라인으로 바뀜 ]
<!DOCTYPE html>
<html>
<head>
    <style>
        *{
            margin: 0; padding: 0;
        }
        #menu{
            width: 500px; padding: 10;
            margin: 20px 0 0 40px;
            background-color: #eee;
            border: solid 2px #aaa;
            text-align: center;
        }
        #menu li{
            display: inline;
        }
    </style>
</head>
<body>
    <ul id="menu">
        <li>회사소개 |</li>
        <li>제품소개 |</li>
        <li>공지사항 |</li>
        <li>업무제휴 |</li>
        <li>고객센터 |</li>
    </ul>
</body>
</html>
-----------------------------------------------------------------
//인라인 + 블록 특성 [ display:inline-block 을 통해서 인라인과 박스 방식을 모두 사용해 공간에 맞게 옆, 아래로 배치 가능 ]
<!DOCTYPE html>
<html >
<head>
    <style>
        .green_box{
            display:inline-block;
            width: 150px; height: 75px; 
            margin: 10px; padding: 10px;
            border: solid 3px #73AD21;
        }
    </style>
</head>
<body>
    <h2>인라인 + 블록 특성 (display:inline-block)</h2>
    <div class="green_box">박스 1</div>
    <div class="green_box">박스 2</div>
    <div class="green_box">박스 3</div>
    <div class="green_box">박스 4</div>
    <div class="green_box">박스 5</div>
    <div class="green_box">박스 6</div>
</body>
</html>
-----------------------------------------------------------------
//CSS 테이블과 폼 [ 테이블의 경계선, 배경, 텍스트를 넣는 방법 ]
<!DOCTYPE html>
<html >
<head>
    <style>
        table{
            border-collapse: collapse; width: 610px
        }
        tr{
            height: 40px; text-align: center;
        }
        td, th{
            padding: 5px;
        }
        .train{
            background-color: #fbdbf2;
            color: #f1477b;
            text-decoration: underline;
            font-weight: bold;
        }
        .table_title{
            height: 30px;
            background-color: #eeeeee;
        }
        #col1, #col4{
            width: 90px;
        }
        #col2, #col3{
            width: 60px;
        }
        #col5, #col6{
            width: 80px;
        }
        table, td, th{
            border:solid 1px #cccccc;
        }
    </style>
</head>
<body>
    <h2>KTX 열차표 예매</h2>
    <table>
        <tr class="table_title">
            <th>열차번호</th>
            <th>출발</th>
            <th>도착</th>
            <th>출발시간</th>
            <th>특실</th>
            <th>일반실</th>
            <th>소요시간</th>
        </tr>
        <tr>
            <td id="col1" class="train">175</td>
            <td id="col2">서울</td>
            <td id="col3">부산</td>
            <td id="col4">21:00</td>
            <td id="col5"><img src="./img/full.png"></td>
            <td id="col6"><img src="./img/full.png"></td>
            <td id="col7">02:44</td>
        </tr>
        <tr>
            <td id="col1" class="train">177</td>
            <td id="col2">서울</td>
            <td id="col3">부산</td>
            <td id="col4">21:30</td>
            <td id="col5"><img src="./img/empty.png"></td>
            <td id="col6"><img src="./img/empty.png"></td>
            <td id="col7">02:38</td>
        </tr>
        <tr>
            <td class="train">179</td>
            <td>서울</td>
            <td>부산</td>
            <td>22:00</td>
            <td><img src="./img/empty.png"></td>
            <td><img src="./img/empty.png"></td>
            <td>02:42</td>
        </tr>
    </table>
</body>
</html>

-----------------------------------------------------------------
//로그인 폼
<!DOCTYPE html>
<html>
<head>
    <style>
        *{
            margin: 0; padding: 0;
        }
        ul{
          list-style-type:none;  
        }
        body{
            font-family:"맑은 고딕", "돋움";
            font-size: 24px;
            color: #444;
        }
        #login_box{
            width: 400px; height: 160px;
            border: solid 1px #bbbbbb;
            border-radius: 15px;
            margin: 20px 0 0 20px;
            padding: 20px 0 0 30px;
        }
        h2{
            font-family: "Arial";
            margin-bottom: 10px;
        }
        #login_box input{
            width: 200; height: 36px;
        }
        #id_pass, #login_btn{
            display: inline-block;
            vertical-align: top;
        }
        #id_pass span{
            display: inline-block;
            width: 40px;
        }
        #pass{
            margin-top: 6px;
        }
        #login_btn button{
            margin-left: 5px;
            padding: 10px;
            border-radius: 5px;
        }
        #btns{
            margin: 12px 0 0 -12px;
            text-decoration: underline;
            font-size: 20px;
        }
        #btns li{
            margin-left: 10px;
            display: inline;
        }
    </style>
</head>
<body>
    <form>
        <div id="login_box">
            <h2>Member Login</h2>
            <ul id="input_button">
                <li id="id_pass">
                    <ul>
                        <li>
                            <span>ID</span>
                            <input type="text">
                        </li> <!-- ID -->
                    </ul>
                </li>
                <li id="login_btn">
                    <button>로그인</button>
                </li>
            </ul>
            <ul id="btns">
                <li>회원가입</li>
                <li>아이디/비밀번호 찾기</li>
            </ul>
        </div> <!-- login Box -->
    </form>
</body>
</html>
-----------------------------------------------------------------
//HTML5 레이아웃
<!DOCTYPE html>
<html>
<head>
    <style>
        .clear {
            clear: both;
        }
        aside{
            width: 175px; height: 398px;
            float: left; padding: 2px;
            border: solid 2px #f00;
        }
        nav{
            height: 150px;
            border: solid 2px #00f;
            margin-bottom: 50px;
            margin: 2px;
        }
        header{
            width: 800px; height: 60px;
            border: solid 2px #f00;
            margin: 2px;
        }
        main{
            width: 618px; height: 400px;
            margin-left: 2px;
            float: left;
            border: solid 2px #00f;
        }
        section{
            width: 500px; height: 150px;
            margin: 2px;
            border: solid 2px #0f0;
        }
        footer{
            width: 800px; height: 90px;
            border: solid 2px #f00;
            margin: 2px;
        }
    </style>
</head>
<body>
    <!--------------- Header --------------->
    <header>상단 헤더</header>
    <aside>좌측
        <nav>메뉴</nav>
    </aside>
    <main>메인 콘텐츠
        <section>콘텐츠 1</section>
        <section>콘텐츠 2</section>
    </main>
    <div class="clear"></div>
    <!--------------- footer --------------->
    <footer>하단 헤더(푸터)</footer>
</body>
</html>
-----------------------------------------------------------------
//Float가 없는 코드와 있는 코드를 각각 만들어 차이
//Float가 없는
<!DOCTYPE html>
<html>
<head>
    <style>
        div{
            width: 150px; height: 80px;
            border: solid 2px #00f;
            margin-top: 2px;
        }
    </style>
</head>
<body>
    <div>박스 A</div>
    <div>박스 B</div>
</body>
</html>
-----------------------------------------------------------------
//Float가 없는 코드와 있는 코드를 각각 만들어 차이
//Float가 있는
<!DOCTYPE html>
<html>
<head>
    <style>
        div{
            width: 150px; height: 80px;
            border: solid 2px #00f;
            margin-top: 2px;
        }
        #box_a{
            float: left;
        }
        #box_b{
            float: right;
        }
    </style>
</head>
<body>
    <div id="box_a">박스 A</div>
    <div id="box_b">박스 B</div>
</body>
</html>
-----------------------------------------------------------------
//About Clear
<!DOCTYPE html>
<html>
<head>
<!-- 여기서 float = inline-block과 비슷한 효과 -->
    <style>
        .items{
            border: solid 3px #000;
            margin: 5px;
            float: left;
        }
        .items img{
            display: block;
        }
        .clear{
            clear: left;
        }
    </style>
</head>
<body>
    <div class="items"><img src="img/image1.jpg"></div>
    <div class="items"><img src="img/image2.jpg"></div>
    <div class="items"><img src="img/image3.jpg"></div>

    <div class="clear"></div>

    <div class="items"><img src="img/image4.jpg"></div>
    <div class="items"><img src="img/image5.jpg"></div>
    <div class="items"><img src="img/image6.jpg"></div>
</body>
</html>
-----------------------------------------------------------------
//Clear를 이용한 배너 레이아웃
<!DOCTYPE html>
<html>
<head>
    <style>
        *{
            margin: 0; padding: 0;
        }
        body{
            background-color: #f2f0f0;
            font-family: "맑은 고딕";
            font-size: 12px; color: #444444;
        }
        ul{
            list-style-type: none;
        }
        #logo{
            padding: 30px 0 0 30px;
            float: left;
        }
        #top_menu{
            margin: 40px 10px 0 5px;
            float: right;
        }
        #top_menu li{
            display: inline;
        }
        #main_menu{
            font-size: 12px; color: #ffffff;
            background-color: #4e4c4d;
            margin-top: 15px;
            padding: 12px;
            text-align: center;
        }
        #main_menu li{
            padding: 0 20px 0 20px;
            display: inline;
        }
        .clear{
            clear: both;
        }
    </style>
</head>
<body>
    <div id="logo">
        <img src="img/logo2.gif">
    </div>
    <ul id="top_menu">
        <li>로그인 |</li>
        <li>회원가입 |</li>
        <li>마이페이지 |</li>
        <li>주문배송 조회 |</li>
        <li>장바구니 |</li>
        <li>이용안내 |</li>
        <li>고객센터 |</li>
    </ul>

    <dib class="clear"></dib>

    <ul id="main_menu">
        <li>다연아트 소개</li>
        <li>상품 Q&A</li>
        <li>시안 확인</li>
        <li>고객 갤러리</li>
        <li>공지사항</li>
    </ul>
</body>
</html>
-----------------------------------------------------------------
//쇼핑몰 상품 레이아웃
<!DOCTYPE html>
<html>
    <title>롤 스크린 쇼핑몰</title>
<head>
    <style>
        body{
            font-family: "돋움"; font-size: 12px;
            color: #444;
        }
        ul{
            list-style-type: none;
        }
        #new h3{
            padding-bottom: 5px;
            border-bottom: solid 2px #9bc32a;
        }
        #new h3 span{
            color: #80a727;
        }
        #new .item{
            float: left; margin-top: 20px;
            text-align: center;
        }
        #new .subject{
            margin-top: 10px; color: #80a727;
        }
        #new .comment{
            margin-top: 5px; color: #888;
        }
        #new .price{
            margin-top: 5px; color: red;
            font-weight: bold;
        }
        .clear{
            clear: both;
        }
    </style>
</head>
<body>
    <div id="new">
        <h3><span>NEW ARRIVAL</span> 신규상품</h3>
        <ul class="item">
            <li><img src="img/new_01.jpg"></li>
            <li class="subject">맞춤제작(풍경)</li>
            <li class="comment">원하는 사이즈로 제작 가능</li>
            <li class="price">20,000원</li>
        </ul>
        <ul class="item">
            <li><img src="img/new_02.jpg"></li>
            <li class="subject">맞춤제작(풍경)</li>
            <li class="comment">원하는 사이즈로 제작 가능</li>
            <li class="price">20,000원</li>
        </ul>
        <ul class="item">
            <li><img src="img/new_03.jpg"></li>
            <li class="subject">맞춤제작(풍경)</li>
            <li class="comment">원하는 사이즈로 제작 가능</li>
            <li class="price">20,000원</li>
        </ul>

        <div class="clear"></div>

        <ul class="item">
            <li><img src="img/new_04.jpg"></li>
            <li class="subject">맞춤제작(풍경)</li>
            <li class="comment">원하는 사이즈로 제작 가능</li>
            <li class="price">20,000원</li>
        </ul>
        <ul class="item">
            <li><img src="img/new_05.jpg"></li>
            <li class="subject">맞춤제작(풍경)</li>
            <li class="comment">원하는 사이즈로 제작 가능</li>
            <li class="price">20,000원</li>
        </ul>
        <ul class="item">
            <li><img src="img/new_06.jpg"></li>
            <li class="subject">맞춤제작(풍경)</li>
            <li class="comment">원하는 사이즈로 제작 가능</li>
            <li class="price">20,000원</li>
        </ul>
    </div>
</body>
</html>
-----------------------------------------------------------------
//1차 보험 웹사이트 레이아웃
<!DOCTYPE html>
<html>
<head>
    <style>
        *{
            margin: 0; padding: 0;
        }
        header{
            padding: 20px;
        }
        header #logo{
            float: left;
        }
        header #moto{
            margin-left: 10px; margin-top: 30px;
            float: left;
        }
        header #top_menu{
            float: right; font-size: 12px;
        }
        li{
            list-style-type: none;
            display: inline;    
        }
        .line{
            margin-top: 25px;
            border-top: solid 1px #ccc;
        }
        nav #sec_menu{
            float: left; margin-top: 10px;
        }
        .clear { clear: both; }
        section{
            padding: 0 20px 0 20px; margin-left: 20px;
            background-image: url("img/banner_bar.gif");
            border: solid 1px;
            float: left;
        }
        section #banner{
            margin: 10px 15px 0 0; 
        }
        article{
            border: solid 2px rgb(7, 222, 222);
            float: left; margin: 10px 0 0 20px;
        }
        article img{
            display: block;
        }
    </style>
</head>
<body>
    <header>
        <img id="logo" src="img/logo.gif">
        <img id="moto" src="img/moto.gif">
    
        <ul id="top_menu">
            <li>로그인 |</li>
            <li>회원가입 |</li>
            <li>사이트맵 |</li>
            <li>회사 소개 |</li>
        </ul>

        <div class="clear"></div>
        <div class="line"></div>
        
        <nav>
            <ul id="sec_menu">
                <li><img src="img/menu01.gif"></li>
                <li><img src="img/menu02.gif"></li>
                <li><img src="img/menu03.gif"></li>
                <li><img src="img/menu04.gif"></li>
                <li><img src="img/menu05.gif"></li>
           </ul>
        </nav>
    </header>
    <main>
        <section>
            <img id="banner" src="img/banner01.gif">
            <img id="banner" src="img/banner02.gif">
            <img id="banner" src="img/banner03.gif">
    
            <div class="clear"></div>
        </section>
        <article>
            <img id="custom" src="img/customer_phone.gif">
        </article>
    </main>
</body>
</html>
