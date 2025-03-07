using System;
using System.ComponentModel.DataAnnotations;

namespace ATMmanagementApplication.Models{
    //định nghĩa cấu trúc dữ liệu
    public class Transaction { 
        [Key]
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSuccessful { get; set; }
        public TransactionType TransactionType {get; set; }
    }
    public enum TransactionType{
        Deposit,
        Withdraw,
        Tranfers,
        Receive
    }
}