
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoaVMs = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoaVMs);
        }
        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hangHoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };
            hangHoaVMs.Add(hangHoa);
            return Ok(new
            {
                Success = true,
                Data = hangHoa
            });

        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var hangHoa = hangHoaVMs.SingleOrDefault(result => result.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                    NotFound();
                return Ok(hangHoa);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, HangHoa ClassModelHangHoa)
        {
            try
            {
                var hangHoa = hangHoaVMs.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (id != hangHoa.MaHangHoa.ToString())
                    NotFound();
                if (hangHoa == null)
                    NotFound();
                // update
                hangHoa.TenHangHoa = ClassModelHangHoa.TenHangHoa;
                hangHoa.DonGia = ClassModelHangHoa.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try { 
                var hangHoa = hangHoaVMs.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if(hangHoa ==null) NotFound();
                if(id!= hangHoa.MaHangHoa.ToString()) NotFound();
                hangHoaVMs.Remove(hangHoa);
                return Ok();
            } catch { return BadRequest(); }
        }

    }
}