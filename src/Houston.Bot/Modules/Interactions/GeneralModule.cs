using Discord;
using Discord.Interactions;
using Houston.Database;
using Houston.Database.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Bot.Modules.Interactions;

public class GeneralModule : InteractionsBase
{
	public GeneralModule() { }

	[SlashCommand("ping", "Display bot latency")]
	public async Task LatencyAsync()
	{
		await DeferAsync();

		var message = await FollowupAsync("📡 Ping 1");

		await message.ModifyAsync(x => x.Content = "📡 Ping 2");
		var latency = (message.CreatedAt.Millisecond - message.EditedTimestamp.Value.Millisecond);

		while (latency < 0)
		{
			var initLat = (message.EditedTimestamp.Value.Millisecond);
			await message.ModifyAsync(x => x.Content = "Another ping???");
			var finalLat = (message.EditedTimestamp.Value.Millisecond);
			latency = (finalLat - initLat);
		}

		await message.ModifyAsync(x =>
		{
			x.Content = $"⏱️ Message Latency: {latency}\n🛰️ Websocket Latency: {Context.Client.Latency}";
		});
	}
}
