/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var attributeList = [];
var productList = [];



/* ------ Change Cases DDL's ------ */

function getBranchList() {
    $.ajax({
        url: window.basePath + "Procurement/PPurchaseManagement/populateBranchListByParam",
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
            if (LocationId != null || LocationId != "" || LocationId != undefined || LocationId != 0) {
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
        url: window.basePath + "Procurement/PPurchaseManagement/populatevStockAdjustmentTypeListByParam",
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
        url: window.basePath + "Procurement/PPurchaseManagement/populatevAttributeListByParam",
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
        url: window.basePath + "Procurement/PPurchaseManagement/populateProductListByParam",
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
function changeEventHandler() {
    $("#DropDownListProduct").on("change", function () {
        var $selected = $(this).find(':selected');
        var attIds = $selected.data('attids');
        if (attIds) {
            var idsArray = attIds.toString().split(',');
            $("#MultiTextBox").empty();

            $.each(idsArray, function (index, attrId) {
                attrId = attrId.trim();
                if (attributeList.length > 0) {
                    var attribute = attributeList.find(a => a.id == attrId);
                    if (attribute) {
                        renderAttributeField(attribute)
                        toastr.info("Selected Attribute: " + attribute.description);
                    }
                }
                
            });
        }
    });
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        if (validater()) {
            e.preventDefault();
            createUpdateDataIntoDB();
        }
    });
}
function renderAttributeField(attribute) {
    var $container = $("#MultiTextBox");
    var fieldHtml = "";
    var fieldId = "attr_" + attribute.id;
    var label = attribute.description;
    var prop = attribute.MOAttributeProperty; // e.g. "text","number","dropdown","date","checkbox","textarea"

    switch (prop) {

        case "text":
            fieldHtml = `
                <div class="col-md-4 mb-3">
                    <label for="${fieldId}">${label}</label>
                    <input type="text" 
                           id="${fieldId}" 
                           name="${fieldId}" 
                           class="form-control attr-field" 
                           data-attr-id="${attribute.id}"
                           placeholder="Enter ${label}">
                </div>`;
            break;

        case "number":
            fieldHtml = `
                <div class="col-md-4 mb-3">
                    <label for="${fieldId}">${label}</label>
                    <input type="number" 
                           id="${fieldId}" 
                           name="${fieldId}" 
                           class="form-control attr-field" 
                           data-attr-id="${attribute.id}"
                           placeholder="Enter ${label}">
                </div>`;
            break;

        case "dropdown":
            // attribute.options expected as array: [{id, text}, ...]
            var options = (attribute.options || [])
                .map(o => `<option value="${o.id}">${o.text}</option>`)
                .join('');
            fieldHtml = `
                <div class="col-md-4 mb-3">
                    <label for="${fieldId}">${label}</label>
                    <select id="${fieldId}" 
                            name="${fieldId}" 
                            class="form-select attr-field" 
                            data-attr-id="${attribute.id}">
                        <option value="">-- Select ${label} --</option>
                        ${options}
                    </select>
                </div>`;
            break;

        case "date":
            fieldHtml = `
                <div class="col-md-4 mb-3">
                    <label for="${fieldId}">${label}</label>
                    <input type="date" 
                           id="${fieldId}" 
                           name="${fieldId}" 
                           class="form-control attr-field" 
                           data-attr-id="${attribute.id}">
                </div>`;
            break;

        case "checkbox":
            fieldHtml = `
                <div class="col-md-4 mb-3">
                    <label class="d-block">${label}</label>
                    <div class="form-check form-switch mt-2">
                        <input type="checkbox" 
                               id="${fieldId}" 
                               name="${fieldId}" 
                               class="form-check-input attr-field" 
                               data-attr-id="${attribute.id}">
                        <label class="form-check-label" for="${fieldId}">Yes / No</label>
                    </div>
                </div>`;
            break;

        case "textarea":
            fieldHtml = `
                <div class="col-md-8 mb-3">
                    <label for="${fieldId}">${label}</label>
                    <textarea id="${fieldId}" 
                              name="${fieldId}" 
                              class="form-control attr-field" 
                              data-attr-id="${attribute.id}"
                              rows="3" 
                              placeholder="Enter ${label}"></textarea>
                </div>`;
            break;

        default:
            // Fallback — plain text input for unknown property types
            fieldHtml = `
                <div class="col-md-4 mb-3">
                    <label for="${fieldId}">${label}</label>
                    <input type="text" 
                           id="${fieldId}" 
                           name="${fieldId}" 
                           class="form-control attr-field" 
                           data-attr-id="${attribute.id}"
                           placeholder="Enter ${label}">
                </div>`;
            break;
    }

    $container.append(fieldHtml);

    // Auto-init Select2 on dropdown type
    if (prop === "dropdown") {
        $("#" + fieldId).select2({
            width: '100%',
            placeholder: '-- Select ' + label + ' --',
            allowClear: true
        });
    }
}

/* ------ Call Initial Components ------ */

function initialize() {
    getBranchList();
    getvAdjustmentTypeList();
    getvAttributeList();
    getProductList(null);
    changeEventHandler();
    $('.select2:not(#DropDownListProduct)').select2({
        width: '100%'
    });

}
/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("PStockForm");
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
    var guID = $("#GuID").val();
    var description = $("#TextBoxDescription").val();

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
    };
    $.ajax({
        url: window.basePath + "CompanySetup/PStockManagement/createUpdateDepartment",
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
                $("#PStockForm").removeClass('was-validated');
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
    $(".form-control").val('');
    $(".select2").val('-1').trigger("change");

}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});
---------------------------------


            /* ------ Global Variables ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var attributeList = [];
var productList = [];
var adjustmentLines = [];   // live lines array
var lineIndex = 0;

/* =====================================================
   POPULATE DROPDOWNS
===================================================== */
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
            // Fix 1: || → && (all conditions must be true)
            if (LocationId != null && LocationId !== "" && LocationId !== undefined && LocationId != 0) {
                $("#DropDownListLocation").val(LocationId).trigger("change").prop("disabled", true);
            }
        },
        error: function (xhr, status, error) {
            console.error("Branch load failed: " + error);
        }
    });
}

