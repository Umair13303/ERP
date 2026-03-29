/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";

/* ------ Depending DDL's ------ */

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