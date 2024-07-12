INSERT INTO Users (id, username, password_hash, email, name, location, dateJoined) VALUES
('user11', 'georgeharris', 'hash11', 'george.harris@example.com', 'George Harris', 'Boston, MA', '2023-07-01'),
('user12', 'victoriaclark', 'hash12', 'victoria.clark@example.com', 'Victoria Clark', 'Austin, TX', '2023-07-02'),
('user13', 'jameslee', 'hash13', 'james.lee@example.com', 'James Lee', 'San Diego, CA', '2023-07-03'),
('user14', 'annaperez', 'hash14', 'anna.perez@example.com', 'Anna Perez', 'Phoenix, AZ', '2023-07-04'),
('user15', 'paulking', 'hash15', 'paul.king@example.com', 'Paul King', 'Portland, OR', '2023-07-05'),
('user16', 'nancydavis', 'hash16', 'nancy.davis@example.com', 'Nancy Davis', 'Philadelphia, PA', '2023-07-06'),
('user17', 'briancarter', 'hash17', 'brian.carter@example.com', 'Brian Carter', 'San Antonio, TX', '2023-07-07'),
('user18', 'lisamiller', 'hash18', 'lisa.miller@example.com', 'Lisa Miller', 'Columbus, OH', '2023-07-08'),
('user19', 'stevenscott', 'hash19', 'steven.scott@example.com', 'Steven Scott', 'Detroit, MI', '2023-07-09'),
('user20', 'karenhall', 'hash20', 'karen.hall@example.com', 'Karen Hall', 'Charlotte, NC', '2023-07-10');

INSERT INTO Products (id, name, image, description, tradeFor, categoryId, condition, location, ownerId, dimensions_width, dimensions_height, dimensions_depth, dimensions_weight, dateListed) VALUES
('product11', 'Smart TV', 'image11.jpg', '50-inch Smart TV with 4K resolution.', 'Home Theater System', 1, 'Used', 'Boston, MA', 'user11', 44.0, 25.5, 2.5, 30.0, '2023-07-06'),
('product12', 'Dining Table Set', 'image12.jpg', 'Dining table with 4 chairs, wooden.', 'Outdoor Furniture', 2, 'Used', 'Austin, TX', 'user12', 60.0, 30.0, 30.0, 80.0, '2023-07-07'),
('product13', 'Winter Coat', 'image13.jpg', 'Warm winter coat, size L, like new.', 'Rain Jacket', 3, 'New', 'San Diego, CA', 'user13', 0.0, 0.0, 0.0, 2.5, '2023-07-08'),
('product14', 'Digital Camera', 'image14.jpg', 'DSLR camera with 18-55mm lens.', 'GoPro', 1, 'Used', 'Phoenix, AZ', 'user14', 5.0, 4.0, 3.0, 1.0, '2023-07-09'),
('product15', 'Kayak', 'image15.jpg', 'One-person kayak with paddle.', 'Canoe', 8, 'Used', 'Portland, OR', 'user15', 120.0, 30.0, 15.0, 50.0, '2023-07-10'),
('product16', 'Necklace', 'image16.jpg', 'Gold necklace with pendant.', 'Bracelet', 11, 'New', 'Philadelphia, PA', 'user16', 0.0, 0.0, 0.0, 0.1, '2023-07-11'),
('product17', 'Washing Machine', 'image17.jpg', 'Front-loading washing machine, 1 year old.', 'Dryer', 7, 'Used', 'San Antonio, TX', 'user17', 27.0, 38.0, 31.0, 150.0, '2023-07-12'),
('product18', 'Office Chair', 'image18.jpg', 'Ergonomic office chair, black.', 'Standing Desk', 15, 'Used', 'Columbus, OH', 'user18', 25.0, 40.0, 25.0, 35.0, '2023-07-13'),
('product19', 'Lawn Mower', 'image19.jpg', 'Electric lawn mower, works perfectly.', 'Leaf Blower', 14, 'Used', 'Detroit, MI', 'user19', 20.0, 40.0, 25.0, 50.0, '2023-07-14'),
('product20', 'Cookware Set', 'image20.jpg', '10-piece non-stick cookware set.', 'Knife Set', 6, 'New', 'Charlotte, NC', 'user20', 18.0, 12.0, 10.0, 10.0, '2023-07-15');

INSERT INTO Bids (product1Id, product2Id) VALUES
('product11', 'product12'),
('product13', 'product14'),
('product15', 'product16'),
('product17', 'product18'),
('product19', 'product20'),
('product11', 'product15');