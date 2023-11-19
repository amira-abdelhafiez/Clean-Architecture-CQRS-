using Microsoft.EntityFrameworkCore;
using PioneersAPI.Application.Interfaces;
using PioneersAPI.Domain;
using PioneersAPI.Domain.Common;

namespace PioneersAPI.Infrastructure.Persisance.DBContext
{
    public class ApplicationDBContext : DbContext
    {

        private readonly IDateTimeService _dateTime;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IDateTimeService dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = "1"; // No authentication so just add dummy user
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = "1";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Just seed data for test

            builder.Entity<Goods>().HasData(new Goods() { 
                Id = 1,
                TransactionId = "EZI172",
                TransactionDate = DateTime.Now,
                Amount = 182,
                Comments = "This is just dummy data",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                LastModifiedBy = "1",
                CreatedBy = "1",
                Direction = "In"

            });

            builder.Entity<Goods>().HasData(new Goods()
            {
                Id = 2,
                TransactionId = "UWJS72",
                TransactionDate = DateTime.Now,
                Amount = 9932,
                Comments = "This is just dummy data",
                Created = DateTime.Now,
                CreatedBy = "1",
                LastModified = DateTime.Now,
                LastModifiedBy = "1",
                Direction = "Out"

            });
            base.OnModelCreating(builder);
        }
    }
}
