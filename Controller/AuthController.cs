//gọi MVC
using Microsoft.AspNetCore.Mvc;
// gọi models chứa entity
using  ATMmanagementApplication.Models;
//chứa context
using ATMmanagementApplication.Data;
//dùng thư viện Linq để truy vấn thực thể Ob
using System.Linq;

//tạo namespace
namespace ATMmanagementApplication.Cotrollers{
    [ApiController] // để xuất bản được api
    [Route("api/auth")]// để truy cập, tạo điều hướng
    //kế thừa từ ControllerAuth
    public class AuthController : Controller{ //bên dưới sẽ định nghĩa những tính chất để truy vấn
        private readonly ATMContext _context;
        public AuthController(ATMContext context){ //contructor tạo 1 đói tượng
            _context = context; 
        }

        //vì là 1 contoller chắc chắn có IActionResult đặc điểm của controller
        [HttpPost("login")]
        public IActionResult Login([FromBody] Customer login){
            var customer = _context.Customers       //Customers <- đây chính là database
            .FirstOrDefault( c => c.Name == login.Name && c.Password == login.Password);

            if(customer == null){
                return Unauthorized("Sai thông tin đăng nhập");
            }

            //return Ok là return 200
            return Ok(new {message = "Login successfull", customerId = customer.CustomerId}); 
        }
    }
}
