using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            //what data do we need?
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(myspecies);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Add(string Name)
        {
            string query = "insert into species(Name) values(@SpeciesName)";
            SqlParameter param = new SqlParameter("@SpeciesName",Name);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            //need information about a particular Species
            //This function will return single species to update page 
            Species selectedpet = db.Species.SqlQuery("select * from Species where SpeciesID = @id", new SqlParameter("@id", id)).FirstOrDefault();

            return View(selectedpet);
        }

        [HttpPost]
        public ActionResult Update(int id,string sname)
        {
            //When user click on update button
            //All information will be passed here and 
            //It will commi on database
            string query = "update Species set Name=@speciesname where SpeciesID=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@speciesname", sname);
            sqlparams[1] = new SqlParameter("@id",id);
            db.Database.ExecuteSqlCommand(query,sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Show(int? id)
        {
            //Id will be passed when user click on species name
            //Particular species data will be pass to page
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //request for a particular species data in object and that object will be passed to show
            Species species = db.Species.SqlQuery("select * from Species where SpeciesID=@id", new SqlParameter("@id", id)).FirstOrDefault();
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }
        public ActionResult Delete(int? id)
        {
            //Delete a particular species
            string query = "delete from Species where SpeciesID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
        // Show
        // Add
        // [HttpPost] Add
        // Update
        // [HttpPost] Update
        // (optional) delete
        // [HttpPost] Delete

    }
}