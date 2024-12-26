using System;
using System.Collections.Generic;
using System.IO;

namespace AirplaneApp7
{
    public class PassengerAirplane : Airplane
    {
        public int PassengerCapacity { get; set; }
        public bool HasBusinessClass { get; set; }

        // Конструктор
        public PassengerAirplane(string name, string model, int range, decimal fuelConsumption, DateTime manufactureDate, string foto, int passengerCapacity, bool hasBusinessClass)
            : base(name, model, range, fuelConsumption, manufactureDate, foto)
        {
            PassengerCapacity = passengerCapacity;
            HasBusinessClass = hasBusinessClass;
        }

        // Переопределение метода для получения информации

        public override string ToString()
        {
            return $"{base.ToString()}, Вместимость: {PassengerCapacity} пассажиров, Бизнес-класс: {(HasBusinessClass ? "Есть" : "Нет")}";
        }

        // Переопределение метода записи в файл
        public override void WriteToFile(List<Aircraft1> aircrafts, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var aircraft in aircrafts)
                {
                    var passengerAirplane = aircraft as PassengerAirplane;
                    if (passengerAirplane != null)
                    {
                        writer.WriteLine($"Пассажирский {passengerAirplane.Name},{passengerAirplane.Model},{passengerAirplane.Range},{passengerAirplane.FuelConsumption},{passengerAirplane.ManufactureDate.ToShortDateString()},{passengerAirplane.Foto},{passengerAirplane.PassengerCapacity},{passengerAirplane.HasBusinessClass}");
                    }
                }
            }
        }
    }
}
