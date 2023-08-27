using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web2T.Models;

namespace Web2T.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/AdminShippers/[action]")]
    public class AdminShippersController : Controller
    {
        private readonly DbMarketsContext _context;
        public INotyfService _notyfService { get; }
        public AdminShippersController(DbMarketsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminShippers
        public async Task<IActionResult> Index()
        {
              return _context.Shippers != null ? 
                          View(await _context.Shippers.ToListAsync()) :
                          Problem("Entity set 'DbMarketsContext.Shippers'  is null.");
        }

        // GET: Admin/AdminShippers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shippers == null)
            {
                return NotFound();
            }

            var shipper = await _context.Shippers
                .FirstOrDefaultAsync(m => m.ShipperId == id);
            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // GET: Admin/AdminShippers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminShippers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipperId,ShipperName,Phone,Company,ShipDate")] Shipper shipper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipper);
                await _context.SaveChangesAsync();
                _notyfService.Success("Thêm mới Shipper thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        // GET: Admin/AdminShippers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shippers == null)
            {
                return NotFound();
            }

            var shipper = await _context.Shippers.FindAsync(id);
            if (shipper == null)
            {
                return NotFound();
            }
            return View(shipper);
        }

        // POST: Admin/AdminShippers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipperId,ShipperName,Phone,Company,ShipDate")] Shipper shipper)
        {
            if (id != shipper.ShipperId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipper);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipperExists(shipper.ShipperId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        // GET: Admin/AdminShippers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shippers == null)
            {
                return NotFound();
            }

            var shipper = await _context.Shippers
                .FirstOrDefaultAsync(m => m.ShipperId == id);
            if (shipper == null)
            {
                return NotFound();
            }

            return View(shipper);
        }

        // POST: Admin/AdminShippers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shippers == null)
            {
                return Problem("Entity set 'DbMarketsContext.Shippers'  is null.");
            }
            var shipper = await _context.Shippers.FindAsync(id);
            if (shipper != null)
            {
                _context.Shippers.Remove(shipper);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa Shipper thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool ShipperExists(int id)
        {
          return (_context.Shippers?.Any(e => e.ShipperId == id)).GetValueOrDefault();
        }
    }
}
