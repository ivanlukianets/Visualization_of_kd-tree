using System;
using System.Collections.Generic;
using System.Linq;


namespace AvaloniaApplication1;

public class KdTree
{
    public static List<(Point, Point, Point)> listToPrintBuilding { get; set; } = new List<(Point, Point, Point)>();
    
    public static List<Rectangle> listToPrintRectangles { get; set; } = new List<Rectangle>();
    
    public static Node CreateKdTreeRectangle(List<Point> points)
    {
        var medianPoint = FindMedian(points, "Longitude");
        double maxLatitude = points.MaxBy(x => x.Latitude).Latitude;
        double minLatitude = points.MinBy(x => x.Latitude).Latitude;
        double maxLongitude = points.MaxBy(x => x.Longitude).Longitude;
        double minLongitude = points.MinBy(x => x.Longitude).Longitude;
        Rectangle rootRectangle = new Rectangle(maxLatitude, minLatitude, maxLongitude, minLongitude);
        Node rootNode = new Node(medianPoint.Latitude, medianPoint.Longitude, medianPoint.Entries);
        rootNode.Rectangle = rootRectangle;
        points.Remove(medianPoint);
        BuildingOfTreeRectangle(rootNode,points,"Longitude");
        return rootNode;
    }
    public static void BuildingOfTreeRectangle(Node rootNode, List<Point> points, string line)
    {
        if (points.Count == 0)
            return;

        if (line == "Latitude")
        {
            var rightPoints = points.Where(x => x.Latitude >= rootNode.Latitude).ToList();
            var leftPoints = points.Where(x => x.Latitude < rootNode.Latitude).ToList();
            if (rightPoints.Count > 0 && leftPoints.Count > 0)
            {
                try
                {
                    var maxLongitude = rootNode.Rectangle.MaxLongitude;
                    var minLongitude = rootNode.Rectangle.MinLongitude;
                    Point point1 = new Point(rootNode.Latitude, maxLongitude);
                    Point point2 = new Point(rootNode.Latitude, minLongitude);
                    listToPrintBuilding.Add((new Point(rootNode.Latitude, rootNode.Longitude), point1, point2));
                }
                catch
                {
                    
                }
                
            }
            
            if (rightPoints.Count > 0)
            {
                if (rightPoints.Count > 1)
                {
                    var rightPoint = FindMedian(rightPoints, "Longitude");
                    Rectangle rectangleWithRightLatitude = Rectangle.DivideByLatitude(new Point(rootNode.Latitude, rootNode.Longitude),
                        rootNode.Rectangle).Item2; 
                    Node rightSon = new Node(rightPoint.Latitude, rightPoint.Longitude, rightPoint.Entries);
                    rightSon.Rectangle = rectangleWithRightLatitude;
                    rootNode.RightSon = rightSon;
                    rightPoints.Remove(rightPoint);
                    BuildingOfTreeRectangle(rightSon, rightPoints, "Longitude");
                }
                else 
                {
                    var rightPoint = rightPoints.Single();
                    rootNode.RightSon = new Node(rightPoint.Latitude, rightPoint.Longitude, rightPoint.Entries);
                }
            }
            
            if (leftPoints.Count > 0)
            {
                if (leftPoints.Count > 1)
                {
                    var leftPoint = FindMedian(leftPoints, "Longitude");
                    Rectangle rectangleWithLeftLatitude = Rectangle.DivideByLatitude(new Point(rootNode.Latitude, rootNode.Longitude),
                        rootNode.Rectangle).Item1; 
                    Node leftSon = new Node(leftPoint.Latitude, leftPoint.Longitude, leftPoint.Entries);
                    leftSon.Rectangle = rectangleWithLeftLatitude;
                    rootNode.LeftSon = leftSon;
                    leftPoints.Remove(leftPoint);
                    BuildingOfTreeRectangle(leftSon, leftPoints, "Longitude");
                }
                else 
                {
                    var leftPoint = leftPoints.Single();
                    rootNode.LeftSon = new Node(leftPoint.Latitude, leftPoint.Longitude, leftPoint.Entries);
                }
            }
        }
        else if (line == "Longitude")
        {
            var rightPoints = points.Where(x => x.Longitude >= rootNode.Longitude).ToList();
            var leftPoints = points.Where(x => x.Longitude < rootNode.Longitude).ToList();
            if (rightPoints.Count > 0 && leftPoints.Count > 0)
            {
                try
                {
                    var maxLatitude = rootNode.Rectangle.MaxLatitude;
                    var minLatitude = rootNode.Rectangle.MinLatitude;
                    Point point1 = new Point(maxLatitude, rootNode.Longitude);
                    Point point2 = new Point(minLatitude, rootNode.Longitude);
                    listToPrintBuilding.Add((new Point(rootNode.Latitude,rootNode.Longitude),point1,point2));
                }
                catch
                {
                }
            }
            if (rightPoints.Count > 0)
            {
                if (rightPoints.Count > 1)
                {
                    var rightPoint = FindMedian(rightPoints, "Latitude");
                    Rectangle rectangleWithRightLongitude =
                        Rectangle.DivideByLongitude(new Point(rootNode.Latitude, rootNode.Longitude),
                            rootNode.Rectangle).Item2; 
                    Node rightSon = new Node(rightPoint.Latitude, rightPoint.Longitude, rightPoint.Entries);
                    rightSon.Rectangle = rectangleWithRightLongitude;
                    rootNode.RightSon = rightSon;
                    rightPoints.Remove(rightPoint);
                    BuildingOfTreeRectangle(rightSon, rightPoints, "Latitude");
                }
                else
                {
                    var rightPoint = rightPoints.Single();
                    rootNode.RightSon = new Node(rightPoint.Latitude, rightPoint.Longitude, rightPoint.Entries);
                }
            }

            if (leftPoints.Count > 0)
            {
                if (leftPoints.Count > 1)
                {
                    var leftPoint = FindMedian(leftPoints, "Latitude");
                    Rectangle rectangleWithLeftLongitude =
                        Rectangle.DivideByLongitude(new Point(rootNode.Latitude, rootNode.Longitude),
                            rootNode.Rectangle).Item1;
                    Node leftSon = new Node(leftPoint.Latitude, leftPoint.Longitude, leftPoint.Entries);
                    leftSon.Rectangle = rectangleWithLeftLongitude;
                    rootNode.LeftSon = leftSon;
                    leftPoints.Remove(leftPoint);
                    BuildingOfTreeRectangle(leftSon, leftPoints, "Latitude");
                }
                else
                {
                    var leftPoint = leftPoints.Single();
                    rootNode.LeftSon = new Node(leftPoint.Latitude, leftPoint.Longitude, leftPoint.Entries);
                }
            }
        }
    }
    
