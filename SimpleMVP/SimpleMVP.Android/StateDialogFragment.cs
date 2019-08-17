using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace SimpleMVP.Droid
{
	public class StateDialogFragment : AppCompatDialogFragment
    {      
        // required ctor
		protected StateDialogFragment()
        {
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Cancelable = false;
        }

        /// <summary>
        /// Event raised when dialog is dismissed
        /// </summary>
        /// <param name="markAsSuccess">If set to <c>true</c> mark as success.</param>
        protected virtual void OnDialogDismissed(bool markAsSuccess)
        {
			System.Diagnostics.Debug.WriteLine($"{GetType().Name} -> OnDialogDismissed: {markAsSuccess}");
        }
              
        /// <summary>
        /// Used for dialog dismiss
        /// </summary>
        /// <param name="markAsSuccess">If set to <c>true</c> mark as success.</param>
        protected void RequestDismiss(bool markAsSuccess)
        {
			if (!IsStateSaved)
			{
				Dismiss();
                OnDialogDismissed(markAsSuccess);
            }
        }
    }
}
