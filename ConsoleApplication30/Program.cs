using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication30
{
    public class GarageContext : DbContext
    {
        public string conString { get; set; }
        public GarageContext() { conString = this.Database.Connection.ConnectionString; }

        public DbSet<Garage> garageTable { get; set; }
        public DbSet<GarageTreatment> treatmentTable { get; set; }
        public DbSet<Owner> ownerTable { get; set; }
        public DbSet<Vehicle> vehiculeTable { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            GenerateDb();
            using (var context = new GarageContext())
            {
                // View the vehicle that underwent the most treatments and the number of treatments it underwent
                var mostTreatVehic = (from v in context.vehiculeTable
                                     where v.treatList.Count == context.vehiculeTable.Max(c=> c.treatList.Count)
                                     select new { car = v, treatmentNumber = v.treatList.Count }).ToList();


                Console.WriteLine("A. View the vehicle that underwent the most treatments and the number of treatments it underwent");
                foreach (var item in mostTreatVehic)
                {
                    Console.WriteLine($"{ item.car.IDPlate} has done { item.treatmentNumber} treatments  ");
                }


                Console.WriteLine();
                //B. For each vehicle show the car owner's name
                var owNameList = from v in context.vehiculeTable select new { Car = v.IDPlate, owner = v.owner };

                Console.WriteLine("B. For each vehicle show the car owner's name");
                foreach (var item in owNameList)
                {
                    Console.WriteLine($"{item.Car} is own by {item.owner.Name} ");
                }

                Console.WriteLine();
                //C. For each vehicle show a list of treatments
                Console.WriteLine("C. For each vehicle show a list of treatments");

                var tretList = from v in context.vehiculeTable select new { vehicleID = v.IDPlate, teatmentList = v.treatList };
                foreach (var item in tretList)
                {
                    Console.WriteLine(item.vehicleID+" did those treatment: ");
                    foreach (var treat in item.teatmentList)
                    {
                        Console.WriteLine("\t"+treat.Name+" for $"+treat.Price);
                    }
                }

                Console.WriteLine();
                //D. From the list of treatments View the last treatment in the list
                Console.WriteLine("D. From the list of treatments View the last treatment in the list");

                var lastTreat = (from t in context.treatmentTable select t).ToList();
                Console.WriteLine("Last treatment,\tID :"+lastTreat.Last().ID+", \tName: "+ lastTreat.Last().Name+ ", \tPrice: $" + lastTreat.Last().Price);
            }



        }



        private static void GenerateDb()
        {
            using (var context = new GarageContext())
            {
                Random ran = new Random();
                #region Owner
                
                context.ownerTable.AddRange(new List<Owner> {
                    new Owner() { Adress = "Ben Yehuda" + ran.Next(1, 100), Name = "Johan Dahan" },
                    new Owner() { Adress = "Ben Yehuda" + ran.Next(1, 100), Name = "Ofek Sitbon" },
                    new Owner() { Adress = "Ben Yehuda" + ran.Next(1, 100), Name = "Aria Cohen" },
                    new Owner() { Adress = "Dizzengoff" + ran.Next(1, 100), Name = "Francis Levi" },
                    new Owner() { Adress = "Dizzengoff" + ran.Next(1, 100), Name = "Benoit Green" },
                    new Owner() { Adress = "Dizzengoff" + ran.Next(1, 100), Name = "George Ben" },
                    new Owner() { Adress = "Mappu" + ran.Next(1, 100), Name = "Rachel Five" },
                    new Owner() { Adress = "Arlozorof" + ran.Next(1, 100), Name = "Joe Dupont" },
                    new Owner() { Adress = "Jabotinski" + ran.Next(1, 100), Name = "Johan Ben" },
                    new Owner() { Adress = "Mazet" + ran.Next(1, 100), Name = "Miki Baba" }
                });
                context.SaveChanges();
                

                #endregion
                #region Vehicles

                List<Vehicle> vehicles = new List<Vehicle>();
                string[] colors = { "red", "blue", "green", " grey", "black" };
                string[] brands = { "Mercedez", "Ferrarie", "Renault", "Peugeot", "Citroen" };

                for (int i = 0; i < 10; i++)
                {
                    vehicles.Add(new Vehicle()
                    {
                        IDPlate = "AB" + (i + 7).ToString() + (char)(i + 70) + (char)(i + 75) + i.ToString() + "ISR" + (i + 19).ToString(),
                        brand = brands[ran.Next(brands.Length)],
                        buyingDate = new DateTime(ran.Next(1920, 2018), ran.Next(1, 13), ran.Next(1, 28)),
                        color = colors[i%colors.Length],
                        owner = context.ownerTable.ToList()[i]
                    });
                }

                context.vehiculeTable.AddRange(vehicles);
                #region Test
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                #endregion

                #endregion
                #region Treatment

                string[] treatArr = { "Paint", "Washing", "Decontamination ", "Polishing ", "Protection ", "Rims and Tires", "Leather ", "Inside ", "Windows and Optics", "Engine" };
                List<GarageTreatment> treatments = new List<GarageTreatment>();
                for (int i = 0; i < 10; i++)
                {
                    int index = i;
                    List<Vehicle> tmp = new List<Vehicle>();
                    do
                    {
                        tmp.Add(context.vehiculeTable.ToList()[index]);
                        index += ran.Next(1, 5);
                    } while (index< context.vehiculeTable.Count());

                    treatments.Add(new GarageTreatment()
                    {
                        Name = treatArr[i],
                        Price = ran.Next(2000) + Math.Round(ran.NextDouble(),2),
                        vehicList = tmp
                    }) ;
                }
                context.treatmentTable.AddRange(treatments);
                context.SaveChanges();
                #endregion

                Garage garages = new Garage() { TreatList = treatments,VehicList=vehicles};

                context.garageTable.AddRange(new List<Garage>{ garages});
                context.SaveChanges();


                //changing data:
                var car = (from c in context.vehiculeTable where c.owner.Name == "Johan Dahan" select c).SingleOrDefault();
                car.color = colors[0];
                context.SaveChanges();
            }
        }

    }
}
