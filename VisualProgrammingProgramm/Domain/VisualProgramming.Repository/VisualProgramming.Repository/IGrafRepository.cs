using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Graf"/> (графов).
/// </summary>
/// <remarks>
/// Наследует базовый CRUD от <see cref="IRepository{Graf, Guid}"/>.
/// Предоставляет методы для получения элементов графа, принадлежащих определённому графу.
/// </remarks>
public interface IGrafRepository : IRepository<Graf, Guid>
{
    /// <summary>
    /// Асинхронно получает элемент графа, принадлежащий указанному графу, по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор графа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит элемент графа <see cref="ElementGraf"/>, 
    /// принадлежащий графу, или null, если граф не найден или не содержит элементов.
    /// </returns>
    /// <remarks>
    /// Обратите внимание на опечатку в имени метода: <c>GetElementGafByIdGrafAsync</c>.
    /// Рекомендуется переименовать в <c>GetElementGrafByIdGrafAsync</c>.
    /// </remarks>
    Task<ElementGraf?> GetElementGafByIdGrafAsync(Guid id, CancellationToken cancellationToken);
}