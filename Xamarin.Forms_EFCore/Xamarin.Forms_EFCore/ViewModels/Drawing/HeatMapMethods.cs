using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;


namespace Xamarin.Forms_EFCore.ViewModels.Drawing
{

    class HeatMapMethods
    {

        DatabaseContext _context;

        public int getMaxMovementSekvIndex()
        {
            _context = new DatabaseContext();

            Pohyb_Sekvencia pohS = _context.MovementSekv.FirstOrDefault(t => t.PohSekvId == _context.MovementSekv.Max(x => x.PohSekvId));


            return pohS.PohSekvId;
        }

    }
}
