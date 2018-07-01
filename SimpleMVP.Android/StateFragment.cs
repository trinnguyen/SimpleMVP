using System;

namespace SimpleMVP.Droid
{
    public class StateFragment : Android.Support.V4.App.Fragment, IStateView
    {
        public StateFragment()
        {
        }

        /// <summary>
        /// Flag to ensure the app is running betweeb OnResume and OnPause
        /// Fragment should only be shown when it is true
        /// </summary>
        /// <value><c>true</c> if is visible and running; otherwise, <c>false</c>.</value>
        public bool IsVisibleAndRunning { get; private set; }

        public override void OnResume()
        {
            base.OnResume();
            IsVisibleAndRunning = true;
        }

        public override void OnPause()
        {
            IsVisibleAndRunning = false;
            base.OnPause();
        }
    }
}
