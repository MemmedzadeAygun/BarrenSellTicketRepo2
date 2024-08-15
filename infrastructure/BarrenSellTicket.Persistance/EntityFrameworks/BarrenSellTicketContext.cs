using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks
{
    public class BarrenSellTicketContext:DbContext
    {

        public BarrenSellTicketContext(DbContextOptions<BarrenSellTicketContext> optionsBuilder)
            :base(optionsBuilder)
        {

        }

        public DbSet<Address> Addresses =>this.Set<Address>();
        public DbSet<BankAccount> BankAccounts=>this.Set<BankAccount>();
        public DbSet<Coupon> Coupons=>this.Set<Coupon>();
        public DbSet<Event> Events=>this.Set<Event>();
        public DbSet<EventCategory> EventCategories=>this.Set<EventCategory>();
        public DbSet<EventType> EventTypes=>this.Set<EventType>();
        public DbSet<Image> Images=>this.Set<Image>();
        public DbSet<Order> Orders=>this.Set<Order>();
        public DbSet<OrderCoupon> OrderCoupons=>this.Set<OrderCoupon>();
        public DbSet<OrganizerDetail> OrganizerDetails=>this.Set<OrganizerDetail>();
        public DbSet<Payouts> Payouts=>this.Set<Payouts>();
        public DbSet<Ticket> Tickets=>this.Set<Ticket>();
        public DbSet<Users> Users => this.Set<Users>();
        public DbSet<Role> Role => this.Set<Role>();
        public DbSet<UserRole> UserRole => this.Set<UserRole>();
        public DbSet<Customer> Customer => this.Set<Customer>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<EventCategory>()
                .HasData(
                    new EventCategory { Id=1,Name="Arts"},
                    new EventCategory { Id=2,Name="Business"},
                    new EventCategory { Id=3,Name="Education and Training"},
                    new EventCategory { Id=4,Name="Family and Friends"},
                    new EventCategory { Id=5,Name="Food and Drink"},
                    new EventCategory { Id=6,Name="Government and Politics"},
                    new EventCategory { Id=7,Name="Health and Wellbeing"},
                    new EventCategory { Id=8,Name="Hobbies and Interest"},
                    new EventCategory { Id=9,Name="Music and Theater"},
                    new EventCategory { Id=10,Name="Science and Technology"},
                    new EventCategory { Id=11,Name="Sports and Fitness"},
                    new EventCategory { Id=12,Name="Sports and Fitness"},
                    new EventCategory { Id=13,Name="Travel and Outdoor"},
                    new EventCategory { Id=14,Name="Community and Culture"},
                    new EventCategory { Id=15,Name="Coaching and Consulting"},
                    new EventCategory { Id=16,Name="Others"}
                );


            modelBuilder
                .Entity<EventType>()
                .HasData(
                    new EventType { Id = 1, Name = "Online" },
                    new EventType { Id = 2, Name = "Vanue" }
                );


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
                
        }

    }
}
