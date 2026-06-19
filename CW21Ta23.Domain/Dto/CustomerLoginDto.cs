using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Dto
{
    public class CustomerLoginDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
