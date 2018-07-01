using System;
using Android.OS;
using Android.Support.V7.App;

namespace SimpleMVP.Droid
{
    public abstract class ActivityBase<TPresenter> : ActivityBase where TPresenter : IPresenter
    {
        public new TPresenter Presenter => base.Presenter is TPresenter typedPresenter ? typedPresenter : default(TPresenter);
    }

    public abstract class ActivityBase : StateAppCompatActivity, IView
    {
        public IPresenter Presenter { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // init view
            InitializeView(savedInstanceState);

            // attach view
            AttachPresenter();
        }

        protected override void OnStart()
        {
            base.OnStart();
            Presenter?.OnAppearing();
        }

        protected override void OnStop()
        {
            Presenter?.OnDisappearing();
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            DetachPresenter();
            base.OnDestroy();
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
        }

        private void DetachPresenter()
        {
            // detach view
            Presenter?.DetachView();
            Presenter = null;
        }

        /// <summary>
        /// Init view OnCreate before Presetner is created
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected abstract void InitializeView(Bundle savedInstanceState);

        protected abstract IPresenter CreatePresenter();

        protected virtual void OnPresenterSet()
        {
        }

#if DEBUG
        ~ActivityBase()
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name} -> ~ActivityBase");
        }
#endif
    }
}
