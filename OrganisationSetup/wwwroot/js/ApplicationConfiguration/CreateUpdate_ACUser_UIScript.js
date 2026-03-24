/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";

/* ------ Depending DDL's ------ */

function getvRoleList() {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACUserManagement/populatevRoleListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListRole").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListRole").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getCompanyList() {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACUserManagement/populateCompanyListByParam",
        type: "GET",
        data: { operationType: operationType },
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCompany").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListCompany").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getBranchList(companyId,branchId) {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACUserManagement/populateBranchListByParam",
        type: "GET",
        data: { operationType: operationType, companyId: companyId, },
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListBranch").empty().append(dropDownListInitOption);
            $("#DropDownListAllowedBranch").empty().append(dropDownListInitOption);

            $.each(data, function (index, item) {
                var selectedOption = (item.id == branchId);
                $("#DropDownListBranch").append(new Option(item.description, item.id, selectedOption, selectedOption));
                $("#DropDownListAllowedBranch").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {

        },
        error: function (error) {
            console.error("Error: " + error);
        }
    });
}
function getEmployeeList(companyId,employeeId) {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACUserManagement/populateEmployeeListByParam",
        type: "GET",
        data: { operationType: operationType, companyId: companyId },
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListEmployee").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == employeeId);
                $("#DropDownListEmployee").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {

        },
        error: function (error) {
            console.error("Error: " + error);
        }
    });

}

/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListCompany").on("change", function () {
        var branchId = -1;
        var employeeId = -1;
        var companyId = $("#DropDownListCompany :selected").val();
        getBranchList(companyId,branchId);
        getEmployeeList(companyId,employeeId);
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
    getvRoleList();
    getCompanyList();
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
    var form = document.getElementById("ACUserForm");
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
    var password = $("#TextBoxPassword").val();
    var contact = $("#TextBoxContact").val();
    var email = $("#TextBoxEmail").val();
    var roleId = $("#DropDownListRole :selected").val();
    var companyId = $("#DropDownListCompany :selected").val();
    var branchId = $("#DropDownListBranch :selected").val();
    var allowedBranchIds = $("#DropDownListAllowedBranch").val();
    var employeeId = $("#DropDownListEmployee :selected").val();


    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
        Password: password,
        Contact: contact,
        Email: email,
        RoleId: roleId,
        CompanyId: companyId,
        BranchId: branchId,
        AllowedBranchIds: allowedBranchIds.toString(),
        EmployeeId: employeeId,
    };
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACUserManagement/createUpdateUser",
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
                $("#ACUserForm").removeClass('was-validated');
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