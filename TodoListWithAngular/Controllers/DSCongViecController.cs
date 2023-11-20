using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListWithAngular.Data;
using TodoListWithAngular.Models;

namespace TodoListWithAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DSCongViecController : ControllerBase
    {
        private MyDbContext _context;

       public DSCongViecController(MyDbContext context)
        {
            _context = context;
        }

    /*    [HttpGet]
        public IActionResult GetAll()
        {
            var dsCongViec = _context.CongViecs.Where(cv => cv.NgayXoa == null).ToList();
            return Ok(dsCongViec);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var congViec = _context.CongViecs.SingleOrDefault(cv => cv.Id == id);
            if(congViec != null)
            {
                return Ok(congViec);
            }
            else
            {
                return NotFound();
            }
        } */

        [HttpGet]
        public List<CongViecModel> GetByTitle(string? tieuDe, DateTime? date, string? sapXep)
        {
            /*  if (string.IsNullOrEmpty(title))
              {
                  return BadRequest("ban chua nhap ten tieu de");
              }*/

            /*var dscongviec = _context.CongViecs.Where(cv =>
            ((title == null)||(title!=null&& cv.TieuDe.Contains(title)))
            &&
            ((date == null)||(date != null && cv.NgayTaoCongViec >= date))
            ||
            (title == null && date == null && cv.NgayXoa==null)
            ).ToList();*/

            IQueryable<CongViec> query = _context.CongViecs;

            if(!string.IsNullOrWhiteSpace(tieuDe))
            {
                query = query.Where(cv => cv.TieuDe.Contains(tieuDe));
            }

            if(date.HasValue)
            {
                query = query.Where(cv => cv.NgayTaoCongViec >= date.Value);
            }

            if(!string.IsNullOrEmpty(sapXep))
            {
                switch (sapXep)
                {
                    case "tieuDe":
                        query = query.OrderBy(cv => cv.TieuDe);
                        break;
                    case "tieuDe_desc":
                        query = query.OrderByDescending(cv => cv.TieuDe);
                        break;
                    case "ngayTao":
                        query = query.OrderByDescending(cv => cv.NgayTaoCongViec); 
                        break;
                }
            }

            if (
                string.IsNullOrWhiteSpace(tieuDe) && 
                date == null &&
                string.IsNullOrWhiteSpace(sapXep)
                )
            {
                query = query.Where(cv => cv.NgayXoa == null);
            }
            return query.Select(cv => new CongViecModel
                {
                    Id = cv.Id,
                    TieuDe = cv.TieuDe,
                    MoTa = cv.MoTa,
                    NgayTaoCongViec = cv.NgayTaoCongViec
                }).ToList();
        }

        [HttpPost]
        public IActionResult Create(CongViecModel model)
        {
            try
            {
                var congViec = new CongViec
                {
                    TieuDe = model.TieuDe,
                    MoTa = model.MoTa,
                    NgayTaoCongViec = DateTime.UtcNow
                };
                _context.Add(congViec);
                _context.SaveChanges();
                return Ok(congViec);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, CongViecModel model)
        {
            var congViec = _context.CongViecs.SingleOrDefault(cv => cv.Id == id);
            if (congViec != null)
            {
                congViec.TieuDe = model.TieuDe;
                congViec.MoTa = model.MoTa;
                congViec.NgayTaoCongViec = DateTime.UtcNow;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var congViec = _context.CongViecs.SingleOrDefault(cv => cv.Id == id);
            if (congViec != null)
            {
                congViec.NgayXoa = DateTime.UtcNow;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
