using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApplication30
{
    public class Vehicle
    {



        private string IDplate;
        [Key]
        public string IDPlate
        {
            get { return IDplate; }
            set
            {
                System.Text.RegularExpressions.Regex myRegex = new Regex(@"[a-zA-Z0-9]");
                System.Text.RegularExpressions.Regex myRegex2 = new Regex(@"[^A-Za-z0-9_]");
                if (value.Length >= 7 && myRegex.IsMatch(value) && !myRegex2.IsMatch(value))
                    IDplate = value;
            }
        }


        //[Key]
        //[Range(7, 300)]
        //public string IDplate { get; set; }
        ////{
        ////    get { return IDplate; }
        //    set
        //    {
        //        System.Text.RegularExpressions.Regex myRegex = new Regex(@"[a-zA-Z0-9]");
        //        System.Text.RegularExpressions.Regex myRegex2 = new Regex(@"[^A-Za-z0-9_]");
        //        if (value.Length >= 7 && myRegex.IsMatch(value) && !myRegex2.IsMatch(value))
        //            IDplate = value;
        //    }
        //}

        public string brand { get; set; }
        public string color { get; set; }
        public DateTime buyingDate { get; set; }

        //[ForeignKey("Owner")]
        //public int OwnerID { get; set; }

        public Owner owner { get; set; }
        public ICollection<GarageTreatment> treatList { get; set; }
        public Garage garage { get; set; }

    }
}
