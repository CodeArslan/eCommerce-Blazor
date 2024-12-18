window.ShowToastr = function (type, message) {
    if (type == "Success") {
        toastr.success(message);
    }
    if (type == "Error") {
        toastr.error(message);
    }
}