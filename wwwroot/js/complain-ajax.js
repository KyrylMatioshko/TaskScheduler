$(document).ready(function () {

    function reloadPage() {
        setTimeout(function () {
            location.reload();
        }, 2000);
    }

    $(document).on('submit', '.add-complaint-form', function (event) {
        event.preventDefault();
        $(".overlay").show();
        
        $.ajax({
            url: $(this).attr("action"),
            type: 'POST',
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.success === true) {
                    $(".modal").html(`Дякуюмо за вашу реакцію`).show();
                    reloadPage();
                }
                else {
                    $(".modal").html(`Спробуйте пізніше`).show();    
                    reloadPage();

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".modal").html(handleErrorResponse(xhr)).show();
            }
        });
    });
});