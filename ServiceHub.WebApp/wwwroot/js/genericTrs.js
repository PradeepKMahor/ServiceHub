//const { min } = require("../lib/moment.js/moment-with-locales");

function loadScript() {
    openModalListner();
    checkFilteredMarker();
    $('#modalcontainer, #modalreadonlycontainer').off('shown.bs.modal').on('shown.bs.modal', function () {
        var form = $(this).find("form");
        $.validator.unobtrusive.parse(form);
    });

    $('.nav-link').off('click').on('click', function () {
        loadView(this);
    });
}

function openModalListner() {
    $('*[data-jqueryselector$=openmodal]').off('click').on('click', function () {

        var modalcontainer = $(this).data('modalcontainer');
        modalcontainer = $('#' + modalcontainer);

        var modalcontent = modalcontainer.find('#modal-content');
        var modaldismiss = $(this).data('modaldismiss');
        if (modaldismiss == 'undefined')
            modaldismiss = 'false';

        var modalpopupwidth = $(this).data('modalpopupwidth');
        if (modalpopupwidth != null) {
            modalcontainer.removeClass("rightw25");
            modalcontainer.removeClass("rightw35");
            modalcontainer.removeClass("rightw50");
            modalcontainer.removeClass("rightw75");
            modalcontainer.addClass(modalpopupwidth);
        }

        var cache = $(this).data('cache');
        if (cache != true) {
            cache = false;
        }

        var modaltitle = $(this).data('modaltitle');
        var modalheader = $(this).data('modalheader');

        modalcontainer.find('#modaltitle').text(modaltitle);
        modalcontainer.find('#modalheader').text(modalheader);

        var staticHTMLSourceDivId = $(this).data('statichtmlsourcedivid');

        if (staticHTMLSourceDivId) {
            staticHTMLSourceDivId = '#' + staticHTMLSourceDivId;
            var staticHTML = $(staticHTMLSourceDivId).html();
            modalcontent.html(staticHTML);
            modalcontainer.modal();
            return;
        }

        var jsUrl = $(this).data('url');

        var dataParams = $(this).data();


        delete dataParams["modalheader"];
        delete dataParams["modaltitle"];
        delete dataParams["url"];
        delete dataParams["statichtmlsourcedivid"];
        delete dataParams["cache"];
        delete dataParams["modalcontainer"];
        delete dataParams["jqueryselector"];
        delete dataParams["modalpopupwidth"];


        $.ajax({
            url: jsUrl,
            type: "GET",
            data: dataParams,

            success: function (data) {
                modalcontent.html(data);

                if (modaltitle !== null && modaltitle !== "undefined")
                    modalcontainer.find('#modaltitle').text(modaltitle);
                if (modalheader !== null && modalheader !== "undefined")
                    modalcontainer.find('#modalheader').text(modalheader);

                if (modalcontainer[0].id == "modalcontainer" && modaldismiss != "auto")
                    modalcontainer.modal({ backdrop: 'static', keyboard: false });
                else
                    modalcontainer.modal();

                if (typeof localScript == 'function') {
                    localScript();
                }

            },
            error: function (result) {
                errorText = result.responseText.indexOf("divErrorMessage") == -1 ? result.responseText : ($(result.responseText).find("div[id*=divErrorMessage]").text()).replace("text-danger", "text-white");
                notify("error", errorText, 'Error');
            }
        });
    });
}

function showDeleteConfirmationPopup(event, obj) {

    event.preventDefault();
    event.stopPropagation();

    var data = $(obj).data();
    var strData = JSON.stringify(data);
    showDeleteConfirmation(strData);
    return false;
    //});

}

function showDeleteConfirmation(jsonString) {

    var dataParams = JSON.parse(jsonString);

    var sTitle = dataParams["modaltitle"];
    delete dataParams["modaltitle"];

    var sConfirmationText = dataParams["confirmationtext"];
    delete dataParams["confirmationtext"];

    var sConfirmationType = dataParams["confirmationtype"]
    delete dataParams["confirmationtype"];

    var sConfirmButtonText = dataParams["confirmbuttontext"]
    delete dataParams["confirmbuttontext"];

    var sUrl = dataParams["url"]
    delete dataParams["url"];

    delete dataParams["jqueryselector"];


    var sRedirectUrl = dataParams["redirecturl"];
    delete dataParams["redirecturl"];

    var sPostSaveReloadDataTable = dataParams["postsavereloaddatatable"];
    delete dataParams["postsavereloaddatatable"];

    var sPostDeleteReLoadDataTables = dataParams["postdeletereloaddatatables"];
    delete dataParams["postdeletereloaddatatables"];


    var bDoRedirect = sRedirectUrl != undefined;

    if (bDoRedirect)
        bDoRedirect = sRedirectUrl.length > 0;

    swal({
        title: sTitle,
        text: sConfirmationText,
        type: sConfirmationType, // warning / error / success /info
        showCancelButton: true,
        confirmButtonClass: "btn-danger btn-sm",
        cancelButtonClass: "btn-sm",
        confirmButtonText: sConfirmButtonText,
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: "POST",
            url: sUrl,
            data: dataParams,
            beforeSend: function () {
                showLoader();
            },
            success: function (data) {
                if (data.success) {
                    if (bDoRedirect)
                        window.location = sRedirectUrl;
                    else if (sPostSaveReloadDataTable == "OK") {
                        if (typeof PostSaveReLoadDataTable == 'function') {
                            PostSaveReLoadDataTable(data);
                        }
                    }
                    else if (sPostDeleteReLoadDataTables == "OK") {
                        if (typeof PostDeleteReLoadDataTables == 'function') {
                            PostDeleteReLoadDataTables(data);
                        }
                    }
                    else {
                        notify("success", data.message, "Success")
                        hideLoader();
                        localScript();
                    }
                } else {
                    hideLoader();
                    notify("error", data.message, "Error")
                }
            },
            error: function (xhr) {
                hideLoader();
                var errorText = xhr.responseText.indexOf("divErrorMessage") == -1 ? xhr.responseText : ($(xhr.responseText).find("div[id*=divErrorMessage]").text()).replace("text-danger", "text-white");
                notify("error", errorText, 'Error');
            }
        });
    });

}
function showLoader() {
    $(".load-bar").removeClass('d-none');
}
function showModalLoader() {
    $(".load-bar-modal").removeClass('d-none');
}

