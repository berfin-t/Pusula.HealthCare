using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class EfCoreProtocolTypeRepository : EfCoreRepository<HealthCareDbContext, ProtocolType, Guid>,
                                            IProtocolTypeRepository
{
    public EfCoreProtocolTypeRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<ProtocolType> FindByNameAsync(string name)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.ProtocolTypes.FirstOrDefaultAsync(pt => pt.Name == name);
    }

    public async Task<long> GetCountAsync(
        string? name = null,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), name)
            .LongCountAsync(GetCancellationToken(cancellationToken));

    public async Task<ProtocolTypeWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) =>
        await (await GetQueryableForNavigationPropertiesAsync())
            .FirstOrDefaultAsync(e => e.ProtocolType.Id == id, GetCancellationToken(cancellationToken));

    public async Task<List<ProtocolType>> GetListAsync(
        string? name = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), name)
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<List<ProtocolTypeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? name = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableForNavigationPropertiesAsync(), name)
              .OrderBy(GetSorting(sorting, true))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    protected virtual async Task<IQueryable<ProtocolTypeWithNavigationProperties>>
        GetQueryableForNavigationPropertiesAsync()
    {
        var dbContext = await GetDbContextAsync();
        return
            from protocolType in dbContext.ProtocolTypes
            join protocolTypeAction in dbContext.ProtocolTypeActions
                on protocolType.Id equals protocolTypeAction.ProtocolTypeId
                into protocolTypeActions
            from protocolTypeAction in protocolTypeActions.DefaultIfEmpty()
            select new ProtocolTypeWithNavigationProperties()
            {
                ProtocolType = protocolType,
                ProtocolTypeActions = protocolTypeActions
            };
    }

    protected virtual IQueryable<ProtocolType> ApplyFilter(
        IQueryable<ProtocolType> query,
        string? name = null
    ) =>
        query
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));

    protected virtual IQueryable<ProtocolTypeWithNavigationProperties> ApplyFilter(
        IQueryable<ProtocolTypeWithNavigationProperties> query,
        string? name = null
    ) =>
        query
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.ProtocolType.Name.Contains(name!));

    protected virtual string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            ProtocolTypeConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "ProtocolType." : string.Empty)}{sorting}";
}