using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace yslada
{
    public partial class InportData : Form
    {
        private string db = " ";
        private string connectionString = ConnectionStr.connectionString();
        public InportData()
        {
            InitializeComponent();
        }
        private void LoadTableNames()
        {
            string[] tables = { "user", "role", "category", "menu" };

            foreach (var table in tables)
            {
                tableCB.Items.Add(table);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InportBtn_Click(object sender, EventArgs e)
        {
            if (tableCB.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите таблицу для импорта.");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportCsvToDatabase(filePath, tableCB.SelectedItem.ToString());
                }
            }
        }
        private void ImportCsvToDatabase(string filePath, string tableName)
        {
            int importedRecordsCount = 0;

            try
            {
                string connectionStringWithCharset = $"{connectionString};charset=utf8mb4";

                using (MySqlConnection con = new MySqlConnection(connectionStringWithCharset))
                {
                    con.Open();
                    using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                    {
                        string line;
                        reader.ReadLine(); 
                        int columnCount = GetColumnCount(con, tableName);

                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(';');
                            if (values.Length != columnCount)
                            {
                                MessageBox.Show($"Несоответствие количества параметров: ожидалось {columnCount}, но получено {values.Length}.");
                                return;
                            }

                            var insertCommand = new MySqlCommand($"INSERT INTO `{tableName}` VALUES ({string.Join(",", values.Select(v => $"'{v.Replace("'", "''")}'"))});", con);
                            insertCommand.ExecuteNonQuery();
                            importedRecordsCount++;
                        }
                    }
                }

                MessageBox.Show($"Успешное импортирование! {importedRecordsCount} строк.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импортировании: {ex.Message}");
            }
        }
        private int GetColumnCount(MySqlConnection con, string tableName)
        {
            var getColumnCountCommand = new MySqlCommand($"DESCRIBE {tableName}", con);
            int columnCount = 0;
            using (var reader = getColumnCountCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    columnCount++;
                }
            }
            return columnCount;
        }

        private void recoverBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.database = db;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                try
                {
                    string createSchemaScript = @"
                      CREATE DATABASE  IF NOT EXISTS `yslada_upd`;
                      USE `yslada_upd`;

                    --
                    -- Table structure for table `category`
                    --

                    DROP TABLE IF EXISTS `category`;
                    CREATE TABLE `category` (
                      `categoryID` int NOT NULL AUTO_INCREMENT,
                      `name` text NOT NULL,
                      PRIMARY KEY (`categoryID`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;

                    --
                    -- Table structure for table `menu`
                    --

                    DROP TABLE IF EXISTS `menu`;
                    CREATE TABLE `menu` (
                      `menuID` int NOT NULL AUTO_INCREMENT,
                      `name` text NOT NULL,
                      `description` text NOT NULL,
                      `category` int NOT NULL,
                      `image` text,
                      `status` varchar(15) CHARACTER SET armscii8 COLLATE armscii8_general_ci DEFAULT NULL,
                      `cost` int NOT NULL,
                      PRIMARY KEY (`menuID`,`category`),
                      KEY `fk_menu_category1_idx` (`category`),
                      CONSTRAINT `fk_menu_category1` FOREIGN KEY (`category`) REFERENCES `yslada`.`category` (`categoryID`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb3;

                    --
                    -- Table structure for table `order`
                    --

                    DROP TABLE IF EXISTS `order`;
                    CREATE TABLE `order` (
                      `orderID` int NOT NULL AUTO_INCREMENT,
                      `tableNum` varchar(15) NOT NULL,
                      `date` datetime NOT NULL,
                      `costOrder` int NOT NULL,
                      `statusOrder` text NOT NULL,
                      `employee` text NOT NULL,
                      `orderItemID` int DEFAULT NULL,
                      PRIMARY KEY (`orderID`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb3;

                    --
                    -- Table structure for table `orderitem`
                    --

                    DROP TABLE IF EXISTS `orderitem`;
                    CREATE TABLE `orderitem` (
                      `orderItemID` int NOT NULL AUTO_INCREMENT,
                      `name` text NOT NULL,
                      `count` int NOT NULL,
                      PRIMARY KEY (`orderItemID`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=305 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

                    --
                    -- Table structure for table `orderitem_has_order`
                    --

                    DROP TABLE IF EXISTS `orderitem_has_order`;
                    CREATE TABLE `orderitem_has_order` (
                      `orderitem_orderItemID` int NOT NULL,
                      `order_orderID` int NOT NULL,
                      PRIMARY KEY (`orderitem_orderItemID`,`order_orderID`),
                      KEY `fk_orderitem_has_order_order1_idx` (`order_orderID`),
                      KEY `fk_orderitem_has_order_orderitem1_idx` (`orderitem_orderItemID`),
                      CONSTRAINT `fk_orderitem_has_order_order1` FOREIGN KEY (`order_orderID`) REFERENCES `order` (`orderID`) ON DELETE CASCADE ON UPDATE CASCADE,
                      CONSTRAINT `fk_orderitem_has_order_orderitem1` FOREIGN KEY (`orderitem_orderItemID`) REFERENCES `orderitem` (`orderItemID`) ON DELETE CASCADE ON UPDATE CASCADE
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

                    --
                    -- Table structure for table `role`
                    --

                    DROP TABLE IF EXISTS `role`;
                    CREATE TABLE `role` (
                      `roleID` int NOT NULL AUTO_INCREMENT,
                      `name` text NOT NULL,
                      PRIMARY KEY (`roleID`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;

                    --
                    -- Table structure for table `user`
                    --

                    DROP TABLE IF EXISTS `user`;
                    CREATE TABLE `user` (
                      `userID` int NOT NULL AUTO_INCREMENT,
                      `surname` text NOT NULL,
                      `name` text NOT NULL,
                      `patronymic` text NOT NULL,
                      `number` varchar(12) NOT NULL,
                      `userLogin` text NOT NULL,
                      `userPasswd` text NOT NULL,
                      `role` int NOT NULL,
                      PRIMARY KEY (`userID`,`role`),
                      KEY `fk_user_role1_idx` (`role`),
                      CONSTRAINT `fk_user_role1` FOREIGN KEY (`role`) REFERENCES `yslada`.`role` (`roleID`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;
            ";

                    using (MySqlCommand command = new MySqlCommand(createSchemaScript, con))
                    {
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Структура базы данных восстановлена успешно.");
                    DialogResult res = MessageBox.Show("Хотите ли добавить данные в таблицы?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No)
                    {
                        Close();
                    }

                    LoadTableNames();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка восстановления структуры базы данных: {ex.Message}");
                }
            }
        }
    }
}
