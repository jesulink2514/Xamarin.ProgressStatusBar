using NControl.Abstractions;
using NGraphics;
using Xamarin.Forms;

namespace DemoLive.Controls
{
    public class RoundedBorderControl : NControlView

    {

        /// <summary>

        /// Initializes a new instance of the <see cref="RoundedBorderControl"/> class.

        /// </summary>

        public RoundedBorderControl()

        {

            base.BackgroundColor = Xamarin.Forms.Color.Transparent;

        }



        #region Properties



        /// <summary>

        /// The corner radius property.

        /// </summary>

        public static BindableProperty CornerRadiusProperty =

            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(RoundedBorderControl), 0,

                BindingMode.OneWay, null, (bindable, oldValue, newValue) =>

                {

                    (bindable as RoundedBorderControl).Invalidate();

                });



        /// <summary>

        /// Gets or sets the corner radius.

        /// </summary>

        /// <value>The corner radius.</value>

        public int CornerRadius

        {

            get { return (int)GetValue(CornerRadiusProperty); }

            set { SetValue(CornerRadiusProperty, value); }

        }



        /// <summary>

        /// The border and fill color property.

        /// </summary>

        public static new BindableProperty BackgroundColorProperty =

            BindableProperty.Create(nameof(BackgroundColor), typeof(Xamarin.Forms.Color), typeof(RoundedBorderControl),

                Xamarin.Forms.Color.Transparent, BindingMode.OneWay, null, (bindable, oldValue, newValue) =>

                {

                    (bindable as RoundedBorderControl).Invalidate();

                });



        /// <summary>

        /// Gets or sets the color which will fill the background of a VisualElement. This is a bindable property.

        /// </summary>

        /// <value>The color of the background.</value>

        public new Xamarin.Forms.Color BackgroundColor

        {

            get { return (Xamarin.Forms.Color)GetValue(BackgroundColorProperty); }

            set { SetValue(BackgroundColorProperty, value); }

        }



        #endregion



        #region Drawing



        /// <summary>

        /// Draw the specified canvas.

        /// </summary>

        /// <param name="canvas">Canvas.</param>

        /// <param name="rect">Rect.</param>

        public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)

        {

            base.Draw(canvas, rect);



            var backgroundBrush = new SolidBrush(new NGraphics.Color(BackgroundColor.R,

                BackgroundColor.G, BackgroundColor.B, BackgroundColor.A));



            var curveSize = CornerRadius;

            var width = rect.Width;

            var height = rect.Height;

            canvas.DrawPath(new PathOp[]{

                new MoveTo(curveSize, 0),

                // Top Right corner

                new LineTo(width-curveSize, 0),

                new CurveTo(

                    new NGraphics.Point(width-curveSize, 0),

                    new NGraphics.Point(width, 0),

                    new NGraphics.Point(width, curveSize)

                ),

                new LineTo(width, height-curveSize),

                // Bottom right corner

                new CurveTo(

                    new NGraphics.Point(width, height-curveSize),

                    new NGraphics.Point(width, height),

                    new NGraphics.Point(width-curveSize, height)

                ),

                new LineTo(curveSize, height),

                // Bottom left corner

                new CurveTo(

                    new NGraphics.Point(curveSize, height),

                    new NGraphics.Point(0, height),

                    new NGraphics.Point(0, height-curveSize)

                ),

                new LineTo(0, curveSize),

                new CurveTo(

                    new NGraphics.Point(0, curveSize),

                    new NGraphics.Point(0, 0),

                    new NGraphics.Point(curveSize, 0)

                ),

                new ClosePath()

            }, null, backgroundBrush);



        }



        #endregion



    }
}