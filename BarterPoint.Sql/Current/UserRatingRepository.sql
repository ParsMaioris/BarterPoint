CREATE OR ALTER PROCEDURE AddUserRating
    @RaterId VARCHAR(255),
    @RateeId VARCHAR(255),
    @Rating INT,
    @Review TEXT,
    @DateRated DATE
AS
BEGIN
    INSERT INTO UserRatings (raterId, rateeId, rating, review, dateRated)
    VALUES (@RaterId, @RateeId, @Rating, @Review, @DateRated);
END;
GO

CREATE OR ALTER PROCEDURE UpdateUserRating
    @Id INT,
    @RaterId VARCHAR(255),
    @RateeId VARCHAR(255),
    @Rating INT,
    @Review TEXT,
    @DateRated DATE
AS
BEGIN
    UPDATE UserRatings
    SET raterId = @RaterId,
        rateeId = @RateeId,
        rating = @Rating,
        review = @Review,
        dateRated = @DateRated
    WHERE id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE DeleteUserRating
    @Id INT
AS
BEGIN
    DELETE FROM UserRatings
    WHERE id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE GetUserRatingById
    @Id INT
AS
BEGIN
    SELECT id, raterId, rateeId, rating, review, dateRated
    FROM UserRatings
    WHERE id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE GetUserRatings
    @UserId VARCHAR(255)
AS
BEGIN
    SELECT 
        AVG(CAST(ur.rating AS FLOAT)) AS AverageRating
    FROM 
        UserRatings ur
    WHERE 
        ur.rateeId = @UserId;
END;
GO

CREATE OR ALTER PROCEDURE GetAllUserRatings
AS
BEGIN
    SELECT id, raterId, rateeId, rating, review, dateRated
    FROM UserRatings;
END;
GO
