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
    public partial class MenuDishes : Form
    {
        private ToolStripMenuItem addNewDishMenuItem;
        private string FIO = "";
        private int orderCount = 0;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem addToOrderMenuItem;
        private string str = ConnectionStr.connectionString();
        private MySqlConnection connection;
        private DataTable table;
        private Button orderButton;
        private string nameDishes = "";
        private int costDishes = 0;
        private AddOrder orderForm;
        private bool isOrderFormOpen = false;
        public string Role = "";
        private int categoryId;
        private string query = "SELECT m.menuID as Номер_Блюда, m.name AS Название, m.description AS Описание, c.name AS Категория, m.image AS Изображение, m.cost AS Цена FROM menu m JOIN category c ON m.category = c.categoryID WHERE m.name LIKE @name";
        public MenuDishes(string fio, string role)
        {

            InitializeComponent();
            InitializeComboBox();
            InitializeOrderButton();
            InitializeFilterCB();
            LoadRowCount();
            FIO += fio;
            Role += role;
            connection = new MySqlConnection(str);
            SortCB.SelectedIndexChanged += SortCB_SelectedIndexChanged;
            contextMenuStrip = new ContextMenuStrip();
            addToOrderMenuItem = new ToolStripMenuItem("Добавить в заказ");
            addToOrderMenuItem.Click += AddToOrderMenuItem_Click;
            contextMenuStrip.Items.Add(addToOrderMenuItem);
            FilterCB.KeyPress += (sender, e) => e.Handled = true;
            SortCB.KeyPress += (sender, e) => e.Handled = true;
            DishesDGW.MouseClick += DishesDGW_MouseClick;
            orderForm = new AddOrder(FIO);

            contextMenuStrip = new ContextMenuStrip();

            addToOrderMenuItem = new ToolStripMenuItem("Добавить в заказ");
            addToOrderMenuItem.Click += AddToOrderMenuItem_Click;
            contextMenuStrip.Items.Add(addToOrderMenuItem);
            if (Role == "1")
            {
                addNewDishMenuItem = new ToolStripMenuItem("Добавить новое блюдо");
                addNewDishMenuItem.Click += AddNewDishMenuItem_Click;
                contextMenuStrip.Items.Add(addNewDishMenuItem);
                DishesDGW.ContextMenuStrip = contextMenuStrip;

                ToolStripMenuItem editDishMenuItem = new ToolStripMenuItem("Редактировать блюдо");
                editDishMenuItem.Click += EditDishMenuItem_Click;
                contextMenuStrip.Items.Add(editDishMenuItem);

                // Add the delete menu item
                ToolStripMenuItem deleteDishMenuItem = new ToolStripMenuItem("Удалить блюдо");
                deleteDishMenuItem.Click += DeleteDishMenuItem_Click;
                contextMenuStrip.Items.Add(deleteDishMenuItem);

                DishesDGW.ContextMenuStrip = contextMenuStrip;
            }
        }
        private void LoadRowCount()
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM menu";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    int rowCount = Convert.ToInt32(command.ExecuteScalar());
                    countLB.Text = $"Количество строк: {rowCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void DeleteDishFromDatabase(int menuID)
        {
            using (var connection = new MySqlConnection(str))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM menu WHERE menuID = @menuID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@menuID", menuID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message);
                }
            }
        }
        private void DeleteDishMenuItem_Click(object sender, EventArgs e)
        {
            if (DishesDGW.SelectedRows.Count > 0)
            {
                // Confirm deletion
                var result = MessageBox.Show("Вы уверены, что хотите удалить выбранное блюдо?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow selectedRow in DishesDGW.SelectedRows)
                    {
                        int menuID = Convert.ToInt32(selectedRow.Cells["Номер_Блюда"].Value);
                        DeleteDishFromDatabase(menuID);
                        DishesDGW.Rows.Remove(selectedRow); // Remove the row from DataGridView
                    }
                    MessageBox.Show("Блюдо(а) удалено(ы) успешно.");
                    Filldgv(); // Refresh the DataGridView to reflect changes
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите блюдо для удаления.");
            }
        }
        private void EditDishMenuItem_Click(object sender, EventArgs e)
        {
            if (DishesDGW.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = DishesDGW.SelectedRows[0];
                int menuID = Convert.ToInt32(selectedRow.Cells["Номер_Блюда"].Value);

                using (editDishes editDishesForm = new editDishes(menuID))
                {
                    if (editDishesForm.ShowDialog() == DialogResult.OK)
                    {
                        Filldgv(); // Обновляем DataGridView после редактирования
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите блюдо для редактирования.");
            }
        }
        private void InitializeFilterCB()
        {
            using (connection = new MySqlConnection(ConnectionStr.connectionString()))
            {
                connection.Open();
                string query = "SELECT categoryID, name FROM category";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable categoryTable = new DataTable();
                adapter.Fill(categoryTable);

                // Создаем объект для элемента "Все"
                var allOption = new { Text = "Все", Value = -1 };
                FilterCB.Items.Add(allOption);

                // Добавляем категории
                foreach (DataRow row in categoryTable.Rows)
                {
                    FilterCB.Items.Add(new { Text = row["name"], Value = row["categoryID"] });
                }

                FilterCB.DisplayMember = "Text";
                FilterCB.ValueMember = "Value";

                // Устанавливаем "Все" как выбранный элемент
                FilterCB.SelectedItem = allOption; // Устанавливаем элемент "Все" по умолчанию
                connection.Close();
            }
        }
        private void AddNewDishMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (addDishes addDishes = new addDishes())
            {
                addDishes.ShowDialog();
                Filldgv();
            }
            // Если форма закрыта с OK, обновляем DataGridView
            Show(); // Перезаполняем DataGridView для отображения нового блюда

        }
        private void MenuDishes_Load(object sender, EventArgs e)
        {
            Filldgv();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Filldgv(string filter = "", int? categoryId = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "SELECT m.menuID as Номер_Блюда, m.name AS Название, m.description AS Описание, c.name AS Категория, m.image AS Изображение, m.cost AS Цена FROM menu m JOIN category c ON m.category = c.categoryID WHERE m.name LIKE @name";

                // If a category ID is provided and is not -1, add the category filter
                if (categoryId.HasValue && categoryId.Value != -1)
                {
                    query += " AND m.category = @categoryID";
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Add parameters for the query
                cmd.Parameters.AddWithValue("@name", "%" + filter + "%");
                if (categoryId.HasValue && categoryId.Value != -1)
                {
                    cmd.Parameters.AddWithValue("@categoryID", categoryId.Value);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);

                // Ensure the column "Номер_Блюда" exists in the table
                if (!table.Columns.Contains("Номер_Блюда"))
                {
                    throw new Exception("Столбец 'Номер_Блюда' не найден в таблице.");
                }

                // Check for the "Картинка" column
                if (!table.Columns.Contains("Картинка"))
                {
                    // Add a new column for images if it doesn't exist
                    DataColumn imageColumn = new DataColumn("Картинка", typeof(Image));
                    table.Columns.Add(imageColumn);
                }

                // Get the path to the img folder
                string imgFolderPath = @"..\..\img";

                foreach (DataRow row in table.Rows)
                {
                    string imageFileName = row["Изображение"].ToString(); // Get the file name from the "Изображение" column
                    string imagePath = Path.Combine(imgFolderPath, imageFileName); // Create the full path to the image

                    // Load the image if the file exists
                    if (File.Exists(imagePath))
                    {
                        row["Картинка"] = Image.FromFile(imagePath); // Load the image
                    }
                    else
                    {
                        row["Картинка"] = null; // If the file is not found, leave it null or set a default image
                    }
                }

                DishesDGW.DataSource = table;

                // Check if the "Картинка" column exists in the DataGridView
                if (!DishesDGW.Columns.Contains("Картинка"))
                {
                    // Set up the "Картинка" column to display images
                    DataGridViewImageColumn imageColumnToDisplay = new DataGridViewImageColumn
                    {
                        Name = "Картинка", // Name of the column
                        HeaderText = "Картинка", // Header text of the column
                        ImageLayout = DataGridViewImageCellLayout.Zoom
                    };
                    DishesDGW.Columns.Add(imageColumnToDisplay);
                }

                DishesDGW.Columns["Номер_Блюда"].Visible = false;
                DishesDGW.Columns.Remove("Изображение");

                // Fill the image column with images from the configured "Картинка" column
                foreach (DataGridViewRow dgvr in DishesDGW.Rows)
                {
                    int rowIndex = dgvr.Index;
                    if (rowIndex < table.Rows.Count) // Check that the index is less than the number of rows
                    {
                        dgvr.Cells["Картинка"].Value = table.Rows[rowIndex]["Картинка"]; // Fill each cell with the corresponding image
                    }
                }

                DishesDGW.MouseClick += DishesDGW_MouseClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void SearchTB_TextChanged(object sender, EventArgs e)
        {
            Filldgv(SearchTB.Text, categoryId);
        }
        private void InitializeComboBox()
        {
            SortCB.Items.Add("По возрастанию");
            SortCB.Items.Add("По убыванию");

        }
        private void SortCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sortOrder = SortCB.SelectedItem.ToString();

            if (sortOrder == "По возрастанию")
            {
                table.DefaultView.Sort = "Цена ASC";
            }
            else if (sortOrder == "По убыванию")
            {
                table.DefaultView.Sort = "Цена DESC";
            }

            // Set the DataSource to the sorted view
            DishesDGW.DataSource = table.DefaultView.ToTable();

            // Remove the unwanted columns
            if (DishesDGW.Columns.Contains("Номер_Блюда"))
            {
                DishesDGW.Columns.Remove("Номер_Блюда");
            }

            if (DishesDGW.Columns.Contains("Изображение"))
            {
                DishesDGW.Columns.Remove("Изображение");
            }
        }

        private void SearchTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void DishesDGW_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = DishesDGW.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    DishesDGW.ClearSelection();
                    DishesDGW.Rows[currentMouseOverRow].Selected = true;

                    contextMenuStrip.Show(DishesDGW, new Point(e.X, e.Y));
                }
                if (orderButton == null)
                {
                    InitializeOrderButton();
                }
            }

        }
        private void AddToOrderMenuItem_Click(object sender, EventArgs e)
        {
            if (DishesDGW.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in DishesDGW.SelectedRows)
                {
                    string nameDishes = selectedRow.Cells["Название"].Value.ToString();
                    int costDishes = Convert.ToInt32(selectedRow.Cells["Цена"].Value);
                    MessageBox.Show($"Блюдо '{nameDishes}' добавлено в заказ!");

                    // Call the method to add the dish to the order form
                    orderForm.dgv(nameDishes, costDishes);
                }

                orderCount += DishesDGW.SelectedRows.Count;
                UpdateOrderButton();

                // Show the order button if it's not visible
                orderButton.Visible = true;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите блюдо для добавления в заказ.");
            }
            // Check if the order form is open
            if (!isOrderFormOpen)
            {
                orderButton.Click += (s, args) => onClickOrder(args);
                isOrderFormOpen = true; // Set the flag that the form is open
            }
        }
        private void UpdateDishInDatabase(int menuID, string name, string description, string category, decimal cost, Image image)
        {
            try
            {
                connection.Open();
                string query = "UPDATE menu SET name = @name, description = @description, category = @category, cost = @cost, image = @image WHERE menuID = @menuID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@menuID", menuID);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@cost", cost);

                // Сохраните изображение в базе данных, если это необходимо
                if (image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        cmd.Parameters.AddWithValue("@image", ms.ToArray());
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@image", DBNull.Value);
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("Блюдо успешно обновлено.");
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
        private void InitializeOrderButton()
        {
            orderButton = new Button
            {
                Text = "Заказ",
                Location = new Point(909, 675),
                Size = new Size(170, 38),
                BackColor = Color.LightSalmon,
                Visible = false
            };
            orderButton.Font = new Font("Nirmala UI", 14);
            this.Controls.Add(orderButton);
        }
        private void UpdateOrderButton()
        {
            orderButton.Text = $"Заказ ({orderCount})";
        }

        private void onClickOrder(EventArgs e)
        {
            if (!orderForm.Visible)
            {
                orderForm.ShowDialog();
            }
            else
            {
                orderForm.BringToFront();
            }
        }

        private void FilterCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterCB.SelectedItem != null)
            {
                var selectedItem = (dynamic)FilterCB.SelectedItem;
                categoryId = selectedItem.Value;
                Filldgv(SearchTB.Text, categoryId);
            }
        }
    }
}