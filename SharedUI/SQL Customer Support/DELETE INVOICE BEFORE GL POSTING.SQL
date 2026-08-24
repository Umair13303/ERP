USE [ERPOrganisationSetup]
GO
/****** Object:  FullTextCatalog [db46757]    Script Date: 8/25/2026 12:02:11 AM ******/
CREATE FULLTEXT CATALOG [db46757] AS DEFAULT
GO
/****** Object:  UserDefinedTableType [dbo].[AFBillProduct_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
CREATE TYPE [dbo].[AFBillProduct_TVP] AS TABLE(
	[Id] [int] NULL,
	[GuID] [uniqueidentifier] NULL,
	[BillId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductATIId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[UnitPurchasePrice] [decimal](18, 2) NULL,
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
/****** Object:  UserDefinedTableType [dbo].[AFCustomerLedger_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
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
/****** Object:  UserDefinedTableType [dbo].[AFInventoryLedger_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
CREATE TYPE [dbo].[AFInventoryLedger_TVP] AS TABLE(
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[RefDocumentDetailGuID] [uniqueidentifier] NULL,
	[Description] [nvarchar](max) NULL,
	[QuantityIn] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[QuantityOut] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[UnitPurchasePrice] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[UnitSalePrice] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Debit] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Credit] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[ReceiptBalanceQuantity] [decimal](18, 2) NULL,
	[RemainingStockQuantity] [decimal](18, 2) NULL,
	[ConsumedInventoryLedgerId] [int] NULL,
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
/****** Object:  UserDefinedTableType [dbo].[AFInvoiceProduct_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
CREATE TYPE [dbo].[AFInvoiceProduct_TVP] AS TABLE(
	[Id] [int] NULL,
	[GuID] [uniqueidentifier] NULL,
	[InvoiceId] [int] NULL,
	[ProductPriceLogId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductATIId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[UnitSalePrice] [decimal](18, 2) NULL,
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
/****** Object:  UserDefinedTableType [dbo].[AFJournalVoucher_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
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
/****** Object:  UserDefinedTableType [dbo].[AFSupplierLedger_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
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
/****** Object:  UserDefinedTableType [dbo].[IAdjustmentPPQD_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
CREATE TYPE [dbo].[IAdjustmentPPQD_TVP] AS TABLE(
	[GuID] [uniqueidentifier] NULL,
	[AdjustmentId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductATIId] [int] NULL,
	[ProductCombinationId] [int] NULL,
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
/****** Object:  UserDefinedTableType [dbo].[IProductStockSummary_Variant_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
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
/****** Object:  UserDefinedTableType [dbo].[PPurchasePPQD_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
CREATE TYPE [dbo].[PPurchasePPQD_TVP] AS TABLE(
	[GuID] [uniqueidentifier] NULL,
	[ProductId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[Quantity] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[UnitPurchasePrice] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[UnitSalePrice] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL DEFAULT ((1))
)
GO
/****** Object:  UserDefinedTableType [dbo].[ProductCombination_TVP]    Script Date: 8/25/2026 12:02:11 AM ******/
CREATE TYPE [dbo].[ProductCombination_TVP] AS TABLE(
	[ProductId] [int] NULL,
	[Attribute] [nvarchar](max) NULL,
	[Status] [bit] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculateLineTotalByATI]    Script Date: 8/25/2026 12:02:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CalculateLineTotalByATI]
(
    @ActualAmount   DECIMAL(18,2),
    @DiscountAmount DECIMAL(18,2),
    @ProductATIId   INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    -- Opening-balance / non-product lines carry no ATI (ProductATIId is NULL by design —
    -- see SaleOperationUpsertService / ProcurementUpsertService OB construction).
    -- No tax configuration applies to these; return the raw net amount directly,
    -- with no dependency on retProductConfigByATI returning/not-returning rows.
    IF @ProductATIId IS NULL OR @ProductATIId = 0
        RETURN (@ActualAmount - @DiscountAmount);

    DECLARE @Total DECIMAL(18,2);
    SELECT @Total = 
        CASE 
            WHEN ISNULL(PC.IsSaleTaxExclusive, 0) = 1 
            THEN (@ActualAmount - @DiscountAmount) * (1.0 + (ISNULL(PC.DefaultRate,0) + ISNULL(PC.AdditionalRate,0)) / 100.0)
            ELSE (@ActualAmount - @DiscountAmount) 
        END
    FROM [dbo].[retProductConfigByATI](@ProductATIId) AS PC;

    RETURN ISNULL(@Total, (@ActualAmount - @DiscountAmount));
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculateLineTotalByPriceLog]    Script Date: 8/25/2026 12:02:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CalculateLineTotalByPriceLog]
(
    @ActualAmount DECIMAL(18,2),
    @DiscountAmount DECIMAL(18,2),
    @ProductPriceLogId INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Total DECIMAL(18,2);
    SELECT @Total = 
        CASE 
            WHEN ISNULL(PC.IsSaleTaxExclusive, 0) = 1 
            THEN (@ActualAmount - @DiscountAmount) * (1.0 + (ISNULL(PC.DefaultRate,0)+ISNULL(PC.AdditionalRate,0))/100.0)
            ELSE (@ActualAmount - @DiscountAmount) 
        END
    FROM [dbo].[retProductConfigByPriceLog](@ProductPriceLogId) AS PC;
    RETURN ISNULL(@Total, (@ActualAmount - @DiscountAmount));
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CodeFormat]    Script Date: 8/25/2026 12:02:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE FUNCTION [dbo].[fn_CodeFormat]  (   @MaxCode bigint     )  RETURNS nvarchar(max)  AS  BEGIN   DECLARE @Code nvarchar(max);    select  @Code=convert(nvarchar(max), getdate(), 12)+'-'+format(isnull(@MaxCode,0)+1,'0000')   RETURN  @Code;  END  
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CodeSplitByCommaAsINT]    Script Date: 8/25/2026 12:02:11 AM ******/
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
/****** Object:  UserDefinedFunction [dbo].[fn_InvoiceStatus]    Script Date: 8/25/2026 12:02:11 AM ******/
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
/****** Object:  Table [dbo].[AFInvoicePPI]    Script Date: 8/25/2026 12:02:11 AM ******/
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
	[ProductATIId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitSalePrice] [decimal](18, 2) NOT NULL,
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
/****** Object:  Table [dbo].[IProduct]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[IProductATI]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vHSCode]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vItemType]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vSaleTaxType]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  UserDefinedFunction [dbo].[retProductConfigByATI]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[retProductConfigByATI]
(
    @ProductATIId INT
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
    FROM IProductATI AS ATI WITH(NOLOCK)
    INNER JOIN IProduct     AS P   WITH(NOLOCK) ON P.Id = ATI.ProductId
    INNER JOIN vItemType    AS IT  WITH(NOLOCK) ON ATI.ItemTypeId = IT.Id
    INNER JOIN vHSCode      AS HS  WITH(NOLOCK) ON ATI.HSCodeId = HS.Id
    INNER JOIN vSaleTaxType AS STT WITH(NOLOCK) ON ATI.SaleTaxTypeId = STT.Id
    WHERE ATI.Id = @ProductATIId
);
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetInvoiceCalculations]    Script Date: 8/25/2026 12:02:12 AM ******/
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
        P.Id AS PPIId, 
        CAST(SUM(P.ActualAmount) AS DECIMAL(18,2))   AS GrossAmount,
        CAST(SUM(P.DiscountAmount) AS DECIMAL(18,2)) AS DiscountAmount,
        CAST(SUM(
            CASE WHEN ISNULL(PC.IsSaleTaxExclusive,0) = 1 
                 THEN (P.ActualAmount - P.DiscountAmount) 
                 ELSE (P.ActualAmount - P.DiscountAmount) / (1.0 + (ISNULL(PC.DefaultRate,0) + ISNULL(PC.AdditionalRate,0)) / 100.0)
            END
        ) AS DECIMAL(18,2)) AS TaxableAmount,
        CAST(SUM(
            CASE WHEN ISNULL(PC.IsSaleTaxExclusive,0) = 1 
                 THEN (P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.DefaultRate,0) / 100.0)
                 ELSE ((P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.DefaultRate,0) / 100.0)) 
                      / (1.0 + (ISNULL(PC.DefaultRate,0) + ISNULL(PC.AdditionalRate,0)) / 100.0)
            END
        ) AS DECIMAL(18,2)) AS SaleTaxAmount,
        CAST(SUM(
            CASE WHEN ISNULL(PC.IsSaleTaxExclusive,0) = 1 
                 THEN (P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.AdditionalRate,0) / 100.0)
                 ELSE ((P.ActualAmount - P.DiscountAmount) * (ISNULL(PC.AdditionalRate,0) / 100.0)) 
                      / (1.0 + (ISNULL(PC.DefaultRate,0) + ISNULL(PC.AdditionalRate,0)) / 100.0)
            END
        ) AS DECIMAL(18,2)) AS AdditionalTaxAmount,
        CAST(SUM(
            CASE WHEN ISNULL(PC.IsSaleTaxExclusive,0) = 1 
                 THEN (P.ActualAmount - P.DiscountAmount) * (1.0 + (ISNULL(PC.DefaultRate,0) + ISNULL(PC.AdditionalRate,0)) / 100.0)
                 ELSE (P.ActualAmount - P.DiscountAmount) 
            END
        ) AS DECIMAL(18,2)) AS NetAmount

    FROM [dbo].[AFInvoicePPI] AS P WITH(NOLOCK)
    OUTER APPLY [dbo].[retProductConfigByATI](P.ProductATIId) AS PC   -- CHANGED
    WHERE P.InvoiceId = @InvoiceId
    GROUP BY P.InvoiceId, P.Id
);
GO
/****** Object:  Table [dbo].[AFProductPriceLog]    Script Date: 8/25/2026 12:02:12 AM ******/
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
 CONSTRAINT [PK__AFProduc__3214EC07ACC0F658] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[retProductConfigByPriceLog]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[retProductConfigByPriceLog]
