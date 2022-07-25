using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WPFToolkit.NetCore.Controls;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;

namespace UserControlsTestArea
{
    public partial class MainViewModel : ViewModelBase
    {
        [ICommand]
        void RunReportWindow()
        {
            var win = new ReportWindow(new ReportViewModel());
            win.ShowDialog();
        }
    }
}
