from typing import Dict, Type, Any
from CQRS import Query, IQueryHandler

class QueryBus:
    def __init__(self):
        self._handlers: Dict[Type[Query], IQueryHandler] = {}
    
    def register_handler(self, query_type: Type[Query], handler: IQueryHandler):
        self._handlers[query_type] = handler
    
    async def dispatch(self, query: Query) -> Any:
        handler = self._handlers.get(type(query))
        if not handler:
            raise ValueError(f"No handler registered for query {type(query).__name__}")
        return await handler.handle(query)