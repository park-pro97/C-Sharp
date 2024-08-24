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
//

