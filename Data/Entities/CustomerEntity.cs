﻿using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerEntity
{
  [Key]
  public int Id { get; set; }
  public string CustomerName { get; set; } = null!;
  public virtual ICollection<ProductEntity> Projects { get; set; } = [];
}
