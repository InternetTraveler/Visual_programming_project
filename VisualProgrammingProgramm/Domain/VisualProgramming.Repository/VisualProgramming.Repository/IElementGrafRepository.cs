using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="ElementGraf"/> (элементов графа).
/// Наследует базовый CRUD от <see cref="IRepository{ElementGraf, Guid}"/>.
/// </summary>
public interface IElementGrafRepository : IRepository<ElementGraf, Guid>
{
    /// <summary>
    /// Асинхронно получает элемент графа по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор элемента графа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденный элемент графа, или <c>null</c>, если элемент 
    /// с указанным идентификатором не существует.
    /// </returns>
    Task<ElementGraf?> GetElementGrafByIdAsync(Guid id, CancellationToken cancellationToken);
}