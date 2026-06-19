using CW21Ta23.Domain.Entities;
using CW21Ta23.Domain.RepositoryInterFaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.RepositoryInterFaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);

        
    }
}
