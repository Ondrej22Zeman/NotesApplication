using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesApplication.Data;
using NotesApplication.Services;
using NotesApplication.Mappings;
using NotesApplication.Models;
using NotesApplication.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(typeof(NoteMapperProfile));
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.MapGet("/", () => Results.Content("<h1>Helloworld</h1>", "text/html"));

app.Run();