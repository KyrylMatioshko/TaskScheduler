$(document).ready(function () {
    $(".form").validate({
        rules: {
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 8
            },
            PasswordConfirmed: {
                required: true,
                equalTo: "#Password"
            }
        },
        messages: {
            FirstName: {
                required: "Введіть ім'я"
            },
            LastName: {
                required: "Введіть прізвище"
            },
            Email: {
                required: "Введіть Email",
                email: "Некоректний формат Email"
            },
            Password: {
                required: "Введіть пароль",
                minlength: "Довжина пароля має бути не менше 8 символів"
            },
            PasswordConfirmed: {
                required: "Підтвердіть пароль",
                equalTo: "Паролі не співпадають"
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
            } else if (element.attr("name") === "PasswordConfirmed") {
                var passwordConfirmedError = element.siblings(".passwordConfirmedErrorMessage");
                passwordConfirmedError.text('');
                error.appendTo(passwordConfirmedError);
            } else if (element.attr("name") === "FirstName") {
                var firstNameError = element.siblings(".firstNameErrorMessage");
                firstNameError.text('');
                error.appendTo(firstNameError);
            } else if (element.attr("name") === "LastName") {
                var lastNameError = element.siblings(".lastNameErrorMessage");
                lastNameError.text('');
                error.appendTo(lastNameError);
            } else {
                error.insertAfter(element);
            }
        }
    });
});