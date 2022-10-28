// See https://aka.ms/new-console-template for more information
using IndexNumberBinder;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");
AutoCsvIndexBinder binder = new AutoCsvIndexBinder(0);
TestClass tstClass = new TestClass();
binder.Bind(tstClass.GetType());
Console.ReadLine();