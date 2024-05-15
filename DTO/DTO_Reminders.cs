using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Reminders
    {
        // id, name, time
        private int _id;
        private string _name;
        private DateTime _time;
        private int _idAppointment;


        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public DateTime Time { get => _time; set => _time = value; }
        public int IdAppointment { get => _idAppointment; set => _idAppointment = value; }
        public DTO_Reminders() { }
        public DTO_Reminders(int id, string name, DateTime time, int idAppointment)
        {
            this.Id = id;
            this.Name = name;
            this.Time = time;
            this.IdAppointment = idAppointment;
        }
    }

    //Model view of Reminders
    public class DTO_RemindersView
    {
        private int _id;
        private string _name;
        private DateTime _time;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public DateTime Time { get => _time; set => _time = value; }
        public DTO_RemindersView() { }
    }
}
