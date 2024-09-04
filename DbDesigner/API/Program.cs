using API.Utils;
using Common.Dtos;
using Common.Exceptions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServicies();
builder.Services.AddInfrastructure();
builder.Services.AddOptions(builder.Configuration);
builder.Services.AddMappers();
builder.Services.AddGoogleAndJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var validationResult = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new ValidationDto {FieldName = e.Key, Message = e.Value?.Errors.First().ErrorMessage })
            .ToList();
        return new BadRequestObjectResult(validationResult);
    };
});

var app = builder.Build();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.MapControllers();

app.Run();