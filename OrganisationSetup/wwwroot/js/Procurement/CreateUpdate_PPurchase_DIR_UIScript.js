/* ==========================================================================
   Global Variables & Runtime Configurations
   ========================================================================== */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select Option</option>";
var attributeList = [];
var supplierList = [];
var productList = [];
var purchaseTable;

/* ==========================================================================
   UI Components & DataTables Layout Configuration
   ========================================================================== */
function initializeDataTable() {
    purchaseTable = $('#AdjustmentDetailTable').DataTable({
        destroy: true,
        searching: false,
        ordering: false,
        paging: false,
        info: false,
        columns: [
            {
                title: 'PRODUCT / SKU DESIGNATION',
                data: 'ProductName',
                width: '35%',
                render: function (data, type, row, meta) {
                    if (row.IsParentRow) {
                        return '<strong class="text-primary text-uppercase"><i class="fa-solid fa-box me-2"></i>' + data + '</strong>';
                    }

                    // Standard execution formatting parameters output styling matrix
                    if (!row.MappedAttributes || row.MappedAttributes.length === 0) {
                        return '<div class="ms-4 text-muted small font-monospace"><i class="fa-solid fa-tags me-1 text-secondary"></i> Standard Base SKU</div>';
                    }

                    // Flat horizontal dynamic attribute display block rendering setup logic
                    var html = '<div class="ms-4 d-flex flex-wrap gap-1 align-items-center variant-fields-block" data-row-idx="' + meta.row + '">';
                    $.each(row.MappedAttributes, function (i, attr) {
                        html += '<div class="d-inline-flex align-items-center me-2 mb-1">';
                        html += '  <span class="text-secondary small fw-bold me-1" style="font-size: 11px;">' + attr.AttributeName + ':</span>';
                        html += '  <input type="text" class="form-control form-control-sm grid-variant-attribute-input font-monospace text-center fw-bold" ' +
                            '         data-attr-id="' + attr.AttributeId + '" ' +
                            '         data-attr-name="' + attr.AttributeName + '" ' +
                            '         value="' + (attr.SelectedValue || "") + '" ' +
                            '         placeholder="..." style="width: 70px; height: 22px; font-size:11px; padding: 0 4px;">';
                        html += '</div>';
                    });
                    html += '</div>';
                    return html;
                }
            },
            {
                title: 'COST PRICE',
                data: 'UnitPurchasePrice',
                className: 'text-end',
                width: '12%',
                render: function (data, type, row) {
                    if (row.IsParentRow) return '';
                    return '<input type="number" class="form-control form-control-sm text-end font-monospace fw-bold text-dark grid-purchase-price" value="' + parseFloat(data).toFixed(2) + '" min="0.01" step="0.01">';
                }
            },
            {
                title: 'RETAIL PRICE',
                data: 'UnitSalePrice',
                className: 'text-end',
                width: '12%',
                render: function (data, type, row) {
                    if (row.IsParentRow) return '';
                    return '<input type="number" class="form-control form-control-sm text-end font-monospace grid-sale-price" value="' + parseFloat(data).toFixed(2) + '" min="0.00" step="0.01">';
                }
            },
            {
                title: 'PURCHASE QTY',
                data: 'QuantityIn',
                className: 'text-end',
                width: '10%',
                render: function (data, type, row) {
                    if (row.IsParentRow) return '';
                    return '<input type="number" class="form-control form-control-sm text-center font-monospace text-success fw-bold grid-qty-in" value="' + data + '" min="0.01" step="any">';
                }
            },
            {
                title: 'BATCH CODE',
                data: 'Batch',
                width: '11%',
                render: function (data, type, row) {
                    if (!row.IsParentRow && row.IsExpiryApplied) {
                        return '<input type="text" class="form-control form-control-sm font-monospace text-center grid-batch-input" value="' + (data || "") + '" placeholder="Batch #">';
                    }
                    return '<span class="text-muted d-block text-center font-monospace small">-</span>';
                }
            },
            {
                title: 'EXPIRY DATE',
                data: 'Expiry',
                width: '12%',
                render: function (data, type, row) {
                    if (!row.IsParentRow && row.IsExpiryApplied) {
                        return '<input type="date" class="form-control form-control-sm font-monospace grid-expiry-input" value="' + (data || "") + '">';
                    }
                    return '<span class="text-muted d-block text-center font-monospace small">-</span>';
                }
            },
            {
                title: 'COMMAND',
                data: null,
                className: 'text-center',
                width: '8%',
                render: function (data, type, row) {
                    if (row.IsParentRow) {
                        return '<button type="button" class="btn btn-outline-primary btn-xs btn-add-variant px-2" data-product-id="' + row.ProductId + '" style="font-size: 10px; padding: 1px 5px;" title="Spawn variant context line item"><i class="fa-solid fa-plus me-1"></i>Variant</button>';
                    }
                    return '<button type="button" class="btn btn-link link-danger p-0 btn-delete-row" title="Remove Line Item"><i class="fa-solid fa-trash-can fa-lg"></i></button>';
                }
            }
        ],
        language: {
            emptyTable: "No rows buffered yet. Pick an item from the upper quick add component dropdown panel entry above to populate lines matrix."
        },
        drawCallback: function () {
            updateManifestationRowBadgeCounter();
        }
    });

    /* --- Event Listeners Matrix Context Mapping --- */
    $('#AdjustmentDetailTable').on('click', '.btn-delete-row', function () {
        syncLiveGridAllInputsToArray();
        var tr = $(this).closest('tr');
        purchaseTable.row(tr).remove().draw(false);
    });

    $('#AdjustmentDetailTable').on('click', '.btn-add-variant', function () {
        var pid = $(this).data('product-id');
        addNewVariantDetailRow(pid);
    });
}

