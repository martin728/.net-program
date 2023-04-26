using dotNetStandardLibraryClass;
using System.Numerics;

Console.WriteLine("Please,enter your name...");
string userName = Console.ReadLine();
Console.WriteLine("Hello,{0}",userName);
Operator o = new();
var res = o.ToContat(userName);
Console.WriteLine(res);