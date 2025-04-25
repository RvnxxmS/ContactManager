using System;
using System.Web.Mvc;
using ContactManager.Core.Entities;
using ContactManager.Core.Services;
using System.Linq;

namespace ContactManager.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public ActionResult Index(string searchQuery = "")
        {
            var contacts = _contactService.GetAllContacts();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                contacts = contacts.Where(c => c.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            ViewBag.SearchQuery = searchQuery;
            return View(contacts);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.AddContact(contact);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(contact);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.AddContact(contact);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                _contactService.RemoveContact(id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}