function updateManifestationRowBadgeCounter() {
    var totalCount = 0;
    if (purchaseTable) {
        totalCount = purchaseTable.rows().data().filter(function (row) { return !row.IsParentRow; }).length;
    }
    $("#RecordCountBadge").text(totalCount + " Transactional " + (totalCount === 1 ? "Line" : "Lines"));
}

function syncLiveGridAllInputsToArray() {
    purchaseTable.rows().nodes().each(function (node, index) {
        var cachedData = purchaseTable.row(node).data();
        if (cachedData && !cachedData.IsParentRow) {
            cachedData.UnitPurchasePrice = parseFloat($(node).find('.grid-purchase-price').val()) || 0;
            cachedData.UnitSalePrice = parseFloat($(node).find('.grid-sale-price').val()) || 0;
            cachedData.QuantityIn = parseFloat($(node).find('.grid-qty-in').val()) || 0;
            cachedData.QuantityOut = 0;
            cachedData.Batch = $(node).find('.grid-batch-input').val() || "";
            cachedData.Expiry = $(node).find('.grid-expiry-input').val() || "";

            $(node).find('.grid-variant-attribute-input').each(function (i) {
                if (cachedData.MappedAttributes && cachedData.MappedAttributes[i]) {
                    cachedData.MappedAttributes[i].SelectedValue = $(this).val().trim();
                }
            });
        }
    });
}

/* ==========================================================================
   Controlled Row Generation Mechanics
   ========================================================================== */
function handleProductSelection() {
    var $selectedOption = $("#DropDownListProduct option:selected");
    var productId = $("#DropDownListProduct").val();
    var productName = $selectedOption.text();
    var isExpiryApplied = $selectedOption.data('isexpiryapplied');

    if (!productId || productId === "-1") return;

    syncLiveGridAllInputsToArray();

    var parentExists = false;
    purchaseTable.rows().data().each(function (row) {
        if (row.ProductId == productId && row.IsParentRow) parentExists = true;
    });

    var hasExpiry = (isExpiryApplied === true || isExpiryApplied === "True" || isExpiryApplied === 1 || isExpiryApplied === "1");

    if (!parentExists) {
        purchaseTable.row.add({
            IsParentRow: true,
            ProductId: productId,
            ProductName: productName,
            MappedAttributes: [],
            UnitPurchasePrice: 0,
            UnitSalePrice: 0,
            QuantityIn: 0,
            QuantityOut: 0,
            IsExpiryApplied: hasExpiry,
            Batch: "",
            Expiry: ""
        }).draw(false);
    }

    addNewVariantDetailRow(productId, hasExpiry);
}

