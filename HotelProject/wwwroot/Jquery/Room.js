var roomId;

$(document).ajaxStart(function () {
    $("#loadingDiv").show();
}).ajaxStop(function () {
    $("#loadingDiv").hide();
});

function insertRoom() {
    var roomData = {
        Name: $('#roomName').val(),
        Price: parseFloat($('#roomPrice').val(), 10),
        CategoryId: parseInt($('#category').val(), 10),
        Description: $('#description').val(),
        Capacity: parseInt($('#capacity').val(), 10),
        isMainPage: $('#isMainPage').val() == 1 ? true : false
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
            roomId = response
            if (ForWhat == "General") {
                $("#roomPrice").val(response.data.price)
                const value = response.data.isMainPage;
                if (value == false) {
                    $("#isMainPage").val(0);
                }
                else {
                    $("#isMainPage").val(1);
                }
                $("#description").val(response.data.description)
                $('#capacity').val(response.data.capacity)
                var imageSubContainer = $("#imageSubContainer");
                imageSubContainer.html("");
                response.data.roomImages.forEach(function (image) {
                    var imgElement = $('<div class="image-wrapper" style="flex: 1 1 30%; max-width: 30%; box-sizing: border-box; position: relative; margin-bottom: 20px;"></div>')
                        .append('<div class="image-controls" style="background-color: #f1f1f1; padding: 10px; display: flex; justify-content: space-between; margin-bottom: 10px;">' +
                            '<button class="btn btn-danger" onclick="deleteImage(' + image.imageId + ')">Sil</button>' +
                            '<button class="btn btn-primary" onclick="setCoverImage(' + image.imageId + ', ' + response.data.id + ')">Kapak Fotoğrafı Yap</button>' +
                            '</div>')
                        .append('<div class="image-content" style="background-color: #e9ecef; padding: 10px; border-radius: 5px;">' +
                            '<img src="data:image/jpeg;base64,' + image.fileData + '" class="img-fluid" id="roomImage">' +
                            '</div>');

                    $('#imageSubContainer').append(imgElement);
                });

                var featureWrapper = $("#featureWrapper");
                featureWrapper.html("");

                response.data.roomFeatures.forEach(function (feature) {
                    var listItem = $('<li class="list-group-item"></li>').text(feature.features);
                    featureWrapper.append(listItem);
                });

            }
            else if (ForWhat == "Feature") {
                var featureContainer = $("#featureContainer");
                featureContainer.html("")
                response.data.roomFeatures.forEach(function (feature) {
                    var listItem = $('<li class="list-group-item d-flex align-items-center"></li>');

                    var checkbox = $('<input type="checkbox" class="form-check-input me-2" id="' + feature.featureId + '">');
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



function updateExtraSettings() {
    var formData = {
        Id: $('#roomNumber').val(),
        Price: parseFloat($('#roomPrice').val(), 10),
        Description: $('#description').val(),
        Capacity: parseInt($('#capacity').val(), 10),
        isMainPage: $('#isMainPage').val() == 1 ? true : false
    };

    $.ajax({
        type: 'POST',
        url: '/Admin/UpdateExtraSettings',
        data: formData,
        //contentType: 'application/json; charset=utf-8', 
        //dataType: 'json', 
        success: function (response) {
            if (response.success == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Başarıyla güncellendi!',
                    confirmButtonText: 'Tamam'
                });
                GetRoomDetail("General")
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Güncellenirken bir hata oluştu!',
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            alert('Beklenmedik bir hata oluştu: ' + error);
        }
    });
}







function setCoverImage(ImageId, RoomId) {

    $.ajax({
        type: 'POST',
        url: '/Admin/SetCoverImage',
        data: {
            ImageId: ImageId,
            RoomId: RoomId
        },
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Seçili resim başarıyla ayarlandı!',
                    confirmButtonText: 'Tamam'
                });
                GetRoomDetail("General");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Resim ayarlanamadı: ' + (response.failed || []).join(", "),
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            console.error("Hata detayları:", xhr.responseText);
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Beklenmedik bir hata oluştu: ' + error,
                confirmButtonText: 'Tamam'
            });
        }
    });
}

function deleteRoom() {
    var roomId = $("#roomNumber").val();

    Swal.fire({
        title: 'Emin misiniz?',
        text: "Bu odayı silmek istediğinizden emin misiniz? Bu işlem geri alınamaz!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, sil!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/Admin/DeleteRoom',
                data: {
                    RoomId: roomId
                },
                success: function (response) {
                  
                    if (response.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Başarılı!',
                            text: 'Seçili oda başarıyla silindi!',
                            confirmButtonText: 'Tamam'
                        }).then((result) => {
                            location.reload();
                        });
                       
                 
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Hata!',
                            text: 'Oda silinemedi',
                            confirmButtonText: 'Tamam'
                        });
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Hata detayları:", xhr.responseText);
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: 'Beklenmedik bir hata oluştu: ' + error,
                        confirmButtonText: 'Tamam'
                    });
                }
            });
        }
    });
}
function updateSocialMediaLinks() {
    var formData = $("#socialMediaLinksForms").serialize()
    $.ajax({
        type: 'POST',
        url: '/Admin/UpdateSocialMediaLinks',
        data: formData,
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Başarıyla güncellendi!',
                    confirmButtonText: 'Tamam'
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Güncellenemedi',
                    confirmButtonText: 'Tamam'
                });
            }
        },
        error: function (xhr, status, error) {
            console.error("Hata detayları:", xhr.responseText);
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Beklenmedik bir hata oluştu: ' + error,
                confirmButtonText: 'Tamam'
            });
        }
    });
}

function getRooms() {
    $.ajax({
        type: 'GET',
        url: '/Admin/GetRooms',
        success: function (response) {
            $('#roomNumber').empty();

            if (response && response.length > 0) {
                response.forEach(function (room) {
                    $('#roomNumber').append($('<option>', {
                        value: room.id,
                        text: room.name
                    }));
                });
            } else {
                $('#roomNumber').append($('<option>', {
                    value: '',
                    text: 'No rooms available'
                }));
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching rooms:", error);
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Odalar alınırken bir hata oluştu: ' + error,
                confirmButtonText: 'Tamam'
            });
        }
    });
}
