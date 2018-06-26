using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointment_System.Models
{
    public class Utils
    {

        public static bool InitialiseDiary()
        {
            DiaryContainer ent = new DiaryContainer();

            try
            {
                for(int i = 0; i < 30; i ++)
                {
                    AppointmentDiary item = new AppointmentDiary();

                    //record ID is auto generated
                    item.Title = "Appt : " + i.ToString();
                    item.SomeImportantKey = i;
                    item.StatuENUM = GetRandomValue(0,3); //radom is exclusive - we have three status enums

                    if(i <= 5) //create ten appointments for todays date
                    {
                        item.DateTimeSchedule = GetRandomAppointmentTime(false, true);
                    
                    }
                    else
                    {
                        //rest of appointments on previous and future dates

                        if(i % 2 == 0)
                        {
                            item.DateTimeSchedule = GetRandomAppointmentTime(true,false);

                            //flip/flop between date ahead of today and behind today
                        }
                        else
                        {
                            item.DateTimeSchedule = GetRandomAppointmentTime(false, false);
                        }

                        item.AppointmentLength = GetRandomValue(1,5) * 15;

                        //appointment length always less than an hour in blocks of fifteen

                        ent.AppointmentDiaries.Add(item);
                        ent.SaveChanges();

                    }

                }
            }
            catch(Exception)
            {
                return false;
            }

            return ent.AppointmentDiaries.Count() > 0;
        }



        public static int GetRandomValue(int LowerBound, int UpperBound)
        {
            Random rnd = new Random();

            return rnd.Next(LowerBound, UpperBound);
        }

        public static DateTime GetRandomAppointmentTime(bool GoBackwards, bool Today)
        {
            Random rnd = new Random(Environment.TickCount);

            var baseDate = DateTime.Today;

            if (Today)
            {
                return new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, rnd.Next(9, 18), rnd.Next(1, 6) * 5, 0);
            }
            else
            {
                int rndDays = rnd.Next(1, 15);

                if (GoBackwards)
                
                    rndDays = rndDays * -1;

                    return new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, rnd.Next(9, 18), rnd.Next(1, 6) * 5, 0).AddDays(rndDays);
                
            }
        }


    }
}