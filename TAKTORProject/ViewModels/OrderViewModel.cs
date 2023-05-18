using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAKTORProject.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace TAKTORProject.ViewModels
{
    //Model for Displaying Items from Table Order
    public partial class OrderViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Order> orders;
        [ObservableProperty]
        List<Product> products;
        private SQLiteAsyncConnection conn;
        public OrderViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Order>().Wait();
            GetOrderList();
        }

        public async void GetOrderList()
        {
            int userIdTemp = Preferences.Get("Userid", 0);
            Orders = await conn.Table<Order>().Where(v => v.UserId.Equals(userIdTemp)).ToListAsync();
        }
    }
}
