using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NControl.Abstractions;
using NControl.Controls;
using NGraphics;
using Xamarin.Forms;
using Color = NGraphics.Color;
using Point = NGraphics.Point;

namespace DemoLive.Controls
{
    public class ProgressBarStatusControl: NControlView
    {
        
        #region Properties
        public static readonly BindableProperty ActiveColorProperty = BindableProperty.Create(nameof(ActiveColor), typeof(Xamarin.Forms.Color), typeof(ProgressBarStatusControl),Xamarin.Forms.Color.FromRgb(0, 0, 0),BindingMode.TwoWay, null, propertyChanged: OnActiveColorChanged);


        public Xamarin.Forms.Color ActiveColor
        {
            get => (Xamarin.Forms.Color)GetValue(ActiveColorProperty);
            set
            {
                SetValue(ActiveColorProperty, value);
                this.Invalidate();
            }
        }

        private static void OnActiveColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as ProgressBarStatusControl;
            if (control == null) return;
            control.ActiveColor = (Xamarin.Forms.Color)newvalue;
        }

        public static readonly BindableProperty InactiveColorProperty = BindableProperty.Create(nameof(InactiveColor), typeof(Xamarin.Forms.Color), typeof(ProgressBarStatusControl), Xamarin.Forms.Color.FromRgb(0, 0, 0), BindingMode.TwoWay, null, propertyChanged: OnInactiveColorChanged);


        public Xamarin.Forms.Color InactiveColor
        {
            get => (Xamarin.Forms.Color)GetValue(InactiveColorProperty);
            set
            {
                SetValue(InactiveColorProperty, value);
                this.Invalidate();
            }
        }

        private static void OnInactiveColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as ProgressBarStatusControl;
            if (control == null) return;
            control.InactiveColor = (Xamarin.Forms.Color)newvalue;
        }

        public static readonly BindableProperty StatusesNumberProperty = BindableProperty.Create(nameof(StatusesNumber),typeof(int),typeof(ProgressBarStatusControl),2,
            BindingMode.Default,propertyChanged:OnStatusChanged);

        private static void OnStatusChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as ProgressBarStatusControl;
            var intValue = (int) newvalue;
            if(intValue <= 0)return;

            if (control == null)return;

            control.StatusesNumber = intValue;
            if (control.CurrentStatusIndex > intValue - 1) control.CurrentStatusIndex = 0;
        }

        public int StatusesNumber
        {
            get => (int)GetValue(StatusesNumberProperty);
            set
            {
                SetValue(StatusesNumberProperty, value);
                Invalidate();
            }
        }

        public int CurrentStatusIndex
        {
            get => (int)GetValue(CurrentStatusIndexProperty);
            set
            {
                SetValue(CurrentStatusIndexProperty, value);
                Invalidate();
            }
        }

        public static readonly BindableProperty CurrentStatusIndexProperty = BindableProperty.Create(nameof(CurrentStatusIndex), typeof(int), typeof(ProgressBarStatusControl), 0,
            BindingMode.Default, propertyChanged: OnCurrentStatusIndexChanged);

        private static void OnCurrentStatusIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as ProgressBarStatusControl;
            if (control == null)return;

            var intValue = (int)newvalue;

            if (intValue > control.StatusesNumber - 1 || intValue < 0) return;

            control.CurrentStatusIndex = intValue;
        }

        #endregion
        public override void Draw(ICanvas canvas, Rect rect)
        {
            var radius = 15;
            var pathsNumber = StatusesNumber - 1;
            var pathWidth = rect.Width / pathsNumber;
            var pathHeight = 10;

            var activeIndex = CurrentStatusIndex;

            //Draw paths
            //Draw base path
            var basex = radius;
            var baseY = (rect.Height - pathHeight) / 2;
            var baseWidth  = rect.Width - 2*radius;
            
            canvas.FillRectangle(basex,baseY,baseWidth,pathHeight,GetInactiveBrush());

            for (int i = 0; i < pathsNumber; i++)
            {
                var currentWidth = pathWidth;

                if (activeIndex < (i+1)) continue;

                var startX = pathWidth * i;
                if (i == 0)
                {
                    startX = startX + radius;
                    currentWidth = currentWidth - radius;
                }

                if (i == (pathsNumber - 1)) currentWidth -= radius;

                var startY = rect.Height / 2 - (pathHeight / 2);

                //Draw path
                //position radius/2
                canvas.FillRectangle(startX, startY, currentWidth, pathHeight, GetActiveBrush());
            }

            for (int i = 0; i < StatusesNumber; i++)
            {
                //Draw Status Circle
                var startX = pathWidth * i;
                if (i != 0)
                    startX = (pathWidth * i) - 2* radius;
                
                var startY = (rect.Height / 2) - radius;

                var color = i <= activeIndex
                    ? GetActiveBrush()
                    : GetInactiveBrush(); 

                canvas.FillEllipse(startX, startY, 2 * radius, 2 * radius,color);
            }

        }

        private NGraphics.SolidBrush GetActiveBrush()
        {
            return new SolidBrush(this.ActiveColor.ToNColor());
        }

        private NGraphics.SolidBrush GetInactiveBrush()
        {
            return new SolidBrush(this.InactiveColor.ToNColor());
        }
    }
}
