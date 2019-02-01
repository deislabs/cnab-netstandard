# .NET Standard 2.0 Client Library for [CNAB](https://github.com/deislabs/cnab-spec)

Work in Progress library for working with Cloud Native Application Bundles in C#.

## Usage

```csharp
static async Task Main(string[] args)
{
    var bundle = await Bundle.LoadUnsignedAsync("bundles/thin-bundle.json");

    Console.WriteLine(
        JsonConvert.SerializeObject(bundle, Formatting.Indented));
}
```

## Contributing

In order to contribute to this project, you need .NET Core SDK 2.2:

```bash
$ dotnet --version
2.2.103

$ dotnet build

$ cd examples && dotnet run
An example 'thin' helloworld Cloud-Native Application Bundle
```

If you want to contribute, any of the following is a great starting point:

- adding unit tests
- building invocation images
- installing bundles
- adding support for claims and signing.

The [CNAB Specification](https://github.com/deislabs/cnab-spec) and [Duffle, the reference implementation](https://github.com/deislabs/duffle) are a great starting point. Also make sure to check [Porter](https://github.com/deislabs/porter) and [Docker App](https://github.com/garethr/docker-app-cnab-examples).