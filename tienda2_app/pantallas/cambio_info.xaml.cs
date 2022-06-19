using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tienda2_app.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cambio_info : ContentPage
    {
        public cambio_info()
        {
            InitializeComponent();
        }

        private void btn_empezar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new qrscaners.qr_cambiar_info());
        }
    }
}