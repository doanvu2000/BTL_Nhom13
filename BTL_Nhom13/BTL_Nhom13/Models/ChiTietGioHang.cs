namespace BTL_Nhom13.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietGioHang")]
    public partial class ChiTietGioHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaGioHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        public int SoLuongMua { get; set; }
        [DisplayName("Giá")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Giá sản phẩm phải là một số.")]
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal Gia { get; set; }

        public virtual GioHang GioHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
