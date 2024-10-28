from sqlalchemy.orm import Session
from models.Tag import Tag
from sqlalchemy.future import select

class TagRepository:
    def __init__(self, db: Session):
        self.db = db

    async def get_tags(self):
        result = await self.db.execute(select(Tag))
        return result.scalars().all()

    async def add_tag(self, tag_name: str):
        tag = Tag(name=tag_name)
        self.db.add(tag)
        await self.db.commit() 
        await self.db.refresh(tag)  
        return tag
