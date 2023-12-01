using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customer.Create
{
    internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWokr;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWokr)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unitOfWokr = unitOfWokr ?? throw new ArgumentNullException(nameof(unitOfWokr));
        }


        public async Task<Unit> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber) 
            {
                throw new ArgumentException(nameof(phoneNumber));
            }
            if (Address.Create(command.Country, command.Line1, command.Line2,
                               command.City, command.State, command.ZipCode)
                               is not Address address)
            {
                throw new ArgumentException(nameof(address));
            }

            var customer = new Customers(
                new CustomerId(Guid.NewGuid()),
                command.Name,
                command.LastName,
                command.Email,
                phoneNumber,
                address,
                true
                
                );

            await _customerRepository.Add(customer);
            await _unitOfWokr.SaveChangesAsync(cancellationToken);
            return Unit.Value;

       
        }
    }
}
