using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TestTask.Model;
using Prism.Commands;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Media;
using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel.Design.Serialization;
using System.Threading;

namespace TestTask.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private UserData _data;
        private DataForTable selectedObject;
        private WorkWithJson workWithJson;
        private DrawingGraph graph;
        public PlotModel MyModel { get; set; }
        public ObservableCollection<DataForTable> dataForGrid { get; set; }

        public DataForTable SelectedObject
        {
            get { return selectedObject; }
            set { selectedObject = value; OnPropertyChanged("SelectedObject"); }
        }
        public ApplicationViewModel()
        {
            _data = new UserData();
            graph = new DrawingGraph();
            workWithJson = new WorkWithJson();
            MyModel = new PlotModel();
            ReadFromFile();
            dataForGrid = new ObservableCollection<DataForTable>();
            AddAllValueInDataForGrid();
        }
        
        private void AddAllValueInDataForGrid()
        {
            foreach (var key in _data.PersonDictionary)
            {
                int averageSteps = _data.getAverageScoreForUser(key.Key);
                int maxSteps = _data.getMaxScoreForUser(key.Key);
                int minSteps = _data.getMinScoreForUser(key.Key);
                DataForTable data = new DataForTable(key.Key, averageSteps, maxSteps, minSteps);
                dataForGrid.Add(data);
            }
            ChangeDataGridRowColor();
        }
        private void ChangeDataGridRowColor()
        {
            
            foreach (var item in dataForGrid)
            {
                if (item.AverageSteps * 1.2 < item.MaxSteps)
                {
                    item.Background = Brushes.ForestGreen;
                }
                else if (item.AverageSteps * 0.8 > item.MinSteps)
                {
                    item.Background = Brushes.DarkRed;
                }
            }
        }
        private void ReadFromFile()
        {
            for (int i = 0; i < 30; i++)
            {
                int j = i + 1;
                workWithJson.ReadFromFile(@"TestData//day" + j + ".json", _data);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        private DelegateCommand<object> _mouseCommand;
        private DelegateCommand<object> _saveInFileCommand;
        public ICommand SaveInFile
        {
            get { return _saveInFileCommand ?? (_saveInFileCommand = new DelegateCommand<object>(SaveInJsonFile)); }
        }
        public ICommand ChoosedPerson 
        {
            get { return _mouseCommand ?? (_mouseCommand = new DelegateCommand<object>(DrawGraphic)); }
        }

        private void DrawGraphic(object e) 
        {
            MyModel.Series.Clear();
            var series1 = new OxyPlot.Series.LineSeries();
            List<Person> persons = new List<Person>();

            persons = _data.PersonDictionary.GetValueOrDefault(SelectedObject.Fio);
            for(int i = 0; i < persons.Count; i++)
            {
                series1.Points.Add(new DataPoint(i, persons[i].Steps));
            }
            MyModel.Series.Add(series1);
            MyModel.InvalidatePlot(true);
        }
        private void SaveInJsonFile(object e)
        {
            ExportDataForJson dataForJson = new ExportDataForJson();
            dataForJson.selectedUser = SelectedObject;
            dataForJson.people = _data.PersonDictionary.GetValueOrDefault(SelectedObject.Fio);
            string filePath = SelectedObject.Fio + ".json";
            workWithJson.SaveInJsonFile(filePath, dataForJson);
        }
        public SeriesCollection SeriesCollection
        { 
            get => graph.SeriesCollection;
        }
    }
}
