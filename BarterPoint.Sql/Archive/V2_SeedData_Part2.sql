DELETE FROM BidStatus WHERE bidId IN (
    SELECT id FROM Bids WHERE product1Id IN ('product6', 'product7', 'product8', 'product9', 'product10')
    AND product2Id IN ('product6', 'product7', 'product8', 'product9', 'product10')
);

DELETE FROM Bids WHERE (product1Id = 'product6' AND product2Id = 'product7') OR
                      (product1Id = 'product7' AND product2Id = 'product8') OR
                      (product1Id = 'product8' AND product2Id = 'product9') OR
                      (product1Id = 'product9' AND product2Id = 'product10') OR
                      (product1Id = 'product10' AND product2Id = 'product6') OR
                      (product1Id = 'product6' AND product2Id = 'product8');

INSERT INTO Bids (product1Id, product2Id) VALUES
('product6', 'product7'),
('product7', 'product8'),
('product8', 'product9'),
('product9', 'product10'),
('product10', 'product6'),
('product6', 'product8');

INSERT INTO BidStatus (bidId, status, dateUpdated) VALUES
((SELECT id FROM Bids WHERE product1Id = 'product6' AND product2Id = 'product7'), 'Pending', '2023-07-15'),
((SELECT id FROM Bids WHERE product1Id = 'product7' AND product2Id = 'product8'), 'Pending', '2023-07-16');

INSERT INTO BidStatus (bidId, status, dateUpdated) VALUES
((SELECT id FROM Bids WHERE product1Id = 'product8' AND product2Id = 'product9'), 'Rejected', '2023-07-17'),
((SELECT id FROM Bids WHERE product1Id = 'product9' AND product2Id = 'product10'), 'Rejected', '2023-07-18');

INSERT INTO BidStatus (bidId, status, dateUpdated) VALUES
((SELECT id FROM Bids WHERE product1Id = 'product10' AND product2Id = 'product6'), 'Pending', '2023-07-19'),
((SELECT id FROM Bids WHERE product1Id = 'product6' AND product2Id = 'product8'), 'Rejected', '2023-07-20');