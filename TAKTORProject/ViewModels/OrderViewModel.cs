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
        //tracking order model
        [ObservableProperty]
        List<Order> orders;
        //tracking product model
        [ObservableProperty]
        List<Product> products;
        
        private SQLiteAsyncConnection conn;
        public OrderViewModel()
        {
            //file path
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
            //create connection 
            conn = new SQLiteAsyncConnection(dbPath);
            //create table
            conn.CreateTableAsync<Order>().Wait();
            GetOrderList();
        }

        //method for get data from Order table
        public async void GetOrderList()
        {
            //getting by User id 
            int userIdTemp = Preferences.Get("Userid", 0);
            Orders = await conn.Table<Order>().Where(v => v.UserId.Equals(userIdTemp)).ToListAsync();
        }
    }
}
