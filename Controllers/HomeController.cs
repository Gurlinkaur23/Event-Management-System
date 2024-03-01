using EMBLL;
using EMENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Event_Management.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// It displays the list of events
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            EventsService es = new EventsService();
            var events = es.GetEventsService();

            return View(events);
        }

        /// <summary>
        /// It creates a form to add a new event. If successful, it displays a confirmation message for the user.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateEventsView()
        {
            return View();
        }

        [HttpPost]

        public ActionResult CreateEventsView(Events newEvent)
        {
            EventsService es = new EventsService();
            if (es.AddEventService(newEvent))
            {
                ViewBag.Message = "The Event is added successfully.";
            }
            return View();
        }

        /// <summary>
        /// It returns a view with a form to update the event information with the record based on EventId.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public ActionResult EditEventsView(int eventId)
        {
            EventsService es = new EventsService();

            return View(es.GetEventsService().Find(x => x.EventId == eventId));
        }

        /// <summary>
        /// This method updates the event. If successful, it displays a confirmation message for the user.
        /// </summary>
        /// <param name="updatedEvent"></param>
        /// <returns></returns>

        [HttpPost]

        public ActionResult EditEventsView(Events updatedEvent)
        {
            EventsService es = new EventsService();

            if (es.UpdateEventService(updatedEvent))
            {
                ViewBag.Message = "The event has been updated successfully.";
            }
            return View();
        }

        /// <summary>
        /// This method deletes an event from the DB based on eventId.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public ActionResult DeleteEvent(int eventId)
        {
            EventsService es = new EventsService();

            if (es.DeleteEventService(eventId))
            {
                ViewBag.Message = "Event is deleted successfully.";
            }
            return RedirectToAction("Index");
        }
    }
}