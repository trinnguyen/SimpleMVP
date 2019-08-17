using Android.OS;
using Android.Views;

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

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            
            // init view
            InitializeView(view);

            // attach view
            AttachPresenter();
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            DetachPresenter();
        }

        private void AttachPresenter()
        {
            // first time init Presenter
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

            // on appearing
            Presenter?.OnAppearing();
        }

        private void DetachPresenter()
        {
            Presenter?.DetachView();
            Presenter = null;
        }

        /// <summary>
        /// Init view OnViewCreated before Presenter is created
        /// </summary>
        /// <param name="view"></param>
        protected abstract void InitializeView(View view);

        protected abstract IPresenter CreatePresenter();

        protected virtual void OnPresenterSet()
        {
        }
    }
}