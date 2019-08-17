using System;

namespace SimpleMVP
{
    public interface IPresenter
    {
        /// <summary>
        /// Attach the view to Presenter
        /// Called by platform when the View is loaded and attached to the Window
        /// Followed by OnStart()
        /// iOS: ViewWillAppear()
        /// Android: Called on OnStart() by Fragment, OnStart() on Activity
        /// </summary>
        /// <param name="view">View.</param>
        void AttachView(IView view);

        /// <summary>
        /// Notify that the view is resuming, register subscription and start job
        /// Android: OnResume()
        /// iOS: ViewWillAppear()
        /// </summary>
        void OnAppearing();

        /// <summary>
        /// Notify that the view is pausing/disappearing, unregister all subscription and stop job
        /// Android: OnPause()
        /// iOS: ViewDidDisappear()
        /// </summary>
        //void OnDisappearing();

        /// <summary>
        /// Detach View from Window
        /// Android: OnStop()
        /// iOS: ViewDidDisappear()
        /// </summary>
        void DetachView();
    }
}
