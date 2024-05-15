using System;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class GUI_Account : Form
    {
        public GUI_Account()
        {
            InitializeComponent();
        }

        //Đăng nhập
        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            //Sử dụng hàm đăng nhập
            switch (BUS_Account.Instance.Login(txtUserName.Text, txtPass.Text))
            {
                case 0:
                    MessageBox.Show("Tài khoản không tồn tại");
                    break;
                case 1:
                    MessageBox.Show("Sai mật khẩu");
                    break;
                case 2:
                    MessageBox.Show("Đăng nhập thành công");
                    // Lấy id tài khoản
                    int id = BUS_Account.Instance.GetIdByUsername(txtUserName.Text);
                    // Mở form Appointment
                    GUI_Appointment guiAppointment = new GUI_Appointment(id);
                    // Ẩn form Account
                    this.Hide();
                    guiAppointment.ShowDialog();
                    // Hiện lại form Account
                    this.Show();
                    break;
            }
        }
        //Đăng ký
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //Tạo Account với thông tin từ form
            DTO_Account account = new DTO_Account();
            account.Username = txtUserName.Text;
            account.Password = txtPass.Text;

            //Sử dụng hàm đăng ký
            if (BUS_Account.Instance.Register(account))
            {
                MessageBox.Show("Đăng ký thành công");
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại");
            }
            

        }
        //Nhấn enter để đăng nhập
        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogIn_Click(sender, e);
            }
        }
    }
}
