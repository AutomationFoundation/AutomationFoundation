using System;
using NUnit.Framework;

namespace AutomationFoundation.Hosting;

[TestFixture]
public class DefaultHostingEnvironmentBuilderTests
{
    [Test]
    public void ThrowsAnExceptionWhenTheEnvironmentNameIsNull()
    {
        var target = new DefaultHostingEnvironmentBuilder();
        Assert.Throws<ArgumentNullException>(() => target.SetEnvironmentName(null));
    }

    [Test]
    public void ThrowsAnExceptionWhenTheEnvironmentNameIsEmpty()
    {
        var target = new DefaultHostingEnvironmentBuilder();
        Assert.Throws<ArgumentNullException>(() => target.SetEnvironmentName(""));
    }

    [Test]
    public void ThrowsAnExceptionWhenTheEnvironmentNameIsWhitespace()
    {
        var target = new DefaultHostingEnvironmentBuilder();
        Assert.Throws<ArgumentNullException>(() => target.SetEnvironmentName("     "));
    }

    [Test]
    public void DefaultsTheEnvironmentNameToDevelopment()
    {
        var target = new DefaultHostingEnvironmentBuilder();
        var result = target.Build();

        Assert.AreEqual("Development", result.EnvironmentName);
    }

    [Test]
    public void SetsTheEnvironmentNameAsExpected()
    {
        var expected = "MyEnvironment";

        var target = new DefaultHostingEnvironmentBuilder();
        target.SetEnvironmentName(expected);

        var result = target.Build();

        Assert.AreEqual(expected, result.EnvironmentName);
    }

    [Test]
    public void UsesTheEnvironmentNameVariableIfNotConfigured()
    {
        var expected = "MyEnvironmentVariable2";

        try
        {
            Environment.SetEnvironmentVariable(DefaultHostingEnvironmentBuilder.EnvironmentNameVariable, expected);

            var target = new DefaultHostingEnvironmentBuilder();
            var result = target.Build();

            Assert.AreEqual(expected, result.EnvironmentName);
        }
        finally
        {
            Environment.SetEnvironmentVariable(DefaultHostingEnvironmentBuilder.EnvironmentNameVariable, null);
        }
    }
}