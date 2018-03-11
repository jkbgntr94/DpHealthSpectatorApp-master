using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels.Drawing;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeatMapPage : ContentPage
	{
        HeatMapScene gameScene;
        Label label;
        private double StepValue;
        Slider slider;
        int max;
        float value = 0;
        StackLayout stackLayoutMap;
        Grid gridMain;

        public HeatMapPage ()
		{
            StepValue = 1.0;
            //InitializeComponent ();
            // This is the top-level grid, which will split our page in half
            gridMain = new Grid();
            this.Content = gridMain;
            gridMain.RowDefinitions = new RowDefinitionCollection {
            // Each half will be the same size:
            new RowDefinition{ Height = new GridLength(2, GridUnitType.Star)},
            new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
        };
            CreateTopHalf(gridMain);
            CreateBottomHalf(gridMain);
           

        }

        void CreateTopHalf(Grid grid)
        {
            stackLayoutMap = new StackLayout();

            var image = new Image
            {
                Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png"),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 30
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                MoveToMovement();
            };
            image.GestureRecognizers.Add(tapGestureRecognizer);

            var gameView = new CocosSharpView()
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = HandleViewCreated
            };
            // We'll add it to the top half (row 0)
            stackLayoutMap.Children.Add(image);
            stackLayoutMap.Children.Add(gameView);
            grid.Children.Add(stackLayoutMap, 0, 0);
        }
        void CreateBottomHalf(Grid grid)
        {
            // We'll use a StackLayout to organize our buttons
            StackLayout stackLayout = new StackLayout();

            HeatMapMethods heatMapMethods = new HeatMapMethods();

            max = heatMapMethods.getMaxMovementSekvIndex();

            slider = new Slider
            {
                Maximum = (float)max,
                Minimum = 1.0f,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            slider.ValueChanged += OnSliderValueChanged;

            label = new Label
            {
                Text = "Slider value is 0",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Button button = new Button
            {
                Text = "Show",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnButtonClicked;

            
            
            stackLayout.Children.Add(slider);
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(button);
            // The stack layout will be in the bottom half (row 1):

            grid.Children.Add(stackLayout, 0, 1);

        }

    

        void HandleViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // This sets the game "world" resolution to 100x100:
                gameView.DesignResolution = new CCSizeI(150, 150);
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new HeatMapScene(gameView, 0 , max);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var gridList = gridMain.Children.ToList();
            foreach(var g in gridList)
            {
                if(Grid.GetRow(g) == 0)
                {
                    gridMain.Children.Remove(g);

                }

            }


            AddHeatMap();
           

        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);

            var val = newStep * StepValue;
            value = (float)val;

            slider.Value = val;
            

            label.Text = String.Format("Slider value is {0}", newStep);
            
        }

        private void AddHeatMap()
        {
            var image = new Image
            {
                Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png"),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 30
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                MoveToMovement();
            };
            image.GestureRecognizers.Add(tapGestureRecognizer);

            stackLayoutMap = new StackLayout();
            var gameView = new CocosSharpView()
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = HandleViewCreatedPoints
            };
            // We'll add it to the top half (row 0)
            stackLayoutMap.Children.Add(image);
            stackLayoutMap.Children.Add(gameView);
            gridMain.Children.Add(stackLayoutMap, 0, 0);

        }

        private void HandleViewCreatedPoints(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // This sets the game "world" resolution to 100x100:
                gameView.DesignResolution = new CCSizeI(150, 150);
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new HeatMapScene(gameView, value, max);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
            }
        }

        async void MoveToMovement()
        {
            try
            {

                Navigation.InsertPageBefore(new MovementVisualPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }


        }
    }
}