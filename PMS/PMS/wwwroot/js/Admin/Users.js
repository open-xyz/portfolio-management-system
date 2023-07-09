$(document).ready(function () {

    $("#btnSave").click(function () {

        var inscattdata = {};
        inscattdata.categoryid = $('#txtcatID').val();
        inscattdata.sectonid = $('#DDSectionID option:selected').val();
        inscattdata.secton = $('#DDSectionID option:selected').text();
        inscattdata.categoryName = $('#txtCategoryName').val();
        inscattdata.Photo = $('#FileUpload1').val();
        inscattdata.DisplayOrder = $('#txtDisplayOrder').val();
        inscattdata.Desc = app.editorData;

        $.ajax({
            type: "POST",
            url: instcaturl,
            data: inscattdata,
            success: function (result) {
                alert(result);
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