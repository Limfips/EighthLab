using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject.SecondTask
{
    public class Airport
    {
        private List<Aeroplane> _aeroplanes;

        static List<Aeroplane> aeroplanesTEST = new List<Aeroplane>
        {
            new Aeroplane(Airport.DestinationNames.Россия,
                01,657,DateTime.Parse("21.05.2019 05:30:00")),
            new Aeroplane(Airport.DestinationNames.Карибы,
                01,123,DateTime.Parse("21.05.2019 06:30:00")),
            new Aeroplane(Airport.DestinationNames.Япония,
                02,456,DateTime.Parse("22.05.2019 05:30:00")),
            new Aeroplane(Airport.DestinationNames.Япония,
                02,234,DateTime.Parse("21.05.2019 06:00:00")),
            new Aeroplane(Airport.DestinationNames.Россия,
                03,568,DateTime.Parse("22.05.2019 07:30:00")),
            new Aeroplane(Airport.DestinationNames.Карибы,
                03,237,DateTime.Parse("21.05.2019 08:00:00")),
            new Aeroplane(Airport.DestinationNames.Россия,
                03,798,DateTime.Parse("23.05.2019 09:30:00"))
        };

        public Airport(): this(aeroplanesTEST){}
        public Airport(List<Aeroplane> aeroplanes)
        {
            _aeroplanes = aeroplanes;
        }

        public List<Aeroplane> GetAllAeroplane()
        {
            return _aeroplanes;
        }

        public Aeroplane GetInformationAeroplane(int flightNumber)
        {
             Aeroplane aeroplane = _aeroplanes.Find(item => item.FlightNumber==flightNumber);
             return aeroplane;
        }
        public IOrderedEnumerable<Aeroplane> GetAeroplanes(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                List<Aeroplane> aeroplanes = new List<Aeroplane>();
                DateTime newDateTime = (DateTime) dateTime;
                foreach (var aeroplane in _aeroplanes)
                {
                    if (aeroplane.DepartureTime >= newDateTime 
                        && aeroplane.DepartureTime <= newDateTime.AddMinutes(60))
                    {
                        aeroplanes.Add(aeroplane);
                    }
                }
                
                var sortedAeroplanes = from aeroplane in aeroplanes
                    orderby aeroplane.DepartureTime  
                    select aeroplane;

                return sortedAeroplanes;
            }

            throw new ArgumentNullException();
        }

        public IOrderedEnumerable<Aeroplane> GetAeroplanes(DestinationNames destinationNames)
        {
            List<Aeroplane> aeroplanes = new List<Aeroplane>();

            foreach (var aeroplane in _aeroplanes)
            {
                if (aeroplane.DestinationName == destinationNames)
                {
                    aeroplanes.Add(aeroplane);
                }
            }
            
            var sortedAeroplanes = from aeroplane in aeroplanes
                orderby aeroplane.DepartureTime  
                select aeroplane;

            return sortedAeroplanes;
        }

        public enum DestinationNames
        {
            Карибы,
            Россия,
            Япония
        }
    }
}