using CognitiveServices;

var customVision = new CustomVision();
var imagePath = "image.jpg";

var objects = customVision.DetectObjects(imagePath);

Console.WriteLine("Object - Probability - Position(X,Y)");
foreach (var obj in objects)
{
    Console.WriteLine("{0}: {1} - ({2},{3})",
        obj.TagName,
        obj.Probability,
        obj.BoundingBox.Left,
        obj.BoundingBox.Top);
}