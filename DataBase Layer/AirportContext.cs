using System;
using System.Collections.Generic;
using Business_Layer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataBase_Layer
{
    public class AirportContext : IDb<Airport, int>
    {
        private readonly AppDbContext _context;
        public AirportContext(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Airport entity)
        {
            _context.Airports.Add(entity);
            _context.SaveChanges();
        }

        

        public Airport Read(int key, bool isReadOnly = true, bool useNavigationalProperties = false)
        {
            IQueryable<Airport> query = _context.Airports;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }
            if (useNavigationalProperties)
            {
                query = query.Include(air => air.Reservations);
            }
            return query.SingleOrDefault(air => air.Id == key);
        }

        public List<Airport> ReadAll(bool isReadOnly = true, bool useNavigationalProperties = false)
        {
            IQueryable<Airport> query = _context.Airports;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }
            if (useNavigationalProperties)
            {
                query = query.Include(air => air.Reservations);
            }
            return query.ToList();
        }

        public void Update(Airport entity, bool useNavigationalProperties = false)
        {
            Airport airportFromDb = Read(entity.Id, false, useNavigationalProperties);
            _context.Entry(airportFromDb).CurrentValues.SetValues(entity);

            if (useNavigationalProperties)
            {
                List<Reservation> reservations = new List<Reservation>(airportFromDb.Reservations);

                for (int i = 0; i < entity.Reservations.Count; i++)
                {
                    Reservation reservationFromDb = _context.Reservations.Find(entity.Reservations[i].Id);

                    if (reservationFromDb is not null && !reservations.Contains(reservationFromDb))
                    {
                        reservations.Add(reservationFromDb);
                    }
                    else
                    {
                        reservations.Add(entity.Reservations[i]);
                    }

                }
                entity.Reservations = reservations;
                _context.SaveChanges();
            }



        }

        public void Delete(int key)
        {
            Airport airportFromDb = Read(key, false);
            if(airportFromDb is null)
            {
                throw new ArgumentException($"Airport with id {key} does not exist.");
            }
            _context.Airports.Remove(airportFromDb);
            _context.SaveChanges();
        }
    }
}
