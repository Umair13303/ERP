/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var attributeList = [];
var supplierList = [];
var productList = [];
var adjustmentTable;

/* ------ UI COMPONENTS ------ */
function initializeDataTable() {
    adjustmentTable = $('#AdjustmentDetailTable').DataTable({
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
                title: 'Combination',
                data: 'AttributeIds',
                render: function (data, type, row) {
                    var attrIds = [];
                    if (data) {
                        attrIds = data.toString().split(',');
                    } else if (row.Attribute) {
                        var attrs = typeof row.Attribute === 'string' ? JSON.parse(row.Attribute) : row.Attribute;
                        if (Array.isArray(attrs)) {
                            attrIds = attrs.map(a => a.Id);
                        }
                    }

                    if (attrIds.length === 0) return '<span class="text-muted">N/A</span>';

                    var html = '<div class="d-flex flex-column gap-1 inline-attr-container" data-product-id="' + row.ProductId + '">';

                    $.each(attrIds, function (i, attrId) {
                        attrId = attrId.trim();
                        var attrObj = attributeList.find(a => a.id == attrId);
                        var attrName = attrObj ? attrObj.description : 'Attribute';

                        var existingVal = '';
                        if (row.Attribute) {
                            var attrs = typeof row.Attribute === 'string' ? JSON.parse(row.Attribute) : row.Attribute;
                            if (Array.isArray(attrs)) {
                                var match = attrs.find(a => String(a.Id) === String(attrId));
                                if (match) {
                                    existingVal = match.Description || '';
                                }
                            }
                        }

                        html += '<input type="text" class="form-control form-control-sm grid-attr-field" ' +
                            'data-attribute-id="' + attrId + '" ' +
                            'value="' + existingVal + '" ' +
                            'placeholder="' + attrName + '">';
                    });

                    html += '</div>';
                    return html;
                }
            },
            {
                title: 'Purchase Price',
                data: 'UnitPurchasePrice',
                className: 'text-end',
                render: function (data) {
                    return '<input type="number" step="0.01" class="form-control form-control-sm text-end grid-purchase-price" value="' + (data || '0.00') + '">';
                }
            },
            {
                title: 'Sale Price',
                data: 'UnitSalePrice',
                className: 'text-end',
                render: function (data) {
                    return '<input type="number" step="0.01" class="form-control form-control-sm text-end grid-sale-price" value="' + (data || '0.00') + '">';
                }
            },
            {
                title: 'QTY IN',
                data: 'QuantityIn',
                className: 'text-end',
                render: function (data) {
                    return '<input type="number" step="1" class="form-control form-control-sm text-end grid-qty-in" value="' + (data || '0') + '">';
                }
            },
            {
                title: 'QTY OUT',
                data: 'QuantityOut',
                className: 'text-end',
                render: function (data) {
                    return '<input type="number" step="1" class="form-control form-control-sm text-end grid-qty-out" value="' + (data || '0') + '">';
                }
            },
            {
                title: 'Batch',
                data: 'Batch',
                render: function (data, type, row) {
                    if (row.IsExpiryApplied) {
                        return '<input type="text" class="form-control form-control-sm grid-batch-input" value="' + (data || "") + '" placeholder="Enter Batch">';
                    }
                    return '<span class="text-muted">-</span>';
                }
            },
            {
                title: 'Expiry',
                data: 'ExpiryDate',
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
                render: function () {
                    return HTML_DATATABLE_UTIL.HTML_TBL_DELETE_BTN("", "");
                }
            }
        ],
        language: {
            emptyTable: "No items added yet. Click 'Add Item' above."
        },
        initComplete: function () {
            var $tableBody = $('#AdjustmentDetailTable').find('tbody');
            $tableBody.off('click', '.btn-danger');
            $tableBody.on('click', '.btn-danger', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $rowElement = $(this).closest('tr');
                if (adjustmentTable) {
                    adjustmentTable.row($rowElement).remove().draw(false);
                }
            });
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
function attributeKeyBuilder(attributes) {
    return attributes
        .slice()
        .sort((a, b) => a.Id.localeCompare(b.Id))
        .map(x => `${x.Id}:${x.Description}`)
        .join("|");
}

/* ------ Change Cases DDL's ------ */
function changeEventHandler() {

    $("#DropDownListProduct").on("change", function () {
        var $selected = $(this).find(':selected');
        var productId = $selected.val();
        var productName = $selected.text();
        if (!productId || productId === "-1") return;
        var attributeIds = $selected.data('attids') || "";
        var isExpiryApplied = $selected.data('isexpiryapplied');
        var hasExpiry = (isExpiryApplied === true || isExpiryApplied === "True" || isExpiryApplied === 1 || isExpiryApplied === "1");

        var lineItem = {
            ProductId: productId,
            ProductName: productName,
            UnitPurchasePrice: 0.00,
            UnitSalePrice: 0.00,
            QuantityIn: 1,
            QuantityOut: 0,
            AttributeIds: attributeIds ? attributeIds.toString() : "",
            IsExpiryApplied: hasExpiry,
            Batch: "",
            ExpiryDate: ""
        };
        adjustmentTable.row.add(lineItem).draw(false);
        $(this).val('-1').trigger('change.select2');
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
    getProductList(null);
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
        var val = $.trim($input.val());
        if (val) {
            attribute.push({
                Id: $input.attr('data-attribute-id'),
                Description: val
            });
        }
    });
    var iAdjustmentPPQD = adjustmentTable.rows().nodes().to$().map(function (index, node) {
        var row = adjustmentTable.row(node).data();

        var unitPurchasePrice = parseFloat($(node).find('.grid-purchase-price').val()) || 0;
        var unitSalePrice = parseFloat($(node).find('.grid-sale-price').val()) || 0;
        var quantityIn = parseFloat($(node).find('.grid-qty-in').val()) || 0;
        var quantityOut = parseFloat($(node).find('.grid-qty-out').val()) || 0;
        var batch = $(node).find('.grid-batch-input').val() || "";
        var expiry = $(node).find('.grid-expiry-input').val() || "";

        batch = (batch && batch.trim() !== "") ? batch : null;
        var expiryDate = (expiry && expiry.trim() !== "") ? expiry : null;

        var rowAttributes = [];
        $(node).find('.grid-attr-field').each(function () {
            var $field = $(this);
            var val = $.trim($field.val());
            if (val) {
                rowAttributes.push({
                    Id: $field.attr('data-attribute-id'),
                    Description: val
                });
            }
        });
        return {
            ProductId: row.ProductId,
            UnitPurchasePrice: unitPurchasePrice,
            UnitSalePrice: unitSalePrice,
            QuantityIn: quantityIn,
            QuantityOut: quantityOut,
            Attribute: rowAttributes.length > 0 ? JSON.stringify(rowAttributes) : null,
            Batch: batch,
            ExpiryDate: expiryDate
        };
    }).get();
    if (iAdjustmentPPQD.length === 0) {
        toastr.warning("Please add at least one item to the adjustment table.");
        return;
    }
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
            if (response.isSuccess == true) {
                toastr.success(response.message);
                $("#IInventoryAdjustmentForm").removeClass('was-validated');
                clearInputFields();
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