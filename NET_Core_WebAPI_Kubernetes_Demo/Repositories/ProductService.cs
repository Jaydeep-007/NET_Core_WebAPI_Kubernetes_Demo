using Microsoft.EntityFrameworkCore;
using NET_Core_WebAPI_Kubernetes_Demo.Data;
using NET_Core_WebAPI_Kubernetes_Demo.Entities;

namespace NET_Core_WebAPI_Kubernetes_Demo.Repositories
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass dbContextClass;

        public ProductService(DbContextClass dbContextClass)
        {
            this.dbContextClass = dbContextClass;
        }

        public async Task<List<ProductDetails>> ProductListAsync()
        {
            return await dbContextClass.Products.ToListAsync();
        }

        public async Task<ProductDetails> GetProductDetailByIdAsync(int productId)
        {
            return await dbContextClass.Products.Where(ele => ele.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddProductAsync(ProductDetails productDetails)
        {
            await dbContextClass.Products.AddAsync(productDetails);
            var result = await dbContextClass.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductDetails productDetails)
        {
            var isProduct = ProductDetailsExists(productDetails.Id);
            if (isProduct)
            {
                dbContextClass.Products.Update(productDetails);
                var result = await dbContextClass.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var findProductData = dbContextClass.Products.Where(_ => _.Id == productId).FirstOrDefault();
            if (findProductData != null)
            {
                dbContextClass.Products.Remove(findProductData);
                var result = await dbContextClass.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool ProductDetailsExists(int productId)
        {
            return dbContextClass.Products.Any(e => e.Id == productId);
        }
    }
}
