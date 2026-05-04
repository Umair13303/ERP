/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1' " + (operationType == "INSERT_INTO_DB" ? "selected='selected'" : "") + ">Select an option</option>";
var customerList = [];
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
        success: function (data) {
            customerList = data;
            var $ddl = $("#DropDownListCustomer");
            $ddl.select2({
                theme: 'bootstrap-5',
                width: '100%',
                placeholder: 'Search customer...',
                allowClear: true,
                data: customerList,
                minimumInputLength: 1,
            });
            if (customerId) {
                $ddl.val(customerId).trigger('change');
            }
        },
        error: function (xhr, status, error) {
            console.error("Customer load failed: " + error);
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
    getvPaymentMethodList();
    domCustomerLedgerTable();
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    $('.select2:not(#DropDownListCustomer)').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });
    getCustomerList(null);
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
function createUpdateDataIntoDB() {
    var operationType = $("#OperationType").val();
    var paymentGuID = $("#GuID").val();
    var locationId = $("#DropDownListLocation :selected").val();
    var transactionDate = $("#TextBoxTransactionDate").val();
    var customerId = $("#DropDownListCustomer :selected").val();
    var description = $("#TextBoxDescription").val();
    var paymentMethodId = $("#DropDownListPaymentMethod :selected").val();
    var reference = $("#TextBoxReference").val();
    var receiptAmount = $("#TextBoxReceiptAmount").val();
    var paymentTypeId = $("#PaymentTypeId").val();

    var jsonData = {
        OperationType: operationType,
        GuID: paymentGuID ? paymentGuID : null,
        LocationId: locationId,
        TransactionDate: transactionDate,
        CustomerId: customerId,
        description: description,
        PaymentMethodId: paymentMethodId,
        Reference: reference,
        ReceiptAmount: receiptAmount,
        PaymentTypeId: paymentTypeId
    };
    $.ajax({
        url: window.basePath + "AccountNfinance/AFPaymentReceiptManagement/createUpdatePaymentReceipt",
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
                $("#AFPaymentReceiptForm").removeClass('was-validated');
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