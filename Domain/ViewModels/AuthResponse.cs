﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class AuthResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
