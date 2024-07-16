INSERT INTO Users (id, username, password_hash, email, name, location, dateJoined) VALUES
('user1', 'johndoe', 'hash1', 'john.doe@example.com', 'John Doe', 'Los Angeles, CA', '2023-01-15'),
('user2', 'janedoe', 'hash2', 'jane.doe@example.com', 'Jane Doe', 'New York, NY', '2023-02-20'),
('user3', 'bobsmith', 'hash3', 'bob.smith@example.com', 'Bob Smith', 'Chicago, IL', '2023-03-10'),
('user4', 'alicejones', 'hash4', 'alice.jones@example.com', 'Alice Jones', 'San Francisco, CA', '2023-04-05'),
('user5', 'charliebrown', 'hash5', 'charlie.brown@example.com', 'Charlie Brown', 'Houston, TX', '2023-05-22');

INSERT INTO Products (id, name, image, description, tradeFor, categoryId, condition, location, ownerId, dimensions_width, dimensions_height, dimensions_depth, dimensions_weight, dateListed) VALUES
('product1', 'iPhone 12', 'image1.jpg', 'A gently used iPhone 12 in excellent condition.', 'Samsung Galaxy S21', 1, 'Used', 'Los Angeles, CA', 'user1', 2.82, 5.78, 0.29, 0.36, '2023-06-01'),
('product2', 'Wooden Coffee Table', 'image2.jpg', 'A stylish wooden coffee table with minor scratches.', 'Bookshelf', 2, 'Used', 'New York, NY', 'user2', 24.0, 18.0, 18.0, 25.0, '2023-06-10'),
('product3', 'Leather Jacket', 'image3.jpg', 'Black leather jacket, size M, barely worn.', 'Sneakers size 10', 3, 'New', 'Chicago, IL', 'user3', 0.0, 0.0, 0.0, 3.0, '2023-06-15'),
('product4', 'Electric Guitar', 'image4.jpg', 'Fender Stratocaster electric guitar, excellent sound quality.', 'Acoustic Guitar', 9, 'Refurbished', 'San Francisco, CA', 'user4', 15.5, 41.0, 3.5, 8.0, '2023-06-20'),
('product5', 'Mountain Bike', 'image5.jpg', '21-speed mountain bike with front suspension.', 'Road Bike', 8, 'Used', 'Houston, TX', 'user5', 25.0, 70.0, 7.0, 30.0, '2023-06-25');

INSERT INTO Bids (product1Id, product2Id) VALUES
('product1', 'product2'),
('product3', 'product4'),
('product2', 'product5'),
('product1', 'product3'),
('product4', 'product5');