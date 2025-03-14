﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProductEntity
{
  [Key]
  public int Id { get; set; }
  public string ProductName { get; set; } = null!;
  public decimal Price { get; set; }
}