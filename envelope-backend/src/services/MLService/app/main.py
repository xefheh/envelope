from fastapi import FastAPI, Depends, HTTPException
from sqlalchemy.ext.asyncio import AsyncSession
from database import AsyncSessionLocal, engine, Base
from schemas.schemas import TagCreate, ClassificationRequest, ClassificationResponse
from services.ClassificationService import ClassificationService
from services.TagService import TagService

app = FastAPI()
classificationService=ClassificationService()

async def init_db():
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)

@app.on_event("startup")
async def startup():
    await init_db()

async def get_db():
    async with AsyncSessionLocal() as session:
        yield session

@app.post("/tags/", response_model=TagCreate)
async def create_tag(tag: TagCreate, db: AsyncSession = Depends(get_db)):
    tagService = TagService(db)
    createdTag = await tagService.addTag(tag.name)
    if createdTag:
        return createdTag
    raise HTTPException(status_code=400, detail="Tag already exists")

@app.post("/classify/", response_model=ClassificationResponse)
async def classify_text(request: ClassificationRequest, db: AsyncSession = Depends(get_db)):
    tagService = TagService(db)
    tags = await tagService.getAllTags()
    candidateLabels = [tag.name for tag in tags]
    classifiedTags = classificationService.classifyText(request.text, candidateLabels)
    return ClassificationResponse(tags=classifiedTags)