using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject.SecondTask
{
    public class Airport
    {
        private List<Aeroplane> _aeroplanes;

        //Тестовый список для проверки работы методов
        static List<Aeroplane> aeroplanesTEST = new List<Aeroplane>
        {
            new Aeroplane(Airport.DestinationNames.Россия,
                03,568,DateTime.Parse("22.05.2019 07:30:00")),
            new Aeroplane(Airport.DestinationNames.Россия,
                03,798,DateTime.Parse("23.05.2019 09:30:00")),
            new Aeroplane(Airport.DestinationNames.Карибы,
                03,237,DateTime.Parse("21.05.2019 08:00:00")),
            new Aeroplane(Airport.DestinationNames.Россия,
                01,657,DateTime.Parse("21.05.2019 05:30:00")),
            new Aeroplane(Airport.DestinationNames.Карибы,
                01,123,DateTime.Parse("21.05.2019 06:30:00")),
            new Aeroplane(Airport.DestinationNames.Япония,
                02,456,DateTime.Parse("22.05.2019 05:30:00")),
            new Aeroplane(Airport.DestinationNames.Карибы,
                02,234,DateTime.Parse("21.05.2019 06:00:00"))
        };

        public Airport(): this(aeroplanesTEST){}
        public Airport(List<Aeroplane> aeroplanes)
        {
            _aeroplanes = aeroplanes;
        }

        public List<Aeroplane> GetAllAeroplanes()
        {
            return _aeroplanes;
        }

        /// <summary>
        /// Получить самолёт по номеру рейса
        /// </summary>
        /// <param name="flightNumber">Номер рейса</param>
        /// <returns>Cамолёт</returns>
        public Aeroplane GetAeroplane(int flightNumber)
        {
             return _aeroplanes.Find(item => item.FlightNumber==flightNumber);
        }

        /// <summary>
        /// Получить список самолётов
        /// </summary>
        /// <param name="dateTime">В течении часа</param>
        /// <returns>Список самолётов</returns>
        public List<Aeroplane> GetAeroplanes(DateTime dateTime)
        {
            List<Aeroplane> aeroplanes = _aeroplanes.FindAll(item => item.DepartureTime >= dateTime &&
                                                                     item.DepartureTime <= dateTime.AddMinutes(60));
            aeroplanes.Sort((a1, a2) => a1.DepartureTime.CompareTo(a2.DepartureTime));
            return aeroplanes;
        }
        
        /// <summary>
        /// Получить список самолётов
        /// </summary>
        /// <param name="destinationNames">По пункту назначения</param>
        /// <returns>список самолётов</returns>
        public List<Aeroplane> GetAeroplanes(DestinationNames destinationNames)
        {
            List<Aeroplane> aeroplanes = _aeroplanes.FindAll(item => item.DestinationName == destinationNames);
            aeroplanes.Sort((a1, a2) => a1.DepartureTime.CompareTo(a2.DepartureTime));
            return aeroplanes;
        }

        public enum DestinationNames
        {
            Карибы,
            Россия,
            Япония
        }
    }
}