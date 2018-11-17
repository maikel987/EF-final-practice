using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication30
{
    public class GarageTreatment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<Vehicle> vehicList { get; set; }
    }
}
