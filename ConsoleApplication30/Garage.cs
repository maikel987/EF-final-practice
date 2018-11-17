using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication30
{
    public class Garage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public ICollection<GarageTreatment> TreatList { get; set; }
        public ICollection<Vehicle> VehicList { get; set; }
    }
}
