using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;

namespace WPF_Cookie_Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double CookiesPerClick = 1;
        public double CookiesPerSecond;
        public double TotalCookies;

        public double ClickPrice = 15;
        public double GrandmaPrice = 100;
        public double FactoryPrice = 1000;
        public MainWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(Cookies);
            thread.SetApartmentState(ApartmentState.STA);
            SaveLoad();
            thread.Start();
        }

        private void SaveLoad()
        {
            if (File.Exists("../Save.txt"))
            {
               TotalCookies = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(0));
               CookiesPerSecond = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(1));
               ClickPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(2));
               GrandmaPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(3));
               FactoryPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(4));
               txtCLick.Text = Convert.ToString(Convert.ToInt32(ClickPrice));
               txtGrandma.Text = Convert.ToString(Convert.ToInt32(GrandmaPrice));
               txtFactory.Text = Convert.ToString(Convert.ToInt32(FactoryPrice));
            }
        }

        private void Cookies()
        {
            while (true)
            {

                Thread.Sleep(100);


                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    TotalCookies = TotalCookies + CookiesPerSecond/10;
                    txtCookiesPs.Text = Convert.ToString(CookiesPerSecond);
                    txtTotalCookies.Text = Convert.ToString(Convert.ToInt32(TotalCookies));

                    if (TotalCookies >= ClickPrice)
                    {
                        btnClick.IsEnabled = true;
                    } else
                    {
                        btnClick.IsEnabled = false;
                    }

                    if (TotalCookies >= GrandmaPrice)
                    {
                        btnGrandma.IsEnabled = true;
                    }
                    else
                    {
                        btnGrandma.IsEnabled = false;
                    }
                    if (TotalCookies >= FactoryPrice)
                    {
                        btnFactory.IsEnabled = true;
                    }
                    else
                    {
                        btnFactory.IsEnabled = false;
                    }
                }));
            }
        }

        private void Cookie_Click(object sender, RoutedEventArgs e)
        {
            TotalCookies = TotalCookies + CookiesPerClick;
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            if (TotalCookies >= ClickPrice)
            {
                CookiesPerSecond = CookiesPerSecond + 0.1;
                TotalCookies = TotalCookies - ClickPrice;
                double Price = ClickPrice * 20 / 100;
                ClickPrice = ClickPrice + Price;
                txtCLick.Text = Convert.ToString(Convert.ToInt32(ClickPrice));
            }
        }

        private void btnGrandma_Click(object sender, RoutedEventArgs e)
        {
            if (TotalCookies >= GrandmaPrice)
            {
                CookiesPerSecond = CookiesPerSecond + 1;
                TotalCookies = TotalCookies - GrandmaPrice;
                double Price = GrandmaPrice * 15 / 100;
                GrandmaPrice = GrandmaPrice + Price;
                txtGrandma.Text = Convert.ToString(Convert.ToInt32(GrandmaPrice));
            }
        }

        private void btnFactory_Click(object sender, RoutedEventArgs e)
        {
            if (TotalCookies >= FactoryPrice)
            {
                CookiesPerSecond = CookiesPerSecond + 10;
                TotalCookies = TotalCookies - FactoryPrice;
                double Price = FactoryPrice * 15 / 100;
                FactoryPrice = FactoryPrice + Price;
                txtFactory.Text = Convert.ToString(Convert.ToInt32(FactoryPrice));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            string[] Data = {txtTotalCookies.Text, txtCookiesPs.Text, txtCLick.Text, txtGrandma.Text, txtFactory.Text }; 
            File.WriteAllLines("../Save.txt", Data);            
        }
    }
}
