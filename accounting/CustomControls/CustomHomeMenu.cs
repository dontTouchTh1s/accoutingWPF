using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace accounting.CustomControls
{
    /// <summary>
    ///     Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///     Step 1a) Using this custom control in a XAML file that exists in the current project.
    ///     Add this XmlNamespace attribute to the root element of the markup file where it is
    ///     to be used:
    ///     xmlns:MyNamespace="clr-namespace:accounting.Themes"
    ///     Step 1b) Using this custom control in a XAML file that exists in a different project.
    ///     Add this XmlNamespace attribute to the root element of the markup file where it is
    ///     to be used:
    ///     xmlns:MyNamespace="clr-namespace:accounting.Themes;assembly=accounting.Themes"
    ///     You will also need to add a project reference from the project where the XAML file lives
    ///     to this project and Rebuild to avoid compilation errors:
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///     Step 2)
    ///     Go ahead and use your control in the XAML file.
    ///     <MyNamespace:CustomHomeMenu />
    /// </summary>
    public class CustomHomeMenu : Control
    {
        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register("Kind", typeof(PackIconKind), typeof(CustomHomeMenu),
                new PropertyMetadata(default(PackIconKind)));

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(CustomHomeMenu),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            "IsChecked", typeof(bool), typeof(CustomHomeMenu), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(CustomHomeMenu), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(CustomHomeMenu), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register(
            "GroupName", typeof(string), typeof(CustomHomeMenu), new PropertyMetadata(default(string)));

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
                typeof(EventHandler<ClickEventArgs>), typeof(CustomHomeMenu));

        static CustomHomeMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomHomeMenu),
                new FrameworkPropertyMetadata(typeof(CustomHomeMenu)));
        }

        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public PackIconKind Kind
        {
            get => (PackIconKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            var args = new ClickEventArgs(ClickEvent, this);
            RaiseEvent(args);
            Command.Execute(CommandParameter);
            IsChecked = true;

            var childCount = VisualTreeHelper.GetChildrenCount(Parent);
            for (var i = 0; i < childCount; i++)
            {
                object child = VisualTreeHelper.GetChild(Parent, i);
                if (child is CustomHomeMenu)
                {
                    var menuItem = (CustomHomeMenu)child;
                    if (menuItem.GroupName == GroupName && menuItem != this)
                        menuItem.IsChecked = false;
                }
            }
        }

        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }
    }

    public class ClickEventArgs : RoutedEventArgs
    {
        public ClickEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}