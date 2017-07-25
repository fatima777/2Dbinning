///*************************************************************************//  
/// Author: Zhang Yiren
/// Update:
/// 0.0.1    Create     Zhang Yiren     
///
///***************************************************************************//

using System.Collections.Generic;
using System.Drawing;

namespace Binning.D2
{
    public class FillPattern
    {
        private int _level=0;

        private SortedList<int, Rectangle> _rectList ;

        private int _lastRectNo = 0;

        private int _currentRectNo = 0;

        public SortedList<int, Rectangle> RectList
        {
            get
            {
                return _rectList;
            }
        }

        public FillPattern()
        {
            _rectList = new SortedList<int, Rectangle>();
        }

        public Rectangle GetNextRect()
        {
            if (RectList.ContainsKey(_currentRectNo))
            {
                _currentRectNo++;
                return RectList[_currentRectNo-1];
            }
            else
            {
                return Rectangle.Empty;
            }
        }

        public void SetLevel(int level)
        {
            if (level > 0)
            {
                _level = level;
            }
        }

        public void AddRect(int x,int y, int length, int width)
        {
            if (!RectList.ContainsKey(_lastRectNo))
            {
                Rectangle rect = new Rectangle(x, y, length, width);
                RectList.Add(_lastRectNo, rect);
                _lastRectNo++;
            }
        }

        public void Reset()
        {
            _rectList.Clear();
            _lastRectNo = 0;
            _currentRectNo = 0;
        }

    }
}
