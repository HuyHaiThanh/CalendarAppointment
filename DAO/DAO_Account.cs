using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;


//Sử dụng DBHelper để thực thi các câu lệnh SQL
namespace DAO
{
    public class DAO_Account
    {
        private static DAO_Account _instance;
        public static DAO_Account Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_Account();
                }
                return _instance;
            }

        }

        private DAO_Account()
        {

        }

        //Chuyển dataRow thành DTO_Account
        private DTO_Account GetAccount(DataRow i)
        {
            return new DTO_Account
            {
                Id = Convert.ToInt32(i["Id"]),
                Username = i["Username"].ToString(),
                Password = i["Password"].ToString(),
                //Appointments = new Dictionary<DateTime, List<DTO_Appointment>>() // eó phải new đâu
            };
        }

        //Chuyển nhiều dataRow thành List DTO_Account
        private List<DTO_Account> GetListAccount(DataTable dt)
        {
            List<DTO_Account> listAccount = new List<DTO_Account>();
            foreach (DataRow i in dt.Rows)
            {
                listAccount.Add(GetAccount(i));
            }
            return listAccount;
        }

        //Lấy tất cả tài khoản
        public List<DTO_Account> GetAccount()
        {
            List<DTO_Account> result = new List<DTO_Account>();
            try
            {
                string query = "SELECT * FROM Account";
                DataTable dt = DBHelper.Instance.GetRecords(query, null);
                result = GetListAccount(dt);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Thêm tài khoản
        public int AddAccount(DTO_Account account)
        {
            try
            {
                string query = "INSERT INTO Account VALUES(@username, @password)";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@username", account.Username),
                    new SqlParameter("@password", account.Password)
                };
                return DBHelper.Instance.ExecuteDB(query, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Sửa tài khoản
        public int UpdateAccount(DTO_Account account)
        {
            try
            {
                string query = "UPDATE Account SET Username = @username, Password = @password WHERE Id = @id";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@id", account.Id),
                    new SqlParameter("@username", account.Username),
                    new SqlParameter("@password", account.Password)
                };
                return DBHelper.Instance.ExecuteDB(query, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Xóa tài khoản
        public int DeleteAccount(int id)
        {
            try
            {
                string query = "DELETE FROM Account WHERE Id = @id";
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

