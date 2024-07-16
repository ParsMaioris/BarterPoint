DELETE FROM TransactionHistory;

DELETE FROM Bids;

DELETE FROM BidStatus;

CREATE TABLE #UniqueProducts (
    id VARCHAR(255),
    name VARCHAR(255),
    row_num INT
);

INSERT INTO #UniqueProducts (id, name, row_num)
SELECT 
    id, 
    name,
    ROW_NUMBER() OVER (PARTITION BY name ORDER BY dateListed) AS row_num
FROM 
    Products;

DELETE FROM Products
WHERE id IN (
    SELECT id
    FROM #UniqueProducts
    WHERE row_num > 1
);

DROP TABLE #UniqueProducts;