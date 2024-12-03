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
        private string connectionString = ConnectionStr.connectionString();
        public InportData()
        {
            InitializeComponent();
            LoadTableNames();
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
                // Обновляем строку подключения, чтобы использовать кодировку utf8mb4
                string connectionStringWithCharset = $"{connectionString};charset=utf8mb4";

                using (MySqlConnection con = new MySqlConnection(connectionStringWithCharset))
                {
                    con.Open();

                    // Используем StreamReader с кодировкой UTF-8
                    using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                    {
                        string line;
                        reader.ReadLine(); // Пропускаем заголовок
                        int columnCount = GetColumnCount(con, tableName);

                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(';');
                            if (values.Length != columnCount)
                            {
                                MessageBox.Show($"Несоответствие количества параметров: ожидалось {columnCount}, но получено {values.Length}.");
                                return;
                            }

                            // Формируем команду INSERT с обработкой кавычек
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
            //using (MySqlConnection con = new MySqlConnection(connectionString))
            //{
            //    con.Open();
            //    try
            //    {
            //        string createSchemaScript = @"
  
            //";

            //        using (MySqlCommand command = new MySqlCommand(createSchemaScript, con))
            //        {
            //            command.ExecuteNonQuery();
            //        }

            //        MessageBox.Show("Структура базы данных восстановлена успешно.");
            //        LoadTableNames();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Ошибка восстановления структуры базы данных: {ex.Message}");
            //    }
            //}
        }
    }
}