function getvAdjustmentTypeList() {
    $.ajax({
        url: window.basePath + "Procurement/PPurchaseManagement/populatevStockAdjustmentTypeListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) {
            $("#DropDownListAdjustmentType").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListAdjustmentType").append(new Option(item.description, item.id));
            });
        },
        error: function (xhr, status, error) {
            console.error("Adjustment type load failed: " + error);
        }
    });
}

function getvAttributeList() {
    $.ajax({
        url: window.basePath + "Procurement/PPurchaseManagement/populatevAttributeListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) {
            attributeList = data;
        },
        error: function (xhr, status, error) {
            console.error("Attribute list load failed: " + error);
        }
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

            // Build options with data-attids stored on each <option>
            var options = data.map(function (p) {
                return $("<option>", {
                    value: p.id,
                    text: p.text,
                    "data-attids": p.attIds   // e.g. "1,2,3"
                });
            });

            $ddl.empty().append(options);
            $ddl.select2({
                width: "100%",
                placeholder: "Search Product...",
                allowClear: true,
                minimumInputLength: 1
            });

            if (productId) {
                $ddl.val(productId).trigger("change");
            }
        },
        error: function (xhr, status, error) {
            console.error("Product load failed: " + error);
        }
    });
}

/* =====================================================
   RENDER ATTRIBUTE FIELDS
===================================================== */
function renderAttributeField(attribute) {
    var $container = $("#MultiTextBox");
    var fieldId = "attr_" + attribute.id;
    var label = attribute.description;
    var prop = (attribute.MOAttributeProperty || "text").toLowerCase();
    var fieldHtml = "";

    switch (prop) {

        case "text":
            fieldHtml = `
                <div class="col-sm-12 col-md-4 mb-3">
                    <label for="${fieldId}" class="col-form-label">${label}</label>
                    <input type="text"
                           id="${fieldId}" name="${fieldId}"
                           class="form-control form-control-sm attr-field"
                           data-attr-id="${attribute.id}"
                           data-attr-name="${label}"
                           placeholder="Enter ${label}">
                </div>`;
            break;

        case "number":
            fieldHtml = `
                <div class="col-sm-12 col-md-4 mb-3">
                    <label for="${fieldId}" class="col-form-label">${label}</label>
                    <input type="number"
                           id="${fieldId}" name="${fieldId}"
                           class="form-control form-control-sm attr-field numberOnly"
                           data-attr-id="${attribute.id}"
                           data-attr-name="${label}"
                           placeholder="Enter ${label}" min="0" step="any">
                </div>`;
            break;

        case "dropdown":
            var opts = (attribute.options || [])
                .map(o => `<option value="${o.id}" data-text="${o.text}">${o.text}</option>`)
                .join("");
            fieldHtml = `
                <div class="col-sm-12 col-md-4 mb-3">
                    <label for="${fieldId}" class="col-form-label">${label}</label>
                    <select id="${fieldId}" name="${fieldId}"
                            class="form-select form-select-sm attr-field select2"
                            data-attr-id="${attribute.id}"
                            data-attr-name="${label}">
                        <option value="">-- Select ${label} --</option>
                        ${opts}
                    </select>
                </div>`;
            break;

        case "date":
            fieldHtml = `
                <div class="col-sm-12 col-md-4 mb-3">
                    <label for="${fieldId}" class="col-form-label">${label}</label>
                    <input type="date"
                           id="${fieldId}" name="${fieldId}"
                           class="form-control form-control-sm attr-field"
                           data-attr-id="${attribute.id}"
                           data-attr-name="${label}">
                </div>`;
            break;

        case "checkbox":
            fieldHtml = `
                <div class="col-sm-12 col-md-4 mb-3">
                    <label class="col-form-label d-block">${label}</label>
                    <div class="form-check form-switch mt-2">
                        <input type="checkbox"
                               id="${fieldId}" name="${fieldId}"
                               class="form-check-input attr-field"
                               data-attr-id="${attribute.id}"
                               data-attr-name="${label}">
                        <label class="form-check-label" for="${fieldId}">Yes / No</label>
                    </div>
                </div>`;
            break;

        case "textarea":
            fieldHtml = `
                <div class="col-sm-12 col-md-8 mb-3">
                    <label for="${fieldId}" class="col-form-label">${label}</label>
                    <textarea id="${fieldId}" name="${fieldId}"
                              class="form-control form-control-sm attr-field"
                              data-attr-id="${attribute.id}"
                              data-attr-name="${label}"
                              rows="2"
                              placeholder="Enter ${label}"></textarea>
                </div>`;
            break;

        default:
            fieldHtml = `
                <div class="col-sm-12 col-md-4 mb-3">
                    <label for="${fieldId}" class="col-form-label">${label}</label>
                    <input type="text"
                           id="${fieldId}" name="${fieldId}"
                           class="form-control form-control-sm attr-field"
                           data-attr-id="${attribute.id}"
                           data-attr-name="${label}"
                           placeholder="Enter ${label}">
                </div>`;
            break;
    }

    $container.append(fieldHtml);

    if (prop === "dropdown") {
        $("#" + fieldId).select2({
            width: "100%",
            placeholder: "-- Select " + label + " --",
            allowClear: true
        });
    }
}

