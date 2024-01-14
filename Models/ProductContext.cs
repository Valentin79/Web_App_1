using Microsoft.EntityFrameworkCore;

namespace Web_API.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost; Database=GB2; Integrated Security=False;
                TrustServerCertificate=true; Trusted_Connection=True").UseLazyLoadingProxies();// Сюда строку подключения
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.Id).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Coast)
                .HasColumnName("Coast")
                .IsRequired();

                entity.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.Id)
                .HasConstraintName("CategoryToProduct");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("ProductCategorys");
                entity.HasKey(x => x.Id).HasName("CategoryID");
                entity.HasIndex(x => x.Name) .IsUnique();
                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");
                entity.HasKey(x => x.Id).HasName("StorageID");
                entity.Property(e => e.Name)
                .HasColumnName("StorageName");

                //entity.Property(c => c.ProductID).HasColumnName("ProductID");
                entity.HasMany(x => x.Products).WithMany(m => m.Storages)
                .UsingEntity(j => j.ToTable("StorageProduct"));
            });
        }
    }
}
