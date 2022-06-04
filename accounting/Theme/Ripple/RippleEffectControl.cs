using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace accounting.Theme.Ripple
{
    class RippleEffectControl : ContentControl
    {
        static RippleEffectControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleEffectControl), new FrameworkPropertyMetadata(typeof(RippleEffectControl)));
        }

        public Brush HighlightBackground
        {
            get => (Brush)GetValue(HighlightBackgroundProperty);
            set => SetValue(HighlightBackgroundProperty, value);
        }

        private const string ChildName = "PART_grid";

        // Using a DependencyProperty as the backing store for HighlightBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.Register("HighlightBackground", typeof(Brush), typeof(RippleEffectControl), new PropertyMetadata(Brushes.White));
        private Ellipse? _ellipse;
        private Grid? _grid;
        private Storyboard? _animation;
        public override void OnApplyTemplate()
        {


            base.OnApplyTemplate();

            _ellipse = (Ellipse)GetTemplateChild("PART_ellipse");
            _grid = (Grid)GetTemplateChild(ChildName);
            _animation = (Storyboard)_grid.FindResource("PART_animation");

            this.AddHandler(MouseDownEvent, new RoutedEventHandler((sender, e) =>
            {
                var targetWidth = Math.Max(ActualWidth, ActualHeight) * 2;
                var mousePosition = ((MouseButtonEventArgs)e).GetPosition(this);
                var startMargin = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);
                //set initial margin to mouse position
                _ellipse.Margin = startMargin;
                //set the to value of the animation that animates the width to the target width
                ((DoubleAnimation)_animation.Children[0]).To = targetWidth;
                //set the to and from values of the animation that animates the distance relative to the container (grid)
                ((ThicknessAnimation)_animation.Children[1]).From = startMargin;
                ((ThicknessAnimation)_animation.Children[1]).To = new Thickness(mousePosition.X - targetWidth / 2, mousePosition.Y - targetWidth / 2, 0, 0);
                _ellipse.BeginStoryboard(_animation);
            }), true);
        }
    }
}
