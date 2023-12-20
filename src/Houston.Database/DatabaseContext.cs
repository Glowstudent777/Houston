using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Houston.Database;

public class DatabaseContext : DbContext
{
	private readonly string _connectionString;

	public DatabaseContext(IConfiguration configuration)
	{
		_connectionString = Environment.GetEnvironmentVariable("DATABASE");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseNpgsql(_connectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	}
}
