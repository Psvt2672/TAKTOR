using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using TAKTORProject.Models;

namespace TAKTORProject.ViewModels
{
    //Model for Displaying Items from Table Product
    public partial class ProductViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Product> products;
        //create object connection 
        private SQLiteAsyncConnection conn;
        //define object for checking whethere data is loading 
        StoreInit iniD;
        public ProductViewModel()
        {
            //define path file of database
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
            //create connection 
            conn = new SQLiteAsyncConnection(dbPath);
            //create table
            conn.CreateTableAsync<Product>().Wait();
            //create object
            iniD = new StoreInit();
            GetProductsList();
        }
        //method for initialize object of Products by read from database 
        public async void GetProductsList()
        {
            //checking content in db null or not
            var content = await conn.Table<Product>().ToListAsync();
            if (content.Count == 0)
            {
                iniD.initializeStoreDataAsync();
            }
            else
            {
                //Products list is created Automatically by CommunityToolKit (ObservableProperty)
                Products = await conn.Table<Product>().ToListAsync();
            }
        }
    }
}
