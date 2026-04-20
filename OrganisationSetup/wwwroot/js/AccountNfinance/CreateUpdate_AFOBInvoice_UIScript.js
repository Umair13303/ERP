/* ============================================================
   createupdate_afobinvoice_uiscript.js
   Outstanding Invoice Payment — UI Script
   ============================================================ */

/* ------ Global Variables ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value=''>Select an option</option>";
var pendingInvoiceData = [];   // Raw data from server
var filteredInvoiceData = [];   // After client-side filters

/* ------ Currency Formatter ------ */
function formatCurrency(amount) {
    var rounded = Math.round(amount || 0);
    return "Rs. " + rounded.toLocaleString("en-PK");
}

/* ------ Status Badge HTML ------ */
function getStatusBadge(status) {
    var map = {
        "Paid": { cls: "bg-success", text: "Paid" },
        "Partial": { cls: "bg-warning text-dark", text: "Partial" },
        "Pending": { cls: "bg-danger", text: "Pending" }
    };
    var s = map[status] || { cls: "bg-secondary", text: status };
    return '<span class="badge ' + s.cls + '">' + s.text + '</span>';
}

/* ============================================================
   DDL Population
   ============================================================ */

function getBranchList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/populateBranchListByParam",
        type: "GET",
        data: { operationType: operationType },
        dataType: "json",
        success: function (data) {
            var $ddl = $("#DropDownListLocation").empty().append(dropDownListInitOption);
            $.each(data, function (i, item) {
                var selected = (item.id === 1);
                $ddl.append(new Option(item.description, item.id, selected, selected));
            });
            $ddl.trigger("change");
        },
        error: function (xhr) {
            toastr.error("Failed to load branch list: " + xhr.statusText);
        }
    });
}

function getCustomerList() {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/populateCustomerList",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var $ddl = $("#DropDownListCustomer").empty().append('<option value="">All Customers</option>');
            $.each(data, function (i, item) {
                $ddl.append(new Option(item.description, item.id));
            });
            $ddl.trigger("change");
        },
        error: function (xhr) {
            toastr.error("Failed to load customer list: " + xhr.statusText);
        }
    });
}

/* ------ Load Document List (UPDATE mode only) ------ */
function getDocumentList() {
    if (operationType !== "UPDATE_DATA_INTO_DB") return;
    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/populatePaymentDocumentList",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var $ddl = $("#DropDownListDocument").empty().append(dropDownListInitOption);
            $.each(data, function (i, item) {
                $ddl.append(new Option(item.description, item.id));
            });
            $ddl.trigger("change");
        },
        error: function (xhr) {
            toastr.error("Failed to load document list: " + xhr.statusText);
        }
    });
}

/* ============================================================
   Pending Invoice Fetch & Render
   ============================================================ */

function loadPendingInvoices(customerId) {
    var params = {};
    if (customerId && customerId !== "") {
        params.customerId = customerId;
    }

    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/populateInvoiceListByParam",
        type: "GET",
        data: params,
        dataType: "json",
        beforeSend: function () {
            if (typeof initLoading === "function") initLoading();
        },
        success: function (data) {
            pendingInvoiceData = data || [];
            renderInvoiceTable();
        },
        error: function (xhr) {
            toastr.error("Failed to load invoices: " + xhr.statusText);
        },
        complete: function () {
            if (typeof stopLoading === "function") stopLoading();
        }
    });
}

/* ------ Client-side filter then render ------ */
function applyFiltersAndRender() {
    var searchText = $("#TextBoxSearchInvoice").val().toLowerCase().trim();
    var statusFilter = $("#DropDownListStatusFilter").val();

    filteredInvoiceData = pendingInvoiceData.filter(function (row) {
        var matchSearch = !searchText ||
            (row.customerName || "").toLowerCase().includes(searchText) ||
            (row.invoiceNo || "").toLowerCase().includes(searchText);

        var matchStatus = !statusFilter || row.status === statusFilter;

        return matchSearch && matchStatus;
    });

    renderInvoiceTable();
}

