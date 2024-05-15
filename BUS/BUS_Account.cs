using DAO;
using DTO;

namespace BUS
{
    public class BUS_Account
    {
        private static BUS_Account _instance;
        public static BUS_Account Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BUS_Account();
                }
                return _instance;
            }
        }

        private BUS_Account()
        {

        }
        //Lấy id tài khoản theo username
        public int GetIdByUsername(string username)
        {
            foreach (DTO_Account i in DAO_Account.Instance.GetAccount())
            {
                if (i.Username == username)
                {
                    return i.Id;
                }
            }
            return -1;
        }

        //Lấy tài khoản theo id
        public DTO_Account GetAccountById(int id)
        {
            foreach (DTO_Account i in DAO_Account.Instance.GetAccount())
            {
                if (i.Id == id)
                {
                    return i;
                }
            }
            return null;
        }
        //Đăng nhập
        public int Login(string username, string password)
        {
            foreach (DTO_Account i in DAO_Account.Instance.GetAccount())
            {
                //Kiểm tra username
                if (i.Username == username)
                {
                    //Kiểm tra password
                    if (i.Password == password)
                    {
                        return 2; //Đăng nhập thành công
                    }
                    return 1; //Sai password
                }
            }
            return 0; //Không tồn tại username
        }

        //Đăng ký
        public bool Register(DTO_Account account)
        {
            //Kiểm tra username đã tồn tại chưa
            foreach (DTO_Account i in DAO_Account.Instance.GetAccount())
            {
                if (i.Username == account.Username)
                {
                    return false;
                }
            }
            return DAO_Account.Instance.AddAccount(account) > 0;
        }

        //Sửa tài khoản
        public int UpdateAccount(DTO_Account account)
        {
            return DAO_Account.Instance.UpdateAccount(account);
        }

        //Xóa tài khoản
        public int DeleteAccount(int id)
        {
            return DAO_Account.Instance.DeleteAccount(id);
        }
    }
}
