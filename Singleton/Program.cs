using Singleton.Pattern;

var pricePool = PricePool.Instance;

pricePool.SetPrice(Product.Magna, 13.99m);
pricePool.SetPrice(Product.Premium, 24.12m);
pricePool.SetPrice(Product.Diesel, 24.23m);

Console.WriteLine("Hash code PricePool: " + pricePool.GetHashCode());

pricePool.SetPrice(Product.Magna, 23.99m);
pricePool.SetPrice(Product.Magna, 22.33m);
pricePool.SetPrice(Product.Magna, 24.12m);
pricePool.SetPrice(Product.Magna, 26.74m);
pricePool.SetPrice(Product.Magna, 27.12m);
pricePool.SetPrice(Product.Magna, 25.12m);
pricePool.SetPrice(Product.Magna, 23.12m);
pricePool.SetPrice(Product.Magna, 21.12m);
pricePool.SetPrice(Product.Magna, 20.12m);

var priceMagna = pricePool.GetPrice(Product.Magna);
Console.WriteLine("Price Magna: " + priceMagna);

pricePool.SetPrice(Product.Magna, 22.12m);

Console.WriteLine("Hash code PricePool: " + pricePool.GetHashCode());

priceMagna = pricePool.GetPrice(Product.Magna);
var pricePremium = pricePool.GetPrice(Product.Premium);
var priceDiesel = pricePool.GetPrice(Product.Diesel);

Console.WriteLine("Price Magna: " + priceMagna);
Console.WriteLine("Price Premium: " + pricePremium);
Console.WriteLine("Price Diesel: " + priceDiesel);