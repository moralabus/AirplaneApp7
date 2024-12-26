using System;
using System.Collections.Generic;

namespace AirplaneApp7
{
    public abstract class Aircraft1
    {
        // Общие свойства для всех самолетов
        public string Name { get; set; }
        public string Model { get; set; }
        public int Range { get; set; }
        public decimal FuelConsumption { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Foto { get; set; }

        // Абстрактный метод для получения информации о самолете
        public abstract string GetInfo();

        // Абстрактный метод для записи в файл
        public abstract void WriteToFile(List<Aircraft1> aircrafts, string filePath);

        // Конструктор
        protected Aircraft1(string name, string model, int range, decimal fuelConsumption, DateTime manufactureDate, string foto)
        {
            Name = name;
            Model = model;
            Range = range;
            FuelConsumption = fuelConsumption;
            ManufactureDate = manufactureDate;
            Foto = foto;
        }
    }
}
