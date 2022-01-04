#include <LWiFi.h>
 
const char* ssid = "TELDAP";
//const char* ssid = "TELDAP";
const char* password =  "TELDAP4F";
 
const uint16_t port = 7100;
const char * host = "192.168.0.7";
bool first = true;
bool s_connect = false;
WiFiClient client;
void setup()
{
  pinMode(2,INPUT);//service_ring
  pinMode(3,INPUT);//order_ring
  pinMode(4,INPUT);//leave_ring
  Serial.begin(9600);
  
 
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.println("...");
  } 
}
 
void loop()
{

    if(client.connected()){
      //按按鈕傳指令(JSON)給server
      if(HIGH == digitalRead(2)){
        String sendserviceStr = "{\"type\":\"service\", \"table_id\":\"12\"}\n";
        client.print(sendserviceStr);
         Serial.println(sendserviceStr);
      }
      else if(HIGH == digitalRead(3)){
        String sendorderStr = "{\"type\":\"order\",  \"table_id\":\"12\"}\n";
        client.print(sendorderStr);
         Serial.println(sendorderStr);
      }
      else if(HIGH == digitalRead(4)){
         String sendleaveStr = "{\"type\":\"leave\", \"table_id\":\"12\"}\n";
        client.print(sendleaveStr);
         Serial.println(sendleaveStr);
      }
    }
    else{
      if(!client.connect(host, port)) {
        Serial.println("Connection to host failed");
        delay(1000);
        return;
      }else{
        Serial.println("Connected to server successful!");
      }
    }
}
