using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace Business_Layer
{
    public class Airport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public double IncomePerYear { get; set; }

        public List<Reservation> Reservations { get; set; }

        
        private Airport() // for Entity Framework 
        {
            Reservations = new List<Reservation>();
        }

        // optional constructor
        public Airport(string name, double incomePerYear)
        {
            Name = name;
            IncomePerYear = incomePerYear;
            Reservations = new List<Reservation>();
        }

        // main constructor 
        public Airport(string name)
        {
            Name = name;
            Reservations = new List<Reservation>();
        }
    }
}
