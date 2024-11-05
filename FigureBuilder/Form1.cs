using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FigureBuilder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace FigureBuilder
{
    public partial class Form1 : Form
    {
        private List<double> x, y;
        private Figure figure;
        private double[] parameters;
        private Label[] labelsList;
        private TextBox[] textsList;

        public Form1()
        {
            InitializeComponent();
            parameters = new double[4];
            labelsList = new Label[4] { label1, label2, label3, label4 };
            textsList = new TextBox[4] { textBox1, textBox2, textBox3, textBox4 };
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            textBoxInfo.Text = string.Empty;
            textBoxInfo.ForeColor = Color.Red;

            if (radioButton1.Checked) // прямоугольник
            {
                if (CheckParameters(2, new bool[2] { false, false }))
                {
                    figure = new Rectangle(parameters[2], parameters[3]);
                }
            }
            else if (radioButton2.Checked) // квадрат
            {
                if (CheckParameters(1, new bool[1] { false }))
                {
                    figure = new Rectangle(parameters[3]);
                }   
            }
            else if (radioButton3.Checked) // параллелограмм
            {
                if (CheckParameters(3, new bool[3] { false, false, true }))
                {
                    figure = new Parallelogram(parameters[1], parameters[2], parameters[3]);
                }
            }
            else if (radioButton4.Checked) // ромб
            {
                if (CheckParameters(2, new bool[2] { false, true }))
                {
                    figure = new Parallelogram(parameters[2], parameters[3]);
                } 
            }
            else if (radioButton5.Checked) // трапеция
            {
                if (CheckParameters(4, new bool[4] { false, false, true, true }))
                {
                    if (parameters[2] % 360 > 180 || parameters[3] % 360 > 180)
                    {
                        textBoxInfo.Text = "Углы должны быть меньше 180 градусов";
                        return;
                    }

                    figure = new Trapeze(parameters[0], parameters[1], parameters[2], parameters[3]);
                }         
            }
            else if (radioButton6.Checked) // прямоугольная трапеция
            {
                if (CheckParameters(3, new bool[3] { false, false, true }))
                {
                    figure = new Trapeze(parameters[1], parameters[2], parameters[3]);
                }
            }
            else if (radioButton7.Checked) // круг
            {
                if (CheckParameters(1, new bool[1] { false }))
                {
                    figure = new Ellipse(parameters[3]);
                }
            }
            else if (radioButton8.Checked) // эллипс
            {
                if (CheckParameters(2, new bool[2] { false, false }))
                {
                    figure = new Ellipse(parameters[2], parameters[3]);
                }
            }
            else if (radioButton9.Checked) // треугольник
            {
                if (CheckParameters(3, new bool[3] { false, true, true }))
                {
                    if (parameters[2] % 360 + parameters[3] % 360 >= 180)
                    {
                        textBoxInfo.Text = "Сумма углов треугольника не может превышать 180 градусов";
                        return;
                    }

                    figure = new Triangle(parameters[1], parameters[2], parameters[3]);
                }
            }
            else if (radioButton10.Checked) // равнобедренный треугольник
            {
                if (CheckParameters(2, new bool[2] { false, true }))
                {
                    if (parameters[3] % 360 >= 90)
                    {
                        textBoxInfo.Text = "Углы при основании р/б треугольника должны быть меньше 90 градусов";
                        return;
                    }

                    figure = new Triangle(parameters[2], parameters[3]);
                }
            }
            else if (radioButton11.Checked) // прямоугольный треугольник
            {
                if (CheckParameters(2, new bool[2] { false, false }))
                {
                    figure = new Triangle(parameters[2], parameters[3], true);
                }
            }
            else if (radioButton12.Checked) // правильный многогранник
            {
                if (!int.TryParse(textsList[2].Text, out int n))
                {
                    textBoxInfo.Text = "Введите корректные числовые данные для поля Количество вершин";
                }
                else
                {
                    if (n<3)
                    {
                        textBoxInfo.Text = "Количество вершин должно быть не меньше 3";
                        return;
                    }

                    if (CheckParameters(1, new bool[1] { false }))
                    {
                        figure = new Polygon(n, parameters[3]);
                    }
                }
            }
            else
            {
                textBoxInfo.Text = "Непредвиденная ошибка";
                return;
            }

            if (figure != null)
            {
                if (checkBox1.Checked)
                {
                    if (!double.TryParse(textBoxRotate.Text, out double r))
                    {
                        textBoxInfo.Text = $"Введите корректный градус поворота фигуры";
                        return;
                    }
                    else
                    {
                        figure.RotationAngle = r * Math.PI / 180;
                    }
                }

                figure.CreateCoordinatesArrays(out x, out y);
                for (int i = 0; i < x.Count(); i++)
                {
                    chart.Series[0].Points.AddXY(x[i], y[i]);
                }

                double margin = 0.5;
                chart.ChartAreas[0].AxisX.Minimum = x.Min() - margin;
                chart.ChartAreas[0].AxisX.Maximum = x.Max() + margin;
                chart.ChartAreas[0].AxisY.Minimum = y.Min() - margin;
                chart.ChartAreas[0].AxisY.Maximum = y.Max() + margin;
            }
        }

        private bool CheckParameters(int activeParams, bool[] isAngle)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = 0;
            }

            for (int i = 4 - activeParams; i < parameters.Length; i++)
            {
                if (!double.TryParse(textsList[i].Text, out parameters[i]))
                {
                    textBoxInfo.Text = $"Введите корректные числовые данные для поля {labelsList[i].Text}";
                    return false;
                }
            }

            for (int i = 4 - activeParams, j = 0; i < parameters.Length; i++, j++)
            {
                if (parameters[i] <= 0 && !isAngle[j])
                {
                    textBoxInfo.Text = $"Значение поля {labelsList[i].Text} должно быть положительным";
                    return false;
                }
                if (parameters[i] % 180 == 0 && isAngle[j])
                {
                    textBoxInfo.Text = "Угол должен быть ненулевым и неразвёрнутым";
                    return false;
                }
            }

            return true;
        }

        private void ResetTextFields(int disableFields)
        {
            textBoxInfo.ForeColor = Color.Green;
            textBoxInfo.Text = $"Будет построена фигура ";

            foreach (var item in labelsList)
            {
                item.Visible = true;
            }

            foreach (var item in textsList)
            {
                item.Text = string.Empty;
                item.Visible = true;
            }

            // включаем поля для ввода параметров снизу вверх
            for (int i = 0; disableFields > 0; disableFields--, i++)
            {
                labelsList[i].Visible = false;
                textsList[i].Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(2);
            textBoxInfo.Text += "прямоугольник";
            label3.Text = "Длина";
            label4.Text = "Высота";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(3);
            textBoxInfo.Text += "квадрат";
            label4.Text = "Сторона";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(1);
            textBoxInfo.Text += "параллелограмм";
            label2.Text = "Основание";
            label3.Text = "Высота";
            label4.Text = "Левый угол при основании";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(2);
            textBoxInfo.Text += "ромб";
            label3.Text = "Сторона";
            label4.Text = "Угол";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(0);
            textBoxInfo.Text += "трапеция";
            label1.Text = "Нижнее основание";
            label2.Text = "Высота";
            label3.Text = "Левый нижний угол";
            label4.Text = "Правый нижний угол";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(1);
            textBoxInfo.Text += "прямоугольная трапеция";
            label2.Text = "Нижнее основание";
            label3.Text = "Высота";
            label4.Text = "Правый нижний угол";
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(3);
            textBoxInfo.Text += "круг";
            label4.Text = "Радиус";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(2);
            textBoxInfo.Text += "эллипс";
            label3.Text = "Большая полуось";
            label4.Text = "Малая полуось";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(1);
            textBoxInfo.Text += "треугольник";
            //label1.Text = "Левая боковая сторона";
            label2.Text = "Левая боковая сторона";
            label3.Text = "Левый угол при основании";
            label4.Text = "Правый угол при основании";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(2);
            textBoxInfo.Text += "равнобедренный треугольник";
            label3.Text = "Основание";
            label4.Text = "Угол при основании";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(2);
            textBoxInfo.Text += "прямоугольный треугольник";
            label3.Text = "Катет 1 (основание)";
            label4.Text = "Катет 2";
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextFields(2);
            textBoxInfo.Text += "правильный многогранник";
            label3.Text = "Количество вершин";
            label4.Text = "Сторона";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            BlockNonDigits(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            BlockNonDigits(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            BlockNonDigits(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            BlockNonDigits(e);
        }

        private void textBoxRotate_KeyPress(object sender, KeyPressEventArgs e)
        {
            BlockNonDigits(e);
        }

        private void BlockNonDigits(KeyPressEventArgs e)
        {
            char digit = e.KeyChar;

            if (!char.IsDigit(digit) && digit != 8 && digit != 44 && digit != 45)
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRotate.Enabled = !textBoxRotate.Enabled;
        }
    }
}
