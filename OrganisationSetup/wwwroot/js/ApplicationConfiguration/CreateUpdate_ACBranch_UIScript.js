/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";

/* ------ Depending DDL's ------ */
function getvOrganisationTypeList() {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACBranchManagement/populatevOrganisationTypeListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListOrganisationType").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListOrganisationType").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvCountryList() {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACBranchManagement/populatevCountryListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCountry").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListCountry").append(new Option(item.description, item.id));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getvCityList(countryId,cityId) {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACBranchManagement/populatevCityListByParam",
        type: "GET",
        data: { countryId: countryId },
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCity").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                var selectedOption = (item.id == cityId);
                $("#DropDownListCity").append(new Option(item.description, item.id, selectedOption, selectedOption));
            });
        },
        complete: function () {

        },
        error: function (error) {
            console.error("Error: " + error);
        }
    });
}

/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListCountry").on("change", function () {
        var cityId = 23;
        var countryId = $("#DropDownListCountry :selected").val();
        getvCityList(countryId, cityId);
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
    getvOrganisationTypeList();
    getvCountryList();
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
    var form = document.getElementById("ACBranchForm");
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
    var organisationTypeId = $("#DropDownListOrganisationType :selected").val();
    var countryId = $("#DropDownListCountry :selected").val();
    var cityId = $("#DropDownListCity :selected").val();
    var contact = $("#TextBoxContact").val();
    var email = $("#TextBoxEmail").val();
    var ntnNumber = $("#TextBoxNTNNumber").val();
    var address = $("#TextAreaAddress").val();

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        OrganisationTypeId: organisationTypeId,
        Description: description,
        CountryId: countryId,
        CityId: cityId,
        Contact: contact,
        Email: email,
        NTNNumber: ntnNumber,
        Address: address
    };
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACBranchManagement/createUpdateBranch",
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

/* ------ Load Dummy Record ------ */
function loadDummyRecord() {
    $("#TextBoxDescription").val("ZT ISLAMABAD");
    $("#DropDownListCountry").val(168).trigger('change');
    getvCityList(168, 26);
}
