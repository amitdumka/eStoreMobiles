using eStore.Shared.Models.Users;
using eStoreMobile.Core.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserEditor : ContentPage
    {
        UserViewModel viewModel;
        bool isNew = true;//Set False when loading data for edit
        public UserEditor()
        {
            InitializeComponent();
            viewModel = new UserViewModel();
            viewModel.isNew = isNew;
            dataForm.DataObject = viewModel;
        }
        private void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
        {
            //if ( e.DataFormItem.Name == "StaffName" )
            //    e.DataFormItem.IsReadOnly = true;
            if (e.DataFormItem.Name == "UserId" || e.DataFormItem.Name == "StoreId")
                e.Cancel = true;
            if (e.DataFormItem.Name == "IsActive")
                e.DataFormItem.Editor = "Switch";
            if (e.DataFormItem.Name == "IsEmployee")
                e.DataFormItem.Editor = "Switch";

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            dataForm.Validate();
            dataForm.Commit();
            viewModel = dataForm.DataObject as UserViewModel;

            if (viewModel != null && await viewModel.SaveData())
            {
                _ = DisplayAlert("Alert", "User is saved!", "Ok");
            }
            else
            {
                _ = DisplayAlert("Error", "User is not saved! Please Try again after checking", "Ok");
            }
        }
    }
    public class UserViewModel
    {
        private User user { get; set; }
        public bool isNew = true;//Set False when loading data for edit

        public User User
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public UserViewModel()
        {
            this.user = new User();
        }

        public async Task<bool> SaveData()
        {
            UserDataModel dm = new UserDataModel();
            if (await dm.SaveUser(user, isNew) > 0)
                return true;
            else return false;

        }
    }
}