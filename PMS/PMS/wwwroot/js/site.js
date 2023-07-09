$(document).ready(function () {

    $('#diverror').hide();

    $.ajax({
        type: "POST",
        url: UrlgetUserDtls,
        success: function (result) {
            $('#DDGender').val(result.gender.trim()).change();
            $('#DDState option:selected').val(result.state);
        },
        error: function (result) {
            alert(result);
        }
    });

    $('#btnupdateUD').click(function () {

        userdtls = {};

        userdtls.FirstName = $('#txtFirstName').val();
        userdtls.Lastname = $('#txtLastName').val();
        userdtls.Gender = $('#DDGender').val();
        userdtls.DOB = $('#txtDateOfBirth').val();
        userdtls.EmailID = $('#txtEmail').val();
        userdtls.Address = $('#txtAddress').val();
        userdtls.State = $('#DDState').val();
        userdtls.City = $('#txtCity').val();
        userdtls.Country = $('#txtCountry').val();
        userdtls.Pincode = $('#txtPin').val();
        userdtls.Phone = $('#txtMobile').val();


        $.ajax({
            type: "POST",
            url: UrlupdateUserDtls,
            data: userdtls,
            success: function (result) {
                if (result == "Success") {
                    $('#UserModel').modal('hide');
                    alert("Profile Updated Successfully!");
                    location.href=window.location;
                }
            },
            error: function (result) {
                alert(result);
            }
        });
    });

    $('#btnupdatepass').click(function () {

        data = {};

        data.oldpass = $('#txtoldpass').val();
        data.newpass = $('#txtnewpass').val();
        data.conpass = $('#txtConPass').val();

        if (data.newpass != data.conpass) {
            $('#lblmessage').text("New Password Confirm Password Does Not Match");
            $('#diverror').show();
        }
        else {
            $.ajax({
                type: "POST",
                url: UrlupdatePass,
                data: data,
                success: function (result) {
                    if (result == "Success") {
                        $('#ChangePassModel').modal('hide');
                        alert("Password Updated Successfully!");
                        $('#txtoldpass').val();
                        $('#txtnewpass').val();
                        $('#txtConPass').val();
                    }
                    else {
                        $('#lblmessage').text(result);
                        $('#diverror').show();
                    }
                },
                error: function (result) {
                    alert(result);
                }
            });
        }
    });

});
