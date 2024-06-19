#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define OLED_RESET       -1
#define SCREEN_ADDRESS 0x3C ///< See datasheet for Address; 0x3D for 128x64, 0x3C for 128x32
Adafruit_SSD1306 display(128, 64, &Wire, OLED_RESET);

#define HEART_RATE_PIN 34
#define BUZZER_PIN 35

int heartRate;
const int numReadings = 10;
int readings[numReadings];      // the readings from the analog input
int readIndex = 0;              // the index of the current reading
int total = 0;                  // the running total
int average = 0;                // the average

unsigned long previousMillis = 0;
const long interval = 1000; // Interval to display heart rate in milliseconds

void setup() {
  pinMode(HEART_RATE_PIN, INPUT);
  pinMode(BUZZER_PIN, OUTPUT);

  Serial.begin(9600);

  // Initialize the display
  if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
    Serial.println(F("SSD1306 allocation failed"));
    for(;;);
  }

  display.display();
  delay(2000);
  display.clearDisplay();

  // Initialize all the readings to 0
  for (int thisReading = 0; thisReading < numReadings; thisReading++) {
    readings[thisReading] = 0;
  }
}

void loop() {
  // Subtract the last reading:
  total = total - readings[readIndex];
  // Read from the sensor:
  readings[readIndex] = analogRead(HEART_RATE_PIN);
  // Add the reading to the total:
  total = total + readings[readIndex];
  // Advance to the next position in the array:
  readIndex = readIndex + 1;

  // If we're at the end of the array:
  if (readIndex >= numReadings) {
    readIndex = 0;
  }

  // Calculate the average:
  average = total / numReadings;

  // Map the average to heart rate
  heartRate = map(average, 0, 1023, 30, 200); // Adjust these values based on your sensor's characteristics

  // Display heart rate
  display.clearDisplay();
  display.setTextSize(2);
  display.setTextColor(SSD1306_WHITE);
  display.setCursor(0,0);
  display.print("Heart Rate: ");
  display.setCursor(0,20);
  display.print(heartRate / 4);
  display.print(" bpm");
  display.display();

  // Check if heart rate is abnormal
  if (heartRate > 100 || heartRate < 60)
  {
    tone(BUZZER_PIN, 1000); // Activate buzzer if heart rate is abnormal
  }
  else
  {
    noTone(BUZZER_PIN); // Turn off the buzzer if heart rate is normal
  }

  // Wait for a while before taking next reading
  unsigned long currentMillis = millis();
  if(currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis;
  }
}