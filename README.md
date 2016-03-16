# SimplePersistence.UoW.EF
SimplePersistence.UoW.EF offers implementations to the SimplePersistence.UoW using the Entity Framework 6 as the ORM.

## Installation
This library can be installed via NuGet package. Just run the following command:

```powershell
Install-Package SimplePersistence.UoW.EF -Pre
```

## Usage

```csharp
public class ApplicationRepository : EFQueryableRepository<Application, string>, IApplicationRepository
{
	public ApplicationRepository(DbContext dbContext) 
		: base(dbContext)
	{
	}

	#region Overrides of EFQueryableRepository<Application,string>

	public override IQueryable<Application> QueryById(string id)
	{
		return Query().Where(e => e.Id == id);
	}

	#endregion
}

public class LevelRepository : EFQueryableRepository<Level, string>, ILevelRepository
{
	public LevelRepository(DbContext dbContext) 
		: base(dbContext)
	{
	}

	#region Overrides of EFQueryableRepository<Level,string>

	public override IQueryable<Level> QueryById(string id)
	{
		return Query().Where(e => e.Id == id);
	}

	#endregion
}

public class LogRepository : EFQueryableRepository<Log, long>, ILogRepository
{
	public LogRepository(DbContext dbContext) 
		: base(dbContext)
	{
	}

	#region Overrides of EFQueryableRepository<Log,long>

	public override IQueryable<Log> QueryById(long id)
	{
		return Query().Where(e => e.Id == id);
	}

	#endregion
	
	public async Task<IEnumerable<Log>> GetAllCreatedAfterAsync(DateTimeOffset on, CancellationToken ct)
	{
		return await Query().Where(e => e.CreatedOn >= on).ToArrayAsync(ct);
	}
}

public class LoggingWorkArea : EFWorkArea<LogisticsContext>, ILoggingWorkArea
{
	public LoggingWorkArea(LogisticsContext context)
		: base(context)
	{
		Applications = new ApplicationRepository(context);
		Levels => new LevelRepository(context);
		Logs => new LogRepository(context);
	}

	public IApplicationRepository Applications { get; }

	public ILevelRepository Levels { get; }

	public ILogRepository Logs { get; }
} 
```
