using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestExpandPerformanceBug.Models
{
    public class DummyObject1
    {
        public decimal Id { get; set; }
        public String Description { get; set; }
        public String Field1 { get; set; }
        public String Field2 { get; set; }
        public String Field3 { get; set; }
        public String Field4 { get; set; }
        public String Field5 { get; set; }
        public String Field6 { get; set; }
        public String Field7 { get; set; }
        public DummyObject2 Field8 { get; set; }
    }
}
