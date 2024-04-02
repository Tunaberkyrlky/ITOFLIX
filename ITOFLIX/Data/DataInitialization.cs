
using Microsoft.AspNetCore.Identity;
using ITOFLIX.Models;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.DTO.Requests;
using ITOFLIX.DTO.Converters;

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
                if(_context.Restrictions.Count() == 0)
                {
                    CreateRestrictions();
                }
                if (_context.Plans.Count() == 0)
                {
                    CreatePlans();
                }
                _context.SaveChanges();
                if (_context.Roles.Count() == 0)
                {
                    CreateRoles();
                }
                if (_context.Users.Count()==0)
                {
                    CreateAdminUser();
                }
                if(_context.Categories.Count() == 0)
                {
                    CreateCategories();
                }
            }
        }

        public void CreateAdminUser()
        {
            IdentityResult identityResult;
            if(_signInManager.UserManager.Users.Count() == 0)
            {
                UserCreateRequest newUserRequest = new UserCreateRequest()
                {
                    Name = "admin",
                    Email = "admin@abc.com",
                    PhoneNumber = "1234567890",
                    BirthDate = DateTime.Now.Date,
                    Password = "Admin123!"
                };

                UserConverter userConverter = new UserConverter();
                ITOFLIXUser NewUser = userConverter.Convert(newUserRequest);

                string adminUserPassword = "Admin123!"; 
                identityResult =  _signInManager.UserManager.CreateAsync(NewUser, adminUserPassword).Result;
                _signInManager.UserManager.AddToRoleAsync(NewUser, "Administrator");
            }
        }


        public void CreateRoles()
        {
            ITOFLIXRole iTOFLIXRole;

            iTOFLIXRole = new ITOFLIXRole("Administrator");
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

        public void CreateCategories()
        {
            Category category = new Category{Name = "Korku" };
            _context.Categories.Add(category);
            Category category1 = new Category { Name = "Animasyon" };
            _context.Categories.Add(category1);
            Category category2 = new Category { Name = "Komedi" };
            _context.Categories.Add(category2);
            Category category3 = new Category { Name = "Romantik" };
            _context.Categories.Add(category3);
            Category category4 = new Category { Name = "Bilim kurgu" };
            _context.Categories.Add(category4);
            Category category5 = new Category { Name = "Psikolojik" };
            _context.Categories.Add(category5);
            Category category6 = new Category { Name = "Belgesel" };
            _context.Categories.Add(category6);
            Category category7 = new Category { Name = "Macera" };
            _context.Categories.Add(category7);
            Category category8 = new Category { Name = "Dram" };
            _context.Categories.Add(category8);

            _context.SaveChanges();
        }

    }
}
