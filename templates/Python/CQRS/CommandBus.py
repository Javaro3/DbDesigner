from typing import Dict, Type, Any
from CQRS import Command, ICommandHandler

class CommandBus:
    def __init__(self):
        self._handlers: Dict[Type[Command], ICommandHandler] = {}
    
    def register_handler(self, command_type: Type[Command], handler: ICommandHandler):
        self._handlers[command_type] = handler
    
    async def dispatch(self, command: Command) -> Any:
        handler = self._handlers.get(type(command))
        if not handler:
            raise ValueError(f"No handler registered for command {type(command).__name__}")
        return await handler.handle(command)