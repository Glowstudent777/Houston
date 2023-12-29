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

	[Group("reputation", "User Repuations")]
	public class ReputationGroupModule : InteractionsBase
	{
		private readonly DatabaseContext _dbContext;
		public ReputationGroupModule(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		[SlashCommand("View", "View another members repuation")]
		public async Task RepViewAsync(IUser? user = null)
		{
			await DeferAsync();
			if (user == null) user = Context.User as IUser;

			var repMember = await _dbContext.ReputationMembers.GetForMemberAsync(Context.Guild.Id, user.Id);
			var rep = repMember?.Reputation ?? 0;

			var embed = new EmbedBuilder()
				.WithTitle($"{user.Username}'s Reputation")
				.WithDescription($"Reputation: {rep}")
				.WithColor(Color.Blue)
				.Build();

			await RespondAsync(embeds: new[] { embed });
		}
	}
}
