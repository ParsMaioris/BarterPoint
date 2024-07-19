CREATE OR ALTER PROCEDURE GetAllProducts
AS
BEGIN
    SELECT 
        id, name, image, description, tradeFor, categoryId, condition, location, ownerId, 
        dimensions_width, dimensions_height, dimensions_depth, dimensions_weight, dateListed
    FROM Products;
END
GO

CREATE OR ALTER PROCEDURE GetProductById
    @ProductId VARCHAR(255)
AS
BEGIN
    SELECT 
        id, name, image, description, tradeFor, categoryId, condition, location, ownerId, 
        dimensions_width, dimensions_height, dimensions_depth, dimensions_weight, dateListed
    FROM Products
    WHERE id = @ProductId;
END
GO

CREATE OR ALTER PROCEDURE AddProduct
    @Id VARCHAR(255),
    @Name VARCHAR(255),
    @Image VARCHAR(255),
    @Description TEXT,
    @TradeFor VARCHAR(255),
    @CategoryId INT,
    @Condition VARCHAR(20),
    @Location VARCHAR(255),
    @OwnerId VARCHAR(255),
    @DimensionsWidth FLOAT,
    @DimensionsHeight FLOAT,
    @DimensionsDepth FLOAT,
    @DimensionsWeight FLOAT,
    @DateListed DATE
AS
BEGIN
    INSERT INTO Products (id, name, image, description, tradeFor, categoryId, condition, location, ownerId, dimensions_width, dimensions_height, dimensions_depth, dimensions_weight, dateListed)
    VALUES (@Id, @Name, @Image, @Description, @TradeFor, @CategoryId, @Condition, @Location, @OwnerId, @DimensionsWidth, @DimensionsHeight, @DimensionsDepth, @DimensionsWeight, @DateListed);
END
GO

CREATE OR ALTER PROCEDURE UpdateProduct
    @Id VARCHAR(255),
    @Name VARCHAR(255),
    @Image VARCHAR(255),
    @Description TEXT,
    @TradeFor VARCHAR(255),
    @CategoryId INT,
    @Condition VARCHAR(20),
    @Location VARCHAR(255),
    @DimensionsWidth FLOAT,
    @DimensionsHeight FLOAT,
    @DimensionsDepth FLOAT,
    @DimensionsWeight FLOAT
AS
BEGIN
    UPDATE Products
    SET 
        name = @Name,
        image = @Image,
        description = @Description,
        tradeFor = @TradeFor,
        categoryId = @CategoryId,
        condition = @Condition,
        location = @Location,
        dimensions_width = @DimensionsWidth,
        dimensions_height = @DimensionsHeight,
        dimensions_depth = @DimensionsDepth,
        dimensions_weight = @DimensionsWeight
    WHERE id = @Id;
END
GO

CREATE PROCEDURE DeleteProduct
    @Id VARCHAR(255)
AS
BEGIN
    DELETE FROM Products
    WHERE id = @Id;
END
GO