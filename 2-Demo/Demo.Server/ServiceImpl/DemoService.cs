using System;
using Demo.Service;

namespace Demo.Server.ServiceImpl
{
    public class DemoService : IDemoService
    {
        public void PrintServer()
        {
            Console.Write("Server Is Called.");
        }
    }
}