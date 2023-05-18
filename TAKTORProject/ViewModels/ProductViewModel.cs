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
    public partial class ProductViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Product> products;
        private SQLiteAsyncConnection conn;

        //public IProductService _productService;
        public ProductViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Product>().Wait();
            GetProductsList();
        }
        public async void GetProductsList()
        {
            /*var products = await _productService.GetProductsList();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }*/
            Products = await conn.Table<Product>().ToListAsync();
        }
    }
}
