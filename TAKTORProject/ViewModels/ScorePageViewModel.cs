using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAKTORProject.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TAKTORProject.ViewModels
{
    public partial class ScorePageViewModel : ObservableObject
    {
        //create object for record services
        ServiceRecord ser = new ServiceRecord();
        //list of Record model : tracking change by using ObservableProperty 
        [ObservableProperty]
        List<Record> records;
        public ScorePageViewModel() {

            LoadObjectRecord();
            
        }
        //loading list of Record then create each
        public async void LoadObjectRecord()
        { 
            try
            {
                //using .GetRect() for get values in database
                Records = await ser.GetRec();
            }
            catch(Exception ee) {
                Console.WriteLine(ee.Message);
            }
            
        }
    }
}
