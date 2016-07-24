------ dGreenStocks
---- Coffee Sorts
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Insert_CoffeeSorts'))
DROP TRIGGER [dbo].GreenStocks_Insert_CoffeeSorts
GO
CREATE TRIGGER GreenStocks_Insert_CoffeeSorts
ON CoffeeSorts
AFTER INSERT AS
INSERT INTO dGreenStocks (CoffeeSort_Id, Quantity)
SELECT i.Id, 0
FROM INSERTED i
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Delete_CoffeeSorts'))
DROP TRIGGER [dbo].GreenStocks_Delete_CoffeeSorts
GO
CREATE TRIGGER GreenStocks_Delete_CoffeeSorts
ON CoffeeSorts
AFTER DELETE AS
DELETE FROM dGreenStocks
WHERE CoffeeSort_Id = (SELECT d.Id 
					   FROM DELETED d)
GO


---- CoffeePurchases
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Insert_CoffeePurchase'))
DROP TRIGGER [dbo].GreenStocks_Insert_CoffeePurchase
GO
CREATE TRIGGER GreenStocks_Insert_CoffeePurchase
ON CoffeePurchase_Details
AFTER INSERT AS
UPDATE dGreenStocks
SET Quantity += i.Quantity
FROM INSERTED i
WHERE dbo.dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Update_CoffeePurchase'))
DROP TRIGGER [dbo].GreenStocks_Update_CoffeePurchase
GO
CREATE TRIGGER GreenStocks_Update_CoffeePurchase
ON CoffeePurchase_Details
AFTER UPDATE AS
UPDATE dGreenStocks
SET Quantity += (i.Quantity - d.Quantity)
FROM INSERTED i, DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Delete_CoffeePurchase'))
DROP TRIGGER [dbo].GreenStocks_Delete_CoffeePurchase
GO
CREATE TRIGGER GreenStocks_Delete_CoffeePurchase
ON CoffeePurchase_Details
AFTER DELETE AS
UPDATE dGreenStocks
SET Quantity -= d.Quantity
FROM DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = d.CoffeeSort_Id
GO


---- Roastings
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Insert_Roasting'))
DROP TRIGGER [dbo].GreenStocks_Insert_Roasting
GO
CREATE TRIGGER GreenStocks_Insert_Roasting
ON Roastings
AFTER INSERT AS
UPDATE dGreenStocks
SET Quantity -= i.InitialAmount
FROM INSERTED i
WHERE dbo.dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Update_Roasting'))
DROP TRIGGER [dbo].GreenStocks_Update_Roasting
GO
CREATE TRIGGER GreenStocks_Update_Roasting
ON Roastings
AFTER UPDATE AS
UPDATE dGreenStocks
SET Quantity -= (i.InitialAmount - d.InitialAmount)
FROM INSERTED i, DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = i.CoffeeSort_Id
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].GreenStocks_Delete_Roasting'))
DROP TRIGGER [dbo].GreenStocks_Delete_Roasting
GO
CREATE TRIGGER GreenStocks_Delete_Roasting
ON Roastings
AFTER DELETE AS
UPDATE dGreenStocks
SET Quantity += d.InitialAmount
FROM DELETED d
WHERE dbo.dGreenStocks.CoffeeSort_Id = d.CoffeeSort_Id
GO



------ dRoastedStocks
---- Coffee Sorts
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Insert_CoffeeSorts'))
DROP TRIGGER [dbo].RoastedStocks_Insert_CoffeeSorts
GO
CREATE TRIGGER RoastedStocks_Insert_CoffeeSorts
ON CoffeeSorts
AFTER INSERT AS
INSERT INTO dRoastedStocks (CoffeeSort_Id, Quantity)
SELECT i.Id, 0
FROM INSERTED i
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Delete_CoffeeSorts'))
DROP TRIGGER [dbo].RoastedStocks_Delete_CoffeeSorts
GO
CREATE TRIGGER RoastedStocks_Delete_CoffeeSorts
ON CoffeeSorts
AFTER DELETE AS
DELETE FROM dRoastedStocks
WHERE CoffeeSort_Id = (SELECT DELETED.Id FROM DELETED)
GO


