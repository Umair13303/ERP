/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var attributeList = [];
var productList = [];
var billPPITable;
var billTable;

/* ------ UI COMPONENTS ------ */
function initializeDataTable() {
    billPPITable = $('#BillDetailTable').DataTable({
        "processing": true,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": true,
        "oLanguage": {
            "oPaginate": {
                "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>',
                "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>'
            },
            "sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
            "sSearchPlaceholder": "Search...",
            "sLengthMenu": "Results :  _MENU_"
        },
        columns: [
            { title: 'Product', data: 'ProductName' },
            {
                title: 'Attributes', data: 'AttributeDisplayString',
            },
            {
                title: 'Unit Cost', data: 'UnitPurchasePrice',
            },
            {
                title: 'QTY IN', data: 'Quantity',
                className: 'text-success fw-bold'
            },
            {
                title: 'Actual', data: 'ActualAmount',
            },
            {
                title: 'Discount', data: 'DiscountAmount',
            },
            {
                title: 'Charged', data: 'ChargedAmount',
            },
            {
                title: 'Batch',
                data: 'Batch',
            },
            {
                title: 'Expiry',
                data: 'Expiry',
            },
            {
                title: 'ACTIONS',
                data: null,
                className: 'text-center',
                orderable: false,
                searchable: false,
                render: function (data, type, row, meta) {
                    return HTML_DATATABLE_UTIL.HTML_TBL_DELETE_BTN("","");
                }
            }
        ],
        language: {
            emptyTable: "No items added yet. Click 'Add Item' above."
        },
        initComplete: function () {
            var $tableBody = $('#BillDetailTable').find('tbody');
            $tableBody.off('click', '.btn-danger');
            $tableBody.on('click', '.btn-danger', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $rowElement = $(this).closest('tr');
                if (billPPITable) {
                    billPPITable.row($rowElement).remove().draw(false);
                }
            });
        }
    });
}
function inputsUISetup() {
    $(".simpleDatePicker").attr("type", "date");
}

/* ------ Depending DDL's ------ */
function getBranchList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillManagement/populateBranchListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListLocation").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListLocation").append(new Option(item.description, item.id));
            });
        },
        complete: function () {
            if (LocationId != null && LocationId != "" && LocationId != undefined && LocationId != 0) {
                $("#DropDownListLocation").val(LocationId).trigger("change").prop('disabled', true);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getSupplierList(supplierId) {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillManagement/populateSupplierListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) {
            supplierList = data;
            var $ddl = $("#DropDownListSupplier");
            $ddl.select2({
                width: '100%',
                placeholder: 'Search Supplier...',
                allowClear: true,
                data: supplierList,
                minimumInputLength: 1,
            });
            if (supplierId) {
                $ddl.val(supplierId).trigger('change');
            }
        },
        error: function (xhr, status, error) {
            console.error("Customer load failed: " + error);
        }
    });
}
function getvAttributeList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillManagement/populatevAttributeListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            attributeList = data;
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getProductList(productId) {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillManagement/populateProductListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType, searchParam: "" },
        success: function (data) {
            productList = data;
            var $ddl = $("#DropDownListProduct");
            $ddl.empty().append(dropDownListInitOption);

            var options = data.map(function (p) {
                return $('<option>', {
                    value: p.id,
                    text: p.text,
                    'data-attids': p.attIds || '',
                    'data-isexpiryapplied': p.isExpiryApplied
                });
            });

            $ddl.append(options);

            $ddl.select2({
                width: '100%',
                placeholder: 'Search Product...',
                allowClear: true,
                minimumInputLength: 1
            });

            if (productId) {
                $ddl.val(productId).trigger('change');
            } else {
                $ddl.val('-1').trigger('change');
            }
        },
        error: function (xhr, status, error) {
            console.error("Product load failed: " + error);
        }
    });
}