/* ------ Group by customer then render rows ------ */
function renderInvoiceTable() {
    var data = filteredInvoiceData.length > 0 ? filteredInvoiceData : pendingInvoiceData;

    /* Group rows by customerId */
    var groups = {};
    var groupOrder = [];
    $.each(data, function (i, row) {
        var key = row.customerId;
        if (!groups[key]) {
            groups[key] = { customerName: row.customerName, rows: [] };
            groupOrder.push(key);
        }
        groups[key].rows.push(row);
    });

    var $tbody = $("#TBodyPendingInvoices").empty();
    var globalIdx = 1;
    var totalGross = 0, totalPaid = 0, totalBalance = 0;

    $.each(groupOrder, function (gi, cid) {
        var group = groups[cid];
        var gGross = 0, gNet = 0, gPaid = 0, gBal = 0;

        $.each(group.rows, function (ri, r) {
            gGross += (r.grossAmount || 0);
            gNet += (r.netAmount || 0);
            gPaid += (r.paidAmount || 0);
            gBal += ((r.netAmount || 0) - (r.paidAmount || 0));
        });

        totalGross += gGross;
        totalPaid += gPaid;
        totalBalance += gBal;

        /* Group header row */
        var grHtml = '<tr class="table-primary">' +
            '<td colspan="3" class="fw-semibold small ps-3">' +
            '<i class="fa-solid fa-user me-1"></i>' + group.customerName +
            ' <span class="badge bg-primary ms-1">' + group.rows.length + ' invoice' + (group.rows.length > 1 ? "s" : "") + '</span>' +
            '</td>' +
            '<td></td><td></td><td></td>' +
            '<td class="text-end fw-semibold">' + formatCurrency(gGross) + '</td>' +
            '<td></td>' +
            '<td class="text-end fw-semibold">' + formatCurrency(gNet) + '</td>' +
            '<td class="text-end text-success fw-semibold">' + formatCurrency(gPaid) + '</td>' +
            '<td class="text-end text-danger fw-semibold">' + formatCurrency(gBal) + '</td>' +
            '<td></td>' +
            '</tr>';
        $tbody.append(grHtml);

        /* Detail rows */
        $.each(group.rows, function (ri, r) {
            var balance = (r.netAmount || 0) - (r.paidAmount || 0);
            var balClass = balance > 0 ? "text-danger" : "text-muted";
            var rowHtml = '<tr class="invoice-detail-row">' +
                '<td class="text-center text-muted small">' + globalIdx + '</td>' +
                '<td class="font-monospace small text-primary">' + (r.invoiceNo || "") + '</td>' +
                '<td class="small">' + (r.invoiceDate || "") + '</td>' +
                '<td class="text-end small">' + formatCurrency(r.totalAmount) + '</td>' +
                '<td class="text-end small">' + formatCurrency(r.saleTax) + '</td>' +
                '<td class="text-end small">' + formatCurrency(r.additionalTax) + '</td>' +
                '<td class="text-end small fw-semibold">' + formatCurrency(r.grossAmount) + '</td>' +
                '<td class="text-end small text-warning">' + formatCurrency(r.totalDiscount) + '</td>' +
                '<td class="text-end small fw-semibold">' + formatCurrency(r.netAmount) + '</td>' +
                '<td class="text-end small text-success">' + formatCurrency(r.paidAmount) + '</td>' +
                '<td class="text-end small ' + balClass + '">' + formatCurrency(balance) + '</td>' +
                '<td class="text-center">' + getStatusBadge(r.status) + '</td>' +
                '</tr>';
            $tbody.append(rowHtml);
            globalIdx++;
        });

        /* Subtotal row */
        var stHtml = '<tr class="table-light">' +
            '<td colspan="3" class="small text-muted ps-4 fst-italic">Subtotal — ' + group.customerName + '</td>' +
            '<td></td><td></td><td></td>' +
            '<td class="text-end small fw-semibold">' + formatCurrency(gGross) + '</td>' +
            '<td></td>' +
            '<td class="text-end small fw-semibold">' + formatCurrency(gNet) + '</td>' +
            '<td class="text-end small text-success fw-semibold">' + formatCurrency(gPaid) + '</td>' +
            '<td class="text-end small text-danger fw-semibold">' + formatCurrency(gBal) + '</td>' +
            '<td></td>' +
            '</tr>';
        $tbody.append(stHtml);
    });

    /* Update summary strip */
    var totalInvoices = data.length;
    $("#BadgeInvoiceCount").text(totalInvoices);
    $("#SumTotalInvoices").text(totalInvoices);
    $("#SumGrossAmount").text(formatCurrency(totalGross));
    $("#SumTotalPaid").text(formatCurrency(totalPaid));
    $("#SumTotalBalance").text(formatCurrency(totalBalance));

    /* DataTable info line */
    var groupCount = groupOrder.length;
    $("#LabelDataTableInfo").text(
        "Showing " + totalInvoices + " invoice" + (totalInvoices !== 1 ? "s" : "") +
        " across " + groupCount + " customer" + (groupCount !== 1 ? "s" : "")
    );
}

/* ============================================================
   Event Handlers
   ============================================================ */

