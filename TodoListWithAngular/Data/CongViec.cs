using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListWithAngular.Data
{
    [Table("DanhSachCongViec")]
    public class CongViec
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TieuDe { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayTaoCongViec { get; set; }
        public DateTime? NgayXoa { get; set; }
    }
}
