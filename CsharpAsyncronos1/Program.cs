// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using CsharpAsyncronos1;

Console.WriteLine($"Starting thread Id: {Environment.CurrentManagedThreadId}");

DummyTask.Run(() => Console.WriteLine($"First dummy thread Id: {Environment.CurrentManagedThreadId}"))
    .ConintiueWith(() => Console.WriteLine($"Second thread Id: {Environment.CurrentManagedThreadId}"));
Console.ReadLine();