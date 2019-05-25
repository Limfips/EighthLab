using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string[] valuesAsList = Enum.GetNames(typeof(Airport.DestinationNames));
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
                MatrixControl.ItemsSource = new FirstTask.FirstTask().GetValues(n,m);
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        /// <summary>
        /// Второе задание. Поиск по времени
        /// </summary>
        private void SearchByDepartureTimeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBox.Items.Clear();
                DateTime dateTime = GetDateTime();
                List<Aeroplane> aeroplanes = airport.GetAeroplanes(dateTime);
                foreach (var aeroplane in aeroplanes)
                {
                    ListBox.Items.Add(aeroplane);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Введите значения заново");
            }
        }
        /// <summary>
        /// Второе задание. Поиск по номеру рейса
        /// </summary>
        private void SearchByFlightNumberButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Aeroplane aeroplane = airport.GetAeroplane(Convert.ToInt32(FlightNumberTextBox.Text));
                string text = airport.GetInformationAeroplane(aeroplane);
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
        /// <summary>
        /// Второе задание. Поиск по пункту назначения
        /// </summary>
        private void SearchByDestinationNameButton_OnClick(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            Airport.DestinationNames destinationNames = (Airport.DestinationNames)ComboBox.SelectedIndex;
            List<Aeroplane> aeroplanes = airport.GetAeroplanes(destinationNames);
            foreach (var aeroplane in aeroplanes)
            {
                ListBox.Items.Add(aeroplane);
            } 
        }

        private DateTime GetDateTime()
        {
            if (DatePicker.SelectedDate.HasValue)
            {
                DateTime dateTime = (DateTime) DatePicker.SelectedDate;
                dateTime = dateTime.AddHours((int)HoursComboBox.SelectedItem-dateTime.Hour);
                dateTime = dateTime.AddMinutes((int)MinutesComboBox.SelectedItem-dateTime.Minute);
                return dateTime;
            }
            throw new NullReferenceException();
        }
        
        private void ListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(airport.GetInformationAeroplane((Aeroplane) ListBox.SelectedItem));
        }

        private void ThirdTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            BeforeTextBox.Clear();
            AfterTextBox.Clear();
            var removeAllComments = new ThirdTask.ThirdTask().RemoveAllComments(PathTextBox.Text);
            if (removeAllComments.Count == 2)
            {
                BeforeTextBox.Text = removeAllComments[0];
                AfterTextBox.Text = removeAllComments[1];
            }
        }
    }
}