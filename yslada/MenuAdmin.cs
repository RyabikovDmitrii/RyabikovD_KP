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
    public partial class MenuAdmin : Form
    {
        #region Переменные
        private string FIO = "";
        private string Role = "";
        private int orderId;
        private string tableNum = "";
        private string statusOrder = "";
        private string employee = "";
        #endregion
        private string str = ConnectionStr.connectionString();
        private MySqlConnection connection;
        private DataTable table;
        private string query = @"
SELECT
    o.orderID AS Номер_заказа,
    GROUP_CONCAT(CONCAT(oi.name, ' (', oi.count, ')')) AS Состав_Заказа,
    o.tableNum AS Номер_Стола,
    o.date AS Время_заказа,
    o.costOrder AS Сумма_заказа,
    o.statusOrder AS Статус_заказа,
    o.employee AS Сотрудник
FROM
    `order` AS o
LEFT JOIN
    orderitem_has_order AS oho ON o.orderID = oho.order_orderID
LEFT JOIN
    orderItem AS oi ON oho.orderitem_orderItemID = oi.orderItemID
GROUP BY
    o.orderID";
        public MenuAdmin(string fio, string role)
        {
            InitializeComponent();
            connection = new MySqlConnection(str);
            FIO += fio;
            Role += role;
        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {
            FillDGV();
        }
        public void SetLBText(string fio)
        {
            fioLB.Text = fio;
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuBtn_Click(object sender, EventArgs e)
        {
            Hide();
            using(MenuDishes dishes = new MenuDishes(FIO, Role))
            {
                dishes.ShowDialog();
                FillDGV();
            }
            Show();
        }
        private void FillDGV()
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                orderDGV.DataSource = table;
                orderDGV.ReadOnly = true;
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

        private void employeeBtn_Click(object sender, EventArgs e)
        {
            Hide();
            using (Employees employees = new Employees())
            {
                employees.ShowDialog();
            }
            Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            using (addDishes add = new addDishes())
            {
                add.ShowDialog();
            }
            Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            if (orderDGV.SelectedRows.Count > 0) 
            {
                DialogResult result = MessageBox.Show("Вы точно хотите удалить заказ?", "Удаления заказа", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Логика удаления заказа
                if (result == DialogResult.Yes)
                {
                    int orderId = Convert.ToInt32(orderDGV.SelectedRows[0].Cells["Номер_заказа"].Value);
                    try
                    {
                        connection.Open();
                        // Удаляем элементы заказа
                        string deleteOrderItemsQuery = "DELETE FROM orderitem WHERE orderItemID IN (SELECT orderitem_orderItemID FROM orderitem_has_order WHERE order_orderID = @orderID)";
                        MySqlCommand deleteOrderItemsCmd = new MySqlCommand(deleteOrderItemsQuery, connection);
                        deleteOrderItemsCmd.Parameters.AddWithValue("@orderID", orderId);
                        deleteOrderItemsCmd.ExecuteNonQuery();

                        // Удаляем связи в orderitem_has_order
                        string deleteRelationsQuery = "DELETE FROM orderitem_has_order WHERE order_orderID = @orderID";
                        MySqlCommand deleteRelationsCmd = new MySqlCommand(deleteRelationsQuery, connection);
                        deleteRelationsCmd.Parameters.AddWithValue("@orderID", orderId);
                        deleteRelationsCmd.ExecuteNonQuery();

                        // Удаляем сам заказ
                        string deleteOrderQuery = "DELETE FROM `order` WHERE orderID = @orderID"; // Изменено на orderID
                        MySqlCommand deleteOrderCmd = new MySqlCommand(deleteOrderQuery, connection);
                        deleteOrderCmd.Parameters.AddWithValue("@orderID", orderId);
                        deleteOrderCmd.ExecuteNonQuery();

                        MessageBox.Show("Заказ удален успешно!");
                        // Обновить список заказов
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления заказа: " + ex.Message);
                    }
                    finally
                    {

                        connection.Close();
                    }
                    FillDGV();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите заказ для удаления.");
                }

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (orderDGV.SelectedRows.Count > 0)
            {
                // Получаем ID заказа из выбранной строки
                int orderId = Convert.ToInt32(orderDGV.SelectedRows[0].Cells["Номер_заказа"].Value); // Предполагается, что у вас есть столбец "orderID"
                    Hide();
                    using (editOrder edit = new editOrder(orderId, Role))
                    {
                        edit.ShowDialog();
                        FillDGV();
                    }
                    Show();                
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ для редактирования.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (orderDGV.SelectedRows.Count > 0)
            {
                int orderId = Convert.ToInt32(orderDGV.SelectedRows[0].Cells["Номер_заказа"].Value);
                GenerateReceipt(orderId);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ для печати чека.");
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
                string userName = "";

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
                        string employee = reader["Сотрудник"].ToString();
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
                        document.SaveAs(saveFileDialog.FileName);
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

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
