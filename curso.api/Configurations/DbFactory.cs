using curso.api.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.api.Configurations
{
    public class DbFactory : IDesignTimeDbContextFactory<CourseDbContext>
    {
        public CourseDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<CourseDbContext> options = new DbContextOptionsBuilder<CourseDbContext>();
            options.UseSqlServer("Server=localhost;Database=Course;user=USR#COURSE;password=course@2022;");
            CourseDbContext context = new CourseDbContext(options.Options);

            return context;
        }
    }
}
