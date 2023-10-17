using System.ComponentModel.DataAnnotations;

namespace TodoListWithAngular.Models
{
    public class CongViecModel
    {
        [Required]
        public string TieuDe { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayTaoCongViec { get; set; }
    }
}
