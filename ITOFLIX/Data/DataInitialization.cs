    
using Microsoft.AspNetCore.Identity;
using ITOFLIX.Models;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.DTO.Requests;
using ITOFLIX.DTO.Converters;
using ITOFLIX.DTO.Requests.MediaRequests;

namespace ITOFLIX.Data
{
    public class DataInitialization
    {
        private readonly ITOFLIXContext _context;
        private readonly SignInManager<ITOFLIXUser> _signInManager;
        private readonly RoleManager<ITOFLIXRole> _roleManager;

        MediaConverter _mediaConverter = new MediaConverter();

        public DataInitialization(ITOFLIXContext context, SignInManager<ITOFLIXUser> signInManager, RoleManager<ITOFLIXRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
            if (_context != null)
            {
                _context.Database.Migrate();
                if (_context.Restrictions.Count() == 0)
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
                if (_context.Users.Count() == 0)
                {
                    CreateAdminUser();
                    CreateContentAdminUser();
                }
                if (_context.Categories.Count() == 0)
                {
                    CreateCategories();
                }
                if (_context.Episodes.Count() == 0
                    && _context.Actors.Count() == 0
                    && _context.Directors.Count() == 0
                    && _context.Media.Count() == 0)
                {
                    CreateExampleMedia();
                }
                _context.SaveChanges();
            }
        }

        public void CreateAdminUser()
        {
            IdentityResult identityResult;
            if (_signInManager.UserManager.Users.Count() <= 2)
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
                identityResult = _signInManager.UserManager.CreateAsync(NewUser, adminUserPassword).Result;
                _signInManager.UserManager.AddToRoleAsync(NewUser, "Administrator").Wait();
            }
        }

        public void CreateContentAdminUser()
        {
            IdentityResult identityResult;
            if (_signInManager.UserManager.Users.Count() <= 2)
            {

                UserCreateRequest newUserRequest = new UserCreateRequest()
                {
                    Name = "ContentAdmin",
                    Email = "ContentAdmin@abc.com",
                    PhoneNumber = "1234567890",
                    BirthDate = DateTime.Now.Date,
                    Password = "Admin123!"
                };

                UserConverter userConverter = new UserConverter();
                ITOFLIXUser NewUser = userConverter.Convert(newUserRequest);

                string adminUserPassword = "Admin123!";
                identityResult = _signInManager.UserManager.CreateAsync(NewUser, adminUserPassword).Result;
                _signInManager.UserManager.AddToRoleAsync(NewUser, "ContentAdmin").Wait();

            }
        }

        public void CreateRoles()
        {
            ITOFLIXRole iTOFLIXRole;

            iTOFLIXRole = new ITOFLIXRole("Administrator");
            _roleManager.CreateAsync(iTOFLIXRole).Wait();

            iTOFLIXRole = new ITOFLIXRole("ContentAdmin");
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
            Category category = new Category { Name = "Korku" };
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
            Category category9 = new Category { Name = "Fantastik" };
            _context.Categories.Add(category9);
            Category category10 = new Category { Name = "Yerli" };
            _context.Categories.Add(category10);
            Category category11 = new Category { Name = "Yabancı" };
            _context.Categories.Add(category11);

            _context.SaveChanges();
        }

