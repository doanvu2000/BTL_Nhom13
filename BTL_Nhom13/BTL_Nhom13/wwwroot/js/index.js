$(document).ready(function () {

    $('#btnSuaThongTin').click(function () {
        $('#btnSuaThongTin').hide();
        $('#btnLuuThongTin').show();
        $('#btnHuy').show();
        $("#TenKhachHang").attr("readonly", false); 
        $("#Email").attr("readonly", false); 
        $("#SoDienThoai").attr("readonly", false); 
        $("#DiaChi").attr("readonly", false); 
    });
    $('#btnHuy').click(function () {
        $('#btnHuy').hide();
        $('#btnLuuThongTin').hide();
        $('#btnSuaThongTin').show();
        $("#TenKhachHang").attr("readonly", true);
        $("#Email").attr("readonly", true);
        $("#SoDienThoai").attr("readonly", true);
        $("#DiaChi").attr("readonly", true);
    });
});