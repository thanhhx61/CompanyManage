using EmployeeManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DBContexts
{
    public class CompanyDBContext : IdentityDbContext<AppIdentityUser>
    {
        public CompanyDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Offices> Offices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedingOffice(modelBuilder);
            SeedingEmployee(modelBuilder);
            SeedingAspNetUser(modelBuilder);
            SeedingAspNetRole(modelBuilder);
            SeedingAspNetUserRole(modelBuilder);
        }
        private void SeedingOffice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offices>().HasData(
                new Offices()
                {
                    OfficeId = 1,
                    OfficeName = "Văn Phòng Hồ Chí Minh",
                    Street = "Số 9 Đinh Tiên Hoàng, P.Đakao, Q1,TP.HCM",
                    Amount = 0,
                    IsDeleted = false
                },
                new Offices()
                {
                    OfficeId = 2,
                    OfficeName = "Văn Phòng Huế",
                    Street = "Số 2 Lê Quý Đôn, P.Phú Hội, TP. Huế, Tỉnh Thừa Thiên Huế",
                    Amount = 0,
                    IsDeleted = false
                }
                );
        }
        private void SeedingEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>().HasData(
                new Employees()
                {
                    EmployeeId = 1,
                    EmployeeName = "Nguyễn Văn B",
                    Dob = DateTime.Now,
                    Photo = "/Images/avatars/05082021_3.png",
                    Street = "tp Huế",
                    Workingday = DateTime.Now,
                    Skill = ".Net",
                    Introduce = "thích chơi thể thao",
                    OfficeId = 2
                },
                new Employees()
                {
                    EmployeeId = 2,
                    EmployeeName = "nguyễn văn A",
                    Dob = DateTime.Now,
                    Photo = "/Images/avatars/06082021121552_1-small",
                    Street = "tp HCM",
                    Workingday = DateTime.Now,
                    Skill = "Java",
                    Introduce = "thích nghe nhạc",
                    OfficeId = 1
                }
                );
        }
        private void SeedingAspNetUser(ModelBuilder modelBuilder)
        {
            AppIdentityUser user = new AppIdentityUser()
            {
                Id = "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                UserName = "Xuân Thanh",
                NormalizedEmail = "xuanthanh6198@gmail.com",
                NormalizedUserName = "xuanthanh6198@gmail.com",
                PhoneNumber = "0814262547",
                LockoutEnabled = false,
                Avatar = "/Images/hello.jpg",
                EmaillUser = "xuanthanh6198@gmail.com"

            };
            PasswordHasher<AppIdentityUser> passwordHasher = new PasswordHasher<AppIdentityUser>();
            var passwordHash = passwordHasher.HashPassword(user, "Thanh123.");
            user.PasswordHash = passwordHash;

            modelBuilder.Entity<AppIdentityUser>().HasData(user);
        }
        private void SeedingAspNetRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    Name = "SystemAdmin",
                    NormalizedName = "SystemAdmin",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole()
                {
                    Id = "32ffd287-205f-43a2-9f0d-80bc5309fb47",
                    Name = "Customer",
                    NormalizedName = "Customer",
                    ConcurrencyStamp = "2"
                });
        }
        private void SeedingAspNetUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    UserId = "2c0fca4e-9376-4a70-bcc6-35bebe497866"
                }
                );
        }
    }
}