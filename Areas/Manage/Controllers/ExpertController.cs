using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class ExpertController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ExpertController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Experts.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Expert expert)
        {
            if(!ModelState.IsValid)return View();
            string path = _environment.WebRootPath + @"\Upload\";
            string filename = Guid.NewGuid() + expert.formFile.FileName;
            using(FileStream fileStream= new FileStream(path+filename, FileMode.Create))
            {
                expert.formFile.CopyTo(fileStream);
            }
            expert.ImgUrl = filename;
            _context.Experts.Add(expert);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id) 
        { 
            Expert expert= _context.Experts.FirstOrDefault(x => x.Id == id);
            if (expert == null)
            {
                return RedirectToAction("Index");
            }

            return View(expert);
        }

        [HttpPost]
        public IActionResult Update(Expert expert)
        {
            Expert olddexpert = _context.Experts.FirstOrDefault(x => x.Id == expert.Id);
            if (!ModelState.IsValid) return NotFound();
            if(expert.formFile != null)
            {
                string path = _environment.WebRootPath + @"\Upload\";
                string filename = Guid.NewGuid() + expert.formFile.FileName;
                using (FileStream fileStream = new FileStream(path + filename, FileMode.Create))
                {
                    expert.formFile.CopyTo(fileStream);
                }
                olddexpert.ImgUrl = filename;
               
            }
            olddexpert.Name = expert.Name;
            olddexpert.Description = expert.Description;
               //_context.Update(expert);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Expert expert = _context.Experts.FirstOrDefault(x => x.Id == id);
            if (expert != null)
            {
                string path = _environment.WebRootPath + @"\Upload\"+expert.formFile;
                FileInfo fileInfo = new FileInfo(path);
                if(fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                _context.Experts.Remove(expert);
                _context.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

           
        
    }
}
