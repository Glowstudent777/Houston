using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Houston.Bot.Services;

public class BotService : DiscordShardedClient
{
	private DiscordShardedClient _client;

	public BotService()
	{
		_client = new DiscordShardedClient(new DiscordSocketConfig()
		{
			GatewayIntents = GatewayIntents.All,
		});
	}

	public async Task Initialize()
	{
		_client.Log += LogAsync;

		await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
		await _client.SetGameAsync("Glowstudent do stuff", null, ActivityType.Watching);

		await _client.StartAsync();

		_client.ShardReady += OnShardReady;
	}

	private async Task OnShardReady(DiscordSocketClient client)
	{
		if (client.ShardId != 0) return;
		Console.WriteLine($"Logged in as {client.CurrentUser.Username}({client.CurrentUser.Id})");
	}

	private static Task LogAsync(LogMessage log)
	{
		if (log.Exception is GatewayReconnectException)
		{
			if (log.Exception.InnerException is not null)
				Console.WriteLine(log.Exception.InnerException.Message);
			return Task.CompletedTask;
		}

		Console.WriteLine(log.ToString());
		return Task.CompletedTask;
	}
}
