namespace MovieWebApi.service.mapper
{
    public class MapperUtil<D, E>
    {
        private readonly Func<E, D> _toDtoMapper;
        private readonly Func<D, E> _toEntityMapper;

        public MapperUtil(Func<E, D> toDtoMapper, Func<D, E> toEntityMapper)
        {
            _toDtoMapper = toDtoMapper;
            _toEntityMapper = toEntityMapper;
        }

        public D MapToDTO(E entity)
        {
            return _toDtoMapper(entity);
        }

        public E MapToEntity(D dto)
        {
            return _toEntityMapper(dto);
        }
    }
}
