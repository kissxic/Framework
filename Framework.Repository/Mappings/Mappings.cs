using DapperExtensions.Mapper;

namespace Framework.Repository.Mappings
{
    public class Mappings
    {
        public static void Initialize()
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);

            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
            {
                typeof(Mappings).Assembly
            });
        }
    }
}
