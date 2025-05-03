from abc import ABC, abstractmethod
from typing import List, Optional, TypeVar, Any
from sqlalchemy.orm import Session
from sqlalchemy import select, delete
from IRepository import IRepository

T = TypeVar('T')

class SQLAlchemyRepository(IRepository[T], ABC):
    
    def __init__(self, session: Session):
        self._session = session
    
    @property
    @abstractmethod
    def _model(self):
        pass
    
    def get_by_id(self, id: Any) -> Optional[T]:
        return self._session.get(self._model, id)
    
    def get_all(self) -> List[T]:
        stmt = select(self._model)
        return list(self._session.scalars(stmt).all())
    
    def find(self, **filters) -> List[T]:
        stmt = select(self._model).filter_by(**filters)
        return list(self._session.scalars(stmt).all())
    
    def add(self, entity: T) -> T:
        self._session.add(entity)
        self._session.flush()
        return entity
    
    def add_range(self, entities: List[T]) -> None:
        self._session.add_all(entities)
        self._session.flush()
    
    def update(self, entity: T) -> None:
        self._session.merge(entity)
        self._session.flush()
    
    def delete(self, entity: T) -> None:
        self._session.delete(entity)
        self._session.flush()
    
    def delete_by_id(self, id: Any) -> bool:
        stmt = delete(self._model).where(self._model.id == id)
        result = self._session.execute(stmt)
        self._session.flush()
        return result.rowcount > 0