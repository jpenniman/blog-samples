using System;

namespace OptionalParameterDefaults
{
    public interface IFoo
    {
        void Bar(string test, int param1 = 1);
    }

    public class Foo : IFoo
    {
        public void Bar(string test, int param1 = 2)
        {
            Console.WriteLine($"{test} = {param1}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var fooVar = new Foo();
            fooVar.Bar(nameof(fooVar));

            IFoo fooIFoo = new Foo();
            fooVar.Bar(nameof(fooIFoo));

            Foo fooFoo = new Foo();
            fooVar.Bar(nameof(fooFoo));

            Console.ReadLine();
        }
    }
}
