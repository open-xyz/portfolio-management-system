$(document).ready(function () {

    $("#btnsave").click(function () {

        var stockdtls = {};
        stockdtls.StockName = $('#txtStockName').val();
        stockdtls.FaceValue = $('#txtStockPrice').val();
        

        $.ajax({
            type: "POST",
            url: UrlsaveDtlsByID,
            data: stockdtls,
            success: function (result) {
                if (result == "Success") {
                    alert("Stock Added Successfully!");
                }
                location.href = UrlStock;
            },
            error: function (result) {
                alert(result);
            }
        });
    });
});
function getuserdtls(id) {
    var data = {};
    data.LoginID = id;

    $.ajax({
        type: "POST",
        url: UrlgetUserDtlsByID,
        data: data,
        success: function (result) {
            //var gender = result.gender;
            //if (gender = "M") {
            //    gender="Male"
            //}

            $('#txtFirstName').val(result.firstName);
            $('#txtLastName').val(result.lastname);
            $('#DDGender').val(result.gender.trim()).change();
            $('#txtMobile').val(result.phone);
            $('#txtEmail').val(result.emailID);
            $('#txtDateOfBirth').val(result.dob);
            $('#txtCountry').val(result.country);
            $('#DDState option:selected').val(result.state);
            $('#txtCity').val(result.city);
            $('#txtPin').val(result.pincode);
            $('#txtAddress').val(result.address);
            $('#txtPassword').val(result.password);
            $('#DDValidUser').val(result.validUserlndicator).change();
            $('#DDISAdmin').val(result.adminlndicator).change();
            $('#lblloginid').val(result.loginID);

            $('#UserModel').modal('show');

        },
        error: function (result) {
            alert(result);
        }
    });
}
function deleteuser(id) {

    if (confirm("Do you want to permenently delete this user?")) {
        var data = {};
        data.LoginID = id;

        $.ajax({
            type: "POST",
            url: UrlDelUserDtlsByID,
            data: data,
            success: function (result) {
                if (result == "Success") {
                    alert("User Deleted Successfully!");
                    location.href = UrlUser;
                }
                else if(result == "Failure") {
                    alert("Oops! Somthing went wrong while Deleting User");
                }
                
            },
            error: function (result) {
                alert(result);
            }
        });
    }
}