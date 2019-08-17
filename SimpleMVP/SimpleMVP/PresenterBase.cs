using System;
namespace SimpleMVP
{
    public abstract class PresenterBase : IPresenter
    {
        private WeakReference<IView> _weakRefView;

        protected IView View
        {
            get
            {
                if (_weakRefView != null && _weakRefView.TryGetTarget(out var view))
                {
                    return view;
                }

                return default(IView);
            }
            private set
            {
                if (value == null)
                {
                    _weakRefView?.SetTarget(null);
                    _weakRefView = null;
                }
                else
                {
                    if (_weakRefView == null)
                    {
                        _weakRefView = new WeakReference<IView>(value);
                    }
                    else
                    {
                        _weakRefView.SetTarget(value);
                    }
                }
            }
        }

        public void AttachView(IView view)
        {
            View = view;
            OnViewAttached();
        }
        
        public void DetachView()
        {
            View = default(IView);
            OnViewDetached();
        }

        /// <summary>
        /// View is loaded and attached to the Window and ready to start working
        /// Should init data from here
        /// </summary>
        protected abstract void OnViewAttached();

        /// <inheritdoc />
        /// <summary>
        /// View is about to be shown, start load/populate the data
        /// </summary>
        public abstract void OnAppearing();

        /// <inheritdoc />
        /// <summary>
        /// View is about to be hidden, should prepare to pause work
        /// There is no need to stop all the pending works because of this methods, the View is not cleared but only hidden
        /// </summary>
        //public abstract void OnDisappearing();

        /// <summary>
        /// View is detached from the Window and stop working
        /// </summary>
        protected abstract void OnViewDetached();
    }

    public abstract class PresenterBase<TView> : PresenterBase where TView : IView
    {
        protected new TView View
        {
            get
            {
                IView view = base.View;
                if (view is TView typedView)
                    return typedView;

                return default(TView);
            }
        }
    }
}
