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
        private ColorSquareLabel colorSquareLabel;
        private string currentFilterValue = ""; // Для хранения выбранной категории
        private string currentSortColumn = "Цена"; // По умолчанию сортируем по цене
        private string currentSortDirection = "ASC"; // По умолчанию по возрастанию
        private string currentSearchText = ""; // Для хранения текущего текста поиска
        private int? currentCategoryId = null; // Для хранения текущей категории
        private string currentSortOrder = ""; // Для хранения текущего порядка сортировки
        private int pageSize = 10;
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
        private int currentPage = 0; // Количество строк на странице
        private int totalRows = 0; // Общее количество строк
        private int categoryId;
        string query = "SELECT m.menuID as Номер_Блюда, m.name AS Название, m.description AS Описание, c.name AS Категория, m.image AS Изображение, m.cost AS Цена, m.status AS Доступность " +
                       "FROM menu m JOIN category c ON m.category = c.categoryID WHERE m.name LIKE @name " +
                       "LIMIT @pageSize OFFSET @offset";
        public MenuDishes(string fio, string role)
        {

            InitializeComponent();
            InitializeComboBox();
            InitializeOrderButton();
            InitializeFilterCB();
            InitializePageLabels();
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
            DishesDGW.AllowUserToAddRows = false;
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
        private void UpdateSquareLabel(bool isAvailable)
        {
            if (isAvailable)
            {
                colorSquareLabel.SquareColor = Color.Green;
                colorSquareLabel.DisplayText = "Доступно";
            }
            else
            {
                colorSquareLabel.SquareColor = Color.Red;
                colorSquareLabel.DisplayText = "Недоступно";
            }
        }
        private void UpdatePageLabels()
        {
            ResetPageLabelColors();

            switch (currentPage)
            {
                case 0:
                    page1LB.BackColor = Color.LightBlue; // Выделение первой страницы
                    break;
                case 1:
                    page2LB.BackColor = Color.LightBlue; // Выделение второй страницы
                    break;
                case 2:
                    page3LB.BackColor = Color.LightBlue; // Выделение третьей страницы
                    break;
            }
        }
        // Пример инициализации меток и их обработчиков событий
        private void InitializePageLabels()
        {
            page1LB.Click += (s, e) => GoToPage(0, page1LB);
            page2LB.Click += (s, e) => GoToPage(1, page2LB);
            page3LB.Click += (s, e) => GoToPage(2, page3LB);
        }

        // Метод для перехода на указанную страницу и выделения метки
        private void GoToPage(int pageIndex, Label selectedLabel)
        {
            if (currentPage == pageIndex) return; // Если текущая страница уже выбрана, ничего не делаем

            currentPage = pageIndex; // Устанавливаем текущую страницу
            Filldgv(); // Загружаем данные для текущей страницы без параметров

            // Сброс цвета для всех меток
            ResetPageLabelColors();

            // Выделение выбранной метки
            selectedLabel.BackColor = Color.LightBlue; // Измените на желаемый цвет
        }

        // Метод для сброса цвета меток
        private void ResetPageLabelColors()
        {
            page1LB.BackColor = Color.Transparent; // Или любой другой цвет по умолчанию
            page2LB.BackColor = Color.Transparent;
            page3LB.BackColor = Color.Transparent;
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
            Filldgv(); // Вызываем Filldgv для первоначальной загрузки
            UpdatePageLabels();

            // Устанавливаем состояние фильтрации и сортировки, если необходимо
            if (!string.IsNullOrEmpty(currentSearchText))
            {
                SearchTB.Text = currentSearchText;
            }
            if (currentCategoryId.HasValue)
            {
                FilterCB.SelectedItem = FilterCB.Items.Cast<dynamic>().FirstOrDefault(item => item.Value == currentCategoryId.Value);
            }
            if (!string.IsNullOrEmpty(currentSortOrder))
            {
                SortCB.SelectedItem = currentSortOrder;
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Filldgv()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Получаем общее количество строк с учетом фильтрации и сортировки
                string countQuery = "SELECT COUNT(*) FROM menu m JOIN category c ON m.category = c.categoryID WHERE m.name LIKE @name";
                if (currentCategoryId.HasValue && currentCategoryId.Value != -1)
                {
                    countQuery += " AND m.category = @categoryId";
                }

                MySqlCommand countCmd = new MySqlCommand(countQuery, connection);
                countCmd.Parameters.AddWithValue("@name", "%" + currentSearchText + "%");
                if (currentCategoryId.HasValue && currentCategoryId.Value != -1)
                {
                    countCmd.Parameters.AddWithValue("@categoryId", currentCategoryId.Value);
                }
                totalRows = Convert.ToInt32(countCmd.ExecuteScalar());

                // Изменяем запрос для получения данных с учетом пагинации, фильтрации и сортировки
                string query = "SELECT m.menuID as Номер_Блюда, m.name AS Название, m.description AS Описание, c.name AS Категория, m.image AS Изображение, m.cost AS Цена, m.status AS Доступность " +
                               "FROM menu m JOIN category c ON m.category = c.categoryID WHERE m.name LIKE @name";
                if (currentCategoryId.HasValue && currentCategoryId.Value != -1)
                {
                    query += " AND m.category = @categoryId";
                }
                query += $" ORDER BY {currentSortColumn} {currentSortDirection} LIMIT @pageSize OFFSET @offset";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", "%" + currentSearchText + "%");
                if (currentCategoryId.HasValue && currentCategoryId.Value != -1)
                {
                    cmd.Parameters.AddWithValue("@categoryId", currentCategoryId.Value);
                }
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@offset", currentPage * pageSize);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);

                // Обновите DataGridView
                DishesDGW.DataSource = table;

                // Подсветка строк в зависимости от статуса
                foreach (DataGridViewRow row in DishesDGW.Rows)
                {
                    // Проверяем, что ячейка не равна null
                    if (row.Cells["Доступность"].Value != null)
                    {
                        string status = row.Cells["Доступность"].Value.ToString(); // Получаем статус
                        if (status == "Доступно")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen; // Подсветка для доступных блюд
                        }
                        else if (status == "Недоступно")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral; // Подсветка для недоступных блюд
                        }
                    }
                }

                // Обновите countRowLB для отображения количества строк на текущей странице
                countRowLB.Text = $"Количество строк на странице: {table.Rows.Count} из {totalRows}";

                // Логика для установки доступности меток страниц
                UpdatePaginationLabels();

                // Вызываем метод обновления меток страниц после всех изменений
                UpdatePageLabels();
        
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
                UpdatePaginationButtons();
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
        private void DishesDGW_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DishesDGW.Columns[e.ColumnIndex].Name == "Доступность")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();
                    if (status == "available")
                    {
                        DishesDGW.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (status == "unavailable")
                    {
                        DishesDGW.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
        }
        private void UpdatePaginationLabels()
        {
            int totalPages = (totalRows + pageSize - 1) / pageSize; // Общее количество страниц

            // Установка доступности меток страниц
            page1LB.Enabled = currentPage > 0; // Первая страница доступна, если текущая страница больше 0
            page2LB.Enabled = (currentPage + 1) < totalPages; // Вторая страница доступна, если текущая страница меньше общего числа страниц
            page3LB.Enabled = (currentPage + 2) < totalPages; // Третья страница доступна, если текущая страница меньше общего числа страниц за 2

            // Обновление цвета меток страниц
            UpdatePageLabels();
        }
        private void SearchTB_TextChanged(object sender, EventArgs e)
        {
            currentSearchText = SearchTB.Text; // Сохраняем текст поиска
            currentPage = 0; // Сбрасываем текущую страницу на первую
            Filldgv(); // Вызываем Filldgv без параметров
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
                currentSortDirection = "ASC";
            }
            else if (sortOrder == "По убыванию")
            {
                currentSortDirection = "DESC";
            }

            Filldgv(); // Вызываем Filldgv без параметров
        

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
                currentCategoryId = selectedItem.Value; // Сохраняем выбранную категорию
                Filldgv(); // Вызываем Filldgv без параметров
            }
        }
        private void UpdatePaginationButtons()
        {
            backBtn.Enabled = currentPage > 0; // Деактивируем кнопку назад, если на первой странице
            forwardBtn.Enabled = (currentPage + 1) * pageSize < totalRows; // Деактивируем кнопку вперед, если достигли последней страницы
        }
        private void backBtn_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                Filldgv();// Перезагружаем данные для текущей страницы
                UpdatePageLabels();
            }
        }

        private void forwardBtn_Click(object sender, EventArgs e)
        {
            if ((currentPage + 1) * pageSize < totalRows)
            {
                currentPage++;
                Filldgv(); // Перезагружаем данные для текущей страницы
                UpdatePageLabels();
            }
        }
    }
}