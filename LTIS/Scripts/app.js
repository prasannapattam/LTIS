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

$(function () {
    toastr.options.positionClass = 'toast-bottom-right';
    toastr.options.backgroundpositionClass = 'toast-bottom-right';

    toastr.info('Hi Prasanna');
    toastr.error('Hi Prasanna');
});

