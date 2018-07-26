$(document).ready(function () {
    $('#form1').submit(function (e) {
        var user_name = $('#userName').val();
        var password = $('#password').val();
        $(".error").remove();

        if (user_name.length < 1) {
            $('#userName').after('<span class="error">This field is required.</span>');
            ischeck = true;
        }
        else {
            ischeck = false;
        }

        if (password.length < 1) {
            $('#password').after('<span class="error">This field is required.</span>');
            ischeck = true;
        }
        else {
            ischeck = false;
        }

        if (ischeck == true) {
            e.preventDefault();
        }
    });

    $('#res_btn').click(function () {
        $('#form1').trigger("reset");
        $(".error").remove();
    });

});
