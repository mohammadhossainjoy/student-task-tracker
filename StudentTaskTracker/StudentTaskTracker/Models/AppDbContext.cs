using System.Data.Entity;

namespace StudentTaskTracker.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DBEFConnection") { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
