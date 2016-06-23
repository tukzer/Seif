using System;
using System.Collections.Generic;

namespace Demo.Service
{
    public  class ComplexModel
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public IDictionary<string,string> Values { get; set; }

    }
}
