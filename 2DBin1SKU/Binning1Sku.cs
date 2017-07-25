///*************************************************************************//  
/// Author: Zhang Yiren
/// Update:
/// 0.0.1    Create     Zhang Yiren     
///
///***************************************************************************//


using System;
using System.Drawing;
using System.Collections.Generic;

namespace Binning.D2
{
    public class Binning1Sku:IBinning
    {
        private Bin _bin = null;
        private Sku _sku = null;

        private bool _isValid = false;

        private FillPattern _fillPattern = null;
        private Rectangle _slice = Rectangle.Empty;

        private AnchorPoint _anchor = AnchorPoint.LeftTop;
        private FillMethod _fill = FillMethod.LengthFirst;

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
        }

        public FillPattern FillPattern
        {
            get
            {
                return _fillPattern;
            }
        }

        public Binning1Sku(Bin bin, Sku sku)
        {
            
            _bin = bin;
            _sku = sku;
            if ((bin != null) && (bin.Cube.IsValid()) && (sku!=null)&&(sku.Cube.IsValid()))
            {
                _isValid = true;
            }

                _fillPattern = new FillPattern();
        }

        public void GeneratePattern()
        {
            int c1 = 0, c2 = 0;
            int c3 = 0, c4 = 0;
            int f1 = 0, f2 = 0;
            int x = 0 , y = 0;

            _fillPattern.Reset();

            FillPattern.SetLevel(_bin.Cube.Height/_sku.Cube.Height);
            if (!_bin.Cube.IsValid() || !_sku.Cube.IsValid())
                return;
            
            switch(_fill)
            {
                case FillMethod.LengthFirst:
                    f1 = _sku.Cube.Width;
                    f2 = _sku.Cube.Length;
                    break;
                case FillMethod.WidthFirst:
                    f1 = _sku.Cube.Length;
                    f2 = _sku.Cube.Width;
                    break;
            }
            
            //Vertical LinearFill
            LinearFill(_bin.Cube.Width, f1, f2, out c1, out c2);
            //Horizontal LinearFill
            if (c1 > 0)
            {
                LinearFill(_bin.Cube.Length, f2, f1, out c3, out c4);
                //if c1>0,not allow c3 = 0;
                if (c3 == 0)
                {
                    LinearFill(_bin.Cube.Length % f2, f2, f1, out c3, out c4);
                    c3 = c3 + _bin.Cube.Length / f2 + c3;
                }
            
            }
            //fillin by three times: according to fillmethod
            //first time is the main area by c3,c1
            //Fillin is always vertical first then horizontal 
            for (int i = 0; i < c3; i++)
            {
                for (int j = 0; j < c1; j++)
                {
                    //swap the length & width when create the pattern
                    FillPattern.AddRect(x, y, f2, f1);
                    y = y + f1;
                }
                x = x + f2;
                y = 0;
            }

            //second time is the most right area by c4 & (bin.width-sku.length*c1)/f2
            //set x to right value, y to 0
            x = f2 * c3;
            y = 0;
            //for (int i = 0; i < (_bin.Cube.Length - f2 * c3) / f1; i++)
            //{
            //    for (int j = 0; j < c4; j++)
            //    {
            //        //swap the length & width when create the pattern
            //        FillPattern.AddRect(x, y, f1, f2);
            //        y = y + f2;
            //    }
            //    x = x + f1;
            //    y = 0;
            //}

            for (int i = 0; i < c4; i++)
            {
                for(int j = 0; j< (_bin.Cube.Width - f2*c2)/f2;j++)
                { 
                    FillPattern.AddRect(x, y, f1, f2);
                    y = y + f2;
                }
                   x = x + f1;
                   y = 0;
            }

            //third time is the bottom area by c2 & bin.length/f1
            //set x to 0, y to right value
            x = 0;
            y = f1 * c1;
            for (int i = 0; i < _bin.Cube.Length/f1; i++)
            {
                for (int j = 0; j < c2; j++)
                {
                    //swap the length & width when create the pattern
                    FillPattern.AddRect(x, y, f1, f2);
                    y = y + f2;
                }
                x = x + f1;
                y = f1 * c1;
            }
        }

        public void LinearFill(int len, int f1, int f2, out int c1, out int c2)
        {
            //int m = 0;
            int left = 0;

            SortedList<int, int> result = new SortedList<int, int>();

            c1 = c2 = 0;

            if ((len <= 0) || (f1 <= 0) || (f2 <= 0))
            {
                return;
            }

            //m = Math.Min(f1, f2);
            int[] n1 = new int[len / f1+1];
            int[] n2 = new int[len / f1+1];

            for (int i = (len / f1); i >= 0; i--)
            {
                n1[i] = i;
                n2[i] = (len - f1 * i) / f2;
                left = len - f1 * n1[i] - f2 * n2[i];
                if (!result.ContainsKey(left))
                {
                    result.Add(left, i);
                }                 
            }

            if (result.Count > 0)
            {
                c1 = n1[result.Values[0]];
                c2 = n2[result.Values[0]];
            }
          }


     }

    public enum AnchorPoint
    {
        LeftTop = 0,
        LeftBottom = 1,
        RightTop = 2,
        RightBottom = 3,
        Center = 4,
    }

    public enum FillMethod
    {
        LengthFirst = 0,
        WidthFirst  = 1,
    }
}
