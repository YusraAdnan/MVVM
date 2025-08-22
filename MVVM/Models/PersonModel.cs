using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
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
