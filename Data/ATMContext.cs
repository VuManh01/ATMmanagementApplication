using Microsoft.EntityFrameworkCore;
using ATMmanagementApplication.Models;
using System.Security.Cryptography.X509Certificates;
using UsermanagementApplication.Models;


namespace ATMmanagementApplication.Data{
    public class ATMContext: DbContext{
        public ATMContext(DbContextOptions<ATMContext> options): base(options){}
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
namespace UsermanagementApplication.Data {
    public class UserContext : DbContext {
        public UserContext(DbContextOptions<UserContext> options): base(options){}
        // Định nghĩa các DbSet ở đây
        public DbSet<User> Users { get; set; }
    }
}
