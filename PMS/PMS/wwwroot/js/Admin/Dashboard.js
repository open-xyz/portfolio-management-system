$(document).ready(function () {

    $.ajax({
        type: "POST",
        url: Urlgetlistcount,
        success: function (result) {
            var data = result.split("~");
            $('#lblUsers').text(data[0]);
            $('#lblStocks').text(data[1]);
            $('#lblMF').text(data[2]);
            $('#lblFI').text(data[3]);
        },
        error: function (result) {
            alert(result);
        }
    });

});
