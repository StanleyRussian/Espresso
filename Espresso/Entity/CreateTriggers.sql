---- Accounts
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Accounts_Insert'))
DROP TRIGGER [dbo].Accounts_Insert
GO
CREATE TRIGGER Accounts_Insert
ON Accounts
AFTER INSERT AS
INSERT INTO dAccountsBalances(Account_Id, Balance)
SELECT i.Id, 0
FROM INSERTED i
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Accounts_Delete'))
DROP TRIGGER [dbo].Accounts_Delete
GO
CREATE TRIGGER Accounts_Delete
ON Accounts
AFTER DELETE AS
DELETE FROM dAccountsBalances
WHERE Account_Id = (SELECT d.Id 
					   FROM DELETED d)
GO


---- CoffeeSorts
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeSorts_Insert'))
DROP TRIGGER [dbo].CoffeeSorts_Insert
GO
CREATE TRIGGER CoffeeSorts_Insert
ON CoffeeSorts
AFTER INSERT AS
--
INSERT INTO dGreenStocks (CoffeeSort_Id, Quantity)
SELECT i.Id, 0
FROM INSERTED i
--
INSERT INTO dRoastedStocks (CoffeeSort_Id, Quantity)
SELECT i.Id, 0
FROM INSERTED i
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeSorts_Delete'))
DROP TRIGGER [dbo].CoffeeSorts_Delete
GO
CREATE TRIGGER CoffeeSorts_Delete
ON CoffeeSorts
AFTER DELETE AS
--
DELETE FROM dGreenStocks
WHERE CoffeeSort_Id = (SELECT d.Id FROM DELETED d)
--
DELETE FROM dRoastedStocks
WHERE CoffeeSort_Id = (SELECT d.Id FROM DELETED d)
--
GO


---- Coffee Purchases
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_CoffeePurchase'))
DROP TRIGGER [dbo].AccountsBalances_Insert_CoffeePurchase
GO
CREATE TRIGGER AccountsBalances_Insert_CoffeePurchase
ON CoffeePurchases
AFTER INSERT AS
-- If it wasn't paid no actions on account's balance and transactions will be made
IF (SELECT i.Paid FROM INSERTED i) = 1
BEGIN
	BEGIN
		UPDATE dAccountsBalances
		SET Balance -= i.dSum
		FROM INSERTED i
		WHERE dAccountsBalances.Id = i.Account_Id
	END
	BEGIN
		INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
		SELECT i.Account_Id, i.Date, i.dSum, rc.Name, N'Закупка кофе'
		FROM INSERTED i
		INNER JOIN dbo.Suppliers rc
			ON rc.Id = i.Supplier_Id
	END
	BEGIN
		UPDATE CoffeePurchases
		SET TransactionId = (SELECT MAX(Id)
							 FROM dTransactions)
		FROM INSERTED i
		WHERE CoffeePurchases.Id = i.Id
	END
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_CoffeePurchase'))
DROP TRIGGER [dbo].AccountsBalances_Update_CoffeePurchase
GO
CREATE TRIGGER AccountsBalances_Update_CoffeePurchase
ON CoffeePurchases
AFTER UPDATE AS
-- If it was paid before
IF (SELECT d.Paid FROM DELETED d) = 1
BEGIN
	-- If it was paid before AND stay paid
	IF (SELECT i.Paid FROM INSERTED i) = 1
	BEGIN
		-- Correct account's balance
		BEGIN
			UPDATE dAccountsBalances
			SET Balance -= (i.dSum - d.dSum)
			FROM INSERTED i, DELETED d
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		-- Correct transaction's sum
		BEGIN
			UPDATE dTransactions
			SET Sum = i.dSum,
				Account_Id = i.Account_Id,
				Date = i.Date,
				Participant =  rc.Name
			FROM INSERTED i
				INNER JOIN dbo.Suppliers rc
				ON rc.Id = i.Supplier_Id
			WHERE dTransactions.Id = i.TransactionId
		END
	END
		-- If it was paid before BUT now it's not
	ELSE
	BEGIN
		-- Correct account's balance
		BEGIN
			UPDATE dAccountsBalances
			SET Balance += d.dSum
			FROM INSERTED i, DELETED d
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		-- Delete corresponding transaction
		BEGIN
			DELETE FROM dTransactions
			WHERE Id = (SELECT d.TransactionId
						FROM DELETED d)
		END
	END
