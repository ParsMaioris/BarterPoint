CREATE OR ALTER PROCEDURE GetProductsByOwner
    @OwnerId VARCHAR(255)
AS
BEGIN
    SELECT
        p.id,
        p.name,
        p.image,
        p.description,
        p.tradeFor,
        pc.category AS category,
        p.condition,
        p.location,
        p.ownerId,
        p.dimensions_width,
        p.dimensions_height,
        p.dimensions_depth,
        p.dimensions_weight,
        p.dateListed
    FROM Products p
    INNER JOIN ProductCategories pc ON p.categoryId = pc.id
    WHERE p.ownerId = @OwnerId;
END;
GO;

CREATE OR ALTER PROCEDURE GetAllProductCategories
AS
BEGIN
    SELECT id, category
    FROM ProductCategories
    ORDER BY category;
END;
GO;

CREATE OR ALTER PROCEDURE GetProductsNotOwnedByUser
    @OwnerId VARCHAR(255)
AS
BEGIN
    SELECT 
        p.id,
        p.name,
        p.image,
        p.description,
        p.tradeFor,
        p.categoryId,
        c.category,
        p.condition,
        p.location,
        p.ownerId,
        p.dimensions_width,
        p.dimensions_height,
        p.dimensions_depth,
        p.dimensions_weight,
        p.dateListed
    FROM 
        Products p
    INNER JOIN 
        ProductCategories c ON p.categoryId = c.id
    WHERE 
        p.ownerId <> @OwnerId;
END;
GO;

CREATE OR ALTER PROCEDURE AddBid
    @product1Id VARCHAR(255),
    @product2Id VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Bids (product1Id, product2Id)
    VALUES (@product1Id, @product2Id);

    SELECT SCOPE_IDENTITY() AS NewBidId;
END;
GO;

CREATE OR ALTER PROCEDURE RemoveBid
    @bidId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Bids
    WHERE id = @bidId;
END;
GO;

CREATE OR ALTER PROCEDURE GetAllBids
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        B.id AS bidId,
        B.product1Id,
        P1.name AS product1Name,
        B.product2Id,
        P2.name AS product2Name
    FROM
        Bids B
    JOIN
        Products P1 ON B.product1Id = P1.id
    JOIN
        Products P2 ON B.product2Id = P2.id;
END;
GO;

CREATE OR ALTER PROCEDURE AddProduct
    @name VARCHAR(255),
    @image VARCHAR(255),
    @description TEXT,
    @tradeFor VARCHAR(255),
    @categoryId INT,
    @condition VARCHAR(20),
    @location VARCHAR(255),
    @ownerId VARCHAR(255),
    @dimensions_width FLOAT,
    @dimensions_height FLOAT,
    @dimensions_depth FLOAT,
    @dimensions_weight FLOAT,
    @dateListed DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @newId VARCHAR(255);
    SET @newId = NEWID();

    INSERT INTO Products (
        id,
        name,
        image,
        description,
        tradeFor,
        categoryId,
        condition,
        location,
        ownerId,
        dimensions_width,
        dimensions_height,
        dimensions_depth,
        dimensions_weight,
        dateListed
    ) VALUES (
        @newId,
        @name,
        @image,
        @description,
        @tradeFor,
        @categoryId,
        @condition,
        @location,
        @ownerId,
        @dimensions_width,
        @dimensions_height,
        @dimensions_depth,
        @dimensions_weight,
        @dateListed
    );

    SELECT @newId AS NewProductId;
END;
GO;

CREATE OR ALTER PROCEDURE RemoveProduct
    @id VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Products
    WHERE id = @id;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No product found with the given ID.', 16, 1);
    END
END;
GO;

CREATE OR ALTER PROCEDURE RegisterUser
    @id UNIQUEIDENTIFIER,
    @username VARCHAR(255),
    @password_hash VARCHAR(255),
    @email VARCHAR(255),
    @name VARCHAR(255),
    @location VARCHAR(255),
    @dateJoined DATE
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Users WHERE username = @username)
        BEGIN
            SELECT 'Error: Username already exists' AS ErrorMessage;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Users WHERE email = @email)
        BEGIN
            SELECT 'Error: Email already exists' AS ErrorMessage;
            RETURN;
        END

        INSERT INTO Users (id, username, password_hash, email, name, location, dateJoined)
        VALUES (@id, @username, @password_hash, @email, @name, @location, @dateJoined);
        
        SELECT 'User registered successfully' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO;

CREATE OR ALTER PROCEDURE SignInUser
    @username VARCHAR(255),
    @password_hash VARCHAR(255)
