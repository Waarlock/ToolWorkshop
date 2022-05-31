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
            await CheckUserAsync("1020", "Andres", "Martinez", "andrem@yopmail.com", "322 311 4620", "P sherman calle wallaby 42 sydney", "liv.jpg", UserType.Admin);
            await CheckUserAsync("2010", "Pedro", "Galindo", "pedrog@yopmail.com", "322 311 4620", "Privet Drive 4", "bob.jpg", UserType.User);
            await CheckToolAsync();

        }

        private async Task CheckToolAsync()
        {
            if (!_context.Tools.Any())
            {
                await AddToolAsync("Micrometro", "9F7845SE7874", 12F, new List<string>() { "Medición" }, new List<string>() { "micrometro.png", "micrometro2.png" });
                await AddToolAsync("Caja de Herramienta", "9X7000R07874", 15F, new List<string>() { "Medición", "Precisión", "Mecánicas" }, new List<string>() { "CajaHerrammientas.png", "CajaHerrammientas2.png", "CajaHerrammientas3.png" });
                await AddToolAsync("Pulidora", "6F7845SE7234", 3F, new List<string>() { "Precisión", "Corte" }, new List<string>() { "cortadora.png", "cortadora2.png" });
                await AddToolAsync("Pie de Rey", "10R5445XE7001", 8F, new List<string>() { "Medición" }, new List<string>() { "PieDeRey.png", "PieDeRey2.png" });
                await AddToolAsync("Taladro", "50T5455PF054", 3F, new List<string>() { "Precisión" }, new List<string>() { "taladro.png", "taladro2.png", "taladro3.png" });
                await AddToolAsync("Torquimetro", "56Y565XL1011", 6F, new List<string>() { "Medición" }, new List<string>() { "torquimetro2.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddToolAsync(string name, String EAN, float stock, List<string> categories, List<string> images)
        {
            Tool tool = new()
            {
                Description = name,
                Name = name,
                EAN = EAN,
                Stock = stock,
                ToolCategories = new List<ToolCategory>(),
                ToolImages = new List<ToolImage>()
            };

            foreach (string? category in categories)
            {
                tool.ToolCategories.Add(new ToolCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }


            foreach (string? image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\tools\\{image}", "products");
                tool.ToolImages.Add(new ToolImage { ImageId = imageId });
            }

            _context.Tools.Add(tool);
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
                        new City() { Name = "Sabaneta" },
                        new City() { Name = "La Ceja" },
                        new City() { Name = "La Union" },
                        new City() { Name = "La Estrella" },
                        new City() { Name = "Copacabana" },
                    }
                },
                new State()
                {
                    Name = "Bogotá",
                    Cities = new List<City>() {
                        new City() { Name = "Usaquen" },
                        new City() { Name = "Champinero" },
                        new City() { Name = "Santa fe" },
                        new City() { Name = "Usme" },
                        new City() { Name = "Bosa" },
                    }
                },
                new State()
                {
                    Name = "Valle",
                    Cities = new List<City>() {
                        new City() { Name = "Calí" },
                        new City() { Name = "Jumbo" },
                        new City() { Name = "Jamundí" },
                        new City() { Name = "Chipichape" },
                        new City() { Name = "Buenaventura" },
                        new City() { Name = "Cartago" },
                        new City() { Name = "Buga" },
                        new City() { Name = "Palmira" },
                    }
                },
                new State()
                {
                    Name = "Santander",
                    Cities = new List<City>() {
                        new City() { Name = "Bucaramanga" },
                        new City() { Name = "Málaga" },
                        new City() { Name = "Barrancabermeja" },
                        new City() { Name = "Rionegro" },
                        new City() { Name = "Barichara" },
                        new City() { Name = "Zapatoca" },
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
                new State()
                {
                    Name = "California",
                    Cities = new List<City>() {
                        new City() { Name = "Los Angeles" },
                        new City() { Name = "San Francisco" },
                        new City() { Name = "San Diego" },
                        new City() { Name = "San Bruno" },
                        new City() { Name = "Sacramento" },
                        new City() { Name = "Fresno" },
                    }
                },
            }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Ecuador",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Pichincha",
                    Cities = new List<City>() {
                        new City() { Name = "Quito" },
                    }
                },
                new State()
                {
                    Name = "Esmeraldas",
                    Cities = new List<City>() {
                        new City() { Name = "Esmeraldas" },
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
                _context.Categories.Add(new Category { Name = "Mecánicas" });
                _context.Categories.Add(new Category { Name = "Medición" });
                _context.Categories.Add(new Category { Name = "Precisión" });
                _context.Categories.Add(new Category { Name = "Corte" });
                await _context.SaveChangesAsync();
            }

        }
    }
}
