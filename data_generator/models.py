from typing import List, Optional
from pydantic import BaseModel

class TableGeneratorRequestDto(BaseModel):
    tableId: int
    rowCount: int

class ColumnPropertyDto(BaseModel):
    id: int
    propertyParams: Optional[str] = None
    columnId: int
    propertyId: int
    propertyName: str = ""
    propertyHasParams: bool = False

class ColumnDto(BaseModel):
    id: int
    name: str = ""
    description: Optional[str] = None
    sqlTypeId: Optional[int] = None
    sqlTypeName: str = ""
    sqlTypeParams: Optional[str] = None
    sqlTypeHasParams: bool = False
    tableId: int
    tableName: str = ""
    properties: List[ColumnPropertyDto] = []

class IndexDto(BaseModel):
    id: int
    description: Optional[str] = None
    indexTypeId: int
    indexTypeName: str = ""
    tableName: str = ""
    columns: List[int] = []
    columnNames: List[str] = []

class RelationDto(BaseModel):
    id: int
    sourceColumn: Optional[ColumnDto] = None
    targetColumn: Optional[ColumnDto] = None
    sourceColumnId: int
    targetColumnId: int
    onDeleteName: str = ""
    onDeleteId: int
    onUpdateName: str = ""
    onUpdateId: int

class TableDto(BaseModel):
    id: int
    name: str = ""
    description: Optional[str] = None
    projectId: int
    columns: List[ColumnDto] = []
    indexes: List[IndexDto] = []

class DataBaseDto(BaseModel):
    id: int
    name: str = ""
    description: Optional[str] = None
    image: str = ""

class ProjectDiagramDto(BaseModel):
    id: int
    name: str = ""
    description: str = ""
    dataBase: Optional[DataBaseDto] = None
    tables: List[TableDto] = []
    relations: List[RelationDto] = []

class DataGeneratorRequestDto(BaseModel):
    project: Optional[ProjectDiagramDto] = None
    generationModelId: int
    generationLanguage: str = ""
    tableGenerateInfos: List[TableGeneratorRequestDto] = []

class PromptModel(BaseModel):
    database: str = ""
    language: str = ""
    tableName: str = ""
    fields: str = ""
    rowCount: int
