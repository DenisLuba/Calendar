using Android.Content;
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
        public static void ShowToast(View view, int messageId) =>
            Snackbar.Make(view, messageId, BaseTransientBottomBar.LengthLong).Show();

        public static void ShowToast(View view, string message) =>
            Snackbar.Make(view, message, BaseTransientBottomBar.LengthLong).Show();

        public static bool CheckPasswords(View view, string password, string repeatPassword)
        {
            if (password != repeatPassword)
            {
                ShowToast(view, Resource.String.passwords_do_not_match);
                return false;
            }
            if (password.Length == 0)
            {
                ShowToast(view, Resource.String.enter_password);
                return false;
            }
            return true;
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
