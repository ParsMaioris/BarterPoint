CREATE OR ALTER PROCEDURE AddFavorite
    @UserId VARCHAR(255),
    @ProductId VARCHAR(255),
    @DateAdded DATE
AS
BEGIN
    INSERT INTO Favorites (userId, productId, dateAdded)
    VALUES (@UserId, @ProductId, @DateAdded);
END;
GO;

CREATE OR ALTER PROCEDURE UpdateFavorite
    @Id INT,
    @UserId VARCHAR(255),
    @ProductId VARCHAR(255),
    @DateAdded DATE
AS
BEGIN
    UPDATE Favorites
    SET userId = @UserId,
        productId = @ProductId,
        dateAdded = @DateAdded
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE DeleteFavorite
    @Id INT
AS
BEGIN
    DELETE FROM Favorites
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE GetAllFavorites
AS
BEGIN
    SELECT id, userId, productId, dateAdded
    FROM Favorites;
END;
GO;

CREATE OR ALTER PROCEDURE GetFavoriteById
    @Id INT
AS
BEGIN
    SELECT id, userId, productId, dateAdded
    FROM Favorites
    WHERE id = @Id;
END;
GO;