//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("tienda_app2.qrscaners.qr_inventario.xaml", "qrscaners/qr_inventario.xaml", typeof(global::tienda_app2.qrscaners.qr_inventario))]

namespace tienda_app2.qrscaners {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("qrscaners\\qr_inventario.xaml")]
    public partial class qr_inventario : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::ZXing.Net.Mobile.Forms.ZXingScannerView scanView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(qr_inventario));
            scanView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::ZXing.Net.Mobile.Forms.ZXingScannerView>(this, "scanView");
        }
    }
}