CREATE TABLE dbo.Users (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    user_name NVARCHAR(100) NOT NULL,
    password NVARCHAR(256) NOT NULL,
    full_name NVARCHAR(200) NOT NULL,
    refresh_token NVARCHAR(500) NULL,
    refresh_token_expiry_time DATETIME2 NULL,
    
    CONSTRAINT UQ_users_user_name UNIQUE (user_name)
);