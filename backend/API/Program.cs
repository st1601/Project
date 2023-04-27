using API.Helpers.EmailHelper;
using API.Repositories.Implements;
using API.Repositories.Interfaces;
using API.Services.Implements;
using API.Services.Interfaces;
using Common.Jwt;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<IIdeaService, IdeaService>();

builder.Services.AddScoped<IIdeaRepository, IdeaRepository>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IThumbRespository, ThumbRespository>();

builder.Services.AddScoped<IThumbsService, ThumbsService>();

builder.Services.AddScoped<IEmailService, EmailService>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddControllers();

var configuration = builder.Configuration;
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IUsersService, UsersService>();

JsonSerializerOptions options = new()
{
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
};

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.RequireHttpsMetadata= false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = JwtConstant.Issuer,
            ValidAudience= JwtConstant.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Key)),
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors(builder => {
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
