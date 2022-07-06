window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operational Successful");
        //setInterval(location.reload(), 2000);
    }
    if (type === "error") {
        toastr.error(message, "Operational Failed");
    }
}