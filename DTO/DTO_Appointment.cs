using System;

namespace DTO
{
    public class DTO_Appointment
    {
        private int _id;
        private string _name;
        private string _location;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _userId;


        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Location { get => _location; set => _location = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }
        public int UserId { get => _userId; set => _userId = value; }

        public DTO_Appointment()
        {

        }
    }
    //Model view of Appointment
    public class DTO_AppointmentView
    {
        private int _id;
        private string _name;
        private string _location;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _numberOfPaticipants;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Location { get => _location; set => _location = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }
        public int NumberOfPaticipants { get => _numberOfPaticipants; set => _numberOfPaticipants = value; }
        public DTO_AppointmentView() { }
    }
}
