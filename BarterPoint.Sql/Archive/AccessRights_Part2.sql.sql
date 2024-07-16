CREATE USER [dev_user] FOR LOGIN [dev_user];

ALTER ROLE db_datareader ADD MEMBER [dev_user];

ALTER ROLE db_datawriter ADD MEMBER [dev_user];

GRANT EXECUTE TO dev_user;