/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var attributeList = [];
var productList = [];
var adjustmentTable;

/* ------ UI COMPONENTS ------ */
function initializeDataTable() {
    adjustmentTable = $('#AdjustmentDetailTable').DataTable({
        destroy: true,
        searching: false,
        ordering: true,
        columns: [
            { title: 'Product', data: 'ProductName' },
            {
                title: 'Attributes', data: 'AttributeDisplayString',
                render: function (data) {
                    return '<span class="badge bg-light text-dark border">' + data + '</span>';
                }
            },
            {
                title: 'Purchase Price/e', data: 'UnitPurchasePrice',
                className: 'text-end',
                render: function (data) { return parseFloat(data).toFixed(2); }
            },
            {
                title: 'Sale Price/e', data: 'UnitSalePrice',
                className: 'text-end',
                render: function (data) { return parseFloat(data).toFixed(2); }
            },
            {
                title: 'QTY IN', data: 'QuantityIn',
                className: 'text-end text-success fw-bold'
            },
            {
                title: 'QTY OUT', data: 'QuantityOut',
                className: 'text-end text-danger fw-bold'
            },
            {
                title: 'Batch',
                data: 'Batch',
                render: function (data, type, row) {
                    // Only render text input if item configuration dictates expiry tracking
                    if (row.IsExpiryApplied) {
                        return '<input type="text" class="form-control form-control-sm grid-batch-input" value="' + (data || "") + '" placeholder="Enter Batch">';
                    }
                    return '<span class="text-muted">-</span>';
                }
            },
            {
                title: 'Expiry',
                data: 'Expiry',
                render: function (data, type, row) {
                    if (row.IsExpiryApplied) {
                        return '<input type="date" class="form-control form-control-sm grid-expiry-input" value="' + (data || "") + '">';
                    }
                    return '<span class="text-muted">-</span>';
                }
            },
            {
                title: 'ACTIONS',
                data: null,
                className: 'text-center',
                orderable: false,
                searchable: false,
                render: function (data, type, row, meta) {
                    return gridButton.deleteInList("", "Delete Adjustment", "");
                }
            }
        ],
        language: {
            emptyTable: "No items added yet. Click 'Add Item' above."
        }
    });
}

