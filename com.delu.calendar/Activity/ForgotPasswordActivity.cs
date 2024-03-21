using Android.Views;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using static Android.Views.View;

namespace Calendar;

[Activity(Label = "ForgotPassword", Theme = "@style/AppTheme")]
public class ForgotPasswordActivity : AppCompatActivity, IOnClickListener
{
    private EditText? emailEditText;
    private Button? resetButton;
    private TextView? backTextView;

    private RelativeLayout? forgotPasswordLayout;

    private FirebaseAuth? firebaseAuth;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_forgot_password);

        // Init Firebase
        firebaseAuth = FirebaseAuth.GetInstance(SignInActivity.firebaseApp);

        // View
        emailEditText = FindViewById<EditText>(Resource.Id.forgot_email_edit_text);
        resetButton = FindViewById<Button>(Resource.Id.forgot_reset_button);
        backTextView = FindViewById<TextView>(Resource.Id.forgot_back_clickable_text_view);

        resetButton?.SetOnClickListener(this);
        backTextView?.SetOnClickListener(this);
    }

    public void OnClick(View? view)
    {
        switch(view?.Id)
        {
            case Resource.Id.forgot_reset_button:

                break;
            case Resource.Id.forgot_back_clickable_text_view:

                break;
        }
    }
}