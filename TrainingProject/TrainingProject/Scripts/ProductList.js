$(document).ready(function () {
    $('.delete').click(function (e) {
        if (!window.confirm("Confirm Delete")) {
            e.preventDefault();
        }
    });


});