END
ELSE
-- If it wasn't paid before
BEGIN
	-- If it wasn't paid before BUT now it's paid
	IF (SELECT i.Paid FROM INSERTED i) = 1
	BEGIN
		BEGIN
			UPDATE dAccountsBalances
			SET Balance -= i.dSum
			FROM INSERTED i
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		BEGIN
			INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
			SELECT i.Account_Id, i.Date, i.dSum, rc.Name, N'Закупка кофе'
			FROM INSERTED i
			INNER JOIN dbo.Suppliers rc
				ON rc.Id = i.Supplier_Id
		END
		BEGIN
			UPDATE CoffeePurchases
			SET TransactionId = (SELECT MAX(Id)
									FROM dTransactions)
			FROM INSERTED i
			WHERE CoffeePurchases.Id = i.Id
		END
	END
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_CoffeePurchase'))
DROP TRIGGER [dbo].AccountsBalances_Delete_CoffeePurchase
GO
CREATE TRIGGER AccountsBalances_Delete_CoffeePurchase
ON CoffeePurchases
AFTER DELETE AS
-- If it wasn't paid no actions needed
IF (SELECT d.Paid FROM DELETED d) = 1
BEGIN
	BEGIN
		UPDATE dAccountsBalances
		SET Balance += d.dSum
		FROM DELETED d
		WHERE dAccountsBalances.Id = d.Account_Id
	END
	BEGIN
		DELETE FROM dTransactions
		WHERE Id = (SELECT d.TransactionId
					FROM DELETED d)
	END
END
GO


-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeePurchase_Insert'))
DROP TRIGGER [dbo].CoffeePurchase_Insert
GO
CREATE TRIGGER CoffeePurchase_Insert
ON CoffeePurchaseDetails
AFTER INSERT AS
--
IF (SELECT dGreenStocks.Quantity 
	FROM dGreenStocks, INSERTED i 
	WHERE dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id) = 0
		UPDATE stock
		SET stock.dCost = i.Price,
			stock.Quantity += i.Quantity
		FROM INSERTED i
		INNER JOIN dbo.dGreenStocks stock
			ON stock.CoffeeSort_Id = i.CoffeeSort_Id
ELSE
		UPDATE stock
		SET stock.dCost = (stock.Quantity*stock.dCost + i.Quantity*i.Price)/(stock.Quantity + i.Quantity),
			stock.Quantity += i.Quantity
		FROM INSERTED i
		INNER JOIN dbo.dGreenStocks stock
			ON stock.CoffeeSort_Id = i.CoffeeSort_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeePurchase_Update'))
