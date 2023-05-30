using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UpFit__main.Models;

namespace UpFit__main.Controllers
{
    public class AdminsController : Controller
    {
        private CodeFirstDb _context = new CodeFirstDb();

        // GET: Admins/Home
        public ActionResult Home()
        {
            return View();
        }

        //****************************List****************************************//

        // GET: Admins/ListAdmins
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult ListAdmins()
        {
            return View(_context.admins.ToList());
        }

        // GET: Admins/ListUsers
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult ListUsers()
        {
            return View(_context.users.ToList());
        }

        // GET: Admins/ListFoods
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult ListFoods()
        {
            return View(_context.foods.ToList());
        }

        // GET: Admins/ListFoodTypes
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult ListFoodTypes()
        {
            return View(_context.foodTypes.ToList());
        }

        //**************************************************************************//

        public ActionResult Login(Admin admin)
        {
            using (CodeFirstDb db = new CodeFirstDb())
            {
                var adm = db.admins.SingleOrDefault(u => u.UserName == admin.UserName && u.Password == admin.Password);
                if (adm != null)
                {
                    Session["UserID"] = adm.AdminID.ToString();
                    Session["UserName"] = adm.UserName.ToString();
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password.");
                }
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        //********************************ADD****************************************//

        // POST: Admins/AddAdmin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _context.admins.Add(admin);
            _context.SaveChanges();
            return RedirectToAction("ListAdmins");
        }

        // POST: Admins/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(User user)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _context.users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("ListUsers");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddFoodTypes(Food food)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Retrieve the selected food type from the database
        //        var foodType = _context.foodTypes.SingleOrDefault(ft => ft.ID_Type == food.type);
        //        if (foodType != null)
        //        {
        //            // Associate the food type with the food entity
        //            food.type = foodType.ID_Type;
        //
        //            _context.foods.Add(food);
        //            _context.SaveChanges();
        //            return RedirectToAction("ListFoodTypes");
        //        }
        //        else
        //            ModelState.AddModelError("", "Invalid food type.");
        //    }
        //
        //    ViewBag.FoodTypes = _context.foodTypes.ToList();
        //
        //    return View(food);
        //}

        //********************************DELETE****************************************//

        // DELETE: Admins/Delete/5
        [HttpDelete]
        public ActionResult DeleteAdmin(int? id)
        {
            Admin admin = _context.admins.SingleOrDefault(u => u.AdminID == id);
            if (admin == null)
                return HttpNotFound();
            _context.admins.Remove(admin);
            _context.SaveChanges();
            return RedirectToAction("ListAdmins");
        }

        // DELETE: Admins/DeleteUser/5
        [HttpDelete]
        public ActionResult DeleteUser(int? id)
        {
            User user = _context.users.SingleOrDefault(u => u.UserID == id);
            if (user == null)
                return HttpNotFound();
            _context.users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("ListUsers");
        }

        // DELETE: Admins/DeleteFood/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFood(int id)
        {
            Food food = _context.foods.SingleOrDefault(f => f.foodID == id);
            if (food == null)
                return HttpNotFound();
            _context.foods.Remove(food);
            _context.SaveChanges();
            return RedirectToAction("ListFoods");
        }

        // DELETE: Admins/DeleteFoodTypes/1
        [HttpDelete]
        public ActionResult DeleteFoodTypes(int? id)
        {
            FoodType foodType = _context.foodTypes.SingleOrDefault(u => u.ID_Type == id);
            if (foodType == null)
                return HttpNotFound();
            _context.foodTypes.Remove(foodType);
            _context.SaveChanges();
            return RedirectToAction("ListFoodTypes");
        }

        //*********************************EDIT******************************************//


        // PUT: Admins/EditFood/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditFoods(Food food)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _context.Entry(food).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ListFoods");
        }

        // PUT: Admins/EditFoodType/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditFoodTypes(FoodType foodType)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _context.Entry(foodType).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ListFoodTypes");
        }

        //********************************DETAILS****************************************//

        // GET: Admins/DetailsFood/5
        public ActionResult DetailsFoods(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _context.foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }
    }
}

