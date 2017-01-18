namespace Service.Database
{
    using System.Data.Entity;

    public class LineContext : DbContext
    {
        static LineContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LineContext>());
        }

        public LineContext() : base("DefaultConnection")
        {
        }

        public DbSet<Line> Lines { get; set; } 
    }
}
