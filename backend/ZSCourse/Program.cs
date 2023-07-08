using ZSCourse.FileService;
using ZSCourse.IdentityService;
using ZSCourse.ListeningService;

var builder = WebApplication.CreateBuilder(args);

IdentityServiceInitializer.Init(builder);
FileServiceInitializer.Init(builder);
ListeningServiceInitializer.Init(builder);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
