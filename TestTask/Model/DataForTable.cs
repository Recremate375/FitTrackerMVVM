using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace TestTask.Model
{
    public class DataForTable : INotifyPropertyChanged
    {
        private string fio;
        private int averageSteps;
        private int maxSteps;
        private int minSteps;
        
        public DataForTable(string Fio, int AverageSteps, int MaxSteps, int MinSteps)
        {
            fio = Fio;
            averageSteps = AverageSteps;
            maxSteps = MaxSteps;
            minSteps = MinSteps;
        }
        public Brush Background { get; set; } = Brushes.Transparent;
        public string Fio
        {
            get { return fio; }
            set {
                fio = value;
                OnPropertyChanged("Fio");
            }
        }
        public int AverageSteps
        {
            get { return averageSteps;}
            set { averageSteps = value; OnPropertyChanged("AverageSteps"); }
        }
        public int MaxSteps
        {
            get { return maxSteps; }
            set { maxSteps = value; OnPropertyChanged("MaxSteps"); }
        }
        public int MinSteps
        {
            get { return minSteps;}
            set { minSteps = value; OnPropertyChanged("MinSteps"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
