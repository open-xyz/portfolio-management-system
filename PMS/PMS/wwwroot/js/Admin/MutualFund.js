$(document).ready(function () {

    var table = $('#tblMF').DataTable();

    $("#btnsave").click(function () {

        var mfdtls = {};
        mfdtls.MFName = $('#txtMFName').val();
        mfdtls.FundHouse = $('#txtFundHouse').val();
        mfdtls.FundType = $('#txtFundType').val();
        mfdtls.FaceValue = $('#txtFaceValue').val();
        

        $.ajax({
            type: "POST",
            url: UrlsaveMFDtls,
            data: mfdtls,
            success: function (result) {
                if (result == "Success") {
                    alert("Mutual Fund Added Successfully!");
                }
                location.href = UrlMF;
            },
            error: function (result) {
                alert(result);
            }
        });
    });
});
function getMFdtls(id) {
    var data = {};
    data.MFID = id;

    $.ajax({
        type: "POST",
        url: UrlgetMFByID,
        data: data,
        success: function (result) {
            //var gender = result.gender;
            //if (gender = "M") {
            //    gender="Male"
            //}
            $('#lblMFid').text(result.mfid);
            $('#txtuMFName').val(result.mfName);
            $('#txtuFundHouse').val(result.fundHouse);
            $('#txtuFundType').val(result.fundType);
            $('#txtuFaceValue').val(result.faceValue);

            $('#MFViewModel').modal('show');

        },
        error: function (result) {
            alert(result);
        }
    });
}
function deleteMF(id) {

    if (confirm("Do you want to permenently delete this user?")) {
        var data = {};
        data.MFID = id;

        $.ajax({
            type: "POST",
            url: UrlDelMFID,
            data: data,
            success: function (result) {
                if (result == "Success") {
                    alert("Mutual Fund Deleted Successfully!");
                    location.href = UrlMF;
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