using Microsoft.EntityFrameworkCore;
using Todolist.Models.Entities;

namespace Todolist.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Tododataset> Tododatasets { get; set; }
    }
}
