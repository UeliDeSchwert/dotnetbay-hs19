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
using System.Windows.Shapes;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        private readonly IAuctionService auctionService;
        private readonly Auction auction;
        public BidView(Auction auction, IAuctionService auctionService)
        {
            InitializeComponent();
            this.auction = auction;
            DataContext = this.auction;
            this.auctionService = auctionService;
        }

        private void PlaceBid(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = double.Parse(tbAmount.Text);

                if (amount < auction.CurrentPrice)
                {
                    MessageBox.Show($"Your bid {amount} is smaller than the current price {auction.CurrentPrice}.");
                    return;
                }
                Bid b = auctionService.PlaceBid(auction, amount);

                if (b.Accepted.HasValue && b.Accepted.Value)
                {
                    MessageBox.Show($"Placed bid {b.Amount}");
                }
                else
                {
                    MessageBox.Show($"Bid not accepted!");
                }
                
                Close();
            }
            catch (FormatException ex)
            {
                tbAmount.BorderBrush = Brushes.Red;
                MessageBox.Show("Please provide a valid number");
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
