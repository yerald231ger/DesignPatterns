// See https://aka.ms/new-console-template for more information

using TemplateMethod;
using TemplateMethod.Pattern;

var table = new TableConfiguration();
var listOfQuotes = new List<DefaultTemplateMethod> { table.GetTemplate() };

table.SetMaterial(Material.Walnut);
listOfQuotes.Add(table.GetTemplate());

table.SetMaterial(Material.Cherry);
listOfQuotes.Add(table.GetTemplate());

table.SetMaterial(Material.Walnut);
table.SetTableArea(1200);
listOfQuotes.Add(table.GetTemplate());

table.SetMaterial(Material.Oak);
table.SetTableArea(800);
listOfQuotes.Add(table.GetTemplate());

table.SetMaterial(Material.Walnut);
table.SetTableArea(1000);
listOfQuotes.Add(table.GetTemplate());

foreach (var quote in listOfQuotes)
{
    quote.QuoteTable();   
}




