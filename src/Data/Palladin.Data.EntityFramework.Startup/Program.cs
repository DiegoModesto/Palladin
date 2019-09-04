using Microsoft.EntityFrameworkCore;
using System;

namespace Palladin.Data.EntityFramework.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PalladinContext>();
            //var context = new PalladinContext(optionsBuilder.UseSqlServer(DBGlobals.DbConnection).Options);

            Console.WriteLine("Hello World!");
        }
    }
}
