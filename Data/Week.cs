namespace TimeSheetApp.Data
{
    
    public class Week
    {
        public DateTime firstDayOfWeek {get; set;}
        public DateTime lastDayOfWeek {get; set;}
        public List<DateTime>? daysInWeek {get; set;}
        public string? firstDayOfWeekMouth {get; set;}
        public string? lastDayOfWeekMouth {get; set;}

        //**** Créer un service qui va me permettre d'update l'instance au complet 
        public Week()
        {
            firstDayOfWeek = GetFirstDayOfWeek(DateTime.Now);
            // Pour avoir le dernier jour de la semaine ajouter 6 jours
            lastDayOfWeek = GetFirstDayOfWeek(DateTime.Now).AddDays(6);
            //Convertion des mois(nombre) en mot pour facilité le l'affichage dans le UI
            firstDayOfWeekMouth = firstDayOfWeek.Month switch
            {
                1 => "Jan",
                2 => "Fev",
                3 => "Mar",
                4 => "Avr",
                5 => "Mai",
                6 => "Jun",
                7 => "Jui",
                8 => "Aou",
                9 => "Sep",
                10 => "Oct",
                11 => "Nov",
                12 => "Dec",
                _ => ""
            };
            lastDayOfWeekMouth = lastDayOfWeek.Month switch
            {
                1 => "Jan",
                2 => "Fev",
                3 => "Mar",
                4 => "Avr",
                5 => "Mai",
                6 => "Jun",
                7 => "Jui",
                8 => "Aou",
                9 => "Sep",
                10 => "Oct",
                11 => "Nov",
                12 => "Dec",
                _ => ""
            };

            daysInWeek = getDaysInWeek(firstDayOfWeek.Day,lastDayOfWeek.Day);
        }

        private static DateTime GetFirstDayOfWeek(DateTime date)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-diff).Date;
        }

        //Creation de la liste de tous les jours contenues dans la semaine courante.
        //Créer un new DateTime object qui part du premier et fini au dernier jour de la semaine. 
        private List<DateTime> getDaysInWeek(int firstDay,int lastDay)
        {
            int year = this.firstDayOfWeek.Year;
            int mouth = this.firstDayOfWeek.Month;
            int day = this.firstDayOfWeek.Day;
            List<DateTime> dayList = new List<DateTime>();
            for (; firstDay <= lastDay; firstDay++)
            {
                dayList.Add(new DateTime(year,mouth,day));
                day++;
            }
            return dayList;
        }

        public void add7Days()
        {
            firstDayOfWeek.AddDays(7);
        }
    }

}

