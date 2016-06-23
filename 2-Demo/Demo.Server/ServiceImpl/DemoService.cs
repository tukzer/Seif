using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Service;

namespace Demo.Server.ServiceImpl
{
    public class DemoService : IDemoService
    {
        private static IList<ComplexModel> _listSource = new List<ComplexModel>();

        public void PrintServer()
        {
            Console.Write("Server Is Called.");
        }

        public int AddModel(ComplexModel model)
        {
            _listSource.Add(model);
            return 1;
        }

        public ComplexModel GetModel(string name)
        {
            return _listSource.FirstOrDefault(p => p.Name == name);
        }

        public ComplexModel GetModel(string name, string value)
        {
            return _listSource.FirstOrDefault(p => p.Name == name && p.Values != null && p.Values.ContainsKey(value));
        }
    }
}