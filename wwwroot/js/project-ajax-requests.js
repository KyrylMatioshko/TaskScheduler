$(document).ready(function () {
    const returnButton = '<a href="/Index">На головну</a>';

    function handleErrorResponse(xhr) {
        const errorMessageForUser = `Помилка.Перезавантажте сторінку та виберіть проєкт`;
        const returnButtonHtml = `<div>${returnButton}</div>`;

        if (xhr && xhr.getResponseHeader && xhr.getResponseHeader('Content-Type')) {
            if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                $(".modal").html(`${xhr.responseText}`).show();
                addValidation();
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


    $(document).on('click', '.modal-close', function () {
        $(".overlay").hide();
        $(".modal").hide();
    });

//add-project
    $(".add-project-modal").click(function (event) {
        event.preventDefault();
        $(".overlay").show();

        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $(".modal").html(data).show();
                addValidation();
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".modal").html(handleErrorResponse(xhr)).show();
            }
        });
    });

    $(document).on('submit', '.add-project-form', function (event) {
        event.preventDefault();
        var csrfToken = $(this).find('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: $(this).attr("action"),
            type: 'POST',
            data: $(this).serialize(),
            dataType: 'json',
            headers: {
                "RequestVerificationToken": csrfToken
            },
            success: function (data) {
                if (data.success === true) {
                    $(".overlay").hide();
                    $(".modal").hide();
                    window.location.href = `/tasks/${data.newProjectId}`;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".modal").html(handleErrorResponse(xhr)).show();
            }
        });
    });
//add-project


 //delete-project
    $(".delete-project-modal").click(function (event) {
        event.preventDefault();
        $(".overlay").show();
        const projectId = $(this).attr('href').split('/').pop();
        const deleteProjectUrl = `/delete-project/${projectId}`;

        $.ajax({
            url: deleteProjectUrl,
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $(".modal").html(data).show();
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".modal").html(handleErrorResponse(xhr)).show();
            }
        });
    });

    $(document).on('submit', '.delete-project-form', function (event) {
        event.preventDefault();

        var csrfToken = $(this).find('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: "/delete-project/projectId/isConfirmed",
            type: 'DELETE',
            data: $(this).serialize(),
            dataType: 'json',
            headers: {
                "RequestVerificationToken": csrfToken
            },
            success: function (data) {
                if (data.success === true) {
                    $(".overlay").hide();
                    $(".modal").hide();
                    window.location.href = "/Index";
                } else {
                    window.location.href = `/tasks/${data.projectId}`;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".modal").html(handleErrorResponse(xhr)).show();
            }
        });
    });
//delete-project


 //update-project
        $(".update-project-modal").click(function (event) {
            event.preventDefault();

            $(".overlay").show();

            $.ajax({
                url: $(this).attr("href"),
                type: 'GET',
                dataType: 'html',
                success: function (data) {
                    $(".modal").html(data).show();
                    addValidation();

                },
                error: function (xhr, textStatus, errorThrown) {
                    $(".modal").html(handleErrorResponse(xhr)).show();
                }
            });
        });

        $(document).on('submit', '.update-project-form', function (event) {
            event.preventDefault();
            var csrfToken = $(this).find('input[name="__RequestVerificationToken"]').val();

            $.ajax({
                url: $(this).attr("action"),
                type: 'PUT',
                data: $(this).serialize(),
                dataType: 'json',
                headers: {
                    "RequestVerificationToken": csrfToken 
                },
                success: function (data) {
                    if (data.success === true) {
                        $(".overlay").hide();
                        $(".modal").hide();

                        location.reload();
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $(".modal").html(handleErrorResponse(xhr)).show();
                }
            });
        });
//update-project

    function addValidation() {
        $(".add-project-form, .update-project-form").validate({
            rules: {
                Name: {
                    required: true,
                    maxlength: 50
                }
            },
            messages: {
                Name: {
                    required: "Введіть назву проєкту",
                    maxlength: "Довжина назви проєкту має бути не більше 50 символів"
                }
            },
            errorPlacement: function (error, element) {
                if (element.attr("name") === "Name") {
                    const nameError = element.siblings(".text-danger");
                    nameError.text('');
                    error.appendTo(nameError);
                } else {
                    error.insertAfter(element);
                }
            }
        });
    }

});
