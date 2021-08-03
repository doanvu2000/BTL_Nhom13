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

    $(".btn-xoagiohang").click(function () {
        //alert("Helo");
        var self = $(this);
        var MaSanPham = $(this).closest("tr").find(".p_MaSP").text();

        //alert(MaSanPham);
        //alert(masanpham + "-" + "-" + mamau + "-" + masize + "-" + machitietsanpham);

        $.ajax({
            url: "/Ajax/XoaGioHang",
            type: "GET",
            data: {
                MaSanPham: MaSanPham
            },
            success: function (value) {
                self.closest("tr").remove();
                if (value != 0) {
                    $("#giohang").html("<span>" + value + "</span>");
                    $("#GioHang").html("<span>Giỏ hàng</span> <strong>" + value + "</strong>");
                } else {
                    $("#GioHang").html("<span>Giỏ hàng</span> <strong>" + "0" + "</strong>");
                }
                location.reload();
            }
        })

    });

    $(".input_soLuongMua").change(function () {
        var SoLuongMua = $(this).val();
        var MaSanPham = $(this).closest("tr").find(".p_MaSP").text();
        var SoLuongTon = parseInt($(this).attr("data-slTon"));
        //alert(SoLuongTon)
        if (SoLuongMua > SoLuongTon) {
            alert("Số lượng hàng trong kho không đủ cung cấp");
        } else {
            if (SoLuongMua == 0) {
                var self = $(this);
                $.ajax({
                    url: "/Ajax/XoaGioHang",
                    type: "GET",
                    data: {
                        MaSanPham: MaSanPham
                    },
                    success: function (value) {
                        self.closest("tr").remove();
                        if (value != 0) {
                            $("#giohang").html("<span>" + value + "</span>");
                            $("#GioHang").html("<span>Giỏ hàng</span> <strong>" + value + "</strong>");
                        } else {
                            $("#GioHang").html("<span>Giỏ hàng</span> <strong>" + "0" + "</strong>");
                        }
                        location.reload();
                    }
                })
            } else {

                $.ajax({
                    url: "/Ajax/CapNhatSoLuongMua",
                    type: "GET",
                    data: {
                        SoLuongMua: SoLuongMua,
                        MaSanPham: MaSanPham

                    },
                    success: function (value) {
                        location.reload();
                    }
                })
            }
        }
        
        
    })

    $(".btn-xoaAll").click(function () {
        $.ajax({
            url: "/Ajax/XoaTatCaGioHang",
            type: "GET",
            data: {
            },
            success: function (value) {
                location.reload();
            }
        })
    })
});