/* =====================================================
   COLLECT ATTRIBUTE VALUES (for Add to Lines)
===================================================== */
function collectAttributeValues() {
    var values = [];
    $(".attr-field").each(function () {
        var $el = $(this);
        var attrId = $el.data("attr-id");
        var attrName = $el.data("attr-name");
        var value, valueText;

        if ($el.is(":checkbox")) {
            value = $el.is(":checked") ? 1 : 0;
            valueText = $el.is(":checked") ? "Yes" : "No";
        } else if ($el.is("select")) {
            value = $el.val();
            valueText = $el.find("option:selected").text();
        } else {
            value = $el.val();
            valueText = $el.val();
        }

        values.push({ attrId, attrName, value, valueText });
    });
    return values;
}

/* =====================================================
   RENDER LINES TABLE
===================================================== */
function renderLines() {
    var $tbody = $("#AdjustmentLinesBody");
    var grandTotal = 0;
    $tbody.empty();

    if (adjustmentLines.length === 0) {
        $tbody.html(`
            <tr id="EmptyLinesRow">
                <td colspan="7" class="text-center text-muted py-4">
                    <i class="fa-solid fa-inbox me-2"></i>
                    No lines added yet. Select a product above and click <strong>Add to Lines</strong>.
                </td>
            </tr>`);
        $("#AdjustmentLinesFoot").hide();
        $("#LineBadge").text("0 lines");
        $("#AdjustmentLinesJSON").val("[]");
        return;
    }

    $.each(adjustmentLines, function (i, line) {
        grandTotal += line.totalValue;
        var $tr = $("<tr>");

        $tr.append($("<td>").text(i + 1));
        $tr.append($("<td>").text(line.productName));
        $tr.append($("<td>").html(
            line.combination
                ? `<span class="badge bg-secondary-subtle text-secondary border border-secondary-subtle px-2">${escapeHtml(line.combination)}</span>`
                : `<span class="text-muted small">—</span>`
        ));
        $tr.append($("<td>").text(line.qty));
        $tr.append($("<td>").text(line.unitCost.toFixed(2)));
        $tr.append($("<td>").text(line.totalValue.toFixed(2)));

        var $delBtn = $("<button>")
            .addClass("btn btn-danger btn-sm")
            .html("<i class='fa-solid fa-trash'></i>")
            .on("click", function () {
                adjustmentLines = adjustmentLines.filter(l => l.index !== line.index);
                renderLines();
            });

        $tr.append($("<td>").append($delBtn));
        $tbody.append($tr);
    });

    $("#GrandTotalCell").text(grandTotal.toFixed(2));
    $("#AdjustmentLinesFoot").show();
    $("#LineBadge").text(adjustmentLines.length + (adjustmentLines.length === 1 ? " line" : " lines"));
    $("#AdjustmentLinesJSON").val(JSON.stringify(adjustmentLines));
}

