using Android.Gms.Tasks;
using Android.Views;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using static Android.Views.View;
using static Calendar.Util;

namespace Calendar;

[Activity(Label = "SignUp", Theme = "@style/AppTheme")]
public class SignUpActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
{
    private Button? registerButton;
    private EditText? emailEditText, passwordEditText, repeatPasswordEditText;
    private TextView? loginTextView;

    private RelativeLayout? signUpLayout;

    private FirebaseAuth? firebaseAuth;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_sign_up);

        // Init Firebase
        firebaseAuth = FirebaseAuth.GetInstance(SignInActivity.firebaseApp);

        // View
        registerButton = FindViewById<Button>(Resource.Id.register_button);
        emailEditText = FindViewById<EditText>(Resource.Id.sign_up_email_edit_text);
        passwordEditText = FindViewById<EditText>(Resource.Id.sign_up_email_edit_text);
        repeatPasswordEditText = FindViewById<EditText>(Resource.Id.sign_up_repeat_password_edit_text);
        loginTextView = FindViewById<TextView>(Resource.Id.sign_in_clicked_text_view);

        signUpLayout = FindViewById<RelativeLayout>(Resource.Id.sign_up_layout);

        registerButton?.SetOnClickListener(this);
        loginTextView?.SetOnClickListener(this);
    }

    public void OnClick(View? view)
    {
        HideSoftKeyboard(this);

        switch (view?.Id)
        {
            case Resource.Id.register_button:
                var email = emailEditText!.Text;
                var password = passwordEditText!.Text;
                var repeatPassword = repeatPasswordEditText!.Text;

                if (signUpLayout!.CheckPasswords(password, repeatPassword)
                    && signUpLayout!.CheckRegistrationDetails(email, password))
                    this.RegisterUser(firebaseAuth, email!, password!);
                break;
            case Resource.Id.sign_in_clicked_text_view:
                StartActivity(new Android.Content.Intent(this, typeof(SignInActivity)));
                Finish();
                break;
        }
    }

    public void OnComplete(Android.Gms.Tasks.Task task)
    {
        if (task.IsSuccessful)
        {
            signUpLayout!.ShowToast(Resource.String.register_successfully);
            StartActivity(new Android.Content.Intent(this, typeof(DashBoardActivity)));
            Finish();
        }
        else signUpLayout!.ShowToast(Resource.String.register_failed);
    }
}