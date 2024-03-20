using AndroidX.AppCompat.App;

namespace Calendar;

[Activity(Label = "DashBoard", Theme = "@style/AppTheme")]
public class DashBoardActivity : AppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_dash_board);


    }
}