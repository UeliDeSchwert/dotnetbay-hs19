using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.Entity;
using DotNetBay.Data.Provider.FileStorage;
using DotNetBay.Interfaces;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly IMainRepository MainRepository;
        public readonly IAuctionRunner AuctionRunner;
        private readonly IAuctionService auctionService;

        public ObservableCollection<Auction> Auctions { get; }

        public MainWindow()
        {
            InitializeComponent();

            this.MainRepository = new FileSystemMainRepository("mainRepository.txt");
            this.AuctionRunner = new AuctionRunner(this.MainRepository);
            this.AuctionRunner.Start();

            //AddRandomData();

            DataContext = this;

            this.auctionService = new AuctionService(this.MainRepository, new SimpleMemberService(this.MainRepository));
            this.Auctions = new ObservableCollection<Auction>(this.auctionService.GetAll());
        }

        void AddRandomData()
        {
            var memberService = new SimpleMemberService(this.MainRepository);
            var service = new AuctionService(this.MainRepository, memberService);
            if (!service.GetAll().Any())
            {
                var me = memberService.GetCurrentMember();
                service.Save(new Auction
                {
                    Title = "My First Auction",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 72,
                    Seller = me
                });
            }
        }
    }
}