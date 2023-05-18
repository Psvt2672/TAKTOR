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

            try
            {
                records = ser.GetRec().Result;
            }
            catch(AggregateException ex)
{
                // Handle or log the aggregate exception and its inner exceptions
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine($"An error occurred: {innerException.Message}");
                }
            }







        }
    }
}
