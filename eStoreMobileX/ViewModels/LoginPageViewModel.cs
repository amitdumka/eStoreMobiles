﻿using eStoreMobile.Core.DataViewModel;
using eStoreMobileX.Validators;
using eStoreMobileX.Validators.Rules;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eStoreMobileX.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
            this.Email.Value = "Admin@estore.in";
            this.password.Value = "Admin";
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public ValidatableObject<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {
            bool isEmailValid = this.Email.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isEmailValid && isPasswordValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Password = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                DoLogin();
            }
        }
        private async void DoLogin()
        {
            UserDataModel vm = new UserDataModel();
            try
            {
                var result = await vm.VerifyLoginAsync(Email.Value, Password.Value);
                if (result != null)
                {
                    ApplicationContext.EmpId = (int)result.EmployeeId;
                    ApplicationContext.IsLoggedIn = true;
                    ApplicationContext.Role = result.UserType;
                    ApplicationContext.UserName = result.UserName;
                    ApplicationContext.StoreId = result.StoreId;

                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    ApplicationContext.EmpId = -1;// (int) result.EmployeeId;
                    ApplicationContext.IsLoggedIn = false;
                    ApplicationContext.Role = EmpType.Others;
                    ApplicationContext.UserName = "";
                    Debug.WriteLine(" Login Failed");
                    await Application.Current.MainPage.DisplayAlert("Error", "Username and password not found!", "Ok");
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("LPVM:\t" + e.Message);
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do Something
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}