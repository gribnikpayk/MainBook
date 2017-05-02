using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MainBook.CustomControls
{
    public class CustomFrame : Frame
    {
        public event EventHandler SwipedUp;
        public event EventHandler SwipedDown;
        public event EventHandler SwipedLeft;
        public event EventHandler SwipedRight;

        private const int _translateTo_x = 500;
        private const int _rotateTo = 100;
        private const int _animationSpeed = 250;

        public int CurrentPosition_X { get; set; }
        public int StartedPosition_X { get; set; }
        public double FrameRotation { get; set; }

        public readonly static BindableProperty BorderRadiusProperty = BindableProperty.Create("BorderRadius", typeof(int), typeof(CustomFrame), 5, BindingMode.OneWay, null, null, null, null, null);

        public readonly static BindableProperty OutlineColorProperty = BindableProperty.Create("OutlineColor", typeof(Color), typeof(CustomFrame), Color.Default, BindingMode.OneWay, null, null, null, null, null);

        public readonly static BindableProperty InlineColorProperty = BindableProperty.Create("InlineColor", typeof(Color), typeof(CustomFrame), Color.Default, BindingMode.OneWay, null, null, null, null, null);

        public readonly static BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(CustomFrame), 2, BindingMode.OneWay, null, null, null, null, null);

        public int BorderWidth
        {
            get
            {
                return (int)base.GetValue(CustomFrame.BorderWidthProperty);
            }
            set
            {
                base.SetValue(CustomFrame.BorderWidthProperty, value);
            }
        }

        public Color OutlineColor
        {
            get
            {
                return (Color)base.GetValue(CustomFrame.OutlineColorProperty);
            }
            set
            {
                base.SetValue(CustomFrame.OutlineColorProperty, value);
            }
        }

        public Color InlineColor
        {
            get
            {
                return (Color)base.GetValue(CustomFrame.InlineColorProperty);
            }
            set
            {
                base.SetValue(CustomFrame.InlineColorProperty, value);
            }
        }

        public int BorderRadius
        {
            get
            {
                return (int)base.GetValue(CustomFrame.BorderRadiusProperty);
            }
            set
            {
                base.SetValue(CustomFrame.BorderRadiusProperty, value);
            }
        }

        public CustomFrame()
        {
            base.Padding = new Size(20, 20);
        }

        public void RaiseSwipedUp()
        {
            if (SwipedUp != null)
                SwipedUp(this, new EventArgs());
        }

        public void RaiseSwipedDown()
        {
            if (SwipedDown != null)
                SwipedDown(this, new EventArgs());
        }

        public void RaiseSwipedLeft()
        {
            this.TranslateTo(-1 * _translateTo_x, 0, _animationSpeed);
            this.FadeTo(0, _animationSpeed);
            this.RotateTo(-1 * _rotateTo, _animationSpeed);
            if (SwipedLeft != null)
                SwipedLeft(this, null);
        }

        public void RaiseSwipedRight()
        {
            this.TranslateTo(_translateTo_x, 0, _animationSpeed);
            this.FadeTo(0, _animationSpeed);
            this.RotateTo(_rotateTo, _animationSpeed);
            if (SwipedRight != null)
                SwipedRight(this, new EventArgs());
        }
    }
}