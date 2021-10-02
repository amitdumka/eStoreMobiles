using eStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class UserEditor : ContentPage
    {
        public UserEditor()
        {
            InitializeComponent ();
        }
        private void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
        {
            //if ( e.DataFormItem.Name == "StaffName" )
            //    e.DataFormItem.IsReadOnly = true;
            if ( e.DataFormItem.Name == "UserId" || e.DataFormItem.Name == "StoreId" )
                e.Cancel = true;
                if ( e.DataFormItem.Name == "IsActive" )
                e.DataFormItem.Editor = "Switch";
            if ( e.DataFormItem.Name == "IsEmployee" )
                e.DataFormItem.Editor = "Switch";

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            dataForm.Commit ();
            
        }
    }
    public class UserViewModel
    {
        private User user { get; set; }

        public User User
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public UserViewModel()
        {
            this.user = new User ();
        }
    }
}