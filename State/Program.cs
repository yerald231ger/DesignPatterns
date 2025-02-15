using State;

var mug = new Mug(250);


mug.AddSomeCoffee(100);
Console.WriteLine(mug.GetState());
mug.AddSomeCoffee(150);
Console.WriteLine(mug.GetState());
mug.AddSomeCoffee(10);
Console.WriteLine(mug.GetState());

mug.DrinkSomeCoffee(50);
Console.WriteLine(mug.GetState());
mug.DrinkSomeCoffee(100);
Console.WriteLine(mug.GetState());
mug.AddSomeCoffee(175);
Console.WriteLine(mug.GetState());

mug.EmptyMug();
Console.WriteLine(mug.GetState());