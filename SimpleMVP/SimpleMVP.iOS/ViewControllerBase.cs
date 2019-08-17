using System;
using Foundation;
using UIKit;

namespace SimpleMVP.iOS
{
    [Register("ViewControllerBase")]
    public abstract class ViewControllerBase : UIViewController, IView
    {
        public IPresenter Presenter { get; private set; }

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
                // There is no similar method like OnStart() and OnResume() on Android because the View on iOS is never dismiss
                Presenter.AttachView(this);
                Presenter.OnAppearing();
            }
        }

        protected virtual void OnPresenterSet()
        {
        }

        protected abstract void InitializeView();
        
        protected abstract IPresenter CreatePresenter();
    }
}
