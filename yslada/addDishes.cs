using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace yslada
{
    public partial class addDishes : Form
    {
        private string selectedImagePath;
        private string connectionString = ConnectionStr.connectionString();
        public addDishes()
        {
            InitializeComponent();
            LoadRoles();
            dishesPB.Click += new EventHandler(dishesPB_Click);
        }
        private void LoadRoles()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT categoryID, name FROM category";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable roleTable = new DataTable();
                    adapter.Fill(roleTable);

                    categoryCB.DataSource = roleTable;
                    categoryCB.DisplayMember = "name"; // Имя роли
                    categoryCB.ValueMember = "categoryID"; // ID роли
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
        private void dishesPB_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите вставить картинку?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "C:\\";
                    openFileDialog.Filter = "Image files (*.jpg,)|*.jpg;";
                    openFileDialog.Title = "Выберите изображение";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Загружаем изображение и отображаем его в PictureBox
                            selectedImagePath = openFileDialog.FileName; // Сохраняем путь к выбранному изображению
                            Image selectedImage = Image.FromFile(selectedImagePath);
                            dishesPB.Image = selectedImage;

                            // Optionally: устанавливаем размер изображения
                            dishesPB.SizeMode = PictureBoxSizeMode.StretchImage; // Растягиваем изображение по размеру PictureBox
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                        }
                    }
                }
            }
        }

    
        private void addBtn_Click(object sender, EventArgs e)
        {
            // Проверка на корректность заполнения полей
            if (string.IsNullOrEmpty(nameTB.Text) || string.IsNullOrEmpty(descriptionTB.Text) ||
                string.IsNullOrEmpty(costTB.Text) || categoryCB.SelectedValue == null ||
                dishesPB.Image == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите изображение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Собираем данные
            string name = nameTB.Text;
            string description = descriptionTB.Text;
            int category = (int)categoryCB.SelectedValue; // ID категории
            string imageFilePath = GetImageFilePath(); // Получили полный путь к изображению
            int cost;

            // Проверка корректности стоимости
            if (!int.TryParse(costTB.Text, out cost))
            {
                MessageBox.Show("Пожалуйста, введите корректную стоимость.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Генерация хеша для имени файла
            string imageHash = GenerateHash(Path.GetFileName(imageFilePath)) + Path.GetExtension(imageFilePath);
            string destinationPath = Path.Combine("img", imageHash); // Путь к папке img

            // Копирование изображения в папку img
            try
            {
                // Убедитесь, что папка img существует
                if (!Directory.Exists("img"))
                {
                    Directory.CreateDirectory("img");
                }

                // Копируем изображение
                File.Copy(imageFilePath, destinationPath, true); // true - перезаписать, если файл уже существует
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при копировании изображения: " + ex.Message);
                return; // Прерываем выполнение, если произошла ошибка
            }

            // Сохранение данных в базу данных
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO menu (name, description, category, image, cost) VALUES (@name, @description, @category, @image, @cost)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@image", imageHash); // Сохраняем только имя файла
                    command.Parameters.AddWithValue("@cost", cost);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Блюдо успешно добавлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Очистка полей после добавления
                    ClearFields();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении блюда: " + ex.Message);
                }
            }
        }

        // Метод для получения полного пути к изображению
        private string GetImageFilePath()
        {
            return selectedImagePath; // Возвращаем сохранённый путь к изображению
        }

        // Метод для генерации хеша
        private string GenerateHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Преобразуем массив байтов в строку хеша
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Преобразование байта в шестнадцатеричное строковое представление
                }
                return builder.ToString();
            }
        }

        // Метод для очистки полей ввода
        private void ClearFields()
        {
            nameTB.Clear();
            descriptionTB.Clear();
            costTB.Clear();
            categoryCB.SelectedIndex = -1; // Сброс категории
            dishesPB.Image = null; // Очистка изображения
        }
    }
}
