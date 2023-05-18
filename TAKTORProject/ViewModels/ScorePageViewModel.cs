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
        ServiceRecord ser = new ServiceRecord();
        [ObservableProperty]
        List<Record> records;
        public ScorePageViewModel() {

            LoadObjectRecord();
            
        }
        public async void LoadObjectRecord()
        { 
            try
            {
                Records = await ser.GetRec();

                foreach (Record record in Records)
                    Console.WriteLine(record.Score);
            }
            catch(Exception ee) {
                Console.WriteLine(ee.Message);
            }
            
        }
    }
}
