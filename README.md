# Maui.TutorialCoachMark
Create a beautiful and easy tutorial for your .net maui application.


 [![NuGet](https://img.shields.io/nuget/v/TutorialCoachMark.Maui.svg)](https://www.nuget.org/packages/TutorialCoachMark.Maui/)
 
 [![Build and publish packages](https://github.com/felipebaltazar/Maui.TutorialCoachMark/actions/workflows/PackageCI.yml/badge.svg)](https://github.com/felipebaltazar/Maui.TutorialCoachMark/actions/workflows/PackageCI.yml)


 ## Getting started

- Install the TutorialCoachMark.Maui package

 ```
 Install-Package TutorialCoachMark.Maui -Version 0.0.2-pre
 ```

- Add TutorialCoachMark declaration to your `MauiAppBuilder` and configure it to connect to your API

```csharp
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
            var builder = MauiApp.CreateBuilder();
            builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseTutorialCoachMark();

		return builder.Build();
	}
}
```

- You can now use the TutorialCoachMark extensions defining the coachmark steps and the target view


```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="Maui.TutorialCoachMark.Sample.MainPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    x:Name="page"
    Tutorial.EnableTutorial="true">

    <ScrollView>
        <VerticalStackLayout Padding="30,0" Spacing="25">
            <Image
                Aspect="AspectFit"
                HeightRequest="185"
                Source="dotnet_bot.png" />

            <Label
                Tutorial.TutorialOrder="1"
                Tutorial.TutorialParent="{Reference page}"
                Text="Hello, World!">
                <Tutorial.CoachMarkView>
                    <StackLayout>
                        <Label
                            FontAttributes="Bold"
                            Text="Describe your elements!"
                            TextColor="White" />
                    </StackLayout>
                </Tutorial.CoachMarkView>
            </Label>
    </ScrollView>
</ContentPage>

```



<p align="center">
	<kbd>
		<img src="https://github.com/felipebaltazar/Maui.TutorialCoachMark/blob/main/Images/sample.gif" alt="home page" width=45% />
	</kbd>
</p>



## Repo Activity

![Alt](https://repobeats.axiom.co/api/embed/c403bea6cd53d846917c19507a78080847b09ce6.svg "Repobeats analytics image")
