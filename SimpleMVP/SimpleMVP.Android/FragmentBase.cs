using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace SimpleMVP.Droid
{
    public abstract class FragmentBase<TPresenter> : FragmentBase where TPresenter : IPresenter
    {
        protected FragmentBase()
        {
        }
        
        protected new TPresenter Presenter =>
            base.Presenter is TPresenter typedPresenter ? typedPresenter : default(TPresenter);
    }
    
    public abstract class FragmentBase : StateFragment, IView
    {
        protected IPresenter Presenter { get; private set; }

        protected FragmentBase()
        {
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            
            // init view
            InitializeView(view, savedInstanceState);

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
        /// <param name="savedInstanceState"></param>
        protected abstract void InitializeView(View view, Bundle savedInstanceState);

        protected abstract IPresenter CreatePresenter();

        protected virtual void OnPresenterSet()
        {
        }
    }
}
