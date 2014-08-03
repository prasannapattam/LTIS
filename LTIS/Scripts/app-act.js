function loadNotesWindow(id, name, type) {
    var newWindow = window.open("", "newWindow", "width=450, height=400;resizable=yes");
    newWindow.document.write($("#" + id).html());
    newWindow.focus();
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

    $.ajax({
        type: "POST",
        url: url,
        data: contact,
        success: function (data) {
            $("#divLoading").hide();
            data = JSON.parse(data);
            if (data.Success === false) {
                //toastr.error(data.Message);
                alert(data.Message);
            }
            else {
                alert(data.Message);
                //toastr.info(data.Message);
                $(trrow).remove();
            }
        },
        error: function (e) {
            $("#divLoading").hide();
            data = JSON.parse(data);
            var message = '';
            if (e.responseJSON.ExceptionMessage !== undefined)
                message = e.responseJSON.ExceptionMessage;
            else
                message = e.responseJSON.Message;
            //toastr.error(message);
            alert(message);
        }
    });

    return false;
}


