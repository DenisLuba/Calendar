using Android.Gms.Tasks;
using Android.Views;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using static Android.Views.View;
using static Calendar.Util;

namespace Calendar;

[Activity(Label = "DashBoard", Theme = "@style/AppTheme")]
public class DashBoardActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
{
    private TextView? welcomeTextView;
    private EditText? passwordEditText, repeatPasswordEditText;
    private Button? newPasswordButton, logoutButton;

    private RelativeLayout? dashBoardLayout;

    FirebaseAuth? firebaseAuth;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_dash_board);

        // Init Firebase
        firebaseAuth = FirebaseAuth.GetInstance(FirebaseUtils.firebaseApp);

        // View
        welcomeTextView = FindViewById<TextView>(Resource.Id.welcome_text_view);
        passwordEditText = FindViewById<EditText>(Resource.Id.welcome_password_edit_text);
        repeatPasswordEditText = FindViewById<EditText>(Resource.Id.welcome_repeat_password_edit_text);
        newPasswordButton = FindViewById<Button>(Resource.Id.welcome_new_password_button);
        logoutButton = FindViewById<Button>(Resource.Id.welcome_logout_button);

        dashBoardLayout = FindViewById<RelativeLayout>(Resource.Id.dash_board_layout);

        newPasswordButton?.SetOnClickListener(this);
        logoutButton?.SetOnClickListener(this);

        welcomeTextView!.Text = $"Welcome,\n{firebaseAuth?.CurrentUser?.Email.Split("@")[0] ?? "User"}";
    }

    public void OnClick(View? view)
    {
        HideSoftKeyboard(this);

        switch(view?.Id)
        {
            case Resource.Id.welcome_new_password_button:
                var password = passwordEditText!.Text;
                var repeatPassword = repeatPasswordEditText!.Text;

                if (dashBoardLayout!.CheckPasswords(password, repeatPassword))
                    this.ChangePassword(firebaseAuth, password);
                break;
            case Resource.Id.welcome_logout_button:
                this.LogoutUser(firebaseAuth);
                if (firebaseAuth?.CurrentUser == null)
                    dashBoardLayout!.ShowToast("current user is null");
                break;
        }
    }

    public void OnComplete(Android.Gms.Tasks.Task task)
    {
        if (task.IsSuccessful)
            dashBoardLayout!.ShowToast(Resource.String.password_changed);
        else dashBoardLayout!.ShowToast(Resource.String.password_not_changed);
    }
}