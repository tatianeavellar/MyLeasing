﻿using MyLeasing.Web.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyLeasing.Web.Data
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Owners.Include(p => p.User); // INNER JOIN
        }
    }
}
