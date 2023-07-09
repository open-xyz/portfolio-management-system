$(document).ready(function () {

    var table = $('#tblselectstock').DataTable();

    $('#tblselectstock tbody').on('click', '#chkstock', function () {
        if (this.checked == true) {
            var data = table.row(this.closest('tr')).data();
            $('#lblstockname').text(data[2]);
            $('#lblstockid').text(data[1]);
            $('#lblfvalue').text(data[3]);
        }
    });

    var table1 = $('#tbluserstocks').DataTable();

    $('#tbluserstocks tbody').on('click', '#update', function () {

        var data = table1.row(this.closest('tr')).data();

        $('#lblutransid').text(data[1]);
        $('#lblustockname').text(data[3]);
        $('#lblustockid').text(data[2]);
        $('#lblufvalue').text(data[4]);
        $('#txtuqty').val(data[5]);

            var qty = $('#txtuqty').val();
            var fv = $('#lblufvalue').text();
            var total = qty * fv;
            $('#lblutotal').text(total);

            $('#updateStockModelQty').modal('show')
    });

    $('#tbluserstocks tbody').on('click', '#delete', function () {

        var data = table1.row(this.closest('tr')).data();

        var transid=data[1];
        var stockid=data[2];
        
        

        var qty = $('#txtuqty').val();
        var fv = $('#lblufvalue').text();
        var total = qty * fv;
        $('#lblutotal').text(total);

        if (confirm("Do you want to permenently delete this user?")) {
            var data = {};
            data.TransactionNo = transid;
            data.StockID = stockid;

            $.ajax({
                type: "POST",
                url: UrlDelUserStockID,
                data: data,
                success: function (result) {
                    if (result == "Success") {
                        alert("Stock Deleted Successfully!");
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

    $('#txtuqty').change(function () {
        var qty = $('#txtuqty').val();
        var fv = $('#lblufvalue').text();
        var total = qty * fv;
        $('#lblutotal').text(total);
    });

    $("#btnAdd").click(function () {
        var stockdata = {};
        stockdata.TransactionNo = "0";
        stockdata.StockID = $('#lblstockid').text();
        stockdata.Quantity = $('#txtqty').val();
        stockdata.PurchasePrice = $('#lblfvalue').text();
        stockdata.StockName = $('#lblstockname').text();
       
        $.ajax({
            type: "POST",
            url: UrlAddStock,
            data: stockdata,
            success: function (result) {

                if (result == "Success") {
                    alert("Stock Added Successfully!");
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
        var stockdata = {};
        stockdata.TransactionNo = $('#lblutransid').text(); 
        stockdata.StockID = $('#lblustockid').text();
        stockdata.Quantity = $('#txtuqty').val();
        stockdata.PurchasePrice = $('#lblufvalue').text();
        stockdata.StockName = $('#lblustockname').text();

        $.ajax({
            type: "POST",
            url: UrlUpdateStock,
            data: stockdata,
            success: function (result) {

                if (result == "Success") {
                    alert("Stock Updated Successfully!");
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


