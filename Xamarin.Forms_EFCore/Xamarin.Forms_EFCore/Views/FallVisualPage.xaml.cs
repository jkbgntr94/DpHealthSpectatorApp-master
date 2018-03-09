using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels;
using Xamarin.Forms_EFCore.ViewModels.Drawing;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FallVisualPage : ContentPage
	{

        private GameSceneFall gameScene;
        private CCGameView _gameView;
        private float _x;
        private float _y;
        public FallVisualPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CreateTopHalf(homeVisualFall);

            tempImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.temperature.png");
            pulseImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.pulse.png");
            dashboardImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.home.png");
            movementImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png");
            fallImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.fall.png");

            BindingContext = new FallVisualViewModel();

            var vm = (FallVisualViewModel)this.BindingContext;
            vm.MyEventFall += (x, y) => { AddPoint(x, y); };
        }

        private void CreateTopHalf(StackLayout stack)
        {
            stack.Children.Clear();
            var gameView = new CocosSharpView()
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = HandleViewCreated
            };
            // We'll add it to the top half (row 0)
            stack.Children.Add(gameView);
        }

        private void HandleViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // This sets the game "world" resolution to 100x100:
                gameView.DesignResolution = new CCSizeI(150, 150);
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new GameSceneFall(gameView);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
                _gameView = gameView;
            }
        }

        private void AddPoint(float x, float y)
        {
            _x = x; _y = y;

            var gameView = new CocosSharpView()
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = HandleViewCreatedPoints
            };
            // We'll add it to the top half (row 0)

            homeVisualFall.Children.Clear();
            homeVisualFall.Children.Add(gameView);

        }

        private void HandleViewCreatedPoints(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // This sets the game "world" resolution to 100x100:
                gameView.DesignResolution = new CCSizeI(150, 150);
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new GameSceneFall(gameView, _x, _y);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
            }
        }

    }
}