$(document).ready(function () {
    $('#form_login').submit(function (e) {
        var user_name = $('#userName').val();
        var password = $('#password').val();

        if (user_name.length < 1) {
            $('#error_username').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#error_username').hide();
            ischeck = false;
        }

        if (password.length < 1) {
            $('#error_password').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#error_password').hide();
            ischeck = false;
        }

        if (ischeck == true) {
            e.preventDefault();
        }
    });

    $('#res_btn').click(function () {
        $('#form_login').trigger("reset");
        $(".error").hide();

    });

});
