using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace souqcomApp.Models;

public partial class SouqcomContext : DbContext
{
    public SouqcomContext()
    {
    }

    public SouqcomContext(DbContextOptions<SouqcomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=JOSEPH-THARWAT;Database=souqcom;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("admins");

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.AdminPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("admin_password");
            entity.Property(e => e.AdminUserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("admin_userName");
        });

        modelBuilder.Entity<Cart>(entity =>
        {   
            entity.ToTable("cart");

            entity.Property(e => e.CartId)
                .ValueGeneratedNever()
                .HasColumnName("cart_id");
            entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");
            entity.Property(e => e.CartQuantity).HasColumnName("cart_quantity");
            entity.Property(e => e.CartUserId).HasColumnName("cart_user_id");

            entity.HasOne(d => d.CartItem).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CartItemId)
                .HasConstraintName("FK_cart_items_id");

            entity.HasOne(d => d.CartUser).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CartUserId)
                .HasConstraintName("FK_cart_users_id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_description");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");
            entity.Property(e => e.CategoryPhoto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_photo");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("items");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("item_id");
            entity.Property(e => e.ItemCategoryId).HasColumnName("item_category_id");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_description");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemPhoto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_photo");
            entity.Property(e => e.ItemPrice).HasColumnName("item_price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
