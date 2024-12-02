using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace yslada
{
    public partial class editUser : Form
    {
        private string connectionString = ConnectionStr.connectionString();
        public string ID = "";
        public string Surname = "";
        public string name = "";
        public string Patronymic = ""; 
        public string Login = "";
        public string Passwd = "";
        public string Number = "";
        public string role = "";
        public editUser(string userID, string surname, string name, string patronymic, string number, string login, string passwd, string roleID)
        {
            InitializeComponent();
            roleCB.KeyPress += (sender, e) => e.Handled = true;

            // Передаем значения в поля
            ID = userID;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Number = number;
            Login = login;
            Passwd = passwd;
            role = roleID;

            // Заполняем текстовые поля формы
            surnameTB.Text = Surname;
            nameTB.Text = Name;
            patronymicTB.Text = Patronymic;
            numberTB.Text = Number;
            loginTB.Text = Login;
            passwdTB.Text = Passwd; // Не показывать, если это небезопасно

            // Загрузка ролей и установка выбранного значения
            LoadRoles(); // Устанавливаем значение роли
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Преобразование в шестнадцатеричный формат
                }
                return builder.ToString();
            }
        }
        private void TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;

            }
        }
        private void LoadRoles()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT roleID, name FROM role";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable roleTable = new DataTable();
                    adapter.Fill(roleTable);

                    roleCB.DataSource = roleTable;
                    roleCB.DisplayMember = "name"; // Имя роли
                    roleCB.ValueMember = "roleID"; // ID роли
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке ролей: " + ex.Message);
                }
            }
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(ID);
            // Получение новых значений из текстовых полей
            string updatedSurname = surnameTB.Text.Trim();
            string updatedName = nameTB.Text.Trim();
            string updatedPatronymic = patronymicTB.Text.Trim();
            string updatedNumber = numberTB.Text.Trim();
            string updatedLogin = loginTB.Text.Trim();
            string updatedPasswd = passwdTB.Text.Trim();
            string selectedRoleID = roleCB.SelectedValue.ToString(); // Получаем выбранный ID роли

            // Проверка на заполненность обязательных полей
            if (string.IsNullOrWhiteSpace(updatedSurname) ||
                string.IsNullOrWhiteSpace(updatedName) ||
                string.IsNullOrWhiteSpace(updatedLogin))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            // Проверка номера телефона
            if (updatedNumber.Length < 11)
            {
                MessageBox.Show("Номер должен содержать ровно 11 цифр.");
                return;
            }

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
            UPDATE user 
            SET surname = @surname, 
                name = @name, 
                patronymic = @patronymic, 
                number = @number, 
                userLogin = @userLogin, 
                userPasswd = @userPasswd, 
                role = @roleID 
            WHERE userID = @userID"; // Используем UserID как уникальный идентификатор

                    MySqlCommand command = new MySqlCommand(query, connection);
                    // Используем параметры для предотвращения SQL-инъекций
                    command.Parameters.AddWithValue("@surname", updatedSurname);
                    command.Parameters.AddWithValue("@name", updatedName);
                    command.Parameters.AddWithValue("@patronymic", updatedPatronymic);
                    command.Parameters.AddWithValue("@number", updatedNumber);
                    command.Parameters.AddWithValue("@userLogin", updatedLogin);

                    // Хешируем пароль только если он изменен
                    if (!string.IsNullOrWhiteSpace(updatedPasswd))
                    {
                        string hashedPassword = HashPassword(updatedPasswd);
                        command.Parameters.AddWithValue("@userPasswd", hashedPassword); // Хешируем новый пароль
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@userPasswd", null); // Или используйте старый пароль
                    }

                    command.Parameters.AddWithValue("@roleID", selectedRoleID);
                    command.Parameters.AddWithValue("@userID", UserID); // Здесь UserID - это уникальный идентификатор пользователя

                    command.ExecuteNonQuery(); // Выполняем запрос обновления

                    MessageBox.Show("Данные пользователя успешно обновлены.");
                    Close(); // Закрываем форму после успешного обновления
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении пользователя: " + ex.Message);
                }
            }
        }
    }
}
