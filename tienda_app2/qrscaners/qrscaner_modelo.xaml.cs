using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tienda_app2.qrscaners
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class qrscaner_modelo : ContentPage
    {
        public qrscaner_modelo()
        {
            InitializeComponent();
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {

        }
    }
}