using BookStore.Domain.RepositoryInterFaces;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Infrastructure.Data;
using CW21Ta23.Infrastructure.Repositiries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        
        public CustomerRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Customer?> GetByUsernameAsync(string username)
        {
           return await _context.Customers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserName == username);
                
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Customers.AnyAsync(c => c.UserName == username);
        }
    }
}
