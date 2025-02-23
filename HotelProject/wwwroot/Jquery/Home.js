$(document).ajaxStart(function () {
    $("#loadingDiv").show();
}).ajaxStop(function () {
    $("#loadingDiv").hide();
});


function SendMail() {
    var formData = $("#SendMail").serializeArray();
    var errors = [];
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; 

    var formValues = {};
    $.each(formData, function (i, field) {
        var value = field.value.trim();
        formValues[field.name] = value;

        if (!value) {
            errors.push(field.name + " alanı boş bırakılamaz.");
        }

        if (field.name === "Email" && !emailPattern.test(value)) {
            errors.push("Geçerli bir e-posta adresi giriniz.");
        }
    });

    if (errors.length > 0) {
        Swal.fire({
            icon: "error",
            title: "Hata!",
            html: errors.join("<br>"),
            confirmButtonColor: "#d33"
        });
        return;
    }
    $.ajax({
        url: "/Home/SendMail/",
        type: "POST",
        data: formValues,
        success: function (response) {
            if (response.success == true) {
                Swal.fire({
                    title: "Başarılı!",
                    text: "Mesajınız başarıyla gönderildi.",
                    icon: "success",
                    confirmButtonText: "Tamam"
                });
            } else {
                Swal.fire({
                    title: "Hata!",
                    text: "Mesajınız gönderilemedi.",
                    icon: "error",
                    confirmButtonText: "Tamam"
                });
            }
        },
        error: function () {
            Swal.fire({
                title: "Hata!",
                text: "Mesajınız gönderilemedi.",
                icon: "error",
                confirmButtonText: "Tamam"
            });
        }
    });
}