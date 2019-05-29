using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WpfProject.SecondTask;
using File = WpfProject.ThirdTask.File;

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
                if (aeroplanes.Count != 0)
                {
                    foreach (var aeroplane in aeroplanes)
                    {
                        ListBox.Items.Add(aeroplane.FlightNumber);
                    }
                }
                else
                {
                    MessageBox.Show("В данный момент самолёты на ваше время отсутствуют");
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
                ListBox.Items.Clear();
                Aeroplane aeroplane = airport.GetAeroplane(Convert.ToInt32(FlightNumberTextBox.Text));
                string text = aeroplane.ToString();
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
                ListBox.Items.Add(aeroplane.FlightNumber);
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
            MessageBox.Show(airport.GetAeroplane((int) ListBox.SelectedItem).ToString());
        }

        private void ClearFile_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder text = new StringBuilder(BeforeTextBox.Text);
//            text.Append(BeforeTextBox.Text);
            AfterTextBox.Text = ThirdTask.ThirdTask.RemoveAllComments(text);
            File.Save(PathTextBox.Text,AfterTextBox.Text);
        }

        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            BeforeTextBox.Clear();
            AfterTextBox.Clear();

            if (PathTextBox.Text.Length > 0)
            {
                try
                {
                    string content = File.Load(PathTextBox.Text);
                    BeforeTextBox.Text = content;
                }
                catch (FileLoadException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}