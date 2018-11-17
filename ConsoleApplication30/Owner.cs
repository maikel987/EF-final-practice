using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication30
{
    public class Owner
    {
        [Key]
        public int OwnerID { get; set; }

        public string Name { get; set; }
        public string Adress { get; set; }

        //[ForeignKey("Vehicle")]
        //public string IDplate { get; set; }

        //public Vehicle vehicle { get; set; }

    }
}   
