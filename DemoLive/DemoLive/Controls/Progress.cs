using NControl.Controls;
using Xamarin.Forms;

namespace DemoLive.Controls
{

    public class ProgressControl : RoundedBorderControl

    {

        /// <summary>

        /// The animation.

        /// </summary>

        private Animation _animation;



        /// <summary>

        /// Initializes a new instance of the <see cref="ProgressControl"/> class.

        /// </summary>

        public ProgressControl()

        {

            BackgroundColor = Xamarin.Forms.Color.Gray;



            var cog1 = new FontAwesomeLabel
            {

                Text = FontAwesomeLabel.FACog,

                FontSize = 39,

                TextColor = Xamarin.Forms.Color.FromHex("#CECECE"),

                HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center,

                VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center,

            };



            var cog2 = new FontAwesomeLabel
            {

                Text = FontAwesomeLabel.FACog,

                FontSize = 18,

                TextColor = Xamarin.Forms.Color.FromHex("#CECECE"),

                HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center,

                VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center,

            };



            _animation = new Animation((val) => {

                cog1.Rotation = val;

                cog2.Rotation = -val;

            }, 0, 360, Easing.Linear);



            var layout = new RelativeLayout();

            layout.Children.Add(cog1, () => new Xamarin.Forms.Rectangle((-layout.Width / 4) + 8, (layout.Height / 4) - 8,

                layout.Width, layout.Height));



            layout.Children.Add(cog2, () => new Xamarin.Forms.Rectangle(layout.Width - 36, -13,

                layout.Width, layout.Width));



            Content = layout;

        }



        /// <summary>

        /// Start animating this instance.

        /// </summary>

        public void Start()

        {

            _animation.Commit(this, "Rotation", length: 2000, repeat: () => true);

        }

    }
}
