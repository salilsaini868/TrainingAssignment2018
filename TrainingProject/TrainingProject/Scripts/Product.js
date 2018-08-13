//JAVASCRIPT PAGE

$(document).ready(function () {

    $('#Product_Form').submit(function (e) {

        var ischeck = true;
        var Product_name = $('#Product_name').val();
        var Categoryid = $('#Category_ID').val();
        var Description = $('#Description').val();
        var Date = $('#Date').val();
        var Price = $("#Price").val();
        var IsActive = $('#IsActive').prop('checked');
        var quantity = $('#NoOfProducts').val();

        var name_regex = /^[A-Za-z]+$/
        var numbers = /^[0-9]+$/;

        if (Product_name.length < 1 || !Product_name.match(name_regex)) {
            $('#p_name').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#p_name').hide();
            ischeck = false;
        }

        if (Categoryid.length < 1 || !Categoryid.match(numbers)) {
            $('#cat').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#cat').hide();
            ischeck = false;
        }

        if (Description.length < 1) {
            $('#p_description').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#p_description').hide();
            ischeck = false;
        }

        if (Date.length < 1) {
            $('#p_date').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#p_date').hide();
            ischeck = false;
        }

        if (Price.length < 1 || !Price.match(numbers)) {
            $('#p_price').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#p_price').hide();
            ischeck = false;
        }


        if (quantity.length < 1 || !quantity.match(numbers)) {
            $('#p_no').show();
            ischeck = true;
            e.preventDefault();
        }
        else {
            $('#p_no').hide();
            ischeck = false;
        }


        if (ischeck == true) {
            e.preventDefault();
        }

        $('#reset_btn').click(function () {
            $('#Product_Form').trigger('reset');
            $(".error").hide();
        });

    });
});


