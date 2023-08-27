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
using PagedList.Core;
using Web2T.Helpper;
using Web2T.Models;

namespace Web2T.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/AdminProducts/[action]")]
    public class AdminProductsController : Controller
    {
        private readonly DbMarketsContext _context;

        public INotyfService _notyfService { get; }

        public AdminProductsController(DbMarketsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminProducts
        public IActionResult Index(int page = 1, int CatID = 0)
        {
            var pageNumber = page;
            var pageSize = 20;

            List<Product> lsProducts = new List<Product>();

            if (CatID != 0)
            {
                lsProducts = _context.Products
                .AsNoTracking()
                .Where(x=>x.CatId==CatID)
                .Include(x => x.Cat)
                .OrderByDescending(x => x.ProductId).ToList();
            }
            else
            {
                lsProducts = _context.Products
                .AsNoTracking()
                .Include(x => x.Cat)
                .OrderByDescending(x => x.ProductId).ToList();
            }
            PagedList<Product> models = new PagedList<Product>(lsProducts.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCateID = CatID;
            ViewBag.CurrentPage = pageNumber;
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", CatID);
            return View(models);
        }
        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Admin/AdminProducts?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminProducts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] Product product, IFormFile? fThumb, IFormFile? fPic1, IFormFile? fPic2, IFormFile? fPic3)
        {
            if (ModelState.IsValid)
            {
                product.ProductName = Utilities.ToTitleCase(product.ProductName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(product.ProductName) + extension;
                    product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());

                }
                if (fPic1 != null)
                {
                    string extension = Path.GetExtension(fPic1.FileName);
                    string image = Utilities.SEOUrl(product.ProductName + "1") + extension;
                    product.Pic1 = await Utilities.UploadFile(fPic1, @"products", image.ToLower());

                }
                if (fPic2 != null)
                {
                    string extension = Path.GetExtension(fPic2.FileName);
                    string image = Utilities.SEOUrl(product.ProductName + "2") + extension;
                    product.Pic2 = await Utilities.UploadFile(fPic2, @"products", image.ToLower());

                }
                if (fPic3 != null)
                {
                    string extension = Path.GetExtension(fPic3.FileName);
                    string image = Utilities.SEOUrl(product.ProductName + "3") + extension;
                    product.Pic3 = await Utilities.UploadFile(fPic3, @"products", image.ToLower());

                }
                if (string.IsNullOrEmpty(product.Thumb)) product.Thumb = "default.jpg";
                product.Alias = Utilities.SEOUrl(product.ProductName);
                product.DateModified = DateTime.Now;
                product.DateCreated = DateTime.Now;

                _context.Add(product);
                await _context.SaveChangesAsync();
                _notyfService.Success("Thêm sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] Product product, IFormFile? fThumb, IFormFile? fPic1, IFormFile? fPic2, IFormFile? fPic3)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid) // <=== do sai  IsValid
            {
                try
                {
                    product.ProductName = Utilities.ToTitleCase(product.ProductName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());

                    }
                    if (fPic1 != null)
                    {
                        string extension = Path.GetExtension(fPic1.FileName);
                        string image = Utilities.SEOUrl(product.ProductName + "1") + extension;
                        product.Pic1 = await Utilities.UploadFile(fPic1, @"products", image.ToLower());

                    }
                    if (fPic2 != null)
                    {
                        string extension = Path.GetExtension(fPic2.FileName);
                        string image = Utilities.SEOUrl(product.ProductName + "2") + extension;
                        product.Pic2 = await Utilities.UploadFile(fPic2, @"products", image.ToLower());

                    }
                    if (fPic3 != null)
                    {
                        string extension = Path.GetExtension(fPic3.FileName);
                        string image = Utilities.SEOUrl(product.ProductName + "3") + extension;
                        product.Pic3 = await Utilities.UploadFile(fPic3, @"products", image.ToLower());

                    }
                    if (string.IsNullOrEmpty(product.Thumb)) product.Thumb = "default.jpg";
                    product.Alias = Utilities.SEOUrl(product.ProductName);
                    product.DateModified = DateTime.Now;

                    _context.Update(product);
                    _notyfService.Success("Cập nhật sản phẩm thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    string msg = ex.Message;
                    if (!ProductExists(product.ProductId))
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
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DbMarketsContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
