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

        private RelativeLayout? signInLayout;

        private FirebaseAuth? firebaseAuth;


        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_sign_in);

            var applicationId = GetString(Resource.String.application_id);
            var apiKey = GetString(Resource.String.api_key);

            // Init Firebase
            firebaseAuth = this.InitFirebaseAuth(applicationId, apiKey);
            // View
            signInButton = FindViewById<Button>(Resource.Id.sign_in_button);
            emailEditText = FindViewById<EditText>(Resource.Id.login_email_edit_text);
            passwordEditText = FindViewById<EditText>(Resource.Id.login_password_edit_text);
            signUpTextView = FindViewById<TextView>(Resource.Id.sign_up_clicked_text_view);
            forgotPasswordTextView = FindViewById<TextView>(Resource.Id.forgot_password_clicked_text_view);

            signInLayout = FindViewById<RelativeLayout>(Resource.Id.sign_in_layout);

            signInButton?.SetOnClickListener(this);
            signUpTextView?.SetOnClickListener(this);
            forgotPasswordTextView?.SetOnClickListener(this);
        }

        public void OnClick(View? view)
        {
            HideSoftKeyboard(this);

            switch (view?.Id)
            {
                case Resource.Id.sign_in_button:
                    var email = emailEditText!.Text;
                    var password = passwordEditText!.Text;
                    
                    if (signInLayout!.CheckRegistrationDetails(email, password))
                        this.LoginUser(firebaseAuth, email!, password!);
                    break;
                case Resource.Id.sign_up_clicked_text_view:
                    StartActivity(new Android.Content.Intent(this, typeof(SignUpActivity)));
                    //StartActivity(new Android.Content.Intent(this, typeof(DashBoardActivity)));
                    Finish();
                    break;
                case Resource.Id.forgot_password_clicked_text_view:
                    StartActivity(new Android.Content.Intent(this, typeof(ForgotPasswordActivity)));
                    Finish();
                    break;
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                StartActivity(new Android.Content.Intent(this, typeof(DashBoardActivity)));
                Finish();
            }
            else signInLayout!.ShowToast(Resource.String.login_failed);
        }
    }
}