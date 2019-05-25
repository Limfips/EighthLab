using System;

namespace WpfProject.SecondTask
{
    public class Aeroplane
    {
    private readonly Airport.DestinationNames _destinationName; //Название пункта назначения
    private readonly int _airlineCode; //Код аэрокомпании
    private readonly int _flightNumber; //Номер рейса    
    private DateTime _departureTime; //Время отправления

    public Aeroplane(Airport.DestinationNames destinationName, int airlineCode, int flightNumber,
        DateTime departureTime)
    {
        _destinationName = destinationName;
        _airlineCode = airlineCode;
        _flightNumber = flightNumber;
        _departureTime = departureTime;
    }

    public Airport.DestinationNames DestinationName
    {
        get { return _destinationName; }
    }

    public int AirlineCode
    {
        get { return _airlineCode; }
    }

    public int FlightNumber
    {
        get { return _flightNumber; }
    }

    public DateTime DepartureTime
    {
        get { return _departureTime; }
    }

    public override string ToString()
    {
        return _flightNumber.ToString();
    }
    }
}