DROP TRIGGER [dbo].CoffeePurchase_Update
GO
CREATE TRIGGER CoffeePurchase_Update
ON CoffeePurchaseDetails
AFTER UPDATE AS
--
UPDATE dGreenStocks
SET Quantity += (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeePurchase_Delete'))
DROP TRIGGER [dbo].CoffeePurchase_Delete
GO
CREATE TRIGGER CoffeePurchase_Delete
ON CoffeePurchaseDetails
AFTER DELETE AS
--
UPDATE dGreenStocks
SET Quantity -= d.Quantity
FROM DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = d.CoffeeSort_Id
--
GO


---- Roasting
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Roasting_Insert'))
DROP TRIGGER [dbo].Roasting_Insert
GO
CREATE TRIGGER Roasting_Insert
ON Roastings
AFTER INSERT AS
--
UPDATE dGreenStocks
SET Quantity -= i.InitialAmount
FROM INSERTED i
WHERE dbo.dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id
--
UPDATE dRoastedStocks
SET Quantity += i.RoastedAmount
FROM INSERTED i
WHERE dbo.dRoastedStocks.CoffeeSort_Id = i.CoffeeSort_Id
--
IF (SELECT dRoastedStocks.Quantity 
	FROM dRoastedStocks, INSERTED i 
	WHERE dRoastedStocks.CoffeeSort_Id = i.CoffeeSort_Id) = 0
		UPDATE stock
		SET stock.dCost = (SELECT dCost FROM dGreenStocks WHERE CoffeeSort_Id = i.CoffeeSort_Id) * i.ShrinkagePercent,
			stock.Quantity += i.RoastedAmount
		FROM INSERTED i
		INNER JOIN dbo.dRoastedStocks stock
			ON stock.CoffeeSort_Id = i.CoffeeSort_Id
ELSE
		UPDATE stock
		SET stock.dCost = (stock.Quantity*stock.dCost + i.RoastedAmount*((SELECT dCost FROM dGreenStocks WHERE CoffeeSort_Id = i.CoffeeSort_Id) * i.ShrinkagePercent))/(stock.Quantity + i.RoastedAmount),
			stock.Quantity += i.RoastedAmount
		FROM INSERTED i
		INNER JOIN dbo.dRoastedStocks stock
			ON stock.CoffeeSort_Id = i.CoffeeSort_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Roasting_Update'))
DROP TRIGGER [dbo].Roasting_Update
GO
CREATE TRIGGER Roasting_Update
ON Roastings
AFTER UPDATE AS
--
UPDATE dGreenStocks
SET Quantity -= (i.InitialAmount - d.InitialAmount)
FROM INSERTED i, DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id
--
UPDATE dRoastedStocks
SET Quantity += (i.RoastedAmount - d.RoastedAmount)
FROM INSERTED i, DELETED d
WHERE dbo.dRoastedStocks.CoffeeSort_Id = i.CoffeeSort_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Roasting_Delete'))
DROP TRIGGER [dbo].Roasting_Delete
GO
CREATE TRIGGER Roasting_Delete
ON Roastings
AFTER DELETE AS
--
UPDATE dGreenStocks
SET Quantity += d.InitialAmount
FROM DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = d.CoffeeSort_Id
--
UPDATE dRoastedStocks
SET Quantity -= d.RoastedAmount
FROM DELETED d
WHERE dbo.dRoastedStocks.CoffeeSort_Id = d.CoffeeSort_Id
--
GO


---- Package
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Insert_Package'))
DROP TRIGGER [dbo].PackageStocks_Insert_Package
GO
CREATE TRIGGER PackageStocks_Insert_Package
ON Packages
AFTER INSERT AS
--
INSERT INTO dPackageStocks(Package_Id, Quantity)
SELECT i.Id, 0
FROM INSERTED i
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Delete_Package'))
DROP TRIGGER [dbo].PackageStocks_Delete_Package
GO
CREATE TRIGGER PackageStocks_Delete_Package
ON Packages
AFTER DELETE AS
--
DELETE FROM dPackageStocks
WHERE Package_Id = (SELECT d.Id 
					FROM DELETED d)
--
GO


---- PackagePurchase
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackagePurchase_Insert'))
DROP TRIGGER [dbo].PackagePurchase_Insert
GO
CREATE TRIGGER PackagePurchase_Insert
ON PackagePurchases
AFTER INSERT AS
--
--UPDATE dPackageStocks
--SET Quantity += i.Quantity
--FROM INSERTED i
--WHERE dPackageStocks.Package_Id = i.Package_Id
--
UPDATE dAccountsBalances
SET Balance -= i.dSum
FROM INSERTED i
WHERE dAccountsBalances.Id = i.Account_Id
--
INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
SELECT i.Account_Id, i.Date, i.dSum, rc.Name, N'Закупка упаковки'
FROM INSERTED i
	INNER JOIN dbo.Suppliers rc
	ON rc.Id = i.Supplier_Id
--
UPDATE PackagePurchases
SET TransactionId = (SELECT MAX(Id)
					 FROM dTransactions)
FROM INSERTED i
WHERE PackagePurchases.Id = i.Id
--
IF (SELECT dPackageStocks.Quantity 
	FROM dPackageStocks, INSERTED i 
	WHERE dPackageStocks.Package_Id = i.Package_Id) = 0
		UPDATE stock
		SET stock.dCost = i.Price,
			stock.Quantity += i.Quantity
		FROM INSERTED i
		INNER JOIN dbo.dPackageStocks stock
			ON stock.Package_Id = i.Package_Id
ELSE
		UPDATE stock
		SET stock.dCost = (stock.Quantity*stock.dCost + i.Quantity*i.Price)/(stock.Quantity + i.Quantity),
			stock.Quantity += i.Quantity
		FROM INSERTED i
		INNER JOIN dbo.dPackageStocks stock
			ON stock.Package_Id = i.Package_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackagePurchase_Update'))
DROP TRIGGER [dbo].PackagePurchase_Update
GO
CREATE TRIGGER PackagePurchase_Update
ON PackagePurchases
AFTER UPDATE AS
--
UPDATE dPackageStocks
SET Quantity += (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
WHERE dPackageStocks.Package_Id = i.Package_Id
--
UPDATE dAccountsBalances
SET Balance -= (i.dSum - d.dSum)
FROM INSERTED i, DELETED d
WHERE dAccountsBalances.Id = i.Account_Id
--
UPDATE dTransactions
SET Sum = i.dSum,
	Account_Id = i.Account_Id,
	Date = i.Date,
	Participant =  rc.Name
FROM INSERTED i
	INNER JOIN dbo.Suppliers rc
	ON rc.Id = i.Supplier_Id
WHERE dTransactions.Id = i.TransactionId
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackagePurchase_Delete'))
DROP TRIGGER [dbo].PackagePurchase_Delete
GO
CREATE TRIGGER PackagePurchase_Delete
ON PackagePurchases
AFTER DELETE AS
--
UPDATE dPackageStocks
SET Quantity -= d.Quantity
FROM DELETED d
WHERE dPackageStocks.Package_Id = d.Package_Id
--
UPDATE dAccountsBalances
SET Balance += d.dSum
FROM DELETED d
WHERE dAccountsBalances.Id = d.Account_Id
--
DELETE FROM dTransactions
WHERE Id = (SELECT d.TransactionId
			FROM DELETED d)
GO



---- Packings
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Packings_Insert'))
DROP TRIGGER [dbo].Packings_Insert
GO
CREATE TRIGGER Packings_Insert
ON Packings
AFTER INSERT AS
--
UPDATE rs SET
  Quantity -= md.Ratio * i.Quantity * pk.Capacity
FROM INSERTED i
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = i.Mix_Id
INNER JOIN dbo.Packages pk
   ON pk.Id = i.Package_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
--
UPDATE dPackedStocks 
SET Quantity += i.Quantity
FROM INSERTED i
WHERE dPackedStocks.Mix_Id = i.Mix_Id 
	AND dPackedStocks.Package_Id = i.Package_Id
IF @@ROWCOUNT = 0
BEGIN
	INSERT INTO dPackedStocks (Mix_Id, Package_Id, Quantity)
	SELECT i.Mix_Id, i.Package_Id, i.Quantity
	FROM INSERTED i
END
--
--IF (SELECT dPackedStocks.Quantity 
--	FROM dPackedStocks, INSERTED i 
--	WHERE dPackedStocks.Mix_Id = i.Mix_Id
--	AND dPackedStocks.Package_Id = i.Package_Id) = 0
--		UPDATE stock
--		SET stock.dCost = (SELECT dCost FROM dRoastedStocks WHERE CoffeeSort_Id = ),
--			stock.Quantity += i.Quantity
--		FROM INSERTED i
--		INNER JOIN dbo.dPackedStocks stock
--			ON stock.Package_Id = i.Package_Id
--			AND stock.Mix_Id = i.Mix_Id
--ELSE
--		UPDATE stock
--		SET stock.dCost = (stock.Quantity*stock.dCost + i.Quantity*i.Price)/(stock.Quantity + i.Quantity),
--			stock.Quantity += i.Quantity
--		FROM INSERTED i
--		INNER JOIN dbo.dGreenStocks stock
--			ON stock.CoffeeSort_Id = i.CoffeeSort_Id
--
UPDATE dPackageStocks
SET Quantity -= i.Quantity
FROM INSERTED i
WHERE dPackageStocks.Package_Id = i.Package_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Packings_Update'))
DROP TRIGGER [dbo].Packings_Update
GO
CREATE TRIGGER Packings_Update
ON Packings
AFTER UPDATE AS
--
UPDATE rs SET
  Quantity += md.Ratio * pk.Capacity * (d.Quantity - i.Quantity)
FROM INSERTED i, DELETED d
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = d.Mix_Id
INNER JOIN dbo.Packages pk
   ON pk.Id = d.Package_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
--
UPDATE dPackedStocks  
SET Quantity += (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
--
UPDATE dPackageStocks
SET Quantity -= (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
WHERE dPackageStocks.Package_Id = i.Package_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Packings_Delete'))
DROP TRIGGER [dbo].Packings_Delete
GO
CREATE TRIGGER Packings_Delete
ON Packings
AFTER DELETE AS
--
UPDATE rs SET
  Quantity += md.Ratio * d.Quantity * pk.Capacity
FROM DELETED d
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = d.Mix_Id
INNER JOIN dbo.Packages pk
   ON pk.Id = d.Package_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
--
UPDATE dPackedStocks
SET Quantity -= D.Quantity
FROM DELETED d
WHERE dPackedStocks.Mix_Id = d.Mix_Id 
	AND dPackedStocks.Package_Id = d.Package_Id
--
UPDATE dPackageStocks
SET Quantity += d.Quantity
FROM DELETED d
WHERE dPackageStocks.Package_Id = d.Package_Id
--
GO


---- Products
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Product_Insert'))
DROP TRIGGER [dbo].Product_Insert
GO
CREATE TRIGGER Product_Insert
ON Products
AFTER INSERT AS
--
INSERT INTO dProductStocks(Product_Id, Quantity)
SELECT i.Id, 0
FROM INSERTED i
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].Product_Delete'))
DROP TRIGGER [dbo].Product_Delete
GO
CREATE TRIGGER Product_Delete
ON Packages
AFTER DELETE AS
--
DELETE FROM dPackageStocks
WHERE Package_Id = (SELECT d.Id 
					FROM DELETED d)
--
GO


---- ProductPurchases
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].ProductPurchases_Insert'))
DROP TRIGGER [dbo].ProductPurchases_Insert
GO
CREATE TRIGGER ProductPurchases_Insert
ON ProductPurchases
AFTER INSERT AS
--
UPDATE dProductStocks
SET Quantity += i.Quantity
FROM INSERTED i
WHERE dProductStocks.Product_Id = i.Product_Id
--
IF (SELECT dProductStocks.Quantity 
	FROM dProductStocks, INSERTED i 
	WHERE dProductStocks.Product_Id = i.Product_Id) = 0
		UPDATE stock
		SET stock.dCost = i.Price,
			stock.Quantity += i.Quantity
		FROM INSERTED i
		INNER JOIN dbo.dProductStocks stock
			ON stock.Product_Id = i.Product_Id
ELSE
		UPDATE stock
		SET stock.dCost = (stock.Quantity*stock.dCost + i.Quantity*i.Price)/(stock.Quantity + i.Quantity),
			stock.Quantity += i.Quantity
		FROM INSERTED i
		INNER JOIN dbo.dProductStocks stock
			ON stock.Product_Id = i.Product_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].ProductPurchases_Update'))
DROP TRIGGER [dbo].ProductPurchases_Update
GO
CREATE TRIGGER ProductPurchases_Update
ON ProductPurchases
AFTER UPDATE AS
--
UPDATE dProductStocks
SET Quantity += (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
WHERE dProductStocks.Product_Id = i.Product_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].ProductPurchases_Delete'))
DROP TRIGGER [dbo].ProductPurchases_Delete
GO
CREATE TRIGGER ProductPurchases_Delete
ON ProductPurchases
AFTER UPDATE AS
--
UPDATE dProductStocks
SET Quantity -= d.Quantity
FROM DELETED d
WHERE dProductStocks.Product_Id = d.Product_Id
--
GO


---- Sales
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_CoffeeSale'))
DROP TRIGGER [dbo].AccountsBalances_Insert_CoffeeSale
GO
CREATE TRIGGER AccountsBalances_Insert_CoffeeSale
ON Sales
AFTER INSERT AS	
-- If it wasn't paid no actions on account's balance and transactions will be made
IF (SELECT i.Paid FROM INSERTED i) = 1
BEGIN
	BEGIN
		UPDATE dAccountsBalances
		SET Balance += i.dSum
		FROM INSERTED i
		WHERE dAccountsBalances.Id = i.Account_Id
	END
	BEGIN
		INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
		SELECT i.Account_Id, i.Date, i.dSum, rc.Name, N'Продажа кофе'
		FROM INSERTED i
			INNER JOIN dbo.Recipients rc
			ON rc.Id = i.Recipient_Id
	END
	BEGIN
		UPDATE Sales
		SET TransactionId = (SELECT MAX(Id)
								FROM dTransactions)
		FROM INSERTED i
		WHERE Sales.Id = i.Id
	END
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_CoffeeSale'))
DROP TRIGGER [dbo].AccountsBalances_Update_CoffeeSale
GO
CREATE TRIGGER AccountsBalances_Update_CoffeeSale
ON Sales
AFTER UPDATE AS
-- If it was paid before
IF (SELECT d.Paid FROM DELETED d) = 1
BEGIN
	-- If it was paid before AND stay paid
	IF (SELECT i.Paid FROM INSERTED i) = 1
	BEGIN
		-- Correct account's balance
		BEGIN
			UPDATE dAccountsBalances
			SET Balance += (i.dSum - d.dSum)
			FROM INSERTED i, DELETED d
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		-- Correct transaction's sum
		BEGIN
			UPDATE dTransactions
			SET Sum = i.dSum,
				Account_Id = i.Account_Id,
				Date = i.Date,
				Participant =  rc.Name
			FROM INSERTED i
				INNER JOIN dbo.Recipients rc
				ON rc.Id = i.Recipient_Id
			WHERE dTransactions.Id = i.TransactionId
		END
	END
	-- If it was paid before BUT now it's not
	ELSE
	BEGIN
		-- Correct account's balance
		BEGIN
			UPDATE dAccountsBalances
			SET Balance += d.dSum
			FROM INSERTED i, DELETED d
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		-- Delete corresponding transaction
		BEGIN
			DELETE FROM dTransactions
			WHERE Id = (SELECT d.TransactionId
						FROM DELETED d)
		END
	END
END
ELSE
-- If it wasn't paid before
BEGIN
	-- If it wasn't paid before BUT now it's paid
	IF (SELECT i.Paid FROM INSERTED i) = 1
	BEGIN
		BEGIN
			UPDATE dAccountsBalances
			SET Balance -= i.dSum
			FROM INSERTED i
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		BEGIN
			INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
			SELECT i.Account_Id, i.Date, i.dSum, rc.Name, N'Продажа кофе'
			FROM INSERTED i
			INNER JOIN dbo.Recipients rc
				ON rc.Id = i.Recipient_Id
		END
		BEGIN
			UPDATE Sales
			SET TransactionId = (SELECT MAX(Id)
									FROM dTransactions)
			FROM INSERTED i
			WHERE Sales.Id = i.Id
		END
	END
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_CoffeeSale'))
DROP TRIGGER [dbo].AccountsBalances_Delete_CoffeeSale
GO
CREATE TRIGGER AccountsBalances_Delete_CoffeeSale
ON Sales
AFTER DELETE AS
-- If it wasn't paid no actions needed
IF (SELECT d.Paid FROM DELETED d) = 1
BEGIN
	BEGIN
		UPDATE dAccountsBalances
		SET Balance -= d.dSum
		FROM DELETED d
		WHERE dAccountsBalances.Id = d.Account_Id
	END
	BEGIN
		DELETE FROM dTransactions
		WHERE Id = (SELECT d.TransactionId
					FROM DELETED d)
	END
END
GO


---- Sales_Coffee
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeSale_Insert'))
DROP TRIGGER [dbo].CoffeeSale_Insert
GO
CREATE TRIGGER CoffeeSale_Insert
ON SaleDetailsCoffee
AFTER INSERT AS
--
UPDATE dPackedStocks
SET Quantity -= i.Quantity
FROM INSERTED i
WHERE dPackedStocks.Mix_Id = i.Mix_Id 
	AND dPackedStocks.Package_Id = i.Package_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeSale_Update'))
DROP TRIGGER [dbo].CoffeeSale_Update
GO
CREATE TRIGGER CoffeeSale_Update
ON SaleDetailsCoffee
AFTER UPDATE AS
--
UPDATE dPackedStocks
SET Quantity -= (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
WHERE dPackedStocks.Mix_Id = i.Mix_Id 
	AND dPackedStocks.Package_Id = i.Package_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeSale_Delete'))
DROP TRIGGER [dbo].CoffeeSale_Delete
GO
CREATE TRIGGER CoffeeSale_Delete
ON SaleDetailsCoffee
AFTER UPDATE AS
--
UPDATE dPackedStocks
SET Quantity += d.Quantity
FROM DELETED d
WHERE dPackedStocks.Mix_Id = d.Mix_Id 
	AND dPackedStocks.Package_Id = d.Package_Id
--
GO


---- Sales_Products
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].ProductSale_Insert'))
DROP TRIGGER [dbo].ProductSale_Insert
GO
CREATE TRIGGER ProductSale_Insert
ON SaleDetailsProduct
AFTER INSERT AS
--
UPDATE dProductStocks
SET Quantity -= i.Quantity
FROM INSERTED i
WHERE dProductStocks.Product_Id = i.Product_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].ProductSale_Update'))
DROP TRIGGER [dbo].ProductSale_Update
GO
CREATE TRIGGER ProductSale_Update
ON SaleDetailsProduct
AFTER UPDATE AS
--
UPDATE dProductStocks
SET Quantity -= (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
WHERE dProductStocks.Product_Id = i.Product_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].ProductSale_Delete'))
DROP TRIGGER [dbo].ProductSale_Delete
GO
CREATE TRIGGER ProductSale_Delete
ON SaleDetailsProduct
AFTER UPDATE AS
--
UPDATE dProductStocks
SET Quantity += d.Quantity
FROM DELETED d
WHERE dProductStocks.Product_Id = d.Product_Id
--
GO


