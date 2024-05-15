using System;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DBHelper
    {
        private readonly SqlConnection _conn;
        private static DBHelper _instance;
        public static DBHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    string s = "Data Source=HUYB;Initial Catalog=CalendarApointment;Integrated Security=True";
                    _instance = new DBHelper(s);
                }
                return _instance;
            }
        }

        private DBHelper(string s)
        {
            _conn = new SqlConnection(s);
        }

        //Sử dụng để thực thi các câu lệnh Select, Insert, Update, Delete
        public int ExecuteDB(string query, SqlParameter[] parameter)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand(query, _conn);
                if (parameter != null)
                {
                    cmd.Parameters.AddRange(parameter);
                }
                int result = cmd.ExecuteNonQuery();
                _conn.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Sử dụng để lấy dữ liệu từ DB
        public DataTable GetRecords(string query, SqlParameter[] parameter)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand(query, _conn);
                if (parameter != null)
                {
                    cmd.Parameters.AddRange(parameter);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                _conn.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Sử dụng để lấy dữ liệu từ DB
        public DataTable GetRecords(string query)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand(query, _conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                _conn.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
