var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('#old_pass').on('blur', function (e) {
            var pass = $(this).val();
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/AdminTaiKhoans/ChangePassword",
                data: { id : id, pass: pass },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        $('#new_pass').prop('readonly', false);
                        $('#again_pass').prop('readonly', false);
                    } else {
                        $('#new_pass').prop('readonly', true);
                        $('#again_pass').prop('readonly', false);
                    }
                }
            });
        });
    }
}
$('#btn-submit').on('click', function (e) {
    var v1 = $('#new_pass').val();
    var v2 = $('#again_pass').val();
    if (v1 != v2) {
        e.preventDefault();
        alert('Mật khẩu mới không khớp');
    }
});

user.init();