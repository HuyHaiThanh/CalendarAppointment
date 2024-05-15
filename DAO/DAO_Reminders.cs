using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;


//Sử dụng DBHelper để thực thi các câu lệnh SQL
namespace DAO
{
    public class DAO_Reminders
    {
        private static DAO_Reminders _instance;
        public static DAO_Reminders Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_Reminders();
                }
                return _instance;
            }

        }

        private DAO_Reminders()
        {

        }

        //Chuyển dataRow thành DTO_Reminders
        private DTO_Reminders GetReminders(DataRow i)
        {
            return new DTO_Reminders
            {
                Id = Convert.ToInt32(i["Id"]),
                Name = i["Name"].ToString(),
                Time = DateTime.Parse(i["Time"].ToString()),
                IdAppointment = Convert.ToInt32(i["IdAppointment"])
            };
        }

        //Chuyển nhiều dataRow thành List DTO_Reminders
        private List<DTO_Reminders> GetListReminders(DataTable dt)
        {
            List<DTO_Reminders> listReminders = new List<DTO_Reminders>();
            foreach (DataRow i in dt.Rows)
            {
                listReminders.Add(GetReminders(i));
            }
            return listReminders;
        }

        //Lấy tất cả nhắc nhở
        public List<DTO_Reminders> GetReminders()
        {
            List<DTO_Reminders> listReminders = new List<DTO_Reminders>();
            try
            {
                string query = "SELECT * FROM Reminders";
                DataTable dt = DBHelper.Instance.GetRecords(query, null);
                listReminders = GetListReminders(dt);
                return listReminders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Thêm nhắc nhở
        public int AddReminders(DTO_Reminders reminders)
        {
            try
            {
                string query = "INSERT INTO Reminders VALUES(@name, @time, @idAppointment)";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@name", reminders.Name),
                    new SqlParameter("@time", reminders.Time),
                    new SqlParameter("@idAppointment", reminders.IdAppointment)
                };
                return DBHelper.Instance.ExecuteDB(query, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Sửa nhắc nhở
        public int UpdateReminders(DTO_Reminders reminders)
        {
            try
            {
                string query = "UPDATE Reminders SET Name = @name, Time = @time WHERE Id = @id";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@id", reminders.Id),
                    new SqlParameter("@name", reminders.Name),
                    new SqlParameter("@time", reminders.Time)
                };
                return DBHelper.Instance.ExecuteDB(query, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Xóa nhắc nhở
        public int DeleteReminders(int id)
        {
            try
            {
                string query = "DELETE FROM Reminders WHERE Id = @id";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                return DBHelper.Instance.ExecuteDB(query, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
