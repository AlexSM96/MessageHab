var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
builder.Services.AddApplicationServices(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(p =>
    {
        p.SetIsOriginAllowed(origin => true);
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowCredentials();
    });
}).AddSignalR();

var app = builder.Build();
app.Services.CreateTable();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapHub<MHub>("/messagehub");
app.MapControllers();
app.Run();
