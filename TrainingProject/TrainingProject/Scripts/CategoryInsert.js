$(document).ready(function () {
    $('#form_category').submit(function (e) {
        var name = $('#category_name').val();
        var desc = $('#category_description').val();

        if (name.length < 1 ){
            $('#error_category_name').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#error_category_name').hide();
            ischeck = false;
        }

        if (desc.length < 1) {
            $('#error_category_description').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#error_category_description').hide();
            ischeck = false;
        }

        if (ischeck == true) {
            e.preventDefault();
        }
    });
    $('#res_btn').click(function () {
        $('#category_description').empty();
        $('#category_name').empty();
        $(".error").hide();
        $('#view_messageInsert').hide();
    });
});