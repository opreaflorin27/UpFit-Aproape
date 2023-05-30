using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UpFit__main.Models;

namespace UpFit__main.Controllers
{
    public class MealsController : Controller
    {
        private CodeFirstDb db = new CodeFirstDb();

        // GET: Meals
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return View(db.meals.ToList());
        }

        // GET: Meals/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }


        // GET: Meals/Create
        public ActionResult Create()
        {
            ViewBag.FoodFK = new SelectList(db.foods, "foodID", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mealID,mealTypeFK,userFK,foodFK,quantity,date,KcalMeal")] Meal meal, User user)
        {

            if (ModelState.IsValid)
            {
                /* foreach (Food food in db.foods)
                 {
                     if (meal.foodFK == food.foodID)
                     {
                         meal.KcalMeal = (meal.quantity / 100) * food.calories;
                     }
                 } */
                Food filteredData = db.foods.SingleOrDefault(x => x.foodID == meal.foodFK);
                meal.KcalMeal = (meal.quantity / 100) * filteredData.calories;
                Session["Quantity"] = meal.quantity;
                Session["calories"] = filteredData.calories;
                Session["name"] = filteredData.name;
                meal.date = DateTime.Today; // Set today's date
                db.meals.Add(meal);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(meal);
        }
        // GET: Meals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mealID,mealTypeFK,userFK,foodFK,quantity")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meal);
        }

        // GET: Meals/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.meals.Find(id);
            db.meals.Remove(meal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
