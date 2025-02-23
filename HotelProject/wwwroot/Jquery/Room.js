
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


$(document).ajaxStart(function () {
    $("#loadingDiv").show();
}).ajaxStop(function () {
    $("#loadingDiv").hide();
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
                GetRoomDetail()
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
                GetRoomDetail("General")
            } else {
                alert('Oda güncellenirken bir hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}


function insertFeatures() {
    var featureData = {
        RoomId: $("#roomNumber").val(),
        Features: $('#featureName').val(),
    };

    $.ajax({
        type: 'POST',
        url: '/Admin/InsertFeatures',
        data: JSON.stringify(featureData),
        contentType: 'application/json',
        success: function (response) {
            if (response.success == true) {
                alert('Özellik başarıyla eklendi veya güncellendi!');
                GetRoomDetail("General")

            } else {
                alert('Özellik eklenirken veya güncellenirken bir hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}
function updateFeatures() {
    var selectedFeatures = [];

    $(".form-check-input:checked").each(function () {
        var featureId = $(this).attr("id");
        var featureValue = $(this).closest("li").find("input[type='text']").val();  

        selectedFeatures.push({ FeatureId: featureId, Features: featureValue });
    });
    if (selectedFeatures.length === 0) {
        Swal.fire({
            icon: 'error',
            title: 'Hata!',
            text: 'Lütfen güncellemek için en az bir özellik seçin.',
            confirmButtonText: 'Tamam'
        });
        return;
    }
    $.ajax({
        type: 'POST',
        url: '/Admin/UpdateFeatures',
        data: { Features: selectedFeatures }, 
        success: function (response) {
            if (response.success == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Özellikler başarıyla güncellendi!',
                    confirmButtonText: 'Tamam'
                });
                GetRoomDetail("Feature");
                GetRoomDetail("General");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Özellikler güncellenirken bir hata oluştu.',
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });

}


function deleteFeatures() {
    var selectedFeatures = []; 

    $(".form-check-input:checked").each(function () {
        selectedFeatures.push($(this).attr('id'));  
    });

    if (selectedFeatures.length === 0) {
        Swal.fire({
            icon: 'error',
            title: 'Hata!',
            text: 'Lütfen silmek için en az bir özellik seçin.',
            confirmButtonText: 'Tamam'
        });
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/Admin/DeleteFeatures',
        data: { FeatureIds: selectedFeatures },  
        traditional: true,
        success: function (response) {
            if (response.success == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Özellik başarıyla silindi!',
                    confirmButtonText: 'Tamam'
                });
                GetRoomDetail("Feature");  
                GetRoomDetail("General");

            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Özellik silinirken bir hata oluştu ' + response.error,
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}

function deleteImage(id) {
    $.ajax({
        type: 'POST',
        url: '/Admin/DeleteImage',
        data: { ImageId: id },
        success: function (response) {
            if (response.success == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Resim başarıyla silindi!',
                    confirmButtonText: 'Tamam'
                });
                GetRoomDetail("General")
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Resim silinirken bir hata oluştu: ' + response.error,
                    confirmButtonText: 'Tamam'
                });
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
                GetRoomDetail("General")
            } else {
                alert('Hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}




function GetRoomDetail(ForWhat) {
    var roomId = $("#roomNumber").val()
    $.ajax({
        type: 'GET',
        url: '/Admin/GetRoomDetail',
        data: { roomId: roomId },
        success: function (response) {
            if (ForWhat == "General") {
                $("#roomPrice").val(response.data.price)
                var imageSubContainer = $("#imageSubContainer");
                imageSubContainer.html("");
                response.data.roomImages.forEach(function (image) {
                    var imgElement = $('<div class="image-wrapper"></div>')
                        .append('<img src="data:image/jpeg;base64,' + image.fileData + '" class="img-fluid" id="roomImage">')
                        .append('<button class="btn btn-danger delete-btn" onclick="deleteImage(' + image.imageId + ')">Sil</button>');

                    $('#imageSubContainer').css({
                        'display': 'flex',
                        'flex-wrap': 'wrap',
                        'gap': '10px'
                    });

                    imgElement.css({
                        'flex': '1 1 18%',
                        'max-width': '18%',
                        'box-sizing': 'border-box'
                    });

                    $('#imageSubContainer').append(imgElement);
                });

                var featureWrapper = $("#featureWrapper");
                featureWrapper.html("");

                response.data.roomFeatures.forEach(function (feature) {
                    var listItem = $('<li class="list-group-item"></li>').text(feature.features);
                    featureWrapper.append(listItem);
                });

            }
            else if (ForWhat == "Feature")
            {
                var featureContainer = $("#featureContainer");
                featureContainer.html("")
                response.data.roomFeatures.forEach(function (feature) {
                    var listItem = $('<li class="list-group-item d-flex align-items-center"></li>'); 

                    var checkbox = $('<input type="checkbox" class="form-check-input me-2" id="'+feature.featureId+'">');
                    var inputField = $('<input type="text" class="form-control ms-2" value="' + feature.features + '">');
                    listItem.append(checkbox).append(inputField);
                    featureContainer.append(listItem);
                });
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}




function SelectAll() {
    var checkboxes = $(".form-check-input:not(#selectAll)");

    var allChecked = checkboxes.toArray().every(function (checkbox) {
        return $(checkbox).prop('checked');
    });

    checkboxes.each(function () {
        $(this).prop('checked', !allChecked);
    });
}
function DeSelectAll() {
    var checkboxes = $(".form-check-input:not(#selectAll)");

    checkboxes.each(function () {
        $(this).prop('checked', false);
    });
}