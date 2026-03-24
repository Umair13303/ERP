/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1' " + (operationType == "INSERT_INTO_DB" ? "selected='selected'" : "") + ">Select an option</option>";

/* ------ Depending DDL's ------ */

function getDepartmentList() {
    $.ajax({
        url: window.basePath + "Inventory/ICategoryManagement/populateDepartmentListByParam",
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
function getSectionList(departmentId,sectionId) {
    $.ajax({
        url: window.basePath + "Inventory/ICategoryManagement/populateSectionListByParam",
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
            console.error("Error: " + error);
        }
    });
}
function getCategoryList(sectionId, categoryId) {
    $.ajax({
        url: window.basePath + "Inventory/ICategoryManagement/populateCategoryListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType, sectionId: sectionId },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCategory").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == categoryId);
                $("#DropDownListCategory").append(new Option(item.description, item.id,selectedOption, selectedOption));
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
        getSectionList(departmentId,sectionId);
    });
    $("#DropDownListDepartment,#DropDownListSection").on("change", function () {
        var sectionId = -1;
        var departmentId = $("#DropDownListSection :selected").val();
        getCategoryList(departmentId, sectionId);
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
    getDepartmentList();
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
    var form = document.getElementById("ISubCategoryForm");
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
    var departmentId = $("#DropDownListDepartment :selected").val();
    var sectionId = $("#DropDownListSection :selected").val();
    var categoryId = $("#DropDownListCategory :selected").val();

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
        DepartmentId: departmentId,
        SectionId: sectionId,
        CategoryId: categoryId,

    };
    $.ajax({
        url: window.basePath + "Inventory/ICategoryManagement/createUpdateSubCategory",
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
                $("#ICategoryForm").removeClass('was-validated');
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