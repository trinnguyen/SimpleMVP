using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace SimpleMVP.Droid
{
	public class StateDialogFragment : Android.Support.V4.App.DialogFragment
    {      
		private bool _markAsSuccess = false;

        // required ctor
		protected StateDialogFragment()
        {
        }

        public override void OnCancel(IDialogInterface dialog)
        {
            base.OnCancel(dialog);
            _markAsSuccess = false;
        }

        public override void OnDismiss(IDialogInterface dialog)
        {
            base.OnDismiss(dialog);
            OnDialogDismissed(_markAsSuccess);
        }
        
        /// <summary>
        /// Event raised when dialog is dismissed
        /// </summary>
        /// <param name="markAsSuccess">If set to <c>true</c> mark as success.</param>
        protected virtual void OnDialogDismissed(bool markAsSuccess)
        {
			System.Diagnostics.Debug.WriteLine($"{GetType().Name} -> OnDialogDismissed: {markAsSuccess}");
        }

        public override void OnStart()
        {
            base.OnStart();
            // reset
            _markAsSuccess = false;
        }
              
        /// <summary>
        /// Used for dialog dismiss
        /// </summary>
        /// <param name="markAsSuccess">If set to <c>true</c> mark as success.</param>
        protected void RequestDismiss(bool markAsSuccess)
        {
            _markAsSuccess = markAsSuccess;
			if (!IsStateSaved)
			{
				Dismiss();
			}         
        }      

    }
}
