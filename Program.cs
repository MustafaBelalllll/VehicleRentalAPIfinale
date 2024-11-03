using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Szolgáltatások regisztrálása a konténerekhez.
builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext<VehicleRentalContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger csak fejlesztési környezetben
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});

app.UseCors("AllowAllOrigins"); // Itt hívják meg a CORS -t!

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
