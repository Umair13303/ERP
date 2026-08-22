ALTER PROCEDURE custInvoiceDelete(  
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