AS
BEGIN
    DECLARE @userId VARCHAR(255);
    SELECT @userId = id
    FROM Users
    WHERE username = @username AND password_hash = @password_hash;

    IF @userId IS NOT NULL
    BEGIN
        SELECT 'Sign-in successful' AS Message;
        SELECT @userId AS UserId;
    END
    ELSE
    BEGIN
        SELECT 'Invalid username or password' AS ErrorMessage;
    END
END;
GO;

CREATE OR ALTER PROCEDURE GetAllBidStatuses
AS
BEGIN
    SELECT 
        id,
        bidId,
        status,
        dateUpdated
    FROM 
        BidStatus;
END;
GO;

CREATE OR ALTER PROCEDURE AddBidStatus
    @BidId INT,
    @Status VARCHAR(50),
    @DateUpdated DATE
AS
BEGIN
    INSERT INTO BidStatus (bidId, status, dateUpdated)
    VALUES (@BidId, @Status, @DateUpdated);
END;
GO

CREATE OR ALTER PROCEDURE UpdateBidStatus
    @BidId INT,
    @Status VARCHAR(50),
    @DateUpdated DATE
AS
BEGIN
    UPDATE BidStatus
    SET status = @Status, dateUpdated = @DateUpdated
    WHERE bidId = @BidId;
END;
GO;

CREATE OR ALTER PROCEDURE AddUserFavorite
    @userId VARCHAR(255),
    @productId VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Favorites (userId, productId, dateAdded)
    VALUES (@userId, @productId, GETDATE());
END;
GO;

CREATE OR ALTER PROCEDURE GetUserFavorites
    @userId VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT f.id, f.userId, f.productId, f.dateAdded, p.name
    FROM Favorites f
    JOIN Products p ON f.productId = p.id
    WHERE f.userId = @userId
    ORDER BY f.dateAdded DESC;
END;
GO;

CREATE OR ALTER PROCEDURE RemoveUserFavorite
    @userId VARCHAR(255),
    @productId VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Favorites
    WHERE userId = @userId AND productId = @productId;
END;
GO;

CREATE OR ALTER PROCEDURE IsFavorite
    @userId VARCHAR(255),
    @productId VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Favorites WHERE userId = @userId AND productId = @productId)
    BEGIN
        SELECT 1 AS IsFavorite;
    END
    ELSE
    BEGIN
        SELECT 0 AS IsFavorite;
    END;
END;
GO;

CREATE OR ALTER PROCEDURE ApproveBidStatus
    @BidId INT
AS
BEGIN
    UPDATE BidStatus
    SET status = 'Approved', dateUpdated = GETDATE()
    WHERE bidId = @BidId;
END;
GO;

CREATE OR ALTER PROCEDURE RejectOtherBids
    @Product1Id VARCHAR(255),
    @Product2Id VARCHAR(255),
    @BidId INT
AS
BEGIN
    UPDATE BidStatus
    SET status = 'Rejected', dateUpdated = GETDATE()
    WHERE bidId IN (
        SELECT id
        FROM Bids
        WHERE (product1Id = @Product1Id OR product2Id = @Product1Id OR
               product1Id = @Product2Id OR product2Id = @Product2Id)
          AND id <> @BidId
    );
END;
GO;

CREATE OR ALTER PROCEDURE AddTransactionHistory
    @Product1Id VARCHAR(255),
    @Product2Id VARCHAR(255)
AS
BEGIN
    DECLARE @SellerId1 VARCHAR(255);
    DECLARE @SellerId2 VARCHAR(255);
    DECLARE @BuyerId1 VARCHAR(255);
    DECLARE @BuyerId2 VARCHAR(255);

    SELECT @SellerId1 = ownerId FROM Products WHERE id = @Product1Id;
    SELECT @SellerId2 = ownerId FROM Products WHERE id = @Product2Id;

    SET @BuyerId1 = @SellerId2;
    SET @BuyerId2 = @SellerId1;

    INSERT INTO TransactionHistory (productId, buyerId, sellerId, dateCompleted)
    VALUES (@Product1Id, @BuyerId1, @SellerId1, GETDATE()),
           (@Product2Id, @BuyerId2, @SellerId2, GETDATE());
END;
GO;

CREATE OR ALTER PROCEDURE GetProductsByBidId
    @BidId INT
AS
BEGIN
    SELECT product1Id, product2Id
    FROM Bids
    WHERE id = @BidId;
END;
GO;

CREATE OR ALTER PROCEDURE GetAllTransactionHistory
AS
BEGIN
    SELECT 
        [id],
        [productId],
        [buyerId],
        [sellerId],
        [dateCompleted]
    FROM 
        [dbo].[TransactionHistory];
END;
GO;