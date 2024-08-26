//JS 첫 번째 코드를 작성
<!DOCTYPE html>
<html>
<head>
    <script>
        /* alert = 팝업 창 */
        /* 서버를 들어갈 때 팝업되는 형태 */
        alert('Hello World!');
    </script>
</head>
<body>
    
</body>
</html>

/* --------------------------------------------- */

<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <p id="name"> </p>
    <script>
        /* 아이디를 부여하여 브라우저에 기입 */
        document.getElementById('name').innerHTML='Hello World';
    </script>
</body>
</html>
[ script를 body에 넣어도 동일하게 팝업 창이 뜬다 ]
------------------------------------------------------------
//출력 및 문자열
[ 팝업 상에서 두 코드가 정상적으로 출력되는 지 확인 ]
<script>
        /* 둘 다 정상적으로 팝업 */
        alert('This is "string"');
        alert("This is 'string'");
    </script>
------------------------------------------------------------
//JS에서 a + b가 잘 나오는지 [잘 나옴]
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <p id="name"> </p>
    <script>
        var a = 100;
        var b = 200;
        document.getElementById('name').innerHTML= a + b;
    </script>
</body>
</html>
------------------------------------------------------------
//팝업 상에서 정상적으로 pi와 value1의 값 출력
<!DOCTYPE html>
<html>
<head>
    <script>
        let pi = 3.141592;
        let value1 = 7;

        let result = pi * value1;
        alert(result);
    </script>
</head>
<body>
    <p id="ppp"> </p>
</body>
</html>
------------------------------------------------------------
//JS 문자열 & 숫자
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        alert('500' + '3.141592');
    </script>
</body>
</html>
------------------------------------------------------------
//PROMPT를 활용한 숫자 입력 받아 계산하는 코드
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        var value1 = prompt('첫번째 숫자를 입력해 주세요.', '0');
        var value2 = prompt('두번째 숫자를 입력해 주세요.', '0');

        alert(Number(value1) + Number(value2));
    </script>
</body>
</html>
------------------------------------------------------------
//javascript에서 복합 대입 연산자를 활용한 list 구문
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        // 페이지 열거나 웹에서 열면 
        window.onload = function() {
            var list = '';

            list += '<ul>';
            list += '   <li>Hello</li>'
            list += '   <li>JavaScript</li>'
            list += '</ul>';

            // 따옴표 안에 적을 내용이 너무 많으므로 변수화
            document.body.innerHTML= list;
        };
    </script>
</body>
</html>
------------------------------------------------------------
//avascript 상에서 배열을 사용
<!DOCTYPE html>
<html>
<head>
</head>
<body>
    <script>
        var array1 = [273, 32, 103, 57, 52];
        // 대괄호 안의 대괄호 = Object
        var array2 = [273, 'Hello', true, function(){}, [273, 103]];
        var array3 = new array3(10, 20, 30);

        alert(array2); // array2가 전부 정상출력
        alert(array1[0]); // array1의 첫 번째 요소 출력
    </script>
</body>
</html>
------------------------------------------------------------
//JS 함수 생성 및 출력
<!DOCTYPE html>
<html>
<head>
    <script>
        // 함수 내용
        var func1 = function(){
            var output = prompt('숫자를 입력해 주세요.','숫자');
            // 함수 안에 함수가 들어가있는 꼴
            alert(output);
        };
        // 함수 동작
        func1();
    </script>
</head>
<body>
    
</body>
</html>

// ----------- 아래와 위는 동일한 결과 --------------------------

<!DOCTYPE html>
<html>
<head>
    <script>
        // 함수 내용
        var func1 = function(){
            var output = prompt('숫자를 입력해 주세요.','숫자');
            // 함수 안에 함수가 들어가있는 꼴
            alert(output);
        };
    </script>
</head>
<body>
    <script>
        func1();
    </script>
</body>
</html>
------------------------------------------------------------
//객체와 배열 사용
<!DOCTYPE html>
<html>
<head>
</head>
<body>
    <script>
        var product={
            // 앞은 키, 뒤는 Value
            제품명: '7D 건조 망고', 
            유형: '당절임',
            성분: '망고, 설탕, 메타중아황산나트륨, 차지황색소',
            원산지: '필리핀'
        };
        alert(product['제품명']);
    </script>