---- Payments
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_Payments'))
DROP TRIGGER [dbo].AccountsBalances_Insert_Payments
GO
CREATE TRIGGER AccountsBalances_Insert_Payments
ON Payments
AFTER INSERT AS
--
UPDATE dAccountsBalances
SET Balance -= i.Sum
FROM INSERTED i
WHERE dAccountsBalances.Id = i.Account_Id
--
INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
SELECT i.Account_Id, i.Date, i.Sum, i.Designation, N'Платёж'
FROM INSERTED i
--
UPDATE PackagePurchases
SET TransactionId = (SELECT MAX(Id)
					 FROM dTransactions)
FROM INSERTED i
WHERE PackagePurchases.Id = i.Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_Payments'))
DROP TRIGGER [dbo].AccountsBalances_Update_Payments
GO
CREATE TRIGGER AccountsBalances_Update_Payments
ON Payments
AFTER UPDATE AS
--
UPDATE dAccountsBalances
SET Balance -= (i.Sum - d.Sum)
FROM INSERTED i, DELETED d
WHERE dAccountsBalances.Id = i.Account_Id
--
UPDATE dTransactions
SET Sum = i.Sum,
	Account_Id = i.Account_Id,
	Date = i.Date,
	Participant =  i.Designation
FROM INSERTED i
WHERE dTransactions.Id = i.TransactionId
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_Payments'))
DROP TRIGGER [dbo].AccountsBalances_Delete_Payments
GO
CREATE TRIGGER AccountsBalances_Delete_Payments
ON Payments
AFTER DELETE AS
--
UPDATE dAccountsBalances
SET Balance += d.Sum
FROM DELETED d
WHERE dAccountsBalances.Id = d.Account_Id
--
DELETE FROM dTransactions
WHERE Id = (SELECT d.TransactionId
			FROM DELETED d)
