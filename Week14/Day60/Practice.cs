//Phi 보드를 Subscriber로 만들어보기
// esp32_subscribe_DHT11.ino
#include <WiFi.h>
#include <PubSubClient.h>
const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
const char *topic = "MyOffice/Indoor/#";
const char* serverIPAddress = "192.168.1.11";
char messageBuf[100];

void callback(char* topic, byte* payload, unsigned int length) { 
  Serial.println("Message arrived!\nTopic: " + String(topic));
  Serial.println("Length: "+ String(length, DEC));
 
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  Serial.println("Payload: "+ String(messageBuf) + "\n\n");
}

WiFiClient wifiClient; 

PubSubClient client(serverIPAddress, 1883, callback, wifiClient);
void setup() {
   Serial.begin(115200);
   Serial.print("\nConnecting to "); Serial.println(ssid);
   WiFi.begin(ssid, password);
   while (WiFi.status() != WL_CONNECTED) {
      Serial.print("."); delay(500);
   }
   Serial.println("\nWiFi Connected\nConnecting to broker");
   while ( !client.connect(clientId, userId, userPw) ){ 
      Serial.print("*"); delay(500);
   }
   Serial.println("\nConnected to broker");
   client.subscribe(topic);
}
void loop() {
   client.loop();
}
--------------------------------------------------------------
//Phi 보드 LED 점등
// esp32_subscribe_LED.ino
#include <WiFi.h>
#include <PubSubClient.h>
const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = “mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char *topic = “MyOffice/Indoor/Lamp";
char* server = "192.168.1.11"; 
char messageBuf[100];

void callback(char* topic, byte* payload, unsigned int length) {  
  Serial.println("Message arrived!\nTtopic: " + String(topic));
  Serial.println("Length: "+ String(length, DEC));
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  String ledState = String(messageBuf);
  Serial.println("Payload: "+ ledState + "\n\n");
  if( ledState == “0"  ){ digitalWrite(LED_BUILTIN, LOW);
  } else if (ledState==“1") { digitalWrite(LED_BUILTIN, HIGH);
  } else { Serial.println("Wrong Message"); }
}


WiFiClient wifiClient; 
PubSubClient client(server, 1883, callback, wifiClient);

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(115200);
  
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");     delay(1000);
  }
  Serial.println("\nWiFi Connected");
 
  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*");     delay(1000);
  }
  Serial.println("\nConnected to broker");
  Serial.println(String("Subscribing! topic = ") + topic);
  client.subscribe(topic);
 }

void loop() {   client.loop(); }
--------------------------------------------------------------
//Phi 보드를 Subscriber & Publisher 다 수행하게 만들어보기
// Phi_SubPub.ino
#include <WiFi.h>
#include <PubSubClient.h>
#include <Wire.h>
#include <BH1750.h>

BH1750 lightMeter;

const char* ssid = "RiatechA2G";
const char* password = "730124go";

const char* userId = “mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;

char *topicSub = “MyOffice/Indoor/lamp";
char *topicPub = “MyOffice/Indoor/Lux";

char* server = "192.168.1.11"; 
char messageBuf[100];

void callback(char* topic, byte* payload, unsigned int length) {  
  Serial.println("Message arrived!\nTtopic: " + String(topic));
  Serial.println("Length: "+ String(length, DEC));
  
  strncpy(messageBuf, (char*)payload, length);
  messageBuf[length] = '\0';
  String ledState = String(messageBuf);
  Serial.println("Payload: "+ ledState + "\n\n");

  if( ledState == “1"  ){      
    digitalWrite(LED_BUILTIN, LOW);
  } else if (ledState==“0") { 
    digitalWrite(LED_BUILTIN, HIGH);
  } else { 
    Serial.println("Wrong Message");
  }
}

WiFiClient wifiClient; 
PubSubClient client(server, 1883, callback, wifiClient);

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(115200);
  Wire.begin();
  lightMeter.begin();

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(1000);
  }
  Serial.println("\nWiFi Connected");
 
  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*");
    delay(1000);
  }
  Serial.println("\nConnected to broker");
  Serial.print("Subscribing! topic = ");
  Serial.println(topicSub);
  client.subscribe(topicSub);
 }


void loop() {
  client.loop();

  char buf[20];
  String strLuxValue =  String( lightMeter.readLightLevel() );
  strLuxValue.toCharArray(buf, strLuxValue.length() );
  client.publish(topicPub, buf);
  Serial.println(String(topicPub) + " : " + buf);

  delay(7000);
}
