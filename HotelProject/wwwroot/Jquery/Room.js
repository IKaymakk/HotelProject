function insertRoom() {
    var roomData = {
        Name: $('#roomName').val(),
        Price: $('#roomPrice').val(),
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
        Id: $('#roomId').val(),
        Name: $('#roomName').val(),
        Price: $('#roomPrice').val()
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


function insertRoomImages(roomId) {
    var formData = new FormData();
    var files = $("#imageFiles").files;

    for (var i = 0; i < files.length; i++) {
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
                alert('Görseller başarıyla eklendi!');
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
