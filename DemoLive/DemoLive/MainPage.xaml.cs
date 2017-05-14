using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoLive
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }

        private void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            (e.Item as VisualElement).FadeTo(1,200);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var current = Status.CurrentStatusIndex + 1;
            var max = Status.StatusesNumber - 1;
            if (current > max) current = 0;

            Status.CurrentStatusIndex = current;
        }
    }

    public class MainViewModel
    {
        public MainViewModel()
        {
            this.Items = new List<SampleItem>()
            {
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"},
                new SampleItem(){Title = "Randomm Title Lorem" ,Detail = "Ipsum Lorem details random some text"}
            };
        }

        public List<SampleItem> Items { get; set; }
    }

    public class SampleItem
    {
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}
