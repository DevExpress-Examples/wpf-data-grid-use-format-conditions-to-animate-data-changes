using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.ComponentModel;
using System;
using DevExpress.Xpf.Grid;
using System.Windows.Controls;

namespace DXGrid_ConditionalFormatting {
    public partial class Window1 : Window {
        public Window1() {
            InitializeComponent();
            grid.ItemsSource = Products.GetData();

        }
    }

    public class ColumnRowIndexesCellValueConverter : MarkupExtension, IMultiValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(values[0] is GridColumn && values[1] != null) { 
                GridColumn column = (GridColumn)values[0];
                object cellValue = TypeDescriptor.GetProperties(values[1])[column.FieldName].GetValue(values[1]);
                Dictionary<string, ValueState> dict = (Dictionary<string, ValueState>)TypeDescriptor.GetProperties(values[1])["States"].GetValue(values[1]);
                ValueState state;
                dict.TryGetValue(column.FieldName, out state);
                if (state == ValueState.Changed) {
                    return Brushes.Red;
                } else {
                    return Brushes.Transparent;
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class Products {
        public static List<Product> GetData() {
            List<Product> data = new List<Product>();
            data.Add(new Product() {
                ProductName = "Ipoh Coffee",
                UnitPrice = 12,
                UnitsOnOrder = 2
            });
            data.Add(new Product() {
                ProductName = "Outback Lager",
                UnitPrice = 25,
                UnitsOnOrder = 2
            });
            data.Add(new Product() {
                ProductName = "Boston Crab Meat",
                UnitPrice = 18,
                UnitsOnOrder = 3332
            });
            data.Add(new Product() {
                ProductName = "Konbu",
                UnitPrice = 24,
                UnitsOnOrder = 111
            });
            return data;
        }
    }
    public enum ValueState { Original, Changed }
    public class Product : INotifyPropertyChanged {
        string productName;
        public string ProductName {
            get { return productName; }
            set {
                if (productName == value) {
                    return;
                }
                if (productName == null) {
                    productName = value;
                    return;
                }
                productName = value;
                RaisePropertyChanged("ProductName");
                States["ProductName"] = ValueState.Changed;
                RaisePropertyChanged("States");
            }
        }

        int unitPrice;
        bool unitPriceIsAssignedFirstTime = true;
        public int UnitPrice {
            get { return unitPrice; }
            set {
                if (unitPrice == value) {
                    return;
                }
                unitPrice = value;
                if (!unitPriceIsAssignedFirstTime) {
                    RaisePropertyChanged("UnitPrice");
                    States["UnitPrice"] = ValueState.Changed;
                    RaisePropertyChanged("States");
                } else {
                    unitPriceIsAssignedFirstTime = false;
                }
            }
        }

        int unitsOnOrder;
        bool unitsOnOrderIsAssignedFirstTime = true;
        public int UnitsOnOrder {
            get { return unitsOnOrder; }
            set {
                if (unitsOnOrder == value)
                    return;
                unitsOnOrder = value;
                if (!unitsOnOrderIsAssignedFirstTime) {
                    RaisePropertyChanged("UnitsOnOrder");
                    States["UnitsOnOrder"] = ValueState.Changed;
                    RaisePropertyChanged("States");
                } else {
                    unitsOnOrderIsAssignedFirstTime = false;
                }
            }
        }


        public Dictionary<string, ValueState> States { get; private set; }
        public Product() {
            States = new Dictionary<string, ValueState>();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        void RaisePropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
