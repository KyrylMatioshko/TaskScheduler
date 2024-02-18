$(document).ready(function () {
    $(".formLogin").validate({
        rules: {
            "Email": {
                required: true,
                email: true
            },
            "Password": {
                required: true,
                minlength: 8
            }
        },
        messages: {
            "Email": {
                required: "Введіть Email",
                email: "Некоректний формат Email"
            },
            "Password": {
                required: "Введіть пароль",
                minlength: "Довжина паролю повинна бути не менше 8 символів"
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") === "Email") {
                var emailError = element.siblings(".emailErrorMessage");
                emailError.text('');
                error.appendTo(emailError);
            } else if (element.attr("name") === "Password") {
                var passwordError = element.siblings(".passwordErrorMessage");
                passwordError.text('');
                error.appendTo(passwordError);
            } else {
                error.insertAfter(element);
            }
        }
    });
});