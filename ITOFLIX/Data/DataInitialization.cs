
using Microsoft.AspNetCore.Identity;
using ITOFLIX.Models;
using Microsoft.EntityFrameworkCore;

namespace ITOFLIX.Data
{
    public class DataInitialization
    {
        private readonly ITOFLIXContext _context;
        private readonly SignInManager<ITOFLIXUser> _signInManager;
        private readonly RoleManager<ITOFLIXRole> _roleManager;
        public DataInitialization(ITOFLIXContext context, SignInManager<ITOFLIXUser> signInManager, RoleManager<ITOFLIXRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
            if (_context != null)
            {
                _context.Database.Migrate();
                if(_context.Users.Count()==0)
                {
                    CreateAdminUser();
                }
                if(_context.Roles.Count()==0)
                {
                    CreateRoles();
                }
                if(_context.Plans.Count()==0)
                {
                    CreatePlans();
                }
                _context.SaveChanges();
            }
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
                    BirthDate = DateTime.Now.Date,
                };
                ITOFLIXUser user = new ITOFLIXUser();
                user.Name = "admin2";
                user.Email = "admin";
                user.PhoneNumber = "1234567890";
                user.BirthDate = DateTime.Now.Date;


                string adminUserPassword = "Admin123!"; 
                _signInManager.UserManager.CreateAsync(adminUser, adminUserPassword).Wait();
                _signInManager.UserManager.CreateAsync(user, adminUserPassword).Wait();
            }
        }


        public void CreateRoles()
        {
            ITOFLIXRole iTOFLIXRole;
            iTOFLIXRole = new ITOFLIXRole() { Name = "Administrator"};
            _roleManager.CreateAsync(iTOFLIXRole).Wait();
        }

        public void CreateRestrictions()
        {
            _context.Restrictions.Add(new Restriction() { Id = 0, Name = "Genel İzleyici" });
            _context.Restrictions.Add(new Restriction() { Id = 7, Name = "7 yaş ve üzeri" });
            _context.Restrictions.Add(new Restriction() { Id = 13, Name = "13 yaş ve üzeri" });
            _context.Restrictions.Add(new Restriction() { Id = 18, Name = "18 yaş ve üzeri" });

            _context.SaveChanges();
        }

        public void CreatePlans()
        {
            _context.Plans.Add(new Plan() { Name = "Standart", Price = 100, Resolution = "1080" });

            _context.SaveChanges();
        }

    }
}
