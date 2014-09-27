using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperList.Model;
using System.ComponentModel;
using SuperList.ViewModels.Utilities;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;

namespace SuperList.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private Item _item;

        public Item Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private RelayCommand _setComplete;

        public ItemViewModel()
        {
            Item = new Item();
            Item.CreateDate = DateTime.Now;
            Item.DueDate = DateTime.Now;
            Item.HeatTicket = "HE...";
            Item.ItemDescription = "Add your description here";

            NotifyPropertyChanged("CreateDate");
            NotifyPropertyChanged("ItemDescription");
        }

        internal ItemViewModel(Item newItem) 
        {
            Item = newItem;
        }

        public DateTime CreateDate
        {
            get { return _item.CreateDate; }
        }

        public DateTime DueDate
        {
            get { return _item.DueDate; }
            set
            {
                NotifyPropertyChanged("DueDate");
                _item.DueDate = value;
            }
        }

        public bool IsComplete
        {
            get { return _item.IsComplete; }
            set
            {
                NotifyPropertyChanged("IsComplete");
                _item.IsComplete = value;
            }
        }

        public String HeatTicket 
        {
            get { return _item.HeatTicket; }
            set 
            {
                _item.HeatTicket = value;
                NotifyPropertyChanged("HeatTicket");
            }
        }
        public String ItemDescription
        {
            get { return _item.ItemDescription; }
            set
            {
                NotifyPropertyChanged("ItemDescription");
                _item.ItemDescription = value;
            }
        }

        public ICommand SetCompleteCommand
        {
            get
            {
                if (_setComplete == null)
                {
                    _setComplete = new RelayCommand(p => ExecuteCompleteCommand(p as Control));
                }
                return _setComplete;
            }
        }

        private void ExecuteCompleteCommand(Control control)
        {
            CheckBox temp = control as CheckBox;

            if (temp.IsChecked == true)
            {
                _item.IsComplete = true;
            }
            else
            {
                _item.IsComplete = false;
            }

            NotifyPropertyChanged("IsComplete");
        }

        /// <summary>
        /// Transform the state of this object into xml that represents the full
        /// state of this object
        /// </summary>
        /// <returns>Reference to a created XElement</returns>
        internal XElement ToXmlElement()
        {
            return
            // Create the Item node
            new System.Xml.Linq.XElement("Item",
            // Create each child node of the Item
            new XElement("CreateDate", CreateDate.ToString()) 
            , new XElement("ItemDescription", ItemDescription)
            , new XElement("IsComplete", IsComplete.ToString())
            , new XElement("DueDate", DueDate.ToString()));
        }

        /// <summary>
        /// Factory method that creates an instance of an ItemViewModel
        /// from an XElement loaded from a file
        /// </summary>
        /// <param name="element">Reference to an XElement from a valid XML file</param>
        /// <returns>New instance of ItemViewModel based on the element</returns>
        internal static ItemViewModel FromXml(XElement element)
        {
            Item item = new Item();

            foreach (XElement child in element.Elements())
            {
                string elementName = child.Name.ToString();

                if (string.Compare(elementName, "CreateDate", true) == 0)
                {
                    DateTime createDate;

                    if (DateTime.TryParse(child.Value, out createDate))
                        item.CreateDate = createDate;
                }
                else if (string.Compare(elementName, "ItemDescription", true) == 0)
                {
                    item.ItemDescription = child.Value;
                }
                else if (string.Compare(elementName, "IsComplete", true) == 0)
                {
                    bool isComplete;

                    if (bool.TryParse(child.Value, out isComplete))
                        item.IsComplete = isComplete;
                }
                else if (string.Compare(elementName, "DueDate", true) == 0)
                {
                    DateTime dueDate;

                    if (DateTime.TryParse(child.Value, out dueDate))
                        item.DueDate = dueDate;
                }
            }
            return new ItemViewModel(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
