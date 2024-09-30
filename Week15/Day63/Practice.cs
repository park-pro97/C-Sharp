//아두이노
#include <WiFi.h>
#include <PubSubClient.h>

// WiFi 및 MQTT 설정
const char* ssid = "pcroom";
const char* password = "12345678";

const char* userId = "mqtt_ship";
const char* userPw = "1234";
const char* clientId = userId;
char* topic = "MyOffice/Indoor/Lamp";
char* server = "192.168.0.43";
char messageBuf[100];

// 1. callback 함수 - 메시지가 수신되면 실행 (코드의 상단에 위치)
void callback(char* topic, byte* payload, unsigned int length) {
  Serial.println("Message received in callback!");
  Serial.println("Topic: " + String(topic));
  Serial.println("Payload length: " + String(length, DEC));

  // 메시지 버퍼를 문자열로 변환
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  String ledState = String(messageBuf);

  // 수신한 메시지를 시리얼 모니터에 출력
  Serial.println("Payload: " + ledState);
  
  // LED 상태 제어
  if (ledState == "1") {
    Serial.println("Turning LED ON (LED_BUILTIN)");
    digitalWrite(LED_BUILTIN, LOW);  // 내장 LED 켜기 (LOW가 켜는 상태)
  } else if (ledState == "0") {
    Serial.println("Turning LED OFF (LED_BUILTIN)");
    digitalWrite(LED_BUILTIN, HIGH); // 내장 LED 끄기 (HIGH가 끄는 상태)
  } else {
    Serial.println("Unknown message received");
  }
}

// WiFi 및 MQTT 클라이언트 설정
WiFiClient wifiClient;
PubSubClient client(server, 1883, callback, wifiClient); // 여기서 callback 함수 사용

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);    // 내장 LED를 출력 모드로 설정
  digitalWrite(LED_BUILTIN, HIGH); // 처음에 LED를 꺼둠 (HIGH로 설정)

  Serial.begin(115200);
  
  // WiFi 연결 시도
  Serial.print("Connecting to WiFi...");
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(1000);
  }
  Serial.println("\nWiFi Connected");

  // MQTT 브로커 연결 시도
  Serial.print("Connecting to MQTT broker...");
  while (!client.connect(clientId, userId, userPw)) {
    Serial.print("*");
    delay(1000);
  }
  Serial.println("\nConnected to broker");

  // MQTT 토픽 구독
  client.subscribe(topic);
}

void loop() {
  client.loop(); // MQTT 메시지 대기 및 처리
}
------------------------------------------------------------------
//라즈베리파이
from flask import Flask, render_template
import paho.mqtt.client as mqtt

app = Flask(__name__)

# MQTT
mqtt_broker = "192.168.0.43"
mqtt_topic = "MyOffice/Indoor/Lamp"
mqtt_client = mqtt.Client("web_client")
mqtt_client.username_pw_set("mqtt_girl", "1234")
mqtt_client.connect(mqtt_broker, 1883, 60)

@app.route("/")
def index():
    return render_template('index.html')

@app.route("/on")
def turn_on():
    mqtt_client.publish(mqtt_topic, "1")
    return "LED On"

@app.route("/off")
def turn_off():
    mqtt_client.publish(mqtt_topic, "0")
    return "LED Off"

if __name__ == "__main__":
    app.run(host='0.0.0.0', port=5000)

-------------------------------------------                                               
//웹
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ESP32 LED Control</title>
</head>
<body>
    <h1>Control LED</h1>
    <button onclick="controlLED('on')">Turn On</button>
    <button onclick="controlLED('off')">Turn Off</button>

    <script>
        function controlLED(action) {
            fetch('/' + action)
            .then(response => response.text())
            .then(data => console.log(data))
            .catch(error => console.error('Error:', error));
        }
    </script>
</body>
</html>