function hideLoader() {
    $(".load-bar").addClass('d-none');
}

function hideModalLoader() {
    $(".load-bar-modal").addClass('d-none');
}
function initSelect2() {
    $('.Select2').select2({
        placeholder: "Select an option",
        allowClear: true,
        width: "100%"
    });

    $('.chosen-select').select2({
        placeholder: "Select an option",
        allowClear: true,
        width: "100%"
    });

    $('.chosen-select-multiple').select2({
        placeholder: "Select an option",
        allowClear: true,
        width: "100%"
    });

}


$(document).ready(function () {
    initSelect2();

    checkFilteredMarker();

    $("#idDeskTopMenuButton").on('click', function () {
        //return;
        $(".app-navbar").toggleClass("app-navbar-collapsed");
        $(".mobile-menu").toggleClass("on");
        toggleMenuState();
    });
    $("#idMobileMenuButton").on('click', function () {
        //return;
        $(".app-navbar").toggleClass("app-navbar-collapsed");
        $(".app-navbar").toggleClass("mobile-sidemenu");
        $(this).toggleClass("on");

        //$(".mobile-menu").toggleClass("on");
        //toggleMenuState();
    });
    $("a.has-sub-menu").on('click', function () {

        var isOpen = $(this).hasClass('has-sub-menu-selected');

        $("a.has-sub-menu.has-sub-menu-selected").each(function () {
            $(this).toggleClass('has-sub-menu-selected');
            $(this).next("ul").slideToggle();
        });

        if (!isOpen) {
            $(this).toggleClass('has-sub-menu-selected');
            $(this).next("ul").slideToggle();
        }
    });
    if (typeof initToastrMessage == 'function') {
        initToastrMessage();
    }
    else {
        console.log("InitToastr NotFound");
    }

    loadScript();

});

function toggleMenuState() {
    //r.style.setProperty('--navbar-width', '264px');
    var r = document.querySelector(':root');
    var oObjects = getComputedStyle(r);
    var navBarWidth = oObjects.getPropertyValue('--navbar-width');
    if (navBarWidth == '264px')
        r.style.setProperty('--navbar-width', '80px');
    else {
        r.style.setProperty('--navbar-width', '264px');
    }
}

function ofbfileDownload(event, object) {
    event.preventDefault();
    event.stopPropagation();


    var data = $(object).data();
    var strData = JSON.stringify(data);

    FileDownload(strData);
    return false;

}

function FileDownload(jsonString) {

    //console.log(jsonString);
    var dataParams = JSON.parse(jsonString);

    var sUrl = dataParams["url"]
    delete dataParams["url"];

    var sClientFileName = dataParams["clientfilename"]
    delete dataParams["clientfilename"];

    var sKeyId = dataParams["keyid"]
    delete dataParams["keyid"];



    $.ajax({

        url: sUrl,
        type: "GET",
        cache: false,
        data: {
            keyid: sKeyId
        },
        //xhrFields: {
        //    responseType: 'blob'
        //},
        xhr: function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 2) {
                    if (xhr.status == 200) {
                        xhr.responseType = "blob";
                    }
                }
            };
            return xhr;
        },
        beforeSend: function () {
            showLoader();
        },
        success: function (data) {
            var link = document.createElement('a');
            var url = window.URL.createObjectURL(data);

            link.href = window.URL.createObjectURL(data);
            link.download = sClientFileName;
            link.click();
            link.remove();
            window.URL.revokeObjectURL(url);
            hideLoader();
            toastr.success("File downloaded successfully.");
        },
        error: function (xhr) {
            showError(xhr);
            hideLoader();
        }
    });

}

// Ajax form events
function onBegin(btn) {
    if (btn == null)
        btn = $('#btnconfirm');

    btnDisable(btn);
    showModalLoader();
    $("#modal-loader").show();
}

