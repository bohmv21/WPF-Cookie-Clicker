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
        public MainWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(Cookies);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
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
    }
}
