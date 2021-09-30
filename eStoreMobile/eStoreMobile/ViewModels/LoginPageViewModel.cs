using eStoreMobile.Validators;
using eStoreMobile.Validators.Rules;
using eStoreMobile.Views;
using eStoreMobile.Core.DataViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;
using System.Net.Http;
using eStore.Shared.Models.Users;
using System.Collections.Generic;

namespace eStoreMobile.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve (AllMembers = true)]
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
            this.InitializeProperties ();
            this.AddValidationRules ();
            this.LoginCommand = new Command  (this.LoginClicked);
            this.SignUpCommand = new Command (this.SignUpClicked);
            this.ForgotPasswordCommand = new Command (this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command (this.SocialLoggedIn);
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
                if ( this.password == value )
                {
                    return;
                }

                this.SetProperty (ref this.password, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command  LoginCommand { get; set; }

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
            bool isEmailValid = this.Email.Validate ();
            bool isPasswordValid = this.Password.Validate ();
            return isEmailValid && isPasswordValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Password = new ValidatableObject<string> ();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.Password.Validations.Add (new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            //if ( this.AreFieldsValid () )
            {
                
                UserViewModel vm = new UserViewModel ();
                //JsonSerializerOptions serializerOptions= new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //    WriteIndented = true
                //}; ;
                //HttpClient client = new HttpClient();
                //Debug.WriteLine("Refreshing user datat");
                //List<User> Items = new List<User>();

                //Uri uri = new Uri(string.Format("https://www.aprajitaretails.in/api/users", string.Empty));
                //try
                //{
                //    var res= client.GetAsync(uri);
                //    res.ConfigureAwait(true);
                //    HttpResponseMessage response = res.GetAwaiter().GetResult();
                //    Debug.WriteLine(response.StatusCode.ToString());
                //    if (response.IsSuccessStatusCode)
                //    {
                //        Debug.WriteLine("got success dude");

                //        var con=  response.Content.ReadAsStringAsync();
                //        con.ConfigureAwait(true);
                //        string content = con.GetAwaiter().GetResult();

                //        Items = JsonSerializer.Deserialize<List<User>>(content, serializerOptions);
                //    }
                //    else
                //    {
                //        Debug.WriteLine("got error dueue");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(@"\tERROR {0}", ex.Message);
                //}

                try
                {
                    var result = vm.VerifyLoginAsync(Email.Value, Password.Value);
                    result.ConfigureAwait(true);
                    var status = result.GetAwaiter().GetResult();

                    if (status)
                    {
                        Shell.Current.GoToAsync(nameof(TestPage1));
                    }
                    else
                    {
                        Debug.WriteLine(" Login Failed");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine("LPVM:\t" + e.Message);
                }

            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do Something
             Shell.Current.GoToAsync ("//SignUpPage");
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            // Do something
            Shell.Current.GoToAsync ("//ForgotPasswordPageWithGradient");
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