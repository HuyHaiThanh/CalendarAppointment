using DAO;
using System.Collections.Generic;
using DTO;


namespace BUS
{
    public class BUS_Reminders
    {
        private static BUS_Reminders _instance;
        public static BUS_Reminders Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BUS_Reminders();
                }
                return _instance;
            }
        }

        private BUS_Reminders()
        {

        }

        //Lấy lời nhắc của appointment có id là idAppointment
        public List<DTO_Reminders> GetReminders(int id)
        {
            List<DTO_Reminders> result = new List<DTO_Reminders>();
            foreach (DTO_Reminders i in DAO_Reminders.Instance.GetReminders())
            {
                if (i.IdAppointment == id)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        //Thêm lời nhắc cho appointment có id là idAppointment
        public int AddReminders(DTO_Reminders reminders)
        {
            return DAO_Reminders.Instance.AddReminders(reminders);
        }

        //Sửa nhắc nhở có id là id
        public int UpdateReminders(DTO_Reminders reminders)
        {
            return DAO_Reminders.Instance.UpdateReminders(reminders);
        }

        //Xóa nhắc nhở có id là id
        public int DeleteReminders(int id)
        {
            return DAO_Reminders.Instance.DeleteReminders(id);
        }
    }
}
