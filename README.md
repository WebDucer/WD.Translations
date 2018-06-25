# WebDucer Translation library for Xamarin.Forms

Library with an Service to get and set the current application languge and to get the device language. It includes an XAML extension, to translate text in XAML with resources of the project (one or more resource).

## States

| Service | Last | Develop | Master |
| :------ | ---: | ------: | -----: |
| AppVeyor | [![Build status last](https://ci.appveyor.com/api/projects/status/i0rx89ds0nsp8yhx?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-translations) | [![Build status develop](https://ci.appveyor.com/api/projects/status/i0rx89ds0nsp8yhx/branch/develop?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-translations/branch/develop) | [![Build status master](https://ci.appveyor.com/api/projects/status/i0rx89ds0nsp8yhx/branch/master?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-translations/branch/master) |
| SonarQube coverage | | [![SonarQube Coverage](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Translations&metric=coverage)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Translations) | [![SonarQube Coverage](https://sonarcloud.io/api/project_badges/measure?project=WD.Translations&metric=coverage)](https://sonarcloud.io/dashboard?id=WD.Translations) |
| SonarQube technical debt | | [![SonarQube Technical Debt](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Translations&metric=sqale_index)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Translations) | [![SonarQube Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=WD.Translations&metric=sqale_index)](https://sonarcloud.io/dashboard?id=WD.Translations) |
| SonarQube quality gate | | [![SonarQube Quality Gate](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Translations&metric=alert_status)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Translations) | [![SonarQube Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=WD.Translations&metric=alert_status)](https://sonarcloud.io/dashboard?id=WD.Translations) |
| Nuget | [![NuGet](https://img.shields.io/nuget/dt/WD.Translations.svg)](https://www.nuget.org/packages/WD.Translations) | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/WD.Translations.svg)](https://www.nuget.org/packages/WD.Translations) | [![NuGet](https://img.shields.io/nuget/v/WD.Translations.svg)](https://www.nuget.org/packages/WD.Translations) |

## Services

- Abstractions:
  - `IResourceManagersSource` - Interface for the resources source singleton to use in DI
  - `ITranslationService` - Translation service to be used in view models over dependency injection
- Implementations
  - `TranslationExtension` - XAML extension for translations
  - `TranslationService` - Implementation for translation service interface (uses `IResourceManagerSource` and `IMultilingual` from [Plugin.Multilingual](https://github.com/CrossGeeks/MultilingualPlugin))

## Sample

Init `ResourceManagersSource` in your `App.xaml.cs` to be able to use this in XAML and code. Register as **Singleton** or **Instance** in your depency injection framework, if you use one. You can register as much of resource managers as you need. The first found translation will be taken. So you can override _common library_ translation with your own, if you register your manager as first.

### App.caml.cs

```csharp
// Create Singleton (without DI)
protected override async void OnInitialized()
{
    InitializeComponent();

    var resourceSource = ResourceManagersSource.Init(
      AppResources.ResourceManager,
      CommonTranslations.ResourceManager); // Add your collection

    mainPage = new NavigationPage(new MainPage());
}

// Register in DI
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.RegisterInstance(ResourceManagersSource.Init(
      AppResources.ResourceManager,
      CommonTranslations.ResourceManager)); // Add your collection of sources
    containerRegistry.RegisterInstance(Plugin.Multilingual.CrossMultilingual.Current);
    containerRegistry.RegisterSingleton<ITranslationService, TranslationService>();

    containerRegistry.RegisterForNavigation<NavigationPage>();
    containerRegistry.RegisterForNavigation<MainPage>();
}
```

### Usage in XAML-Files

```xml
<?xml version="1.0" encoding="utf-8"?>

<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:i18N="clr-namespace:WD.Translations;assembly=WD.Translations"
             Title="{i18N:Translation PAGE_TITLE}">
  <ContentPage.Content>
    <Label Text="{i18N:Translation HELLO_WORLD}"/>
  </ContentPage.Content>
</ContentPage>
```

### Usage in ViewModels

```csharp
public MyVieModel(ITranslationService translationService)
{
  _translServ = translationService;
}

public string Title
{
  get { return _translServ.GetTransalation("PAGE_TITLE"); }
}

public void SomeMethod()
{
  // Translation e.g: "Value have to be between {0} and {1}."
  var validator = new Validator
  {
    _translServ.GetFormattedTranslation("VALIDATION_ERROR_MESSAGE", 0, 500)
  };
}
```