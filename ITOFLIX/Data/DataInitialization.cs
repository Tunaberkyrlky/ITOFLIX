﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using ITOFLIX.Models;


namespace ITOFLIX.Data
{
    public class DataInitialization
    {
        private readonly ITOFLIXContext _context;
        private readonly SignInManager<ITOFLIXUser> _signInManager;
        private readonly RoleManager<ITOFLIXRole> _roleManager;
        public DataInitialization(ITOFLIXContext context, SignInManager<ITOFLIXUser> signInManager, Microsoft.AspNet.Identity.RoleManager<ITOFLIXRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public void CreateAdminUser()
        {
            if(_signInManager.UserManager.Users.Count() == 0)
            {
                ITOFLIXUser adminUser = new ITOFLIXUser()
                {
                    Name = "admin",
                    Email = "admin",
                    PhoneNumber = "1234567890",
                    BirthDate = DateTime.Now
                };
                string adminUserPassword = "Admin123!"; 
                _signInManager.UserManager.CreateAsync(adminUser, adminUserPassword);
            }
        }

        public void CreateRoles()
        {

        }
    }
}