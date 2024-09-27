//InfluxDB에 데이터 넣어보기   - Python 코드
client = InfluxDBClient( host = ‘localhost’, port=8086, username=‘userid’, password=‘userpassword’, database=‘databasename’ )
==============================
json_body = [
	{
	   “measurement” : “temperature”,
	   “tags” : { “Location” : “Indoor”},  
                  “fields” : {‘Temp’: 0.0 }
	} 
             ]
client.write_points(json_body)
--------------------------------------------------------------
//데이터베이스에 데이터를 넣어보기 [2]
from influxdb import InfluxDBClient
import time
import random

client = InfluxDBClient( host='localhost', port=8086, 
	         username=‘influx_ship', password=‘1234',  database='db_riatech’)

def randomDataPoint():
    json_body=[]
    data_point =   {   'measurement' : ‘sensors',
                       'tags' : { 'Location' : 'Indoor'},  # 'outdoor'
                       'fields' : {'Temp': 0.0, 'Humi' : 0.0 }
                    }  
    
    data_point['fields']['Temp'] = random.random() * 50.0
    data_point['fields']['Humi'] = random.random() * 30.0
    
    if (random.random() > 0.5):
        data_point['tags']['Location'] = 'Indoor'
    else:
        data_point['tags']['Location'] = 'Outdoor'
    
    json_body.append(data_point)
    return json_body

//파일이름 : insertBasicRandom.ipynb

while True:
    json_body = randomDataPoint()
    print(json_body)
    client.write_points( json_body )
    time.sleep(5)
--------------------------------------------------------------
//데이터베이스에 데이터를 가져오기
client = InfluxDBClient( host = ‘localhost’, port=8086, username=‘userid’, password=‘userpassword’, database=‘databasename’ )
--------------------------------------------------------------
//Querying Data
results = client.query( ‘select * from My_office’) 
print(results.raw)
--------------------------------------------------------------
//DHT11을 이용한 온습도 정보 가져오기   STEP 1
//Phi_Publish_DHT11.ino
#include <ESP8266WiFi.h>
#include <PubSubClient.h>

// ----- DHT11 ------------------------ //
#include "DHT.h"
#define DHTPIN D4     
#define DHTTYPE DHT11  //DHT11
DHT dht(DHTPIN, DHTTYPE);
// ------------------------------------- //

const char* ssid = "RiatechA2G";
const char* password = "730124go";

const char* clientId = “mqtt_boy";
const char* userId = clientId;
const char* userPw = "1234";
char *topic_t = “Sensors/MyOffice/Indoor/temp";
char *topic_h = “Sensors/MyOffice/Indoor/humi";
char* server = "192.168.1.11"; 

WiFiClient wifiClient; 
PubSubClient client(server, 1883, wifiClient);

void setup() {
  Serial.begin(115200);
 
  Serial.print("\nConnecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }
  Serial.println("\nWiFi Connected");
  
  Serial.println("Connecting to broker");
  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*");
    delay(1000);
  }
  Serial.println("\nConnected to broker");
  dht.begin();
}

void loop() {
  char buf[20];
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  if (isnan(h) || isnan(t) ) {
    Serial.println("Failed to read from DHT sensor!");
    return;
  }

  String str = String(h);
  str.toCharArray(buf, str.length());
  client.publish(topic_h, buf);
  Serial.println(String(topic_h) + " : " + buf);
  
  str = String(t);
  str.toCharArray(buf, str.length());
  client.publish(topic_t, buf);
  Serial.println(String(topic_t)  + " : " + buf);
  delay(2000);
}
--------------------------------------------------------------
// STEP 2   [ Broker가 실행 중인 Raspberry 보드에서는  Subscriber 프로그램이 Broker로 부터 온습도 정보 ]
#file name : SubHumiTemp.py
import paho.mqtt.client as mqtt

def on_connect( client, userdata, flags, rc ):
   print(“Connect with result code “ + str(rc) )
   client.subscribe(“Sensors/MyOffice/#”)

def on_message( client, userdata, msg ):
   print( msg.topic +” “+str(msg.payload) )

client = mqtt.Client( )
client.username_pw_set(username=“mqtt_riatech”, password=“1234”)
client.on_connect = on_connect
client.on_message = on_message
client.connect(“localhost”, 1883, 60)
client.loop_forever( )
--------------------------------------------------------------
// STEP 3 [ 온습도 정보를 구독하는 프로그램이 InfluxDB에 온습도 정보가 들어감 ]
from influxdb import InfluxDBClient
import paho.mqtt.client as mqtt
dbClient = InfluxDBClient(host=‘localhost’, port=8086, username=‘influx_phirippa’, password=‘1234’,
		database=‘db_riatech’)
def on_connect( client, userdata, flags, rc ):
   print(“Connect with result code “ + str(rc) )
   client.subscribe(“Sensors/MyOffice/#”)
def on_message( client, userdata, msg ):
   print( msg.topic +” “+str(msg.payload) )
   topic = msg.topic.split(‘/’)
   json_body = [ ]
   data_point = {
	‘measurement’: ‘My_office’,
	‘tags’: {‘Location’: ‘ ‘},
	‘fields’: {‘Temp’: 0.0, ‘Humi’:0.0}
	}
   data_point[‘tags’][‘Location’] = topic[1]
   data_point[‘tags’][‘SubLocation’] = topic[2]
   data_point[‘fields’][topic[3]] = float(msg.payload)
   json_body.append(data_point)
   dbClient.write_points( json_body )
   
client = mqtt.Client( )
client.username_pw_set(username=“mqtt_riatech”, password=“1234”)
client.on_connect = on_connect
client.on_message = on_message
client.connect(“localhost”, 1883, 60)
client.loop_forever( )
