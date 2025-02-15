using Strategy;

var tank = new Tank(Product.Magna)
{
    Volume = 1000,
    Temperature = 25
};

Console.WriteLine($"Volume: {tank.VolumeTc}. With product: {tank.Product}");

tank.ChangeProduct(Product.Diesel);

Console.WriteLine($"Volume: {tank.VolumeTc}. With product: {tank.Product}");