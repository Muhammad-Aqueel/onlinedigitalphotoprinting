using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using digitalphotoprinting.Data;
using digitalphotoprinting.Models;
using digitalphotoprinting.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace digitalphotoprinting.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrders
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return _context.PurchaseOrders != null ?
                        View(await _context.PurchaseOrders.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PurchaseOrders'  is null.");
        }

        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchaseOrders == null)
            {
                ////return NotFound();
                return RedirectToAction(nameof(page404));
            }

            var purchaseOrder = await _context.PurchaseOrders
                .FirstOrDefaultAsync(m => m.Order_Number == id);

            if (User.IsInRole("User"))
            {
                if (purchaseOrder.Email != User.Identity?.Name)
                {
                    return RedirectToAction(nameof(page404));
                }
            }
            if (purchaseOrder == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(page404));
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Order_Number,Folder_Name,Print_size,Email,Email_Subject,Email_Text,Credit_Card_Number,Image_Name")] PurchaseOrder purchaseOrder)
        public async Task<IActionResult> Create(ImageUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                PurchaseOrder purchaseOrder = new()
                {
                    Image_Title = model.Image_Title,
                    Folder_Name = model.Folder_Name,
                    Print_size = model.Print_size,
                    Email = model.Email,
                    Email_Subject = model.Email_Subject,
                    Email_Text = model.Email_Text,
                    Credit_Card_Number = model.Credit_Card_Number,
                    Status = model.Status,
                    Image_Name = uniqueFileName
                };

                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PurchaseOrders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchaseOrders == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(page404));
            }
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder.Status != "pending")
            {
                return RedirectToAction(nameof(Index));
            }
            var imageUpdateModel = new ImageUpdateModel()
            {
                Order_Number = purchaseOrder.Order_Number,
                Image_Title = purchaseOrder.Image_Title,
                Folder_Name = purchaseOrder.Folder_Name,
                Print_size = purchaseOrder.Print_size,
                Email = purchaseOrder.Email,
                Email_Subject = purchaseOrder.Email_Subject,
                Email_Text = purchaseOrder.Email_Text,
                Credit_Card_Number = purchaseOrder.Credit_Card_Number,
                Status = purchaseOrder.Status,
                Image_Name = purchaseOrder.Image_Name
            };
            if (purchaseOrder == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(page404));
            }
            return View(imageUpdateModel);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ImageUpdateModel imageUpdateModel)
        {
            if (id != imageUpdateModel.Order_Number)
            {
                //return NotFound();
                return RedirectToAction(nameof(page404));
            }

            if (ModelState.IsValid)
            {
                var purchaseOrder = await _context.PurchaseOrders.FindAsync(imageUpdateModel.Order_Number);
                string Existing_Image = purchaseOrder.Image_Name;
                purchaseOrder.Image_Title = imageUpdateModel.Image_Title;
                purchaseOrder.Folder_Name = imageUpdateModel.Folder_Name;
                purchaseOrder.Print_size = imageUpdateModel.Print_size;
                purchaseOrder.Email = imageUpdateModel.Email;
                purchaseOrder.Email_Subject = imageUpdateModel.Email_Subject;
                purchaseOrder.Email_Text = imageUpdateModel.Email_Text;
                purchaseOrder.Credit_Card_Number = imageUpdateModel.Credit_Card_Number;
                purchaseOrder.Status = imageUpdateModel.Status;
                purchaseOrder.Image_Name = imageUpdateModel.Image_Name;
                try
                {
                    if (imageUpdateModel.File != null)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/" + imageUpdateModel.Folder_Name);
                        string fileName = Existing_Image;
                        string fileNameWithPath = Path.Combine(path, fileName);
                        System.IO.File.Delete(fileNameWithPath);

                        purchaseOrder.Image_Name = ProcessUploadedFile(imageUpdateModel);
                    }
                    if (User.IsInRole("Admin")) {
                        if (purchaseOrder.Status != "pending")
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/" + imageUpdateModel.Folder_Name);
                            string fileName = Existing_Image;
                            string fileNameWithPath = Path.Combine(path, fileName);
                            System.IO.File.Delete(fileNameWithPath);
                        }
                    }
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.Order_Number))
                    {
                        //return NotFound();
                        return RedirectToAction(nameof(page404));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imageUpdateModel);
        }

        [Authorize(Roles = "User")]
        // GET: PurchaseOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchaseOrders == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(page404));
            }

            var purchaseOrder = await _context.PurchaseOrders
                .FirstOrDefaultAsync(m => m.Order_Number == id);

            if (purchaseOrder.Email != User.Identity?.Name)
            {
                return RedirectToAction(nameof(page404));
            }
            if (purchaseOrder.Status != "pending")
            {
                return RedirectToAction(nameof(Index));
            }
            if (purchaseOrder == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(page404));
            }

            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchaseOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PurchaseOrders'  is null.");
            }
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder != null)
            {
                _context.PurchaseOrders.Remove(purchaseOrder);

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/" + purchaseOrder.Folder_Name);
                string fileName = purchaseOrder.Image_Name;
                string fileNameWithPath = Path.Combine(path, fileName);
                System.IO.File.Delete(fileNameWithPath);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult page404()
        {
            return View();
        }
        private bool PurchaseOrderExists(int id)
        {
          return (_context.PurchaseOrders?.Any(e => e.Order_Number == id)).GetValueOrDefault();
        }

        private string ProcessUploadedFile(ImageUpdateModel model)
        {
            string uniqueFileName = null;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/" + model.Folder_Name);

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            //FileInfo fileInfo = new FileInfo(model.File.FileName);
            string fileName = model.Image_Name;
            //string fileName = model.ImageName + fileInfo.Extension;

            if (model.File != null)
            {
                string fileNameWithPath = Path.Combine(path, fileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                fileNameWithPath = Path.Combine(path, uniqueFileName);
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
            }

            return uniqueFileName;
        }
    }
}
