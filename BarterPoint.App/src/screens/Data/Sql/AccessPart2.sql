-- Create a new user associated with the login in the specified database
CREATE USER [dev_user] FOR LOGIN [dev_user];

-- Grant read access
ALTER ROLE db_datareader ADD MEMBER [dev_user];

-- Grant write access
ALTER ROLE db_datawriter ADD MEMBER [dev_user];