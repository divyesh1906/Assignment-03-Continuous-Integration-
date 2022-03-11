using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company_List.Models;

namespace Company_List.Models
{
    public class TransactionContext : DbContext
    {
        //Context file created for database connection
        public TransactionContext(DbContextOptions<TransactionContext> options)
                   : base(options)
        {


        }

        //Transaction table connectivity
        public DbSet<Transaction> Transactions { get; set; }
        //TransactionType table Connection
        public DbSet<TransactionType> TransactionTypes { get; set; }
        //company database table
        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Set entity for database
            modelBuilder.Entity<Transaction>().HasData(
               new Transaction
               {
                   TransactionId = 1,
                   Quantity = 100,
                   SharePrice = 500,
                   TransactionTypeId = "S",
                   CompanyID = 1,
               },
                  new Transaction
                  {
                      TransactionId = 2,
                      Quantity = 100,
                      SharePrice = 500,
                      TransactionTypeId = "B",
                      CompanyID = 2,
                  },
                  new Transaction
                  {
                      TransactionId = 3,
                      Quantity = 100,
                      SharePrice = 500,
                      TransactionTypeId = "B",
                      CompanyID=3,
                  }
               );

            modelBuilder.Entity<TransactionType>().HasData(
              new TransactionType { TransactionTypeId = "S", TransactionName = "Sell", CommissionFee = 5.99 },
              new TransactionType { TransactionTypeId = "B", TransactionName = "Buy", CommissionFee = 5.20 }

          );
            modelBuilder.Entity<Company>().HasData(
               new Company { CompanyID = 1, CompanyName = "Alexa", TickerSymbol = "AlX", Address = "41 Hamilton Street" },
               new Company { CompanyID = 2, CompanyName = "Google", TickerSymbol = "GG", Address = "141 Toronto Street"},
               new Company { CompanyID = 3, CompanyName = "Microsoft", TickerSymbol = "MSFT", Address = "321 Microsoft Street" }
           );
        }


        }
}
