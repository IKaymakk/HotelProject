function Login() {
    var formData = $("#loginForm").serialize();

    $.ajax({
        url: "/Login/Login/",
        type: "POST",
        data: formData,
        success: function (response) {
            if (response.success) {
                window.location.href = response.redirectUrl;
                $("#errorMessage").removeClass("d-none").text(response.message);
            } else {
                $("#errorMessage").removeClass("d-none").text(response.message);
            }
        },
        error: function () {
            $("#errorMessage").removeClass("d-none").text("Bir hata oluştu, lütfen tekrar deneyin.");
        }
    });
}

function Register() {
    var formData = $("#registerForm").serialize();

    $.ajax({
        url: "/Login/Register/",
        type: "POST",
        data: formData,
        success: function (result) {
            if (result.success == true) {
                Swal.fire({
                    title: "Başarılı!",
                    text: result.message,
                    icon: "success",
                    confirmButtonText: "Tamam"
                }).then(() => {
                    $("#registerForm")[0].reset();
                    window.location.href = result.redirectUrl;
                });
            } else {
                var formattedMessage = result.message.replace(/\n/g, "<br>");
                $("#registerErrorMessage").removeClass("d-none").html(formattedMessage);
            }
        },
        error: function (xhr) {
            var errorMessage = xhr.responseText || "Kayıt başarısız!";
            Swal.fire({
                title: "Hata!",
                html: errorMessage.replace(/\n/g, "<br>"),
                icon: "error",
                confirmButtonText: "Tamam"
            });
        }
    });
}

function Logout() {
    $.ajax({
        url: "/Login/Logout",
        type: "POST",
        success: function (response) {
            if (response.success) {
                window.location.href = response.redirectUrl
            }
        },
        error: function () {
            alert("Çıkış işlemi sırasında bir hata oluştu.");
        }
    });
}


function ChangePassword() {
    var formData = $("#ChangePasswordForm").serialize();

    $.ajax({
        url: "/Admin/ChangePassword/",
        type: "POST",
        data: formData,
        success: function (result) {
            if (result.success == true) {
                Swal.fire({
                    title: "Başarılı!",
                    text: result.message,
                    icon: "success",
                    confirmButtonText: "Tamam"
                })
            } else {
                Swal.fire({
                    title: "Hata!",
                    text: result.message,
                    icon: "error",
                    confirmButtonText: "Tamam"
                })
            }
        },
        error: function () {
            Swal.fire({
                title: "Hata!",
                text: "error",
                icon: "error",
                confirmButtonText: "Tamam"
            })
        }
    });
}
