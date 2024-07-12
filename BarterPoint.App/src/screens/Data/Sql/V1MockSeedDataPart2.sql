-- More seed data for Users table
INSERT INTO Users (id, username, password_hash, email, name, location, dateJoined) VALUES
('user6', 'emilywhite', 'hash6', 'emily.white@example.com', 'Emily White', 'Miami, FL', '2023-06-01'),
('user7', 'michaeljohnson', 'hash7', 'michael.johnson@example.com', 'Michael Johnson', 'Dallas, TX', '2023-06-15'),
('user8', 'sarahwilliams', 'hash8', 'sarah.williams@example.com', 'Sarah Williams', 'Atlanta, GA', '2023-06-20'),
('user9', 'davidbrown', 'hash9', 'david.brown@example.com', 'David Brown', 'Seattle, WA', '2023-07-01'),
('user10', 'lindamartin', 'hash10', 'linda.martin@example.com', 'Linda Martin', 'Denver, CO', '2023-07-05');

-- More seed data for Products table
INSERT INTO Products (id, name, image, description, tradeFor, categoryId, condition, location, ownerId, dimensions_width, dimensions_height, dimensions_depth, dimensions_weight, dateListed) VALUES
('product6', 'Gaming Laptop', 'image6.jpg', 'High-performance gaming laptop, 16GB RAM, 1TB SSD.', 'Desktop PC', 1, 'Used', 'Miami, FL', 'user6', 15.0, 10.0, 1.0, 4.5, '2023-07-01'),
('product7', 'Vintage Vinyl Records', 'image7.jpg', 'Collection of 50 classic rock vinyl records.', 'Guitar', 10, 'Used', 'Dallas, TX', 'user7', 12.0, 12.0, 0.5, 20.0, '2023-07-02'),
('product8', 'Designer Handbag', 'image8.jpg', 'Authentic designer handbag, hardly used.', 'Smartwatch', 3, 'New', 'Atlanta, GA', 'user8', 12.0, 10.0, 6.0, 1.5, '2023-07-03'),
('product9', 'Electric Drill', 'image9.jpg', 'Cordless electric drill with charger and extra battery.', 'Saw', 6, 'Used', 'Seattle, WA', 'user9', 10.0, 8.0, 4.0, 3.0, '2023-07-04'),
('product10', 'Treadmill', 'image10.jpg', 'Folding treadmill with various speed settings.', 'Exercise Bike', 8, 'Used', 'Denver, CO', 'user10', 60.0, 30.0, 50.0, 100.0, '2023-07-05');