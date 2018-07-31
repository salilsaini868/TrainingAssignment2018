$(document).ready(function () {
    $('#link_edit').click(function (e) {
        if (!window.confirm("Confirm Edit")) {
            e.preventDefault();
        }
    });
    $('#link_delete').click(function (e) {
        if (!window.confirm("Confirm Delete")) {
            e.preventDefault();
        }
    });
});