function addNewVariantDetailRow(productId, expiryFlagOverride) {
    syncLiveGridAllInputsToArray();

    var productDefinition = productList.find(p => p.id == productId);
    var targetName = productDefinition ? productDefinition.text : "Product Configuration Variant";

    var calculatedExpiryFlag = expiryFlagOverride;
    if (calculatedExpiryFlag === undefined && productDefinition) {
        calculatedExpiryFlag = (productDefinition.isExpiryApplied === true || productDefinition.isExpiryApplied === "True" || productDefinition.isExpiryApplied === 1);
    }

    var automatedAttributesSetup = [];

    if (productDefinition && productDefinition.attIds) {
        var distinctAttributeIds = productDefinition.attIds.toString().split(',');
        $.each(distinctAttributeIds, function (idx, rawId) {
            var attributeId = rawId.trim();
            var metaLookup = attributeList.find(a => a.id == attributeId);
            if (metaLookup) {
                automatedAttributesSetup.push({
                    AttributeId: attributeId,
                    AttributeName: metaLookup.description,
                    SelectedValue: ""
                });
            }
        });
    }

    // Default Fallbacks are loaded directly on line addition block sequence
    var initializedDetailRow = {
        IsParentRow: false,
        ProductId: productId,
        ProductName: targetName,
        MappedAttributes: automatedAttributesSetup,
        UnitPurchasePrice: 0.00,
        UnitSalePrice: 0.00,
        QuantityIn: 1.00,
        QuantityOut: 0.00,
        IsExpiryApplied: calculatedExpiryFlag,
        Batch: "",
        Expiry: ""
    };

    var currentTableRowsData = purchaseTable.rows().data().toArray();
    var insertionTargetIndex = -1;

    for (var i = currentTableRowsData.length - 1; i >= 0; i--) {
        if (currentTableRowsData[i].ProductId == productId) {
            insertionTargetIndex = i + 1;
            break;
        }
    }

    if (insertionTargetIndex !== -1) {
        currentTableRowsData.splice(insertionTargetIndex, 0, initializedDetailRow);
        purchaseTable.clear().rows.add(currentTableRowsData).draw(false);
    } else {
        purchaseTable.row.add(initializedDetailRow).draw(false);
    }
}

/* ==========================================================================
   Data Extraction & JSON Payload Compilation
   ========================================================================== */
function extractAndSerializeGridPayload() {
    var collectionLines = [];
    syncLiveGridAllInputsToArray();

    purchaseTable.rows().data().each(function (rowData) {
        if (rowData && !rowData.IsParentRow) {
            collectionLines.push({
                ProductId: rowData.ProductId,
                UnitPurchasePrice: parseFloat(rowData.UnitPurchasePrice),
                UnitSalePrice: parseFloat(rowData.UnitSalePrice),
                QuantityIn: parseFloat(rowData.QuantityIn),
                QuantityOut: 0.00,
                Batch: rowData.Batch,
                ExpiryDate: rowData.Expiry,
                AttributesJson: rowData.MappedAttributes && rowData.MappedAttributes.length > 0 ? JSON.stringify(rowData.MappedAttributes.map(a => ({
                    AttributeId: a.AttributeId,
                    AttributeName: a.AttributeName,
                    Value: a.SelectedValue
                }))) : null
            });
        }
    });

    return collectionLines;
}

