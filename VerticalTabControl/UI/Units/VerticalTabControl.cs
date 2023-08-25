using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace VerticalTabControl.UI.Units
{
    public class VerticalTabControl : ContentControl
    {
        public double ContentHeight
        {
            get { return (double)GetValue (ContentHeightProperty); }
            set { SetValue (ContentHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register ("ContentHeight", typeof (double), typeof (VerticalTabControl), new PropertyMetadata (0.0));


        public DataTemplate TabItem
        {
            get { return (DataTemplate)GetValue (TabItemProperty); }
            set { SetValue (TabItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TabItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabItemProperty =
            DependencyProperty.Register ("TabItem", typeof (DataTemplate), typeof (VerticalTabControl), new PropertyMetadata (null));

        public DataTemplate TabContent
        {
            get { return (DataTemplate)GetValue (TabContentProperty); }
            set { SetValue (TabContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TabItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabContentProperty =
            DependencyProperty.Register ("TabContent", typeof (DataTemplate), typeof (VerticalTabControl), new PropertyMetadata (null));



        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue (ItemsSourceProperty); }
            set { SetValue (ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register ("ItemsSource", typeof (IEnumerable), typeof (VerticalTabControl), new PropertyMetadata (null));

        public Brush SelectTabTextColor
        {
            get { return (Brush)GetValue (SelectTabTextColorProperty); }
            set { SetValue (SelectTabTextColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectTabItemColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectTabTextColorProperty =
            DependencyProperty.Register ("SelectTabTextColor", typeof (Brush), typeof (VerticalTabControl), new PropertyMetadata (new SolidColorBrush (Colors.White)));

        public Brush SelectTabBackgroundColor
        {
            get { return (Brush)GetValue (SelectTabBackgroundColorProperty); }
            set { SetValue (SelectTabBackgroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectTabItemColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectTabBackgroundColorProperty =
            DependencyProperty.Register ("SelectTabBackgroundColor", typeof (Brush), typeof (VerticalTabControl), new PropertyMetadata (new SolidColorBrush ((Color)ColorConverter.ConvertFromString ("#1e1e1e"))));

        public Brush HoverTabBackgroundColor
        {
            get { return (Brush)GetValue (HoverTabBackgroundColorProperty); }
            set { SetValue (HoverTabBackgroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectTabItemColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverTabBackgroundColorProperty =
            DependencyProperty.Register ("HoverTabBackgroundColor", typeof (Brush), typeof (VerticalTabControl), new PropertyMetadata (new SolidColorBrush ((Color)ColorConverter.ConvertFromString ("#1e1e1e"))));

        public Brush HoverTabTextColor
        {
            get { return (Brush)GetValue (HoverTabTextColorProperty); }
            set { SetValue (HoverTabTextColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectTabItemColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverTabTextColorProperty =
            DependencyProperty.Register ("HoverTabTextColor", typeof (Brush), typeof (VerticalTabControl), new PropertyMetadata (new SolidColorBrush (Colors.White)));

        static VerticalTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (VerticalTabControl), new FrameworkPropertyMetadata (typeof (VerticalTabControl)));
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged (e);
            if (e.OldValue == e.NewValue)
                return;
            if (e.Property.Name == "ActualHeight")
            {
                ContentHeight = (double)e.NewValue;
            }
        }

        private ListBox item;
        private ListBox content;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate ();
            item = GetTemplateChild ("PART_ITEM") as ListBox;
            content = GetTemplateChild ("PART_CONTENT") as ListBox;
            item.SelectionChanged += Item_SelectionChanged;
        }

        private void Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double idx = this.item.SelectedIndex;
            double moveOffeset = idx * this.ActualHeight;
            this.ModeScroll (moveOffeset);
        }

        double lastVerticalOffset = 0;
        private void ModeScroll(double targetVerticalOffset)
        {
            TranslateTransform translateTransform = new TranslateTransform ();
            content.RenderTransform = translateTransform;

            DoubleAnimation verticalOffsetAnimation = new DoubleAnimation
            {
                From = -lastVerticalOffset,
                To = -targetVerticalOffset,
                Duration = TimeSpan.FromMilliseconds (300) // 애니메이션 지속 시간
            };
            lastVerticalOffset = targetVerticalOffset;
            // ScrollViewer의 스크롤 위치에 애니메이션 적용
            translateTransform.BeginAnimation (TranslateTransform.YProperty, verticalOffsetAnimation);
        }
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount (parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild (parent, i);
                if (child is T typedChild)
                {
                    return typedChild;
                }

                T childOfChild = FindVisualChild<T> (child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }
    }
}