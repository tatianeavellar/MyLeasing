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

        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();


        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Owners.Any())
            {
                AddOwner("João", "Rodrigues", "Rua Eça de Queiroz");
                AddOwner("Maria", "Callas", "Avenida 25 de Abril");
                AddOwner("Maria", "Lópes", "Rua do Brasil");
                AddOwner("Rui", "Costa", "Rua Maria Teles Mendes");
                AddOwner("Tatiane", "Avellar", "Luis de Camões");
                AddOwner("Luiz", "Santos", "Av Saboia");
                AddOwner("Miguel", "Oliveira", "Av Tomás Ribeiro");
                AddOwner("Sandra", "Matos", "Rua 1º de Maio");
                AddOwner("Felipe", "Reis", "Rua Bartolomeu Dias");
                AddOwner("Ana", "Antunes", "Avenida da Liberdade");
                await _context.SaveChangesAsync();
            }

        }

        private void AddOwner(string firstName, string lastName, string address)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                FixedPhone = _random.Next(999999999),
                CellPhone = _random.Next(999999999),

            }) ;
        }
    }
}
