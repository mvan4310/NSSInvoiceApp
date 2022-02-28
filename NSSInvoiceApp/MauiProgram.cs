using Microsoft.AspNetCore.Components.WebView.Maui;
using MudBlazor.Services;
using NSSInvoiceApp.Data;

namespace NSSInvoiceApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.RegisterBlazorMauiWebView()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddBlazorWebView();
		builder.Services.AddMudServices();
		builder.Services.AddSingleton<DataService>();
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTgwNjg5QDMxMzkyZTM0MmUzMGQrMzZaZTB5cllxTWllOWtCYktGam1tOS9qV0VkYzU4Tlg0RHBWeVlqTVU9;NTgwNjkwQDMxMzkyZTM0MmUzMGZvQ01VOHduWEc0ZU9WRWluTHIyTFo4Nm9IL2VsVXB4OXh1ZXJEUXlzOGc9;NTgwNjkxQDMxMzkyZTM0MmUzMGZpZ2p5RHVNMUlPMDdHbHpROCt3NmlieEhpWFVkRXF0bHpqa0oxblBkM0U9;NTgwNjkyQDMxMzkyZTM0MmUzMFhTYzBpOXh5NU5WRVBzQXNkb1R4aDdzZC9pMWpEYjhFM1hnY1hUR3Y2QkU9;NTgwNjkzQDMxMzkyZTM0MmUzMFVvQ2FRVHpEK0dycHZQaGkzanQ2VWpQdVBBNndKeENOQjJNcjhwZE9Oc009;NTgwNjk0QDMxMzkyZTM0MmUzMG5xN2pvTnVsS1RuVUEyQVl4NURHVHZkRjhLbTdBRTlKTVVFQlNPbC9iams9;NTgwNjk1QDMxMzkyZTM0MmUzMGV5Tm9rOUgzbEUrSGV0cTZ3cm53YWtFcW1mWndVdDVHUjZYYmtacjNpZDQ9;NTgwNjk2QDMxMzkyZTM0MmUzMFNlanhNOG9qY2NOS3lyQ1RHaGxyL0wzUnNvWGh1aHlMbjBSNVd1RGVLVzQ9;NTgwNjk3QDMxMzkyZTM0MmUzMGdHYVRHcWc0NGJIMTJsOTByR2V0cnkxbjBiNm0rWTFVanNkejBVQk1oK2c9;NTgwNjk4QDMxMzkyZTM0MmUzMGdWMXlJb3gyOCtFeDZ5T3p5cTBJdG0xbWw4Z0hlZGhRZjJhc1Fzdzducmc9;NTgwNjk5QDMxMzkyZTM0MmUzMGdBK052aTUrb1hLWlFoeElJdHVoR0VObmFVTFVwcEd6MUl1UUxsRS9oa2s9");

		return builder.Build();
	}
}