/* ------ Grid Actions ------ */
function generateAttributesHTML(attIdsString, attributeList) {
    if (!attIdsString || attIdsString.toString().trim() === "") {
        return "N/A";
    }

    var productAttIds = attIdsString.toString().split(',').map(id => id.trim());
    var htmlContent = "";

    $.each(productAttIds, function (index, attId) {
        var matchedAttribute = attributeList.find(attr => (attr.id || attr.Id).toString() === attId);

        if (matchedAttribute) {
            var label = matchedAttribute.description || matchedAttribute.Description || "Attribute";

            htmlContent += `
                <div class="d-flex align-items-center mb-1 me-2" style="gap: 5px;">
                    <span class="fw-bold text-secondary small">${label}:</span>
                    <input type="text" 
                           class="form-control form-control-sm attr-field" 
                           data-attribute-id="${attId}" 
                           placeholder="Value..." 
                           style="width: 100px; height: 30px;" />
                </div>`;
        }
    });

    return htmlContent !== "" ? `<div class="d-flex flex-wrap">${htmlContent}</div>` : "N/A";
}
function addLineItemToStaging() {
    var $selectedProduct = $("#DropDownListProduct option:selected");
    var productId = $selectedProduct.val();
    if (!productId || productId === "-1" || productId === "") {
        toastr.warning("Please select a valid product first.");
        return;
    }
    var attributeIds = $selectedProduct.data('attids');

    var productName = $selectedProduct.text();
    var isExpiryApplied = $selectedProduct.data('isexpiryapplied');
    var disableBatchField = false;
    var disableExpiryField = false;

    if (isExpiryApplied == true) {
        disableBatchField = false;
        disableExpiryField = false;
    }
    if (isExpiryApplied == false) {
        disableBatchField = true;
        disableExpiryField = true;
    }
    var attributeHTML = HTML_DATATABLE_UTIL.HTML_GENERIC_ATTRIBUTE(attributeIds, attributeList);
    var unitPurchasePriceHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("UnitPurchasePrice", "numbersOnly", 0.00, false);
    var quantityHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("Quantity", "numbersOnly", 0.00, false);
    var actualAmountHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("ActualAmount", "numbersOnly", 0.00, true);
    var discountAmountHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("DiscountAmount", "numbersOnly", 0.00, false);
    var chargedAmountHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("ChargedAmount", "numbersOnly", 0.00, true);
    var batchHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("Batch", "", "", disableBatchField);
    var expiryHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("ExpiryDate", "simpleDatePicker", "", disableExpiryField);

    var lineItem = {
        ProductId: productId,
        ProductName: productName,
        Attribute: attributeHTML,
        AttributeDisplayString: attributeHTML,
        UnitPurchasePrice: unitPurchasePriceHTML,
        Quantity: quantityHTML,
        ActualAmount: actualAmountHTML,
        DiscountAmount: discountAmountHTML,
        ChargedAmount: chargedAmountHTML,
        Batch: batchHTML,
        Expiry: expiryHTML
    };

    billPPITable.row.add(lineItem).draw(false);
    clearLineItemInputs();
}
function clearLineItemInputs() {
}
/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListProduct").on("change", function () {
        addLineItemToStaging();
    });
    $('#BillDetailTable').on('input change', '.UnitPurchasePrice, .Quantity, .DiscountAmount', function () {
        var $row = $(this).closest('tr');

       var unitPurchasePrice = parseFloat($row.find('.UnitPurchasePrice').val()) || 0;
       var quantity = parseFloat($row.find('.Quantity').val()) || 0;
       var discountAmount = parseFloat($row.find('.DiscountAmount').val()) || 0;

       var actualAmount = unitPurchasePrice * quantity;
       var chargedAmount = actualAmount - discountAmount;
        if (chargedAmount < 0) chargedAmount = 0;

        $row.find('.ActualAmount').val(actualAmount.toFixed(2));
        $row.find('.ChargedAmount').val(chargedAmount.toFixed(2));
    });
    $("#ButtonAddLineItem").on("click", function (e) {
        e.preventDefault();
        addLineItemToStaging();
    });
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        if (validater()) {
            e.preventDefault();
            createUpdateDataIntoDB();
        }
    });
    $("#ButtonSearchBillMaster").on("click", function (e) {
        if (validater()) {
            e.preventDefault();
            billTable.ajax.reload(null, false);
        }
    });
}

/* ------ MPO Operation ------ */
function domBillTable() {
    billTable = $('#TableBill').DataTable({
        "processing": true,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": true,
        "ajax": {
            "url": window.basePath + "AccountNfinance/AFBillManagement/populateBillMasterListBySearch",
            "data": function (d) {
                d.operationType = operationType;
                d.supplierIds = $("#DropDownListSupplier :selected").val();
                d.billStatusIds = $("#DropDownListBillStatus :selected").val();
                d.transactionDate = $("#TextBoxTransactionDate").val();
                d.documentStatusIds = $("#DropDownListDocumentStatus :selected").val();
                d.billStatus = $("#DropDownListBillStatus :selected").val();
            },
            "type": "GET",
            "dataSrc": "data",
        },
        "oLanguage": {
            "oPaginate": {
                "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>',
                "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>'
            },
            "sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
            "sSearchPlaceholder": "Search...",
            "sLengthMenu": "Results :  _MENU_"
        },
        columns: [
            { title: 'Date', data: 'transactionDate' },
            { title: 'Code', data: 'code' },
            { title: 'Supplier', data: 'supplierName' },
            { title: 'GROSS AMT', data: 'grossAmount' },
            { title: 'DISC AMT', data: 'discountAmount' },
            { title: 'NET AMT', data: 'netAmount' },
            { title: 'DUE AMT', data: 'dueAmount' },
            { title: 'DOC', data: 'docStatus' },
            { title: 'BILLING', data: 'billStatus' },
        ],
    });
}