(
    @ProductPriceLogId INT
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
    FROM AFProductPriceLog AS PPL WITH(NOLOCK)
    INNER JOIN IProductATI  AS ATI WITH(NOLOCK) ON ATI.Id = PPL.ProductATIId
    INNER JOIN IProduct     AS P   WITH(NOLOCK) ON P.Id   = PPL.ProductId
    INNER JOIN vItemType    AS IT  WITH(NOLOCK) ON ATI.ItemTypeId = IT.Id
    INNER JOIN vHSCode      AS HS  WITH(NOLOCK) ON ATI.HSCodeId = HS.Id
    INNER JOIN vSaleTaxType AS STT WITH(NOLOCK) ON ATI.SaleTaxTypeId = STT.Id
    WHERE PPL.Id = @ProductPriceLogId
);
GO
/****** Object:  Table [dbo].[ACBranch]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[ACCompany]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[ACSaleUnit]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[ACUser]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[AFBill]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[AFBillPPI]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFBillPPI](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[BillId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductATIId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitPurchasePrice] [decimal](18, 2) NOT NULL,
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
/****** Object:  Table [dbo].[AFBillReceipt]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFBillReceipt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[SupplierId] [int] NULL,
	[BillId] [int] NULL,
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
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFChartOfAccount]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[AFCustomerLedger]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[AFGeneralLedger]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFGeneralLedger](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[LocationId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[PartyId] [int] NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentId] [int] NULL,
	[DocumentCode] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Debit] [decimal](18, 4) NOT NULL,
	[Credit] [decimal](18, 4) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[BranchId] [int] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_AFGeneralLedger] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFInventoryLedger]    Script Date: 8/25/2026 12:02:12 AM ******/
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
	[RefDocumentDetailGuID] [uniqueidentifier] NULL,
	[Description] [nvarchar](max) NULL,
	[QuantityIn] [decimal](18, 2) NOT NULL,
	[QuantityOut] [decimal](18, 2) NOT NULL,
	[UnitPurchasePrice] [decimal](18, 2) NOT NULL,
	[UnitSalePrice] [decimal](18, 2) NOT NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NOT NULL,
	[Batch] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NULL,
	[ReceiptBalanceQuantity] [decimal](18, 2) NULL,
	[RemainingStockQuantity] [decimal](18, 2) NULL,
	[ConsumedInventoryLedgerId] [int] NULL,
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
 CONSTRAINT [PK__AFInvent__3214EC07917C876B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AFInvoice]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[AFInvoiceReceipt]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[AFJournalVoucher]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[AFSupplierLedger]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[confApplicationRule]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[confApplicationRule](
	[Id] [int] NOT NULL,
	[ClientKEY] [int] NULL,
	[WalkInCustomerLimit] [int] NULL,
	[DefaultSaleMargin] [decimal](18, 2) NOT NULL,
	[IsWhatsAppInvoicing] [bit] NOT NULL,
	[IsRefundAdvice] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[confclientproductsetting]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[confclientproductsetting](
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
	[EnableDepartment] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[confClientSetting]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[confScheduledMessage]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[confScheduledMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefDocumentType] [int] NULL,
	[RefDocumentGuID] [uniqueidentifier] NULL,
	[ScheduledOn] [datetime] NULL,
	[Contact] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[IsSent] [bit] NULL,
	[APIResponse] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CSDefaultChartOfAccount]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSDefaultChartOfAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[Code] [nvarchar](max) NULL,
	[AccountCategoryId] [int] NULL,
	[DefaultChartOfAccountId] [int] NULL,
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
/****** Object:  Table [dbo].[CSDepartment]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[DocumentStatus]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentStatus](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DocumentTypeId] [int] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentType]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentType](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IAdjustment]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[IAdjustmentPPQD]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IAdjustmentPPQD](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[AdjustmentId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductATIId] [int] NULL,
	[ProductCombinationId] [int] NULL,
	[QuantityIn] [decimal](18, 0) NOT NULL,
	[QuantityOut] [decimal](18, 0) NOT NULL,
	[UnitPurchasePrice] [decimal](18, 2) NOT NULL,
	[UnitSalePrice] [decimal](18, 2) NOT NULL,
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
/****** Object:  Table [dbo].[IBrand]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[ICategory]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[IProductCCE]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IProductCCE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuID] [uniqueidentifier] NULL,
	[RefDocumentType] [int] NULL,
	[ProductId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[QRCode] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DocumentType] [int] NULL,
	[DocumentStatus] [int] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISection]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[ISubCategory]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[osvChartOfAccount]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[PSupplier]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[SOCustomer]    Script Date: 8/25/2026 12:02:12 AM ******/
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
	[IsWalkInCustomer] [bit] NULL,
	[Contact] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[CNICNumber] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[AdditionalDetail] [nvarchar](max) NULL,
	[ReceivableAccountId] [int] NULL,
	[OpeningBalance] [decimal](18, 2) NULL,
	[CreditLimit] [decimal](18, 2) NULL,
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
/****** Object:  Table [dbo].[UserRight]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vAccountCatagory]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vAccountType]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vAttribute]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vCity]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vCostingMode]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vCountry]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vFinancialStatement]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vInventoryAdjustmentType]    Script Date: 8/25/2026 12:02:12 AM ******/
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
	[Status] [bit] NULL,
	[AccountCategoryId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vOrganisationType]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vPaymentMethod]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vProductType]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vRight]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vRight](
	[Id] [int] NOT NULL,
	[Menu] [nvarchar](max) NULL,
	[SubMenu] [nvarchar](max) NULL,
	[FormName] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[Purpose] [nvarchar](max) NULL,
	[RoleIds] [nvarchar](max) NULL,
	[Priority] [int] NULL,
	[IsDisplayAllowed] [bit] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__vRight__3214EC071C1E2EA9] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vRole]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vTierType]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  Table [dbo].[vUnit]    Script Date: 8/25/2026 12:02:12 AM ******/
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
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF_AFBillPPI_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF_AFBillPPI_UnitPurchasePrice]  DEFAULT ((0)) FOR [UnitPurchasePrice]
GO
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF__AFBillPPI__Actua__75F77EB0]  DEFAULT ((0)) FOR [ActualAmount]
GO
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF__AFBillPPI__Disco__76EBA2E9]  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[AFBillPPI] ADD  CONSTRAINT [DF__AFBillPPI__Charg__77DFC722]  DEFAULT ((0)) FOR [ChargedAmount]
GO
ALTER TABLE [dbo].[AFBillReceipt] ADD  DEFAULT ((0)) FOR [ReceiptAmount]
GO
ALTER TABLE [dbo].[AFGeneralLedger] ADD  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[AFGeneralLedger] ADD  DEFAULT ((0.0000)) FOR [Debit]
GO
ALTER TABLE [dbo].[AFGeneralLedger] ADD  DEFAULT ((0.0000)) FOR [Credit]
GO
ALTER TABLE [dbo].[AFGeneralLedger] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__AFInvento__Quant__202DAF9E]  DEFAULT ((0)) FOR [QuantityIn]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__AFInvento__Quant__2121D3D7]  DEFAULT ((0)) FOR [QuantityOut]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__AFInvento__UnitP__2215F810]  DEFAULT ((0)) FOR [UnitPurchasePrice]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__AFInvento__UnitS__230A1C49]  DEFAULT ((0)) FOR [UnitSalePrice]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__AFInvento__Debit__23FE4082]  DEFAULT ((0)) FOR [Debit]
GO
ALTER TABLE [dbo].[AFInventoryLedger] ADD  CONSTRAINT [DF__AFInvento__Credi__24F264BB]  DEFAULT ((0)) FOR [Credit]
GO
ALTER TABLE [dbo].[AFInvoice] ADD  DEFAULT ((0)) FOR [DueAmount]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Quant__06CD04F7]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF_AFInvoicePPI_UnitPurchasePrice]  DEFAULT ((0)) FOR [UnitSalePrice]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Actua__07C12930]  DEFAULT ((0)) FOR [ActualAmount]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Disco__08B54D69]  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[AFInvoicePPI] ADD  CONSTRAINT [DF__AFInvoice__Charg__09A971A2]  DEFAULT ((0)) FOR [ChargedAmount]
GO
ALTER TABLE [dbo].[AFInvoiceReceipt] ADD  DEFAULT ((0)) FOR [ReceiptAmount]
GO
ALTER TABLE [dbo].[AFProductPriceLog] ADD  CONSTRAINT [DF__AFProduct__Defau__1F83A428]  DEFAULT ((0)) FOR [DefaultSalePrice]
GO
ALTER TABLE [dbo].[AFProductPriceLog] ADD  CONSTRAINT [DF__AFProduct__Minim__2077C861]  DEFAULT ((0)) FOR [MinimumSalePrice]
GO
ALTER TABLE [dbo].[AFSupplierLedger] ADD  DEFAULT ((0)) FOR [Debit]
GO
ALTER TABLE [dbo].[AFSupplierLedger] ADD  DEFAULT ((0)) FOR [Credit]
GO
ALTER TABLE [dbo].[confApplicationRule] ADD  DEFAULT ((0)) FOR [DefaultSaleMargin]
GO
ALTER TABLE [dbo].[confApplicationRule] ADD  DEFAULT ((0)) FOR [IsWhatsAppInvoicing]
GO
ALTER TABLE [dbo].[confApplicationRule] ADD  DEFAULT ((0)) FOR [IsRefundAdvice]
GO
ALTER TABLE [dbo].[DocumentStatus] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[DocumentType] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__Quant__43F60EC8]  DEFAULT ((0)) FOR [QuantityIn]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__Quant__44EA3301]  DEFAULT ((0)) FOR [QuantityOut]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__UnitP__420DC656]  DEFAULT ((0)) FOR [UnitPurchasePrice]
GO
ALTER TABLE [dbo].[IAdjustmentPPQD] ADD  CONSTRAINT [DF__IAdjustme__UnitS__4301EA8F]  DEFAULT ((0)) FOR [UnitSalePrice]
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
ALTER TABLE [dbo].[AFInventoryLedger]  WITH CHECK ADD  CONSTRAINT [FK_AFInventoryLedger_Self] FOREIGN KEY([ConsumedInventoryLedgerId])
REFERENCES [dbo].[AFInventoryLedger] ([Id])
GO
ALTER TABLE [dbo].[AFInventoryLedger] CHECK CONSTRAINT [FK_AFInventoryLedger_Self]
GO
/****** Object:  StoredProcedure [dbo].[ACBranch_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[ACCompany_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[ACUser_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AFBill_GLBParam]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AFBill_GLBParam]    
    @GuID               UNIQUEIDENTIFIER,    
    @SupplierIds        NVARCHAR(MAX),    
    @DocumentStatusIds  NVARCHAR(MAX),    
    @BillStatusIds      NVARCHAR(MAX),    
    @TransactionDate    DATETIME
AS    
    
SELECT     
    S.[Description]             AS [SupplierName],    
    'BILL-' + B.[Code]          AS [Code],    
    B.[TransactionDate],    
    SUM(BPPI.ActualAmount)      AS [GrossAmount],  
    SUM(BPPI.DiscountAmount)    AS [DiscountAmount],  
    SUM(BPPI.ChargedAmount)     AS [NetAmount],  
    B.[DueAmount],    
    B.[BillTypeId],    
    B.[DocumentStatus],    
    B.[BillStatus],    
    B.[GuID],    
    S.[Id]              AS  [SupplierId],    
    B.[Id]              AS  [BillId]    
    
FROM        [dbo].[AFBill]      AS B    WITH(NOLOCK)    
INNER JOIN  [dbo].[PSupplier]   AS S    WITH(NOLOCK) ON S.Id = B.SupplierId AND S.Status =1 AND S.DocumentStatus IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))
INNER JOIN  [dbo].[AFBillPPI]   AS BPPI WITH(NOLOCK) ON B.Id = BPPI.BillId  AND BPPI.Status =1 AND BPPI.DocumentStatus IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))
  
WHERE    

B.Status=1    
AND (@TransactionDate IS NULL OR CAST(B.[TransactionDate] AS DATE) = CAST(@TransactionDate AS DATE))
AND (@GuID              IS NULL     OR B.[GuID] = @GuID)    
AND (@SupplierIds       IS NULL     OR B.[SupplierId]IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@SupplierIds))) 
AND B.[DocumentStatus]              IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))  
AND ((@BillStatusIds    IS NULL     OR @BillStatusIds ='' )   OR B.[BillStatus]IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@BillStatusIds))) 

GROUP BY  
    S.[Description],   
    B.[Code],   
    B.[TransactionDate],    
    B.[DueAmount],    
    B.[BillTypeId],    
    B.[DocumentStatus],    
    B.[BillStatus],    
    B.[GuID],    
    S.[Id],   
    B.[Id]  
    


GO
/****** Object:  StoredProcedure [dbo].[AFBill_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
                
   SELECT @Code = dbo.fn_CodeFormat(              
    MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))              
   )              
   FROM [AFBill]              
   WHERE YEAR(CreatedOn) = YEAR(GETDATE())              
     AND MONTH(CreatedOn) = MONTH(GETDATE())              
     AND CompanyId = @CompanyId;              
     --SELECT @DueAmount = SUM(dbo.fn_CalculateLineTotal(ActualAmount, DiscountAmount, ProductId, @DocumentStatus)) -- OLD NEED TO BE REMOVED
     SELECT @DueAmount = SUM(dbo.fn_CalculateLineTotalByATI(ActualAmount, DiscountAmount, ProductATIId))
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
   [ProductATIId],  
   [ProductCombinationId],            
   [Quantity],     
   [UnitPurchasePrice],    
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
   BTVP.[ProductATIId],  
   BTVP.[ProductCombinationId],            
   BTVP.[Quantity],        
   BTVP.[UnitPurchasePrice],    
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
    
                      
  SET @DocumentCode     =   @Code;              
  SET @InsertedId       =   @BillId;              
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
/****** Object:  StoredProcedure [dbo].[AFBillReceipt_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
CREATE PROCEDURE [dbo].[AFBillReceipt_Upsert]      
    @DB_OperationType   NVARCHAR(MAX),      
    @GuID               UNIQUEIDENTIFIER,      
    @LocationId         INT,      
    @TransactionDate    DATETIME,      
    @SupplierId         INT,      
    @BillId             INT,      
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
            FROM [AFBillReceipt]      
            WHERE YEAR(CreatedOn)  = YEAR(GETDATE())      
              AND MONTH(CreatedOn) = MONTH(GETDATE())      
              AND CompanyId        = @CompanyId;      
      
            INSERT INTO [dbo].[AFBillReceipt]      
            (      
                GuID,      
                Code,      
                LocationId,      
                TransactionDate,      
                SupplierId,      
                BillId,      
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
                @SupplierId,      
                @BillId,      
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
/****** Object:  StoredProcedure [dbo].[AFChartOfAccount_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AFCosting_ConsumeCost]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AFCosting_ConsumeCost]
    @ProductId              INT,
    @CostingModeId          INT,   -- FIFO=1, LIFO=2, WAC=3
    @ProductCombinationId   INT = NULL,
    @RequestedQuantity      DECIMAL(18,4),
    @LocationId             INT,
    @CompanyId              INT
    
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @CostingModeId = 1 -- FIFO
    BEGIN

         SELECT 
                IL.Id AS ConsumedInventoryLedgerId,
                IL.ReceiptBalanceQuantity,
                IL.UnitPurchasePrice
            
            FROM  dbo.AFInventoryLedger     AS      IL WITH (UPDLOCK, ROWLOCK, INDEX(IX_AFInventoryLedger_CostLayer))
            WHERE IL.Status = 1
            AND   ProductId = @ProductId
            AND   ProductCombinationId = @ProductCombinationId
            AND   LocationId = @LocationId AND CompanyId = @CompanyId
            AND   QuantityIn > 0 AND ReceiptBalanceQuantity > 0
            
            ORDER BY CASE WHEN @CostingModeId = 1 THEN Id END ASC,
                     CASE WHEN @CostingModeId = 2 THEN Id END DESC;



    END
END



  
GO
/****** Object:  StoredProcedure [dbo].[AFCosting_DecrementLayers]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AFCosting_DecrementLayers]      
    @InventoryLedgerId      INT,      
    @QuantityToDecrement    DECIMAL(18, 2)      
AS      
BEGIN      
    SET NOCOUNT ON;      

    BEGIN TRY      
        UPDATE [dbo].[InventoryLedger]      
        SET [RemainingQuantity] = [RemainingQuantity] - @QuantityToDecrement,
            [UpdatedOn] = GETDATE()      
        WHERE [Id] = @InventoryLedgerId;      
    END TRY      
    BEGIN CATCH      
        THROW;      
    END CATCH      
END
GO
/****** Object:  StoredProcedure [dbo].[AFCosting_ReverseConsumption]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AFCosting_ReverseConsumption]      
    @RefDocumentType        INT,      
    @RefDocumentId          INT,      
    @UpdatedBy              INT,      
    @DeletedDocumentStatus  INT      
AS      
BEGIN      
    SET NOCOUNT ON;      

    BEGIN TRY      
        -- Example logic: Restore quantities back to the inventory ledger based on a tracking map table 
        -- or update document/ledger status flags according to your ERP audit pattern.
        
        UPDATE il
        SET il.[RemainingQuantity] = il.[RemainingQuantity] + cm.[QuantityOut],
            il.[UpdatedOn] = GETDATE(),
            il.[UpdatedBy] = @UpdatedBy
        FROM [dbo].[InventoryLedger] il
        INNER JOIN [dbo].[CostConsumptionMap] cm ON il.[Id] = cm.[InventoryLedgerId]
        WHERE cm.[RefDocumentType] = @RefDocumentType 
          AND cm.[RefDocumentId] = @RefDocumentId;

    END TRY      
    BEGIN CATCH      
        THROW;      
    END CATCH      
END
GO
/****** Object:  StoredProcedure [dbo].[AFCustomerLedger_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AFInventoryLedger_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        
CREATE   PROCEDURE [dbo].[AFInventoryLedger_Upsert]              
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
                  
    BEGIN TRY              
        IF @OperationType = 'INSERT_DATA_INTO_DB'              
        BEGIN              
            IF ISNULL(@documentCode, '') = ''              
            BEGIN              
                SET @documentCode =  CONVERT(NVARCHAR(8), GETDATE(), 112) + '-' + RIGHT('0000' + CAST(ISNULL((SELECT COUNT(DISTINCT RefDocumentId) FROM dbo.AFInventoryLedger), 0) + 1 AS NVARCHAR(10)), 4);              
            END              
              
              
              
              
            INSERT INTO dbo.AFInventoryLedger              
            (              
                    [GuID],        
                    [Code],        
                    [LocationId],       
                    [TransactionDate],     
                    [ProductId],       
                    [ProductCombinationId],    
                    [RefDocumentType],     
                    [RefDocumentId],  
                    [RefDocumentDetailGuID],      
                    [Description],      
                    [QuantityIn],       
                    [QuantityOut],      
                    [UnitPurchasePrice],     
                    [UnitSalePrice],      
                    [Debit],        
                    [Credit],        
                    [Batch],        
                    [ExpiryDate],       
                    [ReceiptBalanceQuantity],    
                    [RemainingStockQuantity],    
                    [ConsumedInventoryLedgerId],   
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
                ILTVP.[GuID],   
                @documentCode,              
                ILTVP.[LocationId],       
                ILTVP.[TransactionDate],     
                ILTVP.[ProductId],       
                ILTVP.[ProductCombinationId],    
                @RefDocumentType,              
                ILTVP.[RefDocumentId],   
                ILTVP.[RefDocumentDetailGuID],      
                ILTVP.[Description],      
                ILTVP.[QuantityIn],       
                ILTVP.[QuantityOut],      
                ILTVP.[UnitPurchasePrice],     
                ILTVP.[UnitSalePrice],      
                ILTVP.[Debit],        
                ILTVP.[Credit],        
                ILTVP.[Batch],        
                ILTVP.[ExpiryDate],       
                ILTVP.[ReceiptBalanceQuantity],    
                ILTVP.[RemainingStockQuantity],    
                ILTVP.[ConsumedInventoryLedgerId],   
                ILTVP.[ReconcillationStatus],    
                ILTVP.[CreatedOn],       
                ILTVP.[CreatedBy],       
                ILTVP.[DocumentType],      
                ILTVP.[DocumentStatus],      
                ILTVP.[Status],        
                ILTVP.[BranchId],       
                ILTVP.[CompanyId]             
  
  
              
            FROM @AFInventoryLedger_TVP AS ILTVP              
                  
              
            SET @InsertedIn = @@ROWCOUNT;              
            SET @Response = 201;               
        END              
              
    END TRY              
    BEGIN CATCH              
        SET @Response = 400;              
        SET @InsertedIn = 0;              
                      
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();              
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();              
        DECLARE @ErrorState INT = ERROR_STATE();              
               
 RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);              
    END CATCH              
END; 
GO
/****** Object:  StoredProcedure [dbo].[AFInvoice_GLBParam]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AFInvoice_GLBParam]          
    @GuID               UNIQUEIDENTIFIER,          
    @CustomerIds        NVARCHAR(MAX),         
    @DocumentStatus     NVARCHAR(MAX),          
    @InvoiceStatuses    NVARCHAR(MAX) ,  
    @TransactionDate    DATETIME    
      
AS          
BEGIN        
    SELECT            
        C.[Description]                 AS [CustomerName],          
        'INV-' + I.[Code]               AS [Code],          
        I.[TransactionDate],          
        SUM(CALC.[GrossAmount])         AS [GrossAmount],          
        SUM(CALC.[DiscountAmount])      AS [DiscountAmount],          
        SUM(CALC.[TaxableAmount])       AS [TaxableAmount],          
        SUM(CALC.[SaleTaxAmount])       AS [SaleTaxAmount],          
        SUM(CALC.[AdditionalTaxAmount]) AS [AdditionalTaxAmount],          
        SUM(CALC.[NetAmount])           AS [NetAmount],          
        I.[DueAmount],          
        I.[InvoiceTypeId],          
        I.[DocumentStatus],          
        I.[InvoiceStatus],          
        I.[GuID],          
        C.[Id]                          AS [CustomerId],          
        I.[Id]                          AS [InvoiceId]          
      
    FROM [dbo].[AFInvoice] AS I WITH(NOLOCK)          
    INNER JOIN [dbo].[SOCustomer] AS C WITH(NOLOCK) ON C.Id = I.CustomerId          
    CROSS APPLY [dbo].[fn_GetInvoiceCalculations](I.Id, 1) AS CALC          
      
    WHERE I.Status = 1    
      AND (@TransactionDate IS NULL OR CAST(I.[TransactionDate] AS DATE) = CAST(@TransactionDate AS DATE))    
      AND (@GuID IS NULL OR I.GuID = @GuID)  
      AND (@CustomerIds       IS NULL     OR I.[CustomerId]IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@CustomerIds)))   
      AND I.DocumentStatus IN (SELECT value FROM STRING_SPLIT(@DocumentStatus, ','))        
    GROUP BY         
        C.[Description],         
        I.[Code],         
        I.[TransactionDate],         
        I.[DueAmount],         
        I.[InvoiceTypeId],         
        I.[DocumentStatus],         
        I.[InvoiceStatus],         
        I.[GuID],         
        C.[Id],         
        I.[Id]        
END
GO
/****** Object:  StoredProcedure [dbo].[AFInvoice_RptDetail_GBLParam]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AFInvoice_RptDetail_GBLParam]    
    @GuID           UNIQUEIDENTIFIER,    
    @DocumentStatus NVARCHAR(MAX)
AS
SELECT
P.Description AS ProductName,
IPPI.Quantity,
IPPI.UnitSalePrice  AS DefaultUnitPrice,
IPPI.ActualAmount,
IPPI.DiscountAmount,
IPPI.ChargedAmount

FROM AFInvoicePPI AS IPPI
INNER JOIN AFInvoice AS I ON IPPI.InvoiceId = I.Id AND I.GuID = @GuID
LEFT JOIN IProduct AS P ON IPPI.ProductId = P.Id

WHERE IPPI.Status  = 1

AND IPPI.DocumentStatus IN (SELECT value FROM STRING_SPLIT(@DocumentStatus, ','))  
GO
/****** Object:  StoredProcedure [dbo].[AFInvoice_RptHeader_GBLParam]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AFInvoice_RptHeader_GBLParam]
 @LocationId INT,
 @InvoiceGUID UNIQUEIDENTIFIER

 AS

SELECT 
	I.Id AS InvoiceId,
	I.[GuID],
	I.[Code],
	B.[Description] AS Location,
	I.[TransactionDate],
	C.[Description] AS Customer,
	I.[Description],
	I.[FBRStamp],
	I.[DueAmount],
	I.[InvoiceStatus],
	I.[CreatedOn],
	I.[DocumentStatus],
	U.[Description] AS UserName,
	SUM(CALC.[GrossAmount])         AS [DocGrossAmount],    
    SUM(CALC.[DiscountAmount])      AS [DocDiscountAmount],    
    SUM(CALC.[TaxableAmount])       AS [DocTaxableAmount],    
    SUM(CALC.[SaleTaxAmount])       AS [DocSaleTaxAmount],    
    SUM(CALC.[AdditionalTaxAmount]) AS [DocAdditionalTaxAmount],    
    SUM(CALC.[NetAmount])           AS [DocNetAmount] 
FROM AFInvoice  AS I
INNER JOIN ACBranch AS B ON I.LocationId = B.Id   
INNER JOIN SOCustomer AS C ON I.CustomerId = C.Id
INNER JOIN ACUser AS U ON I.CreatedBy = U.Id

CROSS APPLY [dbo].[fn_GetInvoiceCalculations](I.Id, 1) AS CALC   

WHERE I.LocationId = @LocationId AND I.[GuID] = @InvoiceGUID

GROUP BY
	I.[Id],
	I.[GuID],
	I.[Code],
	B.[Description],
	I.[TransactionDate],
	C.[Description],
	I.[Description],
	I.[FBRStamp],
	I.[DueAmount],
	I.[InvoiceStatus],
	I.[CreatedOn],
	I.[DocumentStatus],
	U.[Description]
GO
/****** Object:  StoredProcedure [dbo].[AFInvoice_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
   SELECT @DueAmount = SUM(dbo.fn_CalculateLineTotalByATI(ActualAmount, DiscountAmount, ProductATIId))          
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
   [ProductATIId],      
   [ProductCombinationId],          
   [Quantity],            
   [UnitSalePrice],        
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
            [ProductATIId],      
            [ProductCombinationId],          
            [Quantity],         
            [UnitSalePrice],        
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
  SET @InsertedId=@InvoiceId;            
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
/****** Object:  StoredProcedure [dbo].[AFInvoiceReceipt_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AFJournal_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AFJournalVoucher_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AFSupplierLedger_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[CSDepartment_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[custInvoiceDelete]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[custInvoiceDelete](  
    @Mode INT,  
    @InvoiceGUID UNIQUEIDENTIFIER,  
    @InvoicePPIGUID UNIQUEIDENTIFIER  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    -- Wrap everything in a transaction to ensure data integrity  
    BEGIN TRY  
        BEGIN TRANSACTION;  
  
        -- 1. Initial Information Selects (For Debugging/Auditing)  
        SELECT 'INVOICE INFORMATION' AS HEADER, *   
        FROM AFInvoice I   
        WHERE I.GuID = @InvoiceGUID;  
  
        SELECT 'INVENTORY DETAIL INFORMATION' AS HEADER, IPPI.*   
        FROM AFInvoicePPI IPPI   
        INNER JOIN AFInvoice I ON IPPI.InvoiceId = I.ID   
        WHERE I.GuID = @InvoiceGUID   
          AND (@InvoicePPIGUID IS NULL OR IPPI.GuID = @InvoicePPIGUID);  
  
        -- 2. Capture Invoice Context in Temp Table  
        CREATE TABLE #InvoiceInfo (  
            InvoiceId INT,  
            IPPIGuID UNIQUEIDENTIFIER  
        );  
  
        INSERT INTO #InvoiceInfo (InvoiceId, IPPIGuID)  
        SELECT I.ID, IPPI.GuID  
        FROM AFInvoice I   
        INNER JOIN AFInvoicePPI IPPI ON I.ID = IPPI.InvoiceId   
            AND (@InvoicePPIGUID IS NULL OR IPPI.GuID = @InvoicePPIGUID)  
        WHERE I.GuID = @InvoiceGUID;  
  
        SELECT 'CUSTOMER LEDGER INFORMATION' AS HEADER, *   
        FROM AFCustomerLedger   
        WHERE RefDocumentType = 16   
          AND RefDocumentId IN (SELECT InvoiceId FROM #InvoiceInfo);  
  
        SELECT 'INVENTORY LEDGER INFORMATION' AS HEADER, *   
        FROM AFInventoryLedger   
        WHERE RefDocumentType = 16   
          AND RefDocumentDetailGuID IN (SELECT IPPIGuID FROM #InvoiceInfo);  
  
        SELECT 'INVOICE WISE RECEIPT INFORMATION' AS HEADER, *   
        FROM AFInvoiceReceipt   
        WHERE InvoiceId IN (SELECT InvoiceId FROM #InvoiceInfo);  
  
        -- 3. Mode-Specific Logic (Fixed Scope with BEGIN...END)  
        IF @Mode = 2  
        BEGIN  
            CREATE TABLE #ConsumedLedgerId (  
                ID INT,  
                ConsumedQuantity DECIMAL(18,2)  
            );  
  
            INSERT INTO #ConsumedLedgerId (ID, ConsumedQuantity)  
            SELECT ConsumedInventoryLedgerId, QuantityOut   
            FROM AFInventoryLedger   
            WHERE RefDocumentType = 16   
              AND RefDocumentDetailGuID IN (SELECT IPPIGuID FROM #InvoiceInfo);  
  
            SELECT 'INVENTORY LEDGER RE-STOCKING INFORMATION' AS HEADER, *   
            FROM AFInventoryLedger   
            WHERE Id IN (SELECT ID FROM #ConsumedLedgerId);  
  
            -- Corrected UPDATE syntax with proper table aliasing  
            UPDATE L  
            SET L.ReceiptBalanceQuantity = ReceiptBalanceQuantity +C.ConsumedQuantity  
            FROM AFInventoryLedger L  
            INNER JOIN #ConsumedLedgerId C ON L.Id = C.ID; 
            
                  -- 4. Status Deactivations (Setting Status to 0)  
        UPDATE AFInvoiceReceipt    
        SET Status = 0   
        WHERE InvoiceId IN (SELECT InvoiceId FROM #InvoiceInfo);  
  
        UPDATE AFInventoryLedger   
        SET Status = 0   
        WHERE RefDocumentType = 16   
          AND RefDocumentDetailGuID IN (SELECT IPPIGuID FROM #InvoiceInfo);  
  
        UPDATE AFCustomerLedger    
        SET Status = 0   
        WHERE RefDocumentType = 16   
          AND RefDocumentId IN (SELECT InvoiceId FROM #InvoiceInfo);  
  
        UPDATE AFInvoicePPI     
        SET Status = 0   
        WHERE GuID IN (SELECT IPPIGuID FROM #InvoiceInfo);  
  
        UPDATE AFInvoice      
        SET Status = 0   
        WHERE GuID = @InvoiceGUID;  
  
        END  
  
        COMMIT TRANSACTION;  
  
    END TRY  
    BEGIN CATCH  
IF @@TRANCOUNT > 0            ROLLBACK TRANSACTION;  
  
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();  
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();  
        DECLARE @ErrorState INT = ERROR_STATE();  
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);  
    END CATCH  
END  
GO
/****** Object:  StoredProcedure [dbo].[IAdjustment_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
            [ProductATIId],
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
            T.[GuID],         
            @AdjustmentId,         
            T.[ProductId], 
            T.[ProductATIId],
            T.[ProductCombinationId], 
            T.[UnitPurchasePrice],        
            T.[UnitSalePrice],        
            T.[QuantityIn],        
            T.[QuantityOut],        
            T.[Batch],      
            T.[ExpiryDate],      
            @CreatedOn,        
            @CreatedBy,        
            T.[DocumentType],        
            T.[DocumentStatus],        
            T.[Status]        
                  
            FROM @IAdjustmentPPQD_TVP T      
     
        
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
/****** Object:  StoredProcedure [dbo].[IBrand_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[ICategory_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[IProduct_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[IProductATI_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[ISection_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[ISubCategory_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[PPurchase_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PPurchase_Upsert]          
    @DB_OperationType      NVARCHAR(MAX),          
    @GuID                  UNIQUEIDENTIFIER,          
    @BillId                INT,                      -- Provided direct bill linkage param
    @LocationId            INT, 
    @TransactionDate       DATETIME,          
    @SupplierId            INT,          
    @Description           NVARCHAR(MAX),          
    @PurchaseTypeId        INT,          
    @CreatedOn             DATETIME,          
    @CreatedBy             INT,          
    @UpdatedOn             DATETIME,          
    @UpdatedBy             INT,          
    @DocumentType          INT,          
    @DocumentStatus        INT,          
    @Status                BIT,          
    @BranchId              INT,          
    @CompanyId             INT,          
    @PPurchase_TVP         [dbo].[PPurchasePPQD_TVP] READONLY,          
    @DocumentCode          NVARCHAR(MAX) OUTPUT,          
    @InsertedId            INT OUTPUT,          
    @Response              INT OUTPUT        
AS          
BEGIN TRY          
              
    IF @DB_OperationType='INSERT_DATA_INTO_DB'          
    BEGIN           
        DECLARE @PurchaseId INT;          
        DECLARE @Code  NVARCHAR(MAX);          
                 
        -- 1. Process Variations via Product Combination Engine
        DECLARE @CombinationInput dbo.ProductCombination_TVP;               
        INSERT INTO @CombinationInput (ProductId, Attribute, [Status])              
        SELECT DISTINCT ProductId, Attribute, [Status]              
        FROM @PPurchase_TVP;               
        
        DECLARE @MappedCombinations TABLE (              
            ProductCombinationId INT,              
            ProductId     INT,              
            Attribute     NVARCHAR(MAX)              
        );               
        INSERT INTO @MappedCombinations (ProductCombinationId, ProductId, Attribute)              
        EXEC dbo.ProductCombination_Merge                
                                    @RefDocumentType = @DocumentType,                
                                    @Combinations    = @CombinationInput;                
         
        -- 2. Generate Formatted Document Sequence Code
        SELECT @Code = dbo.fn_CodeFormat(          
            MAX(CAST(SUBSTRING(Code,8,LEN(Code)) AS BIGINT))          
        )          
        FROM [PPurchase]          
        WHERE YEAR(CreatedOn) = YEAR(GETDATE())          
          AND MONTH(CreatedOn) = MONTH(GETDATE())          
          AND CompanyId = @CompanyId;          
  
        -- 3. Insert Master Purchase Record
        INSERT INTO [PPurchase]          
        (          
            [GuID], 
            [Code], 
            [BillId],
            [LocationId], 
            [TransactionDate],          
            [SupplierId], 
            [Description], 
            [PurchaseTypeId], 
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
            @BillId,
            @LocationId,
            @TransactionDate,          
            @SupplierId,
            @Description,
            @PurchaseTypeId,
            @CreatedOn,            
            @CreatedBy,
            @DocumentType,
            @DocumentStatus, 
            @Status,          
            @BranchId,
            @CompanyId           
        )          
        SET @PurchaseId = SCOPE_IDENTITY();          
              
        -- 4. Insert Grid Details (Fixed: Restored the missing explicit INSERT clause)
        INSERT INTO [dbo].[PPurchasePPQD]
        (
            [GuID], 
            [PurchaseId], 
            [ProductId],          
            [ProductCombinationId], 
            [Quantity], 
            [UnitPurchasePrice],          
            [UnitSalePrice], 
            [Batch], 
            [ExpiryDate], 
            [CreatedOn],          
            [CreatedBy], 
            [DocumentType], 
            [DocumentStatus], 
            [Status]
        )
        SELECT            
            ISNULL(P_TVP.[GuID], NEWID()), 
            @PurchaseId, 
            P_TVP.[ProductId],          
            MC.[ProductCombinationId], 
            P_TVP.[Quantity], 
            P_TVP.[UnitPurchasePrice],          
            P_TVP.[UnitSalePrice], 
            P_TVP.[Batch], 
            P_TVP.[ExpiryDate], 
            @CreatedOn,          
            @CreatedBy, 
            P_TVP.[DocumentType], 
            P_TVP.[DocumentStatus], 
            P_TVP.[Status]          
        FROM @PPurchase_TVP P_TVP          
        LEFT JOIN @MappedCombinations mc              
               ON mc.ProductId = P_TVP.ProductId              
              AND ((mc.Attribute IS NULL AND P_TVP.Attribute IS NULL)    
               OR mc.Attribute = P_TVP.Attribute);    

        SET @DocumentCode = @Code;          
        SET @InsertedId   = @PurchaseId;          
        SET @Response     = 201;          
    END          
              
END TRY          
BEGIN CATCH          
    SET @DocumentCode = 'N/A';          
    SET @InsertedId   = 0;          
    SET @Response     = 400;          
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[ProductCombination_Merge]    Script Date: 8/25/2026 12:02:12 AM ******/
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
  
    -- Fix 1: NULL-safe MERGE statement using standard NULL-safe evaluation
    MERGE dbo.osvProductCombination AS Target
    USING (
        SELECT DISTINCT ProductId, Attribute, [Status]
        FROM @Combinations
    ) AS Source
    ON (
        Target.ProductId = Source.ProductId
        AND (
            (Target.Attribute IS NULL AND Source.Attribute IS NULL)
            OR Target.Attribute = Source.Attribute
        )
    )
    WHEN MATCHED THEN
        UPDATE SET Target.Status = Source.Status
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (GuID, RefDocumentType, ProductId, Attribute, Status)
        VALUES (NEWID(), @RefDocumentType, Source.ProductId, Source.Attribute, Source.Status)
    OUTPUT INSERTED.Id, INSERTED.ProductId, INSERTED.Attribute
    INTO @OutputTable (CombinationId, ProductId, Attribute);

    -- Fix 2: Fallback SELECT to capture pre-existing matching records
    INSERT INTO @OutputTable (CombinationId, ProductId, Attribute)
    SELECT pc.Id, pc.ProductId, pc.Attribute
    FROM dbo.osvProductCombination pc
    INNER JOIN (SELECT DISTINCT ProductId, Attribute FROM @Combinations) c
        ON pc.ProductId = c.ProductId
        AND (
            (pc.Attribute IS NULL AND c.Attribute IS NULL)
            OR pc.Attribute = c.Attribute
        )
    WHERE NOT EXISTS (
        SELECT 1 FROM @OutputTable o
        WHERE o.ProductId = pc.ProductId
          AND (
              (o.Attribute IS NULL AND pc.Attribute IS NULL) 
              OR o.Attribute = pc.Attribute
          )
    );

    -- Return the final output table data to the calling application
    SELECT CombinationId, ProductId, Attribute 
    FROM @OutputTable;

