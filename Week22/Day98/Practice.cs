테이블 구조 수정해주셨고
우리 조 작업 속도가 빨라서 시나리오도 추가됐음 좋은 건지 나쁜건지 원,,

파싱 변환 업로드 다 switch case 써서 깔끔하게 정리



- 구조체 재정의 및 추가
- DB 테이블 재정의
- IDReport만 만들어뒀었는데 다른 것도 switch case문으로 각각 Report ID가 각 테이블에 올라감
- 폼 종료될 때 스레드 종료하는 코드 넣어서 백그라운드에서 더 돌지 않게 코드 추가
- Start, Cancel 신호 보낼 때 테스트를 위해 MES에서 PROCID, LOTID 정보를 직접 입력했었는데,   시나리오 바뀌면서 LOTID만 Datetime에서 yyyy-mm만 추가되어 자동으로 보내짐
