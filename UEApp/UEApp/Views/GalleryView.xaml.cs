using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using System.ComponentModel;

// Gallery view of banner images to choose for file
// TODO: Save choice on web for event
//       Add more banners

namespace UEApp
{
    public partial class GalleryView : PopupPage, INotifyPropertyChanged
    {
        public event EventHandler<string> ImageSelected;
        public Image[] imageArray = new Image[15];

        public GalleryView()
        {
            InitializeComponent();

            IsLoading = true;

            string[] sourceArray = new string[15] { "campusloop_banner_red_240dp.png", "UC.png", "EntranceSign.jpg", "McMickenGathering.jpg", "FootballStadium.jpg", "Tennis.jpg", "Book.jpg", "Business.jpg", "Engineer.jpg", "Food.jpg", "Math.jpg", "Music.jpg", "Nursing.jpg", "Science.jpg", "Technology.jpg"};
            
            var tapGestureRecognizer = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
            };

            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                string imgSrc = sourceArray[Convert.ToInt16(((Image)sender).StyleId)];
                ImageSelected(this, imgSrc);
            };

            ImageLayout.Children.Clear();
            for (int i = 0; i < 15; i++)
            {
                ImageLayout.Children.Add(imageArray[i] = new Image { Source = sourceArray[i], Aspect = Aspect.AspectFit, StyleId = i.ToString() });
                imageArray[i].GestureRecognizers.Add(tapGestureRecognizer);
            }

            IsLoading = false;
            BindingContext = this;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ImageLayout.Children.Clear();
            for (int i = 0; i < 15; i++)
            {
                imageArray[i] = null;
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("isLoading");
            }
        }

        public bool IsNotLoading
        {
            get
            {
                return !(this.isLoading);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}