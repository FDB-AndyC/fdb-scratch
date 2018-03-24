namespace GuidTest
{
    using System;

    class Program
    {
        private const string PseudoId = @"15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225";

        static void Main(string[] args)
        {
            var guid = new Guid(PseudoId);
            Console.WriteLine("Hello World!");
        }
    }
}
