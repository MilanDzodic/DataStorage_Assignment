using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class MenuDialogs(CustomerService customerService, ProductService productService)
{
  private readonly CustomerService _customerService = customerService;
  private readonly ProductService _productService = productService;

  public async Task MenuOptions()
  {
    while (true)
    {
      Console.WriteLine("1. Create new customer");
      Console.WriteLine("2. Get all customers");
      Console.WriteLine("3. Update customer");
      Console.WriteLine("4. Delete customer");
      Console.WriteLine("5. Create new product");
      Console.WriteLine("6. Get all products");
      Console.WriteLine("7. Update product");
      Console.WriteLine("8. Delete product");

      var option = Console.ReadLine();

      switch (option)
      {
        case "1":
          await CreateCustomerOption();
          break;

        case "2":
          await GetCustomersOption();
          break;

        case "3":
          await UpdateCustomerOption();
          break;

        case "4":
          await DeleteCustomerOption();
          break;

        case "5":
          await CreateProductOption();
          break;

        case "6":
          await GetProductsOption();
          break;

        case "7":
          await UpdateProductOption();
          break;

        case "8":
          await DeleteProductOption();
          break;

        default:
          break;
      }
    }
  }

  private async Task CreateCustomerOption()
  {
    Console.Clear();
    Console.WriteLine("### Create Customer ###");
    Console.Write("Customer Name: ");
    var customerName = Console.ReadLine()!;

    var newCustomer = new CustomerRegistrationForm
    {
      CustomerName = customerName
    };

    var result = await _customerService.CreateCustomerAsync(newCustomer);
    if (result == null)
    {
      Console.WriteLine("Customer was created successfully");
    }
    else
    {
      Console.WriteLine("Customer was not created");
    }

    Console.ReadKey();
  }

  private async Task GetCustomersOption()
  {
    Console.Clear();
    Console.WriteLine("### Get Customers ###");

    var customers = await _customerService.GetCustomersAsync();
    if (customers != null && customers.Any())
    {
      foreach (var customer in customers)
      {
        Console.WriteLine($"{customer?.CustomerName}>");
      }
    }
    else
    {
      Console.WriteLine("No Customers found");
    }
    Console.ReadKey();
  }

  private async Task UpdateCustomerOption()
  {
    Console.Clear();
    Console.WriteLine("### Update Customer ###");

    var existingCustomer = await ShowAvailableCustomersAndSelect();
    if (existingCustomer == null)
      return;


    Console.WriteLine($"Updating Customer: {existingCustomer.CustomerName}");

    Console.Write($"New Customer Name (current: {existingCustomer.CustomerName}, leave empty to keep): ");
    var customerName = Console.ReadLine();

    var updatedForm = new Customer
    {
      CustomerName = customerName!
    };

    var result = await _customerService.UpdateCustomerAsync(existingCustomer.Id, updatedForm);
    if (result != null)
    {
      Console.WriteLine("Customer was updated successfully");
    }
    else
    {
      Console.WriteLine("Customer was not updated");
    }

    Console.ReadKey();

  }


  private async Task DeleteCustomerOption()
  {
    Console.Clear();
    Console.WriteLine("### Delete Customer ###");

    var existingCustomer = await ShowAvailableCustomersAndSelect();
    if (existingCustomer == null)
      return;

    Console.WriteLine($"Are you sure you want to delete Customer: {existingCustomer.CustomerName}? (y/n): ");
    var option = Console.ReadLine();

    if (option?.ToLower() == "y")
    {
      var result = await _customerService.DeleteCustomerAsync(existingCustomer.Id);

      if (result)
      {
        Console.WriteLine("Customer was deleted successfully");
      }
      else
      {
        Console.WriteLine("Customer was not deleted");
      }

      Console.ReadKey();

    }
    else
    {
      Console.WriteLine("Delete operation cancelled");
    }
    Console.ReadKey();
  }

  private async Task<Customer?> ShowAvailableCustomersAndSelect()
  {
    var customers = await _customerService.GetCustomersAsync();
    if (customers == null || !customers.Any())
    {
      Console.WriteLine("No customers found to update");
      Console.ReadKey();
      return null;
    }

    Console.WriteLine("Available Customers: ");
    foreach (var customer in customers)
    {
      Console.WriteLine($"ID: {customer.Id} {customer.CustomerName}");
    }

    Console.Write("Enter Customer Id to manage:");
    if (!int.TryParse(Console.ReadLine(), out var customerId))
    {
      Console.WriteLine("Invalid Id");
      Console.ReadKey();
      return null;
    }

    var existingCustomer = customers.FirstOrDefault(x => x.Id == customerId);
    if (existingCustomer == null)
    {
      Console.WriteLine("Customer not found");
      Console.ReadKey();
      return null;
    }

    return existingCustomer;
  }


  private async Task CreateProductOption()
  {
    Console.Clear();
    Console.WriteLine("### Create Product ###");
    Console.Write("Product Name: ");
    var productName = Console.ReadLine()!;

    var newProduct = new ProductRegistrationForm
    {
      ProductName = productName
    };

    var result = await _productService.CreateProductAsync(newProduct);
    if (result == null)
    {
      Console.WriteLine("Product was created successfully");
    }
    else
    {
      Console.WriteLine("Product was not created");
    }

    Console.ReadKey();
  }

  private async Task GetProductsOption()
  {
    Console.Clear();
    Console.WriteLine("### Get Products ###");

    var products = await _productService.GetProductsAsync();
    if (products != null && products.Any())
    {
      foreach (var product in products)
      {
        Console.WriteLine($"{product?.ProductName}>");
      }
    }
    else
    {
      Console.WriteLine("No Products found");
    }
    Console.ReadKey();
  }

  private async Task UpdateProductOption()
  {
    Console.Clear();
    Console.WriteLine("### Update Product ###");

    var existingProduct = await ShowAvailableProductAndSelect();
    if (existingProduct == null)
      return;


    Console.WriteLine($"Updating Product: {existingProduct.ProductName}");

    Console.Write($"New Product Name (current: {existingProduct.ProductName}, leave empty to keep): ");
    var customerName = Console.ReadLine();

    var updatedForm = new Product
    {
      ProductName = productName
    };

    var result = await _productService.UpdateProductAsync(existingProduct.Id, updatedForm);
    if (result != null)
    {
      Console.WriteLine("Product was updated successfully");
    }
    else
    {
      Console.WriteLine("Product was not updated");
    }

    Console.ReadKey();

  }


  private async Task DeleteProductOption()
  {
    Console.Clear();
    Console.WriteLine("### Delete Product ###");

    var existingProduct = await ShowAvailableProductAndSelect();
    if (existingProduct == null)
      return;

    Console.WriteLine($"Are you sure you want to delete Product: {existingProduct.ProductName}? (y/n): ");
    var option = Console.ReadLine();

    if (option?.ToLower() == "y")
    {
      var result = await _productService.DeleteProductAsync(existingProduct.Id);

      if (result)
      {
        Console.WriteLine("Product was deleted successfully");
      }
      else
      {
        Console.WriteLine("Product was not deleted");
      }

      Console.ReadKey();

    }
    else
    {
      Console.WriteLine("Delete operation cancelled");
    }
    Console.ReadKey();
  }


  private async Task<Product?> ShowAvailableProductAndSelect()
  {
    var products = await _productService.GetProductsAsync();
    if (products == null || !products.Any())
    {
      Console.WriteLine("No Products found to update");
      Console.ReadKey();
      return null;
    }

    Console.WriteLine("Available Products: ");
    foreach (var product in products)
    {
      Console.WriteLine($"ID: {product.Id} {product.ProductName}");
    }

    Console.Write("Enter Product Id to manage:");
    if (!int.TryParse(Console.ReadLine(), out var productId))
    {
      Console.WriteLine("Invalid Id");
      Console.ReadKey();
      return null;
    }

    var existingProduct = products.FirstOrDefault(x => x.Id == productId);
    if (existingProduct == null)
    {
      Console.WriteLine("Product not found");
      Console.ReadKey();
      return null;
    }

    return existingProduct;
  }
}

