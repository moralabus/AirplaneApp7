using AirplaneApp7;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AirplaneApp7
{
    public abstract class AirplaneBase
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public int Range { get; set; }
        public decimal FuelConsumption { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Foto { get; set; }

        // Абстрактный метод получения информации
        public abstract string GetInfo();

        // Абстрактный метод записи в файл
        public abstract void WriteToFile(List<AirplaneBase> airplanes, string filePath);

        // Конструктор
        protected AirplaneBase(string name, string model, int range, decimal fuelConsumption, DateTime manufactureDate, string foto)
        {
            Name = name;
            Model = model;
            Range = range;
            FuelConsumption = fuelConsumption;
            ManufactureDate = manufactureDate;
            Foto = foto;
        }

        // Переопределение метода ToString
        public override string ToString()
        {
            return $"{Name} ({Model}), Дальность: {Range} км, Потребление: {FuelConsumption} л/100км";
        }

        // Статический метод для чтения из файла
        public static List<AirplaneBase> ReadFromFile(string filePath)
        {
            List<AirplaneBase> airplanes = new List<AirplaneBase>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 6)
                    {
                        string name = parts[0].Trim();
                        string model = parts[1].Trim();
                        int range = int.Parse(parts[2]);
                        decimal fuelConsumption = decimal.Parse(parts[3]);
                        DateTime manufactureDate = DateTime.Parse(parts[4]);
                        string foto = parts[5].Trim();

                        // В зависимости от модели создаем разные типы самолетов
                        if (model.Contains("Cargo"))
                        {
                            // Пример, можно добавить дополнительные поля для грузового типа
                            airplanes.Add(new CargoAirplane(name, model, range, fuelConsumption, manufactureDate, foto, 0, string.Empty));
                        }
                        else if (model.Contains("Passenger"))
                        {
                            // Пример, можно добавить дополнительные поля для пассажирского типа
                            airplanes.Add(new PassengerAirplane(name, model, range, fuelConsumption, manufactureDate, foto, 0, false));
                        }
                        else
                        {
                            airplanes.Add(new Airplane(name, model, range, fuelConsumption, manufactureDate, foto));
                        }
                    }
                }
            }
            return airplanes;
        }
    }
}

