CREATE TABLE AdminInfo (
    adminid INT PRIMARY KEY IDENTITY,
    name NVARCHAR(50) NOT NULL,
    username NVARCHAR(50) NOT NULL UNIQUE,
    email NVARCHAR(50) NOT NULL UNIQUE,
    phone NVARCHAR(15) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    gender NVARCHAR(10) NOT NULL,
);
INSERT INTO AdminInfo (name, username, email, phone, password, gender)
VALUES
    ('Sarawar Hossain', 'admin01', 'sarawar@gmail.com', '01623906573', 'admin01', 'Male'),
    ('Turaisha Tasnim', 'admin02',  'turaisha@gmail.com','01823562843', 'admin02', 'Female');