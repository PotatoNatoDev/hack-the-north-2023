#define TILT_SENSOR 2

int tiltState = 0;

void setup() {
  // put your setup code here, to run once:
  pinMode(TILT_SENSOR, INPUT);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  tiltState = digitalRead(TILT_SENSOR);

  if(tiltState == HIGH)
  {
    Serial.println(1);
  }else{
    Serial.println(0);
  }
}
