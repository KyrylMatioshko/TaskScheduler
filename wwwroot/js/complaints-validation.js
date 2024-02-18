 $(document).ready(function () {
        $(".add-complaint-form").validate({
            rules: {
                Name: {
                    required: true,
                    maxlength: 50
                },
                Description: {
                    required: true,
                    maxlength: 300
                }
            },
            messages: {
                Name: {
                    required: "Введіть назву звернення",
                    maxlength: "Довжина назви звернення має бути не більше 50 символів"
                },
                Description: {
                    required: "Введіть опис звернення",
                    maxlength: "Довжина звернення має бути не більше 300 символів"
                }
            },
            errorPlacement: function (error, element) {
                if (element.attr("name") === "Name") {
                    var nameError = element.siblings(".nameError");
                    nameError.text('');
                    error.appendTo(nameError);
                } else if (element.attr("name") === "Description") {
                    var descriptionError = element.siblings(".descError");
                    descriptionError.text('');
                    error.appendTo(descriptionError);
                } else {
                    error.insertAfter(element);
                }
            }
        });
 });