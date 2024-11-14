using TagManagement.Domain.Enums;

namespace TagManagement.Application.Response
{
    public class TagDTO
    {
        /// <summary>
        /// Суррогатный ключ
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Название тэга
        /// </summary>
        public string? TagName { get; set; }

        /// <summary>
        /// Тип тэга
        /// </summary>
        public TagType TagType { get; set; }

        /// <summary>
        /// Суррогатный ключ другой сущности
        /// </summary>
        public Guid EntityId { get; set; }
    }
}
