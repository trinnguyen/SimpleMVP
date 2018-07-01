using System;
using Foundation;
using UIKit;

namespace SimpleMVP.iOS
{
    [Register("ViewControllerBase")]
    public abstract class ViewControllerBase : UIViewController, IView
    {
        protected IPresenter Presenter { get; private set; }

        protected TPresenter GetPresenter<TPresenter>() where TPresenter : IPresenter
        {
            if (Presenter is TPresenter typedPresenter)
                return typedPresenter;

            return default(TPresenter);
        }

        public ViewControllerBase()
        {
        }

        public ViewControllerBase(NSCoder coder) : base(coder)
        {
        }

        public ViewControllerBase(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Addition setup on Presenter
            InitializeView();

            // request init Present
            Presenter = CreatePresenter();
            if (Presenter != null)
            {
                // update presenter
                OnPresenterSet();

                // attach view
                // on iOS, simply attach View when it's about to be appearing
                // There is no similiar method like OnStart() and OnResume() on Android because the View on iOS is never dismiss
                Presenter.AttachView(this);
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Presenter?.OnAppearing();
        }

        public override void ViewDidDisappear(bool animated)
        {
            Presenter?.OnDisappearing();
            base.ViewDidDisappear(animated);
        }

        protected virtual void OnPresenterSet()
        {
        }

        protected abstract void InitializeView();

        protected abstract IPresenter CreatePresenter();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                System.Diagnostics.Debug.WriteLine($"{GetType().Name} -> Dispose: {disposing}");
                Presenter?.DetachView();
                Presenter = null;
            }
            base.Dispose(disposing);
        }

#if DEBUG
        ~ViewControllerBase()
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name} -> ~ViewControllerBase");
        }
#endif
    }
}
