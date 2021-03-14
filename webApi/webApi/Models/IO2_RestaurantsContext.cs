using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace webApi.Models
{
    public partial class IO2_RestaurantsContext : DbContext
    {
        public IO2_RestaurantsContext()
        {
        }

        public IO2_RestaurantsContext(DbContextOptions<IO2_RestaurantsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<DiscountCode> DiscountCodes { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionDish> SectionDishes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=IO2_Restaurants;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("postal_code");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("street");
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.ToTable("Complaint");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer)
                    .IsUnicode(false)
                    .HasColumnName("answer");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.State).HasColumnName("state");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Complaint_User");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Complaint_Order");
            });

            modelBuilder.Entity<DiscountCode>(entity =>
            {
                entity.ToTable("Discount_Code");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.DateFrom)
                    .HasColumnType("datetime")
                    .HasColumnName("date_from");

                entity.Property(e => e.DateTo)
                    .HasColumnType("datetime")
                    .HasColumnName("date_to");
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Dish_Section");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CodeId).HasColumnName("code_id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.FinalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("final_price");

                entity.Property(e => e.OrderState).HasColumnName("order_state");

                entity.Property(e => e.OriginalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("original_price");

                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Address");

                entity.HasOne(d => d.Code)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Discount_Code");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Users");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("rating");

                entity.Property(e => e.State).HasColumnName("state");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Restaurant_Address");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("rating");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_User");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Restaurant");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_Section_Restaurant");
            });

            modelBuilder.Entity<SectionDish>(entity =>
            {
                entity.ToTable("Section_Dish");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DishId).HasColumnName("dish_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.SectionDishes)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Section_Dish_Dish");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.SectionDishes)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Section_Dish_Section");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password_hash");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_User_Address");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
