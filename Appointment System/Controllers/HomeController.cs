using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appointment_System.Models;

namespace Appointment_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Inti() 
        {
            bool rslt = Utils.InitialiseDiary();

            return rslt.ToString();


        }


        public void UpdateEvent(int id, string NewEventStar, string NewEventEnd) 
        {
            DiaryEvent.UpdateDiaryEvent(id, NewEventStar, NewEventEnd);
        }

        public bool SaveEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration) 
        {
            return DiaryEvent.CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventDuration);
        }

        public JsonResult GetSummary(double start, double end) 
        {
            var ApptListForDate = DiaryEvent.LoadAppointmentSummaryInDateRange(start, end);

            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                somekey = e.SomeImportantKeyID,
                                all = false

                            };

            var rows = eventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDiaryEvents(double start, double end)
        {
            var ApptListForDate = DiaryEvent.LoaddAllApointmentInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                                someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }


   }

}