--
GO


---- OtherProfits
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_OtherProfits'))
DROP TRIGGER [dbo].AccountsBalances_Insert_OtherProfits
GO
CREATE TRIGGER AccountsBalances_Insert_OtherProfits
ON OtherProfits
AFTER INSERT AS
--
UPDATE dAccountsBalances
SET Balance += i.Sum
FROM INSERTED i
WHERE dAccountsBalances.Id = i.Account_Id
--
INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
SELECT i.Account_Id, i.Date, i.Sum, i.Designation, N'Доход'
FROM INSERTED i
--
UPDATE OtherProfits
SET TransactionId = (SELECT MAX(Id)
					 FROM dTransactions)
FROM INSERTED i
WHERE OtherProfits.Id = i.Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_OtherProfits'))
DROP TRIGGER [dbo].AccountsBalances_Update_OtherProfits
GO
CREATE TRIGGER AccountsBalances_Update_OtherProfits
ON OtherProfits
AFTER UPDATE AS
--
UPDATE dAccountsBalances
SET Balance += (i.Sum - d.Sum)
FROM INSERTED i, DELETED d
WHERE dAccountsBalances.Id = i.Account_Id
--
UPDATE dTransactions
SET Sum = i.Sum,
	Account_Id = i.Account_Id,
	Date = i.Date,
	Participant =  i.Designation
