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

/* ------ MPO List Operation ------ */
var customerSummaryTable = "";
function domCustomerSummaryTable() {
    customerSummaryTable = $('#TableCustomerSummary').DataTable({
        "processing": true,
        "serverSide": false,
        "responsive": true,
        "ordering": true,
        "searching": true,
        "ajax": {
            "url": window.basePath + "SaleOperation/SOCustomerManagement/populateCustomerSummListByParam?operationType=" + operationType,
            "type": "GET",
            "dataSrc": "data",
            "error": function (xhr, error, thrown) {
                toastr.error("Failed to load customer summary.");
            }
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
            { "data": "code", "title": "Code" },
            { "data": "description", "title": "Customer" },
            { "data": "contact", "title": "Contact" },
            {
                "title": "Payment Summary",
                "data": null,
                "render": function (data, type, row) {
                    return "<b>Actual Cost:</b>" + data.receivable.toFixed(2) + "<br>" +
                        "<b>Recovered :</b> " + data.receipt.toFixed(2) + "<br>" +
                        "<hr style='margin: 5px 0;'>" +
                        "<b>Pending :</b> " + data.due.toFixed(2);
                }
            },
            { "data": "customerId", "title": "CustomerId", visible: false },
        ],
        columnDefs: [
        ],
    });
    customerSummaryTable.on('order.dt search.dt draw.dt', function () {
        customerSummaryTable.column(0, { search: 'applied', order: 'applied' })
            .nodes().each(function (cell, i) { cell.innerHTML = i + 1; });
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
    if (operationType == "MPO_LIST") {
        domCustomerSummaryTable();
    }
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


