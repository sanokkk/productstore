using ProductStore.Shops.Shops.DAL.Repositories.Interfaces;
using ProductStore.Shops.Shops.DAL.Repositories.Interfaces.ManyToManyInterfaces;
using ProductStore.Shops.Shops.Domain.Domain.ManyToManyModels;

namespace ProductStore.Shops.Shops.DAL.Repositories.Implementations.ManyToManyRepo;

public class ProductsShopsRepo: BaseRepo<ProductShop>, IProductsShopsRepo
{
    public ProductsShopsRepo(ShopsContext context) : base(context)
    {
    }
}