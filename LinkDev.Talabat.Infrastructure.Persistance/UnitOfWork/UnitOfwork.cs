using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using LinkDev.Talabat.Infrastructure.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistance.UnitOfWork
{
    internal class UnitOfwork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly Lazy<IGenericRepository<Product, int>> _productRepository;
        private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandsRepository;
        private readonly Lazy<IGenericRepository<ProductCategory, int>> _CategoriesRepository;

        public UnitOfwork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
            _brandsRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
            _CategoriesRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));
        }

        public IGenericRepository<Product, int> ProductRepository => _productRepository.Value;
        public IGenericRepository<ProductBrand, int> BrandsRepository => _brandsRepository.Value;
        public IGenericRepository<ProductCategory, int> CategoryRepository => _CategoriesRepository.Value;  

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();  
      

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();   
       
    }
}
