using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

// Gallery view of banner images to choose for file
// TODO: Save choice on web for event
//       Add more banners

namespace UEApp
{
    public partial class GalleryView : PopupPage
    {
        public GalleryView()
        {
            InitializeComponent();

            string[] sourceArray = new string[15] { "campusloop_banner_red_240dp.png", "UC.png", "EntranceSign.jpg", "McMickenGathering.jpg", "FootballStadium.jpg", "Tennis.jpg", "Book.jpg", "Business.jpg", "Engineer.jpg", "Food.jpg", "Math.jpg", "Music.jpg", "Nursing.jpg", "Science.jpg", "Technology.jpg"};
            Image[] imageArray = new Image[15];

            var tapGestureRecognizer = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
            };

            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                ImageTapped(sourceArray[Convert.ToInt16(((Image)sender).StyleId)]);
            };

            ImageLayout.Children.Clear();
            for (int i = 0; i < 15; i++)
            {
                ImageLayout.Children.Add(imageArray[i] = new Image { Source = sourceArray[i], Aspect = Aspect.AspectFit, StyleId = i.ToString() });
                imageArray[i].GestureRecognizers.Add(tapGestureRecognizer);
            }
        }

        private void ImageTapped(string imgSrc)
        {
            // TODO: Store image chosen on web
            PopupNavigation.PopAsync();
        }
    }
}