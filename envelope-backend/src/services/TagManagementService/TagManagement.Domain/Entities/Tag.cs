using TagManagement.Domain.Enums;

namespace TagManagement.Domain.Entities
{
    public class Tag
    {
        /// <summary>
        /// Суррогатный ключ
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название тэга
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Тип тэга
        /// </summary>   
        public TagType Type { get; set; }

        /// <summary>
        /// Суррогатный ключ другой сущности
        /// </summary>
        public Guid EntityId { get; set; }
    }
}
