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
        private string text = String.Empty;
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
         private void CAPTCHA()
        {
            Size = new Size(707, 421);
            loginTB.Enabled = false;
            passwdTB.Enabled = false;
            authBtn.Enabled = false;
        }
        private async void checkBtn_Click(object sender, EventArgs e)
        {
            if (fieldTB.Text == this.text)
            {
                MessageBox.Show("Верно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loginTB.Enabled = true;
                passwdTB.Enabled = true;
                authBtn.Enabled = true;
                Size = new Size(323, 421);
            }
            else
            {
                fieldTB.Text = "";
                //Предложить пользователю переход на форму настройки 
                MessageBox.Show("Ошибка ввода капчи. Блокировка ввода на 20 сек.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updateBtn.Enabled = false;
                checkBtn.Enabled = false;
                fieldTB.Enabled = false;
                await Task.Delay(10000);
                updateBtn.Enabled = true;
                checkBtn.Enabled = true;
                fieldTB.Enabled = true;

            }
        }
        private void updateBtn_Click(object sender, EventArgs e)
        {
            captchaPB.Image = this.CreateImage(captchaPB.Width, captchaPB.Height);
        }
        public Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            Bitmap result = new Bitmap(Width, Height);

            int Xpos = rnd.Next(0, Width - 100);
            int Ypos = rnd.Next(15, Height - 50);

            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.DarkRed,
                     Brushes.Green };

            Graphics g = Graphics.FromImage((Image)result);

            g.Clear(Color.Gray);

            text = String.Empty;
            string ALF = "!@#$%^&*()=+1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            g.DrawString(text,
                         new Font("Arial", 28, FontStyle.Strikeout),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));

            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
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
                                CAPTCHA();
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
