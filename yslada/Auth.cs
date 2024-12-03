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
    public partial class Auth : Form
    {
        private string conStr = ConnectionStr.connectionString();
        public string FIO = "";
        public string Role = "";
        public Auth()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы точно хотите выйти из приложения", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Close();
            }
        }

        private void authBtn_Click(object sender, EventArgs e)
        {
            string login = loginTB.Text;
            string passwd = passwdTB.Text;
            if (Properties.Settings.Default.Login == login && Properties.Settings.Default.Passwd == passwd)
            {
                loginTB.Text = "";
                passwdTB.Text = "";
                Hide();
                using (InportData inport = new InportData())
                {
                    inport.ShowDialog();
                }
                Show();
            }
            else
            {
                string hashPassword = HashPassword(passwd);  // Хешируем введенный пароль
                using (MySqlConnection con = new MySqlConnection(conStr))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT userID, role, surname, name, patronymic, userPasswd FROM user WHERE userLogin = @login;", con);
                    cmd.Parameters.AddWithValue("@login", login); // Избегаем SQL-инъекций
                    try
                    {
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Введен не правильный логин или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                loginTB.Text = "";
                                passwdTB.Text = "";
                                return;
                            }

                            // Сравниваем хеши
                            string storedHash = reader["userPasswd"].ToString();
                            if (storedHash == hashPassword)
                            {
                                FIO = reader["surname"].ToString() + " " + reader["name"].ToString() + " " + reader["patronymic"];
                                Role = reader["role"].ToString();
                                loginTB.Text = "";
                                passwdTB.Text = "";
                                MessageBox.Show("Успешный вход!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Hide();
                                if (Role == "1")
                                {
                                    using (MenuAdmin admin = new MenuAdmin(FIO, Role))
                                    {
                                        admin.SetLBText(FIO);
                                        admin.ShowDialog();
                                    }
                                    Show();
                                }
                                else if (Role == "2")
                                {
                                    using (MenuManager manager = new MenuManager(FIO, Role))
                                    {
                                        manager.SetLBText(FIO);
                                        manager.ShowDialog();
                                    }
                                    Show();
                                }
                                MenuDishes add = new MenuDishes(FIO, Role);
                            }
                            else
                            {
                                MessageBox.Show("Введен не правильный логин или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                loginTB.Text = "";
                                passwdTB.Text = "";
                            }
                        }
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        // Пример функции для хеширования пароля
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
