// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using CsharpAsyncronos1;

Console.WriteLine($"Starting thread Id: {Environment.CurrentManagedThreadId}");

DummyTask.Run(() => Console.WriteLine($"First dummy thread Id: {Environment.CurrentManagedThreadId}")).Wait();
Console.WriteLine($"Second dummy thread Id: {Environment.CurrentManagedThreadId}");
DummyTask.Run(() => Console.WriteLine($"Second dummy thread Id: {Environment.CurrentManagedThreadId}")).Wait();