function onComplete(btn) {
    if (btn == null)
        btn = $('#btnconfirm');
    btnEnable(btn);
    hideModalLoader();
    $("#modal-loader").hide();
    loadScript();
}

function onSuccess() {
    $('#modalcontainer').modal('hide');
}

function onSuccessTab(tabname) {
    console.log("onSuccessTab(tabname)");

    $('#modalcontainer').modal('hide');
    $('#tabs-' + tabname + '-tab').click();
}

function onSuccessRedirect(redirecturl) {
    $('#modalcontainer').modal('hide');
    window.location = redirecturl;

}

/*
function reDirectURL(reDirectUrl) {
    if (reDirectUrl == "/HRMasters/CostCenter/DeactivateCostCenter") {
        window.location = "/HRMasters/CostCenter";
    }

}
*/

function onError(xhr) {
    errorText = xhr.responseText.indexOf("divErrorMessage") == -1 ? xhr.responseText : ($(xhr.responseText).find("div[id*=divErrorMessage]").text()).replace("text-danger", "text-white");

    notify("error", errorText, 'Error');

    //    notify('error', xhr.responseText, 'danger');
}

function showError(xhr) {
    //console.log(xhr);
    //return;
    errorText = xhr.responseText.indexOf("divErrorMessage") == -1 ? xhr.responseText : ($(xhr.responseText).find("div[id*=divErrorMessage]").text()).replace("text-danger", "text-white");

    notify("error", errorText, 'Error');
}

function onDataTableReloadSuccess() {
    location.reload();
}

// Button spinner
function btnDisable(btn) {
    btn.prop("disabled", true);
    btn.html(btn.data('loading-text'));
}

function btnEnable(btn) {
    btn.prop("disabled", false);
    btn.html(btn.data('text'));
}

function loadView(element) {
    var divid = $(element).data('divid');

    if (divid == null)
        return;

    var id = $(element).data('id');
    var guidid = $(element).data('guidid');
    var parameter = $(element).data('parameter');
    var headertitle = $(element).data('headertitle');
    var containerid = $(element).data('divcontainerid');

    var div = $('#' + divid);
    var area = $(element).data('area');
    var controller = $(element).data('controller');
    var action = $(element).data('action');
    var partialviewname = $(element).data('partialviewname');
    var divcontainer = $('#tabs-' + containerid);

    //var jsUrl = '/' + area + '/' + controller + '/' + action;

    //jsUrl = jsUrl.replace('//', '/');

    var jsUrl = $(element).data('url');

    var cache = $(element).data('cache');

    if (cache != true) {
        cache = false;
    }

    $.ajax({
        url: jsUrl,
        dataType: 'html',
        type: 'GET',
        cache: cache,
        data: {
            'id': id,
            'guidid': guidid,
            'headertitle': headertitle,
            'parameter': parameter,
            'partialviewname': partialviewname
        },
        beforeSend: function () {
            //showLoader();
        },
        success: function (data) {
            $(divcontainer).find('div[id^="pw"]').each(function () {
                $(this).html('');
            });

            div.html(data);

            loadScript();

            if (typeof localScript == 'function') {
                localScript();
            }

            //hideLoader();
        },
        error: function (xhr) {
            hideLoader();
            var errorText = xhr.responseText.indexOf("divErrorMessage") == -1 ? xhr.responseText : ($(xhr.responseText).find("div[id*=divErrorMessage]").text()).replace("text-danger", "text-white");
            notify("error", errorText, 'Error');
        }
    });
}

function resetFilter(event, element) {
    event.preventDefault();
    var actionId = $(element).data('actionid');
    var url = $(element).data('url');
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        data: {
            actionId: actionId
        },
    }).done(function (result) {
        window.location.reload();
        /*
        if (typeof resetFilterDatePicker === "function") {
            resetFilterDatePicker();
        }

        $("input[name^='FilterDataModel.'], input[name^='FilterDataModel_']").each(function (index, item) {
            $(item).val('');
        });
        checkFilteredMarker();

        $('#buttonSearch').click();*/
    });
}
function minutesToHours(minutes) {

    let bIsNegative = minutes < 0;
    let positiveMinutes = bIsNegative ? minutes * -1 : minutes;
    let HH = Math.floor(positiveMinutes / 60);
    let MI = Math.floor(positiveMinutes % 60);
    let retVal = bIsNegative ? "-" : "" + ((HH < 10) ? ("0" + HH) : HH) + ":" + ((MI < 10) ? ("0" + MI) : MI);
    return retVal;
}


function checkFilteredMarker() {
    var result = false;

    $("input[name^='FilterDataModel.'], input[name^='FilterDataModel_'], input[name^='GenericSearch']").each(function (index, item) {
        if ($(item).val().length > 0) {
            result = true;
        }
    });

    if (result == true) {
        $('.filteredmarker-border').removeClass('border-white');
        $('.filteredmarker-visibility').show();
        $('#isFiltered').addClass('text-c-red');

    } else {
        $('.filteredmarker-border').addClass('border-white');
        $('.filteredmarker-visibility').hide();
        $('#isFiltered').removeClass('text-c-red');
    }

}