﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Realization
{
    public class RentalPointRepository : EfGenericRepository<RentalPoint>, IRentalPointRepository
    {
        public RentalPointRepository(ApplicationContext context) : base(context){}

        public async Task<RentalPoint> GetRentalPointByNameAsync(string name)
        {
            return (await GetAsync(p => p.Name.Equals(name))).FirstOrDefault();
        }

        public async Task<IEnumerable<RentalPoint>> GetRentalPointsWithLocations()
        {
            return await (await GetAllAsync(includes: point =>
                point.Include(p => p.Location.City.Country))).ToArrayAsync();
        }
    }
}
