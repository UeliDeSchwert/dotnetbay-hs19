using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.Entity;
using DotNetBay.Interfaces;

namespace DotNetBay.Data.EF
{
    public class EFMainRepository : IMainRepository
    {
        public MainDbContext DbContext { get; }
        public EFMainRepository()
        {
            DbContext = new MainDbContext();
        }

        public IQueryable<Auction> GetAuctions()
        {
            return DbContext.Auctions;
        }

        public IQueryable<Member> GetMembers()
        {
            return DbContext.Members;
        }

        public Auction Add(Auction auction)
        {
            return DbContext.Auctions.Add(auction);
        }

        //todo: check if update is correct
        public Auction Update(Auction auction)
        {
            Auction a = DbContext.Auctions.First(u => u.Id == auction.Id);
            if (a == null)
            {
                return auction;
            }

            
            a.Seller = auction.Seller;
            a.ActiveBid = auction.ActiveBid;
            a.Bids = auction.Bids;
            a.Winner = auction.Winner;
            a.CloseDateTimeUtc = auction.CloseDateTimeUtc;
            a.CurrentPrice = auction.CurrentPrice;
            a.EndDateTimeUtc = auction.EndDateTimeUtc;
            a.Description = auction.Description;
            a.Image = auction.Image;
            a.IsClosed = auction.IsClosed;
            a.IsRunning = auction.IsRunning;
            a.Title = auction.Title;
            a.StartDateTimeUtc = auction.StartDateTimeUtc;
            a.StartPrice = auction.StartPrice;
            return a;
        }

        public Bid Add(Bid bid)
        {
            return DbContext.Bids.Add(bid);
        }

        public Bid GetBidByTransactionId(Guid transactionId)
        {
            return DbContext.Bids.First(c => c.TransactionId == transactionId);
        }

        public Member Add(Member member)
        {
            return DbContext.Members.Add(member);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
