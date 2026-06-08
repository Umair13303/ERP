/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1' " + (operationType == "INSERT_INTO_DB" ? "selected='selected'" : "") + ">Select an option</option>";
var productTable = "";

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
function getProductTypeList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populateProductTypeListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListProductType").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListProductType").append(new Option(item.description, item.id));
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
            $("#DropDownListDepartment").empty();
            $.each(data, function (index, item) {
                $("#DropDownListDepartment").append(new Option(item.description, item.id));
            });
            $("#DropDownListDepartment").val(data[0].id).trigger("change");
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
function getvCostingModeList() {
    $.ajax({
        url: window.basePath + "Inventory/IProductManagement/populatevCostingModeListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCostingMode").empty();
            $.each(data, function (index, item) {
                $("#DropDownListCostingMode").append(new Option(item.description, item.id));
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
    $("#ButtonSearchProductMaster").on("click", function (e) {
        if (validater()) {
            e.preventDefault();
            productTable.ajax.reload(null, false);
        }
    });
}

/* ------ MPO Operation ------ */
function domProductTable() {
    productTable = $('#TableProduct').DataTable({
        "processing": true,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": true,
        "ajax": {
            "url": window.basePath + "Inventory/IProductManagement/populateProductMasterListBySearch",
            "data": function (d) {
                d.brandId = $("#DropDownListBrand :selected").val();
                d.sectionId = $("#DropDownListSection :selected").val();
                d.categoryId = $("#DropDownListCategory :selected").val();
                d.subCategoryId = $("#DropDownListSubCategory :selected").val();
                d.productTypeId = $("#DropDownListProductType :selected").val();
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
        "lengthMenu": [5, 10, 25, 50, 75, 100],
        "columns": [
            { "data": null, "title": "#" },
            { "data": "brand", "title": "Brand" },
            { "data": "description", "title": "Product" },
            { "data": "category", "title": "Category" },
            { "data": "subCategory", "title": "Sub Category" },
            { "data": "subCategory", "title": "Sub Category" },
            { "data": "productType", "title": "Fabric Type" },
            {
                "title": "Action(s)",
                "data": null,
                "render": function (data, type, row) {
                    return 'N/A'
                }
            },
            { "data": "guID", "title": "GuID", visible: false },
        ],
        columnDefs: [
        ],
    });
    productTable.on('order.dt search.dt', function () {
        productTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

/* ------ Call Initial Components ------ */
function initialize() {
    getvAttributeList();
    getBrandList();
    getProductTypeList();
    getDepartmentList();
    getSaleUnitList();
    getInventoryAccountList();
    getSaleRevenueAccountList();
    getCostOfSaleAccountList();
    getvItemTypeList();
    getvHSCodeList();
    getvSaleTaxTypeList();
    getvCostingModeList();
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    $('.select2').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });
    changeEventHandler();
    domProductTable();
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
    var productTypeId = $("#DropDownListProductType :selected").val();
    var isFavorite = $("#CheckBoxIsFavorite").prop("checked");
    var isSaleTaxExclusive = $("#CheckBoxIsSaleTaxExclusive").prop("checked");
    var departmentId = $("#DropDownListDepartment :selected").val();
    var sectionId = $("#DropDownListSection :selected").val();
    var categoryId = $("#DropDownListCategory :selected").val();
    var subCategoryId = $("#DropDownListSubCategory :selected").val();
    var isExpiryApplicable = $("#CheckBoxIsExpiryApplicable").prop("checked");
    var criticalLimit = $("#TextBoxCriticalLimit").val();
    var saleUnitId = $("#DropDownListSaleUnit :selected").val();
    var inventoryAccountId = $("#DropDownListInventoryAccount :selected").val();
    var saleRevenueAccountId = $("#DropDownListSaleRevenueAccount :selected").val();
    var costOfSaleAccountId = $("#DropDownListCostOfSaleAccount :selected").val();
    var itemTypeId = $("#DropDownListItemType :selected").val();
    var hsCodeId = $("#DropDownListHSCode :selected").val();
    var saleTaxTypeId = $("#DropDownListSaleTaxType :selected").val();
    var costingModeId = $("#DropDownListCostingMode :selected").val();

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
        MachineNumber: machineNumber,
        SKU: sku,
        AdditionalDetail: additionalDetail,
        AttributeIds: attributeIds.toString(),
        BrandId: brandId,
        ProductTypeId:productTypeId,
        IsFavorite: isFavorite,
        IsSaleTaxExclusive: isSaleTaxExclusive,
        DepartmentId: departmentId,
        SectionId: sectionId,
        CategoryId: categoryId,
        SubCategoryId: subCategoryId,
        IsExpiryApplicable: isExpiryApplicable,
        CriticalLimit: criticalLimit ? criticalLimit : 0,
        SaleUnitId: saleUnitId,
        InventoryAccountId: inventoryAccountId,
        SaleRevenueAccountId: saleRevenueAccountId,
        CostOfSaleAccountId: costOfSaleAccountId,
        ItemTypeId: itemTypeId,
        HSCodeId: hsCodeId,
        SaleTaxTypeId: saleTaxTypeId,
        CostingModeId: costingModeId
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
    $(".form-control").not("#DropDownListAttribute,#DropDownListProductType,#DropDownListDepartment,#DropDownListInventoryAccount,#DropDownListSaleRevenueAccount,#DropDownListCostOfSaleAccount,#DropDownListItemType,#DropDownListHSCode,#DropDownListSaleTaxType").val("");
    $(".select2").not("#DropDownListAttribute,#DropDownListProductType,,#DropDownListDepartment,#DropDownListInventoryAccount,#DropDownListSaleRevenueAccount,#DropDownListCostOfSaleAccount,#DropDownListItemType,#DropDownListHSCode,#DropDownListSaleTaxType").val("-1").trigger("change");
}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});