using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist.Data;
using System.Diagnostics;
using Todolist.Models;
using Todolist.Models.Entities;

namespace Todolist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            int pageSize = 5;
            var Tododata = _dbContext.Tododatasets
                .Where(t => t.IsActive)
                .AsNoTracking();

            return View(await PaginatedListViewModel<Tododataset>
                .CreateAsync(Tododata, pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var Data = new Tododataset
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Status = true,
                IsActive = true,
                IsDeleted = false,
                AddedOn = DateTime.Now,
                DeletedOn = null,
                UpdatedOn = DateTime.Now,
            };
            await _dbContext.AddAsync(Data);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, int pageindex)
        {
            var Tododataset = await _dbContext.Tododatasets.FindAsync(id);
            return View(Tododataset);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Tododataset viewModel, int pageindex)
        {
            int pageNumber = pageindex;
            var Tododataset = await _dbContext.Tododatasets.FindAsync(viewModel.Id);
            if (Tododataset is not null)
            {
                Tododataset.Title = viewModel.Title;
                Tododataset.Description = viewModel.Description;
                Tododataset.UpdatedOn = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { pageNumber});
        }
        [HttpPost]
        public async Task<IActionResult> Completed(Tododataset viewModel, int pageindex)
        {
            int pageNumber = pageindex;
            var Tododataset = await _dbContext.Tododatasets.FindAsync(viewModel.Id);
            if (Tododataset.Status == true)
            {
                Tododataset.Status = false;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                Tododataset.Status=true;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", new {pageNumber});
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Tododataset viewModel)
        {
            var Data = await _dbContext.Tododatasets.FindAsync(viewModel.Id);
            if (Data is not null)
            {
                Data.IsActive = false;
                Data.DeletedOn = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Bin()
        {
            var Tododata = await _dbContext.Tododatasets.Where(t => !t.IsActive).ToListAsync();
            return View(Tododata);
        }
        [HttpPost]
        public async Task<IActionResult> Restore(Guid id)
        {
            var Data = await _dbContext.Tododatasets.FindAsync(id);
            if(Data is not null)
            {
                Data.IsActive = true;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> PermanentDelete(Tododataset viewModel)
        {
            var Data = await _dbContext.Tododatasets.FindAsync(viewModel.Id);
            if (Data is not null)
            {
                _dbContext.Tododatasets.Remove(Data);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Bin");
        }
            
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
