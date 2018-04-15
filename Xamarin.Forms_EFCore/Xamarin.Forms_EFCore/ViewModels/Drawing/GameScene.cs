using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;

namespace Xamarin.Forms_EFCore.ViewModels.Drawing
{
    
    public class GameScene : CCScene
    {
        CCDrawNode circle;
        CCDrawNode square;
        DatabaseContext _context;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            var layer = new CCLayer();
            this.AddLayer(layer);
            _context = new DatabaseContext();

            var rooms = _context.Rooms.ToList();

            //System.Diagnostics.Debug.WriteLine("******************************count " + rooms.Count);

            foreach (var r in rooms)
            {
                float ldx = r.LavaXhodnota * 10;
                float ldy = r.LavaYhodnota * 10;

                float rhx = r.PravaXhodnota * 10;
                float rhy = r.PravaYhodnota * 10;

                square = new CCDrawNode();
                layer.AddChild(square);

                CCPoint[] verts = new CCPoint[] {
                new CCPoint(ldx,ldy),
                new CCPoint(rhx, ldy),
                new CCPoint(rhx, rhy),
                new CCPoint(ldx, rhy)
                };

                square.DrawPolygon(verts,
               count: verts.Length,
               fillColor: CCColor4B.White,
               borderWidth: 1,
               borderColor: CCColor4B.Red,
               closePolygon: true);

                /*square.PositionX = ldx;
                square.PositionY = ldy;*/

            }


        }

        public GameScene(CCGameView gameView,float x, float y) : base(gameView)
        {
            var layer = new CCLayer();
            this.AddLayer(layer);
            _context = new DatabaseContext();

            var rooms = _context.Rooms.ToList();

            //System.Diagnostics.Debug.WriteLine("******************************count " + rooms.Count);

            foreach (var r in rooms)
            {
                float ldx = r.LavaXhodnota * 10;
                float ldy = r.LavaYhodnota * 10;

                float rhx = r.PravaXhodnota * 10;
                float rhy = r.PravaYhodnota * 10;

                square = new CCDrawNode();
                layer.AddChild(square);

                CCPoint[] verts = new CCPoint[] {
                new CCPoint(ldx,ldy),
                new CCPoint(rhx, ldy),
                new CCPoint(rhx, rhy),
                new CCPoint(ldx, rhy)
                };

                square.DrawPolygon(verts,
               count: verts.Length,
               fillColor: CCColor4B.White,
               borderWidth: 1,
               borderColor: CCColor4B.Red,
               closePolygon: true);

                /*square.PositionX = ldx;
                square.PositionY = ldy;*/

            }
            circle = new CCDrawNode();
            layer.AddChild(circle);
            circle.DrawSolidCircle(
                  pos: new CCPoint(0, 0),
                  radius: 4,
                  color: CCColor4B.Blue);
            circle.PositionX = x;
            circle.PositionY = y;
        }

        public static CCLayer newLayer(float x, float y)
        {
            var layer = new CCLayer();

            CCDrawNode circle = new CCDrawNode();
            layer.AddChild(circle);
            circle.DrawSolidCircle(
                  pos: new CCPoint(0, 0),
                  radius: 4,
                  color: CCColor4B.Blue);
            circle.PositionX = x;
            circle.PositionY = y;

            return layer;
        }

       

    }
}
