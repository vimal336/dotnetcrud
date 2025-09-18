using Microsoft.EntityFrameworkCore;
using StudentAPI.Model;

namespace DemoApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}