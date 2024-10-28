from sqlalchemy.orm import Session
from sentence_transformers import SentenceTransformer
from repositories.TagRepositories import TagRepository
import numpy as np
from sklearn.metrics.pairwise import cosine_similarity

class TagService:
    def __init__(self, db: Session):
        self.repository = TagRepository(db)
        self.model = SentenceTransformer('all-MiniLM-L6-v2')  # Предобученная модель для получения векторов

    async def getAllTags(self):
        return await self.repository.get_tags()

    async def addTag(self, tagName: str):
        if not await self.isSimilarTagExists(tagName):
            return await self.repository.add_tag(tagName)
        return None

    async def isSimilarTagExists(self, tagName: str, threshold: float = 0.7):
        existingTags = await self.repository.get_tags()
        existingTagNames = [tag.name for tag in existingTags]

        if not existingTagNames:
            return False
        # Получаем векторы для существующих тегов и нового тега
        existingVectors = self.model.encode(existingTagNames)
        newVector = self.model.encode([tagName])

        # Проверяем размеры векторов
        if existingVectors.ndim == 1:
            existingVectors = existingVectors.reshape(1, -1)
            
        # Вычисляем косинусное сходство
        similarities = cosine_similarity(newVector, existingVectors)

        # Проверяем, есть ли схожий тег
        return np.any(similarities >= threshold)
