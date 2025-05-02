from abc import ABC, abstractmethod
from typing import Generic, TypeVar, List, Optional, Any

T = TypeVar('T')

class IRepository(Generic[T], ABC):
    """Абстрактный интерфейс репозитория"""
    
    @abstractmethod
    def get_by_id(self, id: Any) -> Optional[T]:
        pass
    
    @abstractmethod
    def get_all(self) -> List[T]:
        pass
    
    @abstractmethod
    def find(self, **filters) -> List[T]:
        pass
    
    @abstractmethod
    def add(self, entity: T) -> T:
        pass
    
    @abstractmethod
    def add_range(self, entities: List[T]) -> None:
        pass
    
    @abstractmethod
    def update(self, entity: T) -> None:
        pass
    
    @abstractmethod
    def delete(self, entity: T) -> None:
        pass
    
    @abstractmethod
    def delete_by_id(self, id: Any) -> bool:
        pass