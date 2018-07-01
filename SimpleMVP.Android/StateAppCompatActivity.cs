using System;
using Android.OS;
using Android.Support.V7.App;

namespace SimpleMVP.Droid
{
    public class StateAppCompatActivity : AppCompatActivity, IStateView
    {
        public StateAppCompatActivity()
        {
        }

        /// <summary>
        /// Flag to ensure the app is running betweeb OnResume and OnPause
        /// Fragment should only be shown when it is true
        /// </summary>
        /// <value><c>true</c> if is visible and running; otherwise, <c>false</c>.</value>
        public bool IsVisibleAndRunning { get; private set; }

        protected override void OnResume()
		{
            base.OnResume();
            IsVisibleAndRunning = true;
		}

		protected override void OnPause()
		{
            IsVisibleAndRunning = false;
            base.OnPause();
		}
	}
}