</body>
</html>
------------------------------------------------------------
//for문으로 브라우저 화면에 전체 속성을 출력할때
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        //선언
        var product = {
            name: 'Microsoft Visual Studio 2012 Ultimate',
            price: '15,000,000원',
            language: '한국어',
            supportOS: 'Win 32/64',
            subscription: true
        };

        //출력
        var output = '';
        for (var key in product){
            output += '●' + key + ': ' + product[key] + '\n';
        }
        alert(output);
    </script>
</body>
</html>
------------------------------------------------------------
//위 코드를 document와 list를 사용하여 표현
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        // 변수 선언
        var product = {
            name: 'Microsoft Visual Studio 2012 Ultimate',
            price: '15,000,000원',
            language: '한국어',
            supportOS: 'Win 32/64',
            subscription: true
        };

        // 출력
        var list = '<ul>';
        for (var key in product){
            list += '<li>● ' + key + ': ' + product[key] + '</li>';
        }
        list += '</ul>';

        // 웹 페이지에 출력
        document.body.innerHTML = list;
    </script>
</body>
</html>
------------------------------------------------------------
//새로운 window 객체 생성
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        var i = 0;
        for (i=0; i<3; i++){
            window.open();
        }
    </script>
</body>
</html>
------------------------------------------------------------
//지정한 사이즈 크기로 네이버 창이 열림
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        window.open('http://naver.com', 'child', 'width=600, height=300', true);
    </script>
</body>
</html>
------------------------------------------------------------
//창을 새로 만들어 텍스트를 넣고 출력
<!DOCTYPE html>
<html>
<head>
</head>
<body>
    <script>
        var child = window.open('','','width=300, height=200');

        child.document.write('<h1>부모 윈도우로부터 탄생!</h1>');
    </script>
</body>
</html>
------------------------------------------------------------
//문서 객체 모델의 예시 코드
<!DOCTYPE html>
<html>
<head>
    <script>
        window.onload = function() {
		        // createElement와 createTextNode의 사용법
            var header = document.createElement('h1');
            var textNode = document.createTextNode('Hello DOM');

            // 노드 연결
            header.appendChild(textNode);
            document.body.appendChild(header);
        };
    </script>
</head>
<body>

</body>
</html>
------------------------------------------------------------
//DOM을 활용해서 이미지를 웹 상에서 출력 가능
<!DOCTYPE html>
<html>
<head>
    <script>
        window.onload = function() {
            var img = document.createElement('img');
            img.src = "./image1.jpg";
            img.width=150; img.height=150;

            document.body.appendChild(img);
        };
    </script>
</head>
<body>

</body>
</html>
------------------------------------------------------------
//innerHTML 활용 리스트 출력
<!DOCTYPE html>
<html>
<head>
    
</head>
<body>
    <script>
        var output = '';
        output += '<ul>';
        output += ' <li>봄</li>';
        output += ' <li>여름</li>';
        output += ' <li>가을</li>';
        output += ' <li>겨울</li>';
        output += '</ul>'

        // document.body.textContent = output;
        document.body.innerHTML = output;
    </script>
</body>
</html>
------------------------------------------------------------
//CSS Color를 red로 설정하고 CDN를 사용한 코드
<!DOCTYPE html>
<html>
<head>
    <title>jQuery</title>

    <script src="https://code.jquery.com/jquery-3.7.1.js" 
        integrity="sha256-eKhayi8LEQwp4NKxN+CfCh+3qOVUtJn3QNZ0TciWLP4=" 
        crossorigin="anonymous">
    </script>

    <script>
        // 모든 css color를 red로 하겠다는 의미
        $(document).ready(function(){
            $('*').css('color', 'red');
        });
    </script>
</head>
<body>
    <h1>안녕하세요.</h1>
</body>
</html>
------------------------------------------------------------
//jQuery Mobile [ 모바일 환경에서도 사용할 수 있도록 가볍게 만들어진 jQuery ]
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <title>jQueryMoblie</title>

    <script src="https://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.js" 
        integrity="sha256-1PYCpx/EXA36KN1NKrK7auaTylVyk01D98R7Ccf04Bc=" 
        crossorigin="anonymous">
    </script>
    
</head>
<body>
    <a href="#" class="ui-btn">Anchor</a>
    <button class="ui-btn">Button</button>
</body>
</html>
