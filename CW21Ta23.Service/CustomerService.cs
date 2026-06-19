using BookStore.Domain.Dto;
using BookStore.Domain.RepositoryInterFaces;
using BookStore.Domain.ServiceIntefaces;
using CW21Ta23.Domain.Entities;
using CW21Ta23.Service.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace BookStore.Service
{
    public class CustomerService :ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            Validate(dto);

            CheckExistCustomerByUserName(dto);

            string hashPassword = PasswordService.GetHashPassword(dto.Password);

            var customer = new Customer{

                FullName = dto.FullName,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                PasswordHash = hashPassword
            };
            await _customerRepository.AddAsync(customer);
            
            

          

            

           

          
        }
        
        private async Task<Customer> GetCustomer(string userName)
        {

            var existCustomer = await _customerRepository.GetByUsernameAsync(userName);
            if (existCustomer == null)
            {
                throw new ItemNotFoundException("Customer",$"{userName}");
            }
            return existCustomer;
            
        }
        private async void CheckExistCustomerByUserName(RegisterDto dto)
        {

            var existUser = await _customerRepository.UsernameExistsAsync(dto.UserName);
            if (existUser)
            {
                throw new ConflictException("Customer", $"{dto.UserName}");
            }
        }

        private void Validate(RegisterDto dto)
        {

            if (string.IsNullOrWhiteSpace(dto.UserName))
                throw BadRequestException.Required("Username");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw BadRequestException.Required("PasswordHash");

            if (dto.Password.Length <= 8)
            {
                throw new BadRequestException("PasswordHash more than seven character");
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw BadRequestException.Required("Email");

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw BadRequestException.Required("PhoneNumber");

            if (dto.PhoneNumber.Contains(" "))
                throw new Exception("Phone number cannot contain spaces");

            if (!dto.PhoneNumber.All(char.IsDigit))
                throw new Exception("Phone number must contain only digits");

            if (dto.PhoneNumber.Length != 11)
                throw new Exception("Phone number must be exactly 11 digits");

            

        }
        //TODO
        public async Task<CustomerLoginDto> Login(LoginCustomerDto dto)
        {

          var foundCustomer = await GetCustomer(dto.UserName);
            PasswordService.VerifyPassword(foundCustomer.PasswordHash, dto.Password);
            return new CustomerLoginDto
            {
             FullName = foundCustomer.FullName,
             PhoneNumber = foundCustomer.PhoneNumber,
             Email = foundCustomer.Email,
             UserName = foundCustomer.UserName,
            };
        }
    }
}
