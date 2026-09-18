IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL,
        Password NVARCHAR(100) NOT NULL,
        Role NVARCHAR(20) NOT NULL
    );
END

INSERT INTO dbo.Users (Username, Password, Role)
VALUES ('admin', '1234', 'Admin');