// Базовый интерфейс для команды
export interface ICommand<TResult = any> {
  validate?(): Promise<boolean> | boolean;
}

// Базовый интерфейс для запроса
export interface IQuery<TResult = any> {}

// Интерфейс обработчика команды
export interface ICommandHandler<TCommand extends ICommand<TResult>, TResult = any> {
  execute(command: TCommand): Promise<TResult>;
}

// Интерфейс обработчика запроса
export interface IQueryHandler<TQuery extends IQuery<TResult>, TResult = any> {
  execute(query: TQuery): Promise<TResult>;
}