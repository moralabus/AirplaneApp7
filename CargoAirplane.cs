using System;
using System.Collections.Generic;
using System.IO;

namespace AirplaneApp7
{
    public class CargoAirplane : Airplane
    {
        public int CargoCapacity { get; set; }
        public string CargoType { get; set; }

        // Конструктор
        public CargoAirplane(string name, string model, int range, decimal fuelConsumption, DateTime manufactureDate, string foto, int cargoCapacity, string cargoType)
            : base(name, model, range, fuelConsumption, manufactureDate, foto)
        {
            CargoCapacity = cargoCapacity;
            CargoType = cargoType;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Грузоподъемность: {CargoCapacity} тонн, Тип груза: {CargoType}";
        }

        // Переопределение метода записи в файл
        public override void WriteToFile(List<Aircraft1> aircrafts, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var aircraft in aircrafts)
                {
                    var cargoAirplane = aircraft as CargoAirplane;
                    if (cargoAirplane != null)
                    {
                        writer.WriteLine($"Грузовой {cargoAirplane.Name},{cargoAirplane.Model},{cargoAirplane.Range},{cargoAirplane.FuelConsumption},{cargoAirplane.ManufactureDate.ToShortDateString()},{cargoAirplane.Foto},{cargoAirplane.CargoCapacity},{cargoAirplane.CargoType}");
                    }
                }
            }
        }
    }
}
