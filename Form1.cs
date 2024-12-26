using AirplaneApp7;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AirplaneApp7
{
    public partial class Form1 : Form
    {
        private List<Airplane> airplanes = new List<Airplane>();

        // Диалоги для выбора шрифта и цвета
        private FontDialog fontDialog;
        private ColorDialog colorDialog;

        public Form1()
        {
            InitializeComponent();
            comboBoxModel.Items.AddRange(new string[] { "CargoAirplane", "PassengerAirplane" });
            comboBoxModel.SelectedIndex = 0; // Устанавливаем начальное значение комбобокса

            // Инициализация диалогов
            fontDialog = new FontDialog();
            colorDialog = new ColorDialog();
        }

        private void buttonAddAirplaneWithFoto_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxAirplaneName.Text;
                string model = comboBoxModel.SelectedItem.ToString();
                int range = (int)numericUpDownRange.Value;
                decimal fuelConsumption = (decimal)numericUpDownFuelConsumption.Value;
                DateTime manufactureDate = dateTimePickerManufactureDate.Value;

                Airplane newAirplane = CreateAirplane(model, name, range, fuelConsumption, manufactureDate);

                if (newAirplane != null)
                {
                    airplanes.Add(newAirplane);
                    listBoxAirplanes.Items.Add(newAirplane.Name); // Добавляем только имя в ListBox

                    // Очистка элементов управления после добавления
                    ClearInputs();
                    MessageBox.Show("Самолет добавлен.");
                }
                else
                {
                    MessageBox.Show("Неверный тип самолета.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении самолета: {ex.Message}");
            }
        }

        private Airplane CreateAirplane(string model, string name, int range, decimal fuelConsumption, DateTime manufactureDate)
        {
            switch (model)
            {
                case "CargoAirplane":
                    int cargoCapacity = (int)numericUpDownCargoCapacity.Value;
                    string cargoType = textBoxCargoType.Text;
                    return new CargoAirplane(name, model, range, fuelConsumption, manufactureDate, null, cargoCapacity, cargoType);

                case "PassengerAirplane":
                    int passengerCapacity = (int)numericUpDownPassengerCapacity.Value;
                    bool hasBusinessClass = checkBoxHasBusinessClass.Checked;
                    return new PassengerAirplane(name, model, range, fuelConsumption, manufactureDate, null, passengerCapacity, hasBusinessClass);

                default:
                    return null; // Неверный тип самолета
            }
        }

        private void ClearInputs()
        {
            textBoxAirplaneName.Clear();
            numericUpDownCargoCapacity.Value = 0;
            textBoxCargoType.Clear();
            numericUpDownPassengerCapacity.Value = 0;
            checkBoxHasBusinessClass.Checked = false;
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Вызов метода для сохранения данных в файл
                foreach (var airplane in airplanes)
                {
                    airplane.WriteToFile(airplanes, saveFileDialog.FileName); // Вызываем метод на экземпляре класса
                }
                MessageBox.Show("Данные сохранены в файл.");
            }
        }

        private void buttonLoadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                airplanes = Airplane.ReadFromFile(openFileDialog.FileName); // Чтение данных из файла
                listBoxAirplanes.Items.Clear();
                foreach (Airplane airplane in airplanes)
                {
                    listBoxAirplanes.Items.Add(airplane.Name); // Добавляем только имя в ListBox
                }
                MessageBox.Show("Данные загружены из файла.");
            }
        }

        private void buttonShowAirplaneInfo_Click(object sender, EventArgs e)
        {
            if (listBoxAirplanes.SelectedItem != null)
            {
                string selectedAirplaneName = listBoxAirplanes.SelectedItem.ToString();
                Airplane selectedAirplane = airplanes.FirstOrDefault(a => a.Name == selectedAirplaneName);
                if (selectedAirplane != null)
                {
                    textBoxOutput.Text = selectedAirplane.GetInfo(); // Выводим информацию о выбранном самолете
                }
                else
                {
                    MessageBox.Show("Самолет не найден.");
                }
            }
        }

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModel = comboBoxModel.SelectedItem.ToString();
            ToggleVisibility(selectedModel);
        }

        private void ToggleVisibility(string selectedModel)
        {
            bool isCargo = selectedModel == "CargoAirplane";
            numericUpDownCargoCapacity.Visible = isCargo;
            textBoxCargoType.Visible = isCargo;
            textBoxCargoCapacityLabel.Visible = isCargo;
            textBoxCargoTypeLabel.Visible = isCargo;

            bool isPassengerOrBusiness = selectedModel == "PassengerAirplane";
            numericUpDownPassengerCapacity.Visible = isPassengerOrBusiness;
            checkBoxHasBusinessClass.Visible = isPassengerOrBusiness;
            textBoxPassengerCapacityLabel.Visible = isPassengerOrBusiness;
        }

        // Новый обработчик для кнопки выбора шрифта
        private void buttonChooseFont_Click(object sender, EventArgs e)
        {
            // Открыть диалоговое окно для выбора шрифта
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // Применить выбранный шрифт к текстовому полю
                listBoxAirplanes.Font = fontDialog.Font;
            }
        }

        // Новый обработчик для кнопки выбора цвета
        private void buttonChooseColor_Click(object sender, EventArgs e)
        {
            // Открыть диалоговое окно для выбора цвета
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Применить выбранный цвет к тексту
                listBoxAirplanes.ForeColor = colorDialog.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Этот метод можно оставить пустым или удалить, если не используется.
        }
    }
}
