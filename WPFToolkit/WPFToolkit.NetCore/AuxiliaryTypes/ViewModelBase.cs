using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolkit.NetCore.AuxiliaryTypes
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        bool isBusy = false;
    }
}
