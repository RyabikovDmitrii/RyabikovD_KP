using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yslada
{
    public class ColorSquareLabel : Control
    {
        private Color squareColor = Color.Green; // Цвет квадрата по умолчанию
        private string displayText = " - Доступно"; // Текст для отображения

        public ColorSquareLabel()
        {
            this.Width = 250; // Ширина всего контрола
            this.Height = 30; // Высота всего контрола
            this.BackColor = Color.White; // Устанавливаем непрозрачный цвет фона
        }

        // Свойство для изменения цвета квадрата
        public Color SquareColor
        {
            get => squareColor;
            set
            {
                squareColor = value;
                Invalidate(); // Перерисовываем контрол, чтобы отобразить изменения
            }
        }

        // Свойство для изменения текста
        public string DisplayText
        {
            get => displayText;
            set
            {
                displayText = value;
                Invalidate(); // Перерисовываем контрол, чтобы отобразить изменения
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Рисуем квадрат
            using (Brush squareBrush = new SolidBrush(SquareColor))
            {
                e.Graphics.FillRectangle(squareBrush, 0, 0, Height, Height); // Квадрат будет размером с высоту контрола
            }

            // Рисуем текст справа от квадрата
            using (Brush textBrush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(DisplayText, Font, textBrush, Height + 5, (Height - Font.Height) / 2); // 5 - отступ между квадратом и текстом
            }
        }
    }
}
