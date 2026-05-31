/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1' " + (operationType == "INSERT_INTO_DB" ? "selected='selected'" : "") + ">Select an option</option>";
var customerList = [];
var billTable = "";
/* ------ DOM Elements ------ */


function domBillTable() {
    billTable = $('#TableBill').DataTable({
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
                "title": "Bill Summary",
                "data": null,
                "render": function (data, type, row) {
                    return "<b>Net Amount:</b> " + data.netAmount.toFixed(2) + "<br>" +
                        "<hr style='margin: 5px 0;'>" +
                        "<b>Due Amount:</b> " + data.dueAmount.toFixed(2);
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
                "title": "Bill Status",
                "data": null,
                "render": function (data, type, row) {
                    return GetInvoiceStatus(data.billStatus)
                }
            },
            {
                "title": "Action(s)",
                "data": null,
                "render": function (data, type, row) {
                    return '<input type="button" class="btn btn-sm btn-success"onclick="createUpdateDataIntoDB(this)" id="ButtomSubmitPayment' + data.guID + '" value="Record Payment" />'
                }
            },
            { "data": "guID", "title": "GuID", visible:false },
            { "data": "billId", "title": "BillId", visible: false },
        ],
        columnDefs: [
        ],
    });
    billTable.on('order.dt search.dt', function () {
        billTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();
}

/* ------ Depending DDL's ------ */
function getBranchList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillReceiptManagement/populateBranchListByParam",
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
function getSupplierList(supplierId) {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillReceiptManagement/populateSupplierByParam",
        type: "GET",
        dataType: "json",
        data: { operationType: operationType },
        success: function (data) {
            supplierList = data;
            var $ddl = $("#DropDownListSupplier");
            $ddl.select2({
                theme: 'bootstrap-5',
                width: '100%',
                placeholder: 'Search Supplier...',
                allowClear: true,
                data: supplierList,
                minimumInputLength: 1,
            });
            if (supplierId) {
                $ddl.val(supplierId).trigger('change');
            }
        },
        error: function (xhr, status, error) {
            console.error("Customer load failed: " + error);
        }
    });
}
function getvPaymentMethodList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillReceiptManagement/populatevPaymentMethodListByParam",
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
function getBillList(supplierId) {
    billTable.clear().draw();
    billTable.ajax.url((window.basePath + "AccountNfinance/AFBillReceiptManagement/populateBillListByParam?supplierId=" + supplierId + "&operationType=" + operationType)).load();

}
/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListSupplier").on("change", function () {
        var supplierId = $("#DropDownListSupplier :selected").val();
        getBillList(supplierId);
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
    domBillTable();
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    $('.select2:not(#DropDownListSupplier)').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });
    getSupplierList(null);
    changeEventHandler();
}
/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("AFBillReceipt");
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
    var rowData = billTable.row(tr).data();
    var guID = rowData.guID;
    var billId = rowData.billId;

    var receiptAmount = parseFloat($('#TextBoxReceiptAmount_' + guID).val());
    if (!receiptAmount || receiptAmount <= 0) {
        toastr.warning("Please enter a valid payment amount.");
        return;
    }

    var jsonData = {
        OperationType: $("#OperationType").val(),
        LocationId: $("#DropDownListLocation :selected").val(),
        TransactionDate: $("#TextBoxTransactionDate").val(),
        supplierId: $("#DropDownListSupplier :selected").val(),
        PaymentMethodId: $("#DropDownListPaymentMethod :selected").val(),
        Reference: $("#TextBoxReference").val(),
        BillId: billId,
        Description: $('#TextBoxDescription_' + guID).val(),
        ReceiptAmount: receiptAmount,
        PaymentTypeId: $("#PaymentTypeId").val(),
    };
    $.ajax({
        url: window.basePath + "AccountNfinance/AFBillReceiptManagement/createUpdateBillReceipt",
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
            $("#AFBillReceipt").removeClass("was-validated");
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
    $(".form-control").not("#DropDownListLocation").val('');
    $(".select2").not("#DropDownListLocation").val('-1').trigger("change");

}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});