using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
    public class pdfContext : DbContext
    {
        public pdfContext(DbContextOptions<pdfContext> options)
            : base(options)
        {
        }
        
        public DbSet<pdf> pdfs { get; set;}
    }
}