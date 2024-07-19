INSERT INTO Products (id, name, image, description, tradeFor, categoryId, condition, location, ownerId, dimensions_width, dimensions_height, dimensions_depth, dimensions_weight, dateListed) VALUES
(NEWID(), 'Smartphone', 'https://images.pexels.com/photos/3769739/pexels-photo-3769739.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'A brand new smartphone with latest features.', 'Laptop', 1, 'New', 'Los Angeles, CA', 'user1', 5, 10, 0.5, 0.2, '2024-07-16'),
(NEWID(), 'Sofa', 'https://images.pexels.com/photos/1866149/pexels-photo-1866149.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Comfortable 3-seater sofa.', 'Coffee Table', 2, 'Used', 'New York, NY', 'user2', 200, 90, 100, 50, '2024-07-16'),
(NEWID(), 'Electric Drill', 'https://images.pexels.com/photos/1462783/pexels-photo-1462783.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Powerful electric drill, barely used.', 'Tool Set', 6, 'Used', 'Chicago, IL', 'user3', 0.1, 0.3, 0.2, 2, '2024-07-16'),
(NEWID(), 'Mountain Bike', 'https://images.pexels.com/photos/1015568/pexels-photo-1015568.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Mountain bike in excellent condition.', 'Road Bike', 8, 'New', 'Los Angeles, CA', 'user4', 150, 100, 30, 15, '2024-07-16'),
(NEWID(), 'Acoustic Guitar', 'https://images.pexels.com/photos/165971/pexels-photo-165971.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Beautiful acoustic guitar with great sound.', 'Electric Guitar', 9, 'Used', 'San Francisco, CA', 'user5', 0.3, 1, 0.2, 3, '2024-07-16'),
(NEWID(), 'Vintage Painting', 'https://images.pexels.com/photos/1573066/pexels-photo-1573066.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'A stunning vintage painting.', 'Sculpture', 10, 'Refurbished', 'Miami, FL', 'user6', 0.5, 0.7, 0.05, 1, '2024-07-16'),
(NEWID(), 'Necklace', 'https://images.pexels.com/photos/1454185/pexels-photo-1454185.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Elegant gold necklace.', 'Bracelet', 11, 'New', 'Seattle, WA', 'user7', 0.01, 0.01, 0.01, 0.1, '2024-07-16'),
(NEWID(), 'Smartwatch', 'https://images.pexels.com/photos/4370447/pexels-photo-4370447.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Latest model smartwatch with various features.', 'Fitness Tracker', 1, 'New', 'Austin, TX', 'user8', 0.1, 0.2, 0.05, 0.2, '2024-07-16'),
(NEWID(), 'Laptop', 'https://images.pexels.com/photos/18105/pexels-photo.jpg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'High-performance laptop with 16GB RAM.', 'Tablet', 1, 'New', 'Boston, MA', 'user9', 13, 9, 0.7, 3, '2024-07-16'),
(NEWID(), 'Tablet', 'https://images.pexels.com/photos/193004/pexels-photo-193004.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', '10-inch tablet with high-resolution display.', 'Smartphone', 1, 'Used', 'Denver, CO', 'user10', 10, 7, 0.3, 0.5, '2024-07-16'),
(NEWID(), 'Desk Chair', 'https://images.pexels.com/photos/276517/pexels-photo-276517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Ergonomic desk chair, black.', 'Standing Desk', 15, 'Used', 'Los Angeles, CA', 'user11', 25, 40, 25, 35, '2024-07-16'),
(NEWID(), 'Blender', 'https://images.pexels.com/photos/991365/pexels-photo-991365.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'High-speed blender for smoothies.', 'Juicer', 6, 'New', 'Phoenix, AZ', 'user12', 8, 15, 8, 4, '2024-07-16'),
(NEWID(), 'Camera', 'https://images.pexels.com/photos/50614/pexels-photo-50614.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Digital camera with 20MP resolution.', 'Camcorder', 1, 'New', 'New York, NY', 'user13', 5, 3, 2, 1, '2024-07-16'),
(NEWID(), 'Tent', 'https://images.pexels.com/photos/615602/pexels-photo-615602.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', '4-person camping tent.', 'Sleeping Bag', 8, 'Used', 'Orlando, FL', 'user14', 20, 15, 5, 8, '2024-07-16'),
(NEWID(), 'Cookware Set', 'https://images.pexels.com/photos/159887/pexels-photo-159887.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2', 'Non-stick cookware set, 10 pieces.', 'Knife Set', 6, 'New', 'Atlanta, GA', 'user15', 18, 12, 10, 10, '2024-07-16');

INSERT INTO Bids (product1Id, product2Id) VALUES
('36ABA24C-3DFB-4EE9-BBFB-C41778161A30', '6737F761-31AC-4F59-AFC6-DC8955EBE57C'),
('67BDDCB4-DEEA-4A72-A476-92463CAC2EBD', '8FB7F0BE-2EF1-4FEE-9BE4-D57CB12B7854'),
('9B955B1E-244C-4B68-B1BE-B08F1C35F03F', 'B3C73F73-AC81-4B02-82E3-82A73326EA00'),
('B590665D-F550-4361-9405-41C5008FB1AF', 'EF38479C-5DB8-40A6-8783-6BF83DFA85D6'),
('F91F2884-B21A-48D2-9D70-EF72CC65E346', 'product1'),
('product10', 'product11'),
('product12', 'product13'),
('product14', 'product15'),
('product16', 'product17'),
('product18', 'product19');

INSERT INTO BidStatus (bidId, status, dateUpdated)
SELECT id, 'Pending', GETDATE()
FROM Bids
WHERE id NOT IN (SELECT bidId FROM BidStatus);