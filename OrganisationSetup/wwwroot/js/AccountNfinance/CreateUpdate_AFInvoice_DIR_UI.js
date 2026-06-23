/* ------ Global Variables ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var attributeList = [];
var productList = [];
var invoicePPITable;
var invoiceTable;

/* ------ UI COMPONENTS ------ */
function initializeDataTable() {
    invoicePPITable = $('#InvoiceDetailTable').DataTable({
        "processing": false,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": false,
        "paging": false,
        "info": false,
        "lengthChange": false,
        "columns": [
            {
                title: 'PRODUCT',
                data: 'ProductName',
                className: 'text-start td-product-name',
                render: function (data, type, row) {
                    if (!data) return '';
                    return `
                        <div class="product-text-wrapper truncated text-truncate" style="max-width: 180px; cursor: pointer;" title="Click to expand">
                            ${data}
                         </div>`;
                }
            },
            {
                title: 'UNIT PRC', data: 'UnitSalePrice',
            },
            { title: 'QTY', data: 'Quantity',className: 'text-danger fw-bold'   },
            { title: 'TOTAL PRC', data: 'ActualAmount' },
            { title: 'DISC', data: 'DiscountAmount', },
            { title: 'SUB NET', data: 'ChargedAmount', },
            { title: 'ACTIONS',
              data: null,
              className: 'text-center',
              orderable: false,
              searchable: false,
              render: function (data, type, row, meta) {
                  return HTML_DATATABLE_UTIL.HTML_TBL_DELETE_BTN("", "");
              }
            },
            {  title: 'BATCH',data: 'Batch',visible:false },
            {  title: 'EXPIRY', data: 'Expiry', visible: false },
        ],
        language: {
            emptyTable: `
        <div class="d-flex flex-column align-items-center justify-content-center py-4 w-100" style="min-height: 120px;">
            <div class="mb-2 p-3 bg-light rounded-circle d-inline-flex align-items-center justify-content-center shadow-sm" style="width: 60px; height: 60px;">
                <i class="fa-solid fa-cart-shopping text-primary-subtle" style="font-size: 1.8rem;"></i>
            </div>
            <h6 class="text-secondary fw-semibold mb-1" style="font-size: 0.8rem;">Invoice Slate is Empty</h6>
            <span class="text-muted" style="font-size: 0.72rem;">Search & select a product from the dropdown above to add items.</span>
        </div>
    `
        },
        initComplete: function () {
            var $tableBody = $('#InvoiceDetailTable').find('tbody');
            $tableBody.off('click', '.btn-danger, .btn-table-delete');
            $tableBody.on('click', '.btn-danger, .btn-table-delete', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $rowElement = $(this).closest('tr');
                if (invoicePPITable) {
                    invoicePPITable.row($rowElement).remove().draw(false);
                }
            });
            $tableBody.off('click', '.product-text-wrapper');
            $tableBody.on('click', '.product-text-wrapper', function () {
                var $wrapper = $(this);
                if ($wrapper.hasClass('truncated')) {
                    $wrapper.removeClass('truncated text-truncate').css('max-width', 'none');
                } else {
                    $wrapper.addClass('truncated text-truncate').css('max-width', '180px');
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
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/populateBranchListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) {
            $("#DropDownListLocation").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListLocation").append(new Option(item.description, item.id));
            });
        },
        complete: function () {
            if (LocationId && LocationId !== 0) {
                $("#DropDownListLocation").val(LocationId).trigger("change").prop('disabled', true);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getCustomerList(customerId) {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/populateCustomerListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) {
            var customerList = data;
            var $ddl = $("#DropDownListCustomer");
            $ddl.select2({
                width: '100%',
                placeholder: 'Search Customer...',
                allowClear: true,
                data: customerList,
                minimumInputLength: 1,
            });
            if (customerId) {
                $ddl.val(customerId).trigger('change');
            }
        },
        error: function (xhr, status, error) {
            console.error("Customer load failed: " + error);
        }
    });
}
function getProductList(productId) {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/populateProductListByParam",
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
                    'data-isexpiryapplied': p.isExpiryApplied,
                    'data-unitprice': p.unitSalePrice || 0
                });
            });

            $ddl.append(options);

            $ddl.select2({
                width: '100%',
                placeholder: 'Search & Add Product...',
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
function getCustomerCurrentBalance(customerId) {
    //var isCustomerFixed = $("#CheckBoxIsCustomerFixed").val();
    var isCustomerFixed = false;
    if (isCustomerFixed === "true") {
        var zeroBalance = 0;
        $("#SpanPreviousBalance").text(zeroBalance.toFixed(2));
        return;
    }
    else {
        $.ajax({
            url: window.basePath + "AccountNfinance/AFInvoiceManagement/populateCustomerSummListByParam",
            type: "GET",
            dataType: "json",
            data: { operationType: operationType, customerIds: customerId },
            success: function (data) {
                $("#SpanPreviousBalance").text(data[0].due.toFixed(2));
            },
            complete: function () {
                if (LocationId && LocationId !== 0) {
                    $("#DropDownListLocation").val(LocationId).trigger("change").prop('disabled', true);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error: " + error);
            }
        });
    }
}
function addLineItemToStaging() {
    var productId = $("#DropDownListProduct :selected").val();
    var priceListId = 0;
    var productName = $("#DropDownListProduct :selected").text();

    var isExpiryApplied = $("#DropDownListProduct :selected").data('isexpiryapplied');
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
    var unitPrice = parseFloat($("#DropDownListProduct :selected").data('unitprice')) || 0.00;
    var unitSalePriceHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("UnitSalePrice", "numbersOnly", unitPrice.toFixed(2), false);
    var quantityHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("Quantity", "numbersOnly", 0.00, false);
    var actualAmountHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("ActualAmount", "numbersOnly", 0.00, true);
    var discountAmountHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("DiscountAmount", "numbersOnly", 0.00, false);
    var chargedAmountHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("ChargedAmount", "numbersOnly", 0.00, true);
    var batchHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("Batch", "", "", disableBatchField);
    var expiryHTML = HTML_DATATABLE_UTIL.HTML_TBL_INPUT("ExpiryDate", "simpleDatePicker", "", disableExpiryField);
    var lineItem = {
        ProductId: productId,
        PriceListId : priceListId,
        ProductName: productName,
        UnitSalePrice: unitSalePriceHTML,
        Quantity: quantityHTML,
        ActualAmount: actualAmountHTML,
        DiscountAmount: discountAmountHTML,
        ChargedAmount: chargedAmountHTML,
        Batch: batchHTML,
        Expiry: expiryHTML
    };

    invoicePPITable.row.add(lineItem).draw(false);
    clearLineItemInputs();
}
function clearLineItemInputs() {
    $("#DropDownListProduct").val('-1').trigger('change.select2');
}
function recalculateSummary() {
    var totalChargedAmount = 0;
    if (invoicePPITable) {
        invoicePPITable.rows().nodes().to$().find('.ChargedAmount').each(function () {
            totalChargedAmount += parseFloat($(this).val()) || 0;
        });
    }
    var receiptAmount = parseFloat($("#TextBoxReceiptAmount").val()) || 0;
    var netReceivable = totalChargedAmount;
    var changeReturn = receiptAmount - netReceivable;
    $("#SpanNetReceivable").text(netReceivable.toFixed(2));
    $("#SpanChangeReturn").text(changeReturn.toFixed(2));
}

/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#CheckBoxIsCustomerFixed").on("change", function () {
        var isCustomerFixed = this.checked;
        $("#DropDownListCustomer").prop('disabled', isCustomerFixed);
        $("#ButtonPrintLedger").prop('disabled', isCustomerFixed);
        if (isCustomerFixed == false) {
            // LOAD PREVIOUS BALANCE
            // ENABLE PAYMENT HISTORY BUTTON
            // RESTRICT RECEIPT AMOUNT MUST BE EQUAL TO 
        }
    });
    $("#DropDownListCustomer").on("change", function () {
        var selectedCustomerInfo = $(this).select2('data');
        if (selectedCustomerInfo && selectedCustomerInfo.length > 0) {
            var contact = selectedCustomerInfo[0].contact;
            $("#TextBoxContact").val(contact);
        } else {
            $("#TextBoxContact").val("");
        }
        var customerId = $("#DropDownListCustomer :selected").val();
        getCustomerCurrentBalance(customerId);
    });
    $("#DropDownListProduct").on("change", function () {
        var val = $(this).val();
        if (val && val !== '-1' && val !== '') {
            addLineItemToStaging();
        }
    });
    $('#InvoiceDetailTable').on('input change', '.UnitSalePrice, .Quantity, .DiscountAmount', function () {
        var $row = $(this).closest('tr');

        var unitSalePrice = parseFloat($row.find('.UnitSalePrice').val()) || 0;
        var quantity = parseFloat($row.find('.Quantity').val()) || 0;
        var discountAmount = parseFloat($row.find('.DiscountAmount').val()) || 0;

        var actualAmount = unitSalePrice * quantity;
        var chargedAmount = actualAmount - discountAmount;
        if (chargedAmount < 0) chargedAmount = 0;

        $row.find('.ActualAmount').val(actualAmount.toFixed(2));
        $row.find('.ChargedAmount').val(chargedAmount.toFixed(2));
        recalculateSummary();
    });
    $("#TextBoxReceiptAmount").on("input change", function () {
        recalculateSummary();
    });
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        e.preventDefault();
        if (validater()) {
            createUpdateDataIntoDB();
        }
    });
    $("#ButtonSearchInvoiceMaster").on("click", function (e) {
        e.preventDefault();
        if (validater() && invoiceTable) {
            invoiceTable.ajax.reload(null, false);
        }
    });
}

