toastr.options = {
    "closeButton": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "timeOut": "3000"
};
/* ------ UI Blockers ------ */
function initLoading() {
    $("#ui-loader").stop(true, true).fadeIn(200);
    $("html").css("pointer-events", "none");
}

function stopLoading() {
    $("#ui-loader").stop(true, true).fadeOut(200);
    $("html").css("pointer-events", "auto");
}

function setupGlobalAjax() {
    $(document).ajaxStart(function () {
        initLoading();
    });

    $(document).ajaxStop(function () {
        stopLoading();
    });

    $(document).ajaxError(function (event, xhr, settings, thrownError) {
        stopLoading();
        toastr.error("Request failed: " + thrownError, "System Error");
    });
}