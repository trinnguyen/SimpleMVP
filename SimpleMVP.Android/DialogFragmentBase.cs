namespace SimpleMVP.Droid
{
	public abstract class DialogFragmentBase<TPresenter> : DialogFragmentBase where TPresenter : IPresenter
    {
        protected DialogFragmentBase()
        {
        }
        
        protected new TPresenter Presenter =>
            base.Presenter is TPresenter typedPresenter ? typedPresenter : default(TPresenter);
    }
    
	public abstract class DialogFragmentBase : StateDialogFragment, IView
    {
        protected IPresenter Presenter { get; private set; }

        protected DialogFragmentBase()
        {
        }

        protected abstract IPresenter CreatePresenter();

        public override void OnStart()
        {
            base.OnStart();

            // First time init Presenter
            if (Presenter == null)
            {
                Presenter = CreatePresenter();
                if (Presenter != null)
                {
                    OnPresenterSet();
                }
            }

            // attach view
            Presenter?.AttachView(this);

            // start
            Presenter?.OnAppearing();
        }

        protected virtual void OnPresenterSet()
        {
        }

        public override void OnStop()
        {
            // stop
            Presenter?.OnDisappearing();

            // detact
            Presenter?.DetachView();
            base.OnStop();
        }
#if DEBUG
        ~DialogFragmentBase()
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name} -> ~DialogFragmentBase");
        }
#endif
    }
}