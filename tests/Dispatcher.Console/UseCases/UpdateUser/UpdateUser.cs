﻿namespace Dispatcher.Console.UseCases.UpdateUser
{
    public record UpdateUser : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
