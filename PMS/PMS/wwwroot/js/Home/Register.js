$(document).ready(function () {

    $("#divregistSuccess").hide();
    $('#lbluserIDError').hide()


    $('#txtconfirmpassword').change(function () {
        if ($('#txtPassword').val() != $('#txtconfirmpassword').val()) {
            $('#lblconpasserorr').text("Password didn't match")
        }
        else {
            $('#lblconpasserorr').text('')
        }
    });

    $('#txtEmail').change(function () {
        var emailregx = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (!emailregx.test($('#txtEmail').val())) {
            $('#lblemailerorr').text('Not a valid e-mail address');
        }
        else {
            $('#lblemailerorr').text('')
        }
    });



    $("#btnRegister").click(function () {

        var emailregx = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

        if (!emailregx.test($('#txtEmail').val())) {
            $('#lblemailerorr').text('Not a valid e-mail address');
        }
        else {
            var userdtls = {};
            userdtls.LoginID = 0;
            userdtls.FirstName = $('#txtFirstName').val();
            userdtls.Lastname = $('#txtLastName').val();
            userdtls.Gender = $('#DDGender option:selected').text();
            userdtls.Phone = $('#txtMobile').val();
            userdtls.EmailID = $('#txtEmail').val();
            userdtls.DOB = $('#txtDateOfBirth').val();
            userdtls.Country = $('#txtCountry').val();
            userdtls.State = $('#DDState option:selected').text();
            userdtls.City = $('#txtCity').val();
            userdtls.Pincode = $('#txtPin').val();
            userdtls.Address = $('#txtAddress').val();
            userdtls.Password = $('#txtPassword').val();

            $.ajax({
                type: "POST",
                url: instuserdtls,
                data: userdtls,
                success: function (result) {
                    if (result == "Success") {
                        //$('#regform').reset();
                        $('#divregistration').hide();
                        $('#divregistSuccess').show();
                    }
                    else if (result == "FAILURE") {
                        alert(result);
                    }
                    else {
                        $('#lbluserIDError').show()
                        $('#lbluserIDError').text(result)
                        $("html, body").animate({ scrollTop: 0 }, "slow");
                    }

                },
                error: function (result) {
                    alert(result);
                }
            });
        }


    });
});