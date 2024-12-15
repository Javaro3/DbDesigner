using Common.Extensions;

namespace Tests.Common;

[TestFixture]
public class StringExtensionTests
{
    [TestCase("hello_world", "HelloWorld")]
    [TestCase("convert-to-pascal-case", "ConvertToPascalCase")]
    [TestCase("this:is:a:test", "ThisIsATest")]
    [TestCase("multiple    spaces", "MultipleSpaces")]
    [TestCase("", "")]
    public void ConvertToPascalCase_ShouldReturnExpectedResults(string input, string expected)
    {
        var result = input.ConvertToPascalCase();
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("box", "boxes")]
    [TestCase("church", "churches")]
    [TestCase("buzz", "buzzes")]
    [TestCase("lady", "ladies")]
    [TestCase("wolf", "wolves")]
    [TestCase("knife", "knives")]
    [TestCase("cat", "cats")]
    [TestCase("dog", "dogs")]
    [TestCase("", "")]
    public void ToPlural_ShouldReturnExpectedResults(string input, string expected)
    {
        var result = input.ToPlural();
        Assert.That(result, Is.EqualTo(expected));
    }
}