
CREATE TABLE ComplainInfo (
    complainID INT PRIMARY KEY IDENTITY(1,1),
    complainType NVARCHAR(50) NOT NULL,
    complaineDetails  NVARCHAR(200),
    complainantEmail NVARCHAR(50) NOT NULL,
    incidentDate DATE NOT NULL,
    policeName VARCHAR(50),
    FOREIGN KEY (complainantEmail) REFERENCES UserInfo(email)
);

-- Insert two sample values into complainInfo
-- Insert two sample values into complainInfo
INSERT INTO ComplainInfo (complainType, complaineDetails, complainantEmail, incidentDate, policeName)
VALUES
('Theft', 'Stolen items from my car parked outside my house.', 'ambakhtiar@gmail.com', '2023-11-15', 'Jane Smith'),
('Assault', 'Physical assault in a public park.', 'jaberamin@example.com', '2023-11-10', NULL);
