# WebDucer Translation library for Xamarin.Forms

Library with an Service to get and set the current application languge and to get the device language. It includes an XAML extension, to translate text in XAML with resources of the project (one or more resource).

## States

| Service | Current Status |
| :------ | -------------: |
| AppVeyor last | [![Build status last](https://ci.appveyor.com/api/projects/status/i0rx89ds0nsp8yhx?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-translations) |
| AppVeyor develop | [![Build status develop](https://ci.appveyor.com/api/projects/status/i0rx89ds0nsp8yhx/branch/develop?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-translations/branch/develop) |
| AppVeyor master | [![Build status master](https://ci.appveyor.com/api/projects/status/i0rx89ds0nsp8yhx/branch/master?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-translations/branch/master) |
| SonarCube coverage - master | [![SonarQube Coverage](https://sonarcloud.io/api/project_badges/measure?project=WD.Translations&metric=coverage)](https://sonarcloud.io/dashboard?id=WD.Translations) |
| SonarCube technical debt - master | [![SonarQube Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=WD.Translations&metric=sqale_index)](https://sonarcloud.io/dashboard?id=WD.Translations) |
| SonarCube Quality Gate - master | [![SonarQube Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=WD.Translations&metric=alert_status)](https://sonarcloud.io/dashboard?id=WD.Translations) |
| SonarCube coverage - develop | [![SonarQube Coverage](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Translations&metric=coverage)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Translations) |
| SonarCube technical debt - develop | [![SonarQube Technical Debt](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Translations&metric=sqale_index)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Translations) |
| SonarCube Quality Gate - develop | [![SonarQube Quality Gate](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Translations&metric=alert_status)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Translations) |
| NuGet stable | [![NuGet](https://img.shields.io/nuget/v/WD.Translations.svg)](https://www.nuget.org/packages/WD.Translations) |
| NuGet pre | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/WD.Translations.svg)](https://www.nuget.org/packages/WD.Translations) |
| NuGet downloads | [![NuGet](https://img.shields.io/nuget/dt/WD.Translations.svg)](https://www.nuget.org/packages/WD.Translations) |

## Services

- Abstractions:
  - `IPlatformCultureInfo` - Inteface to get the culture information from the running platform (current app culture and OS culture)
  - `IResourceManagersSource` - Interface for the resources source singleton to use in DI
  - `ITranslationService` - Translation service to be used in view models over dependency injection
- Implementations
  - `TranslationExtension` - XAML extension for translations
  - `TranslationService` - Implementation for translation service interface (uses `IResourceManagerSource` ans `IPlatformCultureInfo`)

## Sample

Init `ResourceManagersSource` in your `App.xaml.cs` to be able to use this in XAML and code. Register as **Singleton** or **Instance** in your depency injection framework, if you use one. Register `

### App.caml.cs

```csproj
// Create Singleton (without DI)
protected override async void OnInitialized()
{
    InitializeComponent();

    var resourceSource = new ResourceManagersSource(AppResources.ResourceManager).Current;

    mainPage = new NavigationPage(new MainPage());
}

// Register in DI
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.RegisterInstance(ResourceManagersSource.Init(AppResources.ResourceManager));
    containerRegistry.RegisterInstance(Plugin.Multilingual.CrossMultilingual.Current);
    containerRegistry.RegisterSingleton<ITranslationService, TranslationService>();

    containerRegistry.RegisterForNavigation<NavigationPage>();
    containerRegistry.RegisterForNavigation<MainPage>();
}
```

### Usage in XAML-Files

```xml
```

### Usage in ViewModels

```csharp
public MyVieModel(ITranslationService translationService)
{
  _translServ = translationService;
}

public string Title
{
  get { return _translServ.Translate("PAGE_TITLE"); }
}
```