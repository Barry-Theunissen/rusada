using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlainSpotters.Data;
using PlainSpotters.ViewModels;


namespace PlainSpotters.Controllers
{
    public class SightingsController : Controller
    {
        private readonly PlainSpottersContext _context;

        public SightingsController(PlainSpottersContext context)
        {
            _context = context;
        }

        #region Methods

        // GET: Sightings/Create
        public IActionResult Create()
        {
            var viewModel = new Sighting
                            {
                                Id = Guid.NewGuid().ToString(),
                                TimeSighted = DateTime.Now
                            };

            return View(viewModel);
        }

        // POST: Sightings/Create
        // To protect against over-posting attacks, enable the specific properties to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,Make,Model,Registration,TimeSighted")]
            Sighting sighting)
        {
            if (ModelState.IsValid)
            {
                sighting.Id = Guid.NewGuid().ToString();

                _context.Add(sighting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sighting);
        }

        // GET: Sightings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sighting = await _context.Sighting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sighting == null)
            {
                return NotFound();
            }

            return View(sighting);
        }

        // POST: Sightings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sighting = await _context.Sighting.FindAsync(id);
            _context.Sighting.Remove(sighting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Sightings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sighting = await _context.Sighting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sighting == null)
            {
                return NotFound();
            }

            return View(sighting);
        }

        // GET: Sightings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sighting = await _context.Sighting.FindAsync(id);
            if (sighting == null)
            {
                return NotFound();
            }

            return View(sighting);
        }

        // POST: Sightings/Edit/5
        // To protect against over-posting attacks, enable the specific properties to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Location,Make,Model,Registration,TimeSighted")]
            Sighting sighting)
        {
            if (id != sighting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sighting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SightingExists(sighting.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(sighting);
        }

        // GET: Sightings/Filter
        public IActionResult Filter()
        {
            return View();
        }

        // POST: Sightings/Filter
        // To protect against over-posting attacks, enable the specific properties to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Filter([Bind("Make,Model,Registration")] SightingFilterViewModel sightingFilterViewModel)
        {
            if (IsFiltered(sightingFilterViewModel))
            {
                var filteredSightings = await _context.Sighting.Where(x => x.Make.Contains(sightingFilterViewModel.Make) || x.Model.Contains(sightingFilterViewModel.Model) || x.Registration.Contains(sightingFilterViewModel.Registration)).ToListAsync();
                return View("Index", filteredSightings);
            }

            return View("Index", await _context.Sighting.ToListAsync());
        }

        // GET: Sightings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sighting.ToListAsync());
        }

        private bool IsFiltered(SightingFilterViewModel sightingFilterViewModel)
        {
            return !string.IsNullOrEmpty(sightingFilterViewModel.Registration) || !string.IsNullOrEmpty(sightingFilterViewModel.Make) || !string.IsNullOrEmpty(sightingFilterViewModel.Model);
        }

        private bool SightingExists(string id)
        {
            return _context.Sighting.Any(e => e.Id == id);
        }

        #endregion
    }
}