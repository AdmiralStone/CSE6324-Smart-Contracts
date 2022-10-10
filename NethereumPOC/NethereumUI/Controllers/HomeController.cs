using NethereumUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NethereumUI.Controllers
{
    public class HomeController : Controller
    {
        SmartContractsContext db = new SmartContractsContext();
        public ActionResult Index()
        {
            var list = new List<ToDoListModel>();
            var checklist = db.ToDoLists.ToList();
            //model.Count = checklist.Count();
            foreach(var check in checklist)
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

        public ActionResult LoadData()
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
            return Json(new { data = contract }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveContract(HttpPostedFileBase file)
        {
            var fileExists = db.Contracts.Where(x => x.FileName == file.FileName);
            var fname = file.FileName.Split('.');
            if (fileExists.Count() == 0)
            {
                var entity = new Contract();
                entity.FileName = fname[0];
                entity.Size = file.ContentLength.ToString();
                entity.FileType = "sol";
                entity.ModifiedOn = DateTime.Now;
                entity.UploadedBy = "David";
                db.Contracts.Add(entity);
                db.SaveChanges();
            }

            return View("Index");
        }

        public ActionResult DeleteContract(int id)
        {
            var contract = db.Contracts.Where(x => x.FileId == id);
            if(contract.Count() == 1)
            {                
                db.Contracts.Remove(contract.FirstOrDefault());
                db.SaveChanges();
            }
           
            return Json("Contract Deleted.", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateToDoList(string id)
        {
            var entity = db.ToDoLists.Where(x => x.Description == id);
            if (entity.Count() == 1)
            { 
                if(entity.FirstOrDefault().Task == true)
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

            return Json("To Do List Updated.", JsonRequestBehavior.AllowGet);
        }
    }
}