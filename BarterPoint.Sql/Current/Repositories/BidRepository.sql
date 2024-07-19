CREATE OR ALTER PROCEDURE AddBid
    @product1Id VARCHAR(255),
    @product2Id VARCHAR(255)
AS
BEGIN
    INSERT INTO Bids (product1Id, product2Id)
    VALUES (@product1Id, @product2Id);

    SELECT SCOPE_IDENTITY() AS Id;
END;
GO;

CREATE OR ALTER PROCEDURE GetBidById
    @Id INT
AS
BEGIN
    SELECT id, product1Id, product2Id
    FROM Bids
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE GetAllBids
AS
BEGIN
    SELECT id, product1Id, product2Id
    FROM Bids;
END;
GO;

CREATE OR ALTER PROCEDURE UpdateBid
    @Id INT,
    @product1Id VARCHAR(255),
    @product2Id VARCHAR(255)
AS
BEGIN
    UPDATE Bids
    SET product1Id = @product1Id, product2Id = @product2Id
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE DeleteBid
    @Id INT
AS
BEGIN
    DELETE FROM Bids
    WHERE id = @Id;
END;
GO;