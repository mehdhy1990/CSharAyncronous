// See https://aka.ms/new-console-template for more information

using CsharpAsyncronos1;

Console.WriteLine("Cooking started... ");
var turkey = new Turkey();
await turkey.Cook();
Console.WriteLine("cooking turkey is finished");
var grovy = new Gravy();
await grovy.Cook();
Console.WriteLine("ready to eat");