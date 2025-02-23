using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
  private readonly CustomerRepository _customerRepository = customerRepository;

  public async Task CreateCustomerAsync(CustomerRegistrationForm form) 
  {
    var customerEntity = CustomerFactory.Create(form);
    await _customerRepository.CreateAsync(customerEntity!);
  }

  public async Task<IEnumerable<Customer?>> GetCustomersAsync()
  {
    var customerEntities = await _customerRepository.GetAllAsync();
    var customers = customerEntities.Select(CustomerFactory.Create);

    return customers;
  }

  public async Task<Customer?> GetCustomerByNameAsync(string customerName)
  {
    var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
    var customer = CustomerFactory.Create(customerEntity);

    return customer;
  }

  public async Task<Customer?> UpdateCustomerAsync(int Id, Customer form)
  {
    var existingCustomer = await _customerRepository.GetAsync(x => x.Id == form.Id);
    if (existingCustomer == null)
      return null;

    existingCustomer.CustomerName = form.CustomerName ?? existingCustomer.CustomerName;

    var result = await _customerRepository.UpdateAsync(existingCustomer);

    if (result == null)
      return null!;

    return CustomerFactory.Create(existingCustomer);
  }

  public async Task<bool> DeleteCustomerAsync(int id)
  {
    var result = await _customerRepository.DeleteAsync(x => x.Id == id);
    return result;
  }

}