function escapeHtml(str) {
    return $("<div>").text(str).html();
}

/* =====================================================
   EVENT HANDLERS
===================================================== */
function changeEventHandler() {

    /* ── Product change → render attribute fields ── */
    $("#DropDownListProduct").on("change", function () {
        var $selected = $(this).find(":selected");
        var attIds = $selected.data("attids");  // reads data-attids

        $("#MultiTextBox").empty();
        $("#VariantMatchRow").hide();

        if (attIds) {
            // Fix 2: show the container when attributes exist
            $("#VariantAttributesContainer").show();
            $("#NoVariantMsg").hide();

            var idsArray = attIds.toString().split(",");

            $.each(idsArray, function (index, attrId) {
                attrId = attrId.trim();   // Fix 3: trim once at top

                if (attributeList.length > 0) {
                    var attribute = attributeList.find(a => a.id == attrId);
                    if (attribute) {
                        renderAttributeField(attribute);
                        toastr.info("Attribute loaded: " + attribute.description);
                    }
                }
            });

        } else if ($(this).val()) {
            // Product selected but has no attributes
            $("#VariantAttributesContainer").show();
            $("#NoVariantMsg").show();
            $("#MultiTextBox").empty();
        } else {
            // Nothing selected
            $("#VariantAttributesContainer").hide();
            $("#NoVariantMsg").hide();
        }
    });

    $("#ButtonAddLine").on("click", function () {
        var $ddl = $("#DropDownListProduct");
        var productId = $ddl.val();
        var productName = $ddl.find(":selected").text();
        var qty = parseFloat($("#InputQty").val());
        var unitCost = parseFloat($("#InputUnitCost").val()) || 0;

        if (!productId || productId === "-1") {
            toastr.warning("Please select a product.");
            return;
        }
        if (isNaN(qty) || qty <= 0) {
            toastr.warning("Please enter a valid quantity.");
            return;
        }

        var attrValues = collectAttributeValues();
        var combination = attrValues.map(a => a.valueText).filter(Boolean).join(" / ");

        lineIndex++;
        var line = {
            index: lineIndex,
            productId: productId,
            productName: productName,
            combination: combination,
            attrValues: attrValues,
            qty: qty,
            unitCost: unitCost,
            totalValue: qty * unitCost
        };

        adjustmentLines.push(line);
        renderLines();
        resetAddPanel();
        toastr.success("Line added.");
    });

    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        e.preventDefault();
        if (validater()) {
            createUpdateDataIntoDB();
        }
    });
}

