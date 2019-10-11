$(document).ready(function () {
    function hideLoader() {
        $('.se-pre-con').hide();
    }

    $(window).ready(hideLoader);

    setTimeout(hideLoader, 20 * 1000);
    var table = $("#table").DataTable();
    setTimeout(function () {
                $(".alert-success").fadeOut(100, null);
            },
        3000);

    });

