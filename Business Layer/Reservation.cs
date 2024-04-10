using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Business_Layer
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int CountTickets { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [ForeignKey(nameof(Flight))]
        public int FlightId { get; set; }

        [Required]
        [ForeignKey(nameof(Airport))]
        public int AirportId { get; set; }

        private Reservation() { }
        public Reservation(int number, int countTickets, double price, Flight flight, Airport airport)
        {
            this.Number = number;
            this.CountTickets = countTickets;
            this.Price = price;
            this.FlightId = flight.Id;
            this.AirportId = airport.Id;
        }
    }
}
