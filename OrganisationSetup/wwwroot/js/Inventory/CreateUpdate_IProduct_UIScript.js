/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1' " + (operationType == "INSERT_INTO_DB" ? "selected='selected'" : "") + ">Select an option</option>";

/* ------ Depending DDL's ------ */


function getvAttributeList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populatevAttributeListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListAttribute").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListAttribute").append(new Option(item.description, item.id,true,true));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getBrandList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateBrandListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListBrand").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListBrand").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getDepartmentList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateDepartmentListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListDepartment").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListDepartment").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getSectionList(departmentId, sectionId) {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateSectionListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType, departmentId: departmentId },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListSection").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == sectionId);
                $("#DropDownListSection").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error.message);
        }
    });
}
function getCategoryList(sectionId, categoryId) {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateCategoryListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType, sectionId: sectionId },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCategory").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == categoryId);
                $("#DropDownListCategory").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getSubCategoryList(categoryId,subCategoryId) {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateSubCategoryListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType, categoryId: categoryId },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListSubCategory").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == subCategoryId);
                $("#DropDownListSubCategory").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getSaleUnitList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateSaleUnitListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListSaleUnit").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListSaleUnit").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getInventoryAccountList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateInventoryAccountListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListInventoryAccount").empty();
            $.each(data, function (index, item) {
                $("#DropDownListInventoryAccount").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getSaleRevenueAccountList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateSaleRevenueAccountListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListSaleRevenueAccount").empty();
            $.each(data, function (index, item) {
                $("#DropDownListSaleRevenueAccount").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getCostOfSaleAccountList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateCostOfSaleAccountListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCostOfSaleAccount").empty();
            $.each(data, function (index, item) {
                $("#DropDownListCostOfSaleAccount").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvItemTypeList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populatevItemTypeListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListItemType").empty();
            $.each(data, function (index, item) {
                $("#DropDownListItemType").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvHSCodeList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populatevHSCodeListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListHSCode").empty();
            $.each(data, function (index, item) {
                $("#DropDownListHSCode").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvSaleTaxTypeList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populatevSaleTaxTypeListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListSaleTaxType").empty();
            $.each(data, function (index, item) {
                $("#DropDownListSaleTaxType").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}

/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListDepartment").on("change", function () {
        var sectionId = -1;
        var departmentId = $("#DropDownListDepartment :selected").val();
        getSectionList(departmentId, sectionId);
    });
    $("#DropDownListDepartment,#DropDownListSection").on("change", function () {
        var categoryId = -1;
        var sectionId = $("#DropDownListSection :selected").val();
        getCategoryList(sectionId, categoryId);
    });
    $("#DropDownListDepartment,#DropDownListSection,#DropDownListCategory").on("change", function () {
        var subCategoryId = -1;
        var categoryId = $("#DropDownListCategory :selected").val();
        getSubCategoryList(categoryId, subCategoryId);
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
    getvAttributeList();
    getBrandList();
    getDepartmentList();
    getSaleUnitList();
    getInventoryAccountList();
    getSaleRevenueAccountList();
    getCostOfSaleAccountList();
    getvItemTypeList();
    getvHSCodeList();
    getvSaleTaxTypeList();
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    $('.select2').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });
    changeEventHandler();
}
/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("IProductForm");
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
    var machineNumber = $("#TextBoxMachineNumber").val();
    var sku = $("#TextBoxSKU").val();
    var additionalDetail = $("#TextBoxAdditionalDetail").val();
    var attributeIds = $("#DropDownListAttribute").val();
    var brandId = $("#DropDownListBrand :selected").val();
    var isFavorite = $("#CheckBoxIsFavorite").prop("checked");
    var isSaleTaxExclusive = $("#CheckBoxIsSaleTaxExclusive").prop("checked");
    var departmentId = $("#DropDownListDepartment :selected").val();
    var sectionId = $("#DropDownListSection :selected").val();
    var categoryId = $("#DropDownListCategory :selected").val();
    var subCategoryId = $("#DropDownListSubCategory :selected").val();
    var criticalLimit = $("#TextBoxCriticalLimit").val();
    var saleUnitId = $("#DropDownListSaleUnit :selected").val();
    var inventoryAccountId = $("#DropDownListInventoryAccount :selected").val();
    var saleRevenueAccountId = $("#DropDownListSaleRevenueAccount :selected").val();
    var costOfSaleAccountId = $("#DropDownListCostOfSaleAccount :selected").val();
    var itemTypeId = $("#DropDownListItemType :selected").val();
    var hsCodeId = $("#DropDownListHSCode :selected").val();
    var saleTaxTypeId = $("#DropDownListSaleTaxType :selected").val();

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
        MachineNumber: machineNumber,
        SKU: sku,
        AdditionalDetail: additionalDetail,
        AttributeIds: attributeIds.toString(),
        BrandId: brandId,
        IsFavorite: isFavorite,
        IsSaleTaxExclusive: isSaleTaxExclusive,
        DepartmentId: departmentId,
        SectionId: sectionId,
        CategoryId: categoryId,
        SubCategoryId: subCategoryId,
        CriticalLimit: criticalLimit ? criticalLimit : 0,
        SaleUnitId: saleUnitId,
        InventoryAccountId: inventoryAccountId,
        SaleRevenueAccountId: saleRevenueAccountId,
        CostOfSaleAccountId: costOfSaleAccountId,
        ItemTypeId: itemTypeId,
        HSCodeId: hsCodeId,
        SaleTaxTypeId: saleTaxTypeId

    }
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/createUpdateProduct",
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
                $("#IProductForm").removeClass("was-validated");
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
    $(".form-control").not("#DropDownListAttribute,#DropDownListInventoryAccount,#DropDownListSaleRevenueAccount,#DropDownListCostOfSaleAccount,#DropDownListItemType,#DropDownListHSCode,#DropDownListSaleTaxType").val("");
    $(".select2").not("#DropDownListAttribute,#DropDownListInventoryAccount,#DropDownListSaleRevenueAccount,#DropDownListCostOfSaleAccount,#DropDownListItemType,#DropDownListHSCode,#DropDownListSaleTaxType").val("-1").trigger("change");

}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});