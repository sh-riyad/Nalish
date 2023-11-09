CREATE TABLE UserInfo (
    userid INT PRIMARY KEY IDENTITY,
    name NVARCHAR(50) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    email NVARCHAR(50) NOT NULL Unique,
    phone NVARCHAR(15) NOT NULL Unique,
    password NVARCHAR(255) NOT NULL,
    gender NVARCHAR(10) NOT NULL,
);
INSERT INTO UserInfo (name, username, email, phone, password, gender)
VALUES
    ('AM Bakhtia', 'ambakhtiar', 'ambakhtiar@gmail.com', '01614418883',  'password123', 'Male'),
    ('Jaber Amin', 'jaberamin', 'jaberamin@example.com', '01623471284',  'password456', 'Male');