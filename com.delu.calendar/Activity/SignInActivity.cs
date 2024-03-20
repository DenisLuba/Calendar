using Android.Gms.Tasks;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.AppCompat.App;
using Firebase;
using Firebase.Auth;
using static Calendar.Util;
using Microsoft.Maui.ApplicationModel;
using static Android.Views.View;

namespace Calendar
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme")]
    public class SignInActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        private Button? signInButton;
        private EditText? emailEditText, passwordEditText;
        private TextView? signUpTextView, forgotPasswordTextView;

        private RelativeLayout? activityMainLayout;

        public static FirebaseApp? firebaseApp;
        private FirebaseAuth? firebaseAuth;


        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_sign_in);

            var applicationId = GetString(Resource.String.application_id);
            var apiKey = GetString(Resource.String.api_key);

            // Init Firebase
            InitFirebaseAuth(applicationId, apiKey);
            // View
            signInButton = FindViewById<Button>(Resource.Id.sign_in_button);
            emailEditText = FindViewById<EditText>(Resource.Id.login_email_edit_text);
            passwordEditText = FindViewById<EditText>(Resource.Id.login_password_edit_text);
            signUpTextView = FindViewById<TextView>(Resource.Id.sign_up_clicked_text_view);
            forgotPasswordTextView = FindViewById<TextView>(Resource.Id.forgot_password_clicked_text_view);

            activityMainLayout = FindViewById<RelativeLayout>(Resource.Id.sign_in_layout);

            signInButton?.SetOnClickListener(this);
            signUpTextView?.SetOnClickListener(this);
            forgotPasswordTextView?.SetOnClickListener(this);
        }

        private void InitFirebaseAuth(string applicationId, string apiKey)
        {
            if (firebaseApp == null)
            {
                var options = new FirebaseOptions.Builder()
                .SetApplicationId(applicationId)
                .SetApiKey(apiKey)
                .Build();
                
                firebaseApp = FirebaseApp.InitializeApp(this, options);
            }
            firebaseAuth = FirebaseAuth.GetInstance(firebaseApp);
        }

        public void OnClick(View? view)
        {
            switch (view?.Id)
            {
                case Resource.Id.sign_in_button:
                    LoginUser(emailEditText?.Text ?? "", passwordEditText?.Text ?? "");
                    break;
                case Resource.Id.sign_up_clicked_text_view:
                    //StartActivity(new Android.Content.Intent(this, typeof(SignUpActivity)));
                    StartActivity(new Android.Content.Intent(this, typeof(DashBoardActivity)));
                    Finish();
                    break;
                case Resource.Id.forgot_password_clicked_text_view:
                    StartActivity(new Android.Content.Intent(this, typeof(ForgotPasswordActivity)));
                    Finish();
                    break;
            }
        }

        private void LoginUser(string email, string password)
        {
            firebaseAuth?.SignInWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this);
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                StartActivity(new Android.Content.Intent(this, typeof(DashBoardActivity)));
                Finish();
            }
            else ShowToast(activityMainLayout!, Resource.String.login_failed);
        }
    }
}