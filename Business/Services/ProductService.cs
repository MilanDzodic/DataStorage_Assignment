using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class ProductService(ProductRepository productRepository)
{
  private readonly ProductRepository _productRepository = productRepository;

  public async Task CreateProductAsync(ProductRegistrationForm form)
  {
    var productEntity = ProductFactory.Create(form);
    await _productRepository.CreateAsync(productEntity!);
  }

  public async Task<IEnumerable<Product?>> GetProductsAsync()
  {
    var productEntities = await _productRepository.GetAllAsync();
    var products = productEntities.Select(ProductFactory.Create);

    return products;
  }

  public async Task<Product?> GetProductByNameAsync(string productName)
  {
    var productEntity = await _productRepository.GetAsync(x => x.ProductName == productName);
    var product = ProductFactory.Create(productEntity);

    return product;
  }

  public async Task<Product?> UpdateProductAsync(int Id, Product form)
  {
    var existingProduct = await _productRepository.GetAsync(x => x.Id == form.Id);
    if (existingProduct == null)
      return null;

    existingProduct.ProductName = form.ProductName ?? existingProduct.ProductName;

    var result = await productRepository.UpdateAsync(existingProduct);

    if (result == null)
      return null!;

    return ProductFactory.Create(existingProduct);
  }

  public async Task<bool> DeleteProductAsync(int id)
  {
    var result = await _productRepository.DeleteAsync(x => x.Id == id);
    return result;
  }

}
