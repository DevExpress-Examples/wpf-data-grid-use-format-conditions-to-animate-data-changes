using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace DXGrid_ConditionalFormatting {
    public class ViewModel : ViewModelBase {
        public ObservableCollection<Product> Products { get; set; }

        DispatcherTimer updateTimer = new DispatcherTimer();
        public ViewModel() {
            PopulateData();
            SetUpdateTimer();
        }
        protected void PopulateData() {
            Products = new ObservableCollection<Product>();
            Random randomPriceValue = new Random();
            for (int i = 0; i < 30; i++) {
                decimal oldPrice = ((decimal)randomPriceValue.Next(1000) / 10);
                decimal newPrice = ((decimal)randomPriceValue.Next(10000) / 10);
                Products.Add(new Product() { Name = "Product" + i, OldPrice = oldPrice, NewPrice = newPrice, DeltaPrice = newPrice - oldPrice, IsAvailable = (i % 2 == 0) });
            }
        }
        private void SetUpdateTimer() {
            updateTimer.Tick += updateTimer_Tick;
            updateTimer.Interval = new TimeSpan(0, 0, 2);
            updateTimer.Start();
        }
        void updateTimer_Tick(object sender, EventArgs e) {
            Random rnd = new Random();
            int updateProductNumber;
            for (int i = 0; i < 3; i++) {
                updateProductNumber = rnd.Next(Products.Count);
                Products[updateProductNumber].IsAvailable = !Products[updateProductNumber].IsAvailable;
            }
            for (int i = 0; i < 5; i++) {
                updateProductNumber = rnd.Next(Products.Count);

                decimal oldPrice = ((decimal)rnd.Next(1000) / 10);
                decimal newPrice = ((decimal)rnd.Next(10000) / 10);

                Products[updateProductNumber].DeltaPrice = newPrice - oldPrice;

                Products[updateProductNumber].OldPrice = oldPrice;
                Products[updateProductNumber].NewPrice = newPrice;
            }
        }
    }

    public class Product : BindableBase {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public decimal OldPrice { get { return GetValue<decimal>(); } set { SetValue(value); } }
        public decimal NewPrice { get { return GetValue<decimal>(); } set { SetValue(value); } }
        public decimal DeltaPrice { get { return GetValue<decimal>(); } set { SetValue(value); } }
        public bool IsAvailable { get { return GetValue<bool>(); } set { SetValue(value); } }
    }
}