function createUpdateDataIntoDB() {
    var lines = extractAndSerializeGridPayload();

    if (lines.length === 0) {
        toastr.warning("Direct Purchase execution manifesto grid data rows cannot be empty.");
        return;
    }

    var submissionData = {
        OperationType: $("#OperationType").val(),
        GuID: $("#GuID").val() || null,
        LocationId: $("#DropDownListLocation").val(),
        TransactionDate: $("#TextBoxTransactionDate").val(),
        Description: $("#TextBoxDescription").val(),
        SupplierId: $("#DropDownListSupplier").val(),
        PostedPurchaseDetails: lines
    };

    $.ajax({
        url: window.basePath + "Inventory/IAdjustmentManagement/createUpdateInventoryAdjustment",
        type: "POST",
        data: JSON.stringify(submissionData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () { if (typeof initLoading === "function") initLoading(); },
        success: function (response) {
            if (response.IsSuccess) {
                toastr.success(response.message || "Direct purchase processed successfully.");
                $("#PPurchaseDIRForm").removeClass('was-validated');
                clearInputFields();
            } else {
                toastr.info(response.message);
            }
        },
        error: function (xhr) { toastr.error("Infrastructure Exception: " + xhr.statusText); },
        complete: function () { if (typeof stopLoading === "function") stopLoading(); }
    });
}

/* ==========================================================================
   Background Fetch Engine
   ========================================================================== */
function getBranchList() {
    $.ajax({
        url: window.basePath + "Procurement/PPurchaseManagement/populateBranchListByParam",
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
            if (typeof LocationId !== 'undefined' && LocationId) {
                $("#DropDownListLocation").val(LocationId).trigger("change").prop('disabled', true);
            }
        }
    });
}

function getSupplierList(supplierId) {
    $.ajax({
        url: window.basePath + "Procurement/PPurchaseManagement/populateSupplierByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) {
            supplierList = data;
            var $ddl = $("#DropDownListSupplier");
            $ddl.empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $ddl.append(new Option(item.description, item.id));
            });
            $ddl.select2({ width: '100%', placeholder: 'Select Supplier Account...', allowClear: true });
            if (supplierId) { $ddl.val(supplierId).trigger('change'); }
        }
    });
}

function getvAttributeList() {
    $.ajax({
        url: window.basePath + "Procurement/PPurchaseManagement/populatevAttributeListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) { attributeList = data; }
    });
}

function getProductList(productId) {
    $.ajax({
        url: window.basePath + "Procurement/PPurchaseManagement/populateProductListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType, searchParam: "" },
        success: function (data) {
            productList = data;
            var $ddl = $("#DropDownListProduct");
            $ddl.empty().append(dropDownListInitOption);

            $.each(data, function (index, p) {
                var opt = new Option(p.text, p.id);
                $(opt).attr('data-attIds', p.attIds);
                $(opt).attr('data-isExpiryApplied', p.isExpiryApplied);
                $ddl.append(opt);
            });

            $ddl.select2({ width: '100%', placeholder: 'Select stock item catalog registry...', allowClear: true });
            if (productId) { $ddl.val(productId).trigger('change'); }
        }
    });
}

function changeEventHandler() {
    $("#DropDownListProduct").on("change", function () {
        if ($(this).val() !== "-1" && $(this).val() !== null) {
            handleProductSelection();
            $(this).val('-1').trigger('change.select2');
        }
    });

    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        e.preventDefault();
        if (validater()) { createUpdateDataIntoDB(); }
    });
}

function validater() {
    var form = document.getElementById("PPurchaseDIRForm");
    if (!form.checkValidity()) {
        form.classList.add('was-validated');
        var $invalid = $(form).find(":invalid").first();
        if ($invalid.length) { $invalid.focus(); }
        toastr.warning("Please resolve validation exceptions across required core details.");
        return false;
    }
    return true;
}

function clearInputFields() {
    purchaseTable.clear().draw();
    $(".form-control").not("#DropDownListLocation").val('');
    $(".select2").not("#DropDownListLocation").val('-1').trigger("change");
}

/* --- Initialization Sequence --- */
$(function () {
    initializeDataTable();
    getBranchList();
    getSupplierList(null);
    getvAttributeList();
    getProductList();
    changeEventHandler();
});