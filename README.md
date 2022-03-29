# QuickConf

C# ini reader/writer, sorta. works with serialization, solely. I think it's a really neat and easy to apply concept.

## Usage

apply to a class the attribute ``ConfigurationFile``, and ``Serializeable`` if you plan to write the class back to a config file
then, apply the ``ConfigurationField`` attribute to the members you wish to have stuff from the configuration file put in there. they can be assigned a group and a name, but the name must not be blank

these attributes can be used on private members. they'll work fine

what follows is an example implementation, in C#

```ini
# myconfigfile.conf
Value1=a

[Group1]
Value2=b
```

```c#
using System.IO;

[ConfigurationFile, Serializeable] 
class MyConfigurationFile
{
    [ConfigurationField(Name = "Value1")]
    public string Value1 { get; internal set; }

    [ConfigurationField(Group = "Group1", Name = "Value2")]
    public string Value2 { get; set; }
}

class Program
{
    static void Main()
    {
        // serializer.
        ConfigurationSerializer<MyConfigurationFile> serializer = new();
        MyConfigurationFile file = serializer.Deserialize(File.ReadAllLines("myconfigfile.conf"));

        Console.WriteLine($"Value 1 is {file.Value1}.");
        Console.WriteLine($"Value 2 is {file.Value2}.");
    }
}
```

## Todo

- Ability to mark certain fields as required.
- Ability to preserve the original file data (comments, etc.).