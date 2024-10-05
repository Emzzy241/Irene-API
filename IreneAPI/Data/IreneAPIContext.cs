using IreneAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IreneAPI.Data;

public class IreneAPIContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Payment> Payments { get; set; }

    public IreneAPIContext(DbContextOptions<IreneAPIContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Call base method to configure Identity tables properly
        base.OnModelCreating(builder);

        builder.Entity<Payment>()
            .HasData(
                new Payment { Id = 1, FirstName = "Emmanuel", LastName = "Mojiboye", Amount = 1000 },
                new Payment { Id = 2, FirstName = "John", LastName = "Doe", Amount = 20000 },
                new Payment { Id = 3, FirstName = "Racheal", LastName = "Jackson", Amount = 40000 }
            );
    }
}



// using IreneAPI.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// namespace IreneAPI.Data;

// public class IreneAPIContext : IdentityDbContext<ApplicationUser>
// {
//     public DbSet<Payment> Payments { get; set; }

//     public IreneAPIContext(DbContextOptions<IreneAPIContext> options) : base(options)
//     {

//     }

//     protected override void OnModelCreating(ModelBuilder builder)
//     {
//         builder.Entity<Payment>()
//         .HasData(
//             new Payment { Id = 1, FirstName = "Emmanuel", LastName = "Mojiboye", Email ="Dyna123@gmail.com", Password="Dynasty", Amount = 1000 },
//             new Payment { Id = 2, FirstName = "John",  LastName = "Doe", Email ="Dyna123@gmail.com", Password="Dynasty", Amount = 20000},
//             new Payment { Id = 3, FirstName = "Racheal", LastName = "Jackson", Email ="Dyna123@gmail.com", Password="Dynasty", Amount = 40000 }
//         );

        

        
//     }
// }