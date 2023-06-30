using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces.ManyToManyInterfaces;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;
using ProductStore.Shops.Shops.Domain.Domain.Models;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations.ManyToManyRepo;
/// <summary>
/// ХЗ, дописать
/// </summary>
public class ProductsWithTypesRepo: BaseRepo<ProductsWithTypes>, IProductsWithTypesRepo
{

    public ProductsWithTypesRepo(ShopsContext _context)
    :base(_context)
    {
    }

    
}