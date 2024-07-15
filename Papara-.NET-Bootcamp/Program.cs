using Microsoft.AspNetCore.Diagnostics;
using Papara_Bootcamp.Models;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Papara_Bootcamp.Services;
using Papara_Bootcamp.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookService, FakeBookService>();

var app = builder.Build();

app.UseMiddleware<ActionEntryLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var error = context.Features.Get<IExceptionHandlerFeature>();

            if (error != null)
            {
                var ex = error.Error;

                var errorResponse = new ApiResponse
                {
                    Success = false,
                    Message = "Beklenmeyen bir hata ger�ekle�ti.",
                    Data = ex.Message
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        });

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
