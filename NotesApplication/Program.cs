using Microsoft.EntityFrameworkCore;
using NotesApplication.Data;
using NotesApplication.Services;
using NotesApplication.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();