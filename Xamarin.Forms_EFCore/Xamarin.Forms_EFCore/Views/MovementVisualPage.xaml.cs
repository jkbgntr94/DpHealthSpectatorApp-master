using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.ViewModels;
using Xamarin.Forms_EFCore.ViewModels.Drawing;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovementVisualPage : ContentPage
	{
        private GameScene rawGameScene;
        private GameScene gameScene;
        private CCGameView _gameView;
        private float _x;
        private float _y;
        public MovementVisualPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //movImg.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png");
            heatImg.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.heat-map.png");

            CreateTopHalf(homeVisual);

            tempImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.temperature.png");
            pulseImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.pulse.png");
            dashboardImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.home.png");
            movementImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png");
            fallImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.fall.png");

            BindingContext = new MovementVisualViewModel();

            var vm = (MovementVisualViewModel)this.BindingContext;
            vm.MyEvent += (x, y) => { AddPoint(x, y); };

            var vma = (MovementVisualViewModel)this.BindingContext;
            vma.movePageFromMov += () => { goToFall(); };

            var vmb = (MovementVisualViewModel)this.BindingContext;
            vm.toHeatmap += () => { goToHeat(); };
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
                gameView.DesignResolution = new CCSizeI(SettingsController.MaxX, SettingsController.MaxY);
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new GameScene(gameView);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
                _gameView = gameView;
            }
        }


        private void AddLayer(float x, float y)
        {
            _x = x; _y = y;
            
            var agameScene = rawGameScene;

            var gameView = _gameView;


            agameScene.AddLayer(GameScene.newLayer(x,y));
            gameView.RunWithScene(agameScene);
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

            homeVisual.Children.Clear();
            homeVisual.Children.Add(gameView);

        }

        private void HandleViewCreatedPoints(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // This sets the game "world" resolution to 100x100:
                gameView.DesignResolution = new CCSizeI(SettingsController.MaxX, SettingsController.MaxY);
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new GameScene(gameView,_x,_y);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
            }
        }

        async void goToFall()
        {
            try
            {

                Navigation.InsertPageBefore(new FallVisualPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }

        }

        async void goToHeat()
        {
            try
            {

                Navigation.InsertPageBefore(new HeatMapPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }

        }

    }
}