    public static Point FindMedian(List<Point> points, string line)
    {
        int lenght = points.Count;
        if (line == "Latitude")
        {
            return points.OrderBy(x => x.Latitude).ToList()[(lenght / 2)];
        }
        else if (line == "Longitude")
        {
            return points.OrderBy(x => x.Longitude).ToList()[(lenght / 2)];
        }
        else
        {
            throw new ArgumentException();
        }
    }
    
    public static List<(Point point, double distance)> SearchKdTreeRectangle(Node rootNode, Point target, int maxCount,
        int size)
    {
        List<(Point point, double distance)> pointsInArea = new List<(Point point, double distance)>();
        Rectangle targetRectangle = GetRectangle(target, size);
        listToPrintRectangles.Add(targetRectangle);
        SearchingInRectangles(pointsInArea, rootNode, target, targetRectangle, size);
        return pointsInArea.OrderBy(x => x.distance).Take(maxCount).ToList();
    }

    private static Rectangle GetRectangle(Point target, int size)
    {
        double latRad = target.Latitude * Math.PI / 180.0;
        double lonRad = target.Longitude * Math.PI / 180.0;
        size *= 1000;
        double R = 6371e3;
        double maxLatitude = Math.Asin(Math.Sin(latRad) * Math.Cos(size / R) + Math.Cos(latRad) * Math.Sin(size / R) * Math.Cos(0));
        double maxLongitude = lonRad + Math.Atan2(Math.Sin(Math.PI/2) * Math.Sin(size / R) * Math.Cos(latRad), Math.Cos(size / R) - Math.Sin(latRad) * Math.Sin(maxLatitude));
        
        double minLatitude = Math.Asin(Math.Sin(latRad) * Math.Cos(size / R) + Math.Cos(latRad) * Math.Sin(size / R) * Math.Cos(Math.PI));
        double minLongitude = lonRad + Math.Atan2(Math.Sin(-Math.PI/2) * Math.Sin(size / R) * Math.Cos(latRad), Math.Cos(size / R) - Math.Sin(latRad) * Math.Sin(minLatitude));

        return new Rectangle(maxLatitude*180.0/Math.PI, minLatitude*180.0/Math.PI, maxLongitude*180.0/Math.PI, minLongitude*180.0/Math.PI);
    }

    private static void SearchingInRectangles(List<(Point point, double distance)> pointsInArea, Node node, Point target, Rectangle targetRectangle, int size)
    {
        if (node == null) return;

        if (node.Rectangle != null)
        {
            if (targetRectangle.IntersectsRectangle(node.Rectangle))
            {
                listToPrintRectangles.Add(node.Rectangle);
                var currentPoint = new Point(node.Latitude,node.Longitude, node.Entries);
                double distance = DistanceBetweenTwoPoints(currentPoint, target);
                if (distance <= size) pointsInArea.Add((currentPoint, distance));
                SearchingInRectangles(pointsInArea, node.RightSon, target, targetRectangle, size);
                SearchingInRectangles(pointsInArea, node.LeftSon, target, targetRectangle, size);
            }
        }
        else
        {
            var currentPoint = new Point(node.Latitude,node.Longitude, node.Entries);
            if (targetRectangle.RectangleContainsPoint(currentPoint))
            {
                if (DistanceBetweenTwoPoints(currentPoint, target) <= size) pointsInArea.Add((currentPoint, DistanceBetweenTwoPoints(currentPoint, target)));
                // SearchingInRectangles(pointsInArea, node.RightSon, target, targetRectangle, size);
                // SearchingInRectangles(pointsInArea, node.LeftSon, target, targetRectangle, size);
            }
        }
    }
    
    public static double DistanceBetweenTwoPoints(Point point1, Point point2)
    {
        double r = 6371e3;
        double f1 = point1.Latitude * Math.PI / 180;
        double f2 = point2.Latitude * Math.PI / 180;
        double deltaF = (point2.Latitude - point1.Latitude) * Math.PI / 180;
        double deltaLambda = (point2.Longitude - point1.Longitude) * Math.PI / 180;
        double a = Math.Sin(deltaF / 2) * Math.Sin(deltaF / 2) +
                   Math.Cos(f1) * Math.Cos(f2) * Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double d = r * c;
        return d/1000;
    }
}