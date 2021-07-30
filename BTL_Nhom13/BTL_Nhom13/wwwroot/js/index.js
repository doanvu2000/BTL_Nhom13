$(document).ready(function () {

    $('#btnSuaThongTin').click(function () {
        $('#btnSuaThongTin').hide();
        $('#btnLuuThongTin').show();
        $('#btnHuy').show();
        $("#MatKhau").attr("readonly", false); 
        $("#TenKhachHang").attr("readonly", false); 
        $("#Email").attr("readonly", false); 
        $("#SoDienThoai").attr("readonly", false); 
        $("#DiaChi").attr("readonly", false); 
    });
    $('#btnHuy').click(function () {
        $('#btnHuy').hide();
        $('#btnLuuThongTin').hide();
        $('#btnSuaThongTin').show();
        $("#MatKhau").attr("readonly", true); 
        $("#TenKhachHang").attr("readonly", true);
        $("#Email").attr("readonly", true);
        $("#SoDienThoai").attr("readonly", true);
        $("#DiaChi").attr("readonly", true);
    });

    $(".btn-giohang").click(function () {
        var MaSanPham = $(this).closest(".detail_P02").find(".MaSP").text();
        var TenSanPham = $(this).closest(".detail_P02").find(".TenSP").text();
        var NhaSX = $(this).closest(".detail_P02").find(".NhaSX").text();
        var ChatLuong = $(this).closest(".detail_P02").find(".ChatLuong").text();
        var TrongLuong = $(this).closest(".detail_P02").find(".TrongLuong").text();
        var MoTa = $(this).closest(".detail_P02").find(".MoTa").text();
        var Anh = $(this).closest(".detail_P02").find(".Anh").text();
        var SoLuongTon = $(this).closest(".detail_P02").find(".SoLuongTon").text();
        var Gia = $(this).closest(".detail_P02").find(".Gia").text();

        //alert(Anh + " " + TenSanPham);

        //alert(MaSanPham + " " + TenSanPham + " " + NhaSX + ChatLuong + " " + TrongLuong + " " + MoTa + " " + Anh + " " + SoLuongTon + Gia)
        //alert(machitietsanpham);
        //alert(masanpham+tensanpham + "-" + dongia + "-" + mamau + "-" + tenmau + masize + tensize);

        $.ajax({
            url: "/Ajax/ThemGioHang",
            type: "GET",
            data: {
                MaSanPham: MaSanPham,
                TenSanPham: TenSanPham,
                NhaSX: NhaSX,
                ChatLuong: ChatLuong,
                TrongLuong: TrongLuong,
                MoTa: MoTa,
                Anh: Anh,
                SoLuongTon: SoLuongTon,
                Gia: Gia
            },
            success: function (value) {
                $("#GioHang").html("<span>Giỏ hàng</span> <strong>" + value + "</strong>");
                //alert(value);

            }
        })

    });
});