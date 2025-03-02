function updateContactSettings() {
    var formData = $("#ContactInfoForm").serialize()

    $.ajax({
        type: 'POST',
        url: '/Admin/updateContactSettings',
        data: formData, 
        success: function (response) {
            if (response.success == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Başarıyla güncellendi!',
                    confirmButtonText: 'Tamam'
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Güncellenirken bir hata oluştu!',
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Güncellenirken bir hata oluştu!',
                confirmButtonText: 'Tamam'
            });        }
    });
}


function updateAboutSettings() {
    var formData = $("#AboutForm").serialize()

    $.ajax({
        type: 'POST',
        url: '/Admin/updateAboutUsSettings',
        data: formData, 
        success: function (response) {
            if (response.success == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Başarıyla güncellendi!',
                    confirmButtonText: 'Tamam'
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Güncellenirken bir hata oluştu!',
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Güncellenirken bir hata oluştu!',
                confirmButtonText: 'Tamam'
            });        }
    });
}