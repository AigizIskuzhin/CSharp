using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using MyBus.Constants;

namespace MyBus.Infrastructure.Navigation.Base
{
    public class NavigationManager : Interfaces.INavigation
    {
        private readonly ContentControl _FrameControl;

        private readonly List<FrameworkElement> _CashedViews = new();
        private readonly List<object> _CashedViewModels = new();
        
        private readonly Dictionary<Type,object[]> _History = new();
        

        public NavigationManager(ContentControl frameControl) => _FrameControl = frameControl;
        

        public void ClearCashedViews(params object[] args)
        {
            if (args.Length == 0)
            {
                _CashedViews.Clear();
                _CashedViewModels.Clear();
            }
        }

        /// <summary>
        /// Navigation with args, no INavigationAware
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="frame"></param>
        /// <param name="args"></param>
        public static void Navigate(Type viewType, ContentControl frame, params object[] args)
        {
            FrameworkElement view = Activator.CreateInstance(viewType) as FrameworkElement;
            view.DataContext = Activator.CreateInstance(NavigationKeys.NavigationDictionary[viewType], args);
            //OnStaticNavigatingTo(view.DataContext as INavigationAware, args);
            SetView(view, frame);
        }
        /// <summary>
        /// Navigation with args, INavigationAware
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="args"></param>
        public void Navigate(Type viewType, params object[]args)
        {
            if (!IsViewIsCashed(viewType))
                LoadView(viewType);

            FrameworkElement view = GetView(viewType);
            OnNavigatingTo(view.DataContext as INavigationAware, args);
            SetView(view);
        }

        private static void OnStaticNavigatingTo(INavigationAware viewModel, object[] args)
        {
            viewModel.OnNavigatingTo(null, args);
        }
        private void OnNavigatingTo(INavigationAware viewModel, object[] args)
        {
            viewModel.OnNavigatingTo(this, args);
        }

        private void LoadView(Type viewType)
        {
            FrameworkElement view = Activator.CreateInstance(viewType) as FrameworkElement;
            view.DataContext = Activator.CreateInstance(NavigationKeys.NavigationDictionary[viewType]);
            _CashedViews.Add(view);
        }

        private object GetViewModel(Type viewModelType) =>
            _CashedViewModels.Find(cashedViewModel => viewModelType == cashedViewModel.GetType());
        private bool IsViewIsCashed(Type viewType) => _CashedViews.Any(cashedView => viewType == cashedView.GetType());
        private FrameworkElement GetView(Type viewType) => _CashedViews.Find(cashedView => viewType == cashedView.GetType());
        private void SetView(FrameworkElement view) => _FrameControl.Content = view;
        private static void SetView(FrameworkElement view, ContentControl frame) => frame.Content = view;

        public void ClearViewDataContext(Type viewType)
        {
            if (IsViewIsCashed(viewType)) GetView(viewType).DataContext = null;
        }

        public void ReloadViewDataContext(Type viewType)
        {
            if (IsViewIsCashed(viewType))
            {
                FrameworkElement view = GetView(viewType);
                view.DataContext = Activator.CreateInstance(view.DataContext.GetType());
                OnNavigatingTo(view.DataContext as INavigationAware, null);
            }
        }

        public void OpenBackView()
        {
            var last = _History.Last();
            _History.Remove(last.Key);
            Navigate(last.Key, last.Value);
        }

        public void OpenForwardView()
        {

        }

    }
}
