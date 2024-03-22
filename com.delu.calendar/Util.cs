using Android.Content;
using System.ComponentModel.DataAnnotations;
using Android.Gms.Common;
using Android.Views;
using Android.Views.InputMethods;
using Google.Android.Material.Snackbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    internal static class Util
    {
        public static void ShowToast(this View view, int messageId) =>
            Snackbar.Make(view, messageId, BaseTransientBottomBar.LengthLong).Show();

        public static void ShowToast(this View view, string message) =>
            Snackbar.Make(view, message, BaseTransientBottomBar.LengthLong).Show();

        public static bool CheckEmail(this View view, string? email)
        {
            if (email == null || email.Length == 0)
            {
                view.ShowToast(Resource.String.enter_email);
                return false;
            }
            if (!new EmailAddressAttribute().IsValid(email))
            {
                view.ShowToast(Resource.String.email_is_invalid);
                return false;
            }
            return true;
        }

        public static bool CheckPassword(this View view, string? password)
        {
            if (password == null || password.Length == 0)
            {
                view.ShowToast(Resource.String.enter_password);
                return false;
            }
            if (password.Length < 6)
            {
                view.ShowToast(Resource.String.short_password);
                return false;  
            }
            return true;
        }

        public static bool CheckRegistrationDetails(this View view, string? email, string? password) =>
            view.CheckEmail(email) && view.CheckPassword(password);

        public static bool CheckPasswords(this View view, string? password, string? repeatPassword)
        {
            if (password != repeatPassword)
            {
                view.ShowToast(Resource.String.passwords_do_not_match);
                return false;
            }
            return view.CheckPassword(password);
        }

        public static void HideSoftKeyboard(Activity activity)
        {
            var currentFocus = activity.CurrentFocus;
            if (currentFocus != null)
            {
                InputMethodManager? inputMethodManager = activity.GetSystemService(Context.InputMethodService) as InputMethodManager;
                inputMethodManager?.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }
    }
}
