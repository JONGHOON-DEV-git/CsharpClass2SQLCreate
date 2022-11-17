// See https://aka.ms/new-console-template for more information
using IndexNumberBinder;
using System.Runtime.CompilerServices;

//Console.WriteLine("Hello, World!");
AutoCsvIndexBinder binder = new AutoCsvIndexBinder(0);
binder.Bind(new TestClass());

Console.ReadLine();