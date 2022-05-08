// See https://aka.ms/new-console-template for more information
using MyLibrary;
using System;
using System.Runtime.Versioning;

static class Program
{
    static void Main()
    {
        AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        try
        {
            new Class1().Foo();
        }
        catch { }

        Console.ReadLine();
        //throw new ApplicationException("test unhandled");

    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Console.WriteLine("Unhandled Exception");
        Console.WriteLine(e.ExceptionObject);
    }

    private static void CurrentDomain_FirstChanceException(
        object? sender, 
        System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
        Console.WriteLine("Source: {0}", e.Exception.Source);
        Console.WriteLine("Target: {0}", e.Exception.TargetSite?.Name);
        Console.WriteLine(e.Exception);
    }
}
