using AniraSP.DAL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AniraSP.DAL {
    public class AniraSpDbContext : DbContext {
        public AniraSpDbContext(DbContextOptions<AniraSpDbContext> options) : base(options) { }
        public virtual DbSet<Offer> Offers { get; set; }
    }
}