namespace BTL_Nhom13.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        [Key]
        [DisplayName("Mã sản phẩm")]
        public int MaSP { get; set; }
        [DisplayName("Mã danh mục")]
        public int MaDM { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string TenSP { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nhà sản xuất")]
        public string NhaSX { get; set; }

        [DisplayName("Trọng lượng")]
        public double TrongLuong { get; set; }
        [DisplayName("Số lượng tồn")]
        public int SoLuongTon { get; set; }
        [DisplayName("Giá")]
        [DisplayFormat(DataFormatString ="{0:#,###}")]
        public decimal Gia { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Chất lượng")]
        public string ChatLuong { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [DisplayName("Hình ảnh")]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
