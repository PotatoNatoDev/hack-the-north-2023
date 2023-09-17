#include <Wire.h>
#include <MPU6050.h>

MPU6050 mpu;

void setup() {
  Serial.begin (9600);
  Serial.println("Initialize MPU6050");
  while(!mpu.begin(MPU6050_SCALE_2000DPS,MPU6050_RANGE_2G)) { //sensor check
    Serial.println("No valid MPU6050 sensor found :(");
      delay(500);
      mpu.calibrateGyro(); //calibrate mpu
      mpu.setThreshold(3); //set sensitivity to default 3
  }
  checkSettings();
}

void checkSettings() {
  Serial.println();

  Serial.print("*Sleep Mode:  ");
  Serial.println(mpu.getSleepEnabled()?"Enabled":"Disabled"); //Gets current sleep mode enabled status

  Serial.print("*Clock Source:  ");
  switch(mpu.getClockSource()) {
    case MPU6050_CLOCK_KEEP_RESET:     Serial.println("Stops the clock and keeps the timing generator in reset"); break;
    case MPU6050_CLOCK_EXTERNAL_19MHZ: Serial.println("PLL with external 19.2MHz reference"); break;
    case MPU6050_CLOCK_EXTERNAL_32KHZ: Serial.println("PLL with external 32.768kHz reference"); break;
    case MPU6050_CLOCK_PLL_ZGYRO:      Serial.println("PLL with Z axis gyroscope reference"); break;
    case MPU6050_CLOCK_PLL_YGYRO:      Serial.println("PLL with Y axis gyroscope reference"); break;
    case MPU6050_CLOCK_PLL_XGYRO:      Serial.println("PLL with X axis gyroscope reference"); break;
    case MPU6050_CLOCK_INTERNAL_8MHZ:  Serial.println("Internal 8MHz oscillator"); break;
  }

  Serial.print("Accelerometer:  ");
  switch(mpu.getRange())
  {
    case MPU6050_RANGE_16G:            Serial.println("+/- 16 g"); break;
    case MPU6050_RANGE_8G:             Serial.println("+/- 8 g"); break;
    case MPU6050_RANGE_4G:             Serial.println("+/- 4 g"); break;
    case MPU6050_RANGE_2G:             Serial.println("+/- 2 g"); break;
  }

  Serial.print(" * Accelerometer offsets: ");
  Serial.print(mpu.getAccelOffsetX());
  Serial.print(" / ");
  Serial.print(mpu.getAccelOffsetY());
  Serial.print(" / ");
  Serial.println(mpu.getAccelOffsetZ());
  
  Serial.println();
}

void loop() {
  Vector rawAccel = mpu.readRawAccel();
  Vector normAccel = mpu.readNormalizeAccel();

  if(normAccel.XAxis >= 5) {
    Serial.println("down");
  }
  else if(normAccel.XAxis <= -5) {
    Serial.println("up");
  }
  else if(normAccel.YAxis >= 5) {
    Serial.println("left");
  }
  else if(normAccel.YAxis <= -5) {
    Serial.println("right");
  }else{
    Serial.println("none");
  }
  delay(1000);
}
