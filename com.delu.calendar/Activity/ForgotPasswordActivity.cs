using Android.App.Admin;
using Android.Gms.Tasks;
using Android.Views;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using static Android.Views.View;

namespace Calendar;

[Activity(Label = "ForgotPassword", Theme = "@style/AppTheme")]
public class ForgotPasswordActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
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
        firebaseAuth = FirebaseAuth.GetInstance(FirebaseUtils.firebaseApp);

        // View
        emailEditText = FindViewById<EditText>(Resource.Id.forgot_email_edit_text);
        resetButton = FindViewById<Button>(Resource.Id.forgot_reset_button);
        backTextView = FindViewById<TextView>(Resource.Id.forgot_back_clickable_text_view);

        forgotPasswordLayout = FindViewById<RelativeLayout>(Resource.Id.forgot_password_layout);

        resetButton?.SetOnClickListener(this);
        backTextView?.SetOnClickListener(this);
    }

    public void OnClick(View? view)
    {
        switch(view?.Id)
        {
            case Resource.Id.forgot_reset_button:
                var email = emailEditText!.Text;
                if (forgotPasswordLayout!.CheckEmail(email))
                    this.ResetPassword(this, firebaseAuth,email!);
                break;
            case Resource.Id.forgot_back_clickable_text_view:
                StartActivity(new Android.Content.Intent(this, typeof(SignInActivity)));
                Finish();
                break;
        }
    }

    public void OnComplete(Android.Gms.Tasks.Task task)
    {
        if (task.IsSuccessful)
        {
            var message = string.Format(GetString(Resource.String.reset_password_link), emailEditText!.Text ?? "");
            forgotPasswordLayout!.ShowToast(message);
        }
        else
        {
            forgotPasswordLayout!.ShowToast(Resource.String.reset_password_failed);
        }
    }
}