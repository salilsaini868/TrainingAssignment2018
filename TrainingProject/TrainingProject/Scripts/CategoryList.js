$(document).ready(function () {    
    $('.link_delete').click(function (e) {
        if (!window.confirm("Confirm Delete")) {
            e.preventDefault();
        }
    });
    $('#full_list').click(function() {
        $('#txtSearch').val("");
    });
});