---- Roastings
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Insert_Roasting'))
DROP TRIGGER [dbo].RoastedStocks_Insert_Roasting
GO
CREATE TRIGGER RoastedStocks_Insert_Roasting
ON Roastings
AFTER INSERT AS
UPDATE dRoastedStocks
SET Quantity += i.RoastedAmount
FROM INSERTED i
WHERE dbo.dRoastedStocks.CoffeeSort_Id = i.CoffeeSort_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Update_Roasting'))
DROP TRIGGER [dbo].RoastedStocks_Update_Roasting
GO
CREATE TRIGGER RoastedStocks_Update_Roasting
ON Roastings
AFTER UPDATE AS
UPDATE dRoastedStocks
SET Quantity += (i.RoastedAmount - d.RoastedAmount)
FROM INSERTED i, DELETED d
WHERE dbo.dRoastedStocks.CoffeeSort_Id = i.CoffeeSort_Id
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Delete_Roasting'))
DROP TRIGGER [dbo].RoastedStocks_Delete_Roasting
GO
CREATE TRIGGER RoastedStocks_Delete_Roasting
ON Roastings
AFTER DELETE AS
UPDATE dRoastedStocks
SET Quantity -= d.RoastedAmount
FROM DELETED d
WHERE dbo.dRoastedStocks.CoffeeSort_Id = d.CoffeeSort_Id
GO


---- Packings
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Insert_Packing'))
DROP TRIGGER [dbo].RoastedStocks_Insert_Packing
GO
CREATE TRIGGER RoastedStocks_Insert_Packing
ON Packings
AFTER INSERT AS
UPDATE rs SET
  Quantity -= md.Ratio / 100 * i.PackQuantity * pk.Capacity
FROM INSERTED i
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = i.Mix_Id
INNER JOIN dbo.Packages pk
   ON pk.Id = i.Package_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Update_Packing'))
DROP TRIGGER [dbo].RoastedStocks_Update_Packing
GO
CREATE TRIGGER RoastedStocks_Update_Packing
ON Packings
AFTER UPDATE AS
BEGIN
--// Add amount of roasted coffee from deleted packing
UPDATE rs SET
  Quantity += md.Ratio / 100 * d.PackQuantity * pk.Capacity
FROM DELETED d
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = d.Mix_Id
INNER JOIN dbo.Packages pk
   ON pk.Id = d.Package_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
--// Deduct amount of roasted coffee from inserted packing
UPDATE rs SET
  Quantity -= md.Ratio / 100 * i.PackQuantity * pk.Capacity
FROM INSERTED i
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = i.Mix_Id
INNER JOIN dbo.Packages pk
   ON pk.Id = i.Package_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Delete_Packing'))
DROP TRIGGER [dbo].RoastedStocks_Delete_Packing
GO
CREATE TRIGGER RoastedStocks_Delete_Packing
ON Packings
AFTER DELETE AS
UPDATE rs SET
  Quantity += md.Ratio / 100 * d.PackQuantity * pk.Capacity
FROM DELETED d
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = d.Mix_Id
INNER JOIN dbo.Packages pk
   ON pk.Id = d.Package_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
GO


---- CoffeeTransfers
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Insert_CoffeeTransfer'))
DROP TRIGGER [dbo].RoastedStocks_Insert_CoffeeTransfer
GO
CREATE TRIGGER RoastedStocks_Insert_CoffeeTransfer
ON CoffeeTransfers
AFTER INSERT AS
UPDATE rs SET
  Quantity -= md.Ratio / 100 * i.Quantity
FROM INSERTED i
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = i.Mix_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Update_CoffeeTransfers'))
DROP TRIGGER [dbo].RoastedStocks_Update_CoffeeTransfers
GO
CREATE TRIGGER RoastedStocks_Update_CoffeeTransfers
ON CoffeeTransfers
AFTER UPDATE AS
BEGIN
	--// Add amount of roasted coffee from deleted packing
	UPDATE rs SET
	  Quantity += md.Ratio / 100 * d.Quantity
	FROM DELETED d
	INNER JOIN dbo.Mix_Details md
	   ON md.Mix_Id = d.Mix_Id
	INNER JOIN dbo.dRoastedStocks rs
	   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
	--// Deduct amount of roasted coffee from inserted packing
	UPDATE rs SET
	  Quantity -= md.Ratio / 100 * i.Quantity
	FROM INSERTED i
	INNER JOIN dbo.Mix_Details md
	   ON md.Mix_Id = i.Mix_Id
	INNER JOIN dbo.dRoastedStocks rs
	   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].RoastedStocks_Delete_CoffeeTransfers'))
