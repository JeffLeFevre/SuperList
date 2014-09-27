using SuperList.ViewModels.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace SuperList.ViewModels
{
    public class SuperListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ToDoListViewModel> _mainList;

        private RelayCommand _addToDoListCommand;
        private RelayCommand _removeToDoListCommand;
        private RelayCommand _loadFileCommand;
        private RelayCommand _saveFileCommand;
        public SuperListViewModel()
        {
            _mainList = new ObservableCollection<ToDoListViewModel>();
        }

        public ObservableCollection<ToDoListViewModel> MainList
        {
            get { return _mainList; }
            set
            {
                _mainList = value;
                NotifyPropertyChanged("MainList");
            }
        }
        private static string DefaultFile
        {
            get { return Path.Combine(DirectoryHandler.GetPersonalPath(), "SuperList.xml"); }
        }
        public ICommand AddToDoListCommand
        {
            get
            {
                if (_addToDoListCommand == null)
                {
                    _addToDoListCommand = new RelayCommand(p => ExecuteAddToDoListCommand(p as Control));
                }
                return _addToDoListCommand;
            }
        }
        public ICommand RemoveToDoListCommand
        {
            get
            {
                if (_removeToDoListCommand == null)
                {
                    _removeToDoListCommand = new RelayCommand(p => ExecuteRemoveToDoListCommand(p as Object));
                }
                return _removeToDoListCommand;
            }
        }

        public ICommand LoadFileCommand
        {
            get
            {
                if (_loadFileCommand == null)
                {
                    _loadFileCommand = new RelayCommand(p => ExecuteLoadFileCommand(p as Control));
                }
                return _loadFileCommand;
            }
        }

        public ICommand SaveFileCommand 
        {
            get 
            {
                if (_saveFileCommand == null) 
                {
                    _saveFileCommand = new RelayCommand(p => ExecuteSaveFileCommand());
                }
                return _saveFileCommand;
            }
        }

        private void ExecuteSaveFileCommand()
        {
            Save();
        }

        private void ExecuteLoadFileCommand(Control control)
        {
            try
            {
                LoadFromFile();
            }
            catch(FileNotFoundException) 
            {
                MessageBox.Show("Default file not found.");
            }
        }
        
        private void ExecuteAddToDoListCommand(Control control)
        {
            this.MainList.Add(new ToDoListViewModel("New List"));
            NotifyPropertyChanged("MainList");
        }

        private void ExecuteRemoveToDoListCommand(Object thing)
        {
            var button = thing as Button;
            ToDoListViewModel temp = button.DataContext as ToDoListViewModel;
            MainList.Remove(temp);
        }
        public void Save(string targetXmlFileName = null)
        {
            // Create the xml document
            XDocument doc = new XDocument();
            XElement root = new XElement("SuperList");

            // Add the root XML element
            doc.Add(root);

            // Add all of the list records to the 
            // SuperList root
            foreach (ToDoListViewModel toDo in _mainList)
            {
                root.Add(toDo.ToXmlElement());
            }

            var settings = new XmlWriterSettings()
            {
                CloseOutput = true,
                Indent = true,
                Encoding = Encoding.UTF8
            };

            // Write the xml file
            using (XmlWriter writer = XmlWriter.Create(
                targetXmlFileName ?? DefaultFile, settings))
            {
                doc.Save(writer);
            }
        }
        private void LoadFromFile(string sourceXmlFileName = null)
        {
            // Check to see if the file has been specified, and if not,
            // assign it to the default value
            if (string.IsNullOrEmpty(sourceXmlFileName))
                sourceXmlFileName = DefaultFile;

            // Check to make sure the file is there
            
            if(!File.Exists(sourceXmlFileName))
            {
                throw new FileNotFoundException("The default file could not be found.");
            }

            // Create a TextReader to read the xml file. Use the 'using' 
            // keyword so that the limited resource (file handle) will 
            // automatically be cleaned up when the file is closed.
            using (TextReader reader = new StreamReader(sourceXmlFileName, Encoding.UTF8))
            {
                // Create an xml document and assign it to a local variable
                XDocument doc = XDocument.Load(reader);

                // Loop through each of the lists in the file,
                // and add them to the list of lists (_mainList) tracked in this instance
                foreach (XElement element in doc.Descendants("ToDoList"))
                {
                    ToDoListViewModel toDo = ToDoListViewModel.FromXml(element);

                    _mainList.Add(toDo);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
