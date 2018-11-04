//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.Database;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Java.Lang;

//namespace projekt_1.Adapters
//{
//    public class ColorsSpinnerAdapter : ISpinnerAdapter
//    {

//        private readonly IList<Color> _colors = new List<Color> { Color.Black, Color.Red, Color.Purple };

//        public int Count => _colors.Count;

//        public bool HasStableIds => true;

//        public bool IsEmpty => _colors.Count == 0;

//        public int ViewTypeCount => _colors.Count;

//        public IntPtr Handle => throw new NotImplementedException();

//        public void Dispose()
//        {
//        }

//        public View GetDropDownView(int position, View convertView, ViewGroup parent)
//        {
//            throw new NotImplementedException();
//        }

//        public Java.Lang.Object GetItem(int position)
//        {
//            throw new NotImplementedException();
//        }

//        public long GetItemId(int position)
//        {
//            throw new NotImplementedException();
//        }

//        public int GetItemViewType(int position)
//        {
//            throw new NotImplementedException();
//        }

//        public View GetView(int position, View convertView, ViewGroup parent)
//        {
//            throw new NotImplementedException();
//        }

//        public void RegisterDataSetObserver(DataSetObserver observer)
//        {
//            throw new NotImplementedException();
//        }

//        public void UnregisterDataSetObserver(DataSetObserver observer)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}