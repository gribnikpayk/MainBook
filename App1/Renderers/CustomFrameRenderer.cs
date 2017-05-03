using System;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Views;
using App1.Renderers;
using MainBook.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace App1.Renderers
{
    public class CustomFrameRenderer : VisualElementRenderer<CustomFrame>
    {
        private GradientDrawable _normal, _pressed;


        public float X1 { get; set; }
        public float X2 { get; set; }
        public float Y1 { get; set; }
        public float Y2 { get; set; }

        public CustomFrame CustomFrame { get; set; }

        public override bool OnTouchEvent(MotionEvent e)
        {

            if (e.ActionMasked == MotionEventActions.Down)
            {
                X1 = e.GetX();
                Y1 = e.GetY();

                return true;
            }

            X2 = e.GetX();
            Y2 = e.GetY();

            var xChange = X1 - X2;
            var yChange = Y1 - Y2;

            var xChangeSize = Math.Abs(xChange);
            var yChangeSize = Math.Abs(yChange);

            if (xChangeSize > yChangeSize)
            {
                // horizontal
                if (X1 > X2)
                {
                    // left
                    CustomFrame.RaiseSwipedLeft();
                }
                else
                {
                    // right
                    CustomFrame.RaiseSwipedRight();
                }
            }
            else
            {
                // vertical
                if (Y1 > Y2)
                {
                    // up
                    //CustomFrame.RaiseSwipedUp();
                }
                else
                {
                    // down
                    CustomFrame.RaiseSwipedDown();
                }
            }

            return base.OnTouchEvent(e);
        }


        protected override void OnElementChanged(ElementChangedEventArgs<CustomFrame> e)
        {

            CustomFrame = e.NewElement as CustomFrame;
            // Create a drawable for the button's normal state
            _normal = new Android.Graphics.Drawables.GradientDrawable();
            _normal.SetColor(CustomFrame.BackgroundColor.ToAndroid());
            _normal.SetStroke(CustomFrame.BorderWidth, CustomFrame.OutlineColor.ToAndroid());
            _normal.SetCornerRadius(CustomFrame.BorderRadius);
            SetBackgroundDrawable(_normal);
            //SetBackgroundColor(customFram.BackgroundColor.ToAndroid());
            base.OnElementChanged(e);

        }
    }
}