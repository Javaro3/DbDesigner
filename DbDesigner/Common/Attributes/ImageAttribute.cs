namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ImageAttribute : Attribute
{
    public string Image { get; }

    public ImageAttribute(string image)
    {
        Image = image;
    }
}