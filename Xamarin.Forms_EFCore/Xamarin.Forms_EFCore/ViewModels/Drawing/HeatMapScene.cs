using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;

namespace Xamarin.Forms_EFCore.ViewModels.Drawing
{
    class HeatMapScene : CCScene
    {
        CCDrawNode circle;
        CCDrawNode square;
      
        DatabaseContext _context;
        public HeatMapScene(CCGameView gameView, float index, int max) : base(gameView)
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


            if (index > max - 10) index = max - 10;

            var sekv = _context.MovementSekv.Where(t => t.PohSekvId >= index).ToList();
            int poc = 1;
            List<int> vyskyt = new List<int>();
            foreach (var s in sekv)
            {
                if (poc > 10) break;
                //System.Diagnostics.Debug.WriteLine("POCET +++++ " + s.Cas_Zotrvania);
                int n = 0;
                Int32.TryParse(s.Cas_Zotrvania, out n);
                
                vyskyt.Add(n);
                poc++;
            }
            
            int maxVal = vyskyt.Max();

            int divide = maxVal / 4;
            

            poc = 1;

            foreach (var s in sekv)
            {
                
                if (poc > 10) break;

                circle = new CCDrawNode();
                layer.AddChild(circle);
                
                
                /*circle.DrawCircle(
                    new CCPoint(0, 0),
                    radius: 2,
                    color: CCColor4B.Blue);
                circle.PositionX = s.Xhodnota;
                circle.PositionY = s.Yhodnota;*/
                int n = 0;
                Int32.TryParse(s.Cas_Zotrvania, out n);

                if (n >= 0 && n<= divide)
                {
                    circle.DrawSolidCircle(
                    pos: new CCPoint(s.Xhodnota, s.Yhodnota),
                    radius: 2,
                    color: CCColor4B.Green);

                }else if(n > divide && n<= divide * 2)
                {
                    circle.DrawSolidCircle(
                    pos: new CCPoint(s.Xhodnota, s.Yhodnota),
                    radius: 4,
                    color: CCColor4B.Yellow);

                }
                else if (n > divide*2 && n <= divide * 3)
                {
                    circle.DrawSolidCircle(
                    pos: new CCPoint(s.Xhodnota, s.Yhodnota),
                    radius: 6,
                    color: CCColor4B.Orange);

                }
                else
                {
                    circle.DrawSolidCircle(
                    pos: new CCPoint(s.Xhodnota, s.Yhodnota),
                    radius: 8,
                    color: CCColor4B.Red);

                }

                var text = new CCLabel(s.Cas_Zotrvania, "Arial",10)
                {
                    Position = new CCPoint(s.Xhodnota, s.Yhodnota),
                    Color = CCColor3B.Black,
                    IsAntialiased = true
                };

                layer.AddChild(text);
                poc++;
            }


            
        }
    }
}
