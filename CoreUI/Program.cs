using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using SamuraiApp.Core.Data;
using SamuraiApp.Core.Domain;

namespace CoreUI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            // InsertSamurai("Olli Paasikoski");
            // InsertMultipleSamurais();
            // SimpleSamuraiQuery();
            // var response = MoreQueries();
            // RetrieveAndUpdate();
            // RetrieveAndUpdateDisconnected();
            // DeleteSamurai();
            // DeleteSamuraiDisconnected();
            // RawSqlQuery();
            // SqlStoredProcedure();
            SqlCommand();
            _context.SaveChanges();
            _context.Dispose();
        }

        private static void SqlCommand() 
        {
            var procedureResult = new SqlParameter() 
            {
                ParameterName = "@result",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 50
            };
            _context.Database.ExecuteSqlCommand(
                "EXEC FindLongestName @result OUT", procedureResult
            );
            Console.WriteLine($"Result: {procedureResult.Value}");
        }

        private static void SqlStoredProcedure() 
        {
            var namePart = "Ryo";
            var samurais = _context.Samurais.FromSql(
                "EXEC FilterSamuraiByNamePart {0}", namePart)
                .OrderByDescending(s => s.Name).ToList();
            
            samurais.ForEach(s => Console.WriteLine($"{s.Name}"));
            Console.WriteLine();
        }        

        private static void RawSqlQuery()
        {
            // Note that FromSql return a IQueryable (create pagedlist from it in REST)
            var samurais = _context.Samurais.FromSql("SELECT * FROM SAMURAIS").ToList();
            samurais.ForEach(s => Console.WriteLine($"{s.Name}"));
            Console.WriteLine();
        }

        private static void DeleteSamuraiDisconnected()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Olli Paasikoski");

            if (samurai != null) 
            {
                using (var context = new SamuraiContext()) 
                {
                    context.Remove(samurai);
                    context.SaveChanges();
                }   
            }    
        }

        private static void DeleteSamurai()
        {
            var samurais = _context.Samurais.Where(s => s.Name == "Olli Paasikoski").ToList();
            // if deleting by id e.g. REST Delete, 
            // better to use:
            // var guid = Guid.NewGuid();
            // var samurai = _context.Samurais.Remove(_context.Samurais.Find(guid));

            if (samurais != null) {
                _context.RemoveRange(samurais);
            }
        }

        private static void RetrieveAndUpdateDisconnected()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name = "John Mclee";
            
            // will post all properties, not only changed property as 
            // it would with a tracked entity
            using (var context = new SamuraiContext()) {
                context.Samurais.Update(samurai);
                context.SaveChanges();
            }
        }

        private static void RetrieveAndUpdate()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static List<Samurai> MoreQueries()
        {
            var name = "Ryo";
            // return _context.Samurais.FirstOrDefault(s => s.Name == "Ryo")
            return _context.Samurais.Where(s => s.Name.Contains(name)).ToList();
        }

        private static void SimpleSamuraiQuery()
        {
            var samurais = _context.Samurais
                .OrderBy(s => s.Name).ToList();

            Console.WriteLine($"Query found { samurais.Count } samurais.");
        }

        private static void InsertMultipleSamurais()
        {
            var samurais = new List<Samurai>() {
                new Samurai() {
                    Id = Guid.NewGuid(),
                    Name = "Ryo Sasaki"
                },
                new Samurai() {
                    Id = Guid.NewGuid(),
                    Name = "Rashid Jabalameli"
                }
            };

            _context.Samurais.AddRange(samurais);
            _context.SaveChanges();

        }

        private static void InsertSamurai(string name)
        {
            var samurai = new Samurai() {
                Id = Guid.NewGuid(),
                Name = name
            };

            _context.Samurais.Add(samurai);
        }
    }
}
