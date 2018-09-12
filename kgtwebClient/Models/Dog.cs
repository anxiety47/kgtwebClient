using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kgtwebClient.Models
{
    public class Dog
    {
        public int DogID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Object Level { get; set; }
        public List<Object> Workmode { get; set; }
        public string Notes { get; set; }
        public Object Guide { get; set; }

    }

    public class DogsList
    {
        public List<Dog> ListOfDogs { get; set; }
    }
}