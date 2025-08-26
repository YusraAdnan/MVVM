using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    /* Data container for a person, Nothing UI specific 
     Represents the raw data and business rules with the UI specs 
     Its like raw ingredient for a recipe >,< */
    public class PersonModel
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string Address { get; set; }

        public PersonModel(string name, int age, string address)
        {
            Name = name;
            Age = age;
            Address = address;
        }
    }
}
