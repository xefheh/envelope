using TagManagement.Domain.Enums;

namespace TagManagement.Application.Request
{
    public class TagRequest
    {
        /// <summary>
        /// Суррогатный ключ
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Название тэга
        /// </summary>
        public required string TagName { get; set; }

        /// <summary>
        /// Тип тэга
        /// </summary>
        public required TagType TagType { get; set; }

        /// <summary>
        /// Суррогатный ключ другой сущности
        /// </summary>
        public required Guid EntityId { get; set; }
    }
}
