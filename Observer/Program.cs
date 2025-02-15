using Observer;
using Observer.Pattern;

var tank = new Tank { Code = "T1" };
var gerardo = new Client("Gerardo");
var maria = new Client("Maria");
var whatsapp = new WhatsAppListener();
var email = new EmailListener();

tank.Subscribe(whatsapp);
tank.Subscribe(email);
tank.AlarmEvent += gerardo.OnNewAlarm;
tank.AlarmEvent += maria.OnNewAlarm;

tank.AddAlarm("High temperature");
tank.AddAlarm("Low pressure");