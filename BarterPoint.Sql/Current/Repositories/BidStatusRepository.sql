CREATE OR ALTER PROCEDURE AddBidStatus
    @BidId INT,
    @Status VARCHAR(50),
    @DateUpdated DATE
AS
BEGIN
    INSERT INTO BidStatus (bidId, status, dateUpdated)
    VALUES (@BidId, @Status, @DateUpdated);

    SELECT SCOPE_IDENTITY() AS Id;
END;
GO;

CREATE OR ALTER PROCEDURE GetBidStatusById
    @Id INT
AS
BEGIN
    SELECT id, bidId, status, dateUpdated
    FROM BidStatus
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE GetAllBidStatuses
AS
BEGIN
    SELECT id, bidId, status, dateUpdated
    FROM BidStatus;
END;
GO;

CREATE OR ALTER PROCEDURE UpdateBidStatus
    @Id INT,
    @Status VARCHAR(50),
    @DateUpdated DATE
AS
BEGIN
    UPDATE BidStatus
    SET status = @Status, dateUpdated = @DateUpdated
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE DeleteBidStatus
    @Id INT
AS
BEGIN
    DELETE FROM BidStatus
    WHERE id = @Id;
END;
GO;