using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WpfProject.SecondTask;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        Airport airport = new Airport();
        public MainWindow()
        {
            InitializeComponent(); 
            
            //Задаю минуты и часы в соответствующие поля------
            List<int> hours = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i);
            }
            List<int> minutes = new List<int>();
            for (int i = 0; i < 60; i++)
            {
                minutes.Add(i);
            }
            HoursComboBox.ItemsSource = hours;
            MinutesComboBox.ItemsSource = minutes;
            //------------------------------------------------
            //Задаю значения в ComboBox-----------------------
            var valuesAsList = Enum.GetNames(typeof(Airport.DestinationNames));
            foreach (var item in valuesAsList)
            {
                ComboBox.Items.Add(item);
            }
            //------------------------------------------------
        }
        /// <summary>
        /// Первое задание
        /// </summary>
        private void FirstTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(NTextBox.Text);
                int m = Convert.ToInt32(MTextBox.Text);
                Lst.ItemsSource = new FirstTask.FirstTask().GetValues(n,m);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        /// <summary>
        /// Второе задание
        /// </summary>
        private void SecondTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            //По дате
            if ((Button) sender == FirstButton)
            {
                foreach (var aeroplane in airport.GetAeroplanes(GetDateTime()))
                {
                    ListBox.Items.Add(aeroplane.FlightNumber);
                }
            }
            // По номеру рейса
            if ((Button) sender == SecondButton)
            {
                try
                {
                    Aeroplane aeroplane = airport.GetInformationAeroplane(Convert.ToInt32(FlightNumberTextBox.Text));
                    string text = $"Название пункта назначения: {aeroplane.DestinationName}\n" +
                                  $"Код аэрокомпании: {aeroplane.AirlineCode}\n" +
                                  $"Номер рейса: {aeroplane.FlightNumber}\n" +
                                  $"Время отправления: {aeroplane.DepartureTime}";
                    MessageBox.Show(text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Введите значение еще раз");
                }
                catch(NullReferenceException)
                {
                    MessageBox.Show("В данный момент данные рейсы отсутствуют");
                }
            }
            //По названию пункта назначения
            if ((Button) sender == ThirdButton)
            {
                IOrderedEnumerable<Aeroplane> aeroplanes = airport.GetAeroplanes((Airport.DestinationNames)Enum
                    .Parse(typeof(Airport.DestinationNames),ComboBox.SelectedItem.ToString()));
                foreach (var aeroplane in aeroplanes)
                {
                    ListBox.Items.Add(aeroplane.FlightNumber);
                } 
            }

        }
        public DateTime? GetDateTime()
        {
            try
            {
                if (DatePicker.SelectedDate != null)
                {
                    DateTime dateTime = (DateTime) DatePicker.SelectedDate;
                    dateTime = dateTime.AddHours((int)HoursComboBox.SelectedItem-dateTime.Hour);
                    dateTime = dateTime.AddMinutes((int)MinutesComboBox.SelectedItem-dateTime.Minute);
                    return dateTime;
                }

                throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Введите значения заново");
                return null;
            }
        }
        
        private void ListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Aeroplane aeroplane = airport.GetInformationAeroplane((int)ListBox.SelectedItem);
            string text = $"Название пункта назначения: {aeroplane.DestinationName}\n" +
                          $"Код аэрокомпании: {aeroplane.AirlineCode}\n" +
                          $"Номер рейса: {aeroplane.FlightNumber}\n" +
                          $"Время отправления: {aeroplane.DepartureTime}";
            MessageBox.Show(text);
        }

        private void ThirdTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
           new ThirdTask.ThirdTask().RemoveAllComments(PathTextBox.Text);
        }
    }
}