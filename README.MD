1. Create project asp.net webapi

2. Cài đặt thư viện cho Entity framework (data model)
dotnet add package Pomelo.EntityFrameworkCore.Mysql (Pomelo MySQL Provider)
dotnet add package Microsoft.EntityFrameworkCore.Tools

3. Đồng bộ hóa với Database (Tạo Migration)
dotnet ef migrations add InitiaCreate => tạo bảng

Cập nhật vào database
dotnet ef database update

Cập nhật lại vào database
dotnet ef  database update

DbSet(thành phần chứa dữ liệu tạm thời tr khi làm việc với database) -> chính cái này gọi là entity, nếu có 50 entity -> tức có 50 cái Dbset -> sẽ có từng mấy table
trong phần Data. cái ATMContext:DbSet nó sẽ biết có bao nhiên entity nó sẽ biết sinh ra bao nhiêu table.

tạo 1 project tiền tố ban đầu API => tức là có thiết lập có ConnectionStrings: trình kết nối của database, SslMode=no kết nối HTTP chứ k phải HTPPS

CreateBuild, ..: chắc chắn phải có 

đầu tiên: clear phần Model
tiếp đến quan tâm phần Data
=====================================
truy vấn trên đối tượng (class) thì dùng: Linq

Bài tập:

Sau khi hoàn thành tính năng (withdrawal) và gửi tiền (deposit),

Yêu cầu phát triển các tính năng sau:

Lịch sử giao dịch của người dùng.
Chuyển tiền (Transfer) giữa các tài khoản.
Quản lý người dùng: Đăng ký và đăng nhập.
Thay đổi mật khẩu.
Thông báo qua email cho các giao dịch.
Làm bài tập cá nhân và submit link git