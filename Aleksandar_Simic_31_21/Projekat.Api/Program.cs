using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Projekat.Api.Core;
using ProjekatApplication;
using ProjekatApplication.UseCases.Commands.BrandCat;
using ProjekatApplication.UseCases.Commands.Brands;
using ProjekatApplication.UseCases.Commands.Category;
using ProjekatApplication.UseCases.Commands.Products;
using ProjekatApplication.UseCases.Commands.Users;
using ProjekatApplication.UseCases.Queries;
using ProjekatDataAccess;
using ProjekatImplementation;
using ProjekatImplementation.Logging;
using ProjekatImplementation.UseCases.Commands;
using ProjekatImplementation.UseCases.Queries;
using ProjekatImplementation.Validators;
using static Dapper.SqlMapper;
using System.Text;
using Projekat.Api;
using Projekat.Api.Extensions;
using ProjekatApplication.UseCases.Commands.Cart;
using ProjekatApplication.UseCases.Commands.ProductCart;
using ProjekatApplication.UseCases.Commands.Orders;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProjekatImplementation.Email;
using ProjekatApplication.Email;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ProjekatContext>();
builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<JwtTokenCreator>();
builder.Services.AddTransient<IUseCaseLogger, DBUseCaseLogger>();
//Brand implementation
builder.Services.AddTransient<ICreateBrandCommand, EfCreateBrandCommand>();
builder.Services.AddTransient<IGetBrandsQuery, EfGetBrandsQuery>();
builder.Services.AddTransient<IDeleteBrandCommand, EfDeleteBrandCommand>();
builder.Services.AddTransient<CreateBrandValidator>();
//Category implementation
builder.Services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
builder.Services.AddTransient<IGetCategoriesQuery, EfGetCategoryQuery>();
builder.Services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
builder.Services.AddTransient<CreateCategoryValidator>();
//Brand category implementation
builder.Services.AddTransient<ICreateBCCommand, EfCreateBCCommand>();
builder.Services.AddTransient<IGetBCQuery, EfGetBCQuery>();
builder.Services.AddTransient<IDeleteBCCommand, EfDeleteBCCommand>();
builder.Services.AddTransient<CreateBCValidator>();
//Product implementation
builder.Services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
builder.Services.AddTransient<IGetProductQuery, EfGetProductQuery>();
builder.Services.AddTransient<IGetSingleProductQuery, EfGetSingleProductQuery>();
builder.Services.AddTransient<IUpdateProductCommand, EfUpdateProductCommand>();
builder.Services.AddTransient<IDeleteProductCommand, EfDeleteProductCommand>();
builder.Services.AddTransient<CreateProductValidator>();
builder.Services.AddTransient<UpdateProductValidator>();
//Cart implementation
builder.Services.AddTransient<ICreateProductCartCommand, EfCreateProductCartCommand>();
builder.Services.AddTransient<IGetCartInfoQuery, EfGetCartInfoQuery>();
builder.Services.AddTransient<CreateCartValidator>();
//ProductCart implementation
builder.Services.AddTransient<IUpdateQuantityCommand, EfUpdateQuantityCommand>();
builder.Services.AddTransient<IDeleteProductFromCartCommand, EfDeleteProductFromCartCommand>();
builder.Services.AddTransient<UpdateQuantityValidator>();
//Order implementation
builder.Services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
builder.Services.AddTransient<IGetOrderQuery, EfGetOrderQuery>();
//AuditLog implementation
builder.Services.AddTransient<IGetAuditLogsQuery, EfGetAuditLogsQuery>();
//User implementation
builder.Services.AddTransient<ICreateUserCommand, EfUserCreateCommand>();
builder.Services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
builder.Services.AddTransient<CreateUserValidator>();
//mail
builder.Services.AddTransient<ProjekatApplication.Email.IEmailSender, SmtpEmailSender>();
builder.Services.AddTransient<IApplicationActorProvider>(x => 
{
    var accessor = x.GetService<IHttpContextAccessor>();

    var request = accessor.HttpContext.Request;

    var authHeader = request.Headers.Authorization.ToString();

    var context = x.GetService<ProjekatContext>();
    var actor = new JwtApplicationActorProvider(authHeader);
    return actor;
});
builder.Services.AddTransient<IApplicationActor>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    if (accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    return x.GetService<IApplicationActorProvider>().GetActor();
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "asp_api",
        ValidateIssuer = true,
        ValidAudience = "Any",
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKeyForAspProject")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    cfg.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            //Token dohvatamo iz Authorization header-a

            Guid tokenId = context.HttpContext.Request.GetTokenId().Value;

            var storage = builder.Services.BuildServiceProvider().GetService<ITokenStorage>();

            if (!storage.Exists(tokenId))
            {
                context.Fail("Invalid token");
            }


            return Task.CompletedTask;

        }
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjekatAsp", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
});

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
