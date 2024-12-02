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
    public partial class editOrder : Form
    {
        private MySqlConnection connection;
        private int orderId;
        private string str = ConnectionStr.connectionString();
        private NumericUpDown quantityUpDown;
        public editOrder(int orderId, string role)
        {
            InitializeComponent();
            this.orderId = orderId;
            connection = new MySqlConnection(str);
            LoadOrderData();
            LoadStatuses();
            LoadEmployees();
            LoadTableNumbers();

            // Инициализация NumericUpDown
            quantityUpDown = new NumericUpDown();
            quantityUpDown.Minimum = 1; // Минимальное количество
            quantityUpDown.Maximum = 15; // Максимальное количество
            quantityUpDown.Location = new Point(645, 257); // Позиция на форме
            Controls.Add(quantityUpDown);
            if (role == "2")
            {
                fioCB.Enabled = false;
            }

        }

        private void LoadOrderData()
        {
            // Загрузка данных заказа по orderId
            string query = "SELECT tableNum, statusOrder, employee FROM `order` WHERE orderID = @orderID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@orderID", orderId);

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tableCB.Text = reader["tableNum"].ToString();
                statusCB.Text = reader["statusOrder"].ToString();
                fioCB.Text = reader["employee"].ToString();
            }
            reader.Close();

            // Загрузка элементов заказа
            string itemsQuery = "SELECT oi.orderItemID, oi.name as Блюдо, oi.count as Количество FROM orderitem oi " +
                                "JOIN orderitem_has_order oho ON oi.orderItemID = oho.orderitem_orderItemID " +
                                "WHERE oho.order_orderID = @orderID";
            MySqlCommand itemsCmd = new MySqlCommand(itemsQuery, connection);
            itemsCmd.Parameters.AddWithValue("@orderID", orderId);
            MySqlDataAdapter adapter = new MySqlDataAdapter(itemsCmd);
            DataTable itemsTable = new DataTable();
            adapter.Fill(itemsTable);
            structureDGV.DataSource = itemsTable;

            // Удаляем ненужные колонки
            structureDGV.Columns["orderItemID"].Visible = false; // Скрываем ID
            structureDGV.Columns["Блюдо"].HeaderText = "Название блюда"; // Переименовываем заголовок
            structureDGV.Columns["Количество"].HeaderText = "Количество"; // Переименовываем заголовок

            connection.Close();
        }
        private void structureDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (structureDGV.SelectedRows.Count > 0)
            {
                int selectedRowIndex = structureDGV.SelectedRows[0].Index;
                int currentQuantity = Convert.ToInt32(structureDGV.Rows[selectedRowIndex].Cells["Количество"].Value);
                quantityUpDown.Value = currentQuantity; // Устанавливаем текущее количество в NumericUpDown
            }
        }
        private int GetItemPrice(int orderItemId)
        {
            int price = 0;
            string query = "SELECT m.cost FROM menu m JOIN orderitem oi ON m.name = oi.name WHERE oi.orderItemID = @orderItemID";

            using (MySqlConnection connection = new MySqlConnection(str))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@orderItemID", orderItemId);

                try
                {
                    connection.Open();
                    price = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении цены блюда: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return price;
        }
        private void UpdateTotalOrderPrice(int amountToDeduct = 0)
        {
            // Получаем общую стоимость заказа
            string totalQuery = "SELECT SUM(oi.count * m.cost) FROM orderitem oi " +
                                "JOIN menu m ON oi.name = m.name " +
                                "JOIN orderitem_has_order oho ON oi.orderItemID = oho.orderitem_orderItemID " +
                                "WHERE oho.order_orderID = @orderID";

            using (MySqlConnection connection = new MySqlConnection(str))
            {
                MySqlCommand totalCmd = new MySqlCommand(totalQuery, connection);
                totalCmd.Parameters.AddWithValue("@orderID", orderId);

                try
                {
                    connection.Open();
                    int totalPrice = Convert.ToInt32(totalCmd.ExecuteScalar());

                    // Вычитаем сумму, если блюдо было удалено
                    totalPrice -= amountToDeduct;

                    // Обновляем общую стоимость заказа в базе данных
                    string updateTotalQuery = "UPDATE `order` SET costOrder = @totalPrice WHERE orderID = @orderID";
                    MySqlCommand updateTotalCmd = new MySqlCommand(updateTotalQuery, connection);
                    updateTotalCmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                    updateTotalCmd.Parameters.AddWithValue("@orderID", orderId);
                    updateTotalCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении общей стоимости заказа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #region OnClickBtn
        private void editBtn_Click(object sender, EventArgs e)
        {
            // Сначала проверим, заполнены ли все необходимые поля
            if (string.IsNullOrEmpty(tableCB.Text) || string.IsNullOrEmpty(statusCB.Text) || string.IsNullOrEmpty(fioCB.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Создаем соединение с базой данных
            using (MySqlConnection connection = new MySqlConnection(str))
            {
                connection.Open();

                // Получаем полное имя сотрудника из fioCB
                string employeeFullName = fioCB.Text; // Здесь мы берем полное имя

                // Обновление данных заказа
                string updateOrderQuery = "UPDATE `order` SET tableNum = @tableNum, statusOrder = @statusOrder, employee = @employee WHERE orderID = @orderID";
                MySqlCommand updateOrderCmd = new MySqlCommand(updateOrderQuery, connection);
                updateOrderCmd.Parameters.AddWithValue("@tableNum", tableCB.Text);
                updateOrderCmd.Parameters.AddWithValue("@statusOrder", statusCB.Text);
                updateOrderCmd.Parameters.AddWithValue("@employee", employeeFullName); // Используем полное имя сотрудника
                updateOrderCmd.Parameters.AddWithValue("@orderID", orderId);

                try
                {
                    // Выполняем обновление заказа
                    int rowsAffected = updateOrderCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заказ успешно обновлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить заказ. Пожалуйста, проверьте данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении заказа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрано ли блюдо
            if (structureDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите блюдо для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем ID выбранного блюда
            int selectedRowIndex = structureDGV.SelectedRows[0].Index;
            int orderItemId = Convert.ToInt32(structureDGV.Rows[selectedRowIndex].Cells["orderItemID"].Value);

            // Получаем цену блюда для обновления общей стоимости
            int pricePerItem = GetItemPrice(orderItemId);
            int currentQuantity = Convert.ToInt32(structureDGV.Rows[selectedRowIndex].Cells["Количество"].Value);
            int totalPriceToDeduct = pricePerItem * currentQuantity; // Общая стоимость удаляемого блюда

            // Запрос на удаление блюда
            string deleteQuery = "DELETE FROM orderitem WHERE orderItemID = @orderItemID";

            using (MySqlConnection connection = new MySqlConnection(str))
            {
                MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
                deleteCmd.Parameters.AddWithValue("@orderItemID", orderItemId);

                try
                {
                    connection.Open();
                    int rowsAffected = deleteCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Блюдо успешно удалено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrderData(); // Обновляем данные заказа
                        UpdateTotalOrderPrice(-totalPriceToDeduct); // Обновляем общую стоимость заказа
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить блюдо. Пожалуйста, проверьте данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении блюда: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            Hide();
            using (addDishesForm add = new addDishesForm(orderId))
            {
                add.ShowDialog();
                LoadOrderData();
            }
            Show();
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void countBtn_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрано ли блюдо
            if (structureDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите блюдо для изменения количества.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем ID выбранного блюда
            int selectedRowIndex = structureDGV.SelectedRows[0].Index;
            int orderItemId = Convert.ToInt32(structureDGV.Rows[selectedRowIndex].Cells["orderItemID"].Value);
            int newQuantity = (int)quantityUpDown.Value; // Получаем новое количество из NumericUpDown

            // Получаем цену блюда из таблицы menu
            decimal pricePerItem = GetItemPrice(orderItemId);

            // Запрос на обновление количества блюда
            string updateQuery = "UPDATE orderitem SET count = @count WHERE orderItemID = @orderItemID";

            using (MySqlConnection connection = new MySqlConnection(str))
            {
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@count", newQuantity);
                updateCmd.Parameters.AddWithValue("@orderItemID", orderItemId);

                try
                {
                    connection.Open();
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Количество блюда успешно обновлено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrderData(); // Обновляем данные заказа
                        UpdateTotalOrderPrice(); // Обновляем общую стоимость заказа
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить количество блюда. Пожалуйста, проверьте данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении количества блюда: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        #region ComboBox
        private void LoadTableNumbers()
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
            //foreach (DataGridViewColumn column in structureDGV.Columns)
            //{
            //    MessageBox.Show(column.Name);
            //}
        }
        private void LoadStatuses()
        {
            // Загрузка статусов заказа в ComboBox
            statusCB.Items.Add("Новый");
            statusCB.Items.Add("Завершенный");
        }

        private void LoadEmployees()
        {
            // Загрузка сотрудников в ComboBox
            string query = "SELECT surname, name, patronymic FROM user";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable employeeTable = new DataTable();
            adapter.Fill(employeeTable);

            // Создаем новый столбец для полного ФИО
            employeeTable.Columns.Add("fio", typeof(string), "surname + ' ' + name + ' ' + patronymic");

            fioCB.DisplayMember = "fio"; // Указываем, что нужно отображать столбец "fio"
            fioCB.ValueMember = "fio"; // Указываем, что значение будет полное имя
            fioCB.DataSource = employeeTable; // Привязываем DataTable к ComboBox
        }

        #endregion         
    }
}
