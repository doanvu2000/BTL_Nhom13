namespace BTL_Nhom13.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        [Key]
        public int MaSP { get; set; }
        [DisplayName("Mã danh mục")]
        [Required(ErrorMessage = "Mã danh mục không được để trống!")]
        public int MaDM { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [DisplayName("Tên sản phẩm")]
        public string TenSP { get; set; }

        [Required(ErrorMessage = "Nhà sản xuất không được để trống!")]
        [StringLength(100)]
        [DisplayName("Nhà sản xuất")]
        public string NhaSX { get; set; }

        [DisplayName("Trọng lượng")]
        [Required(ErrorMessage = "Trọng lượng không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Trọng lượng phải là một số.")]
        public double TrongLuong { get; set; }
        [DisplayName("Số lượng tồn")]
        [Required(ErrorMessage = "Số lượng tồn không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Số lượng phải là một số.")]
        public int SoLuongTon { get; set; }
        [DisplayName("Giá")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Giá sản phẩm phải là một số.")]
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Chất lượng không được để trống!")]
        [StringLength(100)]
        [DisplayName("Chất lượng")]
        public string ChatLuong { get; set; }

        [Column(TypeName = "ntext")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [DisplayName("Hình ảnh")]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
