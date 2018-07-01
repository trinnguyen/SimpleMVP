using System;

namespace SimpleMVP
{
    public interface IPresenter
    {
        /// <summary>
        /// Attach the view to Presenter
        /// Called by platform when the View is loaded and attached to the Window
        /// iOS: ViewWillAppear()
        /// Android: Called on OnCreate() by Fragment, OnViewCreated() on Activity
        /// </summary>
        /// <param name="view">View.</param>
        void AttachView(IView view);

        /// <summary>
        /// Notify that the view is resuming, register subscription and start job
        /// Android: OnStart()
        /// iOS: ViewWillAppear()
        /// </summary>
        void OnAppearing();

        /// <summary>
        /// Notify that the view is pausing/disappearing, unregister all subscription and stop job
        /// Android: OnStop()
        /// iOS: ViewDidDisappear()
        /// </summary>
        void OnDisappearing();

        /// <summary>
        /// Detach View from Window
        /// Android: OnStop()
        /// iOS: ViewDidDisappear()
        /// </summary>
        void DetachView();
    }
}
