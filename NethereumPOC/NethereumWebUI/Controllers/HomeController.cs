using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NethereumWebUI.Data;
using NethereumWebUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace NethereumWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly SmartContractsContext db;
        public HomeController(ILogger<HomeController> logger, SmartContractsContext dbContext)
        {
            db = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list = new List<ToDoListModel>();
            var checklist = db.ToDoLists.ToList();
            //model.Count = checklist.Count();
            foreach (var check in checklist)
            {
                var data = new ToDoListModel();
                data.ToDoId = check.ToDoId;
                data.UserName = check.UserName;
                data.Description = check.Description;
                data.Task = check.Task;
                data.ModifiedDate = check.ModifiedDate.ToString();
                list.Add(data);
            }
            var model = new ToDoListModel();
            model.toDoLists = list;
            return View(model);
        }

        public IActionResult LoadData()
        {
            var contract = (from dt in db.Contracts
                            select new ContractModel
                            {
                                FileId = dt.FileId,
                                FileName = dt.FileName,
                                Size = dt.Size,
                                FileType = dt.FileType,
                                ModifiedOn = dt.ModifiedOn.ToString(),
                                UploadedBy = dt.UploadedBy,
                            }).OrderBy(x => x.FileName).ToList();

            //return Json(new { data = contract }, JsonRequestBehavior.AllowGet);
            string jsonresult = JsonConvert.SerializeObject(contract);
            return Json(jsonresult);
        }

        [HttpPost]
        public IActionResult SaveContract(IFormFile file)
        {
            var fileExists = db.Contracts.Where(x => x.FileName == file.FileName);
            var fname = file.FileName.Split('.');
            if (fileExists.Count() == 0)
            {
                var entity = new Contract();
                entity.FileName = fname[0];
                entity.Size = file.Length.ToString();
                entity.FileType = "sol";
                entity.ModifiedOn = DateTime.Now;
                entity.UploadedBy = "David";
                db.Contracts.Add(entity);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteContract(int id)
        {
            var contract = db.Contracts.Where(x => x.FileId == id);
            if (contract.Count() == 1)
            {
                db.Contracts.Remove(contract.FirstOrDefault());
                db.SaveChanges();
            }

            string jsonresult = JsonConvert.SerializeObject(contract);
            return Json(jsonresult);
        }

        public IActionResult UpdateToDoList(string id)
        {
            var entity = db.ToDoLists.Where(x => x.Description == id);
            if (entity.Count() == 1)
            {
                if (entity.FirstOrDefault().Task == true)
                {
                    entity.FirstOrDefault().Task = false;
                }
                else
                {
                    entity.FirstOrDefault().Task = true;
                }
                db.Entry(entity.FirstOrDefault()).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json("To Do List Updated.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}