/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1' " + (operationType == "INSERT_INTO_DB" ? "selected='selected'" : "") + ">Select an option</option>";
var customerLedgerTable = "";


/* ------ DOM Elements ------ */
function domCustomerLedgerTable() {
    customerLedgerTable = $('#TableCustomerLedger').DataTable({
        "processing": true,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": false,
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
            { "data": "transactionDate", "title": "Date" },
            { "data": "code", "title": "Code" },
            { "data": "code", "title": "Debit" },
            { "data": "code", "title": "Credit" },
            { "data": "code", "title": "Balance" },
        ],
        columnDefs: [
            {
                targets: 0,
                searchable: false,
                orderable: false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            }
        ],
    });
}

/* ------ Depending DDL's ------ */
function getBranchList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFPaymentReceiptManagement/populateBranchListByParam",
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
function getCustomerList(customerId) {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFPaymentReceiptManagement/populateCustomerListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCustomer").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == customerId);
                $("#DropDownListCustomer").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvPaymentMethodList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFPaymentReceiptManagement/populatevPaymentMethodListByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListPaymentMethod").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListPaymentMethod").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getInvoiceReceiptList(customerId) {
    customerLedgerTable.clear().draw();
    customerLedgerTable.ajax.url((window.basePath + "AccountNfinance/AFPaymentReceiptManagement/populateInvoiceListByParam?customerId=" + customerId + "&operationType=" + operationType)).load();

}
/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListCustomer").on("change", function () {
        var customerId = $("#DropDownListCustomer :selected").val();
        getInvoiceReceiptList(customerId);
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
    getCustomerList();
    getvPaymentMethodList();
    domCustomerLedgerTable();
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
    var form = document.getElementById("AFPaymentReceipt");
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
function createUpdateDataIntoDB(btnElement) {
    if (!validater()) return;

    var tr = $(btnElement).closest('tr');
    var rowData = invoiceTable.row(tr).data();
    var guID = rowData.guID;
    var invoiceId = rowData.invoiceId;

    var receiptAmount = parseFloat($('#TextBoxReceiptAmount_' + guID).val());
    if (!receiptAmount || receiptAmount <= 0) {
        toastr.warning("Please enter a valid payment amount.");
        return;
    }

    var jsonData = {
        OperationType: $("#OperationType").val(),
        LocationId: $("#DropDownListLocation :selected").val(),
        TransactionDate: $("#TextBoxTransactionDate").val(),
        CustomerId: $("#DropDownListCustomer :selected").val(),
        PaymentMethodId: $("#DropDownListPaymentMethod :selected").val(),
        Reference: $("#TextBoxReference").val(),
        InvoiceId: invoiceId,
        Description: $('#TextBoxDescription_' + guID).val(),
        ReceiptAmount: receiptAmount,
        PaymentTypeId: $("#PaymentTypeId").val(),
    };
    $.ajax({
        url: window.basePath + "AccountNfinance/AFPaymentReceiptManagement/createUpdatePaymentReceipt",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            initLoading();
            $(btnElement).prop("disabled", true);
        },
        success: function (response) {
            if (response.isSuccess) {
                toastr.success(response.message);
                $("#AFPaymentReceipt").removeClass("was-validated");
                getInvoiceList(jsonData.customerId);
            } else {
                toastr.info(response.message);
            }
        },
        error: function (xhr) {
            toastr.error("System Error: " + xhr.statusText);
        },
        complete: function () {
            stopLoading();
            $(btnElement).prop("disabled", false);
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