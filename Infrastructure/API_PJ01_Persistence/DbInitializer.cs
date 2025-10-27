using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities.Products;
using API_PJ01_Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_PJ01_Persistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
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
    }
}
