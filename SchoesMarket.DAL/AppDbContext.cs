using Microsoft.EntityFrameworkCore;
using ShoesMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users {  get; set; }
        public DbSet<OrderEntity> Orders{  get; set; }
        public DbSet<PickupPointEntity> PickupPoints {  get; set; }
        public DbSet<ProductEntity> Products {  get; set; }
        public DbSet<RoleEntity> Roles{  get; set; }
        public DbSet<OrderDetailsEntity> OrdersDetailes{  get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = 1, Name = "Администратор" },
                new RoleEntity { Id = 2, Name = "Менеджер" },
                new RoleEntity { Id = 3, Name = "Авторизированный клиент" }
            );
        }
    }
}
