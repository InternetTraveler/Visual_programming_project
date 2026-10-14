using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Servises.Abstraction.Base;

/// <summary>
/// Базовый сервис приложения для CRUD-операций над моделью <typeparamref name="TModel"/>.
/// </summary>
/// <typeparam name="TModel">Модель приложения, реализующая <see cref="IModel{TId}"/>.</typeparam>
/// <typeparam name="TCreateModel">Модель создания, реализующая <see cref="ICreateModel"/>.</typeparam>
/// <typeparam name="TId">Тип идентификатора (struct, <see cref="IEquatable{T}"/>).</typeparam>
public interface IApplicationService<TModel, TCreateModel, in TId>
     where TModel : IModel<TId>
     where TId : struct, IEquatable<TId>
     where TCreateModel : ICreateModel
{
    /// <summary>
    /// Получает модель по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель или <see langword="null"/>, если не найдена.</returns>
    Task<TModel?> GetModelByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все модели.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция моделей.</returns>
    Task<IEnumerable<TModel>> GetModelsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт модель.
    /// </summary>
    /// <param name="userInformation">Модель создания.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданная модель или <see langword="null"/> при ошибке.</returns>
    Task<TModel?> CreateModelAsync(TCreateModel userInformation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет модель.
    /// </summary>
    /// <param name="user">Модель с новыми данными.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see langword="true"/>, если обновлено; иначе <see langword="false"/>.</returns>
    Task<bool> UpdateModelAsync(TModel user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет модель по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see langword="true"/>, если удалено; иначе <see langword="false"/>.</returns>
    Task<bool> DeleteModelAsync(TId id, CancellationToken cancellationToken = default);
}