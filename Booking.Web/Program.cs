using Booking.Data;
using Booking.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT
builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var publicKey = config["Jwt:Public"];
    RSA rsa = RSA.Create();
    var publicKeyBytes = Convert.FromBase64String(publicKey);
    rsa.ImportRSAPublicKey(publicKeyBytes, out var _);

    return new RsaSecurityKey(rsa);
});//jwt token reader
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var rsaPublicKey = builder.Services.BuildServiceProvider()
        .GetRequiredService<RsaSecurityKey>();

        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            //setting the security key
            IssuerSigningKey = rsaPublicKey,
            //validation of this key
            ValidateIssuerSigningKey = true,

            //option for checking(validating) the server for whom token is sent when token is validating
            ValidateIssuer = false,
            //option for checking(validating) the client 
            ValidateAudience = false,
            //option for validation the life time of token
            ValidateLifetime = true,

            //only signed tokens
            RequireSignedTokens = true,
            //only tokens with exp.time
            RequireExpirationTime = true,

        };
    });


//add front
builder.Services.AddCors(options =>
{
    options.AddPolicy("local react", policyOptions => { policyOptions.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod(); });
    
});

//-------------------------------------------------------
//Dependecy injections

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAuthService, AuthService>();
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
app.UseAuthentication();
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
    context.Database.Migrate();
}
//-------------------------------------------------------

app.Run();
