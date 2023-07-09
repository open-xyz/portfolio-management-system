$(document).ready(function () {

    var table = $('#tblselectMF').DataTable();

    $('#tblselectMF tbody').on('click', '#chkstock', function () {
        if (this.checked == true) {
            var data = table.row(this.closest('tr')).data();
            $('#lblstockname').text(data[2]);
            $('#lblstockid').text(data[1]);
            $('#lblfvalue').text(data[5]);
        }
    });

    var table1 = $('#tbluserMF').DataTable();

    $('#tbluserMF tbody').on('click', '#update', function () {

        var data = table1.row(this.closest('tr')).data();

        $('#lblutransid').text(data[1]);
        $('#lblustockname').text(data[3]);
        $('#lblustockid').text(data[2]);
        $('#lblufvalue').text(data[4]);
        $('#txtuPurqty').val(data[5]);
        $('#txtuqty').val(data[6]);

            var qty = $('#txtuqty').val();
            var fv = $('#lblufvalue').text();
            var total = qty * fv;
            $('#lblutotal').text(total);

        $('#updateMFModelQty').modal('show')
    });

    $('#tbluserMF tbody').on('click', '#delete', function () {

        var data = table1.row(this.closest('tr')).data();

        var transid=data[1];
        var MFID=data[2];

        if (confirm("Do you want to permenently delete this user?")) {
            var MFdata = {};
            MFdata.MFTransactionNo = transid;
            MFdata.MFID = MFID;
          
            $.ajax({
                type: "POST",
                url: UrlDelUserMF,
                data: MFdata,
                success: function (result) {
                    if (result == "Success") {
                        alert("Mutual Fund Deleted Successfully!");
                        location.href = Urlredirect;
                    }
                    else if (result == "Failure") {
                        alert("Oops! Somthing went wrong while Deleting Stock");
                    }

                },
                error: function (result) {
                    alert(result);
                }
            });
        }
    });

    $('#txtqty').change(function () {
        var qty = $('#txtqty').val();
        var fv = $('#lblfvalue').text();
        var total = qty * fv;
        $('#lbltotal').text(total);
    });

    $("#btnAdd").click(function () {
        var MFdata = {};
        MFdata.MFTransactionNo = "0";
        MFdata.MFID = $('#lblstockid').text();
        MFdata.MFName = $('#lblstockname').text();
        MFdata.Quantity = $('#txtqty').val();
        MFdata.PurchaseQty = $('#txtPurqty').val();
        MFdata.FolioNo = "0";
        
       
        $.ajax({
            type: "POST",
            url: UrlAddMF,
            data: MFdata,
            success: function (result) {

                if (result == "Success") {
                    alert("Mutual Fund Added Successfully!");
                    location.href = Urlredirect;
                }
                else {
                    alert("Oops! Somthing went wrong while adding stock");
                }
            },
            error: function (result) {
                alert(result);
            }
        });
    });

    $("#btnUpdate").click(function () {
        var MFdata = {};
        MFdata.MFTransactionNo = $('#lblutransid').text();
        MFdata.MFID = $('#lblustockid').text();
        MFdata.Quantity = $('#txtuqty').val();
        MFdata.PurchaseQty = $('#txtuPurqty').val();

        $.ajax({
            type: "POST",
            url: UrlUpdateMF,
            data: MFdata,
            success: function (result) {

                if (result == "Success") {
                    alert("Mutual Fund Updated Successfully!");
                    location.href = Urlredirect;
                }
                else {
                    alert("Oops! Somthing went wrong while adding stock");
                }
            },
            error: function (result) {
                alert(result);
            }
        });
    });

    
});


