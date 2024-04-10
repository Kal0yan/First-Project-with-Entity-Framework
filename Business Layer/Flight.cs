using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Flight
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public string Destination { get; set; }

        [Range(1, 1500)]
        public int Time { get; set; }

        public List <Reservation> Reservations { get; set; }

        private Flight() // for Entity Framework
        {
            Reservations = new List<Reservation>();
        }
        public Flight(string destination, int time)
        {
            Destination = destination;
            Time = time;
            Reservations = new List<Reservation>();
        }
        public Flight(string destination)
        {
            Destination = destination;
            Reservations = new List<Reservation>();
        }
    }
}
