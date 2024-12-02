using MySql.Data.MySqlClient;
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

namespace yslada
{
    public partial class addDishesForm : Form
    {
        private MySqlConnection connection;
        private DataTable table;
        private string str = ConnectionStr.connectionString();
        private int? categoryId = null;
        private int orderID;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addToOrderToolStripMenuItem;
        string query = "SELECT m.menuID as Номер_Блюда, m.name AS Название, m.description AS Описание, c.name AS Категория, m.image AS Изображение, m.cost AS Цена FROM menu m JOIN category c ON m.category = c.categoryID WHERE m.name LIKE @name";

        public addDishesForm(int orderID)
        {
            this.orderID = orderID;
            InitializeComponent();
            InitializeComboBox();
            InitializeFilterCB();
            InitializeContextMenu(); // Инициализация контекстного меню
            connection = new MySqlConnection(str);
            SortCB.SelectedIndexChanged += SortCB_SelectedIndexChanged;
            FilterCB.KeyPress += (sender, e) => e.Handled = true;
            SortCB.KeyPress += (sender, e) => e.Handled = true;
        }

        private void InitializeContextMenu()
        {
            // Создаем ContextMenuStrip
            contextMenuStrip1 = new ContextMenuStrip();

            // Создаем ToolStripMenuItem
            addToOrderToolStripMenuItem = new ToolStripMenuItem("Добавить в заказ");
            addToOrderToolStripMenuItem.Click += AddToOrderToolStripMenuItem_Click;

            // Добавляем элемент в контекстное меню
            contextMenuStrip1.Items.Add(addToOrderToolStripMenuItem);

            // Привязываем контекстное меню к DataGridView
            DishesDGW.ContextMenuStrip = contextMenuStrip1;

            // Подписка на событие правого клика для показа контекстного меню
            DishesDGW.MouseDown += DishesDGW_MouseDown;
        }

        private void DishesDGW_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = DishesDGW.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0) // Если кликнули по строке
                {
                    DishesDGW.ClearSelection();
                    DishesDGW.Rows[hitTest.RowIndex].Selected = true; // Выделяем строку
                    contextMenuStrip1.Show(DishesDGW, e.Location); // Показываем контекстное меню
                }
            }
        }

        private void AddToOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int OrderID = orderID;
            if (DishesDGW.SelectedRows.Count > 0)
            {
                var selectedRow = DishesDGW.SelectedRows[0];
                string dishName = selectedRow.Cells["Название"].Value.ToString();
                decimal dishCost = Convert.ToDecimal(selectedRow.Cells["Цена"].Value);

                // Логика для добавления блюда в заказ
                if (AddDishToOrderItem(OrderID, dishName, 1)) // Здесь количество по умолчанию 1
                {
                    MessageBox.Show($"Блюдо '{dishName}' добавлено в заказ.");
                    this.Close(); // Закрываем форму после добавления
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении блюда в заказ.");
                }
            }
        }
        private void InitializeComboBox()
        {
            SortCB.Items.Add("По возрастанию");
            SortCB.Items.Add("По убыванию");
            SortCB.SelectedIndexChanged += SortCB_SelectedIndexChanged;
        }

        private void InitializeFilterCB()
        {
            using (connection = new MySqlConnection(str))
            {
                connection.Open();
                string query = "SELECT categoryID, name FROM category";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable categoryTable = new DataTable();
                adapter.Fill(categoryTable);

                // Add a default option for filtering
                FilterCB.Items.Add(new { Text = "Все", Value = -1 });
                foreach (DataRow row in categoryTable.Rows)
                {
                    FilterCB.Items.Add(new { Text = row["name"], Value = row["categoryID"] });
                }

                FilterCB.DisplayMember = "Text";
                FilterCB.ValueMember = "Value";
                FilterCB.SelectedItem = -1;
            }
        }

        private void Filldgv(string filter = "", int? categoryId = null)
        {
            try
            {
                connection.Open();               

                // If a category ID is provided and is not -1, add the category filter
                if (categoryId.HasValue && categoryId.Value != -1)
                {
                    query += " AND m.category = @categoryID";
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", "%" + filter + "%");
                if (categoryId.HasValue && categoryId.Value != -1)
                {
                    cmd.Parameters.AddWithValue("@categoryID", categoryId.Value);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);

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

                // Set up the "Картинка" column to display images
                if (!DishesDGW.Columns.Contains("Картинка"))
                {
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

        private void FilterCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterCB.SelectedItem != null)
            {
                var selectedItem = (dynamic)FilterCB.SelectedItem;
                categoryId = selectedItem.Value;
                Filldgv(SearchTB.Text, categoryId);
            }
        }

        private void SearchTB_TextChanged(object sender, EventArgs e)
        {
            Filldgv(SearchTB.Text, categoryId);
        }

        private void addDishesForm_Load(object sender, EventArgs e)
        {
            Filldgv();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool AddDishToOrderItem(int orderId, string dishName, int quantity)
        {
            try
            {
                using (var connection = new MySqlConnection(str))
                {
                    connection.Open();
                    string query = "INSERT INTO orderitem (name, count) VALUES (@name, @count); " +
                                   "INSERT INTO orderitem_has_order (orderitem_orderItemID, order_orderID) " +
                                   "VALUES (LAST_INSERT_ID(), @orderID)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", dishName);
                        command.Parameters.AddWithValue("@count", quantity);
                        command.Parameters.AddWithValue("@orderID", orderId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Если добавление прошло успешно
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                return false;
            }
        }
    }
}

