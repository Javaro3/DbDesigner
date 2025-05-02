import { ICommand, ICommandHandler, IQuery, IQueryHandler } from "./Interfaces";

export abstract class CQRS {
  private commandHandlers: Map<string, ICommandHandler<ICommand, any>>;
  private queryHandlers: Map<string, IQueryHandler<IQuery, any>>;

  constructor() {
    this.commandHandlers = new Map();
    this.queryHandlers = new Map();
  }

  protected registerCommandHandler<TCommand extends ICommand<TResult>, TResult>(
    commandType: string,
    handler: ICommandHandler<TCommand, TResult>
  ): void {
    this.commandHandlers.set(commandType, handler);
  }

  protected registerQueryHandler<TQuery extends IQuery<TResult>, TResult>(
    queryType: string,
    handler: IQueryHandler<TQuery, TResult>
  ): void {
    this.queryHandlers.set(queryType, handler);
  }

  async executeCommand<TResult>(command: ICommand<TResult>): Promise<TResult> {
    const commandName = command.constructor.name;
    const handler = this.commandHandlers.get(commandName);

    if (!handler) {
      throw new Error(`Handler for command ${commandName} not registered`);
    }

    if (command.validate) {
      const isValid = await command.validate();
      if (!isValid) {
        throw new Error(`Command ${commandName} validation failed`);
      }
    }

    return handler.execute(command);
  }

  async executeQuery<TResult>(query: IQuery<TResult>): Promise<TResult> {
    const queryName = query.constructor.name;
    const handler = this.queryHandlers.get(queryName);

    if (!handler) {
      throw new Error(`Handler for query ${queryName} not registered`);
    }

    return handler.execute(query);
  }
}