DROP TRIGGER [dbo].RoastedStocks_Delete_CoffeeTransfers
GO
CREATE TRIGGER RoastedStocks_Delete_CoffeeTransfers
ON CoffeeTransfers
AFTER DELETE AS
UPDATE rs SET
  Quantity += md.Ratio / 100 * d.Quantity
FROM DELETED d
INNER JOIN dbo.Mix_Details md
   ON md.Mix_Id = d.Mix_Id
INNER JOIN dbo.dRoastedStocks rs
   ON rs.CoffeeSort_Id = md.CoffeeSort_Id
GO




------ dPackedStocks
---- Packings
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackedStocks_Insert_Packing'))
DROP TRIGGER [dbo].PackedStocks_Insert_Packing
GO
CREATE TRIGGER PackedStocks_Insert_Packing
ON Packings
AFTER INSERT AS
UPDATE dPackedStocks SET
	PackQuantity += i.PackQuantity
	FROM INSERTED i
	WHERE dPackedStocks.Mix_Id = i.Mix_Id 
		AND dPackedStocks.Package_Id = i.Package_Id
		AND dPackedStocks.PackedCategory_Id = i.PackedCategory_Id
IF @@ROWCOUNT = 0
BEGIN
	INSERT INTO dPackedStocks (Mix_Id, Package_Id, PackedCategory_Id, PackQuantity)
	SELECT i.Mix_Id, i.Package_Id, i.PackedCategory_Id, i.PackQuantity
	FROM INSERTED i
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackedStocks_Update_Packing'))
DROP TRIGGER [dbo].PackedStocks_Update_Packing
GO
CREATE TRIGGER PackedStocks_Update_Packing
ON Packings
AFTER DELETE AS
UPDATE dPackedStocks
SET PackQuantity += (i.PackQuantity - d.PackQuantity)
FROM INSERTED i, DELETED d
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackedStocks_Delete_Packing'))
DROP TRIGGER [dbo].PackedStocks_Delete_Packing
GO
CREATE TRIGGER PackedStocks_Delete_Packing
ON Packings
AFTER DELETE AS
UPDATE dPackedStocks
SET PackQuantity -= D.PackQuantity
FROM DELETED d
WHERE dPackedStocks.Mix_Id = d.Mix_Id 
	AND dPackedStocks.Package_Id = d.Package_Id
	AND dPackedStocks.PackedCategory_Id = d.PackedCategory_Id
GO


---- CoffeeSales
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackedStocks_Insert_Sales'))
DROP TRIGGER [dbo].PackedStocks_Insert_Sales
GO
CREATE TRIGGER PackedStocks_Insert_Sales
ON CoffeeSale_Details
AFTER INSERT AS
UPDATE dPackedStocks
SET PackQuantity -= i.PackQuantity
FROM INSERTED i
WHERE dPackedStocks.Mix_Id = i.Mix_Id 
	AND dPackedStocks.Package_Id = i.Package_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackedStocks_Update_Sales'))
DROP TRIGGER [dbo].PackedStocks_Update_Sales
GO
CREATE TRIGGER PackedStocks_Update_Sales
ON CoffeeSale_Details
AFTER UPDATE AS
UPDATE dPackedStocks
SET PackQuantity -= (i.PackQuantity - d.PackQuantity)
FROM INSERTED i, DELETED d
WHERE dPackedStocks.Mix_Id = i.Mix_Id 
	AND dPackedStocks.Package_Id = i.Package_Id
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackedStocks_Delete_Sales'))
DROP TRIGGER [dbo].PackedStocks_Delete_Sales
GO
CREATE TRIGGER PackedStocks_Delete_Sales
ON CoffeeSale_Details
AFTER UPDATE AS
UPDATE dPackedStocks
SET PackQuantity += d.PackQuantity
FROM DELETED d
WHERE dPackedStocks.Mix_Id = d.Mix_Id 
	AND dPackedStocks.Package_Id = d.Package_Id
GO


---- SELF MOUNTED TRIGGERS
-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackedStocks_Update_PackedStocks'))
DROP TRIGGER [dbo].PackedStocks_Update_PackedStocks
GO
CREATE TRIGGER PackedStocks_Update_PackedStocks
ON CoffeeSale_Details
AFTER UPDATE AS
IF (SELECT i.PackQuantity FROM INSERTED i) = 0
DELETE FROM dPackedStocks
WHERE Id = (SELECT i.Id FROM INSERTED i)
GO


