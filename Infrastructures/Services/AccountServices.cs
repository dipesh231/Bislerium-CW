using Application.Interfaces;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class AccountServices : IAccountServices

    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AccountServices(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }


        public string Login(LoginVM login)
        {
            throw new NotImplementedException();
        }
    }
}
