### QuickConf

This is an INI reader. That's about everything you need to know, except for how to use it

## Usage

Write a class like this

```c#
[ConfigurationFile]	// needed 
[Serializable]		// needed IF you're going to serialize it
class MySuperHorribleConfigurationClass
{
	// this attribute can handle 2 arguments
	// it's documented you should probably read it
	[ConfigurationField("", "MyAwesomeField")]
	private string _myAwesomeField;

	// QuickConf only suports fields so you may want to do this
	public string MyAwesomeField => _myAwesomeField;
}
```

Having a class like this allows you to use the two main components of this library:
- **`ConfigurationReader`**: fills fields marked with the `ConfigurationField` attribute, in classes marked with `ConfigurationFile`.
- **`ConfigurationWriter`**: allows you to write them back into multiline strings, but requires the `Serializable` attribute.

## A Better Example

```ini
DatabaseBackend=sqlite

[Sqlite]
DatabaseFilename=alcoholic_beverage.db
UsePassword=false
Password=
```

```c#
namespace Whatchamacallit
{
    [ConfigurationFile]	// needed 
    class DatabaseConfig
    {	    
        [ConfigurationField("", "DatabaseBackend")]
        private string db_backend;

        [ConfigurationField("Sqlite", "DatabaseFilename")]
        private string db_filename;

        [ConfigurationField("Sqlite", "UsePassword")]
        private string db_use_pwd;

        [ConfigurationField("Sqlite", "Password")]
        private string db_password;

        public string DatabaseBackend => db_backend;

        public string SqliteDatabaseFilename => db_filename;

        public bool SqliteUsePassword => db_use_pwd.StartsWith("true") ? true : false;

        public string SqlitePassword => db_pwd;
    }
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseConfig databaseConfig = new DatabaseConfig();
            ConfigurationReader.DeserializeInto(databaseConfig, File.ReadAllLines("databaseconfig.conf"));

            // you know what to do with these lmao            
        }
    }
}
```

## Todo

- Type conversion
- Compliance?
