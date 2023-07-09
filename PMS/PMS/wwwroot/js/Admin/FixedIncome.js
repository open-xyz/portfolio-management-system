$(document).ready(function () {

    var table = $('#tblFI').DataTable();

    $("#btnsave").click(function () {

        var FIdtls = {};
        FIdtls.FIName = $('#txtFIName').val();
        FIdtls.FIDescription = $('#txtFIDesc').val();
        FIdtls.RateOfInterest = $('#txtFIRateofInt').val();
        FIdtls.Tenure = $('#txtFITenure').val();
        FIdtls.PurchaseUnitValue = $('#txtFIPU').val();

        $.ajax({
            type: "POST",
            url: UrlsaveFIDtls,
            data: FIdtls,
            success: function (result) {
                if (result == "Success") {
                    alert("Fixed Income Plan Added Successfully!");
                }
                location.href = UrlFI;
            },
            error: function (result) {
                alert(result);
            }
        });
    });
});
function getFIdtls(id) {
    var data = {};
    data.FIID = id;

    $.ajax({
        type: "POST",
        url: UrlgetFIByID,
        data: data,
        success: function (result) {
            //var gender = result.gender;
            //if (gender = "M") {
            //    gender="Male"
            //}
            $('#lbluFIid').text(result.fiid);
            $('#txtuFIName').val(result.fiName);
            $('#txtuFIDesc').val(result.fiDescription);
            $('#txtuFIRateofInt').val(result.rateOfInterest);
            $('#txtuFITenure').val(result.tenure);
            $('#txtuFIPU').val(result.purchaseUnitValue);

            $('#FIViewModel').modal('show');

        },
        error: function (result) {
            alert(result);
        }
    });
}
function deleteFI(id) {

    if (confirm("Do you want to permenently delete this user?")) {
        var data = {};
        data.FIID = id;

        $.ajax({
            type: "POST",
            url: UrlDelFIID,
            data: data,
            success: function (result) {
                if (result == "Success") {
                    alert("FIxed Income Plan Deleted Successfully!");
                    location.href = UrlFI;
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