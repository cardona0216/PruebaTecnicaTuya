using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Customers customer) => await _context.Customer.AddAsync(customer);


        public async Task<Customers?> GetByIdAsync(CustomerId id) => await _context.Customer.SingleOrDefaultAsync(c => c.Id == id);
        
    }
}
