namespace BTL_Nhom13.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        [StringLength(100)]
        [Required(ErrorMessage ="Tên đăng nhập không được để trống")]
        [MaxLength(20, ErrorMessage = "Tên đăng nhập phải có tối đa 20 ký tự")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có tối thiểu 3 ký tự")]
        [DisplayName("Tên tài khoản")]
        public string TenTaiKhoan { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MaxLength(20, ErrorMessage = "Mật khẩu phải có tối đa 20 ký tự")]
        [MinLength(3, ErrorMessage = "Mật khẩu nhập phải có tối thiểu 3 ký tự")]
        [DisplayName("Mật khẩu"), DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [DisplayName("Quyền")]
        public int Quyen { get; set; }

        [DisplayName("Tình trạng")]
        [UIHint("Boolean")]
        public bool TinhTrang { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [MaxLength(30, ErrorMessage = "Mật khẩu phải có tối đa 30 ký tự")]
        [DisplayName("Tên khách hàng")]
        public string TenKhachHang { get; set; }

        [StringLength(100)]
        [DisplayName("Địa chỉ email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage ="Địa chỉ email cần nhập đúng định dạng. VD: example@gmail.com")]
        public string Email { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Địa chỉ")]
        [MaxLength(100, ErrorMessage = "Địa chỉ phải có tối đa 100 ký tự")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
