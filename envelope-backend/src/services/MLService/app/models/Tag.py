from sqlalchemy import Column, Integer, String
from database import Base

class Tag(Base):
    __tablename__ = "tags"
    #старое tags
    #решить проблему с бд
    id = Column(Integer, primary_key=True, index=True)
    name = Column(String, unique=True, index=True)
