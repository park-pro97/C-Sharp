//아두이노
// Phi_Publish_DHT11.ino, BH1750
#include <WiFi.h>
#include <PubSubClient.h>
#include <BH1750.h>
#include <Wire.h>
#define PAYLOAD_MAXSIZE 64

BH1750 lightMeter;

// ----- DHT11 ------------------------
#include "DHT.h"
#define DHTPIN 4     
#define DHTTYPE DHT11  //DHT11
DHT dht(DHTPIN, DHTTYPE);
// -------------------------------------

const char* ssid = "RiatechA2G";
const char* password = "730124go";
const char* userId = "mqtt_boy";
const char* userPw = "1234";
const char* clientId = userId;
char *topic = "MyOffice/Indoor/SensorValue";
char* server = "192.168.1.18";

WiFiClient wifiClient; 
PubSubClient client(server, 1883, wifiClient);

void setup() {
  Serial.begin(9600);
  Serial.print("\nConnecting to ");
  Serial.println(ssid);

  
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print("."); delay(500);
  }
  Serial.println("\nWiFi Connected\nConnecting to broker");

  while ( !client.connect(clientId, userId, userPw) ){ 
    Serial.print("*"); delay(500);
  }
  Serial.println("\nConnected to broker");

  Wire.begin();
  lightMeter.begin();
  dht.begin();
  Serial.println(F("BH1750 Test begin"));
}

void loop() {
  char payload[PAYLOAD_MAXSIZE];
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  float lux = lightMeter.readLightLevel();

  if (isnan(h) || isnan(t) ) {
    Serial.println("Failed to read from DHT sensor!");
    return;
  }

  String sPayload = "{'Temp':"
               + String(t, 1)
               + ",'Humi':"
               + String(h, 1)
               + ",'Lux':"
               + String(lux, 1)
               + "}";
   sPayload.toCharArray(payload, PAYLOAD_MAXSIZE);
   client.publish(topic, payload);
   Serial.print(String(topic) + " ");
   Serial.println(payload);
   delay(2000);
}
-------------------------------------------------------
//라즈베리파이
from influxdb import InfluxDBClient

import paho.mqtt.client as mqtt
dbClient = InfluxDBClient(host='localhost', port=8086, username='influx_ship', password='1234', database='db_riatech')


def on_connect( client, userdata, flags, reason_code, properties ):
   print(f"Connect with result code:{reason_code}")
   client.subscribe("MyOffice/Indoor/#")

def on_message( client, userdata, msg ):
   print( msg.topic +" "+str(msg.payload) )
   topic = msg.topic.split('/')
   loc = topic[1]
   subloc = topic[2]
   payload = eval(msg.payload)
   json_body =[]
   data_point = {
        'measurement': 'sensors', #dbTableName
        'tags': {'Location':' ', 'SubLocation':' '},
        'fields': {'Temp':0.0, 'Humi':0.0, 'Lux':0.0}
        }

   data_point['tags']['Location'] = loc
   data_point['tags']['SubLocation'] = subloc
   data_point['fields']['Temp'] = payload['Temp']
   data_point['fields']['Humi'] = payload['Humi']
   data_point['fields']['Lux'] = payload['Lux']

   json_body.append(data_point)
   dbClient.write_points( json_body )

mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.username_pw_set(username="mqtt_ship", password="1234")
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect("localhost", 1883, 60)
mqttc.loop_forever( )
