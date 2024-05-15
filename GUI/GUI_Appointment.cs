using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI_Appointment : Form
    {
        private readonly int userId;

        public GUI_Appointment(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        //Thêm cuộc hẹn
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_NewAppointment createAppointment = new GUI_NewAppointment(0, userId);
            createAppointment.d += new GUI_NewAppointment.MyDel(ShowDGV);
            createAppointment.ShowDialog();
        }
        //Sửa cuộc hẹn
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Chỉ chọn 1 hàng
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Chọn 1 hàng để sửa");
                return;
            }
            //Lấy id của cuộc hẹn được chọn
            int appointmentId = int.Parse(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString());
            GUI_NewAppointment editAppointment = new GUI_NewAppointment(appointmentId, userId);
            editAppointment.d += new GUI_NewAppointment.MyDel(ShowDGV);
            editAppointment.ShowDialog();

        }

        //Reload lại datagridview1
        private void ShowDGV(int userId, DateTime dateTime)
        {
            List<DTO_Appointment> list = BUS_Appointment.Instance.GetAppointment(userId, dateTime);
            List<DTO_AppointmentView> listView = list.Select(i => new DTO_AppointmentView
            {
                Id = i.Id,
                Name = i.Name,
                Location = i.Location,
                StartTime = i.StartTime,
                EndTime = i.EndTime,
                NumberOfPaticipants = BUS_Appointment.Instance.GetListPaticipants(i.Id).Count

            }).ToList();
            dataGridView1.DataSource = listView;
            if (listView.Count > 0)
            {
                dataGridView1.Columns["StartTime"].DefaultCellStyle.Format = "HH:mm";
                dataGridView1.Columns["EndTime"].DefaultCellStyle.Format = "HH:mm";
            }
        }

        //Show tất cả cuộc hẹn
        private void ShowAllDGV(int userId)
        {
            List<DTO_Appointment> list = BUS_Appointment.Instance.GetAppointment(userId);
            List<DTO_AppointmentView> listView = list.Select(i => new DTO_AppointmentView
            {
                Id = i.Id,
                Name = i.Name,
                Location = i.Location,
                StartTime = i.StartTime,
                EndTime = i.EndTime,
                NumberOfPaticipants = BUS_Appointment.Instance.GetListPaticipants(i.Id).Count

            }).ToList();
            dataGridView1.DataSource = listView;
            if (listView.Count > 0)
            {
                dataGridView1.Columns["StartTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dataGridView1.Columns["EndTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }


        //Hiển thị cuộc hẹn theo ngày
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            ShowDGV(userId, monthCalendar1.SelectionStart);
            //Xóa dữ liệu dataGridView2
            dataGridView2.DataSource = null;
            //Xóa dữ liệu textbox và datetimepicker
            textBox1.Text = "";
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            ShowAllDGV(userId);
        }

        //Sự kiện xảy ra khi chọn một cuộc hẹn từ datagridview1
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Nếu không chọn hàng nào thì không làm gì cả
            if (e.RowIndex == -1)
            {
                return;
            }
            //Lấy id của cuộc hẹn được chọn
            int appointmentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            //Lấy dữ liệu nhắc nhở bỏ vào dataGridView2

            List<DTO_Reminders> list = BUS_Reminders.Instance.GetReminders(appointmentId);
            List<DTO_RemindersView> listView = list.Select(i => new DTO_RemindersView
            {
                Id = i.Id,
                Name = i.Name,
                Time = i.Time
            }).ToList();

            dataGridView2.DataSource = listView;
            if (listView.Count > 0)
            {
                dataGridView2.Columns["Time"].DefaultCellStyle.Format = "HH:mm";
            }
            //Xóa dữ liệu textbox và datetimepicker
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        //Xóa cuộc hẹn
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chọn 1 hàng để xóa");
                return;
            }
            //Lấy list cuộc hẹn được chọn
            List<int> listId = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                listId.Add(Convert.ToInt32(row.Cells["Id"].Value.ToString()));
            }
            //Hiển thị thông báo xác nhận xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa " + listId.Count + " cuộc hẹn?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //Xóa cuộc hẹn
                foreach (int id in listId)
                {
                    BUS_Appointment.Instance.DeleteAppointment(id);
                }
                //Hiển thị lại cuộc hẹn
                buttonShowAll.PerformClick();
            }
        }
        
        //Thêm sủa xóa trực tiếp trên datagridview2

        //Sự kiện chọn 1 cột
        //Đưa dữ liệu qua textbox và datetimepicker
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Nếu không chọn hàng nào thì không làm gì cả
            if (e.RowIndex == -1)
            {
                return;
            }
            //Lấy dữ liệu từ datagridview2
            string name = dataGridView2.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            DateTime time = DateTime.Parse(dataGridView2.Rows[e.RowIndex].Cells["Time"].Value.ToString());
            //Đưa dữ liệu vào textbox và datetimepicker
            textBox1.Text = name;
            dateTimePicker1.Value = time;
        }

        //Sự kiện xảy ra khi người dùng xóa hàng trong datagridview2
        private void dataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //Lấy id của cuộc hẹn được chọn
            int appointmentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString());
            //Lấy id của nhắc nhở được chọn
            int remindersId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Id"].Value.ToString());
            //Xóa nhắc nhở
            BUS_Reminders.Instance.DeleteReminders(remindersId);
            //Hiển thị lại nhắc nhở bằng cách sử dụng sự kiện dataGridView1_CellClick
            dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(0, dataGridView1.SelectedRows[0].Index));
        }

        //Lấy dữ liệu từ textbox và datetimepicker bỏ vào database và datagridview2
        private void button2_Click(object sender, EventArgs e)
        {
            //Nếu nhiều hơn 1 hàng được chọn thì thông báo
            if (dataGridView2.SelectedRows.Count > 1)
            {
                MessageBox.Show("Chỉ chọn 1 hàng để thêm/sửa");
                return;
            }
            //Nếu chưa chọn cuộc hẹn 
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn 1 cuộc hẹn để thêm nhắc nhở");
                return;
            }
            //Nếu tên nhắc nhở trống
            if (textBox1.Text == "")
            {
                MessageBox.Show("Nhập tên nhắc nhở");
                return;
            }
            //Lấy id của cuộc hẹn được chọn
            int appointmentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString());
            //Nếu hàng được chọn rỗng thì tạo nhắc nhở mới
            if (dataGridView2.SelectedRows.Count == 0 || dataGridView2.SelectedRows[0].Cells["Id"].Value == null)
            {
                //Tạo mới một nhắc nhở
                DTO_Reminders reminders = new DTO_Reminders
                {
                    Name = textBox1.Text,
                    Time = dateTimePicker1.Value,
                    IdAppointment = appointmentId
                };
                //Thêm nhắc nhở vào database
                if (BUS_Reminders.Instance.AddReminders(reminders) > 0) MessageBox.Show("Thêm nhắc nhở thành công");
                else MessageBox.Show("Thêm nhắc nhở thất bại");
            }
            //Nếu hàng được chọn không rỗng thì cập nhật nhắc nhở
            else
            {
                //Tạo mới một nhắc nhở
                DTO_Reminders reminders = new DTO_Reminders
                {
                    Id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Id"].Value.ToString()),
                    Name = textBox1.Text,
                    Time = dateTimePicker1.Value,
                    IdAppointment = appointmentId
                };
                //Thêm nhắc nhở vào database
                if (BUS_Reminders.Instance.UpdateReminders(reminders) > 0)
                {
                    MessageBox.Show("Cập nhật nhắc nhở thành công");
                    //Xóa dữ liệu textbox và datetimepicker
                    textBox1.Text = "";
                    //Chỉnh lại 12h
                    DateTime New = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 12, 0, 0);
                }
                else MessageBox.Show("Cập nhật nhắc nhở thất bại");
            }
            //Hiển thị lại nhắc nhở
            dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(0, dataGridView1.SelectedRows[0].Index));
        }
        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải đang trỏ vào cột NumberOfParticipants không
            if (e.ColumnIndex == dataGridView1.Columns["NumberOfPaticipants"].Index && e.RowIndex >= 0)
            {
                // Lấy danh sách tên người tham gia từ dữ liệu của hàng tương ứng
                string participantsNames = GetParticipantsNames(e.RowIndex);
                // Hiển thị tooltip với tên người tham gia
                toolTip1.SetToolTip(dataGridView1, participantsNames);
            }
            else
            {
                // Ẩn tooltip nếu không phải cột NumberOfParticipants
                toolTip1.SetToolTip(dataGridView1, string.Empty);
            }
        }

        private string GetParticipantsNames(int rowIndex)
        {
            // Lấy id của cuộc hẹn từ dữ liệu của hàng tương ứng
            int appointmentId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value.ToString());
            List<String> paticipants = BUS_Appointment.Instance.GetListPaticipants(appointmentId);
            return string.Join("\n", paticipants);
        }
    }
}
