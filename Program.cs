using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Szolg�ltat�sok regisztr�l�sa a kont�nerekhez.
builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext<VehicleRentalContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger csak fejleszt�si k�rnyezetben
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

app.UseCors("AllowAllOrigins"); // Itt h�vj�k meg a CORS -t!

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
