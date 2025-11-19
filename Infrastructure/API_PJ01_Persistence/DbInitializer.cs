using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities.Identity;
using API_PJ01_Domain.Entities.Products;
using API_PJ01_Persistence.Data.Contexts;
using API_PJ01_Persistence.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_PJ01_Persistence
{
    public class DbInitializer(
        StoreDbContext _context,
        IdentityStoreDbContext _identityContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbInitializer
    {
        
        public async Task InitializeAsync()
        {
            //Create Db
            //Update Db
            if(_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();
            }
            //Seed Data

            #region PB DataSeeding
            if (!_context.ProductBrands.Any())
            {
                var brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\API_PJ01_Persistence\DataSeeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);

                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);

                }
            }
            #endregion

            #region PT DataSeeding
            if (!_context.ProductTypes.Any())
            {
                var typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\API_PJ01_Persistence\DataSeeding\types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);

                if (types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);

                }
            }
            #endregion

            #region Product DataSeeding
            if (!_context.Products.Any())
            {
                var productsdata = await File.ReadAllTextAsync(@"..\Infrastructure\API_PJ01_Persistence\DataSeeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);

                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);

                }
            }
            #endregion

            await _context.SaveChangesAsync();
        }

        public async Task InitializeIdentityAsync()
        {
            if (_identityContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _identityContext.Database.MigrateAsync();
            }

            //Data Seeding for Identity Db can be done here
            if (!_identityContext.Roles.Any())
            {
                await _roleManager.CreateAsync(role: new IdentityRole() { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(role: new IdentityRole() { Name = "Admin" });
            }

            if (!_identityContext.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01233345555"
                };

                var admin = new AppUser()
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01233344555"
                };

                var r1 = await _userManager.CreateAsync(superAdmin, "P@ssW0rd");
                if (!r1.Succeeded)
                {
                    throw new InvalidOperationException("Failed to create SuperAdmin: " + string.Join("; ", r1.Errors.Select(e => e.Description)));
                }

                var r2 = await _userManager.CreateAsync(admin, "P@ssW0rd");
                if (!r2.Succeeded)
                {
                    throw new InvalidOperationException("Failed to create Admin: " + string.Join("; ", r2.Errors.Select(e => e.Description)));
                }

                var createdSuper = await _userManager.FindByNameAsync(superAdmin.UserName)
                                    ?? throw new InvalidOperationException("SuperAdmin not found after create");
                var createdAdmin = await _userManager.FindByNameAsync(admin.UserName)
                                    ?? throw new InvalidOperationException("Admin not found after create");

                var addRole1 = await _userManager.AddToRoleAsync(createdSuper, "SuperAdmin");
                if (!addRole1.Succeeded)
                    throw new InvalidOperationException("Failed to add SuperAdmin role: " + string.Join("; ", addRole1.Errors.Select(e => e.Description)));

                var addRole2 = await _userManager.AddToRoleAsync(createdAdmin, "Admin");
                if (!addRole2.Succeeded)
                    throw new InvalidOperationException("Failed to add Admin role: " + string.Join("; ", addRole2.Errors.Select(e => e.Description)));

            }
        }
    }
}
