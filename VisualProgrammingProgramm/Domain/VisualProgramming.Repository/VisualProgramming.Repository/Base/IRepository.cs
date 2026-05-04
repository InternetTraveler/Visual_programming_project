using VisualProgramming.Domain.Base;

namespace VisualProgramming.Repository.Base;

/// <summary>
/// Представляет базовый репозиторий для выполнения 
/// операций CRUD над сущностями.
/// </summary>
/// <typeparam name="TEntity">Тип сущности, наследуемый 
/// от <see cref="Entity{TId}"/>.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности. 
/// Должен быть значимым типом и реализовывать <see cref="IEquatable{T}"/>.</typeparam>
public interface IRepository<TEntity, in TId>
     where TEntity : Entity<TId>
     where TId : struct, IEquatable<TId>
{
    /// <summary>
    /// Асинхронно получает все сущности из хранилища.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="asNoTracking">
    /// Определяет, следует ли отслеживать изменения сущностей.
    /// Значение <c>true</c> повышает производительность
    /// при чтении только для просмотра.
    /// </param>
    /// <returns>Коллекция всех сущностей типа 
    /// <typeparamref name="TEntity"/>.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

    /// <summary>
    /// Асинхронно получает сущность по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Сущность типа <typeparamref name="TEntity"/> с указанным идентификатором,
    /// или <c>null</c>, если сущность не найдена.
    /// </returns>
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно добавляет новую сущность в хранилище.
    /// </summary>
    /// <param name="entity">Добавляемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Добавленная сущность с обновлёнными данными 
    /// (например, сгенерированным идентификатором),
    /// или <c>null</c>, если добавление не удалось.
    /// </returns>
    Task<TEntity?> AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно обновляет существующую сущность в хранилище.
    /// </summary>
    /// <param name="entity">Сущность с обновлёнными данными.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// <c>true</c>, если операция обновления успешно выполнена; иначе <c>false</c>.
    /// </returns>
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно удаляет указанную сущность из хранилища.
    /// </summary>
    /// <param name="entity">Удаляемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// <c>true</c>, если операция удаления успешно выполнена; иначе <c>false</c>.
    /// </returns>
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно удаляет сущность по её идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемой сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// <c>true</c>, если операция удаления успешно выполнена; иначе <c>false</c>.
    /// </returns>
    Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken);
}