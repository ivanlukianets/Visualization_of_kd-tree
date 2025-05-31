using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Color = System.Drawing.Color;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    private readonly List<Rectangle> _rectangles = new List<Rectangle>()
    {
        new Rectangle(1700, 0, 960, 0),
    };
    
    public MainWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;
        ShowRectanglesSequentially();
    }
    
    private async void ShowRectanglesSequentially()
    {
        foreach (var rectData in _rectangles)
        {
            var rect = new Avalonia.Controls.Shapes.Rectangle()
            {
                Width = rectData.GetWidthHeight().Item1,
                Height = rectData.GetWidthHeight().Item2,
                Fill = new SolidColorBrush(Colors.White),
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);

            MainCanvas.Children.Add(rect);

            await Task.Delay(2000); 
        }

        /*foreach (var point in Execution().Item1)
        {
            var dot = new Ellipse
            {
                Width = 3,
                Height = 3,
                Fill = Brushes.Blue
            };
            //TimeSpan timeSpan = new TimeSpan(0,0,0,0,1);
            //await Task.Delay(timeSpan);
            Canvas.SetLeft(dot,ConvertLongitudeIntoX( point.Longitude)); // center the circle
            Canvas.SetTop(dot, ConvertLatitudeIntoY( point.Latitude));
            MainCanvas.Children.Add(dot);
        }*/
        
        foreach (var (pointMedian,point1, point2) in Execution().Item2)
        {
            var dot = new Ellipse
            {
                Width = 3,
                Height = 3,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(dot,ConvertLongitudeIntoX( pointMedian.Longitude)); 
            Canvas.SetTop(dot, ConvertLatitudeIntoY( pointMedian.Latitude));
            MainCanvas.Children.Add(dot);
            //await Task.Delay(1);
            var line = new Line
            {
                StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(point1.Longitude),ConvertLatitudeIntoY(point1.Latitude)),
                EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(point2.Longitude),ConvertLatitudeIntoY(point2.Latitude)),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            MainCanvas.Children.Add(line);
        }
        Point targerPoint = new Point(50.212, 35.872);
        
        var target = new Ellipse
        {
            Width = 5,
            Height = 5,
            Fill = Brushes.Lime
        };
        Canvas.SetLeft(target,ConvertLongitudeIntoX( targerPoint.Longitude)); 
        Canvas.SetTop(target, ConvertLatitudeIntoY( targerPoint.Latitude));
        MainCanvas.Children.Add(target);

        
        
        
        var targerrect = Execution().Item3.First();
        
        var linet1 = new Line
        {
            StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MaxLongitude),ConvertLatitudeIntoY(targerrect.MaxLatitude)),
            EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MinLongitude),ConvertLatitudeIntoY(targerrect.MaxLatitude)),
            Stroke = Brushes.Magenta,
            StrokeThickness = 3
        };
        var linet2 = new Line
        {
            StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MaxLongitude),ConvertLatitudeIntoY(targerrect.MaxLatitude)),
            EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MaxLongitude),ConvertLatitudeIntoY(targerrect.MinLatitude)),
            Stroke = Brushes.Magenta,
            StrokeThickness = 3
        };
        var linet3 = new Line
        {
            StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MinLongitude),ConvertLatitudeIntoY(targerrect.MinLatitude)),
            EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MinLongitude),ConvertLatitudeIntoY(targerrect.MaxLatitude)),
            Stroke = Brushes.Magenta,
            StrokeThickness = 3
        };
        var linet4 = new Line
        {
            StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MinLongitude),ConvertLatitudeIntoY(targerrect.MinLatitude)),
            EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(targerrect.MaxLongitude),ConvertLatitudeIntoY(targerrect.MinLatitude)),
            Stroke = Brushes.Magenta,
            StrokeThickness = 3
        };
            
        MainCanvas.Children.Add(linet1);
        MainCanvas.Children.Add(linet2);
        MainCanvas.Children.Add(linet3);
        MainCanvas.Children.Add(linet4);

        await Task.Delay(1000); 
        
        
        var allrectangles = Execution().Item3;
        allrectangles.RemoveAt(0);
        foreach (var rectData in allrectangles)
        {
            var line1 = new Line
            {
                StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MaxLongitude),ConvertLatitudeIntoY(rectData.MaxLatitude)),
                EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MinLongitude),ConvertLatitudeIntoY(rectData.MaxLatitude)),
                Stroke = Brushes.Gold,
                StrokeThickness = 2
            };
            var line2 = new Line
            {
                StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MaxLongitude),ConvertLatitudeIntoY(rectData.MaxLatitude)),
                EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MaxLongitude),ConvertLatitudeIntoY(rectData.MinLatitude)),
                Stroke = Brushes.Gold,
                StrokeThickness = 2
            };
            var line3 = new Line
            {
                StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MinLongitude),ConvertLatitudeIntoY(rectData.MinLatitude)),
                EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MinLongitude),ConvertLatitudeIntoY(rectData.MaxLatitude)),
                Stroke = Brushes.Gold,
                StrokeThickness = 2
            };
            var line4 = new Line
            {
                StartPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MinLongitude),ConvertLatitudeIntoY(rectData.MinLatitude)),
                EndPoint = new Avalonia.Point(ConvertLongitudeIntoX(rectData.MaxLongitude),ConvertLatitudeIntoY(rectData.MinLatitude)),
                Stroke = Brushes.Gold,
                StrokeThickness = 2
            };
            
            MainCanvas.Children.Add(line1);
            MainCanvas.Children.Add(line2);
            MainCanvas.Children.Add(line3);
            MainCanvas.Children.Add(line4);

            await Task.Delay(1000); 
        }
        
        
    }

    public static double ConvertLongitudeIntoX(double longitude)
    {
        double maxLongitude = 40.726570000000002;
        return (maxLongitude - longitude) * 91.397849462365591;
    }
    
    public static double ConvertLatitudeIntoY(double latitude)
    {
        double maxLatitude = 56.170189999999998;
        return (maxLatitude - latitude) * 71.295952469;
    }
        
    public static (HashSet<Point>,List<(Point,Point,Point)>,List<Rectangle>) Execution()
    {
        HashSet<Point> pointsFromFile = new HashSet<Point>();
        Dictionary<Point, double> PointsInRadious = new Dictionary<Point, double>();
        Point initialPoint = new Point(50.45332, 30.63516);
        var lines = File.ReadAllLines(
            $"/Users/ivanlukianets/Documents/C#_projects_for_KSE/AvaloniaApplication1/AvaloniaApplication1/Data/ukraine_poi.csv");
        
        foreach (var line in lines)
        {
            var splitedline = line.Split(";");
            double Latitude = Convert.ToDouble(splitedline[0]);
            double Longitude = Convert.ToDouble(splitedline[1]);
            List<string> entries = splitedline.Skip(2).ToList();
            entries.RemoveAll(x => x is "" or " ");
            pointsFromFile.Add(new Point(Latitude, Longitude, entries));
        }
        
        
        foreach (var VARIABLE in PointsInRadious.OrderBy(x => x.Value).Take(10))
        {
            Console.WriteLine(VARIABLE.Key.ToString()+ $"{VARIABLE.Value}");
        }
        
        var rootNodeRectangle = KdTree.CreateKdTreeRectangle(pointsFromFile.ToList());
        Point targerPoint = new Point(50.212, 35.872);
        var result = KdTree.SearchKdTreeRectangle(rootNodeRectangle, targerPoint, 20,10);
        return (pointsFromFile,KdTree.listToPrintBuilding,KdTree.listToPrintRectangles);
        Console.WriteLine(SizeOfTree(rootNodeRectangle));
        Console.WriteLine(pointsFromFile.Count);
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

    public static int SizeOfTree(Node? rootNode)
    {
        if (rootNode == null)
        {
            return 0;
        }

        return 1 + SizeOfTree(rootNode.RightSon) + SizeOfTree(rootNode.LeftSon);

    }
}