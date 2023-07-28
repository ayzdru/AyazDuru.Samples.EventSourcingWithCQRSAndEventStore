function showAlert(title, message, type) {
    $.notifyClose();
    $.notify({
        title: '<strong>' + title + '</strong>',
        message: '</br>' + message
    },
        {
            type: type,
            newest_on_top: true,
            z_index: 1051
        }
    );
}
function successAlert(data) {
    if (data) {
        5
        showAlert(data.title, data.message, 'success');
    }
}
function errorAlert(error) {
    if (error) {
        showAlert(error.title, error.message, 'danger');
    }
}
function formSubmit(form, modalId, onSuccess, hideAlert) {
    hideAlert = typeof hideAlert !== 'undefined' ? hideAlert : false;
    var $form = $(form);
    if ($form.valid()) {
        var $modal = $('#' + modalId);
        var $button = $form.find('button[type=submit]');
        var formData = new FormData();
        var formParams = $form.serializeArray();
        $.each($form.find('input[type="file"]'), function (i, tag) {
            $.each($(tag)[0].files, function (i, file) {
                formData.append(tag.name, file);
            });
        });

        $.each(formParams, function (i, val) {
            formData.append(val.name, val.value);
        });
        $.ajax({
            url: $form.prop('action'),
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data, textStatus, jqXHR) {
                $modal.modal('hide');
                if (hideAlert == false) {
                    successAlert(data);
                }
                if (onSuccess) {
                    onSuccess(data);
                }
            },
            error: function (error) {
                errorAlert(error.responseJSON);
            }
        });
    }
}