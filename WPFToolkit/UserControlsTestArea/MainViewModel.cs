using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserControlsTestArea
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        string busyContent = "Подождите....";

        [ICommand]
        void MakeBusy()
        {
            IsBusy = true;
            MessageBox.Show("Test started");
            IsBusy = false;
        }
    }
}
