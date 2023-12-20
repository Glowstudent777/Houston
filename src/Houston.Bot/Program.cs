using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Houston.Bot.Services;
using Houston.Database;

namespace Houston.Bot;

public class Program
{
	private static async Task Main(string[] args)
	{
		var services = ConfigureServices();
		var bot = services.GetRequiredService<BotService>();

		await bot.Initialize();

		await Task.Delay(-1);
	}

	private static ServiceProvider ConfigureServices()
	{
		return new ServiceCollection()
			.AddSingleton(new BotService())
			.AddDbContext<DatabaseContext>()
			.BuildServiceProvider();
	}
}