FROM INSERTED i
WHERE dTransactions.Id = i.TransactionId
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_OtherProfits'))
DROP TRIGGER [dbo].AccountsBalances_Delete_OtherProfits
GO
CREATE TRIGGER AccountsBalances_Delete_OtherProfits
ON OtherProfits
AFTER DELETE AS
--
UPDATE dAccountsBalances
SET Balance -= d.Sum
FROM DELETED d
WHERE dAccountsBalances.Id = d.Account_Id
--
DELETE FROM dTransactions
WHERE Id = (SELECT d.TransactionId
			FROM DELETED d)
--
GO


---- MonthlyPayments
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_MonthlyPayments'))
DROP TRIGGER [dbo].AccountsBalances_Insert_MonthlyPayments
GO
CREATE TRIGGER AccountsBalances_Insert_MonthlyPayments
ON MonthlyPayments
AFTER INSERT AS
--
UPDATE dAccountsBalances
SET Balance -= i.PaidAmount
FROM INSERTED i
WHERE dAccountsBalances.Id = i.Account_Id
--
INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
SELECT i.Account_Id, i.Date, i.PaidAmount, me.Designation, N'Ежемесячный платёж'
FROM INSERTED i
	INNER JOIN dbo.MonthlyExpenses me
	ON me.Id = i.MonthlyExpense_Id
