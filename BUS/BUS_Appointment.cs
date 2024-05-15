using System;
using System.Collections.Generic;
using System.Linq;
using DAO;
using DTO;

namespace BUS
{
    public class BUS_Appointment
    {

        private static BUS_Appointment _instance;
        public static BUS_Appointment Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BUS_Appointment();
                }
                return _instance;
            }
        }
        public BUS_Appointment()
        {

        }

        //Lấy thông tin lịch hẹn của người dùng có id = userId
        public List<DTO_Appointment> GetAppointment(int userId)
        {
            List<DTO_Appointment> result = new List<DTO_Appointment>();
            foreach (DTO_Appointment i in DAO_Appointment.Instance.GetAppointment())
            {
                if (i.UserId == userId)
                {
                    result.Add(i);
                }
            }
            return result;
        }
        //Lấy thông tin lịch hẹn của người dùng có id = userId theo ngày
        public List<DTO_Appointment> GetAppointment(int userId, DateTime date)
        {
            List<DTO_Appointment> result = new List<DTO_Appointment>();
            foreach (DTO_Appointment i in DAO_Appointment.Instance.GetAppointment())
            {
                if (i.UserId == userId && i.StartTime.Date == date.Date)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        //Lấy thông tin lịch hẹn theo id
        public DTO_Appointment GetAppointmentById(int id)
        {
           
            foreach (DTO_Appointment i in DAO_Appointment.Instance.GetAppointment())
            {
                if (i.Id == id)
                {
                    return i;
                }
            }
            return null;
        }

        //Thêm hoặc sửa
        public int AddUpdateAppointment(DTO_Appointment appointment)
        {
            if (appointment.Id == 0)
            {
                return DAO_Appointment.Instance.AddAppointment(appointment);
            }
            else
            {
                return DAO_Appointment.Instance.UpdateAppointment(appointment);
            }
        }

        //Xóa cuộc hẹn
        public int DeleteAppointment(int id)
        {
            return DAO_Appointment.Instance.DeleteAppointment(id);
        }

        // Lấy các mốc thời gian bận của người dùng theo ngày
        public List<DateTime> getBusyTime(int userId, DateTime date)
        {
            List<DateTime> result = new List<DateTime>();
            foreach (var item in BUS_Appointment.Instance.GetAppointment(userId))
            {
                //nếu thời gian cùng với thời gian được chọn trên monthcalendar thì thêm vào list
                if (item.StartTime.Date == date.Date)
                {
                    DateTime temp = item.StartTime;
                    while (temp < item.EndTime)
                    {
                        result.Add(temp);
                        temp = temp.AddHours(1);
                    }
                }
            }
            return result;
        }

        //Kiểm tra thời gian có hợp lệ hay không
        public bool IsValidTime(DateTime startTime, DateTime endTime, List<DateTime> listTime)
        {
            return !listTime.Any(x => (startTime >= x && startTime < x.AddHours(1)) || (endTime > x && endTime <= x.AddHours(1)));
        }

        //Kiểm tra thời gian có hợp lệ hay không
        public bool IsValidTime(DateTime startTime, DateTime endTime)
        {
            return startTime < endTime;
        }

        //Lấy danh sách tên các người tham gia cuộc hẹn
        public List<string> GetListPaticipants(int id)
        {
            List<string> result = new List<string>();
            foreach (DTO_Appointment i in DAO_Appointment.Instance.GetAppointment())
            {
                if (i.Id == id)
                {
                    //Lấy id của người tham gia
                    int idUser = i.UserId;
                    //Lấy tên người tham gia
                    string name = BUS_Account.Instance.GetAccountById(idUser).Username;
                    result.Add(name);
                }
            }
            return result;
        }
    }
}
