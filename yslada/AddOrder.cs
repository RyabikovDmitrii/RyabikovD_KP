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
    public partial class AddOrder : Form
    {
        private string str = ConnectionStr.connectionString();
        private MySqlConnection connection;
        private DataTable orderTable = new DataTable();
        private string NameDishes = "";
        private string FIO = "";
        private int costDishes = 0;
        private int countDishes = 1;
        public AddOrder(string fio)
        {
            InitializeComponent();
            initializeTableCB();
            InitializeOrderTable();
            timeOrder.Text = DateTime.Now.ToString();
            employeeTB.Text = fio;
            FIO += fio;
            tableCB.KeyPress += (sender, e) => e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {

        }
        private void initializeTableCB()
        {
            tableCB.Items.Add(1);
            tableCB.Items.Add(2);
            tableCB.Items.Add(3);
            tableCB.Items.Add(4);
            tableCB.Items.Add(5);
            tableCB.Items.Add(6);
            tableCB.Items.Add(7);
            tableCB.Items.Add(8);
            tableCB.Items.Add(9);
            tableCB.Items.Add(10);
        }
        public void dgv(string nameDishes, int costDishes)
        {
            foreach (DataRow existRow in orderTable.Rows)
            {
                if (existRow["Блюдо"].ToString() == nameDishes)
                {
                    existRow["Количество"] = Convert.ToInt32(existRow["Количество"]) + 1;
                    UpdateTotalPrice();
                    return;
                }
            }

            DataRow row = orderTable.NewRow();
            row["Блюдо"] = nameDishes;
            row["Стоимость"] = costDishes;
            row["Количество"] = countDishes; // Default count is 1
            orderTable.Rows.Add(row);

            // Update DataGridView
            orderDGV.DataSource = orderTable;
            orderDGV.ReadOnly = true;
            UpdateTotalPrice();
        }
        private void InitializeOrderTable()
        {
            orderTable.Columns.Add("Блюдо", typeof(string));
            orderTable.Columns.Add("Стоимость", typeof(int));
            orderTable.Columns.Add("Количество", typeof(int));
            orderDGV.DataSource = orderTable;
        }
        private void addOrderBtn_Click(object sender, EventArgs e)
        {
            AddOrderToDatabase();
        }
        private void AddOrderToDatabase()
        {
            try
            {
                connection = new MySqlConnection(str);
                connection.Open();

                // Start a transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        string date = now.ToString("yyyy/MM/dd HH:mm:ss");

                        // List to store IDs of added items
                        List<int> orderItemIDs = new List<int>();

                        // 1. Add items to the orderitem table
                        foreach (DataRow row in orderTable.Rows)
                        {
                            string insertItemQuery = "INSERT INTO orderitem (name, count) VALUES (@name, @count);";
                            using (var command = new MySqlCommand(insertItemQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@name", row["Блюдо"].ToString());
                                command.Parameters.AddWithValue("@count", row["Количество"]);
                                command.ExecuteNonQuery();

                                // Get the ID of the last inserted item
                                string query = "SELECT LAST_INSERT_ID();";
                                using (var cmd = new MySqlCommand(query, connection, transaction))
                                {
                                    int itemID = Convert.ToInt32(cmd.ExecuteScalar());
                                    orderItemIDs.Add(itemID); // Add the ID to the list
                                }
                            }
                        }

                        // 2. Add a record to the order table
                        string insertOrderQuery = "INSERT INTO `order` (tableNum, date, costOrder, statusOrder, employee) VALUES (@tableNum, @date, @costOrder, @statusOrder, @employee); SELECT LAST_INSERT_ID();";
                        int orderID;
                        using (var orderCommand = new MySqlCommand(insertOrderQuery, connection, transaction))
                        {
                            orderCommand.Parameters.AddWithValue("@tableNum", tableCB.SelectedItem); // Selected table number
                            orderCommand.Parameters.AddWithValue("@date", date);
                            orderCommand.Parameters.AddWithValue("@costOrder", costDishes);
                            orderCommand.Parameters.AddWithValue("@statusOrder", "Новый"); // Initial status
                            orderCommand.Parameters.AddWithValue("@employee", employeeTB.Text); // Employee name

                            // Execute the command and retrieve the new orderID
                            orderID = Convert.ToInt32(orderCommand.ExecuteScalar());
                        }

                        // 3. Add entries to the orderitem_has_order table for each orderItemID
                        string insertOrderItemHasOrderQuery = "INSERT INTO orderitem_has_order (orderItem_orderItemID, order_orderID) VALUES (@orderItemID, @orderID)";
                        foreach (int orderItemID in orderItemIDs)
                        {
                            using (var hasOrderCommand = new MySqlCommand(insertOrderItemHasOrderQuery, connection, transaction))
                            {
                                hasOrderCommand.Parameters.AddWithValue("@orderItemID", orderItemID);
                                hasOrderCommand.Parameters.AddWithValue("@orderID", orderID); // Use the newly created orderID
                                hasOrderCommand.ExecuteNonQuery();
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();
                        MessageBox.Show("Заказ успешно добавлен!");
                        DialogResult res = MessageBox.Show("Хотите напечатать чек?", "Печать чека", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            GenerateReceipt(orderID);
                            Close();
                        }
                        else
                        {
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при добавлении заказа: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void GenerateReceipt(int orderId)
        {
            try
            {
                // Создаем новый документ Word
                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var document = wordApp.Documents.Add();

                // Получаем данные заказа
                string orderDetailsQuery = @"
        SELECT
            oi.name AS Блюдо,
            oi.count AS Количество,
            m.cost AS Цена,  -- Получаем цену из таблицы menu
            o.date AS Дата_заказа,
            o.tableNum AS Номер_стола,
            o.employee AS Сотрудник
        FROM
            `order` AS o
        JOIN
            orderitem_has_order AS oho ON o.orderID = oho.order_orderID
        JOIN
            orderitem AS oi ON oho.orderitem_orderItemID = oi.orderItemID
        JOIN
            menu AS m ON oi.name = m.name  -- Соединяем с таблицей menu для получения цены
        WHERE
            o.orderID = @orderID";

                decimal totalCost = 0; // Переменная для хранения общей суммы заказа
                DateTime orderDate = DateTime.Now; // Переменная для хранения даты заказа
                string composition = ""; // Переменная для хранения состава заказа
                int TableNum = 0;
                string userName = FIO;

                using (MySqlConnection connection = new MySqlConnection(str))
                {
                    MySqlCommand cmd = new MySqlCommand(orderDetailsQuery, connection);
                    cmd.Parameters.AddWithValue("@orderID", orderId);
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Добавляем данные о заказе в документ
                    while (reader.Read())
                    {
                        string dishName = reader["Блюдо"].ToString();
                        int quantity = Convert.ToInt32(reader["Количество"]);
                        decimal price = Convert.ToDecimal(reader["Цена"]); // Получаем цену за одно блюдо из таблицы menu
                        decimal cost = quantity * price; // Рассчитываем общую сумму для этого блюда
                        int table = Convert.ToInt32(reader["Номер_стола"]);
                        TableNum += table;

                        // Суммируем стоимость
                        totalCost += cost;

                        // Запоминаем дату заказа (предполагается, что дата одинакова для всех блюд)
                        orderDate = Convert.ToDateTime(reader["Дата_заказа"]);

                        // Формируем состав заказа
                        composition += $"Блюдо: {dishName}, Кол-во: {quantity}, Цена за единицу: {price:C}, Сумма: {cost:C}\n";
                    }
                }

                // Заголовок документа
                var titleRange = document.Range();
                titleRange.Text = "Чек заказа #" + orderId;
                titleRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                titleRange.Font.Size = 16;
                titleRange.Bold = 1;
                titleRange.InsertParagraphAfter();

                // Добавляем состав заказа
                var compositionRange = document.Range();
                compositionRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                compositionRange.Text = composition;
                compositionRange.InsertParagraphAfter();

                // Добавляем дату заказа в правом верхнем углу
                var dateRange = document.Range();
                dateRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                dateRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                dateRange.Text = $"Дата заказа: {orderDate:G}"; // Формат даты
                dateRange.InsertParagraphAfter();

                var tableRange = document.Range();
                tableRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                tableRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                tableRange.Text = $"Номер стола: {TableNum}"; // Формат номер стола
                tableRange.InsertParagraphAfter();

                // Добавляем общую сумму заказа в правом верхнем углу
                var totalCostRange = document.Range();
                totalCostRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                totalCostRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                totalCostRange.Text += $"\nОбщая сумма: {totalCost:C}"; // Формат суммы
                totalCostRange.InsertParagraphAfter();

                var fioEmployeeRange = document.Range();
                fioEmployeeRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                fioEmployeeRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                fioEmployeeRange.Text += $"\nСотрудник: {userName}"; // Формат сотрудника
                fioEmployeeRange.InsertParagraphAfter();

                // Открытие диалогового окна для выбора пути сохранения
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Documents (*.docx)|*.docx";
                    saveFileDialog.Title = "Сохранить чек как";
                    saveFileDialog.FileName = $"Чек_заказа_{orderId}.docx"; // Предложить имя файла

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Сохранение документа по выбранному пути
                        document.SaveAs2(saveFileDialog.FileName);
                        MessageBox.Show($"Чек успешно создан: {saveFileDialog.FileName}");
                    }
                }

                document.Close();
                wordApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании чека: " + ex.Message);
            }
        }
        private void UpdateTotalPrice()
        {
            costDishes = 0;
            foreach (DataRow row in orderTable.Rows)
            {
                costDishes += Convert.ToInt32(row["Стоимость"]) * Convert.ToInt32(row["Количество"]);
            }
            sumOrder.Text = $"{costDishes}";
        }
        public DataTable GetCurrentOrders()
        {
            return orderTable;
        }
    }
}
