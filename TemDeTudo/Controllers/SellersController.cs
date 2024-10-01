using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemDeTudo.Data;
using TemDeTudo.Models;

namespace TemDeTudo.Controllers
{
    public class SellersController : Controller
    {
        private readonly TemDeTudoContext _context;
        public SellersController(TemDeTudoContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {

            //List<Seller> sellers = _context.Seller.ToList();
            var sellers = _context.Seller.Include("Department").ToList();
            return View(sellers);
        }

        public IActionResult Create() 
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Seller seller) 
        {
            //Testa se foi passado um vendedor
            if (seller == null) { return NotFound(); }

            seller.Department = _context.Department.FirstOrDefault();

            //Adicionar o objeto vendedor ao banco
            _context.Seller.Add(seller);
            //Persiste as informacoes
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
