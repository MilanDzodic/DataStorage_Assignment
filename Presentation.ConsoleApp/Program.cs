using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp;

var services = new ServiceCollection()
  .AddDbContext<DataContext>(x => x.UseSqlServer())
  .AddScoped<CustomerRepository>()
  .AddScoped<ProductRepository>()
  .AddScoped<StatusTypeRepository>()
  .AddScoped<UserRepository>()
  .AddScoped<MenuDialogs>()
  .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<MenuDialogs>();
await menuDialogs.MenuOptions();