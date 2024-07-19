CREATE OR ALTER PROCEDURE GetAllTransactions
AS
BEGIN
    SELECT id, productId, buyerId, sellerId, dateCompleted
    FROM TransactionHistory;
END;
GO;

CREATE OR ALTER PROCEDURE GetTransactionById
    @Id INT
AS
BEGIN
    SELECT id, productId, buyerId, sellerId, dateCompleted
    FROM TransactionHistory
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE AddTransaction
    @ProductId VARCHAR(255),
    @BuyerId VARCHAR(255),
    @SellerId VARCHAR(255),
    @DateCompleted DATE
AS
BEGIN
    INSERT INTO TransactionHistory (productId, buyerId, sellerId, dateCompleted)
    VALUES (@ProductId, @BuyerId, @SellerId, @DateCompleted);
    
    SELECT SCOPE_IDENTITY() AS NewId;
END;
GO;

CREATE OR ALTER PROCEDURE UpdateTransaction
    @Id INT,
    @ProductId VARCHAR(255),
    @BuyerId VARCHAR(255),
    @SellerId VARCHAR(255),
    @DateCompleted DATE
AS
BEGIN
    UPDATE TransactionHistory
    SET productId = @ProductId,
        buyerId = @BuyerId,
        sellerId = @SellerId,
        dateCompleted = @DateCompleted
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE DeleteTransaction
    @Id INT
AS
BEGIN
    DELETE FROM TransactionHistory
    WHERE id = @Id;
END;
GO;