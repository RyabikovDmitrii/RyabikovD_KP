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
        private ContextMenuStrip contextMenuStrip;
        private string str = ConnectionStr.connectionString();
        private MySqlConnection connection;
        private DataTable table;
        private object _Click;

        public Employees()
        {
            InitializeComponent();
            FillDGV();
            employeeDGV.MouseDown += EmployeeDGV_MouseDown;
            employeeDGV.ContextMenuStrip = contextMenuStrip;
            //contextMenuStrip = new ContextMenuStrip();

            //ToolStripMenuItem editDishMenuItem = new ToolStripMenuItem("Редактировать блюдо");
            //editDishMenuItem.Click += ViewFullData_Click;
            //contextMenuStrip.Items.Add(editDishMenuItem);
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
                    contextMenu.Items.Add("Полные данные о пользователе", null, ViewFullData_Click);
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
        
        private void EmployeeDGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Проверяем, что мы находимся в нужных столбцах
            if (employeeDGV.Columns[e.ColumnIndex].Name == "Фамилия" ||
                employeeDGV.Columns[e.ColumnIndex].Name == "Имя" ||
                employeeDGV.Columns[e.ColumnIndex].Name == "Отчество")
            {
                // Получаем текущее значение ячейки
                string originalValue = e.Value as string;

                // Проверяем, что значение не null
                if (!string.IsNullOrEmpty(originalValue))
                {
                    // Определяем количество символов для маскировки (вторая половина)
                    int halfLength = originalValue.Length / 2;
                    string visiblePart = originalValue.Substring(0, originalValue.Length - halfLength); // Оставляем видимой первую половину
                    string maskedPart = new string('*', halfLength); // Создаем строку из символов '*'

                    // Обновляем значение ячейки
                    e.Value = visiblePart + maskedPart;
                    e.FormattingApplied = true; // Указываем, что форматирование было применено
                }
            }
            else if (employeeDGV.Columns[e.ColumnIndex].Name == "Номер" ||
                     employeeDGV.Columns[e.ColumnIndex].Name == "Логин" ||
                     employeeDGV.Columns[e.ColumnIndex].Name == "Пароль" ||
                     employeeDGV.Columns[e.ColumnIndex].Name == "Роль")
            {
                // Для этих столбцов полностью скрываем значение
                e.Value = new string('*', 8); // Замените 8 на желаемое количество символов
                e.FormattingApplied = true; // Указываем, что форматирование было применено
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
        private void ViewFullData_Click(object sender, EventArgs e)
        {
            // Проверяем, что есть выбранная строка
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

                Hide();
                using (employeeFullData fullDataForm = new employeeFullData(userID, surname, name, patronymic, number, login, passwd, roleId))
                {
                    fullDataForm.ShowDialog(); // Используем ShowDialog для модального окна
                }
                Show();   
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для просмотра полной информации.");
            }
        }
        private void Employees_Load(object sender, EventArgs e)
        {

        }
    }
}
