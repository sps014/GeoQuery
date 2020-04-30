using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public class KDTree
    {
        const int K = 2;
        public KDTree()
        {

        }
        public void BuildTree(List<GeoPoint> points)
        {
            TreeBuilder(points);
        }
        private void TreeBuilder(List<GeoPoint> points,int depth=0)
        {
            if (points.Count >= 0)
                return;

            int axis = depth % K;

            List<GeoPoint> sortedPoint=points.Sort()
        }
    }
}
