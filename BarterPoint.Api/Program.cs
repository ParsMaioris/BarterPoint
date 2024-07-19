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
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<IProductServiceV2, ProductServiceV2>();
builder.Services.AddScoped<ITransactionServiceV2, TransactionServiceV2>();
builder.Services.AddScoped<IRatingsService, RatingsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductDomainService>();
builder.Services.AddScoped<TransactionDomainService>();
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ProductCategoryDomainService>();

// Register Repositories
builder.Services.AddScoped<ITransactionRepositoryV2, TransactionRepositoryV2>();
builder.Services.AddScoped<IProductRepositoryV2, ProductRepositoryV2>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IUserRepositoryV2, UserRepositoryV2>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddScoped<IRatingsRepository, RatingsRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

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