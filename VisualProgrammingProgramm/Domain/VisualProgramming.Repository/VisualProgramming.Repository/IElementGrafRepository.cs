using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="ElementGraf"/> (элементов графа).
/// </summary>
/// <remarks>
/// Наследует базовый CRUD от <see cref="IRepository{ElementGraf, Guid}"/>.
/// Предоставляет методы для получения портов, связанных с элементами графа.
/// </remarks>
public interface IElementGrafRepository : IRepository<ElementGraf, Guid>
{
    /// <summary>
    /// Асинхронно получает порт, связанный с указанным элементом графа, по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор элемента графа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит порт <see cref="Port"/>, 
    /// связанный с элементом графа, или null, если элемент графа не найден или не имеет порта.
    /// </returns>
    Task<Port?> GetPortByIdElementGrafAsync(Guid id, CancellationToken cancellationToken);
}