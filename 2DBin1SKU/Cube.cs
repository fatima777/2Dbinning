///*************************************************************************//  
/// Author: Zhang Yiren
/// Update:
/// 0.0.1    Create     Zhang Yiren     
///
///***************************************************************************//
///

using System.Drawing;

namespace Binning.D2
{
    
    public class Cube
    {
        protected int _length;    //长度
        protected int _width;     //宽度       
        protected int _height;    //高度

        //Tolerence 正值表示占用额外的体积，负值表示减少可用体积
        protected int _lengthTolerence;
        protected int _widthTolerence;
        protected int _heightTolerence;

        #region properties

        public int Length
        {
            get
            {
                return _length;
            }

            set
            {
                _length = value;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        #endregion

        public Cube(int length, int width,int height)
        {
            _length = length;
            _width = width;
            _height = height;
        }

        public void SetSize(int length, int width, int height)
        {
            _length = length;
            _width = width;
            _height = height;
        }
        
        public void SetTolerence(int lt, int wt, int ht)
        {
            _lengthTolerence = lt;
            _widthTolerence = wt;
            _heightTolerence = ht;
        }

        public bool IsValid()
        {
            int l = (int)_length + _lengthTolerence;
            int w = (int)_width + _widthTolerence;
            int h = (int)_height + _heightTolerence;

            if ((l > 0) && (w > 0) && (h > 0))
                return true;
            else
                return false;
        }

        public int GetFillLength()
        {

            if (IsValid())
                return (int)_length + _lengthTolerence;
            else
                return 0;
        }

        public int GetFillWidth()
        {

            if (IsValid())
                return (int)_width + _widthTolerence;
            else
                return 0;
        }

        public int GetFillHeight()
        {

            if (IsValid())
                return (int)_height + _heightTolerence;
            else
                return 0;
        }

        //Cube的长宽高采用国人默认模式，所有长是底面矩形的宽，宽是底面矩形的高
        public Rectangle GetTopViewRect()
        {
            if (IsValid())
            {
                Rectangle rect = new Rectangle();
                rect.X = 0;
                rect.Y = 0;
                rect.Width = GetFillLength();
                rect.Height = GetFillWidth();
                return rect;
            }
            else
            {
                return Rectangle.Empty;
            }
        }


    }
}
