from abc import ABC, abstractmethod
from typing import Generic, TypeVar
from dataclasses import dataclass

T = TypeVar('T')

@dataclass
class Command:
    pass

@dataclass
class Query:
    pass

class ICommandHandler(ABC, Generic[T]):
    @abstractmethod
    async def handle(self, command: Command) -> T:
        pass

class IQueryHandler(ABC, Generic[T]):
    @abstractmethod
    async def handle(self, query: Query) -> T:
        pass

class UnitOfWork(ABC):
    def __enter__(self):
        return self
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        if exc_type is not None:
            self.rollback()
    
    @abstractmethod
    def commit(self):
        pass
    
    @abstractmethod
    def rollback(self):
        pass

class SQLAlchemyUnitOfWork(UnitOfWork):
    def __init__(self, session_factory):
        self.session_factory = session_factory
    
    def __enter__(self):
        self.session = self.session_factory()
        return self
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        super().__exit__(exc_type, exc_val, exc_tb)
        self.session.close()
    
    def commit(self):
        self.session.commit()
    
    def rollback(self):
        self.session.rollback()