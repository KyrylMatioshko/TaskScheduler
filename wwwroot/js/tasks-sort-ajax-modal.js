$(document).ready(function () {

    const returnButton = '<a href="/Index">На головну</a>';

    function handleErrorResponse(xhr) {
        const errorMessageForUser = `Помилка.Перезавантажте сторінку`;
        const returnButtonHtml = `<div>${returnButton}</div>`;

        if (xhr && xhr.getResponseHeader && xhr.getResponseHeader('Content-Type')) {
            if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                $(".modal").html(`${xhr.responseText}`).show();
            } else {
                const errorMessage = xhr.responseText || 'An error occurred during the request.';
                console.error(errorMessage);
                $(".modal").html(`${errorMessageForUser}${returnButtonHtml}`).show();
            }
        } else {
            console.error('Error: Content-Type header is missing in the response.');
            $(".modal").html(`${errorMessageForUser}${returnButtonHtml}`).show();
        }
    }

    $("#open-sort-modal").click(function (event) {
        event.preventDefault();
        var csrfToken = $(this).find('input[name="__RequestVerificationToken"]').val();

        $(".overlay").show();

        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            dataType: 'html',
            headers: {
                "RequestVerificationToken": csrfToken
            },
            success: function (data) {
                $(".modal").html(data).show();
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".modal").html(handleErrorResponse(xhr)).show();
            }
    });
        });
});


$(document).on('submit', '.sort-tasks-form', function (event) {
    event.preventDefault();

    $.ajax({
        url: $(this).attr("action"),
        type: 'PUT',
        data: $(this).serialize(),
        dataType: 'json',
        success: function (data) {
            if (data.success === true) {
                $(".overlay").hide();
                $(".modal").hide();

                window.location.href = `/tasks/${data.projectId}`;
            }
        },
        error: function (xhr, status, error) {
            if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                $(".modal").html(xhr.responseText).show();
            } else {
                $('.update-display-settings-form button[type="submit"]').removeAttr('disabled');
                console.log('An error occurred while updating the display settings.');
            }
        }
    });
});

$(document).on('click', '.modal-close', function () {
    $(".overlay").hide();
    $(".modal").hide();
});


