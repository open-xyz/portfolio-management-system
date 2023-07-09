$(document).ready(function () {
    $("#DivPreLogin").hide();
    $('#diverror').hide();
    $("#btnLogin").click(function () {

            var userdtls = {};
        userdtls.EmailID = $('#txtUserID').val();
        userdtls.Password = $('#txtPassword').val();

            $.ajax({
                type: "POST",
                url: LoginUrl,
                data: userdtls,
                success: function (result) {
                        
                    if (result == "No data Found") {
                        $('#diverror').show();
                    }
                    else if (result.result == "Success") {
                        location.href = result.url;
                    }
                    else {
                        alert(result);
                    }
                },
                error: function (result) {
                    alert(result);
                }
            });


    });
});
