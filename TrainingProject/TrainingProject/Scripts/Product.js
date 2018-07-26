
//JAVASCRIPT PAGE


    $(document).ready(function () {
        $('#form1').submit(function (e) {

            var ischeck = true;
            var Product_name = $('#Product_name').val();
            var Description = $('#Description').val();
            var Date = $('#Date').val();
            var Price = $("#Price").val();
            var IsActive = $('#IsActive').prop('checked');

            var NoOfProducts = $('#NoOfProducts').val();

            var name_regex = /^[A-Za-z]+$/
            var numbers = /^[0-9]+$/;

            $(".error").remove();

            if (Product_name.length < 1 || !Product_name.match(name_regex)) {
                $('#Product_name').after('<span class="error">Please Enter a Valid Name</span>');
                ischeck = true;
            }
            else {
                ischeck = false;
            }

            if (Description.length < 1) {
                $('#Description').after('<span class="error">This field is required</span>');
                ischeck = true;
            }
            else {
                ischeck = false;
            }

            if (Date.length < 1) {
                $('#Date').after('<span class="error">This field is required</span>');
                ischeck = true;
            }
            else {
                ischeck = false;
            }

            if (Price.length < 1 || !Price.match(numbers)) {
                $('#Price').after('<span class="error">Enter a valid Number</span>');
                ischeck = true;
            }
            else {
                ischeck = false;
            }

            if (NoOfProducts.length < 1 || !Price.match(numbers)) {
                $('#NoOfProducts').after('<span class="error">Enter a valid Number</span>');
                ischeck = true;
            }
            else {
                ischeck = false;
            }

            if (ischeck == true) {
                e.preventDefault();
            }

        });

    $('#reset_btn').click(function () {
        $('#form1').trigger("reset");
    $(".error").remove();
});
});
  