------ dPackageStocks
---- Packages
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Insert_Package'))
DROP TRIGGER [dbo].PackageStocks_Insert_Package
GO
CREATE TRIGGER PackageStocks_Insert_Package
ON Packages
AFTER INSERT AS
INSERT INTO dPackageStocks(Package_Id, Quantity)
SELECT i.Id, 0
FROM INSERTED i
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Delete_Package'))
DROP TRIGGER [dbo].PackageStocks_Delete_Package
GO
--CREATE TRIGGER PackageStocks_Delete_Package
--ON Packages
--AFTER DELETE AS
--DELETE FROM dPackageStocks
--WHERE Package_Id = (SELECT d.Id 
--					FROM DELETED d)
--GO


---- PackagePurchases
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Insert_PackagePurchase'))
DROP TRIGGER [dbo].PackageStocks_Insert_PackagePurchase
GO
CREATE TRIGGER PackageStocks_Insert_PackagePurchase
ON PackagePurchases
AFTER INSERT AS
UPDATE dPackageStocks
SET Quantity += i.PackQuantity
FROM INSERTED i
WHERE dPackageStocks.Package_Id = i.Package_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Update_PackagePurchase'))
DROP TRIGGER [dbo].PackageStocks_Update_PackagePurchase
GO
CREATE TRIGGER PackageStocks_Update_PackagePurchase
ON PackagePurchases
AFTER UPDATE AS
UPDATE dPackageStocks
SET Quantity += (i.PackQuantity - d.PackQuantity)
FROM INSERTED i, DELETED d
WHERE dPackageStocks.Package_Id = i.Package_Id
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Delete_PackagePurchase'))
DROP TRIGGER [dbo].PackageStocks_Delete_PackagePurchase
GO
CREATE TRIGGER PackageStocks_Delete_PackagePurchase
ON PackagePurchases
AFTER DELETE AS
UPDATE dPackageStocks
SET Quantity -= d.PackQuantity
FROM DELETED d
WHERE dPackageStocks.Package_Id = d.Package_Id
GO


---- Packings
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Insert_Packing'))
DROP TRIGGER [dbo].PackageStocks_Insert_Packing
GO
CREATE TRIGGER PackageStocks_Insert_Packing
ON Packings
AFTER INSERT AS
UPDATE dPackageStocks
SET Quantity -= i.PackQuantity
FROM INSERTED i
WHERE dPackageStocks.Id = i.Package_Id
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Update_Packing'))
DROP TRIGGER [dbo].PackageStocks_Update_Packing
GO
CREATE TRIGGER PackageStocks_Update_Packing
ON Packings
AFTER UPDATE AS
UPDATE dPackageStocks
SET Quantity -= (i.PackQuantity - d.PackQuantity)
FROM INSERTED i, DELETED d
WHERE dPackageStocks.Id = i.Package_Id
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].PackageStocks_Delete_Packing'))
DROP TRIGGER [dbo].PackageStocks_Delete_Packing
GO
CREATE TRIGGER PackageStocks_Delete_Packing
ON Packings
AFTER DELETE AS
UPDATE dPackageStocks
SET Quantity += i.PackQuantity
FROM INSERTED i
WHERE dPackageStocks.Id = i.Package_Id
GO



------ dAccountsBalance & dTransactions
---- Accounts
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_Account'))
DROP TRIGGER [dbo].AccountsBalances_Insert_Account
GO
CREATE TRIGGER AccountsBalances_Insert_Account
ON Accounts
AFTER INSERT AS
INSERT INTO dAccountsBalances(Account_Id, Balance)
SELECT i.Id, 0
FROM INSERTED i
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_Account'))
DROP TRIGGER [dbo].AccountsBalances_Delete_Account
GO
--CREATE TRIGGER AccountsBalances_Delete_Account
--ON Accounts
--AFTER DELETE AS
--DELETE FROM dAccountsBalances
--WHERE Account_Id = (SELECT d.Id 
--					   FROM DELETED d)
--GO


