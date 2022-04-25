using Microsoft.EntityFrameworkCore;
using ToolWorkshop.Data.Entities;
using ToolWorkshop.Enums;
using ToolWorkshop.Helpers;

namespace ToolWorkshop.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();
            //_context.Database.Migrate();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckWarehousePlanogramAsync();
            await CheckUserAsync("1010", "Juan", "Vasquez", "juanv@yopmail.com", "322 311 4620", "Avenida Siempreviva", "Brad.jpg", UserType.Admin);
            await CheckUserAsync("1020", "Andres", "Martinez", "andrem@yopmail.com", "322 311 4620", "P sherman calle wallaby 42 sydney", "bob.jpg", UserType.Admin);
            await CheckUserAsync("2010", "Pedro", "Galindo", "pedrog@yopmail.com", "322 311 4620", "Privet Drive 4", "LedysBedoya.jpeg", UserType.User);


        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string image,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                    ImageId = imageId
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
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
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
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
                _context.Countries.Add(new Country
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

        private async Task CheckWarehousePlanogramAsync()
        {
            City Medellin = await _context.Cities.Include(c => c.State).ThenInclude(s => s.Country).FirstOrDefaultAsync(c => c.Name == "Medellín" && c.State.Country.Name == "Colombia");
            City Tampa = await _context.Cities.Include(c => c.State).ThenInclude(s => s.Country).FirstOrDefaultAsync(c => c.Name == "Tampa" && c.State.Country.Name == "Estados Unidos");

            if (!_context.Warehouses.Any())
            {
                _context.Warehouses.Add(
                    new Warehouse()
                    {
                        City = Medellin,
                        Name = "Bodega Ciudad Del Rio",
                        Description = "Bodega de suministros",
                        Planograms = new List<Planogram>()
                        {
                            new Planogram()
                            {
                                Name = "Estanteria A2",
                                Type = "Estanteria"
                            },
                            new Planogram()
                            {
                                Name = "Estanteria B4",
                                Type = "Estanteria"
                            }
                        }
                    });

                _context.Warehouses.Add(
                    new Warehouse()
                    {
                        City = Tampa,
                        Name = "Bodega Palm Harbor",
                        Description = "Bodega de repuestos",
                        Planograms = new List<Planogram>()
                        {
                            new Planogram()
                            {
                                Name = "Estanteria Z3",
                                Type = "Estanteria"
                            },
                            new Planogram()
                            {
                                Name = "Estanteria H7",
                                Type = "Estanteria"
                            }
                        }
                    });
            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category
                {
                    Name = "Herramientas Mecánicas",
                    Tools = new Tool[] {
                       new Tool {
                            Name = "Taladro",
                            Description = "Taladro",
                            EAN = "345678901234567890"
                       },
                       new Tool
                        {
                            Name = "Sierra Electrica",
                            Description = "Sierra",
                            EAN = "234567890123456789"
                        }
                    }
                });

                _context.Categories.Add(new Category
                {
                    Name = "Herramienta de montaje:",
                    Tools = new Tool[] {
                        new Tool
                        {
                            Name = "Destornillador",
                            Description = "Destornillador",
                            EAN = "567890123456789012"
                        }
                    }
                });

                _context.Categories.Add(new Category { Name = "Medicion" });
                _context.Categories.Add(new Category
                {
                    Name = "Caja de Herramientas",
                    Tools = new Tool[]
                    {
                        new Tool
                        {
                            Name = "LLave Inglesa",
                            Description = "LLave Inglesa",
                            EAN = "456789012345678901"
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
