//Digital Output – LED_BULITIN
void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
}
void loop() {
  digitalWrite(LED_BUILTIN, HIGH);  
  delay(1000);                      
  digitalWrite(LED_BUILTIN, LOW);   
  delay(1000);                      
}
----------------------------------------------------
//ADC OneShot mode   AnalogRead()
void setup() {
   Serial.begin(115200);
}
void loop() {
   Serial.println( analogRead(A4) );
   delay(500);
}
----------------------------------------------------
//ADC OneShot mode   AnalogReadMilliVolts()
void setup() {
   Serial.begin(115200);
}
void loop() {
   Serial.println( analogRead(A4) );
   Serial.println( analogReadMilliVolts(A4) );
   delay(500);
}
----------------------------------------------------
//ADC OneShot mode   AnalogReadSolution()
void setup() {
   Serial.begin(115200);
   analogReadResolution(10);
}
void loop() {
   Serial.println( analogRead(A4) );
   Serial.println( analogReadMilliVolts(A4) );
   delay(500);
}
----------------------------------------------------
//dacWrite( )
void setup() {
   Serial.begin(115200);
   unsigned long int startTime = micros();
   dacWrite(25, 64);
   Serial.println(micros() - startTime);
}
void loop() {}
}
----------------------------------------------------
//PWM 신호 출력
#define CH0_LED_PIN1 17
#define CH0_LED_PIN2 16
uint32_t freq = 5000;         // T = 1/f = 1/5 [ms] = 0.2[ms] = 200[us]
uint8_t resolution10 = 10;   // 10 bit
void setup() {  
   ledcAttach(CH0_LED_PIN1, freq, resolution10);
   ledcAttach(CH0_LED_PIN2, freq, resolution10);   
   ledcWrite(CH0_LED_PIN1, 255);    // Resolution 10 bit(0 ~ 1023) : 255는 1/4
   ledcWrite(CH0_LED_PIN2, 511);    // Resolution 10 bit(0 ~ 1023) : 511는 1/2
}
void loop() {
}
----------------------------------------------------
//OLED에 Hello World 출력
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 64 // OLED display height, in pixels
#define OLED_RESET     -1
#define SCREEN_ADDRESS 0x3C
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);
void setup() {
   Serial.begin(9600);
   if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
      Serial.println(F("SSD1306 allocation failed"));
      for(;;); // Don't proceed, loop forever
   }
   display.clearDisplay();
   display.setTextColor(WHITE);
   display.println("Hello World!");
   display.display();
}
void loop() { }
----------------------------------------------------
//OLED Elisped Time
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 64 // OLED display height, in pixels
#define OLED_RESET     -1
#define SCREEN_ADDRESS 0x3C
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);
void setup() {
   Serial.begin(9600);
   if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
      Serial.println(F("SSD1306 allocation failed"));
      for(;;); // Don't proceed, loop forever
   }
   display.clearDisplay();
   display.setTextColor(WHITE);
}
void loop() { 
   display.clearDisplay();
   display.setCursor(0, 0);
   display.setTextSize(2);
   display.print("Time");
   
   display.setCursor(0, 20);
   display.setTextSize(1);
   display.print("Elisped time : ");
   display.print(millis()/1000);
   display.print("[s]");
   display.display();
   delay(1000);
}
----------------------------------------------------
//GPIO Interrupt
#define  PIR      36
volatile bool motionDetectTag = false;
void IRAM_ATTR isrMotionDetect(void) {
   motionDetectTag = true;
}
void setup() {
   Serial.begin(115200);
   pinMode(PIR, INPUT);
   attachInterrupt(PIR, isrMotionDetect, RISING);
}
void loop() {
   if (motionDetectTag) {
      motionDetectTag = false;
      Serial.println("Motion detected!");
   }
   delay(10);
}
----------------------------------------------------
//Bluetooth 예제 파일
#include "BluetoothSerial.h"
String device_name = "ESP32-BT-Slave XXX";BluetoothSerial SerialBT;
void setup() {
  Serial.begin(115200);
  SerialBT.begin(device_name); 
  Serial.printf("The device with name \"%s\" is started.");
  Serial.print("\nNow you can pair it with Bluetooth!\n", device_name.c_str());
}
void loop() {
  if (Serial.available()) {
    SerialBT.write(Serial.read());
  }
  if (SerialBT.available()) {
    Serial.write(SerialBT.read());
  }
  delay(20);
}
----------------------------------------------------
//Time Interrupt
hw_timer_t *timer0 = NULL;
volatile bool has_expired = false;
void IRAM_ATTR isr_timer0Interrupt() {
  has_expired = true;
}
void setup() {
  Serial.begin(115200);
  pinMode(LED_BUILTIN, OUTPUT);
  // Set timer frequency to 1MHz
  timer0 = timerBegin(1000000); 
  // Attach isr_timer0Interrupt to our Timer0. 
  timerAttachInterrupt(timer0, &isr_timer0Interrupt); 
  // Set alarm to call isr_timer0Interrupt every second(value in microseconds)
  // Repeat the alarm (third parameter) with unlimited count = 0(fourth parameter)
  timerAlarm(timer0, 1000000, true, 0);
}
void loop() {
  if (has_expired){
    digitalWrite(LED_BUILTIN, !digitalRead(LED_BUILTIN));
    has_expired = false;
  }
}
----------------------------------------------------
//Real Time Clock
void setup() {
  Serial.begin(9600);
  Serial.print(__DATE__);
  Serial.print(" ");
  Serial.println(__TIME__);
}
void loop() {
}
----------------------------------------------------
//NTP 예제 파일
#include <NTPClient.h>
#include <WiFi.h>
#include <WiFiUdp.h>
const char *ssid     = "RiatechA2G";
const char *password = "730124go";

WiFiUDP ntpUDP;
NTPClient timeClient(ntpUDP);
void setup(){
  Serial.begin(115200);  WiFi.begin(ssid, password);
  while ( WiFi.status() != WL_CONNECTED ) {
    delay ( 500 );
    Serial.print ( "." );
  }  timeClient.begin();
}
void loop() {
  timeClient.update();
  Serial.println(timeClient.getFormattedTime());
  delay(1000);
}
