using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using BottomNavigationBar.Listeners;
using BottomNavigationBar;
using Xamarin.Forms;
using ERecall.Views.Menu;
using ERecall.Droid.Renders;
using ERecall.Views;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(SideMenuDetail), typeof(SideMenuDetailRenderer))]
namespace ERecall.Droid.Renders
{
    public class SideMenuDetailRenderer:VisualElementRenderer<SideMenuDetail>, IOnTabClickListener
    {
        private BottomBar _bottomBar;
        private Page _currentPage;
        private int _lastSelectedTabIndex = -1;

        public void OnTabReSelected(int position)
        {
            //throw new NotImplementedException();
        }

        public void OnTabSelected(int position)
        {
            LoadPageContent(position);
        }
        private void LoadPageContent(int position)
        {
            ShowPage(position);
        }
        private void ShowPage(int position)
        {
            if (position != _lastSelectedTabIndex)
            {
                Element.CurrentPage = Element.Children[position];

                if (Element.CurrentPage != null)
                {
                    LoadPageContent(Element.CurrentPage);
                }
            }

            _lastSelectedTabIndex = position;
        }
        private void LoadPageContent(Page page)
        {
            UnloadCurrentPage();

            _currentPage = page;

            LoadCurrentPage();

            Element.CurrentPage = _currentPage;
        }
        private void LoadCurrentPage()
        {
            var renderer = Platform.GetRenderer(_currentPage);

            if (renderer == null)
            {
                renderer = Platform.CreateRenderer(_currentPage);
                Platform.SetRenderer(_currentPage, renderer);

                AddView(renderer.ViewGroup);
            }
            else
            {
                // As we show and hide pages manually OnAppearing and OnDisappearing
                // workflow methods won't be called by the framework. Calling them manually...
                var basePage = _currentPage as BaseContentPage;
                basePage?.SendAppearing();
            }

            renderer.ViewGroup.Visibility = ViewStates.Visible;
        }
        private void UnloadCurrentPage()
        {
            if (_currentPage != null)
            {
                var basePage = _currentPage as BaseContentPage;
                basePage?.SendDisappearing();

                var renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.ViewGroup.Visibility = ViewStates.Invisible;
                }
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SideMenuDetail> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                ClearElement(e.OldElement);
            }

            if (e.NewElement != null)
            {
                InitializeElement(e.NewElement);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearElement(Element);
            }

            base.Dispose(disposing);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (Element == null)
            {
                return;
            }

            int width = r - l;
            int height = b - t;

            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.AtMost));

            // We need to call measure one more time with measured sizes 
            // in order to layout the bottom bar properly
            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(_bottomBar.ItemContainer.MeasuredHeight, MeasureSpecMode.Exactly));

            int barHeight = _bottomBar.ItemContainer.MeasuredHeight;

            _bottomBar.Layout(0, b - barHeight, width, b);

            float density = Android.Content.Res.Resources.System.DisplayMetrics.Density;

            double contentWidthConstraint = width / density;
            double contentHeightConstraint = (height - barHeight) / density;

            if (_currentPage != null)
            {
                var renderer = Platform.GetRenderer(_currentPage);

                renderer.Element.Measure(contentWidthConstraint, contentHeightConstraint);
                renderer.Element.Layout(new Rectangle(0, 0, contentWidthConstraint, contentHeightConstraint));

                renderer.UpdateLayout();
            }
        }

        private void InitializeElement(SideMenuDetail element)
        {
            PopulateChildren(element);
        }

        private void PopulateChildren(SideMenuDetail element)
        {
            // Unfortunately bottom bar can not be reused so we have to
            // remove it and create the new instance
            _bottomBar?.RemoveFromParent();

            _bottomBar = CreateBottomBar(element.Children);
            AddView(_bottomBar);

            LoadPageContent(0);
        }

        private void ClearElement(SideMenuDetail element)
        {
            if (_currentPage != null)
            {
                IVisualElementRenderer renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.ViewGroup.RemoveFromParent();
                    renderer.ViewGroup.Dispose();
                    renderer.Dispose();

                    _currentPage = null;
                }

                if (_bottomBar != null)
                {
                    _bottomBar.RemoveFromParent();
                    _bottomBar.Dispose();
                    _bottomBar = null;
                }
            }
        }

        private BottomBar CreateBottomBar(IEnumerable<Page> pageIntents)
        {
            var bar = new BottomBar(Context);

            // TODO: Configure the bottom bar here according to your needs

            bar.SetOnTabClickListener(this);
            bar.UseFixedMode();

            PopulateBottomBarItems(bar, pageIntents);

            bar.ItemContainer.SetBackgroundColor(Color.Rgb(242, 242, 242));

            return bar;
        }

        private void PopulateBottomBarItems(BottomBar bar, IEnumerable<Page> pages)
        {
            var barItems = pages.Select(x => new BottomBarTab(Context.Resources.GetDrawable(x.Icon), x.Title));

            bar.SetItems(barItems.ToArray());
        }
    }
}