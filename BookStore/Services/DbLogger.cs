using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class DbLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine($"[DbLogger] - {message}");
        }
    }
}