/* =====================================================
   RESET ADD PANEL
===================================================== */
function resetAddPanel() {
    $("#DropDownListProduct").val(null).trigger("change");
    $("#InputQty").val("");
    $("#InputUnitCost").val("");
    $("#MultiTextBox").empty();
    $("#VariantAttributesContainer").hide();
    $("#NoVariantMsg").hide();
    $("#VariantMatchRow").hide();
}

/* =====================================================
   VALIDATION
   Fix: form ID corrected to #IStockAdjustmentForm
===================================================== */
function validater() {
    var form = document.getElementById("IStockAdjustmentForm"); // Fix 4 ← was #PStockForm

    if (!form.checkValidity()) {
        form.classList.add("was-validated");
        var $firstInvalid = $(form).find(":invalid").first();
        if ($firstInvalid.length) {
            $firstInvalid.trigger("focus");
        }
        toastr.warning("Please fill in all required fields correctly.");
        return false;
    }

    if (adjustmentLines.length === 0) {
        toastr.warning("Please add at least one adjustment line.");
        return false;
    }

    return true;
}

/* =====================================================
   SAVE / UPDATE
   Fix: correct URL + all fields included
===================================================== */
function createUpdateDataIntoDB() {
    var jsonData = {
        OperationType: $("#OperationType").val(),
        GuID: $("#GuID").val() || null,
        LocationId: $("#DropDownListLocation").val(),
        TransactionDate: $("#TextBoxTransactionDate").val(),
        AdjustmentTypeId: $("#DropDownListAdjustmentType").val(),
        AdditionalDetail: $("#TextBoxAdditionalDetail").val(),
        AdjustmentLines: adjustmentLines   // full lines array
    };

    $.ajax({
        // Fix 5: corrected to stock adjustment endpoint
        url: window.basePath + "Procurement/PPurchaseManagement/createUpdateStockAdjustment",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () { initLoading(); },
        success: function (response) {
            if (response.IsSuccess) {
                toastr.success(response.message);
                $("#IStockAdjustmentForm").removeClass("was-validated");
                adjustmentLines = [];
                lineIndex = 0;
                renderLines();
                clearInputFields();
            } else {
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

/* =====================================================
   CLEAR FORM
===================================================== */
function clearInputFields() {
    // Fix 6: don't reset #DropDownListProduct (has its own Select2 config)
    $(".form-control").not("#DropDownListProduct").val("");
    $(".select2").not("#DropDownListProduct").val("-1").trigger("change");
    resetAddPanel();
}

