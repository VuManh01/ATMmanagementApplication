using System.ComponentModel.DataAnnotations;

namespace ATMmanagementApplication.Models{
    public class Customer{
        [Key] //Annotation => Primary key trong java @Id
        public int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public decimal Balance { get; set; }
    }
}