using System.Data.SqlClient;
using BarterPoint.Application;
using BarterPoint.Domain;
using BarterPoint.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register MemoryCache and Controllers
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DbConnectionFactoryDelegate
builder.Services.AddSingleton<DbConnectionFactoryDelegate>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return () => new SqlConnection(connectionString);
});

// Register CacheSettings
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

// Register Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductDomainService>();
builder.Services.AddScoped<TransactionDomainService>();
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ProductCategoryDomainService>();
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<BidDomainService>();
builder.Services.AddScoped<BidStatusDomainService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<FavoriteDomainService>();
builder.Services.AddScoped<UserRatingDomainService>();
builder.Services.AddScoped<IUserRatingService, UserRatingService>();

// Register Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IBidStatusRepository, BidStatusRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IUserRatingRepository, UserRatingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Barter API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();