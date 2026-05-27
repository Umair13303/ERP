USE [ERPOrganisationSetup]
GO
/****** Object:  FullTextCatalog [db46757]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE FULLTEXT CATALOG [db46757] AS DEFAULT
GO
/****** Object:  UserDefinedTableType [dbo].[AFBillProduct_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[AFBillProduct_TVP] AS TABLE(
	[Id] [int] NULL,
	[GuID] [uniqueidentifier] NULL,
	[BillId] [int] NULL,
	[ProductId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[Quantity] [decimal](18, 2) NULL,
	[ActualAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[ChargedAmount] [decimal](18, 2) NULL,
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[AFCustomerLedger_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[AFCustomerLedger_TVP] AS TABLE(
	[Id] [int] NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[CustomerId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[ReconcillationStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[AFInventoryLedger_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[AFInventoryLedger_TVP] AS TABLE(
	[GuID] [uniqueidentifier] NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[ProductId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[QuantityIn] [decimal](18, 4) NULL,
	[QuantityOut] [decimal](18, 4) NULL,
	[Debit] [decimal](18, 4) NULL,
	[Credit] [decimal](18, 4) NULL,
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[ReconcillationStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[AFInvoiceProduct_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[AFInvoiceProduct_TVP] AS TABLE(
	[Id] [int] NULL,
	[GuID] [uniqueidentifier] NULL,
	[InvoiceId] [int] NULL,
	[ProductPriceLogId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[ActualAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[ChargedAmount] [decimal](18, 2) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[AFJournalVoucher_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[AFJournalVoucher_TVP] AS TABLE(
	[Id] [int] NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[ChartOfAccountId] [int] NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[PostingStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[AFSupplierLedger_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[AFSupplierLedger_TVP] AS TABLE(
	[Id] [int] NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[SupplierId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Debit] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Credit] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[ReconcillationStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [int] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[IAdjustmentPPQD_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[IAdjustmentPPQD_TVP] AS TABLE(
	[GuID] [uniqueidentifier] NULL,
	[AdjustmentId] [int] NULL,
	[ProductId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[UnitPurchasePrice] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[UnitSalePrice] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[QuantityIn] [decimal](18, 0) NOT NULL DEFAULT ((0)),
	[QuantityOut] [decimal](18, 0) NOT NULL DEFAULT ((0)),
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[IProductStockSummary_Variant_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[IProductStockSummary_Variant_TVP] AS TABLE(
	[ProductId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[VariantKey] [nvarchar](500) NOT NULL,
	[VariantDisplay] [nvarchar](500) NULL,
	[CurrentQty] [decimal](18, 4) NULL,
	[CurrentValue] [decimal](18, 4) NULL,
	[WAC_UnitCost] [decimal](18, 4) NULL,
	[WAC_TotalValue] [decimal](18, 4) NULL,
	[FIFOBatchQueue] [nvarchar](max) NULL,
	[LIFOBatchStack] [nvarchar](max) NULL,
	[CompanyId] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ProductCombination_TVP]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[ProductCombination_TVP] AS TABLE(
	[ProductId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[Status] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ProductCombinationType]    Script Date: 5/27/2026 7:19:05 AM ******/
CREATE TYPE [dbo].[ProductCombinationType] AS TABLE(
	[ProductId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[Status] [bit] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculateLineTotal]    Script Date: 5/27/2026 7:19:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CalculateLineTotal]
(
    @ActualAmount DECIMAL(18,2),
    @DiscountAmount DECIMAL(18,2),
    @ProductId INT,
    @DocumentStatus INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Total DECIMAL(18,2);
    
    SELECT @Total = 
        CASE 
            WHEN ISNULL(PC.IsSaleTaxExclusive, 0) = 1 
            THEN (@ActualAmount - @DiscountAmount) * (1.0 + (ISNULL(PC.DefaultRate, 0) + ISNULL(PC.AdditionalRate, 0)) / 100.0)
            ELSE (@ActualAmount - @DiscountAmount) 
        END
    FROM [dbo].[retProductConfig](@ProductId, @DocumentStatus) AS PC;

    RETURN ISNULL(@Total, (@ActualAmount - @DiscountAmount));
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CodeFormat]    Script Date: 5/27/2026 7:19:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE FUNCTION [dbo].[fn_CodeFormat]  (   @MaxCode bigint     )  RETURNS nvarchar(max)  AS  BEGIN   DECLARE @Code nvarchar(max);    select  @Code=convert(nvarchar(max), getdate(), 12)+'-'+format(isnull(@MaxCode,0)+1,'0000')   RETURN  @Code;  END  
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CodeSplitByCommaAsINT]    Script Date: 5/27/2026 7:19:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[fn_CodeSplitByCommaAsINT]
(
 @psCSString NVARCHAR(max)
)
RETURNS @otTemp TABLE(sID INT)
AS
BEGIN
 DECLARE @sTemp INT

 WHILE LEN(@psCSString) > 0
 BEGIN
  SET @sTemp = LEFT(@psCSString, ISNULL(NULLIF(CHARINDEX(',', @psCSString) - 1, -1),
                    LEN(@psCSString)))
  SET @psCSString = SUBSTRING(@psCSString,ISNULL(NULLIF(CHARINDEX(',', @psCSString), 0),
                               LEN(@psCSString)) + 1, LEN(@psCSString))

  INSERT INTO @otTemp VALUES (@sTemp)
 END

RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_InvoiceStatus]    Script Date: 5/27/2026 7:19:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_InvoiceStatus]
(
    @NetAmount DECIMAL(18, 2),
    @DueAmount DECIMAL(18, 2)
)
RETURNS VARCHAR(20)
AS
BEGIN
    DECLARE @Status VARCHAR(20);

    SET @Status = CASE 
        WHEN @DueAmount <= 0 THEN 'Paid'
        WHEN @DueAmount > 0 AND @DueAmount < @NetAmount THEN 'Partial'
        WHEN @DueAmount >= @NetAmount THEN 'Unpaid'
        ELSE 'Review Balance' 
    END;

    RETURN @Status;
