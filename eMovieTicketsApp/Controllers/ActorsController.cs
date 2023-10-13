using EmovieTicket.Data;
using EmovieTicket.Data.Services;
using EmovieTicket.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmovieTicket.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            //   var data = _context.Actors.ToList();//Binding Data To the View
            var data = await _service.GetAll();
            return View(data);//Passing List Of Actors
        }
        //Get:Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]//uplaod actor
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            _service.Add(actor);
            return RedirectToAction(nameof(Index));

        }
        //Get:Actors/Details/1
        public IActionResult Details(int id)
        {
            var actorDetails = _service.GetById(id);
            if (actorDetails == null) return View("NotFound");


            return View(actorDetails);
        }
        //Get:Actors/Create
        public IActionResult Edit(int id)
        {

            var actorDetails = _service.GetById(id);
            if (actorDetails == null) return View("NotFound");


            return View(actorDetails);
            
        }
        [HttpPost]//Edit actor
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            _service.Update(id,actor);
            return RedirectToAction(nameof(Index));

        }
        //Get:Actors/Delete
        public IActionResult Delete(int id)
        {

            var actorDetails = _service.GetById(id);
            if (actorDetails == null) return View("NotFound");


            return View(actorDetails);

        }
        [HttpPost, ActionName("Delete")]//Delete actor
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = _service.GetById(id);
            if (actorDetails == null) return View("NotFound");
            _service.Delete(id);
           
            return RedirectToAction(nameof(Index));

        }
    }
}
