using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_Nhom13.Models
{
    public class SanPhamDTO
    {
        public int MaSP { get; set; }
        public int MaDM { get; set; }
        public string TenSP { get; set; }
        public string NhaSX { get; set; }
        public double TrongLuong { get; set; }
        public int SoLuongTon { get; set; }
        public decimal Gia { get; set; }
        public string ChatLuong { get; set; }
        public string MoTa { get; set; }
        public string Anh { get; set; }
        public int SoLuongMua { get; set; }
    }
}