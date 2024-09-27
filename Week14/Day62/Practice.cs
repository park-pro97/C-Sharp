//MQTT 네트워크 구성 STEP 1   - Phi 보드가 온습도 정보를 MQTT를 통해 Broker에 Publish
// esp32_publish_DHT11.ino
#include <WiFi.h>
#include <PubSubClient.h>
#define PAYLOAD_MAXSIZE 64
// ----- DHT11 ------------------------
#include "DHT.h"
#define DHTPIN 4     
#define DHTTYPE DHT11
DHT dht(DHTPIN, DHTTYPE);
// -------------------------------------
const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char *topic = "MyOffice/Indoor/SensorValue";
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
    delay(1000);
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
   char payload[PAYLOAD_MAXSIZE];
   float h = dht.readHumidity();
   float t = dht.readTemperature();
   if (isnan(h) || isnan(t) ) {
      Serial.println("Failed to read from DHT sensor!");
   return;
   }
   String sPayload = "{'Temp':"
               + String(t, 1)
               + ",'Humi':"
               + String(h, 1)
               + "}";
   sPayload.toCharArray(payload, PAYLOAD_MAXSIZE);
   client.publish(topic, payload);
   Serial.print(String(topic) + " ");
   Serial.println(payload);
   delay(2000);
}
--------------------------------------------------------------
//MQTT 네트워크 구성 STEP 2   - 온습도 정보를 구독하는 프로그램이 InfluxDB에 온습도 정보가 들어감
from influxdb import InfluxDBClient	#file name : SubHumiTempInsetMod.py
import paho.mqtt.client as mqtt
dbClient = InfluxDBClient(host=‘localhost’, port=8086, username=‘influx_ship’, password=‘1234’, database=‘db_riatech’)

def on_connect( client, userdata, flags, reason_code, properties):
   print(“Connect with result code “ + str(reason_code) )
   client.subscribe(“MyOffice/Indoor/#”)

def on_message( client, userdata, msg ):
   print( msg.topic +” “+str(msg.payload) )
   # msg.topic  ‘MyOffice/Indoor/SensorValue’
   topic = msg.topic.split(‘/’)
   loc = topic[1]
   subloc = topic[2]
   # msg.payload  “{‘Temp’ : 23.1, ‘Humi’: 33.3}”
   payload = eval(msg.payload)

   json_body = [ ]
   data_point = {
	‘measurement’: ‘sensors’,
	‘tags’: {‘Location’: ‘ ‘, ‘SubLocation’:’ ‘},
	‘fields’: {‘Temp’: 0.0, ‘Humi’:0.0}
	}
   data_point[‘tags’][‘Location’] = loc
   data_point[‘tags’][‘SubLocation’] = subloc
   data_point[‘fields’][‘Temp’] = payload[‘Temp’]
   data_point[‘fields’][‘Humi’] = payload[‘Humi’]
   json_body.append(data_point)
   dbClient.write_points( json_body )

mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.username_pw_set(username=“mqtt_boy”, password=“1234”)
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect(“localhost”, 1883, 60)
mqttc.loop_forever( )