---- CoffeeSale
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_CoffeeSale'))
DROP TRIGGER [dbo].AccountsBalances_Insert_CoffeeSale
GO
CREATE TRIGGER AccountsBalances_Insert_CoffeeSale
ON CoffeeSales
AFTER INSERT AS	
-- If it wasn't paid no actions on account's balance and transactions will be made
IF (SELECT i.Paid FROM INSERTED i) = 1
BEGIN
	BEGIN
		UPDATE dAccountsBalances
		SET Balance += i.Sum
		FROM INSERTED i
		WHERE dAccountsBalances.Id = i.Account_Id
	END
	BEGIN
		INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
		SELECT i.Account_Id, i.Date, i.Sum, rc.Name,'Продажа кофе'
		FROM INSERTED i
		INNER JOIN dbo.Recipients rc
			ON rc.Id = i.Recipient_Id
	END
	BEGIN
		UPDATE CoffeeSales
		SET TransactionId = (SELECT MAX(Id)
								FROM dTransactions)
		FROM INSERTED i
		WHERE CoffeeSales.Id = i.Id
	END
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_CoffeeSale'))
DROP TRIGGER [dbo].AccountsBalances_Update_CoffeeSale
GO
CREATE TRIGGER AccountsBalances_Update_CoffeeSale
ON CoffeeSales
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
			SET Balance += (i.Sum - d.Sum)
			FROM INSERTED i, DELETED d
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		-- Correct transaction's sum
		BEGIN
			UPDATE dTransactions
			SET Sum = i.Sum
			FROM INSERTED i
			WHERE dTransactions.Id = i.TransactionId
		END
	END
	-- If it was paid before BUT now it's not
	ELSE
	BEGIN
		-- Correct account's balance
		BEGIN
			UPDATE dAccountsBalances
			SET Balance += d.Sum
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
			SET Balance -= i.Sum
			FROM INSERTED i
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		BEGIN
			INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
			SELECT i.Account_Id, i.Date, i.Sum, rc.Name,'Продажа кофе'
			FROM INSERTED i
			INNER JOIN dbo.Recipients rc
				ON rc.Id = i.Recipient_Id
		END
		BEGIN
			UPDATE CoffeeSales
			SET TransactionId = (SELECT MAX(Id)
									FROM dTransactions)
			FROM INSERTED i
			WHERE CoffeeSales.Id = i.Id
		END
	END
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_CoffeeSale'))
DROP TRIGGER [dbo].AccountsBalances_Delete_CoffeeSale
GO
CREATE TRIGGER AccountsBalances_Delete_CoffeeSale
ON CoffeeSales
AFTER DELETE AS
-- If it wasn't paid no actions needed
IF (SELECT d.Paid FROM DELETED d) = 1
BEGIN
	BEGIN
		UPDATE dAccountsBalances
		SET Balance -= d.Sum
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


---- CoffeePurchase
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
		SET Balance -= i.Sum
		FROM INSERTED i
		WHERE dAccountsBalances.Id = i.Account_Id
	END
	BEGIN
		INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
		SELECT i.Account_Id, i.Date, i.Sum, rc.Name, N'Закупка кофе'
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
			SET Balance -= (i.Sum - d.Sum)
			FROM INSERTED i, DELETED d
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		-- Correct transaction's sum
		BEGIN
			UPDATE dTransactions
			SET Sum = i.Sum
			FROM INSERTED i
			WHERE dTransactions.Id = i.TransactionId
		END
	END
		-- If it was paid before BUT now it's not
	ELSE
	BEGIN
		-- Correct account's balance
		BEGIN
			UPDATE dAccountsBalances
			SET Balance -= d.Sum
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
			SET Balance += i.Sum
			FROM INSERTED i
			WHERE dAccountsBalances.Id = i.Account_Id
		END
		BEGIN
			INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
			SELECT i.Account_Id, i.Date, i.Sum, rc.Name, N'Продажа кофе'
			FROM INSERTED i
			INNER JOIN dbo.Recipients rc
				ON rc.Id = i.Recipient_Id
		END
		BEGIN
			UPDATE CoffeeSales
			SET TransactionId = (SELECT MAX(Id)
									FROM dTransactions)
			FROM INSERTED i
			WHERE CoffeeSales.Id = i.Id
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
		SET Balance += d.Sum
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


---- PackagePurchases
 -- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_PackagePurchases'))