        public void CreateExampleMedia()
        {
            //Media initialization Friends series as example

            //Create example Actors
            Actor actor = new Actor() { Name = "Jennifer Aniston" };
            _context.Actors.Add(actor);
            Actor actor1 = new Actor() { Name = "Matthew Perry" };
            _context.Actors.Add(actor1);
            Actor actor2 = new Actor() { Name = "David Schwimmer" };
            _context.Actors.Add(actor2);
            Actor actor3 = new Actor() { Name = "Courteney Cox" };
            _context.Actors.Add(actor3);
            Actor actor4 = new Actor() { Name = "Paul Rudd" };
            _context.Actors.Add(actor4);
            Actor actor5 = new Actor() { Name = "Tom Selleck" };
            _context.Actors.Add(actor5);

            _context.SaveChanges();

            //Create example Directors
            Director director = new Director() { Name = "David Crane" };
            _context.Directors.Add(director);
            Director director1 = new Director() { Name = "David Crane" };
            _context.Directors.Add(director1);
            _context.SaveChanges();

            //Create example Media
            MediaCreateRequest mediaCreateRequest = new MediaCreateRequest()
            {
                Name = "Friends",
                Description = "string",
                CategoryIds = { 3 },
                ActorIds = {1,2,3,4,5,6},
                DirectorIds = {1,2},
                RestrictionIds = {13}
            };
            _context.Media.Add(_mediaConverter.Convert(mediaCreateRequest));
            _context.SaveChanges();

            //Create example Episodes
            DateTime dt;
            DateTime.TryParse("1994-01-01", out dt);
            Episode episode = new Episode()
            {
                Title = "First Episode",
                Description = "Friends Episode Description",
                Duration = TimeSpan.FromMinutes(22),
                SeasonNumber = 1,
                EpisodeNumber = 1,
                ReleaseDate = dt,
                MediaId = 1,
                Passive = false
            };
            _context.Episodes.Add(episode);
            Episode episode1 = new Episode()
            {
                Title = "Second Episode",
                Description = "Friends Episode Description",
                Duration = TimeSpan.FromMinutes(22),
                SeasonNumber = 1,
                EpisodeNumber = 2,
                ReleaseDate = dt,
                MediaId = 1,
                Passive = false
            };
            _context.Episodes.Add(episode1);
            Episode episode2 = new Episode()
            {
                Title = "Third Episode",
                Description = "Friends Episode Description",
                Duration = TimeSpan.FromMinutes(22),
                SeasonNumber = 1,
                EpisodeNumber = 3,
                ReleaseDate = dt,
                MediaId = 1,
                Passive = false
            };
            _context.Episodes.Add(episode2);
            Episode episode3 = new Episode()
            {
                Title = "Fourth Episode",
                Description = "Friends Episode Description",
                Duration = TimeSpan.FromMinutes(22),
                SeasonNumber = 2,
                EpisodeNumber = 4,
                ReleaseDate = dt,
                MediaId = 1,
                Passive = false
            };
            _context.Episodes.Add(episode3);
            Episode episode4 = new Episode()
            {
                Title = "Fifth Episode",
                Description = "Friends Episode Description",
                Duration = TimeSpan.FromMinutes(22),
                SeasonNumber = 2,
                EpisodeNumber = 5,
                ReleaseDate = dt,
                MediaId = 1,
                Passive = false
            };
            _context.Episodes.Add(episode4);


            //Media initialization The Great Gatsby series as example


            //Create example Actors
            Actor actor6 = new Actor() { Name = "Toby Stephens" };
            _context.Actors.Add(actor6);
            Actor actor7 = new Actor() { Name = "Francie Swift" };
            _context.Actors.Add(actor7);
            Actor actor8 = new Actor() { Name = "Mira Sorvino" };
            _context.Actors.Add(actor8);
            _context.SaveChanges();

            //Create example Directors
            Director director3 = new Director() { Name = "Robert Markowitz" };
            _context.Directors.Add(director3);
            _context.SaveChanges();

            //Create example Media
            MediaCreateRequest mediaCreateRequest2 = new MediaCreateRequest()
            {
                Name = "The Great Gatsby",
                Description = "string",
                CategoryIds = { 10,12 },
                ActorIds = {  5, 7,8,9 },
                DirectorIds = { 3 },
                RestrictionIds = { 13 }
            };
            _context.Media.Add(_mediaConverter.Convert(mediaCreateRequest2));
            _context.SaveChanges();

            //Create example Episodes
            DateTime dt2;
            DateTime.TryParse("2000-03-29", out dt2);
            Episode episode5 = new Episode()
            {
                Title = "The Great Gatsby",
                Description = "The Great Gatsby Movie 2000",
                Duration = TimeSpan.FromMinutes(90),
                SeasonNumber = 1,
                EpisodeNumber = 1,
                ReleaseDate = dt2,
                MediaId = 2,
                Passive = false
            };
            _context.Episodes.Add(episode5);
            _context.SaveChanges();
        }
    }
}