/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("AFInvoiceForm");
    if (!form.checkValidity()) {
        form.classList.add('was-validated');
        var $firstInvalid = $(form).find(":invalid").first();
        if ($firstInvalid.length) {
            $firstInvalid.trigger("focus");
        }
        toastr.warning("Please fill in all required fields correctly.");
        return false;
    }
    if (invoicePPITable && invoicePPITable.rows().count() === 0) {
        toastr.warning("Please add at least one product line item.");
        return false;
    }
    return true;
}

/* ------ Add/Edit/Delete Operation ------ */
function createUpdateDataIntoDB() {
    var afInvoicePPI = [];

    if (invoicePPITable) {
        afInvoicePPI = invoicePPITable.rows().nodes().to$().map(function (index, node) {
            var row = invoicePPITable.row(node).data();
            var rowAttributes = [];

            $(node).find('.attr-field').each(function () {
                var $attrInput = $(this);
                rowAttributes.push({
                    Id: $attrInput.attr('data-attribute-id'),
                    Value: $attrInput.val()
                });
            });

            var unitSalePrice = parseFloat($(node).find('.UnitSalePrice').val()) || 0;
            var quantity = parseFloat($(node).find('.Quantity').val()) || 0;
            var discountAmount = parseFloat($(node).find('.DiscountAmount').val()) || 0;
            var actualAmount = unitSalePrice * quantity;
            var chargedAmount = Math.max(0, actualAmount - discountAmount);

            return {
                ProductId: row.ProductId,
                UnitSalePrice: unitSalePrice,
                Quantity: quantity,
                ActualAmount: actualAmount,
                DiscountAmount: discountAmount,
                ChargedAmount: chargedAmount,
                Attribute: rowAttributes.length > 0 ? JSON.stringify(rowAttributes) : null
            };
        }).get();
    }

    var jsonData = {
        OperationType: $("#OperationType").val(),
        GuID: $("#GuID").val() || null,
        LocationId: $("#DropDownListLocation").val(),
        TransactionDate: $("#TextBoxTransactionDate").val(),
        CustomerId: $("#DropDownListCustomer").val(),
        Contact: $("#TextBoxContact").val(),
        AdditionalDetail: $("#AdditionalNote").val(),
        ReceiptAmount: parseFloat($("#TextBoxReceiptAmount").val()) || 0,
        PostedDataAFInvoicePPI: afInvoicePPI
    };

    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/createUpdateInvoice",
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
                $("#AFInvoiceForm").removeClass('was-validated');
                clearInputFields();
            } else {
                toastr.info(response.message);
            }
        },
        error: function (xhr) {
            toastr.error("System Error: " + xhr.statusText);
        },
        complete: function () {
            if (typeof stopLoading === "function") stopLoading();
        }
    });
}

function clearInputFields() {
    if (invoicePPITable) {
        invoicePPITable.clear().draw();
    }
    recalculateSummary();
    $(".form-control").not("#DropDownListLocation").val('');
    $(".select2").not("#DropDownListLocation").val('-1').trigger("change");
    $("#AFInvoiceForm").removeClass('was-validated');
    $("#LabelPreviousBalance").text("0.00");
}

/* ------ Call Initial Components ------ */
function initialize() {
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    initializeDataTable();
    inputsUISetup();
    getBranchList();
    getCustomerList(null);
    getProductList();
    changeEventHandler();
    $('.select2').select2({
        width: '100%'
    });
}

$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});0