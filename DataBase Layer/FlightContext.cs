using System;
using Business_Layer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataBase_Layer
{
    public class FlightContext : IDb<Flight, int>
    {
        private readonly AppDbContext _context;
        public FlightContext(AppDbContext context)
        {
            _context = context;
        }


        public void Create(Flight entity)
        {
            _context.Flights.Add(entity);
            _context.SaveChanges();
        }        

        public Flight Read(int key, bool isReadOnly = true, bool useNavigationalProperties = false)
        {
            IQueryable<Flight> query = _context.Flights;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            if (useNavigationalProperties)
            {
                query = query.Include(f => f.Reservations);
            }
            return query.SingleOrDefault(f => f.Id == key);
        }

        public List<Flight> ReadAll(bool isReadOnly = true, bool useNavigationalProperties = false)
        {
            IQueryable<Flight> query = _context.Flights;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            if (useNavigationalProperties)
            {
                query = query.Include(f => f.Reservations);
            }
            return query.ToList();
        }

        public void Update(Flight entity, bool useNavigationalProperties = false)
        {
            Flight flightFromDb = Read(entity.Id, false, useNavigationalProperties);
            _context.Entry(flightFromDb).CurrentValues.SetValues(entity);

            if (useNavigationalProperties)
            {
                List<Reservation> reservations = new List<Reservation>(flightFromDb.Reservations);

                for (int i = 0; i < entity.Reservations.Count; i++)
                {
                    Reservation reservationFromDb = _context.Reservations.Find(entity.Reservations[i].Id);

                    if(reservationFromDb is not null && !reservations.Contains(reservationFromDb))
                    {
                        reservations.Add(reservationFromDb);
                    }
                    else
                    {
                        reservations.Add(entity.Reservations[i]);
                    }
                }
                flightFromDb.Reservations = reservations;
                _context.SaveChanges();
            }
            
        }

        public void Delete(int key)
        {
            Flight flightFromDb = Read(key, false);
            if (flightFromDb is null)
            {
                throw new ArgumentException($"Flight with id {key} does not exist.");
            }
            _context.Flights.Remove(flightFromDb);
            _context.SaveChanges();
            
        }
    }
}
