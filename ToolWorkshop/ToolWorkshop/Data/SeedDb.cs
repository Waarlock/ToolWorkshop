using ToolWorkshop.Data.Entities;
using ToolWorkshop.Enums;
using ToolWorkshop.Helpers;

namespace ToolWorkshop.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userhelper)
        {
            _context = context;
            _userHelper = userhelper;
        }

        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            //  await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Juan", "Vasquez", "juanv@yopmail.com", "322 311 4620", "Avenida Siempreviva", UserType.Admin);
            await CheckUserAsync("1020", "Andres", "Martinez", "andrem@yopmail.com", "322 311 4620", "P sherman calle wallaby 42 sydney", UserType.Admin);
            await CheckUserAsync("2010", "Pedro", "Galindo", "pedrog@yopmail.com", "322 311 4620", "Privet Drive 4", UserType.User);


        }

        private async Task<User> CheckUserAsync(
     string document,
     string firstName,
     string lastName,
     string email,
     string phone,
     string address,
     UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Name = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    Document = document,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());

        }

            private async Task CheckCountriesAsync()
              {
                  if (!_context.countries.Any())
                  {
                      _context.countries.Add(new Country
                      {
                          Name = "Colombia",
                          States = new List<State>()
                          {
                              new State()
                              {
                                  Name = "Antioquia",
                                  Cities = new List<City>() {
                                      new City() { Name = "Medellín" },
                                      new City() { Name = "Itagüí" },
                                      new City() { Name = "Envigado" },
                                      new City() { Name = "Bello" },
                                      new City() { Name = "Rionegro" },
                                  }
                              },
                              new State()
                              {
                                  Name = "Bogotá",
                                  Cities = new List<City>() {
                                      new City() { Name = "Usaquen" },
                                      new City() { Name = "Champinero" },
                                      new City() { Name = "Santa fe" },
                                      new City() { Name = "Useme" },
                                      new City() { Name = "Bosa" },
                                  }
                              },
                          }
                      });
                      _context.countries.Add(new Country
                      {
                          Name = "Estados Unidos",
                          States = new List<State>()
                          {
                              new State()
                              {
                                  Name = "Florida",
                                  Cities = new List<City>() {
                                      new City() { Name = "Orlando" },
                                      new City() { Name = "Miami" },
                                      new City() { Name = "Tampa" },
                                      new City() { Name = "Fort Lauderdale" },
                                      new City() { Name = "Key West" },
                                  }
                              },
                              new State()
                              {
                                  Name = "Texas",
                                  Cities = new List<City>() {
                                      new City() { Name = "Houston" },
                                      new City() { Name = "San Antonio" },
                                      new City() { Name = "Dallas" },
                                      new City() { Name = "Austin" },
                                      new City() { Name = "El Paso" },
                                  }
                              },
                          }
                      });
                  }

                  await _context.SaveChangesAsync();
              }
     

        private async Task CheckCategoriesAsync()
        {
            if (!_context.categories.Any())
            {
                _context.categories.Add(new Category { Name = "Caja de Herramientas" });
                _context.categories.Add(new Category { Name = "Medicion" });

                await _context.SaveChangesAsync();
            }
        }
    }
}
