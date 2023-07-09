$(document).ready(function () {

    var table = $('#tblselectFI').DataTable();

    $('#tblselectFI tbody').on('click', '#chkstock', function () {
        if (this.checked == true) {
            var data = table.row(this.closest('tr')).data();
            $('#lblplanname').text(data[2]);
            $('#lblFIid').text(data[1]);
            $('#lblDescription').text(data[3]);
            $('#lblunitvalue').text(data[5]);
        }
    });

    var table1 = $('#tbluserFI').DataTable();
    $('#tbluserFI tbody').on('click', '#update', function () {

        var data = table1.row(this.closest('tr')).data();

        $('#lblutransid').text(data[1]);
        $('#lbluplanname').text(data[3]);
        $('#lblufid').text(data[2]);
        $('#txtuqty').val(data[4]);
          

            $('#updateStockModelQty').modal('show')
    });

    $('#tbluserFI tbody').on('click', '#delete', function () {

        var data = table1.row(this.closest('tr')).data();

        var transid=data[1];
        var FIID=data[2];

        if (confirm("Do you want to permenently delete this Plan?")) {
            var data = {};
            data.FITransactionNo = transid;
            data.FIID = FIID;

            $.ajax({
                type: "POST",
                url: UrlDelUserFIID,
                data: data,
                success: function (result) {
                    if (result == "Success") {
                        alert("Plan Deleted Successfully!");
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
        var fv = $('#lblunitvalue').text();
        var total = qty * fv;
        $('#lbltotal').text(total);
    });

    $('#txtuqty').change(function () {
        var qty = $('#txtuqty').val();
        var fv = $('#lbluunitvalue').text();
        var total = qty * fv;
        $('#lblutotal').text(total);
    });

    $("#btnAdd").click(function () {
        var FIData = {};
        FIData.FITransactionNo = "0";
        FIData.FIID = $('#lblFIid').text();
        FIData.FIName = $('#lblplanname').text();
        FIData.PurchaseQty = $('#txtqty').val();
       
        $.ajax({
            type: "POST",
            url: UrlAddFI,
            data: FIData,
            success: function (result) {

                if (result == "Success") {
                    alert("Plan Added Successfully!");
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
        var FIDta = {};
        FIDta.FITransactionNo = $('#lblutransid').text(); 
        FIDta.FIID = $('#lblufid').text();
        FIDta.PurchaseQty = $('#txtuqty').val();

        $.ajax({
            type: "POST",
            url: UrlUpdateFI,
            data: FIDta,
            success: function (result) {

                if (result == "Success") {
                    alert("Updated Successfully!");
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


