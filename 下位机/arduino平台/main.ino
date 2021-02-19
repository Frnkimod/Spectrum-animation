#include <Arduino.h>
#include <SPI.h>
#include <U8g2lib.h>
#include <time.h>


U8G2_SSD1306_128X64_NONAME_F_HW_I2C u8g2(U8G2_R2, /* reset=*/U8X8_PIN_NONE);

float a=0;
int ReadData[128];
void setup()
{
  Serial.begin(115200);
  u8g2.begin();
}
void loop()
{
    //srand((unsigned)time(NULL));
    //u8g2.clearBuffer();
    
    u8g2.firstPage();
    do {
      //display_fft_2();
     display_fft_1();
    }while(u8g2.nextPage());
    //u8g2.sendBuffer();
    //delay(60
}
void display_fft_1()
{
  int buffer[64];
  int x; 
  char buffer_int[64][80];
  int a=split(buffer_int,char(Serial.read()),",");
  
	  for (int i = 32; i > 0; i--)
    {
      
      if(Serial.available() <= 0)
      {
        return;
      }
    
         
      u8g2.drawBox(x=i<<2,0,2,10);      
    }
  
}
int split(char dst[][80], char* str, const char* spl)
{
    int n = 0;
    char *result = NULL;
    result = strtok(str, spl);
    while( result != NULL )
    {
        strcpy(dst[n++], result);
        result = strtok(NULL, spl);
    }
    return n;
}