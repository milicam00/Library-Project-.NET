﻿using System.Security.Claims;

namespace OnlineLibrary.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Claim> Claims { get; set; }
        public string Password { get; set; }    
    }
}
