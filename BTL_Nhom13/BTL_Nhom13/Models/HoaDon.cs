namespace BTL_Nhom13.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        public DateTime NgayDat { get; set; }

        [Required]
        [StringLength(100)]
        public string TinhTrang { get; set; }

        [Column(TypeName = "money")]
        public decimal PhiShip { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public int MaGioHang { get; set; }

        public virtual GioHang GioHang { get; set; }
    }
}
