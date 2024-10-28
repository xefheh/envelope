from pydantic import BaseModel
from typing import List

class TagCreate(BaseModel):
    name: str

class TagResponse(BaseModel):
    id: int
    name: str

    class Config:
        from_attributes = True

class ClassificationRequest(BaseModel):
    text: str
    n: int

class ClassificationResponse(BaseModel):
    tags: List[str]
