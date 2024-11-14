using Envelope.Common.ResultPattern;
using TagManagement.Application.Exceptions;
using TagManagement.Application.Request;
using TagManagement.Application.Response;
using TagManagement.Domain.Entities;
using TagManagement.Application.Repositories;
using Envelope.Common.Exceptions;

namespace TagManagement.Application.Services
{
    public class TagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Result<Guid>> AddTagAsync(TagRequest request)
        {
            var exception = CheckTagData(request);

            if (exception is not null)
            {
                return Result<Guid>.OnFailure(exception);
            }
          
            var tag = new Tag()
            {
                Id=request.TagId,
                Name=request.TagName,
                Type=request.TagType,
                EntityId=request.EntityId,
            };

            var result = new TagDTO()
            {
                TagId = tag.Id,
                TagName = tag.Name,
                TagType = tag.Type,
                EntityId = tag.EntityId,
            };

            await _tagRepository.AddTagAsync(tag);
            return Result<Guid>.OnSuccess(result.TagId);
        }

        public async Task<Result<bool>> RemoveTagAsync(TagRequest request)
        {
            var tag = new Tag()
            {
                Id = request.TagId
            };          

            var result = new TagDTO()
            {
                TagId = tag.Id,
            };

            var isDeleted=await _tagRepository.RemoveTagAsync(result.TagId);

            return isDeleted ?
                Result<bool>.OnFailure(new NotFoundException(typeof(Tag), result.TagId)) :
                Result<bool>.OnSuccess(true);
        }


        public async Task<Result<IEnumerable<TagDTO>>> GetTagsForEntityAsync(TagRequest request)
        {
            var exception = CheckTagData(request);

            if (exception is not null)
            {
                return Result<IEnumerable<TagDTO>>.OnFailure(exception);
            }

            var tag = new Tag()
            {
                Id = request.TagId,
                Name = request.TagName,
                Type = request.TagType,
                EntityId = request.EntityId,
            };

            var tags = await _tagRepository.GetTagsForEntityAsync(tag.EntityId);

            var result = tags.Select(tag => new TagDTO()
            {
                TagId = tag?.Id ?? Guid.Empty,
                TagName = tag?.Name,
                TagType = tag?.Type ?? default,
                EntityId = tag?.EntityId ?? Guid.Empty,
            }).ToList();

            return Result<IEnumerable<TagDTO>>.OnSuccess(result);
        }
        private Exception? CheckTagData(TagRequest request)
        {
            if (String.IsNullOrEmpty(request.TagName))
            {
                return new InvalidNameException($"Invalid tag name: {request.TagName}");
            }
            return null;
        }
    }
}
