//Subscribe 프로그램 테스트1
import paho.mqtt.client as mqtt

def on_connect( client, userdata, flags, reason_code, properties ):
   print(f“Connect with result code:{reason_code}“)
   client.subscribe(“temp”)

def on_message( client, userdata, msg ):
   print( msg.topic +” “+str(msg.payload) )

mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.username_pw_set(username=“mqtt_boy”, password=“1234”)
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect(“localhost”, 1883, 60)
mqttc.loop_forever( )
-----------------------------------------------------
//Subscribe 프로그램 테스트2
import paho.mqtt.client as mqtt

def on_connect( client, userdata, flags, reason_code, properties ):
   print(f“Connect with result code:{reason_code}“)
   client.subscribe(“temp”)

def on_message( client, userdata, msg ):
   print( msg.topic +” “+str(msg.payload) )

mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.username_pw_set(username=“mqtt_boy”, password=“1234”)
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect(“broker_IP_Address”, 1883, 60)
mqttc.loop_forever( )
-----------------------------------------------------
//Phi 보드를 Publisher로 동작
#include <WiFi.h>
const char* ssid = "RiatechA2G";
const char* password = "730124go";

void setup() {
  Serial.begin(115200);
 
  Serial.print("\nConnecting to ");
  Serial.println(ssid);

  // 해당 공유기에 접속 시도
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }
  Serial.println("\nWiFi Connected");
  
  // 공유기로부터 할당 받은 "(사설)IP 주소" 출력
  Serial.print("Local IP address : ");
  Serial.println(WiFi.localIP());}

void loop() {}
-----------------------------------------------------
//Phi 보드를 MQTT broker에 연결
#include <WiFi.h>
#include <PubSubClient.h>
const char* ssid = "RiatechA2G";
const char* password = "730124go";

const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char* server = "192.168.1.11"; 

WiFiClient wifiClient; 
PubSubClient client(server, 1883, wifiClient);

void setup() {
  Serial.begin(115200);
 
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }
  Serial.println("\nWiFi Connected");
  Serial.print("Local IP address : ");
  Serial.println(WiFi.localIP());  
  Serial.println("Connecting to broker");
  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*");
    delay(500);
  }
  Serial.println("\nConnected to broker");
}

void loop() {}
