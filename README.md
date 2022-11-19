# DomainServices

# Data Acess Logic

- Add to Web API appSettings.json:
```json
"ConnectionStrings": {
    "DefaultConnection": "Data Source=.; Initial Catalog=ShoppingLikeFlies; Integrated Security=True",
}
```

- call AddDataAccessLayer() in Startup.cs

- DTOs, Mapping folder place?

- Mappings:

```C#
services.AddSingleton(_ => MapperConfig.ConfigureAutoMapper());
IShoppingLikeFliesDbContext context = default; //comes from DI
IMapper mapper = default; //comes from DI

List<CaffDTO> caffDTOs= await context.Caff.ProjectTo<CaffDTO>(mapper.ConfigurationProvider).ToListAsync();

CaffDTO caffDTO_1 = context.Caff.ProjectTo<CaffDTO>(mapper.ConfigurationProvider).FirstOrDefault();

CaffDTO caffDTO_2 = context.Caff.ProjectTo<CaffDTO>(mapper.ConfigurationProvider).Single(cDTO => cDTO.Id == 2);

List<Caff> caffs = default; //Comes from db
List<CaffDTO> caffDTOList = mapper.Map<List<CaffDTO>>(caffs);

Caff caff= default; //Comes from db
CaffDTO caffDTO = mapper.Map<CaffDTO>(caff);
```

- DB creation ([commands](https://www.entityframeworktutorial.net/efcore/pmc-commands-for-ef-core-migration.aspx)): 
*VS Package Manager Console* -> 

Default project: DataAcessLogic

Commands example:
```shell
Add-Migration init
Update-Database

Remove-Migration
```
