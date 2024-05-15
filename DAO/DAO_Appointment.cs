using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_Appointment
    {
        private static DAO_Appointment _instance;
        public static DAO_Appointment Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_Appointment();
                }
                return _instance;
            }
        }

        private DAO_Appointment()
        {

        }

        //Chuyển dataRow thành DTO_Appointment
        private DTO_Appointment GetAppointment(DataRow i)
        {
            return new DTO_Appointment
            {
                Id = Convert.ToInt32(i["Id"]),
                Name = i["Name"].ToString(),
                Location = i["Location"].ToString(),
                StartTime = DateTime.Parse(i["Start Time"].ToString()),
                EndTime = DateTime.Parse(i["End Time"].ToString()),
                UserId = Convert.ToInt32(i["UserId"])
            };
        }

        //Chuyển nhiều dataRow thành List DTO_Appointment
        private List<DTO_Appointment> GetListAppointment(DataTable dt)
        {
            List<DTO_Appointment> listAppointment = new List<DTO_Appointment>();
            foreach (DataRow i in dt.Rows)
            {
                listAppointment.Add(GetAppointment(i));
            }
            return listAppointment;
        }

        //Lấy tất cả appointment
        public List<DTO_Appointment> GetAppointment()
        {
            List<DTO_Appointment> listAppointment = new List<DTO_Appointment>();
            try
            {
                string sql = "SELECT * FROM Appointment";
                DataTable dt = DBHelper.Instance.GetRecords(sql);
                listAppointment = GetListAppointment(dt);
                return listAppointment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Thêm appointment
        public int AddAppointment(DTO_Appointment appointment)
        {
            try
            {
                string sql = "INSERT INTO Appointment VALUES(@name, @location, @starttime, @endtime, @userId)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", appointment.Name),
                    new SqlParameter("@location", appointment.Location),
                    new SqlParameter("@starttime", appointment.StartTime),
                    new SqlParameter("@endtime", appointment.EndTime),
                    new SqlParameter("@userId", appointment.UserId)
                };
                return DBHelper.Instance.ExecuteDB(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Sửa appointment
        public int UpdateAppointment(DTO_Appointment appointment)
        {
            try
            {
                string sql = "UPDATE Appointment SET Name = @name, Location = @location, [Start Time] = @starttime, [End Time] = @endtime WHERE Id = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", appointment.Id),
                    new SqlParameter("@name", appointment.Name),
                    new SqlParameter("@location", appointment.Location),
                    new SqlParameter("@starttime", appointment.StartTime),
                    new SqlParameter("@endtime", appointment.EndTime)
                };
                return DBHelper.Instance.ExecuteDB(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Xóa appointment
        public int DeleteAppointment(int id)
        {
            try
            {
                string sql = "DELETE FROM Appointment WHERE Id = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };
                return DBHelper.Instance.ExecuteDB(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
