using EMBLL;
using EMENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Event_Management.Controllers
{
    public class RegistrationController : Controller
    {
        /// <summary>
        /// It displays the list of registrations for the particualar event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public ActionResult Index(int eventId)
        {
            RegistrationsService rs = new RegistrationsService();
            var registrations = rs.GetRegistrationsService(eventId);

            TempData["eventId"] = eventId;

            return View(registrations);
        }

        /// <summary>
        /// This method creates a new registration in the DB.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateRegistrationsView()
        {
            // Retrieve events from the database
            EventsService es = new EventsService();
            var events = es.GetEventsService();

            // Pass the events to the view
            ViewBag.EventId = new SelectList(events, "EventId", "EventName");

            // Create a new registration object
            Registrations newRegistration = null;

            return View(newRegistration);
        }

        /// <summary>
        /// This method adds a new registration based on the selected event from the dropdown.
        /// </summary>
        /// <param name="newRegistration"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult CreateRegistrationsView(Registrations newRegistration, int eventId)
        {
            // Set the EventId property of the new registration object based on the selected event
            newRegistration.EventId = eventId;

            // Create an instance of EventsService
            EventsService es = new EventsService();

            // Call GetEventsService upon es and store it inside events
            var events = es.GetEventsService();

            var listOfEventsDropdown = events.Select(e => new SelectListItem
            {
                Text = e.EventName,
                Value = e.EventId.ToString()
            }).ToList();

            // Use TempData to store it in listOfEventsDropdown
            TempData["listOfEventsDropdown"] = listOfEventsDropdown;
            TempData.Keep();

            // Save the new registration to the database
            RegistrationsService rs = new RegistrationsService();
            if (rs.AddRegistrationService(newRegistration))
            {
                ViewBag.Message = "The registration has been added successfully.";
            }

            return View(newRegistration);
        }

        /// <summary>
        /// It returns a view with a form to update the registration information with the record based on registrationId.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        public ActionResult EditRegistrationsView(int eventId, int registrationId)
        {
            RegistrationsService rs = new RegistrationsService();

            return View(rs.GetRegistrationsService(eventId).Find(x => x.RegistrationId == registrationId));
        }

        /// <summary>
        /// This method updates the event. If successful, it displays a confirmation message for the user.
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult EditRegistrationsView(Registrations registration)
        {
            RegistrationsService rs = new RegistrationsService();

            if (rs.UpdateRegistrationService(registration))
            {
                ViewBag.Message = "Registration has been updated successfully.";
            }
            return View(registration);
        }

        /// <summary>
        /// This method deletes a registration from the DB based on registrationId.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        public ActionResult DeleteRegistration(int registrationId)
        {
            RegistrationsService rs = new RegistrationsService();

            if (rs.DeleteRegistrationtService(registrationId))
            {
                return RedirectToAction("Index", new { eventId = TempData["eventId"] });
            }
            return null;
        }
    }
}