--
UPDATE PackagePurchases
SET TransactionId = (SELECT MAX(Id)
					 FROM dTransactions)
FROM INSERTED i
WHERE PackagePurchases.Id = i.Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_MonthlyPayments'))
DROP TRIGGER [dbo].AccountsBalances_Update_MonthlyPayments
GO
CREATE TRIGGER AccountsBalances_Update_MonthlyPayments
ON MonthlyPayments
AFTER UPDATE AS
--
UPDATE dAccountsBalances
SET Balance -= (i.PaidAmount - d.PaidAmount)
FROM INSERTED i, DELETED d
WHERE dAccountsBalances.Id = i.Account_Id
--
UPDATE dTransactions
SET Sum = i.PaidAmount,
	Account_Id = i.Account_Id,
	Date = i.Date,
	Participant =  me.Designation
FROM INSERTED i
	INNER JOIN dbo.MonthlyExpenses me
	ON me.Id = i.MonthlyExpense_Id
WHERE dTransactions.Id = i.TransactionId
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_MonthlyPayments'))
DROP TRIGGER [dbo].AccountsBalances_Delete_MonthlyPayments
GO
CREATE TRIGGER AccountsBalances_Delete_MonthlyPayments
ON MonthlyPayments
AFTER DELETE AS
--
UPDATE dAccountsBalances
SET Balance += d.PaidAmount
FROM DELETED d
WHERE dAccountsBalances.Id = d.Account_Id
--
DELETE FROM dTransactions
WHERE Id = (SELECT d.TransactionId
			FROM DELETED d)
