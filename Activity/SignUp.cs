using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using Google.Android.Material.Snackbar;
using static Android.Views.View;

namespace Calendar;

[Activity(Label = "SignUp", Theme = "@style/AppTheme")]
public class SignUp : AppCompatActivity, IOnClickListener
{
    private Button? registerButton;
    private EditText? emailEditText, passwordEditText, repeatPasswordEditText;
    private TextView? loginTextView;

    private RelativeLayout? signUpLayout;

    private FirebaseAuth? auth;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.sign_up);

        // Init Firebase
        auth = FirebaseAuth.GetInstance(MainActivity.firebaseApp);

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
        HideSoftKeyboard();

        switch (view?.Id)
        {
            case Resource.Id.register_button:
                {
                    if (CheckPasswords())
                        RegisterUser(emailEditText?.Text ?? "", passwordEditText?.Text ?? "");
                    else Snackbar.Make(signUpLayout!, Resource.String.passwords_do_not_match, Snackbar.LengthLong).Show();
                }
                break;
            case Resource.Id.sign_in_clicked_text_view:
                StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
                Finish();
                break;
        }
    }

    private bool CheckPasswords()
        => CheckPassword(passwordEditText?.Text) 
        && CheckPassword(repeatPasswordEditText?.Text)
        && DoPasswordsMatch(passwordEditText?.Text, repeatPasswordEditText?.Text);

    private bool CheckPassword(string? password) => password != null && password.Length > 0;

    private bool DoPasswordsMatch(string? password, string? repeatPassword) =>
        password == repeatPassword;

    private void RegisterUser(string email, string password)
    {

    }

    public void HideSoftKeyboard()
    {
        var currentFocus = CurrentFocus;
        if (currentFocus != null)
        {
            InputMethodManager? inputMethodManager = GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager?.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
        }
    }
}