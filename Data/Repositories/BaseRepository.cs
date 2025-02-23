using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
  private readonly DataContext _context = context;
  private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

  public virtual async Task<TEntity> CreateAsync(TEntity entity)
  {
    if (entity == null)
      return null!;

    try
    {
      await _dbSet.AddAsync(entity);
      await _context.SaveChangesAsync();

      return entity;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error creating {nameof(TEntity)} entity :: {ex.Message}");
      return null!;
    }
  }

  public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
  {
    if(expression == null) 
      return null!;

    return await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
  }

  public virtual async Task<TEntity> UpdateAsync(TEntity updatedEntity)
  {
    if (updatedEntity == null)
      return null!;

    try
    {
      _context.Entry(updatedEntity).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return updatedEntity;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error updating {nameof(TEntity)} entity :: {ex.Message}");
      return null!;
    }
  }
  
  public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
  {
    if(expression == null )
      return false;

    try
    {
      var exisitingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
      if (exisitingEntity == null)
        return false;

      _dbSet.Remove(exisitingEntity);
      await _context.SaveChangesAsync();
      return true;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error updating {nameof(TEntity)} entity :: {ex.Message}");
      return false;
    }
  }

  public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expresssion)
  {
    return await _dbSet.AllAsync(expresssion);
  }
}
