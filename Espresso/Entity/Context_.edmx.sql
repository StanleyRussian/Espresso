
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/04/2016 22:54:52
-- Generated from EDMX file: D:\Projects\CoffeeBook\CoffeeBook_Model\Entity\Context.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CoffeeBook_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SupplierCoffeePurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeePurchases] DROP CONSTRAINT [FK_SupplierCoffeePurchase];
GO
IF OBJECT_ID(N'[dbo].[FK_CoffeePurchase_DetailsCoffeePurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeePurchase_Details] DROP CONSTRAINT [FK_CoffeePurchase_DetailsCoffeePurchase];
GO
IF OBJECT_ID(N'[dbo].[FK_CoffeeSortCoffeePurchase_Details]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeePurchase_Details] DROP CONSTRAINT [FK_CoffeeSortCoffeePurchase_Details];
GO
IF OBJECT_ID(N'[dbo].[FK_CoffeeSortdGreenStocks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dGreenStocks] DROP CONSTRAINT [FK_CoffeeSortdGreenStocks];
GO
IF OBJECT_ID(N'[dbo].[FK_RoastingCoffeeSort]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roastings] DROP CONSTRAINT [FK_RoastingCoffeeSort];
GO
IF OBJECT_ID(N'[dbo].[FK_dRoastedStockCoffeeSort]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dRoastedStocks] DROP CONSTRAINT [FK_dRoastedStockCoffeeSort];
GO
IF OBJECT_ID(N'[dbo].[FK_Mix_DetailsMix]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mix_Details] DROP CONSTRAINT [FK_Mix_DetailsMix];
GO
IF OBJECT_ID(N'[dbo].[FK_Mix_DetailsCoffeeSort]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mix_Details] DROP CONSTRAINT [FK_Mix_DetailsCoffeeSort];
GO
IF OBJECT_ID(N'[dbo].[FK_PackingMix]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packings] DROP CONSTRAINT [FK_PackingMix];
GO
IF OBJECT_ID(N'[dbo].[FK_PackingPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packings] DROP CONSTRAINT [FK_PackingPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_PackagePackagePurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PackagePurchases] DROP CONSTRAINT [FK_PackagePackagePurchase];
GO
IF OBJECT_ID(N'[dbo].[FK_dPackedStocksMix]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dPackedStocks] DROP CONSTRAINT [FK_dPackedStocksMix];
GO
IF OBJECT_ID(N'[dbo].[FK_PackagedPackedStocks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dPackedStocks] DROP CONSTRAINT [FK_PackagedPackedStocks];
GO
IF OBJECT_ID(N'[dbo].[FK_dPackageStocksPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dPackageStocks] DROP CONSTRAINT [FK_dPackageStocksPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_SaleSale_Details]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeeSale_Details] DROP CONSTRAINT [FK_SaleSale_Details];
GO
IF OBJECT_ID(N'[dbo].[FK_RecipientSale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeeSales] DROP CONSTRAINT [FK_RecipientSale];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountSale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeeSales] DROP CONSTRAINT [FK_AccountSale];
GO
IF OBJECT_ID(N'[dbo].[FK_MixSale_Details]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeeSale_Details] DROP CONSTRAINT [FK_MixSale_Details];
GO
IF OBJECT_ID(N'[dbo].[FK_PackageSale_Details]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeeSale_Details] DROP CONSTRAINT [FK_PackageSale_Details];
GO
IF OBJECT_ID(N'[dbo].[FK_PackedCategoryPacking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packings] DROP CONSTRAINT [FK_PackedCategoryPacking];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountCoffeePurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeePurchases] DROP CONSTRAINT [FK_AccountCoffeePurchase];
GO
IF OBJECT_ID(N'[dbo].[FK_MixCoffeeTransfer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CoffeeTransfers] DROP CONSTRAINT [FK_MixCoffeeTransfer];
GO
IF OBJECT_ID(N'[dbo].[FK_MixdCafeCoffeeStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[dCafeCoffeeStocks] DROP CONSTRAINT [FK_MixdCafeCoffeeStock];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountPackagePurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PackagePurchases] DROP CONSTRAINT [FK_AccountPackagePurchase];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierPackagePurchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PackagePurchases] DROP CONSTRAINT [FK_SupplierPackagePurchase];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CoffeePurchases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CoffeePurchases];
GO
IF OBJECT_ID(N'[dbo].[Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suppliers];
GO
IF OBJECT_ID(N'[dbo].[CoffeePurchase_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CoffeePurchase_Details];
GO
IF OBJECT_ID(N'[dbo].[CoffeeSorts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CoffeeSorts];
GO
IF OBJECT_ID(N'[dbo].[dGreenStocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dGreenStocks];
GO
IF OBJECT_ID(N'[dbo].[Roastings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roastings];
GO
IF OBJECT_ID(N'[dbo].[dRoastedStocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dRoastedStocks];
GO
IF OBJECT_ID(N'[dbo].[Mixes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mixes];
GO
IF OBJECT_ID(N'[dbo].[Mix_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mix_Details];
GO
IF OBJECT_ID(N'[dbo].[Packings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Packings];
GO
IF OBJECT_ID(N'[dbo].[Packages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Packages];
GO
IF OBJECT_ID(N'[dbo].[PackagePurchases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PackagePurchases];
GO
IF OBJECT_ID(N'[dbo].[dPackedStocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dPackedStocks];
GO
IF OBJECT_ID(N'[dbo].[dPackageStocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dPackageStocks];
GO
IF OBJECT_ID(N'[dbo].[CoffeeSales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CoffeeSales];
GO
IF OBJECT_ID(N'[dbo].[Recipients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recipients];
GO
IF OBJECT_ID(N'[dbo].[CoffeeSale_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CoffeeSale_Details];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[PackedCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PackedCategories];
GO
IF OBJECT_ID(N'[dbo].[CoffeeTransfers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CoffeeTransfers];
GO
IF OBJECT_ID(N'[dbo].[dCafeCoffeeStocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dCafeCoffeeStocks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CoffeePurchases'
CREATE TABLE [dbo].[CoffeePurchases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [Paid] bit  NOT NULL,
    [Supplier_Id] int  NOT NULL,
    [Account_Id] int  NOT NULL
);
GO

-- Creating table 'Suppliers'
CREATE TABLE [dbo].[Suppliers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Contact] nvarchar(max)  NULL,
    [ContactPerson] nvarchar(max)  NULL,
    [Adress] nvarchar(max)  NULL,
    [Notes] nvarchar(max)  NULL
);
GO

-- Creating table 'CoffeePurchase_Details'
CREATE TABLE [dbo].[CoffeePurchase_Details] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Price] int  NOT NULL,
    [CoffeePurchases_Id] int  NOT NULL,
    [CoffeeSort_Id] int  NOT NULL
);
GO

-- Creating table 'CoffeeSorts'
CREATE TABLE [dbo].[CoffeeSorts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [LastPrice] int  NULL
);
GO

-- Creating table 'dGreenStocks'
CREATE TABLE [dbo].[dGreenStocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [CoffeeSort_Id] int  NOT NULL
);
GO

-- Creating table 'Roastings'
CREATE TABLE [dbo].[Roastings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [InitialAmount] int  NOT NULL,
    [RoastedAmount] int  NOT NULL,
    [CoffeeSort_Id] int  NOT NULL
);
GO

-- Creating table 'dRoastedStocks'
CREATE TABLE [dbo].[dRoastedStocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [CoffeeSort_Id] int  NOT NULL
);
GO

-- Creating table 'Mixes'
CREATE TABLE [dbo].[Mixes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Mix_Details'
CREATE TABLE [dbo].[Mix_Details] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ratio] int  NOT NULL,
    [Mix_Id] int  NOT NULL,
    [CoffeeSort_Id] int  NOT NULL
);
GO

-- Creating table 'Packings'
CREATE TABLE [dbo].[Packings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [PackQuantity] int  NOT NULL,
    [Mixes_Id] int  NOT NULL,
    [Packages_Id] int  NOT NULL,
    [PackedCategory_Id] int  NOT NULL
);
GO

-- Creating table 'Packages'
CREATE TABLE [dbo].[Packages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Capacity] int  NOT NULL,
    [Price] int  NOT NULL
);
GO

-- Creating table 'PackagePurchases'
CREATE TABLE [dbo].[PackagePurchases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [PackQuantity] int  NOT NULL,
    [Package_Id] int  NOT NULL,
    [Account_Id] int  NOT NULL,
    [Supplier_Id] int  NOT NULL
);
GO

-- Creating table 'dPackedStocks'
CREATE TABLE [dbo].[dPackedStocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PackQuantity] int  NOT NULL,
    [Mixes_Id] int  NOT NULL,
    [Package_Id] int  NOT NULL
);
GO

-- Creating table 'dPackageStocks'
CREATE TABLE [dbo].[dPackageStocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] nvarchar(max)  NOT NULL,
    [Packages_Id] int  NOT NULL
);
GO

-- Creating table 'CoffeeSales'
CREATE TABLE [dbo].[CoffeeSales] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [Paid] bit  NOT NULL,
    [Recipient_Id] int  NOT NULL,
    [Account_Id] int  NOT NULL
);
GO

-- Creating table 'Recipients'
CREATE TABLE [dbo].[Recipients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Contact] nvarchar(max)  NOT NULL,
    [ContactPerson] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CoffeeSale_Details'
CREATE TABLE [dbo].[CoffeeSale_Details] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Price] int  NOT NULL,
    [PackQuantity] int  NOT NULL,
    [Sale_Id] int  NOT NULL,
    [Mix_Id] int  NOT NULL,
    [Package_Id] int  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [BankNumber] nvarchar(max)  NULL
);
GO

-- Creating table 'PackedCategories'
CREATE TABLE [dbo].[PackedCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CoffeeTransfers'
CREATE TABLE [dbo].[CoffeeTransfers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Quantity] int  NOT NULL,
    [Mix_Id] int  NOT NULL
);
GO

-- Creating table 'dCafeCoffeeStocks'
CREATE TABLE [dbo].[dCafeCoffeeStocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Mix_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CoffeePurchases'
ALTER TABLE [dbo].[CoffeePurchases]
ADD CONSTRAINT [PK_CoffeePurchases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CoffeePurchase_Details'
ALTER TABLE [dbo].[CoffeePurchase_Details]
ADD CONSTRAINT [PK_CoffeePurchase_Details]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CoffeeSorts'
ALTER TABLE [dbo].[CoffeeSorts]
ADD CONSTRAINT [PK_CoffeeSorts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'dGreenStocks'
ALTER TABLE [dbo].[dGreenStocks]
ADD CONSTRAINT [PK_dGreenStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roastings'
ALTER TABLE [dbo].[Roastings]
ADD CONSTRAINT [PK_Roastings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'dRoastedStocks'
ALTER TABLE [dbo].[dRoastedStocks]
ADD CONSTRAINT [PK_dRoastedStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mixes'
ALTER TABLE [dbo].[Mixes]
ADD CONSTRAINT [PK_Mixes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mix_Details'
ALTER TABLE [dbo].[Mix_Details]
ADD CONSTRAINT [PK_Mix_Details]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Packings'
ALTER TABLE [dbo].[Packings]
ADD CONSTRAINT [PK_Packings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [PK_Packages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PackagePurchases'
ALTER TABLE [dbo].[PackagePurchases]
ADD CONSTRAINT [PK_PackagePurchases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'dPackedStocks'
ALTER TABLE [dbo].[dPackedStocks]
ADD CONSTRAINT [PK_dPackedStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'dPackageStocks'
ALTER TABLE [dbo].[dPackageStocks]
ADD CONSTRAINT [PK_dPackageStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CoffeeSales'
ALTER TABLE [dbo].[CoffeeSales]
ADD CONSTRAINT [PK_CoffeeSales]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Recipients'
ALTER TABLE [dbo].[Recipients]
ADD CONSTRAINT [PK_Recipients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CoffeeSale_Details'
ALTER TABLE [dbo].[CoffeeSale_Details]
ADD CONSTRAINT [PK_CoffeeSale_Details]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PackedCategories'
ALTER TABLE [dbo].[PackedCategories]
ADD CONSTRAINT [PK_PackedCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CoffeeTransfers'
ALTER TABLE [dbo].[CoffeeTransfers]
ADD CONSTRAINT [PK_CoffeeTransfers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'dCafeCoffeeStocks'
ALTER TABLE [dbo].[dCafeCoffeeStocks]
ADD CONSTRAINT [PK_dCafeCoffeeStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Supplier_Id] in table 'CoffeePurchases'
ALTER TABLE [dbo].[CoffeePurchases]
ADD CONSTRAINT [FK_SupplierCoffeePurchase]
    FOREIGN KEY ([Supplier_Id])
    REFERENCES [dbo].[Suppliers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierCoffeePurchase'
CREATE INDEX [IX_FK_SupplierCoffeePurchase]
ON [dbo].[CoffeePurchases]
    ([Supplier_Id]);
GO

-- Creating foreign key on [CoffeePurchases_Id] in table 'CoffeePurchase_Details'
ALTER TABLE [dbo].[CoffeePurchase_Details]
ADD CONSTRAINT [FK_CoffeePurchase_DetailsCoffeePurchase]
    FOREIGN KEY ([CoffeePurchases_Id])
    REFERENCES [dbo].[CoffeePurchases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CoffeePurchase_DetailsCoffeePurchase'
CREATE INDEX [IX_FK_CoffeePurchase_DetailsCoffeePurchase]
ON [dbo].[CoffeePurchase_Details]
    ([CoffeePurchases_Id]);
GO

-- Creating foreign key on [CoffeeSort_Id] in table 'CoffeePurchase_Details'
ALTER TABLE [dbo].[CoffeePurchase_Details]
ADD CONSTRAINT [FK_CoffeeSortCoffeePurchase_Details]
    FOREIGN KEY ([CoffeeSort_Id])
    REFERENCES [dbo].[CoffeeSorts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CoffeeSortCoffeePurchase_Details'
CREATE INDEX [IX_FK_CoffeeSortCoffeePurchase_Details]
ON [dbo].[CoffeePurchase_Details]
    ([CoffeeSort_Id]);
GO

-- Creating foreign key on [CoffeeSort_Id] in table 'dGreenStocks'
ALTER TABLE [dbo].[dGreenStocks]
ADD CONSTRAINT [FK_CoffeeSortdGreenStocks]
    FOREIGN KEY ([CoffeeSort_Id])
    REFERENCES [dbo].[CoffeeSorts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CoffeeSortdGreenStocks'
CREATE INDEX [IX_FK_CoffeeSortdGreenStocks]
ON [dbo].[dGreenStocks]
    ([CoffeeSort_Id]);
GO

-- Creating foreign key on [CoffeeSort_Id] in table 'Roastings'
ALTER TABLE [dbo].[Roastings]
ADD CONSTRAINT [FK_RoastingCoffeeSort]
    FOREIGN KEY ([CoffeeSort_Id])
    REFERENCES [dbo].[CoffeeSorts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoastingCoffeeSort'
CREATE INDEX [IX_FK_RoastingCoffeeSort]
ON [dbo].[Roastings]
    ([CoffeeSort_Id]);
GO

-- Creating foreign key on [CoffeeSort_Id] in table 'dRoastedStocks'
ALTER TABLE [dbo].[dRoastedStocks]
ADD CONSTRAINT [FK_dRoastedStockCoffeeSort]
    FOREIGN KEY ([CoffeeSort_Id])
    REFERENCES [dbo].[CoffeeSorts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dRoastedStockCoffeeSort'
CREATE INDEX [IX_FK_dRoastedStockCoffeeSort]
ON [dbo].[dRoastedStocks]
    ([CoffeeSort_Id]);
GO

-- Creating foreign key on [Mix_Id] in table 'Mix_Details'
ALTER TABLE [dbo].[Mix_Details]
ADD CONSTRAINT [FK_Mix_DetailsMix]
    FOREIGN KEY ([Mix_Id])
    REFERENCES [dbo].[Mixes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Mix_DetailsMix'
CREATE INDEX [IX_FK_Mix_DetailsMix]
ON [dbo].[Mix_Details]
    ([Mix_Id]);
GO

-- Creating foreign key on [CoffeeSort_Id] in table 'Mix_Details'
ALTER TABLE [dbo].[Mix_Details]
ADD CONSTRAINT [FK_Mix_DetailsCoffeeSort]
    FOREIGN KEY ([CoffeeSort_Id])
    REFERENCES [dbo].[CoffeeSorts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Mix_DetailsCoffeeSort'
CREATE INDEX [IX_FK_Mix_DetailsCoffeeSort]
ON [dbo].[Mix_Details]
    ([CoffeeSort_Id]);
GO

-- Creating foreign key on [Mixes_Id] in table 'Packings'
ALTER TABLE [dbo].[Packings]
ADD CONSTRAINT [FK_PackingMix]
    FOREIGN KEY ([Mixes_Id])
    REFERENCES [dbo].[Mixes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackingMix'
CREATE INDEX [IX_FK_PackingMix]
ON [dbo].[Packings]
    ([Mixes_Id]);
GO

-- Creating foreign key on [Packages_Id] in table 'Packings'
ALTER TABLE [dbo].[Packings]
ADD CONSTRAINT [FK_PackingPackage]
    FOREIGN KEY ([Packages_Id])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackingPackage'
CREATE INDEX [IX_FK_PackingPackage]
ON [dbo].[Packings]
    ([Packages_Id]);
GO

-- Creating foreign key on [Package_Id] in table 'PackagePurchases'
ALTER TABLE [dbo].[PackagePurchases]
ADD CONSTRAINT [FK_PackagePackagePurchase]
    FOREIGN KEY ([Package_Id])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackagePackagePurchase'
CREATE INDEX [IX_FK_PackagePackagePurchase]
ON [dbo].[PackagePurchases]
    ([Package_Id]);
GO

-- Creating foreign key on [Mixes_Id] in table 'dPackedStocks'
ALTER TABLE [dbo].[dPackedStocks]
ADD CONSTRAINT [FK_dPackedStocksMix]
    FOREIGN KEY ([Mixes_Id])
    REFERENCES [dbo].[Mixes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dPackedStocksMix'
CREATE INDEX [IX_FK_dPackedStocksMix]
ON [dbo].[dPackedStocks]
    ([Mixes_Id]);
GO

-- Creating foreign key on [Package_Id] in table 'dPackedStocks'
ALTER TABLE [dbo].[dPackedStocks]
ADD CONSTRAINT [FK_PackagedPackedStocks]
    FOREIGN KEY ([Package_Id])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackagedPackedStocks'
CREATE INDEX [IX_FK_PackagedPackedStocks]
ON [dbo].[dPackedStocks]
    ([Package_Id]);
GO

-- Creating foreign key on [Packages_Id] in table 'dPackageStocks'
ALTER TABLE [dbo].[dPackageStocks]
ADD CONSTRAINT [FK_dPackageStocksPackage]
    FOREIGN KEY ([Packages_Id])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dPackageStocksPackage'
CREATE INDEX [IX_FK_dPackageStocksPackage]
ON [dbo].[dPackageStocks]
    ([Packages_Id]);
GO

-- Creating foreign key on [Sale_Id] in table 'CoffeeSale_Details'
ALTER TABLE [dbo].[CoffeeSale_Details]
ADD CONSTRAINT [FK_SaleSale_Details]
    FOREIGN KEY ([Sale_Id])
    REFERENCES [dbo].[CoffeeSales]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SaleSale_Details'
CREATE INDEX [IX_FK_SaleSale_Details]
ON [dbo].[CoffeeSale_Details]
    ([Sale_Id]);
GO

-- Creating foreign key on [Recipient_Id] in table 'CoffeeSales'
ALTER TABLE [dbo].[CoffeeSales]
ADD CONSTRAINT [FK_RecipientSale]
    FOREIGN KEY ([Recipient_Id])
    REFERENCES [dbo].[Recipients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecipientSale'
CREATE INDEX [IX_FK_RecipientSale]
ON [dbo].[CoffeeSales]
    ([Recipient_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'CoffeeSales'
ALTER TABLE [dbo].[CoffeeSales]
ADD CONSTRAINT [FK_AccountSale]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountSale'
CREATE INDEX [IX_FK_AccountSale]
ON [dbo].[CoffeeSales]
    ([Account_Id]);
GO

-- Creating foreign key on [Mix_Id] in table 'CoffeeSale_Details'
ALTER TABLE [dbo].[CoffeeSale_Details]
ADD CONSTRAINT [FK_MixSale_Details]
    FOREIGN KEY ([Mix_Id])
    REFERENCES [dbo].[Mixes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MixSale_Details'
CREATE INDEX [IX_FK_MixSale_Details]
ON [dbo].[CoffeeSale_Details]
    ([Mix_Id]);
GO

-- Creating foreign key on [Package_Id] in table 'CoffeeSale_Details'
ALTER TABLE [dbo].[CoffeeSale_Details]
ADD CONSTRAINT [FK_PackageSale_Details]
    FOREIGN KEY ([Package_Id])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackageSale_Details'
CREATE INDEX [IX_FK_PackageSale_Details]
ON [dbo].[CoffeeSale_Details]
    ([Package_Id]);
GO

-- Creating foreign key on [PackedCategory_Id] in table 'Packings'
ALTER TABLE [dbo].[Packings]
ADD CONSTRAINT [FK_PackedCategoryPacking]
    FOREIGN KEY ([PackedCategory_Id])
    REFERENCES [dbo].[PackedCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackedCategoryPacking'
CREATE INDEX [IX_FK_PackedCategoryPacking]
ON [dbo].[Packings]
    ([PackedCategory_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'CoffeePurchases'
ALTER TABLE [dbo].[CoffeePurchases]
ADD CONSTRAINT [FK_AccountCoffeePurchase]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountCoffeePurchase'
CREATE INDEX [IX_FK_AccountCoffeePurchase]
ON [dbo].[CoffeePurchases]
    ([Account_Id]);
GO

-- Creating foreign key on [Mix_Id] in table 'CoffeeTransfers'
ALTER TABLE [dbo].[CoffeeTransfers]
ADD CONSTRAINT [FK_MixCoffeeTransfer]
    FOREIGN KEY ([Mix_Id])
    REFERENCES [dbo].[Mixes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MixCoffeeTransfer'
CREATE INDEX [IX_FK_MixCoffeeTransfer]
ON [dbo].[CoffeeTransfers]
    ([Mix_Id]);
GO

-- Creating foreign key on [Mix_Id] in table 'dCafeCoffeeStocks'
ALTER TABLE [dbo].[dCafeCoffeeStocks]
ADD CONSTRAINT [FK_MixdCafeCoffeeStock]
    FOREIGN KEY ([Mix_Id])
    REFERENCES [dbo].[Mixes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MixdCafeCoffeeStock'
CREATE INDEX [IX_FK_MixdCafeCoffeeStock]
ON [dbo].[dCafeCoffeeStocks]
    ([Mix_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'PackagePurchases'
ALTER TABLE [dbo].[PackagePurchases]
ADD CONSTRAINT [FK_AccountPackagePurchase]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountPackagePurchase'
CREATE INDEX [IX_FK_AccountPackagePurchase]
ON [dbo].[PackagePurchases]
    ([Account_Id]);
GO

-- Creating foreign key on [Supplier_Id] in table 'PackagePurchases'
ALTER TABLE [dbo].[PackagePurchases]
ADD CONSTRAINT [FK_SupplierPackagePurchase]
    FOREIGN KEY ([Supplier_Id])
    REFERENCES [dbo].[Suppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierPackagePurchase'
CREATE INDEX [IX_FK_SupplierPackagePurchase]
ON [dbo].[PackagePurchases]
    ([Supplier_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------