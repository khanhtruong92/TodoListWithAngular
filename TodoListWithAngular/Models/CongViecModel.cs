using System.ComponentModel.DataAnnotations;

namespace TodoListWithAngular.Models
{
    public class CongViecModel
    {
        public int? Id { get; set; }
        [Required]
        public string TieuDe { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayTaoCongViec { get; set; }
    }
}