DROP TRIGGER [dbo].AccountsBalances_Insert_PackagePurchases
GO
CREATE TRIGGER AccountsBalances_Insert_PackagePurchases
ON PackagePurchases
AFTER INSERT AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= i.Sum
	FROM INSERTED i
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
	SELECT i.Account_Id, i.Date, i.Sum, rc.Name,'Закупка упаковки'
	FROM INSERTED i
	INNER JOIN dbo.Suppliers rc
		ON rc.Id = i.Supplier_Id
END
BEGIN
	UPDATE PackagePurchases
	SET TransactionId = (SELECT MAX(Id)
						 FROM dTransactions)
	FROM INSERTED i
	WHERE PackagePurchases.Id = i.Id
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_PackagePurchases'))
DROP TRIGGER [dbo].AccountsBalances_Update_PackagePurchases
GO
CREATE TRIGGER AccountsBalances_Update_PackagePurchases
ON PackagePurchases
AFTER UPDATE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= (i.Sum - d.Sum)
	FROM INSERTED i, DELETED d
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	UPDATE dTransactions
	SET Sum = i.Sum
	FROM INSERTED i
	WHERE dTransactions.Id = i.TransactionId
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_PackagePurchases'))
DROP TRIGGER [dbo].AccountsBalances_Delete_PackagePurchases
GO
CREATE TRIGGER AccountsBalances_Delete_PackagePurchases
ON PackagePurchases
AFTER DELETE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance += d.Sum
	FROM DELETED d
	WHERE dAccountsBalances.Id = d.Account_Id
END
BEGIN
	DELETE FROM dTransactions
	WHERE Id = (SELECT d.TransactionId
				FROM DELETED d)
END
GO


---- Payments
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_Payments'))
DROP TRIGGER [dbo].AccountsBalances_Insert_Payments
GO
CREATE TRIGGER AccountsBalances_Insert_Payments
ON Payments
AFTER INSERT AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= i.Sum
	FROM INSERTED i
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
	SELECT i.Account_Id, i.Date, i.Sum, i.Designation,'Платёж'
	FROM INSERTED i
END
BEGIN
	UPDATE PackagePurchases
	SET TransactionId = (SELECT MAX(Id)
						 FROM dTransactions)
	FROM INSERTED i
	WHERE PackagePurchases.Id = i.Id
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_Payments'))
DROP TRIGGER [dbo].AccountsBalances_Update_Payments
GO
CREATE TRIGGER AccountsBalances_Update_Payments
ON Payments
AFTER UPDATE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= (i.Sum - d.Sum)
	FROM INSERTED i, DELETED d
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	UPDATE dTransactions
	SET Sum = i.Sum
	FROM INSERTED i
	WHERE dTransactions.Id = i.TransactionId
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_Payments'))
DROP TRIGGER [dbo].AccountsBalances_Delete_Payments
GO
CREATE TRIGGER AccountsBalances_Delete_Payments
ON Payments
AFTER DELETE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance += d.Sum
	FROM DELETED d
	WHERE dAccountsBalances.Id = d.Account_Id
END
BEGIN
	DELETE FROM dTransactions
	WHERE Id = (SELECT d.TransactionId
				FROM DELETED d)
END
GO


---- OtherProfits
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_OtherProfits'))
DROP TRIGGER [dbo].AccountsBalances_Insert_OtherProfits
GO
CREATE TRIGGER AccountsBalances_Insert_OtherProfits
ON OtherProfits
AFTER INSERT AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance += i.Sum
	FROM INSERTED i
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
	SELECT i.Account_Id, i.Date, i.Sum, i.Designation,'Поступление'
	FROM INSERTED i
END
BEGIN
	UPDATE PackagePurchases
	SET TransactionId = (SELECT MAX(Id)
						 FROM dTransactions)
	FROM INSERTED i
	WHERE PackagePurchases.Id = i.Id
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_OtherProfits'))
DROP TRIGGER [dbo].AccountsBalances_Update_OtherProfits
GO
CREATE TRIGGER AccountsBalances_Update_OtherProfits
ON OtherProfits
AFTER UPDATE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance += (i.Sum - d.Sum)
	FROM INSERTED i, DELETED d
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	UPDATE dTransactions
	SET Sum = i.Sum
	FROM INSERTED i
	WHERE dTransactions.Id = i.TransactionId
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_OtherProfits'))
DROP TRIGGER [dbo].AccountsBalances_Delete_OtherProfits
GO
CREATE TRIGGER AccountsBalances_Delete_OtherProfits
ON OtherProfits
AFTER DELETE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= d.Sum
	FROM DELETED d
	WHERE dAccountsBalances.Id = d.Account_Id
