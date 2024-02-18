$(document).ready(function () {

    $(document).on('click', '.modal-close', function () {
        $(".overlay").hide();
        $(".modal").hide();
    });

    const returnButton = '<a href="/Index">На головну</a>';

    function handleErrorResponse(xhr) {
        const errorMessageForUser = `Помилка.Перезавантажте сторінку`;
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


    //add-task,update-task
    $(".add-task-modal, .update-task-modal").click(function (event) {
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

    $(document).on('submit', '.add-task-form, .update-task-form', function (event) {
        event.preventDefault();

        const isUpdateForm = $(this).hasClass('update-task-form');
        const requestType = isUpdateForm ? 'PUT' : 'POST';
        var csrfToken = $(this).find('input[name="__RequestVerificationToken"]').val();


        $.ajax({
            url: $(this).attr("action"),
            type: requestType,
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

    //delete-task
    $(".delete-task-modal").click(function (event) {
        event.preventDefault();

        $(".overlay").show();

        $.ajax({
            url: $(this).attr("href"),
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

    $(document).on('submit', '.delete-task-form', function (event) {
        event.preventDefault();
        var csrfToken = $(this).find('input[name="__RequestVerificationToken"]').val();


        $.ajax({
            url: "/delete-task",
            type: 'DELETE',
            data: $(this).serialize(),
            dataType: 'json',
            headers: {
                "RequestVerificationToken": csrfToken
            },
            success: function (data) {
                $(".overlay").hide();
                $(".modal").hide();

                location.reload();
            },
            error: function (xhr, textStatus, errorThrown) {
                $(".modal").html(handleErrorResponse(xhr)).show();
            }
        });
    });
});

function addValidation() {

    jQuery.validator.addMethod(`isTimeInFuture`, function (value) {
        var dateEnd = $("#DateEnd").val();
        var timeEnd = value;

        if (dateEnd === null || timeEnd === null) {
            return false;
        }

        var fullDateEnd = new Date(dateEnd + " " + timeEnd);

        if (fullDateEnd.getTime() < new Date().getTime()) {
            return false;
        }

        return true;
    }, "Оберіть час та дату який буде більшим за поточну");

    jQuery.validator.addMethod("isDateInFutureOrNow", function (value) {
        var dateEnd = value;

        if (!dateEnd) {
            return false;
        }

        var currentDate = new Date();
        currentDate.setHours(0, 0, 0, 0);

        var inputDate = new Date(dateEnd);
        inputDate.setHours(0, 0, 0, 0);

        if (inputDate < currentDate) {
            return false;
        }

        return true;
    }, "Оберіть дату яка буде більша або рівна поточній даті");


    $(".add-task-form,.update-task-form").validate({
        rules: {
            Name: {
                required: true,
                maxlength: 50
            },
            Description: {
                maxlength: 1000,
                required: true
            },
            Priority: {
                required: true
            },
            DateEnd: {
                required: true,
                isDateInFutureOrNow: true
            },
            TimeEnd: {
                required: true,
                isTimeInFuture: true
            }
        },
        messages: {
            Name: {
                required: "Введіть назву задачі",
                maxlength: "Довжина назви задачі має бути не більше 50 символів"
            },
            Description: {
                maxlength: "Довжина опису має бути не більше 1000 символів",
                required: "Введіть опис задачі",
            },
            Priority: {
                required: "Оберіть пріоритет"
            },
            DateEnd: {
                required: "Оберіть дату"
            },
            TimeEnd: {
                required: "Оберіть час"
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") === "Name") {
                const nameError = element.siblings(".text-danger-name");
                nameError.text('');
                error.appendTo(nameError);
            } else if (element.attr("name") === "Description") {
                const descriptionError = element.siblings(".text-danger-description"); 
                descriptionError.text('');
                error.appendTo(descriptionError);
            } else if (element.attr("name") === "Priority") {
                const priorityError = element.siblings(".text-danger-priority");
                priorityError.text('');
                error.appendTo(priorityError);
            } else if (element.attr("name") === "DateCreate") {
                const dateCreateError = element.siblings(".text-danger-dateCreate"); 
                dateCreateError.text('');
                error.appendTo(dateCreateError);
            } else if (element.attr("name") === "DateEnd") {
                const dateEndError = element.siblings(".text-danger-dateEnd");
                dateEndError.text('');
                error.appendTo(dateEndError);
            } else if (element.attr("name") === "TimeEnd") {
                const timeEndError = element.siblings(".text-danger-timeEnd");
                timeEndError.text('');
                error.appendTo(timeEndError);
            } else {
                error.insertAfter(element);
            }
        }
    });
}