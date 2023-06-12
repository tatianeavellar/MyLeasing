using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private Random _random;

        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _random = new Random();


        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userManager.FindByEmailAsync("tatiane.c.r.avellar@gmail.com");
            if(user == null) 
            {
                user = new User
                {
                    Document = "999999999",
                    FirstName = "Tatiane",
                    LastName = "Avellar",
                    Email = "tatiane.c.r.avellar@gmail.com",
                    UserName = "tatiane.c.r.avellar@gmail.com",
                    Address= "Rua Qualquer",
                    PhoneNumber = "1234567890"
                };

                var result = await _userManager.CreateAsync(user, "123456");
                if(result!=IdentityResult.Success) 
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }
            }


            if (!_context.Owners.Any())
            {
                AddOwner("João", "Rodrigues", "Rua Eça de Queiroz", user);
                AddOwner("Maria", "Callas", "Avenida 25 de Abril", user);
                AddOwner("Maria", "Lópes", "Rua do Brasil", user);
                AddOwner("Rui", "Costa", "Rua Maria Teles Mendes", user);
                AddOwner("Tatiane", "Avellar", "Luis de Camões", user);
                AddOwner("Luiz", "Santos", "Av Saboia",user );
                AddOwner("Miguel", "Oliveira", "Av Tomás Ribeiro", user);
                AddOwner("Sandra", "Matos", "Rua 1º de Maio", user);
                AddOwner("Felipe", "Reis", "Rua Bartolomeu Dias", user);
                AddOwner("Ana", "Antunes", "Avenida da Liberdade", user);
                await _context.SaveChangesAsync();
            }

        }

        private void AddOwner(string firstName, string lastName, string address, User user)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                FixedPhone = _random.Next(999999999),
                CellPhone = _random.Next(999999999),
                User = user

            }) ;
        }
    }
}
