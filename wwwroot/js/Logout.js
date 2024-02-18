$(document).ready(function () {

    $(document).on('click', '.modal-close', function () {
        $(".overlay").hide();
        $(".logout-modal-window").hide();
    });
   
    $(".logout-modal, .link.depth-0",).click(function (event) {
        event.preventDefault();
        $(".overlay").show();

        $.ajax({
            url: '/Account/LogoutGet',
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $(".logout-modal-window").html(data).show();
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".logout-modal-window").show();
            }
        });
    });

    $(document).on('submit', '.logout-form', function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr("action"),
            type: 'POST',
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.success === true) {
                    $(".overlay").hide();
                    $(".logout-modal-window").hide();
                    window.location.href = `/Home/Index`;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".logout-modal-window").show();

            }
        });
    });
});