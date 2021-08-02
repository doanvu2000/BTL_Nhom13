using BTL_Nhom13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom13.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public string ThemGioHang(int MaSanPham, string TenSanPham, string NhaSX, string ChatLuong, double TrongLuong, string MoTa, string Anh, int SoLuongTon, decimal Gia)
        {
            if(Session["GioHang"] == null)
            {
                List<SanPhamDTO> listSP = new List<SanPhamDTO>();
                SanPhamDTO sp = new SanPhamDTO();
                sp.MaSP = MaSanPham;
                sp.TenSP = TenSanPham;
                sp.NhaSX = NhaSX;
                sp.ChatLuong = ChatLuong;
                sp.TrongLuong = TrongLuong;
                sp.MoTa = MoTa;
                sp.SoLuongTon = SoLuongTon;
                sp.Anh = Anh;
                sp.Gia = Gia;
                sp.SoLuongMua = 1;

                listSP.Add(sp);

                Session["GioHang"] = listSP;
                Session["SoLuongSPGioHang"] = listSP.Count + "";

                return listSP.Count + "";

            } else
            {
                List<SanPhamDTO> listSP = (List<SanPhamDTO>)Session["GioHang"];
                int vitri = KiemTraSanPhamDaTonTaiGioHang(listSP, MaSanPham);
                if(vitri == -1)
                {
                    SanPhamDTO sp = new SanPhamDTO();
                    sp.MaSP = MaSanPham;
                    sp.TenSP = TenSanPham;
                    sp.NhaSX = NhaSX;
                    sp.ChatLuong = ChatLuong;
                    sp.TrongLuong = TrongLuong;
                    sp.MoTa = MoTa;
                    sp.SoLuongTon = SoLuongTon;
                    sp.Anh = Anh;
                    sp.Gia = Gia;
                    sp.SoLuongMua = 1;

                    listSP.Add(sp);
                } else
                {
                    int SoLuongMuaMoi = listSP[vitri].SoLuongMua + 1;
                    listSP[vitri].SoLuongMua = SoLuongMuaMoi;
                }
                Session["SoLuongSPGioHang"] = listSP.Count + "";
                return listSP.Count + "";
            }
        }
        public int KiemTraSanPhamDaTonTaiGioHang(List<SanPhamDTO> listSP, int MaSP)
        {
            for(int i = 0; i < listSP.Count; i++)
            {
                if(listSP[i].MaSP == MaSP)
                {
                    return i;
                }
            }
            return -1;
        }
        public string XoaGioHang(int MaSanPham)
        {
            if(Session["GioHang"] != null)
            {
                List<SanPhamDTO> listSP = (List<SanPhamDTO>)Session["GioHang"];
                int vitri = KiemTraSanPhamDaTonTaiGioHang(listSP, MaSanPham);
                listSP.RemoveAt(vitri);
                Session["GioHang"] = listSP;
                Session["SoLuongSPGioHang"] = listSP.Count + "";
                return listSP.Count + "";
            }
            return null;
        }
        public string CapNhatSoLuongMua(int MaSanPham, int SoLuongMua)
        {
            if (Session["GioHang"] != null)
            {
                List<SanPhamDTO> listSP = (List<SanPhamDTO>)Session["GioHang"];
                int vitri = KiemTraSanPhamDaTonTaiGioHang(listSP, MaSanPham);
                // cap nhat so luong mua
                listSP[vitri].SoLuongMua = SoLuongMua;
                Session["GioHang"] = listSP;
            }
            return null;
        }

        public string XoaTatCaGioHang()
        {
            if (Session["GioHang"] != null)
            {
                Session["SoLuongSPGioHang"] = 0 + "";
                Session["GioHang"] = null;
            }
            return null;
        }
    }
}