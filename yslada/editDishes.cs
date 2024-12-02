using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace yslada
{
    public partial class editDishes : Form
    {
        private MySqlConnection connection;
        private int menuID;

        public editDishes(int menuID)
        {
            InitializeComponent();
            this.menuID = menuID;
            connection = new MySqlConnection(ConnectionStr.connectionString());

        }

        private void editDishes_Load(object sender, EventArgs e)
        {
            LoadDishData();
            LoadCategories();
            // Подключаем обработчик события для изображения
            dishesPB.Click += DishesPB_Click;
        }

        private void DishesPB_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Хотите изменить изображение?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files (*.jpg)|*.jpg"; // Указываем фильтр для JPG
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dishesPB.Image = Image.FromFile(openFileDialog.FileName); // Устанавливаем выбранное изображение
                    }
                }
            }
        }
        private void LoadDishData()
        {
            try
            {
                connection.Open();
                string query = "SELECT name, description, category, image, cost FROM menu WHERE menuID = @menuID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@menuID", menuID);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    nameTB.Text = reader["name"].ToString();
                    descriptionTB.Text = reader["description"].ToString();
                    costTB.Text = reader["cost"].ToString();
                    string imageFileName = reader["image"].ToString();
                    if (!string.IsNullOrEmpty(imageFileName))
                    {
                        string imgFolderPath = @"..\..\img";
                        string imagePath = Path.Combine(imgFolderPath, imageFileName);
                        if (File.Exists(imagePath))
                        {
                            dishesPB.Image = Image.FromFile(imagePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadCategories()
        {
            try
            {
                connection.Open();
                string query = "SELECT categoryID, name FROM category";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable categoryTable = new DataTable();
                adapter.Fill(categoryTable);

                categoryCB.DisplayMember = "name";
                categoryCB.ValueMember = "categoryID";
                categoryCB.DataSource = categoryTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке категорий: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "UPDATE menu SET name = @name, description = @description, category = @category, image = @image, cost = @cost WHERE menuID = @menuID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@menuID", menuID);
                cmd.Parameters.AddWithValue("@name", nameTB.Text);
                cmd.Parameters.AddWithValue("@description", descriptionTB.Text);
                cmd.Parameters.AddWithValue("@category", categoryCB.SelectedValue);
                cmd.Parameters.AddWithValue("@cost", decimal.Parse(costTB.Text));

                // Сохранение изображения
                if (dishesPB.Image != null)
                {
                    // Генерация уникального имени файла
                    string imageFileName = $"{menuID}.jpg"; // Используйте уникальный идентификатор, например, menuID
                    string imagePath = Path.Combine(@"..\..\img", imageFileName); // Путь к папке для сохранения изображений

                    // Сохранение изображения на диск
                    dishesPB.Image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    cmd.Parameters.AddWithValue("@image", imageFileName); // Сохраняем только имя файла
                }
                else
                {
                    cmd.Parameters.AddWithValue("@image", DBNull.Value);
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("Блюдо успешно обновлено.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dishesPB.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