function changeEventHandler() {



    /* Save / Update button */
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        e.preventDefault();
        if (validator()) {
            createUpdateDataIntoDB();
        }
    });

    /* Clear button */
    $("#ButtonClearForm").on("click", function () {
        clearInputFields();
    });

    /* Customer DDL change → reload invoices */
    $("#DropDownListCustomer").on("change", function () {
        var customerId = $(this).val();
        loadPendingInvoices(customerId);
    });

    /* Document DDL change (UPDATE mode) → load payment into form */
    $("#DropDownListDocument").on("change", function () {
        var docGuID = $(this).val();
        if (docGuID && docGuID !== "") {
            loadPaymentDocument(docGuID);
        }
    });

    /* Search & status filter */
    $("#TextBoxSearchInvoice").on("input", function () {
        applyFiltersAndRender();
    });

    $("#DropDownListStatusFilter").on("change", function () {
        applyFiltersAndRender();
    });
}

/* ============================================================
   Load Payment Document for UPDATE mode
   ============================================================ */

function loadPaymentDocument(docGuID) {
    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/getPaymentDocumentByGuID",
        type: "GET",
        data: { guID: docGuID },
        dataType: "json",
        beforeSend: function () {
            if (typeof initLoading === "function") initLoading();
        },
        success: function (data) {
            if (data) {
                $("#GuID").val(data.guID);
                $("#TextBoxTransactionDate").val(data.transactionDate);
                $("#DropDownListLocation").val(data.locationId).trigger("change");
                $("#DropDownListCustomer").val(data.customerId).trigger("change");
                $("#DropDownListPaymentMode").val(data.paymentMode).trigger("change");
                $("#TextBoxReferenceNo").val(data.referenceNo);
                $("#TextBoxRemarks").val(data.remarks);
            }
        },
        error: function (xhr) {
            toastr.error("Failed to load document: " + xhr.statusText);
        },
        complete: function () {
            if (typeof stopLoading === "function") stopLoading();
        }
    });
}

/* ============================================================
   Validation
   ============================================================ */

function validator() {
    var form = document.getElementById("OSInvoiceForm");
    if (!form.checkValidity()) {
        form.classList.add("was-validated");
        var $firstInvalid = $(form).find(":invalid").first();
        if ($firstInvalid.length) {
            $firstInvalid.trigger("focus");
        }
        toastr.warning("Please fill in all required fields correctly.");
        return false;
    }
    return true;
}

/* ============================================================
   Save / Update AJAX Call
   ============================================================ */

function createUpdateDataIntoDB() {
    var jsonData = {
        OperationType: operationType,
        GuID: $("#GuID").val() || null,
        TransactionDate: $("#TextBoxTransactionDate").val(),
        LocationId: $("#DropDownListLocation :selected").val(),
        CustomerId: $("#DropDownListCustomer :selected").val() || null,
        PaymentMode: $("#DropDownListPaymentMode :selected").val(),
        ReferenceNo: $("#TextBoxReferenceNo").val(),
        Remarks: $("#TextBoxRemarks").val(),
        DocumentGuID: $("#DropDownListDocument :selected").val() || null,
    };

    console.log("Payload →", jsonData);

    $.ajax({
        url: window.basePath + "AccountNfinance/AFInvoiceManagement/createUpdateOSInvoicePayment",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            if (typeof initLoading === "function") initLoading();
            $("#ButtonSaveData, #ButtonUpdateData").prop("disabled", true);
        },
        success: function (response) {
            if (response.IsSuccess === true) {
                toastr.success(response.message);
                $("#OSInvoiceForm").removeClass("was-validated");
                clearInputFields();
                loadPendingInvoices();
            } else {
                toastr.info(response.message);
            }
        },
        error: function (xhr) {
            toastr.error("System Error: " + xhr.statusText);
        },
        complete: function () {
            if (typeof stopLoading === "function") stopLoading();
            $("#ButtonSaveData, #ButtonUpdateData").prop("disabled", false);
        }
    });
}

/* ============================================================
   Clear / Reset
   ============================================================ */

function clearInputFields() {
    $("#OSInvoiceForm")[0].reset();
    $("#OSInvoiceForm").removeClass("was-validated");
    $(".select2").val("").trigger("change");
    $("#TextBoxTransactionDate").val(getTodayDate());
    pendingInvoiceData = [];
    filteredInvoiceData = [];
    renderInvoiceTable();
}

/* ============================================================
   Helpers
   ============================================================ */

function getTodayDate() {
    return new Date().toISOString().split("T")[0];
}

/* ============================================================
   Initialization
   ============================================================ */

function initialize() {
    /* Date masking / input masking */
    if (typeof UIMasking !== "undefined") {
        var inputMasking = new UIMasking();
        inputMasking.initialize();
    }

    /* Select2 */
    $(".select2").select2({
        theme: "bootstrap-5",
        width: "100%"
    });

    /* Set today as default date */
    $("#TextBoxTransactionDate").val(getTodayDate());

    /* Load DDLs */
    getBranchList();
    getCustomerList();
    getDocumentList();

    /* Load all pending invoices on start */
    loadPendingInvoices();

    /* Wire up events */
    changeEventHandler();
}

/* ============================================================
   Document Ready
   ============================================================ */

$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});