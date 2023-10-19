using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ejercicio 1.
            IFilter fg = (IFilter) new FilterGreyscale();
            IFilter fn = (IFilter) new FilterNegative();

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            PipeSerial ps = new PipeSerial(fg, new PipeSerial(fn, new PipeNull()));
            IPicture p1 = ps.Send(picture);

            PipeSerial ps2 = (PipeSerial) ps.Next;
            IPicture p2 = ps2.Send(p1);

            PipeNull pn = (PipeNull) ps2.Next;
            IPicture p3 = pn.Send(p2);

            provider.SavePicture(p3, @"beer2.jpg");

            // Ejercicio 2.
            string baseFolder = "./trans/";

            provider.SavePicture(picture, baseFolder + @"beer_base_picture.jpg");
            provider.SavePicture(p1, baseFolder + @"beer_after_filter_negative.jpg");
            provider.SavePicture(p2, baseFolder + @"beer_after_filter_greyscale.jpg");
        }
    }
}
