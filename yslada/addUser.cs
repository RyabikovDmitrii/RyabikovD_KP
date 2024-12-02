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
    public partial class addUser : Form
    {
        private string connectionString = ConnectionStr.connectionString();
        public addUser()
        {
            InitializeComponent();
            LoadRoles();
            roleCB.KeyPress += (sender, e) => e.Handled = true;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
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
        private void AddEmployee()
        {
            if (string.IsNullOrWhiteSpace(surnameTB.Text) || string.IsNullOrWhiteSpace(nameTB.Text) || string.IsNullOrWhiteSpace(patronymicTB.Text) || string.IsNullOrWhiteSpace(loginTB.Text) || string.IsNullOrWhiteSpace(passwdTB.Text) || string.IsNullOrWhiteSpace(numberTB.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            if (numberTB.Text.Length < 12)
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
                        INSERT INTO user (surname, name, patronymic, number, userLogin, userPasswd, role) 
                        VALUES (@surname, @name, @patronymic, @number, @userLogin, @userPasswd, @roleID)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@surname", surnameTB.Text);
                    command.Parameters.AddWithValue("@name", nameTB.Text);
                    command.Parameters.AddWithValue("@patronymic", patronymicTB.Text);
                    command.Parameters.AddWithValue("@number", numberTB.Text); // Убедитесь, что у вас есть numberTB для номера
                    command.Parameters.AddWithValue("@userLogin", loginTB.Text);
                    // Хешируем пароль
                    string hashedPassword = HashPassword(passwdTB.Text);
                    command.Parameters.AddWithValue("@userPasswd", hashedPassword);
                    command.Parameters.AddWithValue("@roleID", roleCB.SelectedValue); // ID выбранной роли

                    command.ExecuteNonQuery(); // Выполнение запроса  на вставку

                    MessageBox.Show("Пользователь добавлен успешно.");
                    Close(); // Закрываем форму после успешного добавления
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении пользователя: " + ex.Message);
                }
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Хешируем пароль в массив байт
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Преобразуем массив байт в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Переводим в шестнадцатеричный вид
                }

                return builder.ToString();
            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            AddEmployee();
        }

        private void TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