END;
GO
/****** Object:  StoredProcedure [dbo].[PSupplier_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[RptCustomerSummary_GLBParam]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RptCustomerSummary_GLBParam]                    
    @BranchId             INT,                    
    @CompanyId            INT,                    
    @PaymentStatusIds     NVARCHAR(MAX),                    
    @InvoiceStatusIds     NVARCHAR(MAX),                    
    @DocumentStatusIds    NVARCHAR(MAX),          
    @CustomerIds          NVARCHAR(MAX)         
AS                    
BEGIN
    SET NOCOUNT ON;

    SELECT                    
        C.[Id] AS [CustomerId],                    
        C.[Code],                    
        C.[Description],                    
        C.[Contact],                    
        ISNULL(SUM(L.Debit), 0)  AS [Receivable],                    
        ISNULL(SUM(L.Credit), 0) AS [Receipt],                    
        ISNULL(SUM(L.Debit), 0) - ISNULL(SUM(L.Credit), 0) AS [Due]      
        
    FROM [dbo].[SOCustomer]              AS C WITH(NOLOCK)                    
    LEFT JOIN [dbo].[AFCustomerLedger]   AS L WITH(NOLOCK) ON C.Id = L.CustomerId AND L.[Status] = 1 AND L.BranchId = @BranchId AND L.CompanyId = @CompanyId AND L.[DocumentStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))
    WHERE C.[Status] = 1                    
      AND C.CompanyId = @CompanyId                    
      AND C.BranchId = @BranchId                    
      AND (@CustomerIds IS NULL OR @CustomerIds = '' OR C.[Id] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@CustomerIds)))
    GROUP BY 
        C.[Id], 
        C.[Code], 
        C.[Description], 
        C.[Contact]

END
GO
/****** Object:  StoredProcedure [dbo].[RptInventoryAdjustment_GLBParam]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[RptInvoiceReceipt_GLBParam]    Script Date: 8/25/2026 12:02:12 AM ******/
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
/****** Object:  StoredProcedure [dbo].[RptSaleLedger_GLBParam]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RptSaleLedger_GLBParam]    
 @BranchId     INT,    
 @CompanyId     INT,    
 @DocumentStatusIds   NVARCHAR(MAX),    
 @ReconcillationStatusIds NVARCHAR(MAX),    
 @CustomerId     INT,    
 @StartDate     DATETIME,    
 @EndDate     DATETIME    
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
 CL.[Status]     = 1  
AND CL.[DocumentStatus]   IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))        
AND CL.[ReconcillationStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@ReconcillationStatusIds))   
AND (@CustomerId = 0 OR CL.[CustomerId] = @CustomerId)  
  
ORDER BY   
--CL.CustomerId,  
TransactionDate  
       
GO
/****** Object:  StoredProcedure [dbo].[RptSupplierSummary_GLBParam]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RptSupplierSummary_GLBParam]                    
 @BranchId           INT,       
 @CompanyId          INT,    
 @PaymentStatusIds   NVARCHAR(MAX),          
 @BillStatusIds      NVARCHAR(MAX),          
 @DocumentStatusIds  NVARCHAR(MAX),
 @SupplierIds		 NVARCHAR(MAX)

AS
BEGIN
    SET NOCOUNT ON;

        SELECT           
            P.[Id] AS [SupplierId],          
            P.[Code],          
            P.[Description],          
            P.[Contact],
            ISNULL(SUM(L.Debit), 0)  AS [Receivable],                    
            ISNULL(SUM(L.Credit), 0) AS [Receipt],                    
            ISNULL(SUM(L.Debit), 0) - ISNULL(SUM(L.Credit), 0) AS [Due]      

        FROM      [dbo].[PSupplier]          AS P WITH(NOLOCK)
        LEFT JOIN [dbo].[AFSupplierLedger]   AS L WITH(NOLOCK) ON P.Id = L.SupplierId AND L.[Status] = 1 AND L.BranchId = @BranchId AND L.CompanyId = @CompanyId AND L.[DocumentStatus] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@DocumentStatusIds))
        
        WHERE P.[Status] = 1                    
          AND P.CompanyId = @CompanyId                    
          AND P.BranchId = @BranchId                    
          AND (@SupplierIds IS NULL OR @SupplierIds = '' OR P.[Id] IN (SELECT [sID] FROM dbo.fn_CodeSplitByCommaAsINT(@SupplierIds)))
        GROUP BY 
            P.[Id], 
            P.[Code], 
            P.[Description], 
            P.[Contact]
END
GO
/****** Object:  StoredProcedure [dbo].[SOCustomer_Upsert]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER TABLE SOCustomer ADD IsWalkInCustomer BIT
    
CREATE    PROCEDURE  [dbo].[SOCustomer_Upsert]     
 @DB_OperationType  NVARCHAR(MAX),    
 @GuID     UNIQUEIDENTIFIER,    
 @Description   NVARCHAR(MAX),   
 @TierTypeId INT,  
 @IsWalkInCustomer BIT,  
 @Contact    NVARCHAR(MAX),    
 @Email     NVARCHAR(MAX),    
 @CNICNumber    NVARCHAR(MAX),    
 @Address    NVARCHAR(MAX),    
 @AdditionalDetail  NVARCHAR(MAX),    
 @ReceivableAccountId INT,    
 @OpeningBalance   DECIMAL(18,2), 
 @CreditLimit   DECIMAL(18,2),    
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
    [IsWalkInCustomer],
    [Contact],        
    [Email],        
    [CNICNumber],       
    [Address],        
    [AdditionalDetail],     
    [ReceivableAccountId],     
    [OpeningBalance],
    [CreditLimit],
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
    @IsWalkInCustomer,
    @Contact,       
    @Email,        
    @CNICNumber,       
    @Address,        
    @AdditionalDetail,     
    @ReceivableAccountId,    
    @OpeningBalance, 
    @CreditLimit,
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
/****** Object:  StoredProcedure [dbo].[SRP_IProduct_CostingEngine]    Script Date: 8/25/2026 12:02:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SRP_IProduct_CostingEngine]
(
    @ProductId              INT          ,
    @CostingModeId          INT          ,
    @ProductCombinationId   INT          ,
    @RequestedQuantity      DECIMAL(18,4),
    @LocationId             INT          ,
    @CompanyId              INT          
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;   -- with XACT_ABORT ON, any error auto-rolls back the batch's
                          -- current transaction correctly — you don't need manual TRAN control.

    DECLARE @CostingLayerRecord TABLE (
        ConsumedInventoryLedgerId   INT,
        QuantityOut                 DECIMAL(18,4),
        UnitPurchasePrice           DECIMAL(18,4),
        Batch                       NVARCHAR(100),
        ExpiryDate                  DATE,
        IsStockDeficit              BIT
    );

    DECLARE @Remaining DECIMAL(18,4) = @RequestedQuantity;

    IF @CostingModeId = 1 OR @CostingModeId = 2 -- FIFO/LIFO
    BEGIN
        DECLARE @ConsumedInventoryLedgerId INT, @Balance DECIMAL(18,4),
                @UnitCost DECIMAL(18,4), @LayerBatch NVARCHAR(100), @LayerExpiry DATE;

        IF @CostingModeId = 1
        BEGIN
            DECLARE Layer_Cursor CURSOR LOCAL FAST_FORWARD FOR
            SELECT IL.Id, IL.ReceiptBalanceQuantity, IL.UnitPurchasePrice, IL.Batch, IL.ExpiryDate
            FROM dbo.AFInventoryLedger AS IL WITH (UPDLOCK, ROWLOCK, INDEX(IX_AFInventoryLedger_CostLayer))
            WHERE IL.[Status] = 1
              AND IL.[ProductId] = @ProductId
              AND (IL.[ProductCombinationId] = @ProductCombinationId
                   OR (@ProductCombinationId IS NULL AND IL.[ProductCombinationId] IS NULL))
              AND IL.[LocationId] = @LocationId
              AND IL.[CompanyId] = @CompanyId
              AND IL.[QuantityIn] > 0
              AND IL.[ReceiptBalanceQuantity] > 0
            ORDER BY IL.Id ASC;
        END
        ELSE
        BEGIN
            DECLARE Layer_Cursor CURSOR LOCAL FAST_FORWARD FOR
            SELECT IL.Id, IL.ReceiptBalanceQuantity, IL.UnitPurchasePrice, IL.Batch, IL.ExpiryDate
            FROM dbo.AFInventoryLedger AS IL WITH (UPDLOCK, ROWLOCK, INDEX(IX_AFInventoryLedger_CostLayer))
            WHERE IL.[Status] = 1
              AND IL.[ProductId] = @ProductId
              AND (IL.[ProductCombinationId] = @ProductCombinationId
                   OR (@ProductCombinationId IS NULL AND IL.[ProductCombinationId] IS NULL))
              AND IL.[LocationId] = @LocationId
              AND IL.[CompanyId] = @CompanyId
              AND IL.[QuantityIn] > 0
              AND IL.[ReceiptBalanceQuantity] > 0
            ORDER BY IL.Id DESC;
        END

        OPEN Layer_Cursor;
        FETCH NEXT FROM Layer_Cursor INTO @ConsumedInventoryLedgerId, @Balance, @UnitCost, @LayerBatch, @LayerExpiry;

        WHILE @@FETCH_STATUS = 0 AND @Remaining > 0
        BEGIN
            DECLARE @LayerQuantityOut DECIMAL(18,4) =
                CASE WHEN @Balance < @Remaining THEN @Balance ELSE @Remaining END;

            IF @LayerQuantityOut > 0
            BEGIN
                UPDATE dbo.AFInventoryLedger
                SET ReceiptBalanceQuantity = ReceiptBalanceQuantity - @LayerQuantityOut
                WHERE Id = @ConsumedInventoryLedgerId;

                INSERT INTO @CostingLayerRecord
                    (ConsumedInventoryLedgerId, QuantityOut, UnitPurchasePrice, Batch, ExpiryDate, IsStockDeficit)
                VALUES
                    (@ConsumedInventoryLedgerId, @LayerQuantityOut, @UnitCost, @LayerBatch, @LayerExpiry, 0);

                SET @Remaining = @Remaining - @LayerQuantityOut;
            END

            FETCH NEXT FROM Layer_Cursor INTO @ConsumedInventoryLedgerId, @Balance, @UnitCost, @LayerBatch, @LayerExpiry;
        END
        CLOSE Layer_Cursor;
        DEALLOCATE Layer_Cursor;

        IF @Remaining > 0
            INSERT INTO @CostingLayerRecord (ConsumedInventoryLedgerId, QuantityOut, UnitPurchasePrice, Batch, ExpiryDate, IsStockDeficit)
            VALUES (NULL, @Remaining, 0, NULL, NULL, 1);
    END

    SELECT ConsumedInventoryLedgerId, QuantityOut, UnitPurchasePrice, Batch, ExpiryDate, IsStockDeficit
    FROM @CostingLayerRecord;
END
GO