--
GO


---- MoneyTranfers
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_MoneyTransfers'))
DROP TRIGGER [dbo].AccountsBalances_Insert_MoneyTransfers
GO
CREATE TRIGGER AccountsBalances_Insert_MoneyTransfers
ON MoneyTransfers
AFTER INSERT AS
--
UPDATE dAccountsBalances
SET Balance -= i.Sum
FROM INSERTED i
WHERE dAccountsBalances.Id = i.InitialAccount_Id
--
UPDATE dAccountsBalances
SET Balance += i.Sum
FROM INSERTED i
WHERE dAccountsBalances.Id = i.TargetAccount_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_MoneyTransfers'))
DROP TRIGGER [dbo].AccountsBalances_Update_MoneyTransfers
GO
CREATE TRIGGER AccountsBalances_Update_MoneyTransfers
ON MoneyTransfers
AFTER UPDATE AS
--
UPDATE dAccountsBalances
SET Balance -= (i.Sum - d.Sum)
FROM INSERTED i, DELETED d
WHERE dAccountsBalances.Id = i.InitialAccount_Id
--
UPDATE dAccountsBalances
SET Balance += (i.Sum - d.Sum)
FROM INSERTED i, DELETED d
WHERE dAccountsBalances.Id = i.TargetAccount_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_MoneyTransfers'))
DROP TRIGGER [dbo].AccountsBalances_Delete_MoneyTransfers
GO
CREATE TRIGGER AccountsBalances_Delete_MoneyTransfers
ON MoneyTransfers
AFTER DELETE AS
--
UPDATE dAccountsBalances
SET Balance += d.Sum
FROM DELETED d
WHERE dAccountsBalances.Id = d.InitialAccount_Id
--
UPDATE dAccountsBalances
SET Balance -= d.Sum
FROM DELETED d
WHERE dAccountsBalances.Id = d.TargetAccount_Id
--
GO


---- CoffeeTransfers
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeTransfers_Insert'))
DROP TRIGGER [dbo].CoffeeTransfers_Insert
GO
CREATE TRIGGER CoffeeTransfers_Insert
ON CoffeeTransfers
AFTER INSERT AS
--
UPDATE rs SET
  Quantity -= md.Ratio * i.Quantity
FROM INSERTED i
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = i.Mix_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
--
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeTransfers_Update'))
DROP TRIGGER [dbo].CoffeeTransfers_Update
GO
CREATE TRIGGER CoffeeTransfers_Update
ON CoffeeTransfers
AFTER UPDATE AS
--
UPDATE rs SET
  Quantity += md.Ratio * (d.Quantity - i.Quantity)
FROM INSERTED i, DELETED d
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = d.Mix_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
--
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].CoffeeTransfers_Delete'))
DROP TRIGGER [dbo].CoffeeTransfers_Delete
GO
CREATE TRIGGER CoffeeTransfers_Delete
ON CoffeeTransfers
AFTER DELETE AS
--
UPDATE rs SET
  Quantity += md.Ratio * d.Quantity
FROM DELETED d
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = d.Mix_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
--
GO
