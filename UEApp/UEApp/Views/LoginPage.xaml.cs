using System;
using Xamarin.Forms;

/* Login page
 * TODO: Check against real email/pass
 *       Complete OnRegister and OnForgot
 */

namespace UEApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Checks a lot of things to see if they are okay, if everything passes it pops the page
        private void OnSignIn(object sender, EventArgs e)
        {
            // Cleans out past warnings
            Warn_Layout.Children.Clear();

            Boolean Valid_Email = Email_Validate();
            Boolean Valid_Password = Password_Validate();

            if (Valid_Email == true && Valid_Password == true)
            {
                Navigation.PushModalAsync(new MainPage() { Title = "Home" });
                return;
            }
            else
            {
                Email_Image_Feedback_Layout.IsVisible = true;
                Pass_Image_Feedback_Layout.IsVisible = true;
                Warn_Layout.IsVisible = true;
            }
        }

        private void OnProceedAnon(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage() { Title = "Home" });
        }

        private void OnRegister(object sender, EventArgs e)
        {
            //TODO
        }

        private void OnForgot(object sender, EventArgs e)
        {
            //TODO
        }

        private void OnShowPass(object sender, EventArgs e)
        {
            if (Pass.IsPassword)
            {
                Pass.IsPassword = false;
            }
            else
            {
                Pass.IsPassword = true;
            }
        }

        private Boolean Email_Validate()
        {
            // TODO: check against actual Email
            String testEmail = "donkey@mail.uc.edu";

            if (String.IsNullOrEmpty(Email.Text))
            {
                Label Email_Warn_Label = new Label { Text = "Please enter your email", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(Email_Warn_Label);
                Email_Check.IsVisible = false;
                Email_X.IsVisible = true;
                return false;
            }
            else if (Email.Text.Equals(testEmail))
            {
                Email_X.IsVisible = false;
                Email_Check.IsVisible = true;
                return true;
            }
            else
            {
                Label Email_Wrong_Label = new Label { Text = "Email incorrect", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(Email_Wrong_Label);
                Email_Check.IsVisible = false;
                Email_X.IsVisible = true;
                return false;
            }
        }

        private Boolean Password_Validate()
        {
            // TODO: check against actual password
            String testPass = "dicktracy007";

            if (String.IsNullOrEmpty(Pass.Text))
            {
                Label Pass_Warn_Label = new Label { Text = "Please enter your password", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(Pass_Warn_Label);
                Pass_Check.IsVisible = false;
                Pass_X.IsVisible = true;
                return false;
            }
            else if (Pass.Text.Equals(testPass))
            {
                Pass_X.IsVisible = false;
                Pass_Check.IsVisible = true;
                return true;
            }
            else
            {
                Label Pass_Wrong_Label = new Label { Text = "Password incorrect", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(Pass_Wrong_Label);
                Pass_Check.IsVisible = false;
                Pass_X.IsVisible = true;
                return false;
            }
        }
    }
}