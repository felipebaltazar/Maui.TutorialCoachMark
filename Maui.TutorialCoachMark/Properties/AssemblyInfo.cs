﻿using XmlnsPrefixAttribute = Microsoft.Maui.Controls.XmlnsPrefixAttribute;

//Custom xaml schema <see href="https://docs.microsoft.com/pt-br/xamarin/xamarin-forms/xaml/custom-namespace-schemas#defining-a-custom-namespace-schema"/>
[assembly: XmlnsDefinition("http://schemas.microsoft.com/dotnet/2021/maui", "Maui.TutorialCoachMark")]
[assembly: XmlnsDefinition("http://neocontrols.com/schemas/xaml", "Maui.TutorialCoachMark")]

//Recommended prefix <see href="https://docs.microsoft.com/pt-br/xamarin/xamarin-forms/xaml/custom-prefix"/>
[assembly: XmlnsPrefix("http://Maui.TutorialCoachMark.com/schemas/xaml", "tutorial")]