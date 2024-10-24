using System;
using System.ComponentModel.DataAnnotations;

namespace UsermanagementApplication.Models{
    public class User{
        [Key]
        public int UserId { get; set; }
         //required nó sẽ kiểm tra và yêu cầu người dùng phải điền k được để trống
        public required string Username { get; set; }
       
        public required string UserPassword { get; set; }
       
        public required string Email { get; set; }
    }
}