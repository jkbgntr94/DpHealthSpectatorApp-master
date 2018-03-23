using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.ViewModels.Drawing;

namespace Xamarin.Forms_EFCore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawHomePage : ContentPage
    {
        GameScene gameScene;
        Button addRoom;
        Button nextPage;
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

                goToDraw();

               // Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());
            };

            nextPage.Clicked += (sender, e) => {

                goToDash();

                // Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());
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

            nextPage = new Button
            {
                Text = "Ďalej"
            };
            stackLayout.Children.Add(nextPage);
            // The stack layout will be in the bottom half (row 1):

            grid.Children.Add(stackLayout, 0, 1);
        }

         void HandleViewCreated(object sender, EventArgs e)
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
             }
         }

        async void goToDash()
        {
            try
            {

                Navigation.InsertPageBefore(new InstructionPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }

        }

        async void goToDraw()
        {
            try
            {
                Navigation.InsertPageBefore(new AddRoomsPage(), this);

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
