using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProductFactory
{
  public static ProductEntity? Create(ProductRegistrationForm form) => form == null ? null : new()
  {
    ProductName = form.ProductName,
    Price = form.Price
};

  public static Product? Create(ProductEntity entity) => entity == null ? null : new()
  {
    Id = entity.Id,
    ProductName = entity.ProductName,
    Price = entity.Price
  };
}
