CREATE TABLE Users (
    id VARCHAR(255) PRIMARY KEY,
    username VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    location VARCHAR(255),
    dateJoined DATE NOT NULL
);

CREATE TABLE ProductCategories (
    id INT IDENTITY(1,1) PRIMARY KEY,
    category VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE Products (
    id VARCHAR(255) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    image VARCHAR(255),
    description TEXT,
    tradeFor VARCHAR(255),
    categoryId INT NOT NULL,
    condition VARCHAR(20) NOT NULL,
    location VARCHAR(255),
    ownerId VARCHAR(255) NOT NULL,
    dimensions_width FLOAT,
    dimensions_height FLOAT,
    dimensions_depth FLOAT,
    dimensions_weight FLOAT,
    dateListed DATE NOT NULL,
    FOREIGN KEY (ownerId) REFERENCES Users(id),
    FOREIGN KEY (categoryId) REFERENCES ProductCategories(id),
    CHECK (condition IN ('New', 'Used', 'Refurbished'))
);

CREATE TABLE Bids (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product1Id VARCHAR(255) NOT NULL,
    product2Id VARCHAR(255) NOT NULL,
    FOREIGN KEY (product1Id) REFERENCES Products(id),
    FOREIGN KEY (product2Id) REFERENCES Products(id)
);

INSERT INTO ProductCategories (category) VALUES
('Electronics'),
('Furniture'),
('Clothing'),
('Books'),
('Toys'),
('Tools'),
('Appliances'),
('SportsEquipment'),
('MusicalInstruments'),
('Art'),
('Jewelry'),
('Collectibles'),
('Automotive'),
('Gardening'),
('OfficeSupplies'),
('PetSupplies'),
('HealthAndBeauty'),
('HomeDecor'),
('OutdoorGear'),
('Other');