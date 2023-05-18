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
        private SQLiteAsyncConnection conn;
        public ProductViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Product>().Wait();
            GetProductsList();
        }
        public async void GetProductsList()
        {
            //Products list is created Automatically by CommunityToolKit (ObservableProperty)
            Products = await conn.Table<Product>().ToListAsync();
        }
    }
}