END;
GO
/****** Object:  Table [dbo].[IProduct]    Script Date: 5/27/2026 7:19:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[MachineNumber] [nvarchar](max) NULL,
	[SKU] [nvarchar](max) NULL,
	[AdditionalDetail] [nvarchar](max) NULL,
	[AttributeIds] [nvarchar](max) NULL,
	[BrandId] [int] NULL,
	[ProductTypeId] [int] NULL,
	[IsFavorite] [bit] NULL,
	[IsSaleTaxExclusive] [bit] NULL,
	[DepartmentId] [int] NULL,
	[SectionId] [int] NULL,
	[CategoryId] [int] NULL,
	[SubCategoryId] [int] NULL,
	[IsExpiryApplicable] [bit] NULL,
	[CriticalLimit] [decimal](18, 2) NOT NULL,
	[SaleUnitId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK__IProduct__3214EC074BA48951] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IProductATI]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IProductATI](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[ProductId] [int] NULL,
	[InventoryAccountId] [int] NULL,
	[SaleRevenueAccountId] [int] NULL,
	[CostOfSaleAccountId] [int] NULL,
	[ItemTypeId] [int] NULL,
	[HSCodeId] [int] NULL,
	[SaleTaxTypeId] [int] NULL,
	[CostingModeId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__IProduct__3214EC076FEE2B1F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vHSCode]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vHSCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[AdditionalDetail] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vItemType]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vItemType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vSaleTaxType]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vSaleTaxType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DefaultRate] [decimal](18, 2) NULL,
	[AdditionalRate] [decimal](18, 2) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[retProductConfig]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[retProductConfig]
(
    @ProductId INT, 
    @DocumentStatus INT
)
RETURNS TABLE 
AS
RETURN 
(
    SELECT 
        P.Description   AS ProductName,
        P.IsSaleTaxExclusive,
        IT.Description  AS ItemType,
        HS.Description  AS HSCode,
        STT.Description AS SaleTaxType,
        STT.DefaultRate,
        STT.AdditionalRate
    FROM IProduct AS P WITH(NOLOCK)
    INNER JOIN IProductATI AS ATI WITH(NOLOCK) ON ATI.PRODUCTID= P.ID
    INNER JOIN vItemType AS IT WITH(NOLOCK) ON ATI.ItemTypeId = IT.Id
    INNER JOIN vHSCode AS HS WITH(NOLOCK) ON ATI.HSCodeId = HS.Id
    INNER JOIN vSaleTaxType AS STT WITH(NOLOCK) ON ATI.SALETAXTYPEID = STT.Id
    WHERE 
        ATI.ProductId = @ProductId
    AND ATI.DocumentStatus =@DocumentStatus
);
GO
/****** Object:  Table [dbo].[AFInvoicePPI]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFInvoicePPI](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[InvoiceId] [int] NULL,
	[ProductPriceLogId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[ActualAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[ChargedAmount] [decimal](18, 2) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__AFInvoic__3214EC07A5281937] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetInvoiceCalculations]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GetInvoiceCalculations]
(
    @InvoiceId INT,
    @DocumentStatus INT = 1
)
RETURNS TABLE
AS
RETURN 
(
    SELECT 
        P.InvoiceId,

        -- 1. Gross: Summed and cast to 2 decimal places
        CAST(SUM(P.ActualAmount) AS DECIMAL(18, 2)) AS GrossAmount,

        -- 2. Discount: Summed and cast to 2 decimal places
        CAST(SUM(P.DiscountAmount) AS DECIMAL(18, 2)) AS DiscountAmount,

        -- 3. Taxable Amount (Actual Cost of Item without Tax)
        CAST(SUM(
            CASE 
                WHEN ISNULL(PC.IsSaleTaxExclusive, 0) = 1 
                THEN (P.ActualAmount - P.DiscountAmount) 
                ELSE (P.ActualAmount - P.DiscountAmount) / (1.0 + (ISNULL(PC.DefaultRate, 0) + ISNULL(PC.AdditionalRate, 0)) / 100.0)
            END
        ) AS DECIMAL(18, 2)) AS TaxableAmount,

        -- 4. Sales Tax Amount
        CAST(SUM(
            CASE 
                WHEN ISNULL(PC.IsSaleTaxExclusive, 0) = 1 
                THEN (P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.DefaultRate, 0) / 100.0)
                ELSE ((P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.DefaultRate, 0) / 100.0)) 
                     / (1.0 + (ISNULL(PC.DefaultRate, 0) + ISNULL(PC.AdditionalRate, 0)) / 100.0)
            END
        ) AS DECIMAL(18, 2)) AS SaleTaxAmount,

        -- 5. Additional Tax Amount
        CAST(SUM(
            CASE 
                WHEN ISNULL(PC.IsSaleTaxExclusive, 0) = 1 
                THEN (P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.AdditionalRate, 0) / 100.0)
                ELSE ((P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.AdditionalRate, 0) / 100.0)) 
                     / (1.0 + (ISNULL(PC.DefaultRate, 0) + ISNULL(PC.AdditionalRate, 0)) / 100.0)
            END
        ) AS DECIMAL(18, 2)) AS AdditionalTaxAmount,

        -- 6. Net Amount (Total Charged to Customer)
        CAST(SUM(
            CASE 
                WHEN ISNULL(PC.IsSaleTaxExclusive, 0) = 1 
                THEN (P.ActualAmount - P.DiscountAmount) * (1.0 + (ISNULL(PC.DefaultRate, 0) + ISNULL(PC.AdditionalRate, 0)) / 100.0)
                ELSE (P.ActualAmount - P.DiscountAmount) 
            END
        ) AS DECIMAL(18, 2)) AS NetAmount

    FROM [dbo].[AFInvoicePPI] AS P WITH(NOLOCK)
    OUTER APPLY [dbo].[retProductConfig](P.ProductId, @DocumentStatus) AS PC
    WHERE P.InvoiceId = @InvoiceId
    GROUP BY P.InvoiceId,P.Id
);
GO
/****** Object:  Table [dbo].[ACBranch]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACBranch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[OrganisationTypeId] [int] NULL,
	[CountryId] [int] NULL,
	[CityId] [int] NULL,
	[Contact] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[NTNNumber] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_ACBranch_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ACCompany]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCompany](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CountryId] [int] NULL,
	[CityId] [int] NULL,
	[Contact] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[Logo] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_ACCompany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ACSaleUnit]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSaleUnit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[PackingQuantity] [decimal](18, 2) NULL,
	[UnitId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ACUser]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[EmployeeId] [int] NULL,
	[RoleId] [int] NULL,
	[AllowedBranchIds] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_ACUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFBill]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFBill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[SupplierId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[DueAmount] [decimal](18, 2) NULL,
	[BillTypeId] [int] NULL,
	[BillStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFBillPPI]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFBillPPI](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[BillId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Quantity] [int] NULL,
	[ActualAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[ChargedAmount] [decimal](18, 2) NOT NULL,
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__AFBillPP__3214EC0769720BF3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFChartOfAccount]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFChartOfAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[AccountCategoryId] [int] NULL,
	[FinancialStatementId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_AFChartOfAccount_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFCustomerLedger]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFCustomerLedger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[CustomerId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[ReconcillationStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_AFCustomerLedger_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFInventoryLedger]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFInventoryLedger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[QuantityIn] [decimal](18, 2) NOT NULL,
	[QuantityOut] [decimal](18, 2) NOT NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[ReconcillationStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK__ILedger__3214EC07240B5AAA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFInvoice]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFInvoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[CustomerId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[FBRStamp] [nvarchar](max) NULL,
	[DueAmount] [decimal](18, 2) NOT NULL,
	[InvoiceTypeId] [int] NULL,
	[InvoiceStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_AFInvoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFInvoiceReceipt]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFInvoiceReceipt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[CustomerId] [int] NULL,
	[InvoiceId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[PaymentTypeId] [int] NULL,
	[PaymentMethodId] [int] NULL,
	[Reference] [nvarchar](max) NULL,
	[ReceiptAmount] [decimal](18, 2) NOT NULL,
	[PaymentStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFJournalVoucher]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFJournalVoucher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[ChartOfAccountId] [int] NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[PostingStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_AFJournalVoucher_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFProductPriceLog]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFProductPriceLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[TierTypeId] [int] NULL,
	[DefaultSalePrice] [decimal](18, 2) NOT NULL,
	[MinimumSalePrice] [decimal](18, 2) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFSupplierLedger]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFSupplierLedger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[SupplierId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[ReconcillationStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [int] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppForm]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppForm](
	[FormId] [int] IDENTITY(1,1) NOT NULL,
	[FormCode] [nvarchar](50) NOT NULL,
	[FormName] [nvarchar](200) NOT NULL,
	[ModuleName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[FormCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[BranchId] [int] NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
	[FullName] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[IsSuperAdmin] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[BranchName] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](200) NOT NULL,
	[AllowForms] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[confClientProductSetting]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[confClientProductSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ClientKEY] [int] NULL,
	[EnableSKU] [bit] NULL,
	[EnableMachineNumber] [bit] NULL,
	[EnableFavorite] [bit] NULL,
	[EnableTaxSetting] [bit] NULL,
	[EnableAttribute] [bit] NULL,
	[EnableExpiry] [bit] NULL,
	[EnableATI] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Status] [bit] NULL,
	[ProductTypeLabel] [nvarchar](max) NULL,
	[EnableDepartment] [bit] NULL,
 CONSTRAINT [PK__confClie__3214EC07BF3A289D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[confClientSetting]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[confClientSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ClientKey] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CSDepartment]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSDepartment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_ACDepartment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IAdjustment]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IAdjustment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[AdjustmentTypeId] [int] NULL,
	[AdjustmentStatus] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IAdjustmentPPQD]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IAdjustmentPPQD](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[AdjustmentId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[UnitPurchasePrice] [decimal](18, 2) NOT NULL,
	[UnitSalePrice] [decimal](18, 2) NOT NULL,
	[QuantityIn] [decimal](18, 0) NOT NULL,
	[QuantityOut] [decimal](18, 0) NOT NULL,
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__IAdjustm__3214EC0733D4010F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IBrand]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IBrand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_IBrand_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ICategory]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ICategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[DepartmentId] [int] NULL,
	[SectionId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISection]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[DepartmentId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_ACSection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISubCategory]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISubCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[DepartmentId] [int] NULL,
	[CategoryId] [int] NULL,
	[SectionId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[osvChartOfAccount]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[osvChartOfAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[AccountCategoryId] [int] NULL,
	[FinancialStatementId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_CSChartOfAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[osvProductCombination]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[osvProductCombination](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[RefDocumentType] [int] NULL,
	[ProductId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PSupplier]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PSupplier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[CNICNumber] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[AdditionalDetail] [nvarchar](max) NULL,
	[PayableAccountId] [int] NULL,
	[OpeningBalance] [decimal](18, 2) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SOCustomer]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SOCustomer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[TierTypeId] [int] NULL,
	[Contact] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[CNICNumber] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[AdditionalDetail] [nvarchar](max) NULL,
	[ReceivableAccountId] [int] NULL,
	[OpeningBalance] [decimal](18, 2) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK__SOCustom__3214EC079C9ACE51] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRight]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRight](
	[UserRightId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FormId] [int] NOT NULL,
	[CanView] [bit] NULL,
	[CanAdd] [bit] NULL,
	[CanEdit] [bit] NULL,
	[CanDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_UserForm] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[FormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vAccountCatagory]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vAccountCatagory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ShortCode] [nvarchar](max) NULL,
	[AccountTypeId] [int] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vAccountType]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vAccountType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShortCode] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vAttribute]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vAttribute](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vCity]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vCity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[StateId] [int] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vCostingMode]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vCostingMode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vCountry]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vCountry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NULL,
	[CallingCode] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vFinancialStatement]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vFinancialStatement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vInventoryAdjustmentType]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vInventoryAdjustmentType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsPurchasePrice] [bit] NOT NULL,
	[IsSalePrice] [bit] NOT NULL,
	[IsQuantityIn] [bit] NOT NULL,
	[IsQuantityOut] [bit] NOT NULL,
	[IsAutoPriceUpdate] [bit] NULL,
	[Status] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vOrganisationType]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vOrganisationType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vPaymentMethod]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vPaymentMethod](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vProductType]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vProductType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vRight]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vRight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Menu] [nvarchar](max) NULL,
	[SubMenu] [nvarchar](max) NULL,
	[FormName] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[Purpose] [nvarchar](max) NULL,
	[RoleIds] [nvarchar](max) NULL,
	[Priority] [int] NULL,
	[IsDisplayAllowed] [bit] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vRole]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vTierType]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vTierType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vUnit]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vUnit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsScalar] [bit] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ACSaleUnit] ADD  DEFAULT (newid()) FOR [GuID]
GO
ALTER TABLE [dbo].[ACSaleUnit] ADD  DEFAULT ((0)) FOR [PackingQuantity]
GO
ALTER TABLE [dbo].[ACSaleUnit] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[ACSaleUnit] ADD  DEFAULT ((1)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF__AFBillPPI__Actua__75F77EB0]  DEFAULT ((0)) FOR [ActualAmount]
GO
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF__AFBillPPI__Disco__76EBA2E9]  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF__AFBillPPI__Charg__77DFC722]  DEFAULT ((0)) FOR [ChargedAmount]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__ILedger__Quantit__52442E1F]  DEFAULT ((0)) FOR [QuantityIn]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__ILedger__Quantit__53385258]  DEFAULT ((0)) FOR [QuantityOut]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__ILedger__Debit__542C7691]  DEFAULT ((0)) FOR [Debit]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__ILedger__Credit__55209ACA]  DEFAULT ((0)) FOR [Credit]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__ILedger__Status__5614BF03]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[AFInvoice] ADD  DEFAULT ((0)) FOR [DueAmount]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Quant__06CD04F7]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Actua__07C12930]  DEFAULT ((0)) FOR [ActualAmount]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Disco__08B54D69]  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Charg__09A971A2]  DEFAULT ((0)) FOR [ChargedAmount]
GO
ALTER TABLE [dbo].[AFInvoiceReceipt] ADD  DEFAULT ((0)) FOR [ReceiptAmount]
GO
ALTER TABLE [dbo].[AFProductPriceLog] ADD  DEFAULT ((0)) FOR [DefaultSalePrice]
GO
ALTER TABLE [dbo].[AFProductPriceLog] ADD  DEFAULT ((0)) FOR [MinimumSalePrice]
GO
ALTER TABLE [dbo].[AFSupplierLedger] ADD  DEFAULT ((0)) FOR [Debit]
GO
ALTER TABLE [dbo].[AFSupplierLedger] ADD  DEFAULT ((0)) FOR [Credit]
GO
ALTER TABLE [dbo].[AppUser] ADD  DEFAULT ((0)) FOR [IsSuperAdmin]
GO
ALTER TABLE [dbo].[AppUser] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AppUser] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Branch] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Branch] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[confClientProductSetting] ADD  CONSTRAINT [DF__confClien__Enabl__4C364F0E]  DEFAULT ((0)) FOR [EnableSKU]
GO
ALTER TABLE [dbo].[confClientProductSetting] ADD  CONSTRAINT [DF__confClien__Enabl__4D2A7347]  DEFAULT ((0)) FOR [EnableMachineNumber]
GO
ALTER TABLE [dbo].[confClientProductSetting] ADD  CONSTRAINT [DF__confClien__Enabl__4E1E9780]  DEFAULT ((0)) FOR [EnableFavorite]
GO
ALTER TABLE [dbo].[confClientProductSetting] ADD  CONSTRAINT [DF__confClien__Enabl__4F12BBB9]  DEFAULT ((0)) FOR [EnableTaxSetting]
GO
ALTER TABLE [dbo].[confClientProductSetting] ADD  CONSTRAINT [DF__confClien__Enabl__5006DFF2]  DEFAULT ((0)) FOR [EnableAttribute]
GO
ALTER TABLE [dbo].[confClientProductSetting] ADD  CONSTRAINT [DF__confClien__Enabl__50FB042B]  DEFAULT ((0)) FOR [EnableATI]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__UnitP__420DC656]  DEFAULT ((0)) FOR [UnitPurchasePrice]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__UnitS__4301EA8F]  DEFAULT ((0)) FOR [UnitSalePrice]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__Quant__43F60EC8]  DEFAULT ((0)) FOR [QuantityIn]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__Quant__44EA3301]  DEFAULT ((0)) FOR [QuantityOut]
GO
ALTER TABLE [dbo].[IProduct] ADD  CONSTRAINT [DF__IProduct__Critic__0A9D95DB]  DEFAULT ((0)) FOR [CriticalLimit]
GO
ALTER TABLE [dbo].[PSupplier] ADD  DEFAULT ((0)) FOR [OpeningBalance]
GO
ALTER TABLE [dbo].[SOCustomer] ADD  CONSTRAINT [DF_SOCustomer_OpeningBalance]  DEFAULT ((0)) FOR [OpeningBalance]
GO
ALTER TABLE [dbo].[UserRight] ADD  DEFAULT ((0)) FOR [CanView]
GO
ALTER TABLE [dbo].[UserRight] ADD  DEFAULT ((0)) FOR [CanAdd]
GO
ALTER TABLE [dbo].[UserRight] ADD  DEFAULT ((0)) FOR [CanEdit]
GO
ALTER TABLE [dbo].[UserRight] ADD  DEFAULT ((0)) FOR [CanDelete]
GO
ALTER TABLE [dbo].[vCostingMode] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[vInventoryAdjustmentType] ADD  DEFAULT ((0)) FOR [IsAutoPriceUpdate]
GO
ALTER TABLE [dbo].[vInventoryAdjustmentType] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[vProductType] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[vTierType] ADD  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[vTierType] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[UserRight]  WITH CHECK ADD FOREIGN KEY([FormId])
REFERENCES [dbo].[AppForm] ([FormId])
GO
ALTER TABLE [dbo].[UserRight]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUser] ([UserId])
GO
/****** Object:  StoredProcedure [dbo].[ACBranch_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[ACBranch_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@Description			NVARCHAR(MAX),
	@OrganisationTypeId		INT,
	@CountryId				INT,
	@CityId					INT,
	@Contact				NVARCHAR(MAX),
	@Email					NVARCHAR(MAX),
	@Address				NVARCHAR(MAX),
	@NTNNumber				NVARCHAR(MAX),
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [ACBranch]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[ACBranch]
			(
				GuID,
				Code,
				Description,
				OrganisationTypeId,
				CountryId,
				CityId,
				Contact,
				Email,
				Address,
				NTNNumber,
				CreatedOn,
				CreatedBy,
				DocumentType,
				DocumentStatus,
				Status,
				BranchId,
				CompanyId
			)
			VALUES
			(
				@GuID,
				@Code,
				@Description,
				@OrganisationTypeId,
				@CountryId,
				@CityId,
				@Contact,
				@Email,
				@Address,
				@NTNNumber,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
			)

			SET @Response = 201;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[ACBranch]
			SET
				Description			= @Description,
				OrganisationTypeId	= @OrganisationTypeId,
				CountryId			= @CountryId,
				CityId				= @CityId,
				Contact				= @Contact,
				Email				= @Email,
				Address				= @Address,
				NTNNumber			= @NTNNumber,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID

			SET @Response = 202;
		END

	END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ACCompany_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[ACCompany_Upsert]
	@DB_OperationType			NVARCHAR(MAX),
	@GuID						UNIQUEIDENTIFIER,
	@Description				NVARCHAR(MAX),
	@CountryId					INT,
	@CityId						INT,
	@Contact					NVARCHAR(MAX),
	@Email						NVARCHAR(MAX),
	@Address					NVARCHAR(MAX),
	@Website					NVARCHAR(MAX),
	@Logo						NVARCHAR(MAX),
	@CreatedOn					DATETIME,
	@CreatedBy					INT,
	@UpdatedOn					DATETIME,
	@UpdatedBy					INT,
	@DocumentType				INT,
	@DocumentStatus				INT,
	@Status						BIT,
	@BranchId					INT,
	@CompanyId					INT,
	@Response					INT OUTPUT -- RESPONSE CODE 

AS
	BEGIN
	SET NOCOUNT ON;
		BEGIN TRY

				IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
				BEGIN
					DECLARE @Code NVARCHAR(MAX)
					SELECT @Code =  dbo.fn_CodeFormat(MAX(cast(substring(code,8,LEN(code)) as bigint)))FROM [ACCompany] WHERE Year(CreatedOn) = Year(getdate())AND Month([CreatedOn]) = Month(getdate())AND (CompanyId = @CompanyId);
					INSERT INTO [dbo].[ACCompany]
					(
						[GuID],
						[Code],
						[Description],
						[CountryId],
						[CityId],
						[Contact],
						[Email],
						[Address],
						[Website],
						[Logo],
						[CreatedOn],
						[CreatedBy],
						[DocumentType],
						[DocumentStatus],
						[Status],
						[BranchId],
						[CompanyId]
					)

					VALUES
					(
						@GuID,
						@Code,
						@Description,
						@CountryId,
						@CityId,
						@Contact,
						@Email,
						@Address,
						@Website,
						@Logo,
						@CreatedOn,
						@CreatedBy,
						@DocumentType,
						@DocumentStatus,
						@Status,
						@BranchId,
						@CompanyId
					)

					SET @Response = 201;

				END
				ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
				BEGIN
				UPDATE [dbo].[ACCompany]
				SET
						[Description]		=	@Description,
						[CountryId]			=	@CountryId,
						[CityId]			=	@CityId,
						[Contact]			=	@Contact,
						[Email]				=	@Email,
						[Address]			=	@Address,
						[Website]			=	@Website,
						[Logo]				=	@Logo,
						[UpdatedOn]			=	@UpdatedOn,
						[UpdatedBy]			=	@UpdatedBy,
						[BranchId]			=	@BranchId,
						[CompanyId]			=	@CompanyId
				WHERE [dbo].[ACCompany].[GuID]=@GuID

				SET @Response = 202;
				END
		END TRY
		BEGIN CATCH
					SET @Response = 400;--CODE BAD REQUEST
		END CATCH
	END

GO
/****** Object:  StoredProcedure [dbo].[ACUser_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[ACUser_Upsert]
(
    @DB_OperationType   NVARCHAR(MAX),
    @GuID               UNIQUEIDENTIFIER,
    @Description        NVARCHAR(MAX),
    @Password           NVARCHAR(MAX),
    @Contact            NVARCHAR(MAX),
    @Email              NVARCHAR(MAX),
    @EmployeeId         INT,
    @RoleId             INT,
    @AllowedBranchIds   NVARCHAR(MAX),
    @CreatedOn          DATETIME,
    @CreatedBy          INT,
    @UpdatedOn          DATETIME,
    @UpdatedBy          INT,
    @DocumentType       INT,
    @DocumentStatus     INT,
    @Status             BIT,
    @BranchId           INT,
    @CompanyId          INT,
    @Response           INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
        BEGIN
        	DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [ACUser]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
            INSERT INTO [dbo].[ACUser]
            (
                GuID,
                Code,
                Description,
                Password,
                Contact,
                Email,
                EmployeeId,
                RoleId,
                AllowedBranchIds,
                CreatedOn,
                CreatedBy,
                DocumentType,
                DocumentStatus,
                Status,
                BranchId,
                CompanyId
            )
            VALUES
            (
                @GuID,
                @Code,
                @Description,
                @Password,
                @Contact,
                @Email,
                @EmployeeId,
                @RoleId,
                @AllowedBranchIds,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
            );

            SET @Response = 201;
        END
        ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
        BEGIN
            UPDATE [dbo].[ACUser]
            SET
                Description = @Description,
                Password = @Password,
                Contact = @Contact,
                Email = @Email,
                EmployeeId = @EmployeeId,
                RoleId = @RoleId,
                AllowedBranchIds = @AllowedBranchIds,
                UpdatedOn = @UpdatedOn,
                UpdatedBy = @UpdatedBy,
                BranchId = @BranchId,
                CompanyId = @CompanyId
            WHERE GuID = @GuID

            SET @Response = 202;
        END
    END TRY
    BEGIN CATCH
        SET @Response = 400;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AFBill_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AFBill_Upsert]  
    @DB_OperationType      NVARCHAR(MAX),  
    @GuID                  UNIQUEIDENTIFIER,  
    @LocationId            INT,  
    @TransactionDate       DATETIME,  
    @SupplierId            INT,  
    @Description           NVARCHAR(MAX),  
    @DueAmount             DECIMAL(18,2),  
    @BillTypeId            INT,  
    @BillStatus            INT,  
    @CreatedOn             DATETIME,  
    @CreatedBy             INT,  
    @UpdatedOn             DATETIME,  
    @UpdatedBy             INT,  
    @DocumentType          INT,  
    @DocumentStatus        INT,  
    @Status                BIT,  
    @BranchId              INT,  
    @CompanyId             INT,  
    @AFBill_TVP            [dbo].[AFBillProduct_TVP] READONLY,  
    @DocumentCode          NVARCHAR(MAX) OUTPUT,  
    @InsertedId            INT OUTPUT,  
    @TotalBillAmount       DECIMAL(18,2) OUTPUT,  
    @Response              INT OUTPUT
AS  
BEGIN TRY  
  
 IF @DB_OperationType='INSERT_DATA_INTO_DB'  
 BEGIN  
  DECLARE @BillId INT;  
  DECLARE @Code  NVARCHAR(MAX)  
       
        -- INSERT INTO PRODUCT COMBINATION ENGINE      
        DECLARE @CombinationInput dbo.ProductCombination_TVP;      
        INSERT INTO @CombinationInput (ProductId, Attribute, [Status])      
        SELECT DISTINCT ProductId, Attribute, [Status]      
        FROM @AFBill_TVP;      
        DECLARE @MappedCombinations TABLE (      
            ProductCombinationId INT,      
            ProductId     INT,      
            Attribute     NVARCHAR(MAX)      
        );      
        INSERT INTO @MappedCombinations (ProductCombinationId, ProductId, Attribute)      
        EXEC dbo.ProductCombination_Merge       
                                    @RefDocumentType = @DocumentType,       
                                    @Combinations    = @CombinationInput;       


    
   SELECT @Code = dbo.fn_CodeFormat(  
    MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))  
   )  
   FROM [AFBill]  
   WHERE YEAR(CreatedOn) = YEAR(GETDATE())  
     AND MONTH(CreatedOn) = MONTH(GETDATE())  
     AND CompanyId = @CompanyId;  
     SELECT @DueAmount = SUM(dbo.fn_CalculateLineTotal(ActualAmount, DiscountAmount, ProductId, @DocumentStatus))  
    FROM @AFBill_TVP;  
  DECLARE @DocumentNumber NVARCHAR(MAX) =  @Code;  
  INSERT INTO [AFBill]  
  (  
   [GuID],   
   [Code],  
   [LocationId],    
   [TransactionDate],  
   [SupplierId],    
   [Description],   
   [DueAmount],  
   [BillTypeId],  
   [BillStatus],  
   [CreatedOn],    
   [CreatedBy],  
   [DocumentType],  
   [DocumentStatus],  
   [Status],  
   [BranchId],    
   [CompanyId]   
  )  
  VALUES(  
   @GuID,   
   @Code,  
   @LocationId,    
   @TransactionDate,  
   @SupplierId,    
   @Description,   
   @DueAmount,  
   @BillTypeId,  
   @BillStatus,  
   @CreatedOn,    
   @CreatedBy,  
   @DocumentType,  
   @DocumentStatus,  
   @Status,  
   @BranchId,    
   @CompanyId   
  )  
  SET @BillId = SCOPE_IDENTITY();  
  
  INSERT INTO AFBillPPI  
  (  
   [GuID],  
   [BillId],  
   [ProductId],
   [ProductCombinationId],
   [Quantity],  
   [ActualAmount],  
   [DiscountAmount],  
   [ChargedAmount],  
   [Batch],
   [ExpiryDate],
   [CreatedOn],    
   [CreatedBy],  
   [DocumentType],  
   [DocumentStatus],  
   [Status]  
  )  
  SELECT   
   BTVP.[GuID],  
   @BillId,  
   BTVP.[ProductId], 
   MC.[ProductCombinationId],
   BTVP.[Quantity],  
   BTVP.[ActualAmount],  
   BTVP.[DiscountAmount],  
   BTVP.[ChargedAmount],
   BTVP.[Batch],
   BTVP.[ExpiryDate],
   @CreatedOn,  
   @CreatedBy,  
   BTVP.[DocumentType],  
   BTVP.[DocumentStatus],  
   BTVP.[Status]  
  FROM @AFBill_TVP  BTVP
   INNER JOIN @MappedCombinations mc      
            ON mc.ProductId = BTVP.ProductId      
            AND mc.Attribute = BTVP.Attribute;      
          
  SET @DocumentCode     =   @Code;  
  SET @InsertedId       =   SCOPE_IDENTITY();  
  SET @TotalBillAmount  =   @DueAmount;  
  SET @Response         =   201  
 END  
  
END TRY  
BEGIN CATCH  
  SET @DocumentCode     =   'N/A';  
  SET @InsertedId       =   0;  
  SET @TotalBillAmount  =   0;  
  SET @Response         =   400;  
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[AFChartOfAccount_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE    PROCEDURE [dbo].[AFChartOfAccount_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@Description			NVARCHAR(MAX),
	@AccountCategoryId		INT,
	@FinancialStatementId	INT,
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@InsertedId				INT OUTPUT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [AFChartOfAccount]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[AFChartOfAccount]
			(
				GuID,
				Code,
				Description,
				AccountCategoryId,
				FinancialStatementId,
				CreatedOn,
				CreatedBy,
				DocumentType,
				DocumentStatus,
				Status,
				BranchId,
				CompanyId
			)
			VALUES
			(
				@GuID,
				@Code,
				@Description,
				@AccountCategoryId,
				@FinancialStatementId,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
			)
			SET @InsertedId = SCOPE_IDENTITY();
			SET @Response = 201;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[AFChartOfAccount]
			SET
				Description			= @Description,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID
			SELECT @InsertedId = Id FROM AFChartOfAccount WHERE GuID=@GuID
			SET @Response = 202;
		END

	END TRY
	BEGIN CATCH
		SET @InsertedId =0;
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AFCustomerLedger_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[AFCustomerLedger_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@CompanyId				INT,
	@AFCustomerLedger_TVP	[dbo].[AFCustomerLedger_TVP] READONLY,
	@DocumentCode			NVARCHAR(MAX)	OUTPUT,
	@InsertedId				INT				OUTPUT,
	@Response				INT				OUTPUT
AS 
BEGIN TRY
	IF @DB_OperationType ='INSERT_DATA_INTO_DB' OR @DB_OperationType='MPO_LIST'
		BEGIN

				DECLARE @Code		NVARCHAR(MAX)

				SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
				)
				FROM [AFCustomerLedger]
				WHERE YEAR(CreatedOn) = YEAR(GETDATE())
				AND MONTH(CreatedOn) = MONTH(GETDATE())
				AND CompanyId = @CompanyId;

				INSERT INTO [AFCustomerLedger]
				(
					[GuID],
					[Code],
					[LocationId],
					[TransactionDate],
					[CustomerId],
					[RefDocumentType],
					[RefDocumentId],
					[Description],
					[Debit],
					[Credit],
					[ReconcillationStatus],
					[CreatedOn],
					[CreatedBy],
					[DocumentType],
					[DocumentStatus],
					[Status],
					[BranchId],
					[CompanyId]
				)
				SELECT 

					[GuID],
					@Code,
					[LocationId],
					[TransactionDate],
					[CustomerId],
					[RefDocumentType],
					[RefDocumentId],
					[Description],
					[Debit],
					[Credit],
					[ReconcillationStatus],
					[CreatedOn],
					[CreatedBy],
					[DocumentType],
					[DocumentStatus],
					[Status],
					[BranchId],
					[CompanyId]
				FROM @AFCustomerLedger_TVP
			SET @DocumentCode	=	@Code
			SET @InsertedId		=	SCOPE_IDENTITY()
			SET @Response		=	201;
		END
END TRY

BEGIN CATCH
	SET @DocumentCode	=	'N/A'
	SET @InsertedId		=	0
	SET @Response		=	400
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[AFInventoryLedger_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AFInventoryLedger_Upsert]  
    @OperationType              NVARCHAR(MAX),
    @RefDocumentType            INT,
    @AFInventoryLedger_TVP      dbo.AFInventoryLedger_TVP READONLY, 
    @Response                   INT OUTPUT,  
    @InsertedIn                 INT OUTPUT,  
    @documentCode               NVARCHAR(MAX) OUTPUT 
AS  
BEGIN  
    SET NOCOUNT ON;  
      
    SET @Response = 0;  
    SET @InsertedIn = 0;  
      
        
        -- INSERT INTO PRODUCT COMBINATION ENGINE    
        DECLARE @CombinationInput dbo.ProductCombination_TVP;    
        INSERT INTO @CombinationInput (ProductId, Attribute, [Status])    
        SELECT DISTINCT ProductId, Attribute, [Status]    
        FROM @AFInventoryLedger_TVP;    
        DECLARE @MappedCombinations TABLE (    
            ProductCombinationId INT,    
            ProductId     INT,    
            Attribute     NVARCHAR(MAX)    
        );    
        INSERT INTO @MappedCombinations (ProductCombinationId, ProductId, Attribute)    
        EXEC dbo.ProductCombination_Merge     
                                    @RefDocumentType = @RefDocumentType,     
                                    @Combinations    = @CombinationInput;     
  
  
  
    BEGIN TRY  
        IF @OperationType = 'INSERT_DATA_INTO_DB'  
        BEGIN  
            IF ISNULL(@documentCode, '') = ''  
            BEGIN  
                SET @documentCode =  CONVERT(NVARCHAR(8), GETDATE(), 112) + '-' + RIGHT('0000' + CAST(ISNULL((SELECT COUNT(DISTINCT RefDocumentId) FROM dbo.AFInventoryLedger), 0) + 1 AS NVARCHAR(10)), 4);  
            END  
  
  
  
  
            INSERT INTO dbo.AFInventoryLedger  
            (  
                GuID,  
                Code,  
                LocationId,  
                TransactionDate,  
                ProductId,  
                ProductCombinationId,  
                RefDocumentType,  
                RefDocumentId,  
                Description,  
                QuantityIn,  
                QuantityOut,  
                Debit,  
                Credit,  
                Batch,
                ExpiryDate,
                ReconcillationStatus,  
                CreatedOn,  
                CreatedBy,  
                DocumentType,  
                DocumentStatus,  
                Status,  
                BranchId,  
                CompanyId  
            )  
            SELECT  
                ILTVP.[GuID],   
                @documentCode,  
                ILTVP.[LocationId],  
                ILTVP.[TransactionDate],  
                ILTVP.[ProductId],  
                MC.[ProductCombinationId],    
                @RefDocumentType,  
                ILTVP.[RefDocumentId],  
                ILTVP.[Description],  
                ILTVP.[QuantityIn],  
                ILTVP.[QuantityOut],  
                ILTVP.[Debit],  
                ILTVP.[Credit],
                ILTVP.[Batch],
                ILTVP.[ExpiryDate],
                ILTVP.[ReconcillationStatus],  
                ILTVP.[CreatedOn],  
                ILTVP.[CreatedBy],  
                ILTVP.[DocumentType],  
                ILTVP.[DocumentStatus],  
                ILTVP.[Status],  
                ILTVP.[BranchId],  
                ILTVP.[CompanyId]  
  
            FROM @AFInventoryLedger_TVP AS ILTVP  
            INNER JOIN @MappedCombinations mc    
            ON mc.ProductId = ILTVP.ProductId    
            AND mc.Attribute = ILTVP.Attribute;    
      
  
            SET @InsertedIn = @@ROWCOUNT;  
            SET @Response = 1;   
        END  
  
    END TRY  
    BEGIN CATCH  
        SET @Response = 0;  
        SET @InsertedIn = 0;  
          
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();  
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();  
        DECLARE @ErrorState INT = ERROR_STATE();  
          
 RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);  
    END CATCH  
END;  
GO
/****** Object:  StoredProcedure [dbo].[AFInvoice_GLBParam]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AFInvoice_GLBParam]
    @GuID               UNIQUEIDENTIFIER,
    @CustomerId         INT,
    @DocumentStatus     NVARCHAR(MAX),
    @InvoiceStatuses    NVARCHAR(MAX)
AS

SELECT 
    C.[Description]     AS [CustomerName],
    'INV-' + I.[Code]   AS [Code],
    I.[TransactionDate],
    CALC.[GrossAmount],
    CALC.[DiscountAmount],
    CALC.[TaxableAmount],
    CALC.[SaleTaxAmount],
    CALC.[AdditionalTaxAmount],
    CALC.[NetAmount],
    I.[DueAmount],
    I.[InvoiceTypeId],
    I.[DocumentStatus],
    I.[InvoiceStatus],
    I.[GuID],
    C.[Id]              AS  [CustomerId],
    I.[Id]              AS  [InvoiceId]

FROM        [dbo].[AFInvoice]   AS I WITH(NOLOCK)
INNER JOIN  [dbo].[SOCustomer]  AS C WITH(NOLOCK) ON C.Id = I.CustomerId
CROSS APPLY [dbo].[fn_GetInvoiceCalculations](I.Id, 1) AS CALC
WHERE
I.Status=1
AND (@GuID IS NULL OR I.GuID = @GuID)
AND (@CustomerId = -1 OR I.CustomerId = @CustomerId)
AND I.DocumentStatus IN(@DocumentStatus)
--AND I.InvoiceStatus  IN(@InvoiceStatuses)

GO
/****** Object:  StoredProcedure [dbo].[AFInvoice_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[AFInvoice_Upsert]  
 @DB_OperationType NVARCHAR(MAX),  
 @GuID    UNIQUEIDENTIFIER,  
 @LocationId   INT,  
 @TransactionDate DATETIME,  
 @CustomerId   INT,  
 @Description  NVARCHAR(MAX),  
 @FBRStamp   NVARCHAR(MAX),  
 @DueAmount   DECIMAL(18,2),  
 @InvoiceTypeId  INT,  
 @InvoiceStatus  INT,  
 @CreatedOn   DATETIME,  
 @CreatedBy   INT,  
 @UpdatedOn   DATETIME,  
 @UpdatedBy   INT,  
 @DocumentType  INT,  
 @DocumentStatus  INT,  
 @Status    BIT,  
 @BranchId   INT,  
 @CompanyId   INT,  
 @AFInvoice_TVP  [dbo].[AFInvoiceProduct_TVP] READONLY,  
 @DocumentCode  NVARCHAR(MAX) OUTPUT,  
 @InsertedId   INT OUTPUT,  
 @TotalInvoiceAmount DECIMAL(18,2) OUTPUT,  
 @Response   INT OUTPUT  
AS  
BEGIN TRY  
  
 IF @DB_OperationType='INSERT_DATA_INTO_DB'  
 BEGIN  
  DECLARE @InvoiceId INT;  
  DECLARE @Code  NVARCHAR(MAX)  
  
    
   SELECT @Code = dbo.fn_CodeFormat(  
    MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))  
   )  
   FROM [AFInvoice]  
   WHERE YEAR(CreatedOn) = YEAR(GETDATE())  
     AND MONTH(CreatedOn) = MONTH(GETDATE())  
     AND CompanyId = @CompanyId;  
     SELECT @DueAmount = SUM(dbo.fn_CalculateLineTotal(ActualAmount, DiscountAmount, ProductId, @DocumentStatus))  
    FROM @AFInvoice_TVP;  
  DECLARE @DocumentNumber NVARCHAR(MAX) =  @Code;  
  INSERT INTO [AFInvoice]  
  (  
   [GuID],   
   [Code],  
   [LocationId],    
   [TransactionDate],  
   [CustomerId],    
   [Description],   
   [FBRStamp],   
   [DueAmount],  
   [InvoiceTypeId],  
   [InvoiceStatus],  
   [CreatedOn],    
   [CreatedBy],  
   [DocumentType],  
   [DocumentStatus],  
   [Status],  
   [BranchId],    
   [CompanyId]   
  )  
  VALUES(  
   @GuID,   
   @Code,  
   @LocationId,    
   @TransactionDate,  
   @CustomerId,    
   @Description,   
   @FBRStamp,  
   @DueAmount,  
   @InvoiceTypeId,  
   @InvoiceStatus,  
   @CreatedOn,    
   @CreatedBy,  
   @DocumentType,  
   @DocumentStatus,  
   @Status,  
   @BranchId,    
   @CompanyId   
  )  
  SET @InvoiceId = SCOPE_IDENTITY();  
  
  INSERT INTO AFInvoicePPI  
  (  
   [GuID],  
   [InvoiceId],
   [ProductPriceLogId],
   [ProductId],  
   [ProductCombinationId],
   [Quantity],  
   [ActualAmount],  
   [DiscountAmount],  
   [ChargedAmount],  
   [CreatedOn],    
   [CreatedBy],  
   [DocumentType],  
   [DocumentStatus],  
   [Status]  
  )  
  SELECT   
            [GuID],  
            @InvoiceId, 
            [ProductPriceLogId],
            [ProductId],  
            [ProductCombinationId],
            [Quantity],  
            [ActualAmount],  
            [DiscountAmount],  
            [ChargedAmount],  
            @CreatedOn,  
            @CreatedBy,  
            [DocumentType],  
            [DocumentStatus],  
            [Status]  
  FROM @AFInvoice_TVP  
    
  SET @DocumentCode=@Code;  
  SET @InsertedId=SCOPE_IDENTITY();  
  SET @TotalInvoiceAmount = @DueAmount;  
  SET @Response=201  
 END  
  
END TRY  
BEGIN CATCH  
  SET @DocumentCode='N/A';  
  SET @InsertedId=0;  
  SET @TotalInvoiceAmount = 0;  
  SET @Response=400;  
END CATCH  
  
  
GO
/****** Object:  StoredProcedure [dbo].[AFInvoiceReceipt_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[AFInvoiceReceipt_Upsert]    
    @DB_OperationType   NVARCHAR(MAX),    
    @GuID               UNIQUEIDENTIFIER,    
    @LocationId         INT,    
    @TransactionDate    DATETIME,    
    @CustomerId         INT,    
    @InvoiceId          INT,    
    @Description        NVARCHAR(MAX),    
    @PaymentTypeId      INT,    
    @PaymentMethodId    INT,    
    @Reference          NVARCHAR(MAX),    
    @ReceiptAmount      DECIMAL(18, 2),    
    @PaymentStatus      INT,    
    @CreatedOn          DATETIME,    
    @CreatedBy          INT,    
    @UpdatedOn          DATETIME,    
    @UpdatedBy          INT,    
    @DocumentType       INT,    
    @DocumentStatus     INT,    
    @Status             BIT,    
    @BranchId           INT,    
    @CompanyId          INT,    
    @InsertedId         INT OUTPUT,    
    @DocumentCode       NVARCHAR(MAX) OUTPUT,    
    @Response           INT OUTPUT    
AS    
BEGIN    
    SET NOCOUNT ON;    
    BEGIN TRY    
        IF @DB_OperationType = 'INSERT_DATA_INTO_DB'  OR  @DB_OperationType = 'MPO_LIST'  
        BEGIN    
            DECLARE @Code NVARCHAR(MAX)    
            SELECT @Code = dbo.fn_CodeFormat(    
                MAX(CAST(SUBSTRING(Code, 8, LEN(Code)) AS BIGINT))    
            )    
            FROM [AFInvoiceReceipt]    
            WHERE YEAR(CreatedOn)  = YEAR(GETDATE())    
              AND MONTH(CreatedOn) = MONTH(GETDATE())    
              AND CompanyId        = @CompanyId;    
    
            INSERT INTO [dbo].[AFInvoiceReceipt]    
            (    
                GuID,    
                Code,    
                LocationId,    
                TransactionDate,    
                CustomerId,    
                InvoiceId,    
                Description,    
                PaymentTypeId,  
                PaymentMethodId,   
                Reference,    
                ReceiptAmount,    
                PaymentStatus,    
                CreatedOn,    
                CreatedBy,    
                DocumentType,    
                DocumentStatus,    
                Status,    
                BranchId,    
                CompanyId    
            )    
            VALUES    
            (    
                @GuID,    
                @Code,    
                @LocationId,    
                @TransactionDate,    
                @CustomerId,    
                @InvoiceId,    
                @Description,    
                @PaymentTypeId,  
                @PaymentMethodId,  
                @Reference,    
                @ReceiptAmount,    
                @PaymentStatus,    
                @CreatedOn,    
                @CreatedBy,    
                @DocumentType,    
                @DocumentStatus,    
                @Status,    
                @BranchId,    
                @CompanyId    
            );    
            SET @InsertedId   = SCOPE_IDENTITY();    
            SET @DocumentCode = @Code;    
            SET @Response     = 201;    
        END    
            
        END TRY    
    BEGIN CATCH    
  SET @DocumentCode='N/A';    
  SET @InsertedId=0;    
  SET @Response=400;    
    END CATCH    
END 
GO
/****** Object:  StoredProcedure [dbo].[AFJournal_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[AFJournal_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@ReferanceGuID			NVARCHAR(MAX),
	@ReferanceDocumentType	DATETIME,
	@ChartOfAccountId		INT,
	@Description			NVARCHAR(MAX),
	@Debit					DECIMAL(18,2),
	@Credit					DECIMAL(18,2),
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [AFJournal]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[AFJournal]
			(
				[GuID],					
				[Code],					
				[ReferanceGuID],			
				[ReferanceDocumentType],	
				[ChartOfAccountId],		
				[Description],				
				[Debit],					
				[Credit],					
				[CreatedOn],				
				[CreatedBy],				
				[DocumentType],				
				[DocumentStatus],				
				[Status],						
				[BranchId],					
				[CompanyId]
			)
			VALUES
			(
				@GuID,					
				@Code,					
				@ReferanceGuID,			
				@ReferanceDocumentType,	
				@ChartOfAccountId,		
				@Description,				
				@Debit,					
				@Credit,					
				@CreatedOn,				
				@CreatedBy,				
				@DocumentType,				
				@DocumentStatus,				
				@Status,						
				@BranchId,					
				@CompanyId
			)

			SET @Response = 200;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[AFJournal]
			SET
				Description			= @Description,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID

			SET @Response = 300;
		END

	END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AFJournalVoucher_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[AFJournalVoucher_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@CompanyId				INT,
	@AFJournalVoucher_TVP	[dbo].[AFJournalVoucher_TVP] READONLY,
	@Response				INT		OUTPUT

AS 
BEGIN TRY
	IF @DB_OperationType ='INSERT_DATA_INTO_DB'
		BEGIN

				DECLARE @Code		NVARCHAR(MAX)

				SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
				)
				FROM [AFJournalVoucher]
				WHERE YEAR(CreatedOn) = YEAR(GETDATE())
				AND MONTH(CreatedOn) = MONTH(GETDATE())
				AND CompanyId = @CompanyId;

				INSERT INTO [AFJournalVoucher]
				(
					[GuID],
					[Code],
					[LocationId],
					[RefDocumentType],
					[RefDocumentId],
					[Description],
					[ChartOfAccountId],
					[Debit],
					[Credit],
					[PostingStatus],
					[CreatedOn],
					[CreatedBy],
					[DocumentType],
					[DocumentStatus],
					[Status],
					[BranchId],
					[CompanyId]
				)
				SELECT 

					[GuID],
					@Code,
					[LocationId],
					[RefDocumentType],
					[RefDocumentId],
					[Description],
					[ChartOfAccountId],
					[Debit],
					[Credit],
					[PostingStatus],
					[CreatedOn],
					[CreatedBy],
					[DocumentType],
					[DocumentStatus],
					[Status],
					[BranchId],
					[CompanyId]
				FROM @AFJournalVoucher_TVP
			SET @Response		=	201;
		END
END TRY

BEGIN CATCH
	SET @Response		=	400
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[AFSupplierLedger_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[AFSupplierLedger_Upsert]  
 @DB_OperationType      NVARCHAR(MAX),  
 @CompanyId             INT,  
 @AFSupplierLedger_TVP  [dbo].[AFSupplierLedger_TVP] READONLY,  
 @DocumentCode          NVARCHAR(MAX)   OUTPUT,  
 @InsertedId            INT             OUTPUT,  
 @Response              INT             OUTPUT  
AS   
BEGIN TRY  
 IF @DB_OperationType ='INSERT_DATA_INTO_DB' OR @DB_OperationType='MPO_LIST'  
  BEGIN  
  
    DECLARE @Code  NVARCHAR(MAX)  
  
    SELECT @Code = dbo.fn_CodeFormat(  
    MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))  
    )  
    FROM [AFSupplierLedger]  
    WHERE YEAR(CreatedOn) = YEAR(GETDATE())  
    AND MONTH(CreatedOn) = MONTH(GETDATE())  
    AND CompanyId = @CompanyId;  
  
    INSERT INTO [AFSupplierLedger]  
    (  
     [GuID],  
     [Code],  
     [LocationId],  
     [TransactionDate],  
     [SupplierId],  
     [RefDocumentType],  
     [RefDocumentId],  
     [Description],  
     [Debit],  
     [Credit],  
     [ReconcillationStatus],  
     [CreatedOn],  
     [CreatedBy],  
     [DocumentType],  
     [DocumentStatus],  
     [Status],  
     [BranchId],  
     [CompanyId]  
    )  
    SELECT   
  
     [GuID],  
     @Code,  
     [LocationId],  
     [TransactionDate],  
     [SupplierId],  
     [RefDocumentType],  
     [RefDocumentId],  
     [Description],  
     [Debit],  
     [Credit],  
     [ReconcillationStatus],  
     [CreatedOn],  
     [CreatedBy],  
     [DocumentType],  
     [DocumentStatus],  
     [Status],  
     [BranchId],  
     [CompanyId]  
    FROM @AFSupplierLedger_TVP  
   SET @DocumentCode = @Code  
   SET @InsertedId  = SCOPE_IDENTITY()  
   SET @Response  = 201;  
  END  
END TRY  
  
BEGIN CATCH  
 SET @DocumentCode = 'N/A'  
 SET @InsertedId  = 0  
 SET @Response  = 400  
END CATCH  
GO
/****** Object:  StoredProcedure [dbo].[CSDepartment_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[CSDepartment_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@Description			NVARCHAR(MAX),
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [CSDepartment]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[CSDepartment]
			(
				GuID,
				Code,
				Description,
				CreatedOn,
				CreatedBy,
				DocumentType,
				DocumentStatus,
				Status,
				BranchId,
				CompanyId
			)
			VALUES
			(
				@GuID,
				@Code,
				@Description,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
			)

			SET @Response = 201;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[CSDepartment]
			SET
				Description			= @Description,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID

			SET @Response = 202;
		END

	END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[IAdjustment_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IAdjustment_Upsert]   
    @DB_OperationType               NVARCHAR(MAX),  
    @GuID                           UNIQUEIDENTIFIER,  
    @LocationId                     INT,  
    @TransactionDate                DATETIME,  
    @Description                    NVARCHAR(MAX),  
    @AdjustmentTypeId               INT,  
    @AdjustmentStatus               INT,  
    @CreatedOn                      DATETIME,  
    @CreatedBy                      INT,  
    @UpdatedOn                      DATETIME,  
    @UpdatedBy                      INT,  
    @DocumentType                   INT,  
    @DocumentStatus                 INT,  
    @Status                         BIT,  
    @BranchId                       INT,  
    @CompanyId                      INT,  
    @IAdjustmentPPQD_TVP            [dbo].[IAdjustmentPPQD_TVP] READONLY,  
    @DocumentCode                   NVARCHAR(MAX) OUTPUT,  
    @InsertedId                     INT OUTPUT,  
    @Response                       INT OUTPUT  
AS  
BEGIN TRY  
  
    IF @DB_OperationType = 'INSERT_DATA_INTO_DB'  
    BEGIN  
        DECLARE @AdjustmentId INT;  
        DECLARE @Code NVARCHAR(MAX);  
        
        -- INSERT INTO PRODUCT COMBINATION ENGINE
        DECLARE @CombinationInput dbo.ProductCombination_TVP;
        INSERT INTO @CombinationInput (ProductId, Attribute, [Status])
        SELECT DISTINCT ProductId, Attribute, [Status]
        FROM @IAdjustmentPPQD_TVP;
        DECLARE @MappedCombinations TABLE (
            ProductCombinationId INT,
            ProductId     INT,
            Attribute     NVARCHAR(MAX)
        );
        INSERT INTO @MappedCombinations (ProductCombinationId, ProductId, Attribute)
        EXEC dbo.ProductCombination_Merge 
                                    @RefDocumentType = @DocumentType, 
                                    @Combinations    = @CombinationInput; 


        SELECT @Code = dbo.fn_CodeFormat(  
                         MAX(CAST(SUBSTRING(Code, 8, LEN(Code)) AS BIGINT))  
                       )  
        FROM IAdjustment  
        WHERE YEAR(CreatedOn) = YEAR(GETDATE())  
          AND MONTH(CreatedOn) = MONTH(GETDATE())  
          AND CompanyId = @CompanyId;  
  
        INSERT INTO IAdjustment  
        (  
            [GuID],  
            [Code],   
            [LocationId],   
            [TransactionDate],   
            [Description],   
            [AdjustmentTypeId],  
            [AdjustmentStatus],   
            [CreatedOn],   
            [CreatedBy],   
            [DocumentType],   
            [DocumentStatus],   
            [Status],  
            [BranchId],   
            [CompanyId]  
         )  
        VALUES  
        (     
            @GuID,  
            @Code,  
            @LocationId,   
            @TransactionDate,   
            @Description,   
            @AdjustmentTypeId,  
            @AdjustmentStatus,   
            @CreatedOn,  
            @CreatedBy,  
            @DocumentType,   
            @DocumentStatus,   
            @Status,   
            @BranchId,   
            @CompanyId  
        );  
  
        SET @AdjustmentId = SCOPE_IDENTITY();  
  


        INSERT INTO IAdjustmentPPQD  
        (  
            [GuID],   
            [AdjustmentId],   
            [ProductId],   
            [ProductCombinationId],   
            [UnitPurchasePrice],  
            [UnitSalePrice],  
            [QuantityIn],  
            [QuantityOut],  
            [Batch],
            [ExpiryDate],
            [CreatedOn],  
            [CreatedBy],  
            [DocumentType],  
            [DocumentStatus],  
            [Status]  
        )  
        SELECT  
            [GuID],   
            @AdjustmentId,   
            t.[ProductId],   
            mc.ProductCombinationId,     -- mapped from merge result
            [UnitPurchasePrice],  
            [UnitSalePrice],  
            [QuantityIn],  
            [QuantityOut],  
            [Batch],
            [ExpiryDate],
            @CreatedOn,  
            @CreatedBy,  
            [DocumentType],  
            [DocumentStatus],  
            [Status]  
              
            FROM @IAdjustmentPPQD_TVP t
    INNER JOIN @MappedCombinations mc
    ON mc.ProductId = t.ProductId
    AND mc.Attribute = t.Attribute;
  
        SET @DocumentCode = @Code;  
        SET @InsertedId  = @AdjustmentId;  
        SET @Response = 201;  
    END  
  
END TRY  
BEGIN CATCH
    SET @DocumentCode = 'N/A';
    SET @InsertedId = 0;
    SET @Response = 400;
    
    -- ADD THESE to see the actual error
    SELECT 
        ERROR_NUMBER()    AS ErrorNumber,
        ERROR_MESSAGE()   AS ErrorMessage,
        ERROR_LINE()      AS ErrorLine,
        ERROR_PROCEDURE() AS ErrorProcedure;
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[IBrand_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[IBrand_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@Description			NVARCHAR(MAX),
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [IBrand]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[IBrand]
			(
				GuID,
				Code,
				Description,
				CreatedOn,
				CreatedBy,
				DocumentType,
				DocumentStatus,
				Status,
				BranchId,
				CompanyId
			)
			VALUES
			(
				@GuID,
				@Code,
				@Description,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
			)

			SET @Response = 201;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[IBrand]
			SET
				Description			= @Description,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID

			SET @Response = 202;
		END

	END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ICategory_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[ICategory_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@Description			NVARCHAR(MAX),
	@DepartmentId			INT,
	@SectionId				INT,
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [ICategory]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[ICategory]
			(
				GuID,
				Code,
				Description,
				DepartmentId,
				SectionId,
				CreatedOn,
				CreatedBy,
				DocumentType,
				DocumentStatus,
				Status,
				BranchId,
				CompanyId
			)
			VALUES
			(
				@GuID,
				@Code,
				@Description,
				@DepartmentId,
				@SectionId,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
			)

			SET @Response = 201;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[ICategory]
			SET
				Description			= @Description,
				DepartmentId		= @DepartmentId,
				SectionId			= @SectionId,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID

			SET @Response = 202;
		END

	END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[IProduct_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IProduct_Upsert]    
 @DB_OperationType      NVARCHAR(MAX),    
 @GuID                  UNIQUEIDENTIFIER,    
 @Description           NVARCHAR(MAX),    
 @MachineNumber         NVARCHAR(MAX),    
 @SKU                   NVARCHAR(MAX),    
 @AdditionalDetail      NVARCHAR(MAX),    
 @AttributeIds          NVARCHAR(MAX),  
 @BrandId               INT,    
 @ProductTypeId         INT,
 @IsFavorite            BIT,    
 @IsSaleTaxExclusive    BIT,    
 @DepartmentId          INT,    
 @SectionId             INT,    
 @CategoryId            INT,    
 @SubCategoryId         INT,    
 @IsExpiryApplicable    BIT,    
 @CriticalLimit         DECIMAL(18, 2),    
 @SaleUnitId            INT,    
 @CreatedOn             DATETIME,    
 @CreatedBy             INT,    
 @UpdatedOn             DATETIME,    
 @UpdatedBy             INT,    
 @DocumentType          INT,    
 @DocumentStatus        INT,    
 @Status                BIT,    
 @BranchId              INT,    
 @CompanyId             INT,    
 @InsertedId            INT OUTPUT,    
 @Response              INT OUTPUT    
    
AS    
BEGIN    
 SET NOCOUNT ON;    
    
 BEGIN TRY    
    
  IF @DB_OperationType = 'INSERT_DATA_INTO_DB'    
  BEGIN    
   DECLARE @Code NVARCHAR(MAX)    
    
   SELECT @Code = dbo.fn_CodeFormat(    
    MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))    
   )    
   FROM [IProduct]    
   WHERE YEAR(CreatedOn) = YEAR(GETDATE())    
     AND MONTH(CreatedOn) = MONTH(GETDATE())    
     AND CompanyId = @CompanyId;    
       
   INSERT INTO [dbo].[IProduct]    
   (    
    [GuID],    
    [Code],    
    [Description],    
    [MachineNumber],    
    [SKU],    
    [AdditionalDetail],    
    [AttributeIds],    
    [BrandId],  
    [ProductTypeId],
    [IsFavorite],    
    [IsSaleTaxExclusive],   
    [DepartmentId],    
    [SectionId],    
    [CategoryId],    
    [SubCategoryId],  
    [IsExpiryApplicable],    
    [CriticalLimit],    
    [SaleUnitId],    
    [CreatedOn],    
    [CreatedBy],    
    [DocumentType],    
    [DocumentStatus],    
    [Status],    
    [BranchId],    
    [CompanyId]    
   )    
   VALUES    
   (    
    @GuID,    
    @Code,    
    @Description,    
    @MachineNumber,    
    @SKU,    
    @AdditionalDetail,    
    @AttributeIds,    
    @BrandId,    
    @ProductTypeId,
    @IsFavorite,    
    @IsSaleTaxExclusive,    
    @DepartmentId,    
    @SectionId,    
    @CategoryId,    
    @SubCategoryId,    
    @IsExpiryApplicable,    
    @CriticalLimit,    
    @SaleUnitId,    
    @CreatedOn,    
    @CreatedBy,    
    @DocumentType,    
    @DocumentStatus,    
    @Status,    
    @BranchId,    
    @CompanyId    
   )    
   SET @InsertedId = SCOPE_IDENTITY();    
   SET @Response = 201;    
    
  END    
 END TRY    
 BEGIN CATCH    
  SET @Response = 400;    
  SET @InsertedId = 0;    
    
 END CATCH    
END 
GO
/****** Object:  StoredProcedure [dbo].[IProductATI_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[IProductATI_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@ProductId				INT,
	@InventoryAccountId		INT,
	@SaleRevenueAccountId	INT,
	@CostOfSaleAccountId	INT,
	@ItemTypeId				INT,
	@HSCodeId				INT,
	@SaleTaxTypeId			INT,
	@CostingModeId			INT,
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			INSERT INTO [dbo].[IProductATI]
			(
				[GuID],
				[ProductId], 
				[InventoryAccountId], 
				[SaleRevenueAccountId],
				[CostOfSaleAccountId],
				[ItemTypeId],
				[HSCodeId],
				[SaleTaxTypeId],
				[CostingModeId],
				[CreatedOn],
				[CreatedBy],
				[DocumentType],
				[DocumentStatus],
				[Status]
			)
			VALUES
			(
				@GuID,
				@ProductId,
				@InventoryAccountId,
				@SaleRevenueAccountId,
				@CostOfSaleAccountId,
				@ItemTypeId,
				@HSCodeId,
				@SaleTaxTypeId,
				@CostingModeId,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status
			)
			SET @Response = 201;
		END
		END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ISection_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[ISection_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@Description			NVARCHAR(MAX),
	@DepartmentId			INT,
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [ISection]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[ISection]
			(
				GuID,
				Code,
				Description,
				DepartmentId,
				CreatedOn,
				CreatedBy,
				DocumentType,
				DocumentStatus,
				Status,
				BranchId,
				CompanyId
			)
			VALUES
			(
				@GuID,
				@Code,
				@Description,
				@DepartmentId,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
			)

			SET @Response = 201;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[ISection]
			SET
				Description			= @Description,
				DepartmentId		= @DepartmentId,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID

			SET @Response = 202;
		END

	END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ISubCategory_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE    PROCEDURE [dbo].[ISubCategory_Upsert]
	@DB_OperationType		NVARCHAR(MAX),
	@GuID					UNIQUEIDENTIFIER,
	@Description			NVARCHAR(MAX),
	@DepartmentId			INT,
	@SectionId				INT,
	@CategoryId				INT,
	@CreatedOn				DATETIME,
	@CreatedBy				INT,
	@UpdatedOn				DATETIME,
	@UpdatedBy				INT,
	@DocumentType			INT,
	@DocumentStatus			INT,
	@Status					BIT,
	@BranchId				INT,
	@CompanyId				INT,
	@Response				INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		IF @DB_OperationType = 'INSERT_DATA_INTO_DB'
		BEGIN
			DECLARE @Code NVARCHAR(MAX)

			SELECT @Code = dbo.fn_CodeFormat(
				MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))
			)
			FROM [ISubCategory]
			WHERE YEAR(CreatedOn) = YEAR(GETDATE())
			  AND MONTH(CreatedOn) = MONTH(GETDATE())
			  AND CompanyId = @CompanyId;

			INSERT INTO [dbo].[ISubCategory]
			(
				GuID,
				Code,
				Description,
				DepartmentId,
				SectionId,
				CategoryId,
				CreatedOn,
				CreatedBy,
				DocumentType,
				DocumentStatus,
				Status,
				BranchId,
				CompanyId
			)
			VALUES
			(
				@GuID,
				@Code,
				@Description,
				@DepartmentId,
				@SectionId,
				@CategoryId,
				@CreatedOn,
				@CreatedBy,
				@DocumentType,
				@DocumentStatus,
				@Status,
				@BranchId,
				@CompanyId
			)

			SET @Response = 201;
		END

		ELSE IF @DB_OperationType = 'UPDATE_DATA_INTO_DB'
		BEGIN
			UPDATE [dbo].[ISubCategory]
			SET
				Description			= @Description,
				DepartmentId		= @DepartmentId,
				SectionId			= @SectionId,
				CategoryId			= @CategoryId,
				UpdatedOn			= @UpdatedOn,
				UpdatedBy			= @UpdatedBy
			WHERE GuID = @GuID

			SET @Response = 202;
		END

	END TRY
	BEGIN CATCH
		SET @Response = 400;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ProductCombination_Merge]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductCombination_Merge]
    @RefDocumentType INT,
    @Combinations dbo.ProductCombination_TVP READONLY

AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OutputTable TABLE (
        CombinationId INT,
        ProductId     INT,
        Attribute     NVARCHAR(MAX)
    );

    MERGE dbo.osvProductCombination AS Target
    USING (
        SELECT DISTINCT ProductId, Attribute, [Status]
        FROM @Combinations
    ) AS Source
    ON (
        Target.ProductId = Source.ProductId
        AND Target.Attribute = Source.Attribute
    )
    WHEN MATCHED THEN


        UPDATE SET Target.Status = Source.Status
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (GuID,RefDocumentType, ProductId, Attribute, Status)
        VALUES (NEWID(), @RefDocumentType, Source.ProductId, Source.Attribute, Source.Status)
    OUTPUT INSERTED.Id, INSERTED.ProductId, INSERTED.Attribute
    INTO @OutputTable;

    INSERT INTO @OutputTable (CombinationId, ProductId, Attribute)
    SELECT pc.Id, pc.ProductId, pc.Attribute
    FROM dbo.osvProductCombination pc
    INNER JOIN (
        SELECT DISTINCT ProductId, Attribute FROM @Combinations
    ) c ON pc.ProductId = c.ProductId AND pc.Attribute = c.Attribute
    WHERE NOT EXISTS (
        SELECT 1 FROM @OutputTable o
        WHERE o.ProductId = pc.ProductId AND o.Attribute = pc.Attribute
    );

    SELECT CombinationId, ProductId, Attribute FROM @OutputTable;
END;
GO
/****** Object:  StoredProcedure [dbo].[PSupplier_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE    PROCEDURE  [dbo].[PSupplier_Upsert]   
 @DB_OperationType  NVARCHAR(MAX),  
 @GuID              UNIQUEIDENTIFIER,  
 @Description       NVARCHAR(MAX),  
 @Contact           NVARCHAR(MAX),  
 @Email             NVARCHAR(MAX),  
 @CNICNumber        NVARCHAR(MAX),  
 @Address           NVARCHAR(MAX),  
 @AdditionalDetail  NVARCHAR(MAX),  
 @PayableAccountId  INT,  
 @OpeningBalance    DECIMAL(18,2),  
 @CreatedOn         DATETIME,  
 @CreatedBy         INT,  
 @UpdatedOn         DATETIME,  
 @UpdatedBy         INT,  
 @DocumentType      INT,  
 @DocumentStatus    INT,  
 @Status            BIT,  
 @BranchId          INT,  
 @CompanyId         INT,  
 @InsertedId        INT OUTPUT,  
 @Response          INT OUTPUT  
AS  
BEGIN  
 SET NOCOUNT ON;  
  
 BEGIN TRY  
  IF @DB_OperationType = 'INSERT_DATA_INTO_DB'  
  BEGIN  
   DECLARE @Code NVARCHAR(MAX)  
  
   SELECT @Code = dbo.fn_CodeFormat(  
    MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))  
   )  
   FROM [PSupplier]  
   WHERE YEAR(CreatedOn) = YEAR(GETDATE())  
     AND MONTH(CreatedOn) = MONTH(GETDATE())  
     AND CompanyId = @CompanyId;  
  
   INSERT [dbo].[PSupplier]  
   (  
    [GuID],  
    [Code],  
    [Description],    
    [Contact],      
    [Email],      
    [CNICNumber],     
    [Address],      
    [AdditionalDetail],   
    [PayableAccountId],   
    [OpeningBalance],  
    [CreatedOn],     
    [CreatedBy],     
    [DocumentType],  
    [DocumentStatus],  
    [Status],  
    [BranchId],  
    [CompanyId]  
   )  
   VALUES(  
    @GuID,   
    @Code,  
    @Description,     
    @Contact,     
    @Email,      
    @CNICNumber,     
    @Address,      
    @AdditionalDetail,   
    @PayableAccountId,  
    @OpeningBalance,  
    @CreatedOn,     
    @CreatedBy,     
    @DocumentType,  
    @DocumentStatus,  
    @Status,     
    @BranchId,     
    @CompanyId    
   )  
   SET @InsertedId = SCOPE_IDENTITY();  
   SET @Response = 201;  
  
  END  
  
 END TRY  
 BEGIN CATCH  
  SET @InsertedId = 0;  
  SET @Response = 400;  
  
 END CATCH  
END  
GO
/****** Object:  StoredProcedure [dbo].[RptCustomerSummary_GLBParam]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RptCustomerSummary_GLBParam]    
    @BranchId           INT,    
    @CompanyId          INT,    
    @PaymentStatusIds   NVARCHAR(MAX),    
    @InvoiceStatusIds   NVARCHAR(MAX),    
    @DocumentStatusIds  NVARCHAR(MAX)    
AS    
    
    WITH PaymentRecieptCTE AS (    
        SELECT     
            CustomerId,     
            SUM(ReceiptAmount) AS TotalReciept    
    
        FROM [dbo].[AFInvoiceReceipt] AS PR WITH(NOLOCK)    
        WHERE     
            PR.[Status] = 1     
        AND PR.[DocumentStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))      
        AND PR.[PaymentStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@PaymentStatusIds))      
        GROUP BY CustomerId    
    ),    
    InvoiceCTE AS (    
        SELECT     
                CustomerId,     
                SUM(DueAmount) AS TotalDue,    
                SUM(dbo.fn_CalculateLineTotal(ActualAmount, DiscountAmount, ProductId, 0))  AS TotalRecievable    
    
            FROM [dbo].[AFInvoice]          AS I    WITH(NOLOCK)    
            INNER JOIN [dbo].[AFInvoicePPI] AS PPI  WITH(NOLOCK) ON PPI.InvoiceId =I.Id AND PPI.Status=1    
            WHERE     
    
                I.[Status] = 1     
            AND I.[DocumentStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))      
            AND I.[InvoiceStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@InvoiceStatusIds))      
            GROUP BY CustomerId    
    )    
    SELECT     
        C.[Id] AS [CustomerId],    
        C.[Code],    
        C.[Description],    
        C.[Contact],    
        ISNULL(IC.[TotalRecievable], 0)                         AS [Receivable],    
        ISNULL(PRC.[TotalReciept], 0)                           AS [Receipt],  
        ISNULL(IC.[TotalRecievable] - PRC.[TotalReciept], 0)    AS [Due]    
    
    FROM [dbo].[SOCustomer]     AS C    WITH(NOLOCK)    
    LEFT JOIN InvoiceCTE        AS IC   WITH(NOLOCK) ON C.Id = IC.CustomerId    
    LEFT JOIN PaymentRecieptCTE AS PRC  WITH(NOLOCK) ON C.Id = PRC.CustomerId    
    WHERE C.[Status]=1    
    AND C.CompanyId =@CompanyId    
    AND C.BranchId =@BranchId 
GO
/****** Object:  StoredProcedure [dbo].[RptInventoryAdjustment_GLBParam]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RptInventoryAdjustment_GLBParam]  
	@AdjustmentStatusIds	NVARCHAR(MAX),  
	@DocumentStatusIds		NVARCHAR(MAX),  
	@LocationId				INT,  
	@BranchId				INT,  
	@CompanyId				INT,   
	@ProductId				INT  
AS  
  
SELECT   
	IA.[TransactionDate],
	IA.[Code],
	IA.[Description],  
	IA.[QuantityIn],  
	IA.[QuantityOut],  
	IA.[UnitPurchasePrice],
	IA.[UnitSalePrice],
	IA.[GuID],  
	IA.[Id],
	B.[Id] AS [LocationId],
	B.[Description] AS [LocationId]

  
FROM		[dbo].[IInventoryAdjustment]	AS IA WITH(NOLOCK)  
INNER JOIN	[dbo].[ACBranch]				AS B  WITH(NOLOCK)  ON IA.LocationId = B.Id
WHERE   
	IA.[Status]   = 1  
AND IA.[DocumentStatus]		IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))  
AND IA.[AdjustmentStatus]	IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@AdjustmentStatusIds))
AND (@ProductId = 0			OR IA.ProductId = @ProductId)   
ORDER BY IA.[TransactionDate] 

GO
/****** Object:  StoredProcedure [dbo].[RptInvoiceReceipt_GLBParam]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RptInvoiceReceipt_GLBParam]    
 @PaymentStatusIds NVARCHAR(MAX),    
 @DocumentStatusIds NVARCHAR(MAX),    
 @BranchId   INT,    
 @CompanyId   INT,     
 @CustomerId   INT    
AS    
    
SELECT     
    
 PR.[TransactionDate],    
 PR.[Code],    
 PR.[Description],    
 PR.[ReceiptAmount],    
 PR.[GuID],    
 PR.[Id]    
    
FROM [dbo].[AFInvoiceReceipt] AS PR WITH(NOLOCK)    
WHERE     
 PR.[Status]   = 1    
AND PR.[DocumentStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))    
AND PR.[PaymentStatus]  IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@PaymentStatusIds))  
AND (@CustomerId = 0 OR PR.CustomerId = @CustomerId)     
ORDER BY PR.[TransactionDate] 
GO
/****** Object:  StoredProcedure [dbo].[RptSaleLedger_GLBParam]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RptSaleLedger_GLBParam]  
	@BranchId					INT,  
	@CompanyId					INT,  
	@DocumentStatusIds			NVARCHAR(MAX),  
	@ReconcillationStatusIds	NVARCHAR(MAX),  
	@CustomerId					INT,  
	@StartDate					DATETIME,  
	@EndDate					DATETIME  
AS  
SELECT 
	CL.[Code],
	CL.[TransactionDate],
	CL.[Description],
	CL.[Debit],
	CL.[Credit],
	    SUM(CL.[Debit] - CL.[Credit]) OVER (
        ORDER BY CL.[TransactionDate], CL.[Code]
        ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
    ) AS [RunningBalance]
FROM [dbo].[AFCustomerLedger] AS CL WITH(NOLOCK)
WHERE 
	CL.[Status]					=	1
AND CL.[DocumentStatus]			IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))      
AND CL.[ReconcillationStatus]	IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds)) 
AND (@CustomerId = 0 OR CL.[CustomerId] = @CustomerId)

ORDER BY 
--CL.CustomerId,
TransactionDate
     
GO
/****** Object:  StoredProcedure [dbo].[SOCustomer_Upsert]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE    PROCEDURE  [dbo].[SOCustomer_Upsert]   
 @DB_OperationType  NVARCHAR(MAX),  
 @GuID     UNIQUEIDENTIFIER,  
 @Description   NVARCHAR(MAX), 
 @TierTypeId INT,
 @Contact    NVARCHAR(MAX),  
 @Email     NVARCHAR(MAX),  
 @CNICNumber    NVARCHAR(MAX),  
 @Address    NVARCHAR(MAX),  
 @AdditionalDetail  NVARCHAR(MAX),  
 @ReceivableAccountId INT,  
 @OpeningBalance   DECIMAL(18,2),  
 @CreatedOn    DATETIME,  
 @CreatedBy    INT,  
 @UpdatedOn    DATETIME,  
 @UpdatedBy    INT,  
 @DocumentType   INT,  
 @DocumentStatus   INT,  
 @Status     BIT,  
 @BranchId    INT,  
 @CompanyId    INT,  
 @InsertedId    INT OUTPUT,  
 @Response    INT OUTPUT  
AS  
BEGIN  
 SET NOCOUNT ON;  
  
 BEGIN TRY  
  IF @DB_OperationType = 'INSERT_DATA_INTO_DB'  
  BEGIN  
   DECLARE @Code NVARCHAR(MAX)  
  
   SELECT @Code = dbo.fn_CodeFormat(  
    MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))  
   )  
   FROM [SOCustomer]  
   WHERE YEAR(CreatedOn) = YEAR(GETDATE())  
     AND MONTH(CreatedOn) = MONTH(GETDATE())  
     AND CompanyId = @CompanyId;  
  
   INSERT [dbo].[SOCustomer]  
   (  
    [GuID],  
    [Code],  
    [Description], 
    [TierTypeId],
    [Contact],      
    [Email],      
    [CNICNumber],     
    [Address],      
    [AdditionalDetail],   
    [ReceivableAccountId],   
    [OpeningBalance],  
    [CreatedOn],     
    [CreatedBy],     
    [DocumentType],  
    [DocumentStatus],  
    [Status],  
    [BranchId],  
    [CompanyId]  
   )  
   VALUES(  
    @GuID,   
    @Code,  
    @Description,
    @TierTypeId,
    @Contact,     
    @Email,      
    @CNICNumber,     
    @Address,      
    @AdditionalDetail,   
    @ReceivableAccountId,  
    @OpeningBalance,  
    @CreatedOn,     
    @CreatedBy,     
    @DocumentType,  
    @DocumentStatus,  
    @Status,     
    @BranchId,     
    @CompanyId    
   )  
   SET @InsertedId = SCOPE_IDENTITY();  
   SET @Response = 201;  
  
  END  
  
 END TRY  
 BEGIN CATCH  
  SET @InsertedId = 0;  
  SET @Response = 400;  
  
 END CATCH  
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOrCreateProductCombinations]    Script Date: 5/27/2026 7:19:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_GetOrCreateProductCombinations]
    @Combinations dbo.ProductCombination_TVP READONLY
AS
BEGIN
    SET NOCOUNT ON;

    -- Directly return the merged/inserted dataset to the calling block
    MERGE dbo.osvProductCombination AS Target
    USING (
        SELECT DISTINCT ProductId, Attribute, [Status] 
        FROM @Combinations
    ) AS Source
    ON (Target.ProductId = Source.ProductId 
        AND Target.Attribute = Source.Attribute) 
    
    WHEN MATCHED THEN
        UPDATE SET Target.Status = Source.Status

    WHEN NOT MATCHED BY TARGET THEN
        INSERT (GuID, ProductId, Attribute, Status)
        VALUES (NEWID(), Source.ProductId, Source.Attribute, Source.Status)
        
    OUTPUT INSERTED.Id, INSERTED.ProductId, INSERTED.Attribute;
END;
GO
