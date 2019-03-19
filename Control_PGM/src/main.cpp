#include <Arduino.h>
#include "BluetoothSerial.h"

// Create one object of the BluetoothSerialClass
BluetoothSerial SerialBT;

// Functions
void ActuateThePGM(int _state);
void ActuationCase(int _case);

int PGM_ValveA = 32;
int PGM_ValveB = 33;
int PGM_ValveC = 25;
int PGM_ValveD = 26;
int PGM_ValveE = 27;

bool ValveStateOne = false;
bool ValveStateTwo = false;

int dataReceived = 0;

int randomCase = 0;

void setup()
{
  Serial.begin(115200);

  pinMode(PGM_ValveA, OUTPUT);
  pinMode(PGM_ValveB, OUTPUT);
  pinMode(PGM_ValveC, OUTPUT);
  pinMode(PGM_ValveD, OUTPUT);
  pinMode(PGM_ValveE, OUTPUT);

  digitalWrite(PGM_ValveA, LOW);
  digitalWrite(PGM_ValveB, LOW);
  digitalWrite(PGM_ValveC, LOW);
  digitalWrite(PGM_ValveD, LOW);
  digitalWrite(PGM_ValveE, LOW);

  // This will stat the Bluetooth
  SerialBT.begin("ESP32Muscle");
}

void loop()
{

  SerialBT.read();
  dataReceived = SerialBT.parseInt();

  // decide which case is going to be actuated
  ActuateThePGM(dataReceived);
  dataReceived = 0;
}

void ActuateThePGM(int _state)
{
  if (_state == 5)
  {
    SerialBT.end();
    ESP.restart();
  }
  else if (_state == 1)
  {
    randomCase = random(0, 2);
    ActuationCase(randomCase);
  }
  else if (_state == 2)
  {
    randomCase = random(0, 5);
    ActuationCase(randomCase);
  }
  else if (_state == 0)
  {
    digitalWrite(PGM_ValveA, LOW);
    digitalWrite(PGM_ValveB, LOW);
    digitalWrite(PGM_ValveC, LOW);
    digitalWrite(PGM_ValveD, LOW);
    digitalWrite(PGM_ValveE, LOW);
  }
}

void ActuationCase(int _case)
{
  for (int i = 0; i < 3; i++)
  {
    if (randomCase == 0)
    {
      digitalWrite(PGM_ValveA, HIGH);
      digitalWrite(PGM_ValveB, HIGH);
      digitalWrite(PGM_ValveC, HIGH);
      digitalWrite(PGM_ValveD, HIGH);
      digitalWrite(PGM_ValveE, HIGH);
    }
    else if (randomCase == 1)
    {
      digitalWrite(PGM_ValveA, HIGH);
      digitalWrite(PGM_ValveB, HIGH);
      digitalWrite(PGM_ValveC, HIGH);
      digitalWrite(PGM_ValveD, HIGH);
      digitalWrite(PGM_ValveE, LOW);
    }
    else if (randomCase == 2)
    {
      digitalWrite(PGM_ValveA, HIGH);
      digitalWrite(PGM_ValveB, HIGH);
      digitalWrite(PGM_ValveC, HIGH);
      digitalWrite(PGM_ValveD, LOW);
      digitalWrite(PGM_ValveE, LOW);
    }
    else if (randomCase == 3)
    {
      digitalWrite(PGM_ValveA, HIGH);
      digitalWrite(PGM_ValveB, HIGH);
      digitalWrite(PGM_ValveC, LOW);
      digitalWrite(PGM_ValveD, LOW);
      digitalWrite(PGM_ValveE, LOW);
    }
    else if (randomCase == 4)
    {
      digitalWrite(PGM_ValveA, HIGH);
      digitalWrite(PGM_ValveB, LOW);
      digitalWrite(PGM_ValveC, LOW);
      digitalWrite(PGM_ValveD, LOW);
      digitalWrite(PGM_ValveE, LOW);
    }

    // To generate the sensation of burning
    // digitalWrite(PGM_ValveA, LOW);
    // digitalWrite(PGM_ValveB, LOW);
    // digitalWrite(PGM_ValveC, LOW);
    // digitalWrite(PGM_ValveD, LOW);
    // digitalWrite(PGM_ValveE, LOW);
  }
}
