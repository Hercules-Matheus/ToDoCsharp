using Microsoft.EntityFrameworkCore;
using toDo.Models;

namespace toDo.Contexts;

public class ToDoContext : DbContext
{
  public DbSet<Todo> Todos => Set<Todo>();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=toDo.sqlite3");
  }
}
