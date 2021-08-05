$('#btn-change-pass').on('click', function (e) {
    var v1 = $('#new_pass').val();
    var v2 = $('#again_pass').val();
    if (v1 != v2) {
        e.preventDefault();
        alert('Mật khẩu mới không khớp');
    }
});