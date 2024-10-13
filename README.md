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
    xmlns:t="clr-namespace:Maui.TutorialCoachMark;assembly=Maui.TutorialCoachMark"
    x:Name="page"
    t:Tutorial.EnableTutorial="true">

    <ScrollView>
        <VerticalStackLayout Padding="30,0" Spacing="25">
            <Image
                Aspect="AspectFit"
                HeightRequest="185"
                Source="dotnet_bot.png" />

            <Label
                t:Tutorial.TutorialOrder="1"
                t:Tutorial.TutorialParent="{Reference page}"
                Text="Hello, World!">
                <t:Tutorial.CoachMarkView>
                    <StackLayout>
                        <Label
                            FontAttributes="Bold"
                            Text="Describe your elements!"
                            TextColor="White" />
                    </StackLayout>
                </t:Tutorial.CoachMarkView>
            </Label>
    </ScrollView>
</ContentPage>

```



<p align="center">
	<kbd>
		<img src="https://github.com/felipebaltazar/Maui.TutorialCoachMark/Images/sample.gif" alt="home page" width=45% />
	</kbd>
</p>



## Repo Activity

![Alt](https://repobeats.axiom.co/api/embed/e3457a9dc9131c33ca38ceb2203bfffa67864080.svg "Repo activity analytics image")