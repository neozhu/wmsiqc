﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "全名")]
        public string FullName { get; set; }
        [Display(Name = "性别")]
        public int Gender { get; set; }
        public int AccountType { get; set; }
        [Display(Name = "所属公司")]
        public string CompanyCode { get; set; }
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }
        [Display(Name = "是否在线")]
        public bool IsOnline { get; set; }
        [Display(Name = "是否开启聊天功能")]
        public bool EnabledChat { get; set; }
        [Display(Name = "小头像")]
        public string AvatarsX50 { get; set; }
        [Display(Name = "大头像")]
        public string AvatarsX120 { get; set; }
    }

   

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    public System.Data.Entity.DbSet<WebApp.Models.RECEIPT> RECEIPTs { get; set; }

    public System.Data.Entity.DbSet<WebApp.Models.RECEIPTDETAIL> RECEIPTDETAILs { get; set; }

    public System.Data.Entity.DbSet<WebApp.Models.STOCK> STOCKs { get; set; }
  }

   
}