/* ------ Depending DDL's ------ */
function getBranchList() {
    $.ajax({
        url: window.basePath + "Inventory/IAdjustmentManagement/populateBranchListByParam",
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
function getvAdjustmentTypeList() {
    $.ajax({
        url: window.basePath + "Inventory/IAdjustmentManagement/populatevAdjustmentTypeListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListAdjustmentType").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListAdjustmentType").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvAttributeList() {
    $.ajax({
        url: window.basePath + "Inventory/IAdjustmentManagement/populatevAttributeListByParam",
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
        url: window.basePath + "Inventory/IAdjustmentManagement/populateProductListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType, searchParam: "" },
        success: function (data) {
            productList = data;
            var $ddl = $("#DropDownListProduct");
            var options = data.map(function (p) {
                return $('<option>', {
                    value: p.id,
                    text: p.text,
                    'data-attIds': p.attIds,
                    'data-isExpiryApplied': p.isExpiryApplied,
                });
            });

            $ddl.empty().append(options);

            $ddl.select2({ width: '100%', placeholder: 'Search Product...', allowClear: true, minimumInputLength: 1 });

            if (productId) {
                $ddl.val(productId).trigger('change');
            }
        },
        error: function (xhr, status, error) {
            console.error("Product load failed: " + error);
        }
    });
}


/* ------ Grid Actions ------ */
function addLineItemToStaging() {
    var productName = $("#DropDownListProduct option:selected").text();
    var productId = $("#DropDownListProduct").val();
    var isExpiryApplied = $("#DropDownListProduct option:selected").data('isexpiryapplied');

    var unitPurchasePrice = parseFloat($("#TextBoxUnitPurchasePrice").val()) || 0;
    var unitSalePrice = parseFloat($("#TextBoxUnitSalePrice").val()) || 0;
    var quantityIn = parseFloat($("#TextBoxQuantityIn").val()) || 0;
    var quantityOut = parseFloat($("#TextBoxQuantityOut").val()) || 0;

    if (!productId || productId === "-1") {
        toastr.warning("Please select a valid product.");
        return;
    }
    if (quantityIn <= 0 && quantityOut <= 0) {
        toastr.warning("Quantity In or Quantity Out must be greater than zero.");
        return;
    }

    var hasExpiry = (isExpiryApplied === true || isExpiryApplied === "True" || isExpiryApplied === 1 || isExpiryApplied === "1");

    var itemAttributes = [];
    var attributeDescriptions = [];
    $("#ContainerStockAttribute .attr-field, #ContainerStockAttribute input").each(function () {
        var $input = $(this);
        var attrId = $input.attr('data-attribute-id') || $input.attr('id');
        var val = $input.val();
        if (val) {
            itemAttributes.push({ Id: attrId, Description: val });
            attributeDescriptions.push(val);
        }
    });

    var attributeString = attributeDescriptions.length > 0 ? attributeDescriptions.join(', ') : "N/A";
    var lineItem = {    
        ProductId: productId,
        ProductName: productName,
        UnitPurchasePrice: unitPurchasePrice,
        UnitSalePrice: unitSalePrice,
        QuantityIn: quantityIn,
        QuantityOut: quantityOut,
        Attribute: itemAttributes,
        AttributeDisplayString: attributeString,
        IsExpiryApplied: hasExpiry,
        Batch: "",
        Expiry: ""
    };
    adjustmentTable.row.add(lineItem).draw(false);
    clearLineItemInputs();
}
function clearLineItemInputs() {
    $("#TextBoxUnitPurchasePrice").val('0.00');
    $("#TextBoxUnitSalePrice").val('0.00');
    $("#TextBoxQuantityIn").val('0.00');
    $("#TextBoxQuantityOut").val('0.00');
    $("#ContainerStockAttribute").empty();
    $("#DivVariantInformation").slideUp('fast');
}
/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListProduct").on("change", function () {
        var $selected = $(this).find(':selected');
        var attributeIds = $selected.data('attids');
        if (attributeIds) {
            var attributeArray = attributeIds.toString().split(',');
            $("#ContainerStockAttribute").empty();
            $.each(attributeArray, function (index, attrId) {
                attrId = attrId.trim();
                if (attributeList.length > 0) {
                    $("#DivVariantInformation").slideDown('slow');
                    var attribute = attributeList.find(a => a.id == attrId);
                    if (attribute) {
                        var fieldHtml = inputField.textBox(attribute.description, attribute.description, "", "Standard", false, attribute.id);
                        $("#ContainerStockAttribute").append(fieldHtml);
                    }
                }
            });
        }
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
}


/* ------ Call Initial Components ------ */
function initialize() {
    initializeDataTable();
    getBranchList();
    getvAdjustmentTypeList();
    getvAttributeList();
    getProductList();
    changeEventHandler();
    $('.select2').select2({
        width: '100%'
    });
}

/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("IInventoryAdjustmentForm");
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
    var description = $("#TextBoxDescription").val();
    var adjustmentTypeId = $("#DropDownListAdjustmentType :selected").val();
    var attribute = [];
    $("#ContainerStockAttribute .attr-field").each(function () {
        var $input = $(this);
        attribute.push({
            Id: $input.attr('data-attribute-id'),
            Description: $input.val()
        });
    });
    var iAdjustmentPPQD = adjustmentTable.rows().nodes().to$().map(function (index, node) {
        var row = adjustmentTable.row(node).data();
        var batch = $(node).find('.grid-batch-input').val() || "";
        var expiry = $(node).find('.grid-expiry-input').val() || "";
        return {
            ProductId: row.ProductId,
            UnitPurchasePrice: row.UnitPurchasePrice,
            UnitSalePrice: row.UnitSalePrice,
            QuantityIn: row.QuantityIn,
            QuantityOut: row.QuantityOut,
            Attribute: (row.Attribute && row.Attribute.length > 0) ? JSON.stringify(row.Attribute) : null,
            Batch: batch,
            ExpiryDate: expiry
        };
    }).get();
    var jsonData = {
        OperationType: operationType,
        GuID: adjustmentGuID ? adjustmentGuID : null,
        LocationId: locationId,
        TransactionDate: transactionDate,
        Description: description,
        AdjustmentTypeId: adjustmentTypeId,
        PostedDataIAdjustmentPPQD: iAdjustmentPPQD
    };

    $.ajax({
        url: window.basePath + "Inventory/IAdjustmentManagement/createUpdateInventoryAdjustment",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            initLoading();
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
            stopLoading();
            clearInputFields();
        }
    });
}
function clearInputFields() {
    adjustmentTable.clear().draw();
    $(".form-control").not("#DropDownListLocation").val('');
    $(".select2").not("#DropDownListLocation").val('-1').trigger("change");
}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});