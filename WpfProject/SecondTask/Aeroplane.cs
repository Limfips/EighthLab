using System;

namespace WpfProject.SecondTask
{
    public class Aeroplane
    {
        private readonly Airport.DestinationNames _destinationName;    //Название пункта назначения
        private readonly int _airlineCode;                             //Код аэрокомпании
        private readonly int _flightNumber;                            //Номер рейса    
        private DateTime _departureTime;                               //Время отправления

        public Aeroplane(Airport.DestinationNames destinationName, int airlineCode, int flightNumber, DateTime departureTime)
        {
            _destinationName = destinationName;
            _airlineCode = airlineCode;
            _flightNumber = flightNumber;
            _departureTime = departureTime;
        }
        
        public Airport.DestinationNames DestinationName => _destinationName;
        public int AirlineCode => _airlineCode;
        public int FlightNumber => _flightNumber;
        public DateTime DepartureTime => _departureTime;
    }
}