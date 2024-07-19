CREATE OR ALTER PROCEDURE GetAllUsers
AS
BEGIN
    SELECT id, username, password_hash, email, name, location, dateJoined
    FROM Users;
END;
GO;

CREATE OR ALTER PROCEDURE GetUserById
    @Id VARCHAR(255)
AS
BEGIN
    SELECT id, username, password_hash, email, name, location, dateJoined
    FROM Users
    WHERE id = @Id;
END;
GO;

CREATE OR ALTER PROCEDURE AddUser
    @Id VARCHAR(255),
    @Username VARCHAR(255),
    @PasswordHash VARCHAR(255),
    @Email VARCHAR(255),
    @Name VARCHAR(255),
    @Location VARCHAR(255),
    @DateJoined DATE
AS
BEGIN
    INSERT INTO Users (id, username, password_hash, email, name, location, dateJoined)
    VALUES (@Id, @Username, @PasswordHash, @Email, @Name, @Location, @DateJoined);
END;
GO;

CREATE OR ALTER PROCEDURE DeleteUser
    @Id VARCHAR(255)
AS
BEGIN
    DELETE FROM Users
    WHERE id = @Id;
END;
GO;