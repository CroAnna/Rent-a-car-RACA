using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class Car
    {
        public string Id { get; set; } // nez jel mi ovo treba
        public string Company { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool Rented { get; set; }
        
        public Car(string company, string model, int year, bool rented)
        {
            Company = company; 
            Model = model; 
            Year = year; 
            Rented = rented; 
        }
    }
}
