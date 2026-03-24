/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";


/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        if (validater()) {
            e.preventDefault();
            createUpdateDataIntoDB();
        }
    });
}

/* ------ Call Initial Components ------ */
function initialize() {
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    changeEventHandler();
    $('.select2').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });
}
/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("SOCustomerForm");
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
    var contact = $("#TextBoxContact").val();
    var email = $("#TextBoxEmail").val();
    var cnicNumber = $("#TextBoxCNICNumber").val();
    var address = $("#TextBoxAddress").val();
    var additionalDetail = $("#TextBoxAdditionalDetail").val();
    var isAutoChartOfAccount = $("#CheckBoxIsAutoChartOfAccount").prop("checked");
    var defaultReceivableAccount = null;
    if (isAutoChartOfAccount == true) {
        defaultReceivableAccount = description.trim() + " Default Receivable Account";
    }
    var openingBalance = $("#TextBoxOpeningBalance").val();

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
        Contact: contact,
        Email: email,
        CNICNumber: cnicNumber,
        Address: address,
        AdditionalDetail: additionalDetail,
        IsAutoChartOfAccount: isAutoChartOfAccount,
        DefaultReceivableAccount: defaultReceivableAccount,
        OpeningBalance: openingBalance
    };
    $.ajax({
        url: window.basePath + "SaleOperation/SOCustomerManagement/createUpdateCustomer",
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
                $("#SOCustomerForm").removeClass('was-validated');
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