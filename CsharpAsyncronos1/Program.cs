// See https://aka.ms/new-console-template for more information

using CsharpAsyncronos1;

Console.WriteLine("Cooking started... ");
var turkey = new Turkey();
var gravy = new Gravy();
await Task.WhenAll(turkey.Cook(), gravy.Cook());


Console.WriteLine("ready to eat");