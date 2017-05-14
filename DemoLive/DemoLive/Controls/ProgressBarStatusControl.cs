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
        public static readonly BindableProperty ActiveStatusColorProperty = BindableProperty.Create(nameof(ActiveStatusColor), typeof(Xamarin.Forms.Color), typeof(ProgressBarStatusControl), Xamarin.Forms.Color.FromRgb(0, 0, 0), BindingMode.TwoWay, null, propertyChanged: OnActiveStatusColorChanged);
        public Xamarin.Forms.Color ActiveStatusColor
        {
            get => (Xamarin.Forms.Color)GetValue(ActiveStatusColorProperty);
            set
            {
                SetValue(ActiveStatusColorProperty, value);
                this.Invalidate();
            }
        }
        private static void OnActiveStatusColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as ProgressBarStatusControl;
            if (control == null) return;
            control.ActiveStatusColor = (Xamarin.Forms.Color)newvalue;
        }


        public static readonly BindableProperty InactiveStatusColorProperty = BindableProperty.Create(nameof(InactiveStatusColor), typeof(Xamarin.Forms.Color), typeof(ProgressBarStatusControl), Xamarin.Forms.Color.FromRgb(0, 0, 0), BindingMode.TwoWay, null, propertyChanged: OnInactiveStatusColorChanged);
        public Xamarin.Forms.Color InactiveStatusColor
        {
            get => (Xamarin.Forms.Color)GetValue(InactiveStatusColorProperty);
            set
            {
                SetValue(InactiveStatusColorProperty, value);
                this.Invalidate();
            }
        }

        private static void OnInactiveStatusColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as ProgressBarStatusControl;
            if (control == null) return;
            control.InactiveStatusColor = (Xamarin.Forms.Color)newvalue;
        }
        
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
                var an = new Animation((d) =>
                {
                    this._percentage = d;
                    Invalidate();
                },0,1);
                SetValue(CurrentStatusIndexProperty, value);
                an.Commit(this, "percentage",easing:Easing.CubicInOut, length: 500);
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

        private double _percentage = 0;

        public override void Draw(ICanvas canvas, Rect rect)
        {
            //calculating using padding
            var totalWidth = rect.Width - (Padding.Right + Padding.Left);
            var totalHeight = rect.Height - (Padding.Top + Padding.Bottom);

            var radius = 15;
            var pathsNumber = StatusesNumber - 1;
            var pathWidth = totalWidth / pathsNumber;
            var pathHeight = 10;

            var activeIndex = CurrentStatusIndex;

            //Draw paths
            //Draw base path
            var basex = radius + Padding.Left;
            var baseY = (totalHeight - pathHeight) / 2 + Padding.Top;
            var baseWidth  = totalWidth - 2*radius;
            
            canvas.FillRectangle(basex,baseY,baseWidth,pathHeight,GetInactiveBrush());

            var width = (activeIndex - 1 + _percentage) * pathWidth - 2 * radius;
            canvas.FillRectangle(basex, baseY, width, pathHeight, GetActiveBrush());

            //Draw status circles
            var startYCircle = (totalHeight / 2) - radius + Padding.Top;

            for (int i = 0; i < StatusesNumber; i++)
            {
                //Draw Status Circle
                var startX = pathWidth * i + Padding.Left;
                if (i != 0)
                    startX = (pathWidth * i) - 2* radius;
                
                SolidBrush color;

                if (i == activeIndex)
                {
                    color = Math.Abs(_percentage - 1) < 0.1 ? GetActiveCircleBrush() : GetInactiveCircleBrush();
                }
                else
                {
                    color = i <= activeIndex
                        ? GetActiveCircleBrush()
                        : GetInactiveCircleBrush();

                }

                canvas.FillEllipse(startX, startYCircle, 2 * radius, 2 * radius,color);
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

        private NGraphics.SolidBrush GetActiveCircleBrush()
        {
            return new SolidBrush(this.ActiveStatusColor.ToNColor());
        }

        private NGraphics.SolidBrush GetInactiveCircleBrush()
        {
            return new SolidBrush(this.InactiveStatusColor.ToNColor());
        }
    }
}
