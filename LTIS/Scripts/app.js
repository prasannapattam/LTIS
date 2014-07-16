function loadNotesWindow(id, name, type) {
    var notesWindow = $("#" + id);
    if (!notesWindow.data("kendoWindow")) {
        notesWindow.kendoWindow({
            width: "400px",
            height: "350px",
            title: type + " for " + name
        });
    }

    notesWindow.data("kendoWindow").open().center();

    return false;
}

function UpdateContact(id, url) {
    
    var trrow = $("#" + id);
    var rep = $("#" + id + "-rep").val();
    var action = $("input:checked", trrow).val();

    if (action === undefined) {
        alert('Action is needed, please select an action');
        return;
    }

    if (action !== "Remove" && rep === "") {
        alert('Please select a Sales Rep to continue');
        return;
    }
    
    var contact = { ContactID: id, Action: action, SalesRep: rep };
    $("#divLoading").show();
    $.post(url, contact)
    .done(function (data) {
        $("#divLoading").hide();
        if (data.Success === false) {
            toastr.error(data.Message);
        }
        else {
            toastr.info(data.Message);
            $(trrow).remove();
        }
    }).fail(function (e) {
        $("#divLoading").hide();
            var message = '';
            if (e.responseJSON.ExceptionMessage !== undefined)
                message = e.responseJSON.ExceptionMessage;
            else
                message = e.responseJSON.Message;
            toastr.error(message);
     });



    return false;
}

$(function () {
    toastr.options.positionClass = 'toast-bottom-right';
    toastr.options.backgroundpositionClass = 'toast-bottom-right';

    //toastr.info('Hi Prasanna');
    //toastr.error('Hi Prasanna');

    //$("#divLoading").ajaxStart(function () { $(this).show(); })
    //               .ajaxStop(function () { $(this).hide(); });
});