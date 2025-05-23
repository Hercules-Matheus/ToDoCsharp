using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using toDo.Models;

namespace toDo.Contexts;

public class ToDoContext : IdentityDbContext<ApplicationUser>
{
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    {
    }
    
  public DbSet<Todo> Todos => Set<Todo>();
}
