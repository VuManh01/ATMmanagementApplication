using Microsoft.AspNetCore.Mvc;
using ATMmanagementApplication.Models;
using ATMmanagementApplication.Data;
using System.Linq;
using System;

//namespace ATMmanagementApplication.Controllers 
//định nghĩa ra ATMController

// class and interface phải nằm trong sự quản lý của namespace
namespace ATMmanagementApplication.Controllers{
    [ApiController]
    [Route("api/atm")]
    public class ATMController : ControllerBase{

        private readonly ATMContext _context;
        public ATMController(ATMContext context){
            _context = context;
        }

    //Các phương thức xử lý API cho ATM sẽ được viết ở đây
        
        //Lịch sử giao dịch
       [HttpGet("history/{customerId}")]
        public IActionResult GetTransactionHistory(int customerId) {
            var transactions = _context.Transactions
            .Where(t => t.CustomerId == customerId)
            .OrderByDescending(t => t.Timestamp)
            .ToList();

            if (!transactions.Any()) return NotFound("No transactions found for this customer");

            return Ok(transactions);
        }

        [HttpGet("balance/{customerId}")]
        public IActionResult GetBalance(int customerId){
            var customer = _context.Customers.Find(customerId);
            if(customer == null) return NotFound("Customer no found");

            return Ok(new {balance = customer.Balance});
        }

        //xử lý phần rút tiền
        [HttpPost("withdraw")] 
        public IActionResult Withdraw([FromBody] withdrawRequest request){
            var customer = _context.Customers.Find(request.CustomerId);
            if(customer==null) return NotFound("Customer not found");

            if(customer.Balance <request.Amount) 
            return BadRequest("Insufficient balance");

            customer.Balance -= request.Amount;

            var trancsaction = new Transaction{
                CustomerId = request.CustomerId,
                Amount = request.Amount,
                Timestamp = DateTime.Now,
                IsSuccessful = true,
                TransactionType = TransactionType.Withdraw
            }; 

            _context.Transactions.Add(trancsaction);
            _context.SaveChanges(); //lưu từ Dbset vào Database

            return Ok(new {message ="Withdraw successful", newBalance = customer.Balance});
        }

        //xử lý phần gửi tiền
        [HttpPost("deposit")] 
        public IActionResult Deposit([FromBody] depositRequest request){
            var customer = _context.Customers.Find(request.CustomerId);
            if(customer==null) return NotFound("Customer not found");

            if(customer.Balance < request.deposit)
            return BadRequest("Insufficient balance");

            var BalanceNew = customer.Balance - request.deposit;
            customer.Balance = BalanceNew;
            
            var transaction = new Transaction{
                CustomerId = request.CustomerId,
                Amount = BalanceNew,
                Timestamp = DateTime.Now,
                IsSuccessful = true,
                TransactionType = TransactionType.Deposit 
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            
            return Ok(new {message = "Deposit successful", newBalance = BalanceNew});
        }

        //chuyển tiền giữa các tài khoản
        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransferRequest request) {
            var fromCustomer = _context.Customers.Find(request.FromCustomerId);
            var toCustomer = _context.Customers.Find(request.ToCustomerId);

            if (fromCustomer == null || toCustomer == null) return NotFound("One or both customers not found");
            if (fromCustomer.Balance < request.Amount) return BadRequest("Insufficient balance");

            fromCustomer.Balance -= request.Amount;
            toCustomer.Balance += request.Amount;

            var transaction = new Transaction {
                CustomerId = request.FromCustomerId,
                Amount = request.Amount,
                Timestamp = DateTime.Now,
                IsSuccessful = true,
                TransactionType = TransactionType.Tranfers
            };  

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok(new {message = "Transfer successful", newBalance = fromCustomer.Balance});
        }


    }
    public class withdrawRequest{
        public int CustomerId {get; set; }
        public decimal Amount {get; set; }
    }
    public class depositRequest{
        public int CustomerId {get; set; }
        public decimal deposit {get; set; }
    }
    public class TransferRequest {
        public int FromCustomerId { get; set; }
        public int ToCustomerId { get; set; }
        public decimal Amount { get; set; }
    }

}