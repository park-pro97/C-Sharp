//Mosquitto Broker 설치
$ cd
$ sudo apt-get update && sudo apt-get upgrade -y
$ sudo apt-get install mosquitto mosquitto-clients
-----------------------------------------------------
//Subscribe 프로그램
import paho.mqtt.client as mqtt

def on_connect( client, userdata, flags, rc ):
   print(“Connect with result code “ + str(rc) )
   client.subscribe(“temp”)

def on_message( client, userdata, msg ):
   print( msg.topic +” “+str(msg.payload) )

mqttc = mqtt.Client( )
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect(“localhost”, 1883, 60)
mqttc.loop_forever( )
|--------------------------|
import paho.mqtt.client as mqtt

def on_connect( client, userdata, flags, reason_code, properties ):
   print(f“Connect with result code:{reason_code}“)
   client.subscribe(“temp”)

def on_message( client, userdata, msg ):
   print( msg.topic +” “+str(msg.payload) )

mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.on_connect = on_connect
mqttc.on_message = on_message
mqttc.connect(“localhost”, 1883, 60)
mqttc.loop_forever( )
-----------------------------------------------------
//Publish 프로그램
import paho.mqtt.client as mqtt
mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
mqttc.connect(“localhost”, 1883, 60)
mqttc.publish(‘temp’, 25.1)
|--------------------------|
#file name : pubBasic2.py
import paho.mqtt.publish as publish
publish.single(“temp”, 21.1, hostname=“localhost”)
