using Firebase.Auth;
using Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Tasks;
using System.Runtime.CompilerServices;

namespace Calendar
{
    internal static class FirebaseUtils
    {
        public static FirebaseApp? firebaseApp;

        public static FirebaseAuth InitFirebaseAuth(this Context context, string applicationId, string apiKey)
        {
            if (firebaseApp == null)
            {
                var options = new FirebaseOptions.Builder()
                .SetApplicationId(applicationId)
                .SetApiKey(apiKey)
                .Build();

                firebaseApp = FirebaseApp.InitializeApp(context, options);
            }
            return FirebaseAuth.GetInstance(firebaseApp);
        }

        public static void LoginUser(this IOnCompleteListener listener, FirebaseAuth? firebaseAuth, string email, string password)
        {
            firebaseAuth?.SignInWithEmailAndPassword(email, password)
                .AddOnCompleteListener(listener);
        }

        public static void RegisterUser(this IOnCompleteListener listener, FirebaseAuth? firebaseAuth, string email, string password)
        {
            firebaseAuth?.CreateUserWithEmailAndPassword(email, password)
                .AddOnCompleteListener(listener);
        }

        public static void ChangePassword(this IOnCompleteListener listener, FirebaseAuth? firebaseAuth, string? password)
        {
            firebaseAuth?.CurrentUser // FirebaseUser
                .UpdatePassword(password)
                .AddOnCompleteListener(listener);
        }

        public static void LogoutUser(this Activity activity, FirebaseAuth? firebaseAuth)
        {
            firebaseAuth?.SignOut();
            if (firebaseAuth?.CurrentUser == null)
            {
                activity.StartActivity(new Android.Content.Intent(activity, typeof(SignInActivity)));
                activity.Finish();
            }
        }

        public static void ResetPassword(this IOnCompleteListener listener, Activity activity, FirebaseAuth? firebaseAuth, string email)
        {
            firebaseAuth?.SendPasswordResetEmail(email)
                .AddOnCompleteListener(activity, listener);
        }
    }
}
