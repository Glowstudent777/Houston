using Microsoft.EntityFrameworkCore;
using Houston.Database.Entities.Base;
using Houston.Database.Entities;
using System.Threading.Tasks;

namespace Houston.Database.Extensions;

public static class DbSetExtensions
{
	public static Task<T> GetForMemberAsync<T>(this DbSet<T> dbSet, ulong guildId, ulong memberId) where T : MemberEntity
	{
		return dbSet.FirstOrDefaultAsync(x => x.GuildId == guildId && x.MemberId == memberId);
	}
}
