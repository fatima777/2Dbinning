///*************************************************************************//  
/// Author: Zhang Yiren
/// Update:
/// 0.0.1    Create     Zhang Yiren     
///
///***************************************************************************//
///

namespace Binning.D2
{
    public class Sku
    {
        private string _ID;
        private string _name;
        private string _Description;

        public Cube Cube = null;

        public string ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        public Sku(int length, int width, int height, int lTolerence = 0, int wTolerence = 0, int hTolerence = 0)
        {
            Cube = new Cube(length, width, height);
            Cube.SetTolerence(lTolerence, wTolerence, hTolerence);
        }

        public void UpdateSize(int length, int width, int height, int lTolerence = 0, int wTolerence = 0, int hTolerence = 0)
        {
            Cube.SetSize(length, width, height);
            Cube.SetTolerence(lTolerence, wTolerence, hTolerence);
        }
    }
}
