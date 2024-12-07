using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yslada
{
    public partial class employeeFullData : Form
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
        public employeeFullData(string userID, string surname, string name, string patronymic, string number, string login, string passwd, string roleID)
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

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
