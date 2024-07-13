DELETE FROM BidStatus WHERE bidId IN (
    SELECT id FROM Bids WHERE product1Id = 'product1' AND product2Id = 'product2'
);
DELETE FROM BidStatus WHERE bidId IN (
    SELECT id FROM Bids WHERE product1Id = 'product2' AND product2Id = 'product3'
);
DELETE FROM BidStatus WHERE bidId IN (
    SELECT id FROM Bids WHERE product1Id = 'product3' AND product2Id = 'product4'
);
DELETE FROM BidStatus WHERE bidId IN (
    SELECT id FROM Bids WHERE product1Id = 'product4' AND product2Id = 'product5'
);
DELETE FROM BidStatus WHERE bidId IN (
    SELECT id FROM Bids WHERE product1Id = 'product5' AND product2Id = 'product1'
);

DELETE FROM Bids WHERE product1Id = 'product1' AND product2Id = 'product2';
DELETE FROM Bids WHERE product1Id = 'product2' AND product2Id = 'product3';
DELETE FROM Bids WHERE product1Id = 'product3' AND product2Id = 'product4';
DELETE FROM Bids WHERE product1Id = 'product4' AND product2Id = 'product5';
DELETE FROM Bids WHERE product1Id = 'product5' AND product2Id = 'product1';

INSERT INTO Bids (product1Id, product2Id) VALUES
('product1', 'product2'),
('product2', 'product3'),
('product3', 'product4'),
('product4', 'product5'),
('product5', 'product1');

INSERT INTO BidStatus (bidId, status, dateUpdated) VALUES
((SELECT id FROM Bids WHERE product1Id = 'product1' AND product2Id = 'product2'), 'Approved', '2024-07-05'),
((SELECT id FROM Bids WHERE product1Id = 'product2' AND product2Id = 'product3'), 'Approved', '2024-07-06'),
((SELECT id FROM Bids WHERE product1Id = 'product3' AND product2Id = 'product4'), 'Approved', '2024-07-07'),
((SELECT id FROM Bids WHERE product1Id = 'product4' AND product2Id = 'product5'), 'Approved', '2024-07-08'),
((SELECT id FROM Bids WHERE product1Id = 'product5' AND product2Id = 'product1'), 'Approved', '2024-07-09');

DELETE FROM TransactionHistory WHERE productId IN ('product1', 'product2', 'product3', 'product4', 'product5');

INSERT INTO TransactionHistory (productId, buyerId, sellerId, dateCompleted) VALUES
('product1', 'user2', 'user1', '2024-07-10'),
('product2', 'user3', 'user2', '2024-07-11'),
('product3', 'user1', 'user3', '2024-07-12'),
('product4', 'user5', 'user4', '2024-07-13'),
('product5', 'user4', 'user5', '2024-07-14');

INSERT INTO Favorites (userId, productId, dateAdded) VALUES
('user1', 'product6', '2024-07-06'),
('user2', 'product7', '2024-07-07'),
('user3', 'product8', '2024-07-08'),
('user5', 'product10', '2024-07-10');

INSERT INTO UserRatings (raterId, rateeId, rating, review, dateRated) VALUES
('user2', 'user1', 5, 'Great communication and quick transaction.', '2024-07-15'),
('user3', 'user2', 4, 'Item as described, smooth transaction.', '2024-07-16'),
('user1', 'user3', 5, 'Very friendly and honest.', '2024-07-17'),
('user5', 'user4', 3, 'Took a bit longer to ship, but item was good.', '2024-07-18'),
('user4', 'user5', 4, 'Good deal and nice product.', '2024-07-19');