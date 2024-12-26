using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace AirplaneApp7
{
    public class Airplane : Aircraft1
    {
        // Статическое свойство для цвета фона
        public static string BackColor
        {
            get
            {
                int currentDay = DateTime.Now.DayOfWeek.GetHashCode();
                return (currentDay % 2 == 1) ? "LightPink" : "LightBlue";
            }
        }

        // Конструктор
        public Airplane(string name, string model, int range, decimal fuelConsumption, DateTime manufactureDate, string foto)
            : base(name, model, range, fuelConsumption, manufactureDate, foto)
        {
        }
        // Статический метод для чтения данных о самолетах из текстового файла
        public static List<Airplane> ReadFromFile(string filePath)
        {
            List<Airplane> airplanes = new List<Airplane>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 6)
                    {
                        airplanes.Add(new Airplane(
                            parts[0].Trim(),
                            parts[1].Trim(),
                            int.Parse(parts[2]),
                            decimal.Parse(parts[3]),
                            DateTime.Parse(parts[4]),
                            parts[5].Trim()));
                    }
                }
            }
            return airplanes;
        }
        // Нестатический метод для записи в файл
        public virtual void WriteToFile(List<Airplane> airplanes, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Airplane airplane in airplanes)
                {
                    writer.WriteLine($"{airplane.Name},{airplane.Model},{airplane.Range},{airplane.FuelConsumption},{airplane.ManufactureDate.ToShortDateString()},{airplane.Foto}");
                }
            }
        }
        // Переопределение метода для получения информации о самолете
        public override string GetInfo()
        {
            return $"Самолет: {Name}, Модель: {Model}, Дальность полета: {Range} км, Потребление горючего: {FuelConsumption} л/100км, Дата производства: {ManufactureDate.ToShortDateString()}";
        }

        // Переопределение метода записи в файл
        public override void WriteToFile(List<Aircraft1> aircrafts, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var aircraft in aircrafts)
                {
                    writer.WriteLine($"{aircraft.Name},{aircraft.Model},{aircraft.Range},{aircraft.FuelConsumption},{aircraft.ManufactureDate.ToShortDateString()},{aircraft.Foto}");
                }
            }
        }
    }
}
