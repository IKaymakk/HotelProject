
$(document).ready(function () {
    // FilePond ile dosya yüklemeyi başlat
    const inputElement = document.querySelector('#imageFiles');
    const pond = FilePond.create(inputElement, {
        allowMultiple: true,  // Birden fazla dosya seçmeye izin ver
        imagePreviewHeight: 100, // Önizleme resminin yüksekliği
        imageCropAspectRatio: '1:1', // Resmin oranını 1:1 yaparak kare göster
        imageResizeTargetWidth: 100, // Resmin önizleme genişliği
        imageResizeTargetHeight: 100, // Resmin önizleme yüksekliği
    });

    // Dosya seçildikçe önizlemeleri gösterir
    inputElement.addEventListener('change', function (event) {
        var files = event.target.files;
        var previewContainer = $("#previewContainer");
        previewContainer.empty(); // Önizleme alanını temizle

        // Dosya seçildikçe, her birini FilePond'a yükle
        for (var i = 0; i < files.length; i++) {
            // FilePond ile resim önizlemesi
            pond.addFile(files[i]).then(function (file) {
                // FilePond kendiliğinden önizlemeyi oluşturacak
            });
        }
    });
});



function insertRoom() {
    var roomData = {
        Name: $('#roomName').val(),
        Price: parseInt($('#roomPrice').val(), 10),
        CategoryId: parseInt($('#category').val(), 10),
    };

    $.ajax({
        type: 'POST',
        url: '/Admin/InsertRoom',
        data: JSON.stringify(roomData),
        contentType: 'application/json',
        success: function (response) {
            if (response.success) {
                alert('Oda başarıyla eklendi!');
            } else {
                alert('Oda eklenirken bir hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}


function updateRoom() {
    var roomData = {
        Id: 1,
        Name: "$('#roomName').val()",
        Price: 14
    };

    $.ajax({
        type: 'POST',
        url: '/Admin/UpdateRoom',
        data: JSON.stringify(roomData),
        contentType: 'application/json',
        success: function (response) {
            if (response.success) {
                alert('Oda başarıyla güncellendi!');

            } else {
                alert('Oda güncellenirken bir hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}


function insertOrUpdateFeatures() {
    var featureData = {
        RoomId: $('#roomId').val(),
        FeatureName: $('#featureName').val(),
        FeatureId: $('#featureId').val(),
    };

    $.ajax({
        type: 'POST',
        url: '/Admin/InsertOrUpdateFeatures',
        data: JSON.stringify(featureData),
        contentType: 'application/json',
        success: function (response) {
            if (response.success) {
                alert('Özellik başarıyla eklendi veya güncellendi!');
            } else {
                alert('Özellik eklenirken veya güncellenirken bir hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}


function deleteFeatures() {
    var featureId = $('#featureId').val()
    $.ajax({
        type: 'POST',
        url: '/Admin/DeleteFeatures',
        data: JSON.stringify({ FeatureId: featureId }),
        contentType: 'application/json',
        success: function (response) {
            if (response.success) {
                alert('Özellik başarıyla silindi!');
            } else {
                alert('Özellik silinirken bir hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}


function insertRoomImages() {
    var roomId = $("#roomNumber").val()
    var formData = new FormData();
    var files = $("#imageFiles")[0].files;

    if (files.length === 0) {
        alert("Lütfen en az bir dosya seçin!");
        return;
    }

    for (var i = 0; i < files.length; i++) {
        //if (files[i].size > 1048576) { // 1MB = 1048576 byte
        //    alert("Dosya boyutu 1MB'den büyük olamaz: " + files[i].name);
        //    return;
        //}
        formData.append("imageFiles", files[i]);
    }

    formData.append("roomId", roomId);

    $.ajax({
        type: 'POST',
        url: '/Admin/InsertRoomImages',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.success) {
                alert('Görseller başarıyla yüklendi!');
            } else {
                alert('Hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}



function deleteRoomImage(imageId) {
    $.ajax({
        type: 'POST',
        url: '/Admin/DeleteRoomImages',
        data: { imageId: imageId },
        success: function (response) {
            if (response.success) {
                alert('Görsel başarıyla silindi!');
            } else {
                alert('Hata: ' + response.message);
            }
        },
        error: function (xhr, status, error) {
            alert('Bir hata oluştu: ' + error);
        }
    });
}