END
BEGIN
	DELETE FROM dTransactions
	WHERE Id = (SELECT d.TransactionId
				FROM DELETED d)
END
GO


---- MonthlyPayments
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_MonthlyPayments'))
DROP TRIGGER [dbo].AccountsBalances_Insert_MonthlyPayments
GO
CREATE TRIGGER AccountsBalances_Insert_MonthlyPayments
ON MonthlyPayments
AFTER INSERT AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= i.PaidAmount
	FROM INSERTED i
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	INSERT INTO dTransactions(Account_Id, Date, Sum, Participant, Description)
	SELECT i.Account_Id, i.Date, i.PaidAmount, me.Designation,'Ежемесячный платёж'
	FROM INSERTED i
	INNER JOIN dbo.MonthlyExpenses me
		ON me.Id = i.MonthlyExpense_Id
END
BEGIN
	UPDATE PackagePurchases
	SET TransactionId = (SELECT MAX(Id)
						 FROM dTransactions)
	FROM INSERTED i
	WHERE PackagePurchases.Id = i.Id
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_MonthlyPayments'))
DROP TRIGGER [dbo].AccountsBalances_Update_MonthlyPayments
GO
CREATE TRIGGER AccountsBalances_Update_MonthlyPayments
ON MonthlyPayments
AFTER UPDATE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= (i.PaidAmount - d.PaidAmount)
	FROM INSERTED i, DELETED d
	WHERE dAccountsBalances.Id = i.Account_Id
END
BEGIN
	UPDATE dTransactions
	SET Sum = i.PaidAmount
	FROM INSERTED i
	WHERE dTransactions.Id = i.TransactionId
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_MonthlyPayments'))
DROP TRIGGER [dbo].AccountsBalances_Delete_MonthlyPayments
GO
CREATE TRIGGER AccountsBalances_Delete_MonthlyPayments
ON MonthlyPayments
AFTER DELETE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance += d.PaidAmount
	FROM DELETED d
	WHERE dAccountsBalances.Id = d.Account_Id
END
BEGIN
	DELETE FROM dTransactions
	WHERE Id = (SELECT d.TransactionId
				FROM DELETED d)
END
GO


---- MoneyTransfers
-- on INSERT
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Insert_MoneyTransfers'))
DROP TRIGGER [dbo].AccountsBalances_Insert_MoneyTransfers
GO
CREATE TRIGGER AccountsBalances_Insert_MoneyTransfers
ON MoneyTransfers
AFTER INSERT AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= i.Sum
	FROM INSERTED i
	WHERE dAccountsBalances.Id = i.InitialAccount_Id
END
BEGIN
	UPDATE dAccountsBalances
	SET Balance += i.Sum
	FROM INSERTED i
	WHERE dAccountsBalances.Id = i.TargetAccount_Id
END
GO

-- on UPDATE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Update_MoneyTransfers'))
DROP TRIGGER [dbo].AccountsBalances_Update_MoneyTransfers
GO
CREATE TRIGGER AccountsBalances_Update_MoneyTransfers
ON MoneyTransfers
AFTER UPDATE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= (i.Sum - d.Sum)
	FROM INSERTED i, DELETED d
	WHERE dAccountsBalances.Id = i.InitialAccount_Id
END
BEGIN
	UPDATE dAccountsBalances
	SET Balance += (i.Sum - d.Sum)
	FROM INSERTED i, DELETED d
	WHERE dAccountsBalances.Id = i.TargetAccount_Id
END
GO

-- on DELETE
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].AccountsBalances_Delete_MoneyTransfers'))
DROP TRIGGER [dbo].AccountsBalances_Delete_MoneyTransfers
GO
CREATE TRIGGER AccountsBalances_Delete_MoneyTransfers
ON MoneyTransfers
AFTER DELETE AS
BEGIN
	UPDATE dAccountsBalances
	SET Balance += d.Sum
	FROM DELETED d
	WHERE dAccountsBalances.Id = d.InitialAccount_Id
END
BEGIN
	UPDATE dAccountsBalances
	SET Balance -= d.Sum
	FROM DELETED d
	WHERE dAccountsBalances.Id = d.TargetAccount_Id
END
GO
