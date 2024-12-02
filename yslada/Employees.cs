using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace yslada
{
    public partial class Employees : Form
    {
        private string str = ConnectionStr.connectionString();
        private MySqlConnection connection;
        private DataTable table;
        public Employees()
        {
            InitializeComponent();
            FillDGV();
            employeeDGV.MouseDown += EmployeeDGV_MouseDown;
        }
        private void DeleteUser_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранная строка
            if (employeeDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = employeeDGV.SelectedRows[0];
                string userID = selectedRow.Cells["userID"].Value.ToString();

                // Подтверждение удаления
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DeleteUser(userID);
                    MessageBox.Show("Пользователь удалён", "Подтверждение удаления", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDGV(); // Обновить DataGridView
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
        }

        private void DeleteUser(string userID)
        {
            using (connection = new MySqlConnection(str))
            {
                connection.Open();
                string query = "DELETE FROM user WHERE userID = @userID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userID", userID);
                command.ExecuteNonQuery();
            }
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void EmployeeDGV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = employeeDGV.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0) // Убедитесь, что строка выбрана
                {
                    employeeDGV.ClearSelection(); // Очистить выбор
                    employeeDGV.Rows[hit.RowIndex].Selected = true; // Выделить строку

                    ContextMenuStrip contextMenu = new ContextMenuStrip();
                    contextMenu.Items.Add("Удалить", null, DeleteUser_Click);
                    contextMenu.Show(employeeDGV, e.Location);
                }
            }
        }
        private void FillDGV()
        {
            using (connection = new MySqlConnection(str))
            {
                connection.Open();
                string query = @"
            SELECT u.userID,
                u.surname as Фамилия, 
                u.name as Имя, 
                u.patronymic as Отчество, 
                u.number as Номер, 
                u.userLogin as Логин, 
                u.userPasswd as Пароль, 
                r.name as Роль 
            FROM user u
            JOIN role r ON u.role = r.roleID";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);

                employeeDGV.DataSource = table;
                employeeDGV.Columns["userID"].Visible = false;
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Hide();
            using (addUser add = new addUser())
            {
                add.ShowDialog();
                FillDGV();
            }
            Show();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранная строка
            if (employeeDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = employeeDGV.SelectedRows[0];

                // Извлекаем данные из выбранной строки
                string userID = selectedRow.Cells["userID"].Value.ToString();
                string surname = selectedRow.Cells["Фамилия"].Value.ToString();
                string name = selectedRow.Cells["Имя"].Value.ToString();
                string patronymic = selectedRow.Cells["Отчество"].Value.ToString();
                string number = selectedRow.Cells["Номер"].Value.ToString(); // Если номер скрыт, нужно получать его исходное значение
                string login = selectedRow.Cells["Логин"].Value.ToString();
                string passwd = selectedRow.Cells["Пароль"].Value.ToString(); // Если пароль скрыт, нужно получить его настоящее значение из базы или использовать подходящий метод для его обработки
                string roleId = selectedRow.Cells["Роль"].Value.ToString();

                // Открываем форму редактирования с переданными данными
                Hide();
                using (editUser edit = new editUser(userID, surname, name, patronymic, number, login, passwd, roleId))
                {
                    edit.ShowDialog();
                    FillDGV();
                }
                Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для редактирования.");
            }
        }

        private void Employees_Load(object sender, EventArgs e)
        {

        }
    }
}
