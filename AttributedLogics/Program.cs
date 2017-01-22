using AttributedLogics.Extensions;
using AttributedLogics.Samples.Models;
using System;

namespace AttributedLogics
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person
            {
                FirstName = "fn",
                LastName = "ln",
                UserName = "un"
            };

            p.Bind();

            var p2 = p;

            p2.FirstName = p2.LastName = p2.UserName = string.Empty;

            p2.Unbind();

            Console.WriteLine(p.Profile);
            Console.ReadKey();
        }
    }
}
