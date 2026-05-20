/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var attributeList = [];
var productList = [];
var stagedLineItems = [];

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
            if (LocationId != null || LocationId != "" || LocationId != undefined || LocationId != 0) {
                $("#DropDownListLocation").val(LocationId).trigger("change").prop('disabled', true);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvInventoryAdjustmentTypeList() {
    $.ajax({
        url: window.basePath + "Inventory/IAdjustmentManagement/populatevInventoryAdjustmentTypeListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListInventoryAdjustmentType").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListInventoryAdjustmentType").append(new Option(item.description, item.id));
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
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        if (validater()) {
            e.preventDefault();
            createUpdateDataIntoDB();
        }
    });
}

/* ------ Call Initial Components ------ */
function initialize() {
    getBranchList();
    getvInventoryAdjustmentTypeList();
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
    var productId = $("#DropDownListProduct :selected").val();
    var adjustmentTypeId = $("#DropDownListAdjustmentType  :selected").val();
    var unitPurchasePrice = $("#TextBoxUnitPurchasePrice").val();
    var unitSalePrice = $("#TextBoxUnitSalePrice").val();
    var quantityIn = $("#TextBoxQuantityIn").val();
    var quantityOut = $("#TextBoxQuantityOut").val();
    var attribute = [];
    $("#ContainerStockAttribute .attr-field").each(function () {
        var $input = $(this);
        attribute.push({
            Id: $input.attr('data-attribute-id'),
            Description: $input.val()
        });
    });

    var jsonData = {
        OperationType: operationType,
        GuID: adjustmentGuID ? adjustmentGuID : null,
        LocationId: locationId,
        TransactionDate: transactionDate,
        Description: description,
        ProductId: productId,
        adjustmentTypeId: adjustmentTypeId,
        unitPurchasePrice: unitPurchasePrice,
        unitSalePrice: unitSalePrice,
        quantityIn: quantityIn,
        quantityOut: quantityOut,
        Attribute: attribute
    };


    console.log(jsonData);
    return;
    $.ajax({
        url: window.basePath + "CompanySetup/CSDepartmentManagement/createUpdateDepartment",
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
    $(".form-control").val('');
    $(".select2").val('-1').trigger("change");

}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});