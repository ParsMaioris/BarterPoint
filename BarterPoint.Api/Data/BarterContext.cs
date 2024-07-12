using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BarterContext : DbContext
{
    public BarterContext(DbContextOptions<BarterContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Bid> Bids { get; set; }
}

public class User
{
    [Key]
    [Column("id")]
    public string Id { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("location")]
    public string Location { get; set; }

    [Column("dateJoined")]
    public DateTime DateJoined { get; set; }
}

public class Product
{
    [Key]
    [Column("id")]
    public string Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("image")]
    public string Image { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("tradeFor")]
    public string TradeFor { get; set; }

    [Column("categoryId")]
    public int CategoryId { get; set; }

    [Column("condition")]
    public string Condition { get; set; }

    [Column("location")]
    public string Location { get; set; }

    [Column("ownerId")]
    public string OwnerId { get; set; }

    [Column("dimensions_width")]
    public float DimensionsWidth { get; set; }

    [Column("dimensions_height")]
    public float DimensionsHeight { get; set; }

    [Column("dimensions_depth")]
    public float DimensionsDepth { get; set; }

    [Column("dimensions_weight")]
    public float DimensionsWeight { get; set; }

    [Column("dateListed")]
    public DateTime DateListed { get; set; }
}

public class ProductCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("category")]
    public string Category { get; set; }
}

public class Bid
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("product1Id")]
    public string Product1Id { get; set; }

    [Column("product2Id")]
    public string Product2Id { get; set; }
}