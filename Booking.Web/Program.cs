using Booking.Data;
using Booking.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add front
builder.Services.AddCors(options =>
{
    options.AddPolicy("local react", policyOptions => { policyOptions.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod(); });
    
});

//-------------------------------------------------------
//Dependecy injections

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
/*builder.Services.AddScoped<BookingDbContext>();*/
builder.Services.AddDbContext<BookingDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("BookingDb");
    options.UseSqlServer(connectionString);
});
//-------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("local react");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//-------------------------------------------------------
//Data base
//(creates scope for one dbContext but for different requests)
//(+makes sure that db is created)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookingDbContext>();
   /* context.Database.EnsureDeleted();*/
    context.Database.EnsureCreated();
}
//-------------------------------------------------------

app.Run();
