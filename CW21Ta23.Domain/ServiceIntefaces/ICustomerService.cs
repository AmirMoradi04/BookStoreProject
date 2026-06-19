using BookStore.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.ServiceIntefaces
{
    public interface ICustomerService
    {
        Task RegisterAsync(RegisterDto dto);

    }
}