/* ------ Call Initial Components ------ */
function initialize() {
    initializeDataTable();
    inputsUISetup();
    getBranchList();
    getSupplierList(null);
    getvAttributeList();
    getProductList();
    changeEventHandler();
    $('.select2').select2({
        width: '100%'
    });
    domBillTable();
}

/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("AFBillForm");
    if (!form.checkValidity()) {
        form.classList.add('was-validated');

        var $firstInvalid = $(form).find(":invalid").first();
        if ($firstInvalid.length) {
            $firstInvalid.trigger("focus");
        }

        toastr.warning("Please fill in all required fields correctly.");
        return false;
    }
    return true;
}

/* ------ Add/Edit/Delete Operation ------ */
function createUpdateDataIntoDB() {
    var operationType = $("#OperationType").val();
    var adjustmentGuID = $("#GuID").val();
    var locationId = $("#DropDownListLocation :selected").val();
    var transactionDate = $("#TextBoxTransactionDate").val();
    var supplierId = $("#DropDownListSupplier :selected").val();
    var description = $("#TextBoxDescription").val();
    var afBillPPI = [];
    if (billPPITable) {
        afBillPPI = billPPITable.rows().nodes().to$().map(function (index, node) {
            var row = billPPITable.row(node).data();
            var rowAttributes = [];
            $(node).find('.attr-field').each(function () {
                var $attrInput = $(this);
                rowAttributes.push({
                    Id: $attrInput.attr('data-attribute-id'),
                    Value: $attrInput.val()
                });
            });

            var unitPurchasePrice = parseFloat($(node).find('.UnitPurchasePrice').val()) || 0;
            var quantity = parseFloat($(node).find('.Quantity').val()) || 0;
            var discountAmount = parseFloat($(node).find('.DiscountAmount').val()) || 0;
            var actualAmount = parseFloat($(node).find('.ActualAmount').val()) || (unitPurchasePrice * quantity);
            var chargedAmount = parseFloat($(node).find('.ChargedAmount').val()) || (actualAmount - discountAmount);
            var batch = $(node).find('input[name="Batch"]').val() || "";
            var expiry = $(node).find('input[name="ExpiryDate"]').val() || "";

            batch = (batch && batch.trim() !== "") ? batch : null;
            var expiryDate = (expiry && expiry.trim() !== "") ? expiry : null;

            return {
                ProductId: row.ProductId,
                UnitPurchasePrice: unitPurchasePrice,
                Quantity: quantity,
                ActualAmount: actualAmount,
                DiscountAmount: discountAmount,
                ChargedAmount: chargedAmount,
                Attribute: rowAttributes.length > 0 ? JSON.stringify(rowAttributes) : null,
                Batch: batch,
                ExpiryDate: expiryDate
            };
        }).get();
    }

    var jsonData = {
        OperationType: operationType,
        GuID: adjustmentGuID ? adjustmentGuID : null,
        LocationId: locationId,
        TransactionDate: transactionDate,
        Description: description,
        SupplierId: supplierId,
        PostedDataAFBillPPI: afBillPPI
    };

    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillManagement/createUpdateBill",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            if (typeof initLoading === "function") initLoading();
        },
        success: function (response) {
            if (response.IsSuccess == true) {
                toastr.success(response.message);
                $("#IInventoryAdjustmentForm").removeClass('was-validated');
            }
            else {
                toastr.info(response.message);
            }
        },
        error: function (xhr) {
            toastr.error("System Error: " + xhr.statusText);
        },
        complete: function () {
            if (typeof stopLoading === "function") stopLoading();
            clearInputFields();
        }
    });
}
function clearInputFields() {
    if (billPPITable) {
        billPPITable.clear().draw();
    }
    $(".form-control").not("#DropDownListLocation").val('');
    $(".select2").not("#DropDownListLocation").val('-1').trigger("change");
}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});