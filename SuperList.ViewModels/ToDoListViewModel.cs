using SuperList.ViewModels.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace SuperList.ViewModels
{
    public class ToDoListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ItemViewModel> _items;
        private RelayCommand _addItemCommand;
        private RelayCommand _removeItemCommand;

        private String _name;

        public ToDoListViewModel(String name) 
        {
            Items = new ObservableCollection<ItemViewModel>();
            _name = name;
            Items.Add(new ItemViewModel());
        }

        public ToDoListViewModel() 
        {
            Items = new ObservableCollection<ItemViewModel>();
        }
        public ObservableCollection<ItemViewModel> Items 
        {
            get { return _items; }
            set 
            { 
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }

        public String Name 
        {
            get { return _name; }
            set 
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        internal XElement ToXmlElement()
		{
			XElement ToDoListElement = new XElement("ToDoList");
            XElement ToDoListName = new XElement("ListName", Name);
			XElement ToDoListItemsElement = new XElement("ToDoListItems");

            ToDoListElement.Add(ToDoListName);
			ToDoListElement.Add(ToDoListItemsElement);

			foreach (ItemViewModel listItem in _items)
				ToDoListItemsElement.Add(listItem.ToXmlElement());

			return ToDoListElement;
		}
        internal static ToDoListViewModel FromXml(XElement element)
        {
            ToDoListViewModel toDo = new ToDoListViewModel();

            XElement name = element.Element("ListName");

            toDo.Name = name.Value.ToString();

            // Loop through each of the item elements
            foreach (System.Xml.Linq.XElement itemElement in element.Descendants("Item"))
            {
                // Construct the ItemViewModel by calling the static factory method that reads it from xml
                ItemViewModel item = ItemViewModel.FromXml(itemElement);

                // Check to make sure that an item was constructed.
                if (item == null)
                    continue;

                // Add the item to this to do list
                toDo.Items.Add(item);
            }

            // return the constructed to do list
            return toDo;
        }
        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => ExecuteAddItemCommand(p as Control));
                }
                return _addItemCommand;
            }
        }
        public ICommand RemoveItemCommand
        {
            get
            {
                if (_removeItemCommand == null)
                {
                    _removeItemCommand = new RelayCommand(p => ExecuteRemoveItemCommand(p as Control));
                }
                return _removeItemCommand;
            }
        }

        private void ExecuteAddItemCommand(Control control) 
        { 
            Items.Add(new ItemViewModel());
            NotifyPropertyChanged("Items");
        }

        private void ExecuteRemoveItemCommand(Object thing) 
        {
            var button = thing as Button;
            ItemViewModel temp = button.DataContext as ItemViewModel;
            Items.Remove(temp);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
