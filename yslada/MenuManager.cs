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
    public partial class MenuManager : Form
    {
        private string FIO = "";
        private string Role = "";
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
        public MenuManager(string fio, string role)
        {
            InitializeComponent();
            connection = new MySqlConnection(str);
            FIO += fio;
            Role += role;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuBtn_Click(object sender, EventArgs e)
        {
            Hide();
            using (MenuDishes dishes = new MenuDishes(FIO, Role))
            {
                dishes.ShowDialog();
                FillDGV();
            }
            Show();
        }
        public void SetLBText(string fio)
        {
            fioLB.Text = fio;
        }

        private void MenuManager_Load(object sender, EventArgs e)
        {
            FillDGV();
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

        private void button4_Click(object sender, EventArgs e)
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
    }
}
