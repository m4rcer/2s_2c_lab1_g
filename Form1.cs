using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2s_2c_lab1_g
{
    public partial class Form1 : Form
    {
        int TrianglesCount = 30;
        int Step = 50;
        int Angle = 6;
        PointF RotatePoint = new PointF(3, 3);


        public Form1()
        {
            InitializeComponent();
        }

        private void DrawTriangle(Graphics g, PointF[] points, Pen pen)
        {
            var trianglePoints = new List<PointF>(points);
            trianglePoints.Add(trianglePoints[0]);
            g.DrawLines(pen, trianglePoints.ToArray());
        }

        private PointF[] PointsToPixels(PointF[] points)
        {
            var width = pictureBox.Width;
            var height = pictureBox.Height;

            var pxPoints = new List<PointF>();

            foreach (PointF point in points)
            {
                var xk = point.X < 0 ? -1 : 1;
                var yk = point.Y < 0 ? -1 : 1;

                var newPoint = new PointF(width / 2 + (point.X * Step * xk), height /2 + (point.Y * Step * yk));

                pxPoints.Add(newPoint);
            }

            return pxPoints.ToArray();
        }

        private PointF[] RotatePoints(PointF[] points)
        {
            var cos = Math.Cos(Angle);
            var sin = Math.Sin(Angle);
            var x0 = RotatePoint.X;
            var y0 = RotatePoint.Y;

            var newPoints = new List<PointF>();

            foreach (PointF point in points) {
                var newX = x0 + (point.X - x0)*cos - (point.Y - y0)*sin;
                var newY = y0 + (point.X - x0)*sin + (point.Y - y0)*cos;

                var newPoint = new PointF((float)newX, (float)newY);

                newPoints.Add(newPoint);
            }

            return newPoints.ToArray();
        }

        private void DrawZero(Graphics g)
        {
            var zWidth = pictureBox.Width / 2;
            var zHeight = pictureBox.Height / 2;

            var zeroRect = new RectangleF(zWidth - 1, zHeight - 1, 2, 2);
            g.FillRectangle(Brushes.Red, zeroRect);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            var g = pictureBox.CreateGraphics();
            var pen = Pens.Black;

            var points = new PointF[] { new PointF(1, 1), new PointF(6, 1), new PointF(3, 5) };

            DrawZero(g);

            for (int i = 0; i < TrianglesCount; i++)
            {
                var pxPoints = PointsToPixels(points);

                DrawTriangle(g, pxPoints, pen);

                points = RotatePoints(points);
            }
        }
    }
}
