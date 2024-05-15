using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI_NewAppointment : Form
    {
        private int UserId = 0;
        private int IdAppointment = 0;
        private DTO_Appointment appointment;

        public delegate void MyDel(int id, DateTime dateTime);
        public MyDel d;

        public void SetGUI()
        {
            if (IdAppointment == 0)
            {
                this.Text = "Create Appointment";
                this.buttonOK.Text = "Create";
            }
            else
            {
                this.Text = "Edit Appointment";
                this.buttonOK.Text = "Edit";
                //Hiển thị thông tin cuộc hẹn
                appointment = BUS_Appointment.Instance.GetAppointmentById(IdAppointment);
                txtName.Text = appointment.Name;
                txtLocation.Text = appointment.Location;
                dateTimePicker1.Value = appointment.StartTime;
                dateTimePicker2.Value = appointment.EndTime;
                monthCalendar1.SelectionStart = appointment.StartTime;
            }
        }

        public GUI_NewAppointment(int idAppointment, int userId)
        {
            InitializeComponent();
            IdAppointment = idAppointment;
            UserId = userId;
            SetGUI();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Tạo cuộc hẹn dựa trên thuộc tính trên giao diện
            DTO_Appointment newAppointment = new DTO_Appointment
            {
                Id = IdAppointment,
                Name = txtName.Text,
                Location = txtLocation.Text,
                StartTime = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, monthCalendar1.SelectionStart.Day, dateTimePicker1.Value.Hour, dateTimePicker1.Value.Minute, dateTimePicker1.Value.Second),
                EndTime = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, monthCalendar1.SelectionStart.Day, dateTimePicker2.Value.Hour, dateTimePicker2.Value.Minute, dateTimePicker2.Value.Second),
                UserId = UserId
            };
            if (CheckNull())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else if(!BUS_Appointment.Instance.IsValidTime(newAppointment.StartTime, newAppointment.EndTime))
            {
                MessageBox.Show("Thời gian không hợp lệ");
            }
            else
            {
                //Lấy dữ liệu thời gian bận ( trừ thời gian của cuộc hẹn đang sửa )
                List<DateTime> busyTime = BUS_Appointment.Instance.getBusyTime(UserId, monthCalendar1.SelectionStart);
                //Xóa thời gian của cuộc hẹn đang sửa
                if (IdAppointment != 0)
                {
                    DateTime temp = appointment.StartTime;
                    while (temp < appointment.EndTime)
                    {
                        busyTime.Remove(temp);
                        temp = temp.AddHours(1);
                    }
                }

                if (!BUS_Appointment.Instance.IsValidTime(newAppointment.StartTime, newAppointment.EndTime, busyTime))
                {
                    MessageBox.Show("Vui lòng chọn khoảng thời gian rảnh");
                }
                else
                {
                    BUS_Appointment.Instance.AddUpdateAppointment(newAppointment);
                    if(IdAppointment == 0)
                        MessageBox.Show("Tạo cuộc hẹn thành công");
                    else
                        MessageBox.Show("Sửa cuộc hẹn thành công");
                    d(UserId, monthCalendar1.SelectionStart);
                    this.Close();
                }
            }
        }


        //thoát
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //check các thông tin nhập vào có rỗng hay không
        private bool CheckNull()
        {
            if (txtName.Text == "") return true;
            if (txtLocation.Text == "") return true;
            return false;
        }


        //Tô màu xanh ( giống khi click radio button ) cho các button tương ứng với thời gian rảnh ( có màu = bận )
        public void ColorTime()
        {
            //lấy dữ liệu thời gian bận
            List<DateTime> busyTime = BUS_Appointment.Instance.getBusyTime(UserId, monthCalendar1.SelectionStart);
            // Tạo mảng chứa các button
            Button[] buttons = new Button[]
            {
                button00, button01, button02, button03, button04, button05,
                button06, button07, button08, button09, button10, button11,
                button12, button13, button14, button15, button16, button17,
                button18, button19, button20, button21, button22, button23
            };

            // Đặt màu trắng cho tất cả các button
            foreach (Button button in buttons)
            {
                button.BackColor = Color.White;
            }

            // Tô màu xanh cho các mốc thời gian bận trong ngày
            foreach (DateTime item in busyTime)
            {
                int hour = item.Hour;
                if (hour >= 0 && hour <= 23)
                {
                    buttons[hour].BackColor = SystemColors.ControlDark;
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //lấy dữ liệu thời gian rảnh
            ColorTime();
        }
    }
}
