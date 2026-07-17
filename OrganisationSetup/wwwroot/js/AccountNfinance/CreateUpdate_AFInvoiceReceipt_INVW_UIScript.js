/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1' " + (operationType == "INSERT_INTO_DB" ? "selected='selected'" : "") + ">Select an option</option>";
var customerList = [];
var invoiceTable = "";
/* ------ DOM Elements ------ */
function domInvoiceTable() {
    invoiceTable = $('#TableInvoice').DataTable({
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
            {
                "title": "Invoice Summary",
                "data": null,
                "render": function (data, type, row) {
                    return "<b>Net Total:</b> " + data.netAmount.toFixed(2) + "<br>" +
                        "<b>Receivable:</b> " + data.dueAmount.toFixed(2);
                }
            },
            {
                "title": "Description",
                "data": null,
                "render": function (data, type, row) {
                    return '<input type="text" class="form-control" id="TextBoxDescription_'+data.guID+'" value="Payment Received For Invoice Code: '+ data.code + '" />'
                }
            },
            {
                "title": "Payment",
                "data": null,
                "render": function (data, type, row) {
                    return '<input type="number" class="form-control" id="TextBoxReceiptAmount_'+data.guID+'" value="0" />'
                }
            },
            {
                "title": "Invoice Status",
                "data": null,
                "render": function (data, type, row) {
                    return GetInvoiceStatus(data.invoiceStatus)
                }
            },
            {
                "title": "Action(s)",
                "data": null,
                "render": function (data, type, row) {
                    return `
        <div style="display: flex; gap: 12px; justify-content: center;">
            <!-- Payment Icon -->
            <button type="button" class="btn-icon-only btn-pay" 
                    onclick="createUpdateDataIntoDB(this)" 
                    id="ButtomSubmitPayment${data.guID}" title="Record Payment">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <rect x="1" y="4" width="22" height="16" rx="2" ry="2"></rect>
                    <line x1="1" y1="10" x2="23" y2="10"></line>
                </svg>
            </button>

            <!-- Print Icon -->
            <button type="button" class="btn-icon-only btn-print" 
                    onclick="printThermalInvoice(this)" 
                    id="PrintInvoiceThermalView${data.guID}" title="Print Invoice">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <polyline points="6 9 6 2 18 2 18 9"></polyline>
                    <path d="M6 18H4a2 2 0 0 1-2-2v-5a2 2 0 0 1 2-2h16a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2h-2"></path>
                    <path d="M6 14h12v8H6z"></path>
                </svg>
            </button>
        </div>
    `;
                }
            },
            { "data": "guID", "title": "GuID", visible:false },
            { "data": "invoiceId", "title": "InvoiceId", visible: false },
        ],
        columnDefs: [
        ],
    });
    invoiceTable.on('order.dt search.dt', function () {
        invoiceTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();
}

/* ------ Depending DDL's ------ */
function getBranchList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceReceiptManagement/populateBranchListByParam",
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
        url: window.basePath + "AccountNfinance/AFInvoiceReceiptManagement/populateCustomerListByParam",
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
        url: window.basePath + "AccountNfinance/AFInvoiceReceiptManagement/populatevPaymentMethodListByParam",
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
function getInvoiceList(customerId) {
    invoiceTable.clear().draw();
    invoiceTable.ajax.url((window.basePath + "AccountNfinance/AFInvoiceReceiptManagement/populateInvoiceListByParam?customerId=" + customerId + "&operationType=" + operationType)).load();

}
/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListCustomer").on("change", function () {
        var customerId = $("#DropDownListCustomer :selected").val();
        getInvoiceList(customerId);
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
    domInvoiceTable();
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
    var form = document.getElementById("AFInvoiceReceipt");
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
        url: window.basePath + "AccountNfinance/AFInvoiceReceiptManagement/createUpdateInvoiceReceipt",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            initLoading();
            $(btnElement).prop("disabled", true);
        },
        success: function (response) {
            toastr.success(response.message);
            $("#AFInvoiceReceipt").removeClass("was-validated");
            getInvoiceList($("#DropDownListCustomer :selected").val());
            clearInputFields();
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
    $(".form-control").not("#DropDownListLocation,#DropDownListCustomer").val('');
    $(".select2").not("#DropDownListLocation,#DropDownListCustomer").val('-1').trigger("change");
}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});

function printThermalInvoice(btnElement) {
    var tr = $(btnElement).closest('tr');
    var rowData = invoiceTable.row(tr).data();
    var guID = rowData.guID;
    var url = '/AccountNfinance/AFReport/InvoiceRptThermal?guID=' + encodeURIComponent(guID);
    window.open(url, '_blank', 'width=400,height=700');
}