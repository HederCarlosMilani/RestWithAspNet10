CREATE TABLE dbo.person (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(80) NOT NULL,
    last_name VARCHAR(80) NOT NULL,
    address VARCHAR(100) NOT NULL,
    gender VARCHAR(6) NOT NULL
);
