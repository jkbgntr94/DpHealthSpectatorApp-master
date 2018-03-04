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
    public partial class DrawHomePage : ContentPage
    {
        GameScene gameScene;
        Button addRoom;
        public DrawHomePage()
        {
            //InitializeComponent ();
            // This is the top-level grid, which will split our page in half
            var grid = new Grid();
            this.Content = grid;
            grid.RowDefinitions = new RowDefinitionCollection {
            // Each half will be the same size:
            new RowDefinition{ Height = new GridLength(2, GridUnitType.Star)},
            new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
        };
            CreateTopHalf(grid);
            CreateBottomHalf(grid);

            addRoom.Clicked += (sender, e) => {
                Application.Current.MainPage.Navigation.PushAsync(new ChooseSecPhasePage());
            };

        }

        void CreateTopHalf(Grid grid)
        {
            var gameView = new CocosSharpView()
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = HandleViewCreated
            };
            // We'll add it to the top half (row 0)
            grid.Children.Add(gameView, 0, 0);
        }
        void CreateBottomHalf(Grid grid)
        {
            // We'll use a StackLayout to organize our buttons
            var stackLayout = new StackLayout();

            // The second button will move the circle to the right when clicked:
            addRoom = new Button
            {
                Text = "Pridať"
            };
            stackLayout.Children.Add(addRoom);
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
                 gameScene = new GameScene(gameView);
                 // Starts CocosSharp:
                 gameView.RunWithScene(gameScene);
             }
         }


     }

        
    }
