/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";

/* ------ Depending DDL's ------ */
function getvAccountTypeList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFChartOfAccountManagement/populatevAccountTypeListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListAccountType").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListAccountType").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvAccountCatagoryList(accountCatagoryId) {
    var accountTypeId = $("#DropDownListAccountType :selected").val();
    if (!accountTypeId || accountTypeId == "-1") {
        $("#DropDownListAccountCatagory").empty().append(dropDownListInitOption);
        return;
    }
    $.ajax({
        url: window.basePath + "AccountNfinance/AFChartOfAccountManagement/populatevAccountCatagoryListByParam",
        type: "GET",
        data: { accountTypeId: accountTypeId },
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListAccountCatagory").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == accountCatagoryId);
                $("#DropDownListAccountCatagory").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {

        },
        error: function (error) {
            console.error("Error: " + error);
        }
    });
}
function getvFinancialStatementList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFChartOfAccountManagement/populatevFinacialStatementListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListFinancialStatement").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListFinancialStatement").append(new Option(item.description, item.id));
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
    $("#DropDownListAccountType").on("change", function () {
        var accountCatagoryId = 1;
        getvAccountCatagoryList(accountCatagoryId);
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
    getvAccountTypeList();
    getvFinancialStatementList();
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
    var form = document.getElementById("ACChartOfAccountForm ");
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
    var accountCatagoryId = $("#DropDownListAccountCatagory :selected").val();
    var financialStatementId = $("#DropDownListFinancialStatement :selected").val();
   // var isDefault = $("CheckBoxIsDefault").prop().val();
    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
        AccountCatagoryId: accountCatagoryId,
        FinancialStatementId: financialStatementId,
       // IsDefault: isDefault,
    };
    console.log(jsonData);
    return;

    $.ajax({
        url: window.basePath + "AccountNfinance/AFChartOfAccountManagement/createUpdateChartOfAccount",
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
                $("#ACBranchForm").removeClass('was-validated');
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