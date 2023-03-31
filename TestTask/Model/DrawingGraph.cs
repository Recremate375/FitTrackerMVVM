using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace TestTask.Model
{
    public class DrawingGraph :INotifyPropertyChanged
    {
        ObservableCollection<double> x = new ObservableCollection<double>();
        ObservableCollection<double> y = new ObservableCollection<double>();
        ChartValues<ObservablePoint> points = new ChartValues<ObservablePoint>();
        private SeriesCollection seriesCollection { get; set; }
        public SeriesCollection SeriesCollection
        {
            get => seriesCollection;
            set { seriesCollection = value; OnPropertyChanged(); }

        }

        public void setData(List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                y.Add(people[i].Steps);
                x.Add(i);
            }
        }

        public void Print(DataForTable data, UserData userData)
        {
            List<Person> people = userData.PersonDictionary.GetValueOrDefault(data.Fio);

            setData(people);
            if (x.Count == 0 || y.Count == 0)
            {
                return;
            }
            points.Clear();
            points = new ChartValues<ObservablePoint>();
            SeriesCollection series = new SeriesCollection();

            for (int i = 0; i < people.Count; i++)
            {
                points.Add(new ObservablePoint(x[i], y[i]));
            }
            series.Add(new LineSeries
            {
                Values = points,
                Fill = Brushes.Red
            });
            SeriesCollection = series;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
