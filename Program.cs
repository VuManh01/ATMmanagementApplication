using Microsoft.EntityFrameworkCore;
using ATMmanagementApplication.Data;
using UsermanagementApplication.Data; // Thêm không gian tên cho UserContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container ==> thiết lập cấu hình models
builder.Services.AddControllers();

// Đăng ký DbContext cho ATM
builder.Services.AddDbContext<ATMContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    new MySqlServerVersion(new Version(8, 0, 403))));

// Đăng ký DbContext cho User
builder.Services.AddDbContext<UserContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 403))));

// Build the app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
