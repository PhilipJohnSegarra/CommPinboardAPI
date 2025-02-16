using AutoMapper;
using AutoMapper.Internal.Mappers;
using CommPinboardAPI.Data;
using CommPinboardAPI.Helpers;
using CommPinboardAPI.Helpers.Interfaces;
using CommPinboardAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ObjectMapper));
builder.Services.AddSingleton<HashHelper>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IPostHelper, PostHelper>();
builder.Services.AddScoped<ICommentHelper, CommentHelper>();
builder.Services.AddScoped<IPinnedPostHelper, PinnedPostHelper>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
